namespace Yelo.Carnage
{
    partial class MapDownloader
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.lstBaseMap = new System.Windows.Forms.CheckedListBox();
            this.lstSupportedGametypes = new System.Windows.Forms.CheckedListBox();
            this.lstTags = new System.Windows.Forms.CheckedListBox();
            this.gridMaps = new System.Windows.Forms.DataGridView();
            this.cmdDownload = new System.Windows.Forms.Button();
            this.cmdDownloadAndUpdate = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMaps)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel1.Controls.Add(this.lstBaseMap);
            this.splitContainer1.Panel1.Controls.Add(this.lstSupportedGametypes);
            this.splitContainer1.Panel1.Controls.Add(this.lstTags);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridMaps);
            this.splitContainer1.Panel2.Controls.Add(this.cmdDownload);
            this.splitContainer1.Panel2.Controls.Add(this.cmdDownloadAndUpdate);
            this.splitContainer1.Size = new System.Drawing.Size(410, 451);
            this.splitContainer1.SplitterDistance = 136;
            this.splitContainer1.TabIndex = 3;
            // 
            // toolStrip1
            // 
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.toolStripSeparator2,
            this.toolStripLabel3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(410, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Padding = new System.Windows.Forms.Padding(102, 0, 0, 0);
            this.toolStripLabel1.Size = new System.Drawing.Size(134, 22);
            this.toolStripLabel1.Text = "Tags";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.toolStripLabel2.Size = new System.Drawing.Size(134, 22);
            this.toolStripLabel2.Text = "Supported Gametypes";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Padding = new System.Windows.Forms.Padding(65, 0, 0, 0);
            this.toolStripLabel3.Size = new System.Drawing.Size(123, 22);
            this.toolStripLabel3.Text = "Base Map";
            // 
            // lstBaseMap
            // 
            this.lstBaseMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstBaseMap.CheckOnClick = true;
            this.lstBaseMap.FormattingEnabled = true;
            this.lstBaseMap.IntegralHeight = false;
            this.lstBaseMap.Location = new System.Drawing.Point(277, 24);
            this.lstBaseMap.Name = "lstBaseMap";
            this.lstBaseMap.Size = new System.Drawing.Size(134, 110);
            this.lstBaseMap.TabIndex = 5;
            this.lstBaseMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Filter_MouseUp);
            // 
            // lstSupportedGametypes
            // 
            this.lstSupportedGametypes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lstSupportedGametypes.CheckOnClick = true;
            this.lstSupportedGametypes.FormattingEnabled = true;
            this.lstSupportedGametypes.IntegralHeight = false;
            this.lstSupportedGametypes.Location = new System.Drawing.Point(137, 24);
            this.lstSupportedGametypes.Name = "lstSupportedGametypes";
            this.lstSupportedGametypes.Size = new System.Drawing.Size(141, 110);
            this.lstSupportedGametypes.TabIndex = 4;
            this.lstSupportedGametypes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Filter_MouseUp);
            // 
            // lstTags
            // 
            this.lstTags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstTags.CheckOnClick = true;
            this.lstTags.FormattingEnabled = true;
            this.lstTags.IntegralHeight = false;
            this.lstTags.Location = new System.Drawing.Point(-1, 24);
            this.lstTags.Name = "lstTags";
            this.lstTags.Size = new System.Drawing.Size(139, 110);
            this.lstTags.TabIndex = 3;
            this.lstTags.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Filter_MouseUp);
            // 
            // gridMaps
            // 
            this.gridMaps.AllowUserToAddRows = false;
            this.gridMaps.AllowUserToDeleteRows = false;
            this.gridMaps.AllowUserToResizeColumns = false;
            this.gridMaps.AllowUserToResizeRows = false;
            this.gridMaps.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridMaps.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.gridMaps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMaps.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridMaps.Location = new System.Drawing.Point(0, 0);
            this.gridMaps.Name = "gridMaps";
            this.gridMaps.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gridMaps.RowHeadersVisible = false;
            this.gridMaps.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridMaps.Size = new System.Drawing.Size(410, 270);
            this.gridMaps.TabIndex = 7;
            // 
            // cmdDownload
            // 
            this.cmdDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdDownload.Location = new System.Drawing.Point(12, 276);
            this.cmdDownload.Name = "cmdDownload";
            this.cmdDownload.Size = new System.Drawing.Size(118, 23);
            this.cmdDownload.TabIndex = 6;
            this.cmdDownload.Text = "Download Only";
            this.cmdDownload.UseVisualStyleBackColor = true;
            // 
            // cmdDownloadAndUpdate
            // 
            this.cmdDownloadAndUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDownloadAndUpdate.Location = new System.Drawing.Point(242, 276);
            this.cmdDownloadAndUpdate.Name = "cmdDownloadAndUpdate";
            this.cmdDownloadAndUpdate.Size = new System.Drawing.Size(156, 23);
            this.cmdDownloadAndUpdate.TabIndex = 5;
            this.cmdDownloadAndUpdate.Text = "Download and Update XBox";
            this.cmdDownloadAndUpdate.UseVisualStyleBackColor = true;
            // 
            // MapDownloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 451);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = global::Yelo.Carnage.Resources.H2_Black_and_White_BIG;
            this.MaximizeBox = false;
            this.Name = "MapDownloader";
            this.Text = "Map Downloader";
            this.Shown += new System.EventHandler(this.MapDownloader_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMaps)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckedListBox lstBaseMap;
        private System.Windows.Forms.CheckedListBox lstSupportedGametypes;
        private System.Windows.Forms.CheckedListBox lstTags;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button cmdDownload;
        private System.Windows.Forms.Button cmdDownloadAndUpdate;
        private System.Windows.Forms.DataGridView gridMaps;
    }
}