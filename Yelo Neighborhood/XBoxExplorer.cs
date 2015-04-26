using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Yelo.Debug;
using System.IO;
using System.Threading;
using System.Xml;
using Yelo.Shared;
using System.Reflection;
using System.Diagnostics;

namespace Yelo.Neighborhood
{
    public partial class XBoxExplorer : Form
    {
        public enum Images
        {
            HDD = 0,
            Folder = 1,
            XBE = 2,
            Map = 3,
            Bik = 4,
            Other = 5,
        }

        string CurrentDirectory = "";
        Stack<string> DirectoryBackHistory = new Stack<string>();
        Stack<string> DirectoryForwardHistory = new Stack<string>();

        public XBoxExplorer()
        {
            InitializeComponent();
            LoadPartitions();

            Text = XBoxIO.XBox.DebugName + " - " + XBoxIO.XBox.DebugIP + " - Yelo Neighborhood v" + Cache.Version;

            LoadScripts();
        }

        bool FindXbox()
        {
            Enabled = false;
            if (XBoxIO.FindXBox() == false)
            {
                Enabled = true;
                return false;
            }
            return true;
        }

        void CompletedOperation()
        {
            Enabled = true;
            probar.Style = ProgressBarStyle.Blocks;
            StatusChanged("Ready.");
        }

        public void RefreshFiles()
        {
            if (CurrentDirectory == "") LoadPartitions();
            else LoadDirectory(CurrentDirectory);
        }

        public void LoadPartitions()
        {
            if (FindXbox() == false) return;

            CurrentDirectory = "";

			listFiles.BeginUpdate();
            listFiles.Clear();

            ListViewGroup partitionsGroup = new ListViewGroup("Partitions");
            listFiles.Groups.Add(partitionsGroup);

            foreach (string s in XBoxIO.XBox.GetPartitions())
                listFiles.Items.Add(new ListViewItem(s, (int)Images.HDD, partitionsGroup));

            listFiles.LabelEdit = false;
            listFiles.ContextMenuStrip = null;
			listFiles.EndUpdate();
            cmdNewFolder.Enabled = false;

            CompletedOperation();
        }

        public void LoadDirectory(string dir)
        {
            if (FindXbox() == false) return;

            List<FileInformation> files = null;
            try
            { files = XBoxIO.XBox.GetDirectoryList(dir); }
            catch
            {
                MessageBox.Show("Could Not Access: " + dir, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }

            CurrentDirectory = dir;

			listFiles.BeginUpdate();
            listFiles.Clear();
            
            ListViewGroup foldersGroup = new ListViewGroup("Folders");
            ListViewGroup filesGroup = new ListViewGroup("Files");

            listFiles.Groups.Add(foldersGroup);
            listFiles.Groups.Add(filesGroup);
            
            foreach (FileInformation fi in files)
            {
                ListViewItem lvt = new ListViewItem();
                lvt.Tag = fi;

                if (fi.Attributes == FileAttributes.Directory)
                {
                    lvt.Group = foldersGroup;
                    lvt.ImageIndex = (int)Images.Folder;
                }
                else
                {
                    lvt.Group = filesGroup;
                    switch (new FileInfo(fi.Name).Extension)
                    {
                        case ".xbe":
                            lvt.ImageIndex = (int)Images.XBE;
                            break;
                        case ".bik":
                            lvt.ImageIndex = (int)Images.Bik;
                            break;
                        case ".map":
                            lvt.ImageIndex = (int)Images.Map;
                            break;
                        default:
                            lvt.ImageIndex = (int)Images.Other;
                            break;
                    }
                }
                lvt.Text = fi.Name;
                lvt.SubItems.Add(new ListViewItem.ListViewSubItem(lvt, fi.ChangeTime.ToString()));
                lvt.SubItems.Add(new ListViewItem.ListViewSubItem(lvt, fi.Size.ToString()));
                listFiles.Items.Add(lvt);
            }

            listFiles.LabelEdit = true;
            listFiles.ContextMenuStrip = mnuFiles;
			listFiles.EndUpdate();
            cmdNewFolder.Enabled = true;

            CompletedOperation();
        }

        #region Drag-Drop Send Files
        void listFiles_DragEnter(object sender, DragEventArgs e)
        { if (CurrentDirectory != "" && e.Data.GetDataPresent(DataFormats.FileDrop, false)) e.Effect = DragDropEffects.Copy; }

        void listFiles_DragDrop(object sender, DragEventArgs e)
        {
            if (FindXbox() == false) return;

            List<FileInformation> files = new List<FileInformation>();
            foreach (string s in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                FileInformation fi = new FileInformation();
                fi.Name = s;
                if (Directory.Exists(s)) fi.Attributes = FileAttributes.Directory;
                files.Add(fi);
            }

            probar.Style = ProgressBarStyle.Marquee;
            sendFileWorker.RunWorkerAsync(files);
        }

        void sendFileWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string workingDir = CurrentDirectory;
            List<FileInformation> files = (List<FileInformation>)e.Argument;
            foreach (FileInformation fi in files)
            {
                if (fi.Attributes == FileAttributes.Directory) XBoxIO.SendDirectory(fi, workingDir, StatusChanged);
                else XBoxIO.SendFile(fi, workingDir, StatusChanged);
            }
        }
        #endregion

