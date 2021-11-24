namespace BigDataAnalyzer.Forms
{
    partial class LoadAnalyzePicturesForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imageListPhotos = new System.Windows.Forms.ImageList(this.components);
            this.pictureBoxPhoto = new System.Windows.Forms.PictureBox();
            this.startGettingColorsButton = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.dgvPreData = new System.Windows.Forms.DataGridView();
            this.loadPhotoButton = new System.Windows.Forms.Button();
            this.openPhotosDialog = new System.Windows.Forms.OpenFileDialog();
            this.rectangleListComboBox = new System.Windows.Forms.ComboBox();
            this.skinNoSkinCheckBox = new System.Windows.Forms.CheckBox();
            this.addRectangleButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhoto)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreData)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageListPhotos
            // 
            this.imageListPhotos.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListPhotos.ImageSize = new System.Drawing.Size(256, 256);
            this.imageListPhotos.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pictureBoxPhoto
            // 
            this.pictureBoxPhoto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPhoto.Location = new System.Drawing.Point(3, 16);
            this.pictureBoxPhoto.Name = "pictureBoxPhoto";
            this.pictureBoxPhoto.Size = new System.Drawing.Size(480, 426);
            this.pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPhoto.TabIndex = 0;
            this.pictureBoxPhoto.TabStop = false;
            this.pictureBoxPhoto.Click += new System.EventHandler(this.pictureBoxPhoto_Click);
            this.pictureBoxPhoto.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxPhoto_Paint);
            this.pictureBoxPhoto.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxPhoto_MouseDown);
            this.pictureBoxPhoto.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxPhoto_MouseMove);
            this.pictureBoxPhoto.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxPhoto_MouseUp);
            // 
            // startGettingColorsButton
            // 
            this.startGettingColorsButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.startGettingColorsButton.Location = new System.Drawing.Point(0, 2);
            this.startGettingColorsButton.Name = "startGettingColorsButton";
            this.startGettingColorsButton.Size = new System.Drawing.Size(239, 23);
            this.startGettingColorsButton.TabIndex = 1;
            this.startGettingColorsButton.Text = "Выделить цвета";
            this.startGettingColorsButton.UseVisualStyleBackColor = true;
            this.startGettingColorsButton.Click += new System.EventHandler(this.startGettingColorsButton_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.dgvPreData);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(673, 2);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox7.Size = new System.Drawing.Size(221, 476);
            this.groupBox7.TabIndex = 7;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Предпросмотр данных";
            // 
            // dgvPreData
            // 
            this.dgvPreData.AllowUserToAddRows = false;
            this.dgvPreData.AllowUserToDeleteRows = false;
            this.dgvPreData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPreData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPreData.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvPreData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvPreData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPreData.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPreData.Location = new System.Drawing.Point(5, 15);
            this.dgvPreData.Name = "dgvPreData";
            this.dgvPreData.RowHeadersVisible = false;
            this.dgvPreData.Size = new System.Drawing.Size(216, 456);
            this.dgvPreData.TabIndex = 5;
            // 
            // loadPhotoButton
            // 
            this.loadPhotoButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.loadPhotoButton.Location = new System.Drawing.Point(0, 2);
            this.loadPhotoButton.Name = "loadPhotoButton";
            this.loadPhotoButton.Size = new System.Drawing.Size(243, 23);
            this.loadPhotoButton.TabIndex = 8;
            this.loadPhotoButton.Text = "Загрузить фото";
            this.loadPhotoButton.UseVisualStyleBackColor = true;
            this.loadPhotoButton.Click += new System.EventHandler(this.loadPhotoButton_Click);
            // 
            // openPhotosDialog
            // 
            this.openPhotosDialog.FileName = "openFileDialog1";
            this.openPhotosDialog.Filter = "png files (*.png)|*.png|jpg files (*.jpg)|*.jpg|jpeg files (*.jpeg)|*.jpeg|Bitmap" +
    " files (*.bmp)|*.bmp|All files (*.*)|*.*";
            this.openPhotosDialog.Multiselect = true;
            // 
            // rectangleListComboBox
            // 
            this.rectangleListComboBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rectangleListComboBox.FormattingEnabled = true;
            this.rectangleListComboBox.Location = new System.Drawing.Point(0, 29);
            this.rectangleListComboBox.Name = "rectangleListComboBox";
            this.rectangleListComboBox.Size = new System.Drawing.Size(173, 21);
            this.rectangleListComboBox.TabIndex = 9;
            this.rectangleListComboBox.SelectedIndexChanged += new System.EventHandler(this.rectangleListComboBox_SelectedIndexChanged);
            // 
            // skinNoSkinCheckBox
            // 
            this.skinNoSkinCheckBox.AutoSize = true;
            this.skinNoSkinCheckBox.Checked = true;
            this.skinNoSkinCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.skinNoSkinCheckBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.skinNoSkinCheckBox.Location = new System.Drawing.Point(0, 0);
            this.skinNoSkinCheckBox.Name = "skinNoSkinCheckBox";
            this.skinNoSkinCheckBox.Size = new System.Drawing.Size(173, 17);
            this.skinNoSkinCheckBox.TabIndex = 10;
            this.skinNoSkinCheckBox.Text = "Область выбора кожи";
            this.skinNoSkinCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.skinNoSkinCheckBox.UseVisualStyleBackColor = true;
            // 
            // addRectangleButton
            // 
            this.addRectangleButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.addRectangleButton.Location = new System.Drawing.Point(0, 50);
            this.addRectangleButton.Name = "addRectangleButton";
            this.addRectangleButton.Size = new System.Drawing.Size(173, 22);
            this.addRectangleButton.TabIndex = 11;
            this.addRectangleButton.Text = "Добавить область";
            this.addRectangleButton.UseVisualStyleBackColor = true;
            this.addRectangleButton.Click += new System.EventHandler(this.addRectangleButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox7, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(896, 480);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.skinNoSkinCheckBox);
            this.panel1.Controls.Add(this.rectangleListComboBox);
            this.panel1.Controls.Add(this.addRectangleButton);
            this.panel1.Location = new System.Drawing.Point(495, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(173, 72);
            this.panel1.TabIndex = 13;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(486, 474);
            this.splitContainer1.SplitterDistance = 445;
            this.splitContainer1.TabIndex = 13;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.startGettingColorsButton);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.loadPhotoButton);
            this.splitContainer2.Size = new System.Drawing.Size(486, 25);
            this.splitContainer2.SplitterDistance = 239;
            this.splitContainer2.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBoxPhoto);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(486, 445);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Картинка";
            // 
            // LoadAnalyzePicturesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 480);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "LoadAnalyzePicturesForm";
            this.Text = "LoadAnalyzePicturesForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoadAnalyzePicturesForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhoto)).EndInit();
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreData)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageListPhotos;
        private System.Windows.Forms.PictureBox pictureBoxPhoto;
        private System.Windows.Forms.Button startGettingColorsButton;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button loadPhotoButton;
        private System.Windows.Forms.OpenFileDialog openPhotosDialog;
        private System.Windows.Forms.DataGridView dgvPreData;
        private System.Windows.Forms.ComboBox rectangleListComboBox;
        private System.Windows.Forms.CheckBox skinNoSkinCheckBox;
        private System.Windows.Forms.Button addRectangleButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}