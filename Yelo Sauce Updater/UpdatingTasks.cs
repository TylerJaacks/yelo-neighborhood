using System;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace Yelo.Updater
{
    public static class UpdatingTasks
    {
        public static Uri VersionDownloadDirectory { get; set; }
        public static Uri UpdateDownloadDirectory { get; set; }
        public static string ProgramLocation { get; set; }
        public static int CurrentVersion { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0) return;
            else
            {
                VersionDownloadDirectory = new Uri(args[0]);
                UpdateDownloadDirectory = new Uri(args[1]);
                ProgramLocation = args[2];
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Downloader(args[3]));
            }
        }

        public static void CheckForUpdates()
        { 
            WebClient wc = new WebClient();
            wc.OpenReadCompleted += new OpenReadCompletedEventHandler(wc_OpenReadCompleted);
            wc.OpenReadAsync(new Uri(VersionDownloadDirectory, "Version.txt"));
        }

        static void wc_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
			int latest;
			using (var sr = new StreamReader(e.Result))
			{
				latest = Convert.ToInt32(sr.ReadLine());
			}

            if (latest > CurrentVersion)
            {
                if (MessageBox.Show("Update Is Available, Download Now?", "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo(Assembly.GetExecutingAssembly().Location); 
                    startInfo.Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\" \"{3}\"",
                        VersionDownloadDirectory.OriginalString, UpdateDownloadDirectory.OriginalString, ProgramLocation, latest.ToString());

                    Process.Start(startInfo);
                    Application.Exit();
                }
            }
            else MessageBox.Show("You Already Have The Latest Version!", "No Update Available");
        }
    }
}