        private void cmdDownload_Click(object sender, EventArgs e)
        {
            if (FindXbox() == false) return;

            FileInformation selectedFile = (FileInformation)listFiles.SelectedItems[0].Tag;
            SFD.FileName = selectedFile.Name;
            SFD.Filter = Path.GetExtension(selectedFile.Name) + "|" + Path.GetExtension(selectedFile.Name);
            if (SFD.ShowDialog() != DialogResult.OK) return;

            probar.Style = ProgressBarStyle.Marquee;
            downloadFileWorker.RunWorkerAsync(new object[] { selectedFile, SFD.FileName });
        }

        private void downloadFileWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            FileInformation selectedFile = (FileInformation)((object[])e.Argument)[0];
            string filename = (string)((object[])e.Argument)[1];
            string workingDir = CurrentDirectory;
            if (selectedFile.Attributes == FileAttributes.Directory) XBoxIO.DownloadDirectory(selectedFile, workingDir, StatusChanged);
            else XBoxIO.DownloadFile(selectedFile, workingDir, SFD.FileName, StatusChanged);
        }
       

        #region Navigation
        void cmdUpDir_Click(object sender, EventArgs e)
        {
            if (FindXbox() == false) return;

            if (CurrentDirectory == "") return;
            
            DirectoryBackHistory.Push(CurrentDirectory);
            cmdBack.Enabled = true;

            DirectoryForwardHistory.Clear();
            cmdForward.Enabled = false;
            
            DirectoryInfo di = new DirectoryInfo(CurrentDirectory);
            if (di.Parent != null) LoadDirectory(di.Parent.FullName);
            else LoadPartitions();
        }

        void cmdBack_Click(object sender, EventArgs e)
        {
            if (FindXbox() == false) return;

            DirectoryForwardHistory.Push(CurrentDirectory);
            string dir = DirectoryBackHistory.Pop();

            if (dir == "") LoadPartitions();
            else LoadDirectory(dir);

            if (DirectoryBackHistory.Count == 0) cmdBack.Enabled = false;
            cmdForward.Enabled = true;
        }

        void cmdForward_Click(object sender, EventArgs e)
        {
            if (FindXbox() == false) return;

            DirectoryBackHistory.Push(CurrentDirectory);
            string dir = DirectoryForwardHistory.Pop();

            if (dir == "") LoadPartitions();
            else LoadDirectory(dir);

            if (DirectoryForwardHistory.Count == 0) cmdForward.Enabled = false;
            cmdBack.Enabled = true;
        }

        void cmdRefresh_Click(object sender, EventArgs e)
        { RefreshFiles(); }
        #endregion

