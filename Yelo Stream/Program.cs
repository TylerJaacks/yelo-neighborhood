using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Yelo.Shared;

namespace Yelo.Stream
{
    static class Program
    {
        public static ScreenshotTool MainWindow { get { return _mainWindow; } }
        static ScreenshotTool _mainWindow;

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
                ShowScreenshotTool();
        }

        static void ShowScreenshotTool()
        {
            if (_mainWindow == null)
            {
                _mainWindow = new ScreenshotTool();
                Application.Run(_mainWindow);
            }
        }
    }
}