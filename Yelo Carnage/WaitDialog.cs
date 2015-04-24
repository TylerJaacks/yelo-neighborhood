using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace Yelo.Carnage
{
    public partial class WaitDialog : Form
    {
        public WaitDialog()
        {
            InitializeComponent();
        }

        static WaitDialog wd = new WaitDialog();
        public static void Show(string message)
        {
            wd.Text = message;
            wd.Show(); 
        }

        public static void ShowDialog(string message)
        {
            wd.Text = message;
            wd.ShowDialog();
        }

        public new static void Hide()
        { ((Form)wd).Hide(); }

        public new static bool Visible { get { return ((Form)wd).Visible; } }

        private void WaitDialog_Shown(object sender, EventArgs e)
        {
            Location = new Point(
                Screen.PrimaryScreen.WorkingArea.Right - Size.Width - 5
                , Screen.PrimaryScreen.WorkingArea.Bottom - Size.Height - 5
                );
        }
    };
}