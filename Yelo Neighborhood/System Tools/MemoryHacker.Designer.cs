namespace Yelo.Neighborhood.System_Tools
{
    partial class MemoryHacker
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
            this.cboType = new System.Windows.Forms.ComboBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdReduceList = new System.Windows.Forms.Button();
            this.cmdSetValue = new System.Windows.Forms.Button();
            this.lstOffsets = new System.Windows.Forms.DataGridView();
            this.cmdBegin = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cboModule = new System.Windows.Forms.ComboBox();
            this.probar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.lstOffsets)).BeginInit();
            this.SuspendLayout();
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(60, 38);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(153, 21);
            this.cboType.TabIndex = 1;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(259, 39);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(97, 20);
            this.txtValue.TabIndex = 2;
            this.txtValue.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Value:";
            // 
            // cmdReduceList
            // 
            this.cmdReduceList.Enabled = false;
            this.cmdReduceList.Location = new System.Drawing.Point(362, 65);
            this.cmdReduceList.Name = "cmdReduceList";
            this.cmdReduceList.Size = new System.Drawing.Size(101, 23);
            this.cmdReduceList.TabIndex = 5;
            this.cmdReduceList.Text = "Reduce List";
            this.cmdReduceList.UseVisualStyleBackColor = true;
            this.cmdReduceList.Click += new System.EventHandler(this.cmdReduceList_Click);
            // 
            // cmdSetValue
            // 
            this.cmdSetValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSetValue.Enabled = false;
            this.cmdSetValue.Location = new System.Drawing.Point(362, 37);
            this.cmdSetValue.Name = "cmdSetValue";
            this.cmdSetValue.Size = new System.Drawing.Size(101, 23);
            this.cmdSetValue.TabIndex = 6;
            this.cmdSetValue.Text = "Set Value";
            this.cmdSetValue.UseVisualStyleBackColor = true;
            // 
            // lstOffsets
            // 
            this.lstOffsets.AllowUserToAddRows = false;
            this.lstOffsets.AllowUserToDeleteRows = false;
            this.lstOffsets.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.lstOffsets.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lstOffsets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lstOffsets.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lstOffsets.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.lstOffsets.Location = new System.Drawing.Point(0, 95);
            this.lstOffsets.Name = "lstOffsets";
            this.lstOffsets.ReadOnly = true;
            this.lstOffsets.Size = new System.Drawing.Size(471, 299);
            this.lstOffsets.TabIndex = 7;
            // 
            // cmdBegin
            // 
            this.cmdBegin.Location = new System.Drawing.Point(362, 10);
            this.cmdBegin.Name = "cmdBegin";
            this.cmdBegin.Size = new System.Drawing.Size(101, 23);
            this.cmdBegin.TabIndex = 8;
            this.cmdBegin.Text = "Begin";
            this.cmdBegin.UseVisualStyleBackColor = true;
            this.cmdBegin.Click += new System.EventHandler(this.cmdBegin_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Module:";
            // 
            // cboModule
            // 
            this.cboModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModule.FormattingEnabled = true;
            this.cboModule.Location = new System.Drawing.Point(60, 12);
            this.cboModule.Name = "cboModule";
            this.cboModule.Size = new System.Drawing.Size(296, 21);
            this.cboModule.TabIndex = 11;
            // 
            // probar
            // 
            this.probar.Location = new System.Drawing.Point(12, 65);
            this.probar.Name = "probar";
            this.probar.Size = new System.Drawing.Size(344, 23);
            this.probar.TabIndex = 12;
            // 
            // MemoryHacker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 394);
            this.Controls.Add(this.probar);
            this.Controls.Add(this.cboModule);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdBegin);
            this.Controls.Add(this.lstOffsets);
            this.Controls.Add(this.cmdSetValue);
            this.Controls.Add(this.cmdReduceList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.cboType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "MemoryHacker";
            this.Text = "Memory Hacker";
            this.Shown += new System.EventHandler(this.MemoryHacker_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.lstOffsets)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdReduceList;
        private System.Windows.Forms.Button cmdSetValue;
        private System.Windows.Forms.DataGridView lstOffsets;
        private System.Windows.Forms.Button cmdBegin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboModule;
        private System.Windows.Forms.ProgressBar probar;
    }
}