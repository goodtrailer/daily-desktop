// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using DailyDesktop.Core;
using DailyDesktop.Core.Providers;
using Microsoft.Win32.TaskScheduler;

namespace DailyDesktop.Desktop
{
    public partial class MainForm : Form
    {
        private const string APP_DATA_DIR = "Daily Desktop";
        private const string PROVIDERS_DIR = "providers";
        private const string SERIALIZE_JSON_DIR = "";
        private const string LICENSE_FILENAME = "LICENSE";
        private const string EULA_FILENAME = "EULA";

        private const string TASK_NAME_PREFIX = "Daily Desktop";

        private const string NULL_DESCRIPTION = "No description.";
        private const string NULL_TEXT = "null";
        private const string FETCHED_TEXT = "fetched on";

        private DailyDesktopCore core;
        private WallpaperInfo wallpaper;
        private TaskState previousState;

        public MainForm()
        {
            string appDataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), APP_DATA_DIR);
            string providersDir = Path.Combine(appDataDir, PROVIDERS_DIR);
            string serializeJsonDir = Path.Combine(appDataDir, SERIALIZE_JSON_DIR);

            string userId = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string userName = userId.Split('\\').Last();
            string taskName = $"{TASK_NAME_PREFIX}_{userName}";

            core = new DailyDesktopCore(providersDir, serializeJsonDir, taskName, true);
            InitializeComponent();

            wallpaperDescriptionRichTextBox.LinkClicked += wallpaperDescriptionRichTextBox_LinkClicked;
            overviewRichTextBox.LinkClicked += overviewRichTextBox_LinkClicked;
            licenseRichTextBox.LinkClicked += licenseRichTextBox_LinkClicked;

            string baseDirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                ?? throw new NullReferenceException("Assembly directory could not be found.");

