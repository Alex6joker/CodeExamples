namespace GPSSEmu
{
    partial class EmulationStaticticsForm
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
            this.StatisticInfo = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // StatisticInfo
            // 
            this.StatisticInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatisticInfo.Location = new System.Drawing.Point(0, 0);
            this.StatisticInfo.Name = "StatisticInfo";
            this.StatisticInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StatisticInfo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.StatisticInfo.Size = new System.Drawing.Size(945, 577);
            this.StatisticInfo.TabIndex = 0;
            this.StatisticInfo.Text = "";
            // 
            // EmulationStaticticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 577);
            this.Controls.Add(this.StatisticInfo);
            this.Name = "EmulationStaticticsForm";
            this.Text = "EmulationStaticticsForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox StatisticInfo;
    }
}