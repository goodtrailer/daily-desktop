// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using System;
using System.Diagnostics;
using System.Windows.Forms;
using DailyDesktop.Core;
using DailyDesktop.Core.Providers;

namespace DailyDesktop.Desktop
{
    public partial class MainForm : Form
    {
        private const string NULL_DESCRIPTION = "No description.";

        private DailyDesktopCore core = new DailyDesktopCore();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            providerComboBox.SelectedItem = core.CurrentProvider;
            providerComboBox.SelectedText = core.CurrentProvider?.DisplayName;
            updateProviderInfo();

            enabledCheckBox.Checked = core.Enabled;
            updateTimePicker.Value = core.UpdateTime;
            updateBlurStrengthToolTip();
        }

        private void providerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProviderComboboxItem item = providerComboBox.SelectedItem as ProviderComboboxItem;
            core.CurrentProvider = item.Provider;
            updateProviderInfo();
        }

        private void updateProviderInfo()
        {
            providerDescriptionLabel.Text = core.CurrentProvider?.Description ?? NULL_DESCRIPTION;
            providerSourceLinkLabel.Text = core.CurrentProvider?.SourceUri;
        }

        private void providerComboBox_DropDown(object sender, EventArgs e)
        {
            providerComboBox.Items.Clear();
            foreach (IProvider provider in core.Providers)
            {
                ProviderComboboxItem item = new ProviderComboboxItem(provider);
                providerComboBox.Items.Add(item);
            }
        }

        private void providerSourceLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = providerSourceLinkLabel.Text,
                UseShellExecute = true,
            };
            Process.Start(psi);
        }

        private void updateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            core.UpdateTime = updateTimePicker.Value;
        }

        private void enabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            core.Enabled = enabledCheckBox.Checked;
        }

        private void updateWallpaperButton_Click(object sender, EventArgs e)
        {
            core.UpdateWallpaper();
        }

        private void providersDirectoryButton_Click(object sender, EventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = core.ProvidersDirectory,
                UseShellExecute = true,
            };
            Process.Start(psi);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void blurStrengthTrackBar_Scroll(object sender, EventArgs e)
        {
            updateBlurStrengthToolTip();
        }

        private void updateBlurStrengthToolTip()
        {
            string strength = (blurStrengthTrackBar.Value / 100f).ToString("0.00");
            mainToolTip.SetToolTip(blurStrengthTrackBar, strength);
        }
    }
}
