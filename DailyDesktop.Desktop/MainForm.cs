using DailyDesktop.Core;
using DailyDesktop.Core.Providers;
using System;
using System.Windows.Forms;

namespace DailyDesktop.Desktop
{
    public partial class MainForm : Form
    {
        private DailyDesktopCore core = new DailyDesktopCore();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            updateTimePicker.Value = core.UpdateTime;
            if (core.CurrentProvider != null)
            {
                providerComboBox.SelectedItem = core.CurrentProvider;
                providerComboBox.SelectedText = core.CurrentProvider.DisplayName;
                providerDescriptionBox.Text = core.CurrentProvider.Description;
            }
            else
            {
                providerComboBox.SelectedItem = null;
                providerDescriptionBox.Text = null;
            }
        }

        private void providerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProviderComboboxItem item = providerComboBox.SelectedItem as ProviderComboboxItem;
            core.CurrentProvider = item.Provider;
            providerDescriptionBox.Text = core.CurrentProvider.Description;
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

        private void updateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            core.UpdateTime = updateTimePicker.Value;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
