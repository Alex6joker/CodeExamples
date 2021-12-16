namespace Diplo
{
    partial class SelectServerForm
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
            this.ServersGrid = new System.Windows.Forms.DataGridView();
            this.Head = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InterfacesIPs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DomainName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ComputerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkedFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoServerButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.ServerSelectedButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ServersGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ServersGrid
            // 
            this.ServersGrid.AllowUserToAddRows = false;
            this.ServersGrid.AllowUserToDeleteRows = false;
            this.ServersGrid.AllowUserToResizeColumns = false;
            this.ServersGrid.AllowUserToResizeRows = false;
            this.ServersGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.ServersGrid.ColumnHeadersHeight = 20;
            this.ServersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ServersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Head,
            this.InterfacesIPs,
            this.DomainName,
            this.ComputerName,
            this.WorkedFile});
            this.ServersGrid.Cursor = System.Windows.Forms.Cursors.Default;
            this.ServersGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ServersGrid.Location = new System.Drawing.Point(0, 98);
            this.ServersGrid.MultiSelect = false;
            this.ServersGrid.Name = "ServersGrid";
            this.ServersGrid.ReadOnly = true;
            this.ServersGrid.RowHeadersVisible = false;
            this.ServersGrid.RowHeadersWidth = 40;
            this.ServersGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ServersGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ServersGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ServersGrid.Size = new System.Drawing.Size(604, 344);
            this.ServersGrid.TabIndex = 1;
            // 
            // Head
            // 
            this.Head.DividerWidth = 1;
            this.Head.HeaderText = "№ Сервера";
            this.Head.MinimumWidth = 40;
            this.Head.Name = "Head";
            this.Head.ReadOnly = true;
            this.Head.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Head.Width = 40;
            // 
            // InterfacesIPs
            // 
            this.InterfacesIPs.DividerWidth = 1;
            this.InterfacesIPs.HeaderText = "IP";
            this.InterfacesIPs.MinimumWidth = 120;
            this.InterfacesIPs.Name = "InterfacesIPs";
            this.InterfacesIPs.ReadOnly = true;
            this.InterfacesIPs.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.InterfacesIPs.Width = 120;
            // 
            // DomainName
            // 
            this.DomainName.HeaderText = "Имя домена";
            this.DomainName.MinimumWidth = 130;
            this.DomainName.Name = "DomainName";
            this.DomainName.ReadOnly = true;
            this.DomainName.Width = 130;
            // 
            // ComputerName
            // 
            this.ComputerName.HeaderText = "Имя компьютера";
            this.ComputerName.MinimumWidth = 130;
            this.ComputerName.Name = "ComputerName";
            this.ComputerName.ReadOnly = true;
            this.ComputerName.Width = 130;
            // 
            // WorkedFile
            // 
            this.WorkedFile.HeaderText = "Рабочий файл";
            this.WorkedFile.MinimumWidth = 180;
            this.WorkedFile.Name = "WorkedFile";
            this.WorkedFile.ReadOnly = true;
            this.WorkedFile.Width = 180;
            // 
            // NoServerButton
            // 
            this.NoServerButton.Location = new System.Drawing.Point(22, 69);
            this.NoServerButton.Name = "NoServerButton";
            this.NoServerButton.Size = new System.Drawing.Size(105, 23);
            this.NoServerButton.TabIndex = 2;
            this.NoServerButton.Text = "Без подключения";
            this.NoServerButton.UseVisualStyleBackColor = true;
            this.NoServerButton.Click += new System.EventHandler(this.NoServerButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(273, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Обновить список";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ServerSelectedButton
            // 
            this.ServerSelectedButton.Location = new System.Drawing.Point(153, 68);
            this.ServerSelectedButton.Name = "ServerSelectedButton";
            this.ServerSelectedButton.Size = new System.Drawing.Size(102, 23);
            this.ServerSelectedButton.TabIndex = 4;
            this.ServerSelectedButton.Text = "Выбрать сервер";
            this.ServerSelectedButton.UseVisualStyleBackColor = true;
            this.ServerSelectedButton.Click += new System.EventHandler(this.ServerSelectedButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "\"текстовые файлы |*.txt";
            this.openFileDialog1.Title = "Выберите файл для работы по сети";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Diplo.Properties.Resources.OK_Button;
            this.pictureBox3.Location = new System.Drawing.Point(179, 12);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(50, 50);
            this.pictureBox3.TabIndex = 7;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Diplo.Properties.Resources.Refresh_Button;
            this.pictureBox2.Location = new System.Drawing.Point(302, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(50, 50);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Diplo.Properties.Resources.CreateNewFile_Button;
            this.pictureBox1.Location = new System.Drawing.Point(50, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // SelectServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 442);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ServerSelectedButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.NoServerButton);
            this.Controls.Add(this.ServersGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SelectServerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Окно выбора сервера";
            ((System.ComponentModel.ISupportInitialize)(this.ServersGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ServersGrid;
        private System.Windows.Forms.Button NoServerButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button ServerSelectedButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Head;
        private System.Windows.Forms.DataGridViewTextBoxColumn InterfacesIPs;
        private System.Windows.Forms.DataGridViewTextBoxColumn DomainName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComputerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkedFile;
    }
}