namespace Diplo
{
    partial class NetworkLogForm
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
            this.Log = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // Log
            // 
            this.Log.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Log.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Log.DetectUrls = false;
            this.Log.Location = new System.Drawing.Point(0, 0);
            this.Log.Name = "Log";
            this.Log.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.Log.Size = new System.Drawing.Size(747, 423);
            this.Log.TabIndex = 0;
            this.Log.Text = "";
            // 
            // NetworkLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 422);
            this.Controls.Add(this.Log);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "NetworkLogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Лог сети";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NetworkLogForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NetworkLogForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox Log;
    }
}