namespace Yelo.Carnage
{
    partial class CarnageViewer
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
            this.gridGames = new System.Windows.Forms.DataGridView();
            this.colPlayerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKills = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDeaths = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAssists = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSuicides = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMedalCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMedalTypes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalShots = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShotsHit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHeadShots = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlaceString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridGames)).BeginInit();
            this.SuspendLayout();
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
            this.colPlayerName,
            this.colScore,
            this.colKills,
            this.colDeaths,
            this.colAssists,
            this.colSuicides,
            this.colPlace,
            this.colMedalCount,
            this.colMedalTypes,
            this.colTotalShots,
            this.colShotsHit,
            this.colHeadShots,
            this.colPlaceString});
            this.gridGames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridGames.Location = new System.Drawing.Point(0, 0);
            this.gridGames.Name = "gridGames";
            this.gridGames.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gridGames.RowHeadersVisible = false;
            this.gridGames.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridGames.Size = new System.Drawing.Size(854, 427);
            this.gridGames.TabIndex = 3;
            // 
            // colPlayerName
            // 
            this.colPlayerName.HeaderText = "Player";
            this.colPlayerName.Name = "colPlayerName";
            this.colPlayerName.ReadOnly = true;
            // 
            // colScore
            // 
            this.colScore.HeaderText = "Score";
            this.colScore.Name = "colScore";
            this.colScore.ReadOnly = true;
            // 
            // colKills
            // 
            this.colKills.HeaderText = "Kills";
            this.colKills.Name = "colKills";
            this.colKills.ReadOnly = true;
            // 
            // colDeaths
            // 
            this.colDeaths.HeaderText = "Deaths";
            this.colDeaths.Name = "colDeaths";
            // 
            // colAssists
            // 
            this.colAssists.HeaderText = "Assists";
            this.colAssists.Name = "colAssists";
            this.colAssists.ReadOnly = true;
            // 
            // colSuicides
            // 
            this.colSuicides.HeaderText = "Suicides";
            this.colSuicides.Name = "colSuicides";
            this.colSuicides.ReadOnly = true;
            // 
            // colPlace
            // 
            this.colPlace.HeaderText = "Place";
            this.colPlace.Name = "colPlace";
            this.colPlace.ReadOnly = true;
            // 
            // colMedalCount
            // 
            this.colMedalCount.HeaderText = "Medal Count";
            this.colMedalCount.Name = "colMedalCount";
            this.colMedalCount.ReadOnly = true;
            // 
            // colMedalTypes
            // 
            this.colMedalTypes.HeaderText = "Medal Types";
            this.colMedalTypes.Name = "colMedalTypes";
            this.colMedalTypes.ReadOnly = true;
            // 
            // colTotalShots
            // 
            this.colTotalShots.HeaderText = "Total Shots";
            this.colTotalShots.Name = "colTotalShots";
            this.colTotalShots.ReadOnly = true;
            // 
            // colShotsHit
            // 
            this.colShotsHit.HeaderText = "Shots Hit";
            this.colShotsHit.Name = "colShotsHit";
            this.colShotsHit.ReadOnly = true;
            // 
            // colHeadShots
            // 
            this.colHeadShots.HeaderText = "HeadShots";
            this.colHeadShots.Name = "colHeadShots";
            this.colHeadShots.ReadOnly = true;
            // 
            // colPlaceString
            // 
            this.colPlaceString.HeaderText = "Place";
            this.colPlaceString.Name = "colPlaceString";
            this.colPlaceString.ReadOnly = true;
            // 
            // GameViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 427);
            this.Controls.Add(this.gridGames);
            this.Name = "GameViewer";
            this.Text = "00/00/00 00:00 - Coagulation - Slayer - Comment";
            this.Icon = Resources.H2_Black_and_White_BIG;
            ((System.ComponentModel.ISupportInitialize)(this.gridGames)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridGames;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlayerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScore;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKills;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDeaths;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAssists;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSuicides;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlace;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMedalCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMedalTypes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalShots;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShotsHit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHeadShots;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlaceString;
    }
}