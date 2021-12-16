namespace Diplo
{
    partial class TextEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextEditor));
            this.MainTextEditorField = new System.Windows.Forms.RichTextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.OpenFilePictureBox = new System.Windows.Forms.PictureBox();
            this.CreateNewPictureBox = new System.Windows.Forms.PictureBox();
            this.SaveButtonPictureBox = new System.Windows.Forms.PictureBox();
            this.DisconnectPictureBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сетьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отключитьсяОтСетиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.логСетиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокПользователейToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.OpenFilePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CreateNewPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaveButtonPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DisconnectPictureBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTextEditorField
            // 
            this.MainTextEditorField.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.MainTextEditorField.Location = new System.Drawing.Point(0, 99);
            this.MainTextEditorField.Name = "MainTextEditorField";
            this.MainTextEditorField.Size = new System.Drawing.Size(811, 462);
            this.MainTextEditorField.TabIndex = 0;
            this.MainTextEditorField.Text = "";
            this.MainTextEditorField.TextChanged += new System.EventHandler(this.MainTextEditorField_TextChanged);
            this.MainTextEditorField.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainTextEditorField_KeyDown);
            this.MainTextEditorField.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainTextEditorField_KeyPress);
            this.MainTextEditorField.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainTextEditorField_KeyUp);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "\"текстовые файлы |*.txt";
            this.saveFileDialog1.Title = "Выберите путь для сохранения результата работы";
            // 
            // OpenFilePictureBox
            // 
            this.OpenFilePictureBox.Image = global::Diplo.Properties.Resources.OpenFile_Button;
            this.OpenFilePictureBox.Location = new System.Drawing.Point(94, 27);
            this.OpenFilePictureBox.Name = "OpenFilePictureBox";
            this.OpenFilePictureBox.Size = new System.Drawing.Size(50, 50);
            this.OpenFilePictureBox.TabIndex = 4;
            this.OpenFilePictureBox.TabStop = false;
            this.OpenFilePictureBox.Click += new System.EventHandler(this.OpenFilePictureBox_Click);
            // 
            // CreateNewPictureBox
            // 
            this.CreateNewPictureBox.Image = global::Diplo.Properties.Resources.CreateNewFile_Button;
            this.CreateNewPictureBox.Location = new System.Drawing.Point(12, 27);
            this.CreateNewPictureBox.Name = "CreateNewPictureBox";
            this.CreateNewPictureBox.Size = new System.Drawing.Size(50, 50);
            this.CreateNewPictureBox.TabIndex = 3;
            this.CreateNewPictureBox.TabStop = false;
            this.CreateNewPictureBox.Click += new System.EventHandler(this.CreateNewPictureBox_Click);
            // 
            // SaveButtonPictureBox
            // 
            this.SaveButtonPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("SaveButtonPictureBox.Image")));
            this.SaveButtonPictureBox.Location = new System.Drawing.Point(180, 27);
            this.SaveButtonPictureBox.Name = "SaveButtonPictureBox";
            this.SaveButtonPictureBox.Size = new System.Drawing.Size(50, 50);
            this.SaveButtonPictureBox.TabIndex = 2;
            this.SaveButtonPictureBox.TabStop = false;
            this.SaveButtonPictureBox.Click += new System.EventHandler(this.SaveButtonPictureBox_Click);
            // 
            // DisconnectPictureBox
            // 
            this.DisconnectPictureBox.Image = global::Diplo.Properties.Resources.Exit_Button;
            this.DisconnectPictureBox.Location = new System.Drawing.Point(749, 27);
            this.DisconnectPictureBox.Name = "DisconnectPictureBox";
            this.DisconnectPictureBox.Size = new System.Drawing.Size(50, 50);
            this.DisconnectPictureBox.TabIndex = 1;
            this.DisconnectPictureBox.TabStop = false;
            this.DisconnectPictureBox.Click += new System.EventHandler(this.DisconnectPictureBox_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.сетьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(811, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьToolStripMenuItem,
            this.открытьToolStripMenuItem,
            this.сохранитьToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(48, 20);
            this.toolStripMenuItem1.Text = "Файл";
            // 
            // создатьToolStripMenuItem
            // 
            this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
            this.создатьToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.создатьToolStripMenuItem.Text = "Создать";
            this.создатьToolStripMenuItem.Click += new System.EventHandler(this.создатьToolStripMenuItem_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // сетьToolStripMenuItem
            // 
            this.сетьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.отключитьсяОтСетиToolStripMenuItem,
            this.логСетиToolStripMenuItem,
            this.списокПользователейToolStripMenuItem});
            this.сетьToolStripMenuItem.Name = "сетьToolStripMenuItem";
            this.сетьToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.сетьToolStripMenuItem.Text = "Сеть";
            // 
            // отключитьсяОтСетиToolStripMenuItem
            // 
            this.отключитьсяОтСетиToolStripMenuItem.Name = "отключитьсяОтСетиToolStripMenuItem";
            this.отключитьсяОтСетиToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.отключитьсяОтСетиToolStripMenuItem.Text = "Отключиться от сети";
            this.отключитьсяОтСетиToolStripMenuItem.Click += new System.EventHandler(this.отключитьсяОтСетиToolStripMenuItem_Click);
            // 
            // логСетиToolStripMenuItem
            // 
            this.логСетиToolStripMenuItem.Name = "логСетиToolStripMenuItem";
            this.логСетиToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.логСетиToolStripMenuItem.Text = "Лог сети";
            this.логСетиToolStripMenuItem.Click += new System.EventHandler(this.логСетиToolStripMenuItem_Click);
            // 
            // списокПользователейToolStripMenuItem
            // 
            this.списокПользователейToolStripMenuItem.Name = "списокПользователейToolStripMenuItem";
            this.списокПользователейToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.списокПользователейToolStripMenuItem.Text = "Список пользователей";
            this.списокПользователейToolStripMenuItem.Click += new System.EventHandler(this.списокПользователейToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "\"текстовые файлы |*.txt";
            // 
            // TextEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 561);
            this.Controls.Add(this.OpenFilePictureBox);
            this.Controls.Add(this.CreateNewPictureBox);
            this.Controls.Add(this.SaveButtonPictureBox);
            this.Controls.Add(this.DisconnectPictureBox);
            this.Controls.Add(this.MainTextEditorField);
            this.Controls.Add(this.menuStrip1);
            this.Name = "TextEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактор текста";
            this.ResizeEnd += new System.EventHandler(this.TextEditor_ResizeEnd);
            this.Resize += new System.EventHandler(this.TextEditor_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.OpenFilePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CreateNewPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaveButtonPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DisconnectPictureBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox MainTextEditorField;
        private System.Windows.Forms.PictureBox DisconnectPictureBox;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        public System.Windows.Forms.PictureBox SaveButtonPictureBox;
        public System.Windows.Forms.PictureBox CreateNewPictureBox;
        public System.Windows.Forms.PictureBox OpenFilePictureBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem создатьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сетьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отключитьсяОтСетиToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem логСетиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокПользователейToolStripMenuItem;
    }
}