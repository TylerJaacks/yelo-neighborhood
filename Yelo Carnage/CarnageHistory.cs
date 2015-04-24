using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Yelo.Carnage
{
    public partial class CarnageHistory : Form
    {
        public CarnageHistory()
        {
            InitializeComponent();
        }

        private void CarnageHistory_Shown(object sender, EventArgs e)
        { UpdateConnectionInfo(); }

        public void UpdateConnectionInfo()
        { Text = Program.DebugConnectionInfo + " - Carnage History v" + Program.Version; }
    }
}
