namespace Yelo.Carnage
{
    partial class CarnageHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CarnageHistory));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdSignOut = new System.Windows.Forms.ToolStripButton();
            this.gridGames = new System.Windows.Forms.DataGridView();
            this.colDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGametype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGames)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdSignOut});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(414, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cmdSignOut
            // 
            this.cmdSignOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cmdSignOut.Image = ((System.Drawing.Image)(resources.GetObject("cmdSignOut.Image")));
            this.cmdSignOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdSignOut.Name = "cmdSignOut";
            this.cmdSignOut.Size = new System.Drawing.Size(57, 22);
            this.cmdSignOut.Text = "Sign Out";
            // 
            // gridGames
            // 
            this.gridGames.AllowUserToAddRows = false;
            this.gridGames.AllowUserToDeleteRows = false;
            this.gridGames.AllowUserToOrderColumns = true;
            this.gridGames.AllowUserToResizeRows = false;
            this.gridGames.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridGames.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.gridGames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridGames.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDateTime,
            this.colMap,
            this.colGametype,
            this.colComment});
            this.gridGames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridGames.Location = new System.Drawing.Point(0, 25);
            this.gridGames.Name = "gridGames";
            this.gridGames.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gridGames.RowHeadersVisible = false;
            this.gridGames.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridGames.Size = new System.Drawing.Size(414, 430);
            this.gridGames.TabIndex = 2;
            // 
            // colDateTime
            // 
            this.colDateTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colDateTime.FillWeight = 20F;
            this.colDateTime.HeaderText = "Date Time";
            this.colDateTime.Name = "colDateTime";
            this.colDateTime.ReadOnly = true;
            this.colDateTime.Width = 81;
            // 
            // colMap
            // 
            this.colMap.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colMap.FillWeight = 20F;
            this.colMap.HeaderText = "Map";
            this.colMap.Name = "colMap";
            this.colMap.ReadOnly = true;
            this.colMap.Width = 53;
            // 
            // colGametype
            // 
            this.colGametype.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colGametype.FillWeight = 20F;
            this.colGametype.HeaderText = "Gametype";
            this.colGametype.Name = "colGametype";
            this.colGametype.ReadOnly = true;
            this.colGametype.Width = 80;
            // 
            // colComment
            // 
            this.colComment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colComment.FillWeight = 40F;
            this.colComment.HeaderText = "Comment";
            this.colComment.Name = "colComment";
            // 
            // CarnageHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 455);
            this.Controls.Add(this.gridGames);
            this.Controls.Add(this.toolStrip1);
            this.Icon = Resources.H2_Black_and_White_BIG;
            this.Name = "CarnageHistory";
            this.Text = "Carnage History";
            this.Shown += new System.EventHandler(this.CarnageHistory_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGames)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton cmdSignOut;
        private System.Windows.Forms.DataGridView gridGames;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGametype;
        private System.Windows.Forms.DataGridViewTextBoxColumn colComment;
    }
}

