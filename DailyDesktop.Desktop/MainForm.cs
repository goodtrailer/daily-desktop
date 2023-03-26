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
using DailyDesktop.Core.Configuration;
using DailyDesktop.Core.Providers;
using Microsoft.Win32.TaskScheduler;

namespace DailyDesktop.Desktop
{
    public partial class MainForm : Form
    {
        private const string app_data_dir = "Daily Desktop";
        private const string providers_dir = "providers";
        private const string serialization_dir = "";

        private const string path_config_filename = "path.json";
        private const string license_filename = "LICENSE";
        private const string eula_filename = "EULA";

        private const string task_name_prefix = "Daily Desktop";

        private const string null_description = "No description.";
        private const string null_text = "null";
        private const string fetched_text = "fetched on";

        private DailyDesktopCore core;
        private ITaskConfiguration taskConfig => core.TaskConfig;

        private Wallpaper wallpaper;
        private TaskState previousState;

        public MainForm()
        {
            string userId = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string userName = userId.Split('\\').Last();
            string taskName = $"{task_name_prefix}_{userName}";

            string assemblyDirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                ?? throw new NullReferenceException("Assembly directory could not be found.");
            string assemblyDir = Uri.UnescapeDataString(new Uri(assemblyDirName).AbsolutePath);
            

            string appDataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), app_data_dir);
            string providersDir = Path.Combine(appDataDir, providers_dir);
            
            string serializationDir = Path.Combine(appDataDir, serialization_dir);

            core = new DailyDesktopCore(new PathConfiguration(Path.Combine(assemblyDir, path_config_filename))
            {
                AssemblyDir = assemblyDir,
                ProvidersDir = providersDir,
                SerializationDir = serializationDir,
            }, taskName, true);

            InitializeComponent();

            wallpaperDescriptionRichTextBox.LinkClicked += wallpaperDescriptionRichTextBox_LinkClicked;
            overviewRichTextBox.LinkClicked += overviewRichTextBox_LinkClicked;
            licenseRichTextBox.LinkClicked += licenseRichTextBox_LinkClicked;

            string licenseUri = $"file:///{assemblyDir}/{license_filename}";
            string eulaUri = $"file:///{assemblyDir}/{eula_filename}";
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
                providerComboBox.SelectedIndex = providerComboBox.FindString(core.CurrentProvider.DisplayName);
            updateProviderInfo();

            optionsEnabledCheckBox.Checked = taskConfig.IsEnabled;

            optionsUpdateTimePicker.Value = taskConfig.UpdateTime;
            optionsUpdateTimePicker.Enabled = optionsEnabledCheckBox.Checked;

            optionsResizeCheckBox.Checked = taskConfig.DoResize;

            optionsBlurredFitCheckBox.Checked = taskConfig.DoBlurredFit;
            optionsBlurStrengthTrackBar.Value = taskConfig.BlurStrength;
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
            taskConfig.Dll = (providerComboBox.SelectedItem as ProviderWrapper)?.Dll ?? "";
            updateProviderInfo();
        }

        private void updateProviderInfo()
        {
            providerDescriptionLabel.Text = core.CurrentProvider?.Description;
            providerSourceLinkLabel.Text = core.CurrentProvider?.SourceUri;
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
                    items.Add(provider.Dll, i);
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
            taskConfig.UpdateTime = optionsUpdateTimePicker.Value;
        }

        private void optionsEnabledCheckBox_CheckedChanged(object? _, EventArgs e)
        {
            taskConfig.IsEnabled = optionsEnabledCheckBox.Checked;
            optionsUpdateTimePicker.Enabled = optionsEnabledCheckBox.Checked;
        }

        private void optionsUpdateWallpaperButton_Click(object? _, EventArgs e)
        {
            core.UpdateWallpaper();
        }

        private void optionsProvidersDirectoryButton_Click(object? _, EventArgs e) => openUri(core.PathConfig.ProvidersDir);

        private void optionsBlurStrengthTrackBar_Scroll(object? _, EventArgs e)
        {
            taskConfig.BlurStrength = optionsBlurStrengthTrackBar.Value;
            updateBlurStrengthToolTip();
        }

        private void updateBlurStrengthToolTip()
        {
            string strength = (optionsBlurStrengthTrackBar.Value / 100f).ToString("0.00");
            mainToolTip.SetToolTip(optionsBlurStrengthTrackBar, strength);
        }

        private void optionsBlurredFitCheckBox_CheckedChanged(object? _, EventArgs e)
        {
            taskConfig.DoBlurredFit = optionsBlurredFitCheckBox.Checked;
            optionsBlurStrengthTrackBar.Enabled = optionsBlurredFitCheckBox.Checked;
        }

        private void optionsResizeCheckBox_CheckedChanged(object? _, EventArgs e)
        {
            taskConfig.DoResize = optionsResizeCheckBox.Checked;
        }

        private void wallpaperTitleLinkLabel_LinkClicked(object? _, LinkLabelLinkClickedEventArgs e) => openUri(wallpaper.TitleUri);

        private void wallpaperAuthorLinkLabel_LinkClicked(object? _, LinkLabelLinkClickedEventArgs e) => openUri(wallpaper.AuthorUri);

        private void updateWallpaperInfo()
        {
            try
            {
                string jsonString = File.ReadAllText(core.PathConfig.WallpaperJson);
                wallpaper = JsonSerializer.Deserialize<Wallpaper>(jsonString);
                string updateDate = wallpaper.Date.ToString("dddd, MMMM d");
                wallpaperUpdatedLabel.Text = $"{fetched_text} {updateDate ?? null_text}";
                wallpaperTitleLinkLabel.Text = wallpaper.Title ?? null_text;
                wallpaperAuthorLinkLabel.Text = wallpaper.Author ?? null_text;
                string text = wallpaper.Description ?? null_description;
                wallpaperDescriptionRichTextBox.Text = Regex.Replace(text, "(?<=[^\r])\n", "\r\n");

                wallpaperTitleLinkLabel.Links[0].Enabled = Uri.TryCreate(wallpaper.TitleUri, UriKind.Absolute, out _);
                wallpaperTitleLinkLabel.TabStop = wallpaperTitleLinkLabel.Links[0].Enabled;
                wallpaperAuthorLinkLabel.Links[0].Enabled = Uri.TryCreate(wallpaper.AuthorUri, UriKind.Absolute, out _);
                wallpaperAuthorLinkLabel.TabStop = wallpaperAuthorLinkLabel.Links[0].Enabled;
            }
            catch (Exception e) when (e is JsonException or FileNotFoundException)
            {
                Console.WriteLine(e.StackTrace);

                wallpaperUpdatedLabel.Text = $"{fetched_text} {null_text}";
                wallpaperTitleLinkLabel.Text = null_text;
                wallpaperAuthorLinkLabel.Text = null_text;
                wallpaperDescriptionRichTextBox.Text = null_description;

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
