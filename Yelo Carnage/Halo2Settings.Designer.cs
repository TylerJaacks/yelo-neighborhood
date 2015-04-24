namespace Yelo.Carnage
{
    partial class Halo2Settings
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
            this.cmdTryAgain = new System.Windows.Forms.Button();
            this.txtHalo2Dir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdTryAgain
            // 
            this.cmdTryAgain.Location = new System.Drawing.Point(201, 38);
            this.cmdTryAgain.Name = "cmdTryAgain";
            this.cmdTryAgain.Size = new System.Drawing.Size(75, 23);
            this.cmdTryAgain.TabIndex = 0;
            this.cmdTryAgain.Text = "Try Again";
            this.cmdTryAgain.UseVisualStyleBackColor = true;
            this.cmdTryAgain.Click += new System.EventHandler(this.cmdTryAgain_Click);
            // 
            // txtHalo2Dir
            // 
            this.txtHalo2Dir.Enabled = false;
            this.txtHalo2Dir.Location = new System.Drawing.Point(104, 12);
            this.txtHalo2Dir.Name = "txtHalo2Dir";
            this.txtHalo2Dir.Size = new System.Drawing.Size(172, 20);
            this.txtHalo2Dir.TabIndex = 3;
            this.txtHalo2Dir.Text = "E:\\Games\\Halo2\\";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Halo 2 Directory:";
            // 
            // Halo2Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 73);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHalo2Dir);
            this.Controls.Add(this.cmdTryAgain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Halo2Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Halo 2 Not Found";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdTryAgain;
        private System.Windows.Forms.TextBox txtHalo2Dir;
        private System.Windows.Forms.Label label1;
    }
}