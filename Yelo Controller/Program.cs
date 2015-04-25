using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Yelo.Shared;

namespace Yelo.Controller
{
    static class Program
    {
        public static XBoxController MainWindow { get { return _mainWindow; } }
        static XBoxController _mainWindow;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            XBoxIO.LoadSettings();
            if (XBoxIO.FindXBox())
                ShowController();
        }

        static void ShowController()
        {
            if (_mainWindow == null)
            {
                _mainWindow = new XBoxController();
                Application.Run(_mainWindow);
            }
        }
    }
}