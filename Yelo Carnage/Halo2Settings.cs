using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Yelo.Carnage
{
    public partial class Halo2Settings : Form
    {
        public Halo2Settings()
        {
            InitializeComponent();

            txtHalo2Dir.Text = Properties.Settings.Default.Halo2Dir;
        }

        void cmdTryAgain_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Halo2Dir = txtHalo2Dir.Text;
            Properties.Settings.Default.Save();

            DialogResult = DialogResult.OK;
        }
    }
}