// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using DailyDesktop.Core;
using DailyDesktop.Core.Configuration;
using DailyDesktop.Core.Providers;
using DailyDesktop.Core.Util;
using TaskState = Microsoft.Win32.TaskScheduler.TaskState;

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

        private const string failed_scan_text = "Could not load all providers. (took too long?)";

        private readonly DailyDesktopCore core;
        private readonly WallpaperConfiguration wallpaperConfig;

        private IReadOnlyPathConfiguration pathConfig => core.PathConfig;
        private IPublicTaskConfiguration taskConfig => core.TaskConfig;

        private TaskState previousState;

        public static async Task<MainForm> CreateFormAsync(CancellationToken cancellationToken)
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

            return new MainForm(await DailyDesktopCore.CreateCoreAsync(new PathConfiguration(Path.Combine(assemblyDir, path_config_filename))
            {
                AssemblyDir = assemblyDir,
                ProvidersDir = providersDir,
                SerializationDir = serializationDir,
            }, taskName, true, cancellationToken));
        }

        private MainForm(DailyDesktopCore core)
        {
            this.core = core;
            wallpaperConfig = new WallpaperConfiguration(pathConfig.WallpaperJson);

            InitializeComponent();

            wallpaperDescriptionRichTextBox.LinkClicked += wallpaperDescriptionRichTextBox_LinkClicked;
            overviewRichTextBox.LinkClicked += overviewRichTextBox_LinkClicked;
            licenseRichTextBox.LinkClicked += licenseRichTextBox_LinkClicked;

            string licenseUri = $"file:///{pathConfig.AssemblyDir}/{license_filename}";
            string eulaUri = $"file:///{pathConfig.AssemblyDir}/{eula_filename}";
            licenseRichTextBox.Text = string.Format(licenseRichTextBox.Text, licenseUri, eulaUri);
        }

        private void openUri(string? uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
                return;

            var psi = new ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = $"\"{HttpUtility.UrlDecode(uri)}\"",
                UseShellExecute = true,
            };
            Process.Start(psi);
        }

        private async void MainForm_Load(object? sender, EventArgs e)
        {
            await repopulateProviderComboBox();

            Invoke(() =>
            {
                optionsEnabledCheckBox.Checked = taskConfig.IsEnabled;

                optionsUpdateTimePicker.Value = taskConfig.UpdateTime;
                optionsUpdateTimePicker.Enabled = optionsEnabledCheckBox.Checked;

                optionsResizeCheckBox.Checked = taskConfig.DoResize;

                optionsBlurredFitCheckBox.Checked = taskConfig.DoBlurredFit;
                optionsBlurStrengthTrackBar.Value = taskConfig.BlurStrength;
                optionsBlurStrengthTrackBar.Enabled = optionsBlurredFitCheckBox.Checked;
                updateBlurStrengthToolTip();

                stateBackgroundWorker.RunWorkerAsync();
            });
        }

        private void MainForm_FormClosing(object? sender, EventArgs e)
        {
            stateBackgroundWorker.CancelAsync();
            core.Dispose();

            // TODO: SUPER SUPER SUPER BAD LOL WTF??
            // investigate: maybe has something to do with ProviderStore not
            // properly closing dlls?
            Task.Delay(10_000).ContinueWith(_ => Process.GetCurrentProcess().Kill());
        }

        private async void providerComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (providerComboBox.SelectedItem is not ProviderWrapper p)
                return;

            await taskConfig.SetDllAsync(p.Dll, AsyncUtils.TimedCancel());

            Invoke(() =>
            {
                var provider = core.CurrentProvider;
                providerDescriptionLabel.Text = provider?.Description ?? null_description;
                providerSourceLinkLabel.Text = provider?.SourceUri ?? null_text;
                providerSourceLinkLabel.Links[0].Enabled = Uri.TryCreate(providerSourceLinkLabel.Text, UriKind.Absolute, out _);
                providerSourceLinkLabel.TabStop = wallpaperAuthorLinkLabel.Links[0].Enabled;
            });
        }

        private async Task repopulateProviderComboBox()
        {
            bool isFullyLoaded = true;

            try
            {
                await core.ScanProvidersAsync(AsyncUtils.TimedCancel(3000));
            }
            catch (OperationCanceledException)
            {
                isFullyLoaded = false;
            }

            Invoke(() =>
            {
                providerComboBox.Items.Clear();
                providerComboBox.Items.Add(isFullyLoaded ? ProviderWrapper.Null : failed_scan_text);

                foreach (var keyVal in core.Providers)
                {
                    try
                    {
                        var provider = IProvider.Instantiate(keyVal.Value);
                        var item = new ProviderWrapper(keyVal.Key, provider);
                        providerComboBox.Items.Add(item);
                    }
                    catch (ProviderException pe)
                    {
                        Console.WriteLine(pe.StackTrace);
                    }
                }

                if (core.CurrentProvider != null)
                    providerComboBox.SelectedIndex = providerComboBox.FindString(core.CurrentProvider.DisplayName);
            });
        }

        private async void providerComboBox_DropDown(object? sender, EventArgs e) => await repopulateProviderComboBox();

        private void providerSourceLinkLabel_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e) => openUri(providerSourceLinkLabel.Text);

        private async void optionsUpdateTimePicker_ValueChanged(object? sender, EventArgs e) => await taskConfig.SetUpdateTimeAsync(optionsUpdateTimePicker.Value, AsyncUtils.TimedCancel());

        private async void optionsEnabledCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            await taskConfig.SetIsEnabledAsync(optionsEnabledCheckBox.Checked, AsyncUtils.TimedCancel());
            Invoke(() => optionsUpdateTimePicker.Enabled = optionsEnabledCheckBox.Checked);
        }

        private void optionsUpdateWallpaperButton_Click(object? sender, EventArgs e) => core.UpdateWallpaper();

        private void optionsProvidersDirectoryButton_Click(object? sender, EventArgs e) => openUri(core.PathConfig.ProvidersDir);

        private void optionsBlurStrengthTrackBar_Scroll(object? sender, EventArgs e) => updateBlurStrengthToolTip();

        private async void optionsBlurStrengthTrackBar_MouseUp(object? sender, MouseEventArgs e) => await taskConfig.SetBlurStrengthAsync(optionsBlurStrengthTrackBar.Value, AsyncUtils.TimedCancel());

        private async void optionsBlurStrengthTrackBar_KeyUp(object? sender, KeyEventArgs e) => await taskConfig.SetBlurStrengthAsync(optionsBlurStrengthTrackBar.Value, AsyncUtils.TimedCancel());

        private void updateBlurStrengthToolTip()
        {
            string strength = (optionsBlurStrengthTrackBar.Value / 100f).ToString("0.00");
            mainToolTip.SetToolTip(optionsBlurStrengthTrackBar, strength);
        }

        private async void optionsBlurredFitCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            await taskConfig.SetDoBlurredFitAsync(optionsBlurredFitCheckBox.Checked, AsyncUtils.TimedCancel());
            Invoke(() => optionsBlurStrengthTrackBar.Enabled = optionsBlurredFitCheckBox.Checked);
        }

        private async void optionsResizeCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            await taskConfig.SetDoResizeAsync(optionsResizeCheckBox.Checked, AsyncUtils.TimedCancel());
        }

        private void wallpaperTitleLinkLabel_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e) => openUri(wallpaperConfig.TitleUri);

        private void wallpaperAuthorLinkLabel_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e) => openUri(wallpaperConfig.AuthorUri);

        private void updateWallpaperInfo(bool isDeserializationSuccessful)
        {
            if (isDeserializationSuccessful)
            {
                wallpaperUpdatedLabel.Text = $"{fetched_text} {wallpaperConfig.Date.ToString("dddd, MMMM d") ?? null_text}";
                wallpaperTitleLinkLabel.Text = wallpaperConfig.Title ?? null_text;
                wallpaperAuthorLinkLabel.Text = wallpaperConfig.Author ?? null_text;
                wallpaperDescriptionRichTextBox.Text = Regex.Replace(wallpaperConfig.Description ?? null_description, "(?<=[^\r])\n", "\r\n");

                wallpaperTitleLinkLabel.Links[0].Enabled = Uri.TryCreate(wallpaperConfig.TitleUri, UriKind.Absolute, out _);
                wallpaperTitleLinkLabel.TabStop = wallpaperTitleLinkLabel.Links[0].Enabled;

                wallpaperAuthorLinkLabel.Links[0].Enabled = Uri.TryCreate(wallpaperConfig.AuthorUri, UriKind.Absolute, out _);
                wallpaperAuthorLinkLabel.TabStop = wallpaperAuthorLinkLabel.Links[0].Enabled;
            }
            else
            {
                wallpaperUpdatedLabel.Text = $"{fetched_text} {null_text}";
                wallpaperTitleLinkLabel.Text = null_text;
                wallpaperAuthorLinkLabel.Text = null_text;
                wallpaperDescriptionRichTextBox.Text = null_description;

                wallpaperTitleLinkLabel.Links[0].Enabled = false;
                wallpaperAuthorLinkLabel.Links[0].Enabled = false;
            }
        }

        private void stateBackgroundWorker_DoWork(object? sender, EventArgs e)
        {
            while (!stateBackgroundWorker.CancellationPending)
            {
                if (previousState == core.TaskState)
                    continue;

                stateBackgroundWorker.ReportProgress((int)core.TaskState);
                previousState = core.TaskState;
                Thread.Sleep(100);
            }
        }

        private async void stateBackgroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            var state = (TaskState)e.ProgressPercentage;
            Invoke(() => stateLabel.Text = state.ToString());

            if (state != TaskState.Ready)
                return;

            bool isDeserializationSuccessful = await wallpaperConfig.TryDeserializeAsync(AsyncUtils.TimedCancel());
            Invoke(() => updateWallpaperInfo(isDeserializationSuccessful));
        }

        private void okButton_Click(object? sender, EventArgs e) => Application.Exit();

        private void wallpaperDescriptionRichTextBox_LinkClicked(object? sender, LinkClickedEventArgs e) => openUri(e.LinkText);

        private void overviewRichTextBox_LinkClicked(object? sender, LinkClickedEventArgs e) => openUri(e.LinkText);

        private void licenseRichTextBox_LinkClicked(object? sender, LinkClickedEventArgs e) => openUri(e.LinkText);
    }
}
