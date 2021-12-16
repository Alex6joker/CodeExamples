namespace BigDataAnalyzer.Forms
{
    partial class OptionsForm
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
            this.groupBoxClassifierParams = new System.Windows.Forms.GroupBox();
            this.comboBoxClassifierColumnParam = new System.Windows.Forms.ComboBox();
            this.comboBoxSecondDataColumnParam = new System.Windows.Forms.ComboBox();
            this.comboBoxFirstDataColumnParam = new System.Windows.Forms.ComboBox();
            this.labelClassifierColumnParam = new System.Windows.Forms.Label();
            this.labelSecondDataColumnParam = new System.Windows.Forms.Label();
            this.labelFirstDataColumnParam = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxClassifierParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxClassifierParams
            // 
            this.groupBoxClassifierParams.Controls.Add(this.comboBoxClassifierColumnParam);
            this.groupBoxClassifierParams.Controls.Add(this.comboBoxSecondDataColumnParam);
            this.groupBoxClassifierParams.Controls.Add(this.comboBoxFirstDataColumnParam);
            this.groupBoxClassifierParams.Controls.Add(this.labelClassifierColumnParam);
            this.groupBoxClassifierParams.Controls.Add(this.labelSecondDataColumnParam);
            this.groupBoxClassifierParams.Controls.Add(this.labelFirstDataColumnParam);
            this.groupBoxClassifierParams.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxClassifierParams.Location = new System.Drawing.Point(0, 0);
            this.groupBoxClassifierParams.Name = "groupBoxClassifierParams";
            this.groupBoxClassifierParams.Size = new System.Drawing.Size(384, 109);
            this.groupBoxClassifierParams.TabIndex = 0;
            this.groupBoxClassifierParams.TabStop = false;
            this.groupBoxClassifierParams.Text = "Настройка классификатора";
            // 
            // comboBoxClassifierColumnParam
            // 
            this.comboBoxClassifierColumnParam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxClassifierColumnParam.FormattingEnabled = true;
            this.comboBoxClassifierColumnParam.Location = new System.Drawing.Point(229, 73);
            this.comboBoxClassifierColumnParam.Name = "comboBoxClassifierColumnParam";
            this.comboBoxClassifierColumnParam.Size = new System.Drawing.Size(125, 21);
            this.comboBoxClassifierColumnParam.TabIndex = 1;
            // 
            // comboBoxSecondDataColumnParam
            // 
            this.comboBoxSecondDataColumnParam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSecondDataColumnParam.FormattingEnabled = true;
            this.comboBoxSecondDataColumnParam.Location = new System.Drawing.Point(229, 46);
            this.comboBoxSecondDataColumnParam.Name = "comboBoxSecondDataColumnParam";
            this.comboBoxSecondDataColumnParam.Size = new System.Drawing.Size(125, 21);
            this.comboBoxSecondDataColumnParam.TabIndex = 1;
            // 
            // comboBoxFirstDataColumnParam
            // 
            this.comboBoxFirstDataColumnParam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFirstDataColumnParam.FormattingEnabled = true;
            this.comboBoxFirstDataColumnParam.Location = new System.Drawing.Point(229, 17);
            this.comboBoxFirstDataColumnParam.Name = "comboBoxFirstDataColumnParam";
            this.comboBoxFirstDataColumnParam.Size = new System.Drawing.Size(125, 21);
            this.comboBoxFirstDataColumnParam.TabIndex = 1;
            // 
            // labelClassifierColumnParam
            // 
            this.labelClassifierColumnParam.AutoSize = true;
            this.labelClassifierColumnParam.Location = new System.Drawing.Point(7, 78);
            this.labelClassifierColumnParam.Name = "labelClassifierColumnParam";
            this.labelClassifierColumnParam.Size = new System.Drawing.Size(222, 13);
            this.labelClassifierColumnParam.TabIndex = 0;
            this.labelClassifierColumnParam.Text = "Идентификатор столбца классификатора:";
            // 
            // labelSecondDataColumnParam
            // 
            this.labelSecondDataColumnParam.AutoSize = true;
            this.labelSecondDataColumnParam.Location = new System.Drawing.Point(7, 49);
            this.labelSecondDataColumnParam.Name = "labelSecondDataColumnParam";
            this.labelSecondDataColumnParam.Size = new System.Drawing.Size(217, 13);
            this.labelSecondDataColumnParam.TabIndex = 0;
            this.labelSecondDataColumnParam.Text = "Идентификатор второго столбца данных:";
            // 
            // labelFirstDataColumnParam
            // 
            this.labelFirstDataColumnParam.AutoSize = true;
            this.labelFirstDataColumnParam.Location = new System.Drawing.Point(7, 20);
            this.labelFirstDataColumnParam.Name = "labelFirstDataColumnParam";
            this.labelFirstDataColumnParam.Size = new System.Drawing.Size(218, 13);
            this.labelFirstDataColumnParam.TabIndex = 0;
            this.labelFirstDataColumnParam.Text = "Идентификатор первого столбца данных:";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(94, 126);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(200, 126);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Отменить";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxClassifierParams);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "OptionsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsForm_FormClosing);
            this.groupBoxClassifierParams.ResumeLayout(false);
            this.groupBoxClassifierParams.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxClassifierParams;
        private System.Windows.Forms.ComboBox comboBoxClassifierColumnParam;
        private System.Windows.Forms.ComboBox comboBoxSecondDataColumnParam;
        private System.Windows.Forms.ComboBox comboBoxFirstDataColumnParam;
        private System.Windows.Forms.Label labelClassifierColumnParam;
        private System.Windows.Forms.Label labelSecondDataColumnParam;
        private System.Windows.Forms.Label labelFirstDataColumnParam;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}