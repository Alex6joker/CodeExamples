namespace Diplo
{
    partial class ChangeInterfaceForm
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
            this.InterfacesGrid = new System.Windows.Forms.DataGridView();
            this.Head = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InterfacesIPs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectInterfaceButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.InterfacesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // InterfacesGrid
            // 
            this.InterfacesGrid.AllowUserToAddRows = false;
            this.InterfacesGrid.AllowUserToDeleteRows = false;
            this.InterfacesGrid.AllowUserToResizeColumns = false;
            this.InterfacesGrid.AllowUserToResizeRows = false;
            this.InterfacesGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.InterfacesGrid.ColumnHeadersHeight = 20;
            this.InterfacesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.InterfacesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Head,
            this.InterfacesIPs});
            this.InterfacesGrid.Cursor = System.Windows.Forms.Cursors.Default;
            this.InterfacesGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.InterfacesGrid.Location = new System.Drawing.Point(0, 91);
            this.InterfacesGrid.MultiSelect = false;
            this.InterfacesGrid.Name = "InterfacesGrid";
            this.InterfacesGrid.ReadOnly = true;
            this.InterfacesGrid.RowHeadersVisible = false;
            this.InterfacesGrid.RowHeadersWidth = 40;
            this.InterfacesGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.InterfacesGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.InterfacesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.InterfacesGrid.Size = new System.Drawing.Size(543, 310);
            this.InterfacesGrid.TabIndex = 0;
            // 
            // Head
            // 
            this.Head.DividerWidth = 1;
            this.Head.HeaderText = "№ интерфейса";
            this.Head.MinimumWidth = 140;
            this.Head.Name = "Head";
            this.Head.ReadOnly = true;
            this.Head.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Head.Width = 140;
            // 
            // InterfacesIPs
            // 
            this.InterfacesIPs.DividerWidth = 1;
            this.InterfacesIPs.HeaderText = "IP";
            this.InterfacesIPs.MinimumWidth = 400;
            this.InterfacesIPs.Name = "InterfacesIPs";
            this.InterfacesIPs.ReadOnly = true;
            this.InterfacesIPs.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.InterfacesIPs.Width = 400;
            // 
            // SelectInterfaceButton
            // 
            this.SelectInterfaceButton.Location = new System.Drawing.Point(12, 62);
            this.SelectInterfaceButton.Name = "SelectInterfaceButton";
            this.SelectInterfaceButton.Size = new System.Drawing.Size(159, 23);
            this.SelectInterfaceButton.TabIndex = 1;
            this.SelectInterfaceButton.Text = "Выбрать интерфейс";
            this.SelectInterfaceButton.UseVisualStyleBackColor = true;
            this.SelectInterfaceButton.Click += new System.EventHandler(this.SelectInterfaceButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Diplo.Properties.Resources.OK_Button;
            this.pictureBox1.Location = new System.Drawing.Point(66, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // ChangeInterfaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 401);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.SelectInterfaceButton);
            this.Controls.Add(this.InterfacesGrid);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ChangeInterfaceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Окно выбора интерфейса";
            ((System.ComponentModel.ISupportInitialize)(this.InterfacesGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView InterfacesGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Head;
        private System.Windows.Forms.DataGridViewTextBoxColumn InterfacesIPs;
        private System.Windows.Forms.Button SelectInterfaceButton;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

