using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using Yelo.Debug;
using System.IO;
using System.Drawing;
using Yelo.Shared;

namespace Yelo.Stream
{
    public partial class ScreenshotTool : Form
    {
		List<Image> Images = new List<Image>();
        ImageFormatSelector IFS = new ImageFormatSelector();

        public ScreenshotTool()
        {
            InitializeComponent();

            Text = XBoxIO.XBox.DebugName + " - " + XBoxIO.XBox.DebugIP + " - Yelo Stream v" + Cache.Version;

            cboViewStyle.Items.Add(ImageLayout.Center);
            cboViewStyle.Items.Add(ImageLayout.Stretch);
            cboViewStyle.Items.Add(ImageLayout.Zoom);
            cboViewStyle.SelectedItem = ImageLayout.Zoom;

            cboStreamSize.Items.Add(XboxVideoStream.VideoSize.Small);
            cboStreamSize.Items.Add(XboxVideoStream.VideoSize.Medium);
            cboStreamSize.Items.Add(XboxVideoStream.VideoSize.Full);
            cboStreamSize.SelectedItem = XboxVideoStream.VideoSize.Medium;

            foreach (Enum e in Enum.GetValues(typeof(XboxVideoStream.VideoPresentationInterval)))
                cboFrameInterval.Items.Add(e);
            cboFrameInterval.SelectedItem = XboxVideoStream.VideoPresentationInterval.Immediate;

            StartStream();
            LiveStreamThread = new Thread(LiveStreamLoop);
            LiveStreamThread.Start();
        }

        public void TakeScreenshot()
        {
            if (!LiveStreamRunning && XBoxIO.XBox.Connected == false) return;

            DateTime now = DateTime.Now;
            listImages.Items.Add(new ListViewItem(now.ToString() + " " + now.Second.ToString() + "." + now.Millisecond.ToString(), imageList.Images.Count, listImages.Groups[0]));

            lock (XBoxIO.XBox)
            {
                Image screenshot = XBoxIO.XBox.Screenshot();
                Images.Add(screenshot);
                imageList.Images.Add(screenshot);
            }

            if (!checkLiveStream.Checked)
            {
                listImages.SelectedItems.Clear();
                listImages.SelectedIndices.Clear();
                listImages.Items[listImages.Items.Count - 1].Selected = true;
            }
            cmdSaveChecked.Enabled = true;
            cmdSaveSelected.Enabled = true;
        }

        void ScreenshotTool_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            { 
                case Keys.F5:
                    TakeScreenshot();
                    break;
            }
        }

        void captureF5ToolStripMenuItem_Click(object sender, EventArgs e)
        { TakeScreenshot(); }

        void listImages_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        { imageBox.BackgroundImage = Images[e.Item.ImageIndex]; }

        void checkLiveStream_CheckedChanged(object sender, EventArgs e)
        {
            if (checkLiveStream.Checked)
                ResumeStream();
            else
                StopStream();
        }

        void StartStream()
        {
            lock (XBoxIO.XBox)
            {
                if (XBoxIO.XBox.Connected == false && XBoxIO.FindXBox() == false)
                {
                    checkLiveStream.Text = "Live Stream (Off)";
                    return;
                }

                if (videoStream != null)
                    videoStream.End();

                videoStream = new XboxVideoStream(XBoxIO.XBox,
                    (XboxVideoStream.VideoSize)cboStreamSize.SelectedItem,
                    XboxVideoStream.VideoQuality.Regular,
                    (XboxVideoStream.VideoPresentationInterval)cboFrameInterval.SelectedItem);

                videoStream.Begin();

                checkLiveStream.Text = "Live Stream (On)";
            }
        }

        void ResumeStream()
        {
            lock (XBoxIO.XBox)
            {
                if (XBoxIO.XBox.Connected == false && XBoxIO.FindXBox() == false)
                {
                    checkLiveStream.Text = "Live Stream (Off)";
                    return;
                }

                LiveStreamPaused = false;
                videoStream.Restart();
                checkLiveStream.Text = "Live Stream (On)";
            }
        }

        void StopStream()
        {
            videoStream.End();
            LiveStreamPaused = true;
            checkLiveStream.Text = "Live Stream (Off)";
        }

        Thread LiveStreamThread;
        bool LiveStreamRunning = false;
        bool LiveStreamPaused = false;
        XboxVideoStream videoStream;
        void LiveStreamLoop()
        {
            LiveStreamRunning = true;
            while (LiveStreamRunning)
            {
                if (videoStream.IsActive)
                {
                    lock (XBoxIO.XBox)
                    {
                        imageBox.BackgroundImage = videoStream.NextFrame();
                    }
                }
                else if (LiveStreamPaused == false)
                {
                    ResumeStream();
                }
            }
        }

        void ScreenshotTool_FormClosing(object sender, FormClosingEventArgs e)
        {
            lock (XBoxIO.XBox)
            {
                videoStream.End();
                LiveStreamRunning = false;
                XBoxIO.XBox.Disconnect();
            }
        }

        void cmdSaveChecked_Click(object sender, EventArgs e)
        {
            if (listImages.CheckedItems.Count == 0) return;
            if (FBD.ShowDialog() != DialogResult.OK) return;
            if (IFS.ShowDialog() != DialogResult.OK) return;

            foreach (ListViewItem lvt in listImages.CheckedItems)
            {
                string name = lvt.Text;
                foreach (char c in Path.GetInvalidFileNameChars()) name = name.Replace(c, '_');

                Image outImage = new Bitmap(Images[lvt.ImageIndex], Images[0].Width, Images[0].Height);
                using (var fs = new FileStream(Path.Combine(FBD.SelectedPath, name) + "." + IFS.ImageFormat.ToString().ToLower(), FileMode.Create))
                {
                    outImage.Save(fs, IFS.ImageFormat);
                }
            }
        }

        void cmdSaveSelected_Click(object sender, EventArgs e)
        {
            if (listImages.SelectedItems.Count == 0) return;
            if (FBD.ShowDialog() != DialogResult.OK) return;
            if (IFS.ShowDialog() != DialogResult.OK) return;

            foreach (ListViewItem lvt in listImages.SelectedItems)
            {
                string name = lvt.Text;
                foreach (char c in Path.GetInvalidFileNameChars()) name = name.Replace(c, '_');

                Image outImage = new Bitmap(Images[lvt.ImageIndex], Images[0].Width, Images[0].Height);
                using (var fs = new FileStream(Path.Combine(FBD.SelectedPath, name) + "." + IFS.ImageFormat.ToString().ToLower(), FileMode.Create))
				{
                    outImage.Save(fs, IFS.ImageFormat);
                }
            }
        }

        void cboViewStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            imageBox.BackgroundImageLayout = (ImageLayout)cboViewStyle.SelectedItem;
        }

        private void cboStreamSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LiveStreamRunning)
                StartStream();
        }

        private void cboFrameInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LiveStreamRunning)
                StartStream();
        }
    };
}