            string baseDir = new Uri(baseDirName).AbsolutePath;
            string licenseUri = $"file:///{baseDir}/{LICENSE_FILENAME}";
            string eulaUri = $"file:///{baseDir}/{EULA_FILENAME}";
            licenseRichTextBox.Text = string.Format(licenseRichTextBox.Text, licenseUri, eulaUri);
        }

        private void openUri(string? uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
                return;

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = $"\"{HttpUtility.UrlDecode(uri)}\"",
                UseShellExecute = true,
            };
            Process.Start(psi);
        }

        private void MainForm_Load(object? _, EventArgs e)
        {
            repopulateProviderComboBox();
            if (core.CurrentProvider != null)
                providerComboBox.SelectedIndex = providerComboBox.FindString(core.CurrentProvider.ToString());
            updateProviderInfo();

            optionsEnabledCheckBox.Checked = core.Enabled;

            optionsUpdateTimePicker.Value = core.UpdateTime;
            optionsUpdateTimePicker.Enabled = optionsEnabledCheckBox.Checked;

            optionsResizeCheckBox.Checked = core.DoResize;

            optionsBlurredFitCheckBox.Checked = core.DoBlurredFit;
            optionsBlurStrengthTrackBar.Value = core.BlurStrength;
            optionsBlurStrengthTrackBar.Enabled = optionsBlurredFitCheckBox.Checked;
            updateBlurStrengthToolTip();

            stateBackgroundWorker.RunWorkerAsync();
        }

        private void MainForm_FormClosing(object? _, EventArgs e)
        {
            stateBackgroundWorker.CancelAsync();
        }

        private void providerComboBox_SelectedIndexChanged(object? _, EventArgs e)
        {
            core.CurrentProvider = providerComboBox.SelectedItem as ProviderWrapper;
            updateProviderInfo();
        }

        private void updateProviderInfo()
        {
            providerDescriptionLabel.Text = core.CurrentProvider?.Provider.Description ?? NULL_DESCRIPTION;
            providerSourceLinkLabel.Text = core.CurrentProvider?.Provider.SourceUri;
        }

        // The most over-engineered vaguely O(n) algorithm of all time that will
        // almost certainly run slower than a normal O(n^2) algorithm for any
        // normal use-case where there are less than 5 providers.
        // 
        // tbh idek why i did this O(n^2) isn't even that unreasonable...
        private void repopulateProviderComboBox()
        {
            var items = new Dictionary<string, int>();
            var providers = core.Providers;

            for (int i = 0; i < providerComboBox.Items.Count; i++)
            {
                var provider = providerComboBox.Items[i] as ProviderWrapper;

                if (provider != null)
                    items.Add(provider.DllPath, i);
            }

            foreach (var keyVal in providers)
            {
                if (!items.ContainsKey(keyVal.Key))
                    try
                    {
                        var provider = IProvider.Instantiate(keyVal.Value);
                        var item = new ProviderWrapper(keyVal.Key, provider);
                        providerComboBox.Items.Add(item);
                    }
                    catch (ProviderException ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
            }

            foreach (var keyVal in items)
            {
                if (!core.Providers.ContainsKey(keyVal.Key))
                    providerComboBox.Items.RemoveAt(keyVal.Value);
            }
        }

        private void providerComboBox_DropDown(object? _, EventArgs e) => repopulateProviderComboBox();

        private void providerSourceLinkLabel_LinkClicked(object? _, LinkLabelLinkClickedEventArgs e) => openUri(providerSourceLinkLabel.Text);

        private void optionsUpdateTimePicker_ValueChanged(object? _, EventArgs e)
        {
            core.UpdateTime = optionsUpdateTimePicker.Value;
        }

        private void optionsEnabledCheckBox_CheckedChanged(object? _, EventArgs e)
        {
            core.Enabled = optionsEnabledCheckBox.Checked;
            optionsUpdateTimePicker.Enabled = optionsEnabledCheckBox.Checked;
        }

        private void optionsUpdateWallpaperButton_Click(object? _, EventArgs e)
        {
            core.UpdateWallpaper();
        }

        private void optionsProvidersDirectoryButton_Click(object? _, EventArgs e) => openUri(core.ProvidersDirectory);

        private void optionsBlurStrengthTrackBar_Scroll(object? _, EventArgs e)
        {
            core.BlurStrength = optionsBlurStrengthTrackBar.Value;
            updateBlurStrengthToolTip();
        }

        private void updateBlurStrengthToolTip()
        {
            string strength = (optionsBlurStrengthTrackBar.Value / 100f).ToString("0.00");
            mainToolTip.SetToolTip(optionsBlurStrengthTrackBar, strength);
        }

        private void optionsBlurredFitCheckBox_CheckedChanged(object? _, EventArgs e)
        {
            core.DoBlurredFit = optionsBlurredFitCheckBox.Checked;
            optionsBlurStrengthTrackBar.Enabled = optionsBlurredFitCheckBox.Checked;
        }

        private void optionsResizeCheckBox_CheckedChanged(object? _, EventArgs e)
        {
            core.DoResize = optionsResizeCheckBox.Checked;
        }

        private void wallpaperTitleLinkLabel_LinkClicked(object? _, LinkLabelLinkClickedEventArgs e) => openUri(wallpaper.TitleUri);

        private void wallpaperAuthorLinkLabel_LinkClicked(object? _, LinkLabelLinkClickedEventArgs e) => openUri(wallpaper.AuthorUri);

        private void updateWallpaperInfo()
        {
            try
            {
                string jsonString = File.ReadAllText(core.WallpaperInfoJsonPath);
                wallpaper = JsonSerializer.Deserialize<WallpaperInfo>(jsonString);
                string updateDate = wallpaper.Date.ToString("dddd, MMMM d");
                wallpaperUpdatedLabel.Text = FETCHED_TEXT + (updateDate ?? NULL_TEXT);
                wallpaperTitleLinkLabel.Text = wallpaper.Title ?? NULL_TEXT;
                wallpaperAuthorLinkLabel.Text = wallpaper.Author ?? NULL_TEXT;
                string text = wallpaper.Description ?? NULL_DESCRIPTION;
                wallpaperDescriptionRichTextBox.Text = Regex.Replace(text, "(?<=[^\r])\n", "\r\n");

                wallpaperTitleLinkLabel.Links[0].Enabled = Uri.TryCreate(wallpaper.TitleUri, UriKind.Absolute, out _);
                wallpaperTitleLinkLabel.TabStop = wallpaperTitleLinkLabel.Links[0].Enabled;
                wallpaperAuthorLinkLabel.Links[0].Enabled = Uri.TryCreate(wallpaper.AuthorUri, UriKind.Absolute, out _);
                wallpaperAuthorLinkLabel.TabStop = wallpaperAuthorLinkLabel.Links[0].Enabled;
            }
            catch (Exception e) when (e is JsonException or FileNotFoundException)
            {
                Console.WriteLine(e.StackTrace);

                wallpaperUpdatedLabel.Text = $"{FETCHED_TEXT} {NULL_TEXT}";
                wallpaperTitleLinkLabel.Text = NULL_TEXT;
                wallpaperAuthorLinkLabel.Text = NULL_TEXT;
                wallpaperDescriptionRichTextBox.Text = NULL_DESCRIPTION;

                wallpaperTitleLinkLabel.Links[0].Enabled = false;
                wallpaperAuthorLinkLabel.Links[0].Enabled = false;
            }
        }

        private void stateBackgroundWorker_DoWork(object? _, EventArgs e)
        {
            while (!stateBackgroundWorker.CancellationPending)
            {
                if (previousState != core.TaskState)
                {
                    stateBackgroundWorker.ReportProgress((int)core.TaskState);
                    previousState = core.TaskState;
                }
            }
        }

        private void stateBackgroundWorker_ProgressChanged(object? _, ProgressChangedEventArgs e)
        {
            TaskState state = (TaskState)e.ProgressPercentage;
            stateLabel.Text = state.ToString();

            if (state == TaskState.Ready)
                updateWallpaperInfo();
        }

        private void okButton_Click(object? _, EventArgs e)
        {
            Application.Exit();
        }

        private void wallpaperDescriptionRichTextBox_LinkClicked(object? _, LinkClickedEventArgs e) => openUri(e.LinkText);

        private void overviewRichTextBox_LinkClicked(object? _, LinkClickedEventArgs e) => openUri(e.LinkText);

        private void licenseRichTextBox_LinkClicked(object? _, LinkClickedEventArgs e) => openUri(e.LinkText);
    }
}
