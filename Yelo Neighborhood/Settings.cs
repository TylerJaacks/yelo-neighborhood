using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Yelo.Neighborhood
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();

            checkAutoDiscover.Checked = Properties.Settings.Default.AutoDiscover;
            txtIP.Text = Properties.Settings.Default.XBoxIP;
        }

        void checkAutoDiscover_CheckedChanged(object sender, EventArgs e)
        { txtIP.Enabled = !checkAutoDiscover.Checked; }

        void cmdConnect_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoDiscover = checkAutoDiscover.Checked;
            Properties.Settings.Default.XBoxIP = txtIP.Text;
            Properties.Settings.Default.Save();

            Hide();
            DialogResult = DialogResult.OK;

            if (checkAutoDiscover.Checked) Program.FindXBox();
            else Program.FindXBox(txtIP.Text);
        }

        private void cmdCheckForUpdates_Click(object sender, EventArgs e)
        {
            Updater.Jobs.VersionDownloadDirectory = new Uri("http://open-sauce.googlecode.com/hg/Xbox/Xbox1/Yelo%20Neighborhood/Latest%20Release/");
            Updater.Jobs.UpdateDownloadDirectory = new Uri("http://open-sauce.googlecode.com/files/");
            Updater.Jobs.ProgramName = "Yelo%20Neighborhood";
            Updater.Jobs.CheckForUpdates(Program.Version);
        }
    }
}