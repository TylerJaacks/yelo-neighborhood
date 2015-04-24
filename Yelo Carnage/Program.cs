using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Yelo.Debug;
using System.Threading;
using System.Xml;
using System.IO;
using Yelo.Debug.Exceptions;
using System.ComponentModel;

namespace Yelo.Carnage
{
    static partial class Program
    {
        static bool MainThreadRunning = true;

        public static int Version { get { return 1; } }
        public static string DebugConnectionInfo { get; private set; }

        public static CarnageHistory CarnageHistory { get { return _carnageHistory; } }
        static CarnageHistory _carnageHistory;

        public static MapDownloader MapDownloader { get { return _mapDownloader; } }
        static MapDownloader _mapDownloader;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DebugConnectionInfo = "Unconnected";

            Control.CheckForIllegalCrossThreadCalls = false;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MakeTaskbarIcon();
            Application.Run();

            MainThreadRunning = false;
        }
    };
}