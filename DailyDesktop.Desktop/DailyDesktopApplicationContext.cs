using System;
using System.Windows.Forms;

namespace DailyDesktop.Desktop
{
    public class DailyDesktopApplicationContext : ApplicationContext
    {
        private NotifyIcon notifyIcon;

        public DailyDesktopApplicationContext()
        {
            notifyIcon = new NotifyIcon()
            {
                Icon = Properties.Resources.AppIcon,
                ContextMenuStrip = new ContextMenuStrip(),
                Visible = true,
            };
            notifyIcon.ContextMenuStrip.Items.Add("Exit", null, exit);
        }

        private void exit(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            Application.Exit();
        }
    }
}
