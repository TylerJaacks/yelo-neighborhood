using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Yelo.Shared;

namespace Yelo.Neighborhood
{
    partial class SystemProperties : Form
    {
        public SystemProperties()
        {
            InitializeComponent();

            if (XBoxIO.FindXBox() == false) Close();
            xboxProperties.SelectedObject = XBoxIO.XBox;
        }
    }
}
