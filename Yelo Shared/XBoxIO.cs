using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Yelo.Debug;

namespace Yelo.Shared
{
    public static class XBoxIO
    {
        public class LaunchInfo
        {
            public FileInfo FileInfo { get; set; }

            public LaunchInfo(FileInfo fileInfo)
            {
                FileInfo = fileInfo;
            }
        }

        public static Xbox XBox { get; private set; }

        public static bool AutoConnect { get; set; }
        public static string SelectedIP { get; set; }

        public delegate void XBoxConnectedHandler();

        public delegate void StatusChangedHandler(string status);

        public static bool FindXBox()
        {
            if (XBox == null) XBox = new Xbox();
            if (XBox.Ping() == false)
            {
                do
                {
                    if (AutoConnect) AsyncConnect();
                    else AsyncConnect(SelectedIP);

                    Cache.XBoxLocator.ShowDialog(); //Acts as wait

                    if (XBox.Connected)
                        return true;
                }
                while (new Settings().ShowDialog() == DialogResult.OK);
                return false;
            }
            return true;
        }

        static void AsyncConnect(string ip)
        { new Thread(new ParameterizedThreadStart(Connect)).Start(ip); }

        static void AsyncConnect()
        { new Thread(new ThreadStart(Connect)).Start(); }

        static void Connect(object ip)
        {
            try
            { XBox.Connect((string)ip); }
            catch (Exception e)
            { MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally
            { Cache.XBoxLocator.Hide(); }
        }

        static void Connect()
        {
            try
            { XBox.Connect(); }
            catch (Exception e)
            { MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally
            { Cache.XBoxLocator.Hide(); }
        }

        public static void SendDirectory(FileInformation dir, string workingDir, StatusChangedHandler statusChanged)
        {
            if (FindXBox() == false) return;
            string dirname = Path.GetFileName(dir.Name);
            if (!XBox.FileExists(Path.Combine(workingDir, dirname))) XBox.CreateDirectory(Path.Combine(workingDir, dirname));
            foreach (string s in Directory.GetFiles(dir.Name, "*", SearchOption.TopDirectoryOnly))
            {
                FileInformation fi = new FileInformation();
                fi.Name = s;
                SendFile(fi, Path.Combine(workingDir, dirname), statusChanged);
            }
            foreach (string s in Directory.GetDirectories(dir.Name, "*", SearchOption.TopDirectoryOnly))
            {
                FileInformation fi = new FileInformation();
                fi.Name = s;
                SendDirectory(fi, Path.Combine(workingDir, dirname), statusChanged);
            }
        }

        public static void SendFile(FileInformation file, string workingDir, StatusChangedHandler statusChanged)
        {
            if (FindXBox() == false) return;
            string filename = Path.GetFileName(file.Name);
            string xboxFilename = Path.Combine(workingDir, filename);
            if (XBox.FileExists(xboxFilename) && MessageBox.Show(filename + "\n\nWould You Like To Overwrite The Old File?", "File Already Exists.", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
            statusChanged(string.Concat("Sending File: ", filename));

            XBox.SendFile(file.Name, xboxFilename);
        }

        public static void DownloadDirectory(FileInformation dir, string workingDir, StatusChangedHandler statusChanged)
        {
            if (FindXBox() == false) return;
            throw new NotImplementedException("Can Only Download Single Files!");

            //string dirname = Path.GetFileName(dir.Name);
            //if (!Program.XBox.FileExists(Path.Combine(workingDir, dirname))) Program.XBox.CreateDirectory(Path.Combine(workingDir, dirname));
            //foreach (string s in Directory.GetFiles(dir.Name, "*", SearchOption.TopDirectoryOnly))
            //{
            //    FileInformation fi = new FileInformation();
            //    fi.Name = s;
            //    SendFile(fi, Path.Combine(workingDir, dirname));
            //}
            //foreach (string s in Directory.GetDirectories(dir.Name, "*", SearchOption.TopDirectoryOnly))
            //{
            //    FileInformation fi = new FileInformation();
            //    fi.Name = s;
            //    SendDirectory(fi, Path.Combine(workingDir, dirname));
            //}
        }

        public static void DownloadFile(FileInformation file, string workingDir, string destination, StatusChangedHandler statusChanged)
        {
            if (FindXBox() == false) return;
            string xboxFilename = Path.Combine(workingDir, file.Name);
            if (!XBox.FileExists(xboxFilename)) return;
            statusChanged(string.Concat("Downloading File: ", file.Name));

            XBox.ReceiveFile(destination, xboxFilename);
        }

        public static void DeleteDirectory(FileInformation dir, string workingDir, StatusChangedHandler statusChanged)
        {
            if (FindXBox() == false) return;
            List<FileInformation> files = XBox.GetDirectoryList(Path.Combine(workingDir, dir.Name));
            foreach (FileInformation fi in files)
            {
                statusChanged(string.Concat("Deleting: ", fi.Name));
                if (fi.Attributes == FileAttributes.Directory) DeleteDirectory(fi, Path.Combine(workingDir, dir.Name), statusChanged);
                else XBox.DeleteFile(Path.Combine(Path.Combine(workingDir, dir.Name), fi.Name));
            }
            XBox.DeleteDirectory(Path.Combine(workingDir, dir.Name));
        }

        public static void LoadSettings()
        {
            AutoConnect = Properties.Settings.Default.AutoDiscover;
            SelectedIP = Properties.Settings.Default.XBoxIP;
        }
    }
}
