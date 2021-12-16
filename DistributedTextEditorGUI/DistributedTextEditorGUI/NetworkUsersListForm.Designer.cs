namespace Diplo
{
    partial class NetworkUsersListForm
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
            this.UsersGrid = new System.Windows.Forms.DataGridView();
            this.Head = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InterfacesIPs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DomainName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ComputerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.UsersGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // UsersGrid
            // 
            this.UsersGrid.AllowUserToAddRows = false;
            this.UsersGrid.AllowUserToDeleteRows = false;
            this.UsersGrid.AllowUserToResizeColumns = false;
            this.UsersGrid.AllowUserToResizeRows = false;
            this.UsersGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.UsersGrid.ColumnHeadersHeight = 20;
            this.UsersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.UsersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Head,
            this.InterfacesIPs,
            this.DomainName,
            this.ComputerName});
            this.UsersGrid.Cursor = System.Windows.Forms.Cursors.Default;
            this.UsersGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.UsersGrid.Location = new System.Drawing.Point(0, 49);
            this.UsersGrid.MultiSelect = false;
            this.UsersGrid.Name = "UsersGrid";
            this.UsersGrid.ReadOnly = true;
            this.UsersGrid.RowHeadersVisible = false;
            this.UsersGrid.RowHeadersWidth = 40;
            this.UsersGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.UsersGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.UsersGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.UsersGrid.Size = new System.Drawing.Size(583, 344);
            this.UsersGrid.TabIndex = 2;
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
            this.InterfacesIPs.MinimumWidth = 180;
            this.InterfacesIPs.Name = "InterfacesIPs";
            this.InterfacesIPs.ReadOnly = true;
            this.InterfacesIPs.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.InterfacesIPs.Width = 180;
            // 
            // DomainName
            // 
            this.DomainName.HeaderText = "Имя домена";
            this.DomainName.MinimumWidth = 180;
            this.DomainName.Name = "DomainName";
            this.DomainName.ReadOnly = true;
            this.DomainName.Width = 180;
            // 
            // ComputerName
            // 
            this.ComputerName.HeaderText = "Имя компьютера";
            this.ComputerName.MinimumWidth = 180;
            this.ComputerName.Name = "ComputerName";
            this.ComputerName.ReadOnly = true;
            this.ComputerName.Width = 180;
            // 
            // NetworkUsersListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 393);
            this.Controls.Add(this.UsersGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "NetworkUsersListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список пользователей текущей маркерной сети";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NetworkUsersListForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.UsersGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView UsersGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Head;
        private System.Windows.Forms.DataGridViewTextBoxColumn InterfacesIPs;
        private System.Windows.Forms.DataGridViewTextBoxColumn DomainName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComputerName;
    }
}