        #region Reboot
        void cmdCyclePower_Click(object sender, EventArgs e)
        {
            if (FindXbox() == false) return;

            try { XBoxIO.XBox.DmReboot(BootFlag.Cold, null); }
            catch { }
            finally
            {
                XBoxIO.FindXBox();
            }

            CompletedOperation();
        }

        void cmdReset_Click(object sender, EventArgs e)
        {
            if (FindXbox() == false) return;

            XBoxIO.XBox.Reset();
            XBoxIO.FindXBox();

            CompletedOperation();
        }

        void cmdShutdown_Click(object sender, EventArgs e)
        {
            if (FindXbox() == false) return;

            try
            { XBoxIO.XBox.Shutdown(); }
            catch { }
            finally
            { Application.Exit(); }
        }

        void cmdCustomReboot_Click(object sender, EventArgs e)
        { }
        #endregion

        #region Tray
        void cmdEjectTray_Click(object sender, EventArgs e)
        {
            if (FindXbox() == false) return;
            XBoxIO.XBox.EjectTray();
            CompletedOperation();
        }

        void cmdLoadTray_Click(object sender, EventArgs e)
        {
            if (FindXbox() == false) return;
            XBoxIO.XBox.LoadTray();
            CompletedOperation();
        }
        #endregion

        #region Update
        void cmdCheckForUpdates_Click(object sender, EventArgs e)
        {
            Cache.CheckForUpdate();
        }
        #endregion

        void listFiles_ItemActivate(object sender, EventArgs e)
        {
            if (FindXbox() == false) return;

            FileInformation selectedFile = (FileInformation)listFiles.SelectedItems[0].Tag;
            if (selectedFile == null || selectedFile.Attributes == FileAttributes.Directory)
            {
                DirectoryBackHistory.Push(CurrentDirectory);
                cmdBack.Enabled = true;

                DirectoryForwardHistory.Clear();
                cmdForward.Enabled = false;

                LoadDirectory(Path.Combine(CurrentDirectory, listFiles.SelectedItems[0].Text));
            }
            else
            {
                FileInfo fi = new FileInfo(Path.Combine(CurrentDirectory, selectedFile.Name));
                switch (fi.Extension)
                {
                    case ".xbe":
                        lblStatus.Text = "Launching: " + fi.Name;
                        probar.Style = ProgressBarStyle.Marquee;
                        this.Enabled = false;
                        launchTitleWorker.RunWorkerAsync(new XBoxIO.LaunchInfo(fi));
                        break;
                }
            }

            CompletedOperation();
        }

