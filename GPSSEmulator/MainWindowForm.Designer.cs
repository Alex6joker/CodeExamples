namespace GPSSEmu
{
    partial class MainWindowForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.CodeTextBox = new System.Windows.Forms.RichTextBox();
            this.LoadFileButton = new System.Windows.Forms.Button();
            this.StartEmulationButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // CodeTextBox
            // 
            this.CodeTextBox.Location = new System.Drawing.Point(0, 0);
            this.CodeTextBox.Name = "CodeTextBox";
            this.CodeTextBox.Size = new System.Drawing.Size(784, 500);
            this.CodeTextBox.TabIndex = 0;
            this.CodeTextBox.Text = "";
            // 
            // LoadFileButton
            // 
            this.LoadFileButton.Location = new System.Drawing.Point(131, 519);
            this.LoadFileButton.Name = "LoadFileButton";
            this.LoadFileButton.Size = new System.Drawing.Size(112, 23);
            this.LoadFileButton.TabIndex = 1;
            this.LoadFileButton.Text = "Загрузить файл";
            this.LoadFileButton.UseVisualStyleBackColor = true;
            this.LoadFileButton.Click += new System.EventHandler(this.LoadFileButton_Click);
            // 
            // StartEmulationButton
            // 
            this.StartEmulationButton.Location = new System.Drawing.Point(472, 519);
            this.StartEmulationButton.Name = "StartEmulationButton";
            this.StartEmulationButton.Size = new System.Drawing.Size(112, 23);
            this.StartEmulationButton.TabIndex = 2;
            this.StartEmulationButton.Text = "Анализ";
            this.StartEmulationButton.UseVisualStyleBackColor = true;
            this.StartEmulationButton.Click += new System.EventHandler(this.StartEmulationButton_Click);
            // 
            // MainWindowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.StartEmulationButton);
            this.Controls.Add(this.LoadFileButton);
            this.Controls.Add(this.CodeTextBox);
            this.Name = "MainWindowForm";
            this.Text = "GPSS";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RichTextBox CodeTextBox;
        private System.Windows.Forms.Button LoadFileButton;
        private System.Windows.Forms.Button StartEmulationButton;
    }
}

