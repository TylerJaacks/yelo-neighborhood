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
        private static NotifyIcon taskbarIcon;
        private static ContextMenuStrip taskbarMenu;
        private static ToolStripMenuItem cmdConnectToHalo2;
        private static ToolStripMenuItem cmdMapDownloader;
        private static ToolStripMenuItem cmdCarnageHistory;
        private static ToolStripMenuItem cmdExit;

        static void MakeTaskbarIcon()
        {
            taskbarIcon = new NotifyIcon();
            taskbarMenu = new ContextMenuStrip();
            cmdConnectToHalo2 = new ToolStripMenuItem();
            cmdMapDownloader = new ToolStripMenuItem();
            cmdCarnageHistory = new ToolStripMenuItem();
            cmdExit = new ToolStripMenuItem();

            taskbarIcon.Icon = Resources.H2_Black_and_White;
            taskbarIcon.Text = "Halo 2";
            taskbarIcon.Visible = true;
            taskbarIcon.ContextMenuStrip = taskbarMenu;

            taskbarMenu.Items.AddRange(new ToolStripItem[] { cmdConnectToHalo2, new ToolStripSeparator(), cmdMapDownloader, cmdCarnageHistory, new ToolStripSeparator(), cmdExit }
            );
            taskbarMenu.ShowImageMargin = false;
            taskbarMenu.ShowCheckMargin = true;
            taskbarMenu.Size = new System.Drawing.Size(141, 26);

            cmdConnectToHalo2.Text = "Connect To Halo 2";
            cmdMapDownloader.Text = "Map Downloader";
            cmdCarnageHistory.Text = "Carnage History";
            cmdExit.Text = "Exit";

            cmdConnectToHalo2.Click += new EventHandler(cmdConnectToHalo2_Click);
            cmdMapDownloader.Click += new EventHandler(cmdMapDownloader_Click);
            cmdCarnageHistory.Click += new EventHandler(cmdGameHistory_Click);
            cmdExit.Click += new EventHandler(cmdExit_Click);
        }
        
        static void cmdConnectToHalo2_Click(object sender, EventArgs e)
        { FindXBox(); }

        static void cmdMapDownloader_Click(object sender, EventArgs e)
        {
            if (_mapDownloader == null || _mapDownloader.IsDisposed) _mapDownloader = new MapDownloader();
            _mapDownloader.Show(); 
        }

        static void cmdGameHistory_Click(object sender, EventArgs e)
        {
            if (_carnageHistory == null || _carnageHistory.IsDisposed) _carnageHistory = new CarnageHistory();
            _carnageHistory.Show(); 
        }

        static void cmdExit_Click(object sender, EventArgs e)
        { Application.Exit(); }
    };
}