        void launchTitleWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            XBoxIO.LaunchInfo lmi = (XBoxIO.LaunchInfo)e.Argument;
            XBoxIO.XBox.LaunchTitle(lmi.FileInfo.FullName);
        }

        void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CompletedOperation();
            RefreshFiles();
        }

        void listFiles_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    cmdDelete_Click(null, null);
                    break;
            }
        }

        void deleteFileWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string workingDir = CurrentDirectory;
            List<FileInformation> files = (List<FileInformation>)e.Argument;
            foreach (FileInformation fi in files)
            {
                lblStatus.Text = "Deleting: " + fi.Name;
                if (fi.Attributes == FileAttributes.Directory) XBoxIO.DeleteDirectory(fi, workingDir, StatusChanged);
                else XBoxIO.XBox.DeleteFile(Path.Combine(workingDir, fi.Name));
            }
        }

        void listFiles_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == "" || e.Label == null)
            {
                e.CancelEdit = true;
                if (listFiles.Items[e.Item].Text == "") listFiles.Items.RemoveAt(e.Item);
                return;
            }

            if (FindXbox() == false) return;

            if (listFiles.Items[e.Item].Text == "")
                XBoxIO.XBox.CreateDirectory(Path.Combine(CurrentDirectory, e.Label));
            else
            {
                XBoxIO.XBox.RenameFile(Path.Combine(CurrentDirectory, listFiles.Items[e.Item].Text), Path.Combine(CurrentDirectory, e.Label));
                ((FileInformation)listFiles.Items[e.Item].Tag).Name = e.Label;
            }

            CompletedOperation();
        }

        void cmdRename_Click(object sender, EventArgs e)
        { listFiles.SelectedItems[0].BeginEdit(); }

        void cmdDelete_Click(object sender, EventArgs e)
        {
            if (CurrentDirectory == "") return;
            if (MessageBox.Show("Would You Like To Permanently Delete " + listFiles.SelectedItems.Count + " File(s)?", "Comfirm.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            if (FindXbox() == false) return;

            List<FileInformation> files = new List<FileInformation>();
            foreach (ListViewItem lvt in listFiles.SelectedItems)
                files.Add((FileInformation)lvt.Tag);
            this.Enabled = false;
            deleteFileWorker.RunWorkerAsync(files);
        }

        void cmdNewFolder_Click(object sender, EventArgs e)
        {
            ListViewItem newFolder = new ListViewItem("", (int)Images.Folder, listFiles.Groups[1]);
            listFiles.Items.Add(newFolder);
            newFolder.BeginEdit();
        }

        #region Executables
        void cmdAddToScriptsMenu_Click(object sender, EventArgs e)
        {
            ExecutableTool ET = new ExecutableTool();
            Executable exe = new Executable();
            exe.Name = "Unknown";
            exe.Filename = Path.Combine(CurrentDirectory, listFiles.SelectedItems[0].Text);
            ET.Executable = exe;
            if (ET.ShowDialog() == DialogResult.OK)
            {
                Program.Executables.Add(exe);
                SaveExecutables();
                LoadScripts();
            }
        }

        void ExecutableEdit_Click(object sender, EventArgs e)
        {
            ExecutableTool ET = new ExecutableTool();
            ET.Executable = (Executable)((ToolStripMenuItem)sender).Tag;
            if (ET.ShowDialog() == DialogResult.OK)
            {
                SaveExecutables();
                LoadScripts();
            }
        }

        void ExecutableLaunch_Click(object sender, EventArgs e)
        {
            if (FindXbox() == false) return;

            Executable exe = (Executable)((ToolStripMenuItem)sender).Tag;
            FileInfo fi = new FileInfo(exe.Filename);
            lblStatus.Text = "Launching: " + fi.Name;
            probar.Style = ProgressBarStyle.Marquee;
            
            launchTitleWorker.RunWorkerAsync(new XBoxIO.LaunchInfo(fi));
        }

        void ExecutableScript_Click(object sender, EventArgs e)
        {
            Executable.Script cmd = (Executable.Script)((ToolStripMenuItem)sender).Tag;
            cmd.Run("");
        }

        void ExecutableFileScript_Click(object sender, EventArgs e)
        {
            Executable.Script cmd = (Executable.Script)((ToolStripMenuItem)sender).Tag;
            cmd.Run(Path.GetFileNameWithoutExtension(listFiles.SelectedItems[0].Text));
        }

        void LoadFileScripts(string filename, string fileType)
        {
            mnuFiles.Items.Clear();
            cmdLaunch.DropDown.Items.Clear();

            mnuFiles.Items.Add(cmdRename);
            mnuFiles.Items.Add(cmdDelete);
            mnuFiles.Items.Add(new ToolStripSeparator());
            mnuFiles.Items.Add(cmdDownload);

            if (fileType == ".xbe")
            {
                mnuFiles.Items.Add(new ToolStripSeparator());
                mnuFiles.Items.Add(cmdAddToScriptsMenu);

                cmdLaunch.Tag = new Executable() { Name = Path.GetFileName(filename), Filename = filename };
                mnuFiles.Items.Add(cmdLaunch);
            }

            foreach (Executable exe in Program.Executables)
            {
                ToolStripMenuItem mi = new ToolStripMenuItem();
                mi.Tag = exe;
                mi.Text = exe.Name;

                foreach (Executable.Script s in exe.Scripts)
                {
                    if ("." + s.FileType != fileType) continue;
                    ToolStripMenuItem miScript = new ToolStripMenuItem();
                    miScript.Tag = s;
                    miScript.Text = s.Name;
                    miScript.Click += new EventHandler(ExecutableFileScript_Click);
                    mi.DropDown.Items.Add(miScript);
                }

                if (mi.DropDown.Items.Count > 0)
                    mnuFiles.Items.Add(mi);

                if (filename == exe.Filename)
                {
                    cmdLaunch.Tag = exe;
                }
            }
        }

        public void LoadScripts()
        {
            mnuScripts.DropDown.Items.Clear();

            foreach (Executable exe in Program.Executables)
            {
                ToolStripMenuItem mi = new ToolStripMenuItem();
                mi.Tag = exe;
                mi.Text = exe.Name;

                foreach (Executable.Script s in exe.Scripts)
                {
                    if (s.FileType != "" && s.FileType != null) continue;
                    ToolStripMenuItem miScript = new ToolStripMenuItem();
                    miScript.Tag = s;
                    miScript.Text = s.Name;
                    miScript.Click += new EventHandler(ExecutableScript_Click);
                    mi.DropDown.Items.Add(miScript);
                }

                mi.DropDown.Items.Add(new ToolStripSeparator());

                ToolStripMenuItem miEdit = new ToolStripMenuItem();
                miEdit.Tag = exe;
                miEdit.Text = "Edit";
                miEdit.Click += new EventHandler(ExecutableEdit_Click);
                mi.DropDown.Items.Add(miEdit);

                ToolStripMenuItem miLaunch = new ToolStripMenuItem();
                miLaunch.Tag = exe;
                miLaunch.Text = "Launch";
                miLaunch.Click += new EventHandler(ExecutableLaunch_Click);
                mi.DropDown.Items.Add(miLaunch);

                mnuScripts.DropDown.Items.Add(mi);
            }
        }

        public void SaveExecutables()
        {
            XmlDocument xd = new XmlDocument();
            xd.Load("Executables.xml");
            xd.DocumentElement.RemoveAll();

            foreach (Executable exe in Program.Executables)
            {
                XmlElement exeNode = xd.CreateElement("Executable");
                exeNode.SetAttribute("Name", exe.Name);
                exeNode.SetAttribute("Filename", exe.Filename);
                xd.DocumentElement.AppendChild(exeNode);

                foreach (Executable.Script s in exe.Scripts)
                {
                    XmlElement scriptNode = xd.CreateElement("Script");
                    scriptNode.SetAttribute("Name", s.Name);
                    scriptNode.SetAttribute("FileType", s.FileType);
                    scriptNode.InnerText = s.Code;
                    exeNode.AppendChild(scriptNode);
                }
            }

            xd.Save("Executables.xml");
        }
        #endregion

        void mnuFiles_Opening(object sender, CancelEventArgs e)
        {
            if (listFiles.SelectedItems.Count != 1)
                e.Cancel = true;
            else
                LoadFileScripts(Path.Combine(CurrentDirectory, listFiles.SelectedItems[0].Text), Path.GetExtension(listFiles.SelectedItems[0].Text));
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        { new SystemProperties().ShowDialog(); }

        private void syncTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Synchronize The XBox System Time With The Computer's Current Time?", "Confirm Synchronization", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (FindXbox() == false) return;
                XBoxIO.XBox.SynchronizeTime();
                CompletedOperation();
            }
        }

        private void lEDStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FindXbox() == false) return;
            Program.LEDStateChanger.ShowDialog();
            CompletedOperation();
        }

        private void memoryHackerToolStripMenuItem_Click(object sender, EventArgs e)
        { Program.MemoryHacker.ShowDialog(); }

        private void StatusChanged(string status)
        {
            lblStatus.Text = status;
        }

        private void yeloStreamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(typeof(Yelo.Stream.ScreenshotTool).Assembly.Location);
            Process.Start(startInfo);
        }

        private void yeloControllerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(typeof(Yelo.Controller.XBoxController).Assembly.Location);
            Process.Start(startInfo);
        }
    };
}