using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Yelo.Carnage
{
    public partial class ConnectionSettings : Form
    {
        public ConnectionSettings()
        {
            InitializeComponent();

            checkAutoDiscover.Checked = Properties.Settings.Default.AutoDiscover;
            txtIP.Text = Properties.Settings.Default.XBoxIP;
        }

        void checkAutoDiscover_CheckedChanged(object sender, EventArgs e)
        { txtIP.Enabled = !checkAutoDiscover.Checked; }

        void cmdTryAgain_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoDiscover = checkAutoDiscover.Checked;
            Properties.Settings.Default.XBoxIP = txtIP.Text;
            Properties.Settings.Default.Save();

            Hide();
            DialogResult = DialogResult.OK;

            if (checkAutoDiscover.Checked) Program.FindXBox();
            else Program.FindXBox(txtIP.Text);
        }
    }
}