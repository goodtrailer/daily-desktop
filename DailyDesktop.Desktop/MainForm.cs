using DailyDesktop.Core;
using DailyDesktop.Core.Providers;
using System;
using System.Diagnostics;
using System.Windows.Forms;

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
            updateTimePicker.Value = core.UpdateTime;
            providerComboBox.SelectedItem = core.CurrentProvider;
            providerComboBox.SelectedText = core.CurrentProvider?.DisplayName;
            enabledCheckBox.Checked = core.Enabled;
            updateProviderInfo();
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
    }
}
