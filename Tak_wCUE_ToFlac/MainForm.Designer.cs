namespace Flac_with_CUE_to_Tak
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.infoLabel1 = new System.Windows.Forms.Label();
            this.infoLabel2 = new System.Windows.Forms.Label();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.startConvertButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // infoLabel1
            // 
            this.infoLabel1.AutoSize = true;
            this.infoLabel1.Location = new System.Drawing.Point(12, 9);
            this.infoLabel1.Name = "infoLabel1";
            this.infoLabel1.Size = new System.Drawing.Size(302, 13);
            this.infoLabel1.TabIndex = 0;
            this.infoLabel1.Text = "Папка для поиска аудио .Tak с .Cue и перевод его в .Flac.";
            // 
            // infoLabel2
            // 
            this.infoLabel2.AutoSize = true;
            this.infoLabel2.Location = new System.Drawing.Point(29, 22);
            this.infoLabel2.Name = "infoLabel2";
            this.infoLabel2.Size = new System.Drawing.Size(275, 13);
            this.infoLabel2.TabIndex = 1;
            this.infoLabel2.Text = "Файлы .Tak без .Cue не будут переконвертированы!";
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(17, 44);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.ReadOnly = true;
            this.pathTextBox.Size = new System.Drawing.Size(297, 20);
            this.pathTextBox.TabIndex = 2;
            this.pathTextBox.Click += new System.EventHandler(this.pathTextBox_Click);
            this.pathTextBox.TextChanged += new System.EventHandler(this.pathTextBox_TextChanged);
            // 
            // startConvertButton
            // 
            this.startConvertButton.Enabled = false;
            this.startConvertButton.Location = new System.Drawing.Point(121, 76);
            this.startConvertButton.Name = "startConvertButton";
            this.startConvertButton.Size = new System.Drawing.Size(75, 23);
            this.startConvertButton.TabIndex = 3;
            this.startConvertButton.Text = "Запуск";
            this.startConvertButton.UseVisualStyleBackColor = true;
            this.startConvertButton.Click += new System.EventHandler(this.startConvertButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 136);
            this.Controls.Add(this.startConvertButton);
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.infoLabel2);
            this.Controls.Add(this.infoLabel1);
            this.Name = "MainForm";
            this.Text = "Flac to Tak";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label infoLabel1;
        private System.Windows.Forms.Label infoLabel2;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button startConvertButton;
    }
}

