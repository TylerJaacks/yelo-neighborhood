using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using System.IO;
using SevenZip;
using System.Collections.Generic;

namespace Yelo.Updater
{
    internal partial class Downloader : Form
    {
        public string Version { get; set; }

        public string UpdateFilename { get { return Version + ".zip"; } }

        public string UpdateURL { get { return UpdatingTasks.UpdateDownloadDirectory + UpdateFilename; } }

        private string DownloadedUpdateFilename { get { return Application.StartupPath + "\\Update.zip"; } }

        public Downloader(string version)
        {
            InitializeComponent();
            Version = version;
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        void Downloader_Load(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();

            using (var sr = new StreamReader(wc.OpenRead(new Uri(UpdatingTasks.VersionDownloadDirectory, "ChangeLog.txt"))))
			{
				lblChangeLog.Text = sr.ReadToEnd();
			}

            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadFileCompleted);
            wc.DownloadFileAsync(new Uri(UpdatingTasks.UpdateDownloadDirectory, UpdateFilename), DownloadedUpdateFilename);
        }

        void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error");
                Application.Exit();
            }
            lblStatus.Text = "Decompressing...";
            probar.Style = ProgressBarStyle.Blocks;
            SevenZipExtractor extractor = new SevenZip.SevenZipExtractor(DownloadedUpdateFilename);
            extractor.Extracting += new EventHandler<ProgressEventArgs>(extractor_Extracting);
            extractor.ExtractionFinished += new EventHandler<EventArgs>(extractor_ExtractionFinished);

            extractor.BeginExtractArchive(Application.StartupPath);
        }

        void extractor_Extracting(object sender, ProgressEventArgs e)
        { probar.Value = e.PercentDone; }

        void extractor_ExtractionFinished(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();
            cmdClose.Visible = true; probar.Visible = false; lblStatus.Visible = false;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(UpdatingTasks.ProgramLocation);
            Application.Exit();
        }

        private void Downloader_FormClosed(object sender, FormClosedEventArgs e)
        { File.Delete(DownloadedUpdateFilename); }
    }
}
