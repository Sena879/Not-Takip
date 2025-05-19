namespace NotTakip
{
    partial class OgrenciPanel
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
            this.lstnotlar = new System.Windows.Forms.ListBox();
            this.lblkarsiama = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstnotlar
            // 
            this.lstnotlar.FormattingEnabled = true;
            this.lstnotlar.Location = new System.Drawing.Point(12, 73);
            this.lstnotlar.Name = "lstnotlar";
            this.lstnotlar.Size = new System.Drawing.Size(357, 355);
            this.lstnotlar.TabIndex = 0;
            // 
            // lblkarsiama
            // 
            this.lblkarsiama.AutoSize = true;
            this.lblkarsiama.Location = new System.Drawing.Point(12, 19);
            this.lblkarsiama.Name = "lblkarsiama";
            this.lblkarsiama.Size = new System.Drawing.Size(62, 13);
            this.lblkarsiama.TabIndex = 1;
            this.lblkarsiama.Text = "lblKarsilama";
            // 
            // OgrenciPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 440);
            this.Controls.Add(this.lblkarsiama);
            this.Controls.Add(this.lstnotlar);
            this.Name = "OgrenciPanel";
            this.Text = "OgrenciPanel";
            this.Load += new System.EventHandler(this.OgrenciPanel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstnotlar;
        private System.Windows.Forms.Label lblkarsiama;
    }
}