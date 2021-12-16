namespace BigDataAnalyzer.Forms
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileRootMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TeachingFileOpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AnalyzingFileOpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.FileExitStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditRootMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditOptionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AnalyseRootMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AnalyseCompareMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AnalyseClearModelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpRootMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpAboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabAnalyse = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBoxTesting = new System.Windows.Forms.GroupBox();
            this.dgvTestingSource = new System.Windows.Forms.DataGridView();
            this.btnTestingRun = new System.Windows.Forms.Button();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.graphTesting = new ZedGraph.ZedGraphControl();
            this.tabTeaching = new System.Windows.Forms.TabPage();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.dgvLearningSource = new System.Windows.Forms.DataGridView();
            this.btnSampleRunAnalysis = new System.Windows.Forms.Button();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.graphInput = new ZedGraph.ZedGraphControl();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.groupBoxTeachingResultGraph = new System.Windows.Forms.GroupBox();
            this.graphTeachingResult = new ZedGraph.ZedGraphControl();
            this.groupBoxTreeView = new System.Windows.Forms.GroupBox();
            this.menuStrip1.SuspendLayout();
            this.tabAnalyse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxTesting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestingSource)).BeginInit();
            this.groupBox11.SuspendLayout();
            this.tabTeaching.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).BeginInit();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.Panel2.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLearningSource)).BeginInit();
            this.groupBox15.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileRootMenuItem,
            this.EditRootMenuItem,
            this.AnalyseRootMenuItem,
            this.HelpRootMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(834, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileRootMenuItem
            // 
            this.FileRootMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TeachingFileOpenMenuItem,
            this.AnalyzingFileOpenMenuItem,
            this.toolStripSeparator3,
            this.FileExitStripMenuItem});
            this.FileRootMenuItem.Name = "FileRootMenuItem";
            this.FileRootMenuItem.Size = new System.Drawing.Size(48, 20);
            this.FileRootMenuItem.Text = "Файл";
            // 
            // TeachingFileOpenMenuItem
            // 
            this.TeachingFileOpenMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TeachingFileOpenMenuItem.Name = "TeachingFileOpenMenuItem";
            this.TeachingFileOpenMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.TeachingFileOpenMenuItem.Size = new System.Drawing.Size(338, 22);
            this.TeachingFileOpenMenuItem.Text = "Открыть файл обучающей выборки";
            this.TeachingFileOpenMenuItem.Click += new System.EventHandler(this.MenuTeachingFileOpen_Click);
            // 
            // AnalyzingFileOpenMenuItem
            // 
            this.AnalyzingFileOpenMenuItem.Name = "AnalyzingFileOpenMenuItem";
            this.AnalyzingFileOpenMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.AnalyzingFileOpenMenuItem.Size = new System.Drawing.Size(338, 22);
            this.AnalyzingFileOpenMenuItem.Text = "Открыть файл анализируемой выборки";
            this.AnalyzingFileOpenMenuItem.Click += new System.EventHandler(this.MenuAnalyzingFileOpen_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(335, 6);
            // 
            // FileExitStripMenuItem
            // 
            this.FileExitStripMenuItem.Name = "FileExitStripMenuItem";
            this.FileExitStripMenuItem.Size = new System.Drawing.Size(338, 22);
            this.FileExitStripMenuItem.Text = "Выход";
            // 
            // EditRootMenuItem
            // 
            this.EditRootMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditOptionsMenuItem});
            this.EditRootMenuItem.Name = "EditRootMenuItem";
            this.EditRootMenuItem.Size = new System.Drawing.Size(59, 20);
            this.EditRootMenuItem.Text = "Правка";
            // 
            // EditOptionsMenuItem
            // 
            this.EditOptionsMenuItem.Name = "EditOptionsMenuItem";
            this.EditOptionsMenuItem.Size = new System.Drawing.Size(247, 22);
            this.EditOptionsMenuItem.Text = "Настройки обучения и анализа";
            this.EditOptionsMenuItem.Click += new System.EventHandler(this.EditOptionsMenuItem_Click);
            // 
            // AnalyseRootMenuItem
            // 
            this.AnalyseRootMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AnalyseCompareMenuItem,
            this.AnalyseClearModelMenuItem});
            this.AnalyseRootMenuItem.Name = "AnalyseRootMenuItem";
            this.AnalyseRootMenuItem.Size = new System.Drawing.Size(70, 20);
            this.AnalyseRootMenuItem.Text = "Действия";
            // 
            // AnalyseCompareMenuItem
            // 
            this.AnalyseCompareMenuItem.Name = "AnalyseCompareMenuItem";
            this.AnalyseCompareMenuItem.Size = new System.Drawing.Size(223, 22);
            this.AnalyseCompareMenuItem.Text = "Сравнить";
            this.AnalyseCompareMenuItem.Click += new System.EventHandler(this.AnalyseCompareMenuItem_Click);
            // 
            // AnalyseClearModelMenuItem
            // 
            this.AnalyseClearModelMenuItem.Name = "AnalyseClearModelMenuItem";
            this.AnalyseClearModelMenuItem.Size = new System.Drawing.Size(223, 22);
            this.AnalyseClearModelMenuItem.Text = "Очистить текущую модель";
            this.AnalyseClearModelMenuItem.Click += new System.EventHandler(this.AnalyseClearModelMenuItem_Click);
            // 
            // HelpRootMenuItem
            // 
            this.HelpRootMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpAboutMenuItem});
            this.HelpRootMenuItem.Name = "HelpRootMenuItem";
            this.HelpRootMenuItem.Size = new System.Drawing.Size(44, 20);
            this.HelpRootMenuItem.Text = "&Help";
            // 
            // HelpAboutMenuItem
            // 
            this.HelpAboutMenuItem.Name = "HelpAboutMenuItem";
            this.HelpAboutMenuItem.Size = new System.Drawing.Size(107, 22);
            this.HelpAboutMenuItem.Text = "&About";
            this.HelpAboutMenuItem.Click += new System.EventHandler(this.HelpAboutMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // tabAnalyse
            // 
            this.tabAnalyse.Controls.Add(this.splitContainer1);
            this.tabAnalyse.Location = new System.Drawing.Point(4, 22);
            this.tabAnalyse.Name = "tabAnalyse";
            this.tabAnalyse.Padding = new System.Windows.Forms.Padding(3);
            this.tabAnalyse.Size = new System.Drawing.Size(826, 561);
            this.tabAnalyse.TabIndex = 12;
            this.tabAnalyse.Text = "Анализ";
            this.tabAnalyse.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxTesting);
            this.splitContainer1.Panel1.Controls.Add(this.btnTestingRun);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox11);
            this.splitContainer1.Size = new System.Drawing.Size(820, 555);
            this.splitContainer1.SplitterDistance = 201;
            this.splitContainer1.TabIndex = 6;
            // 
            // groupBoxTesting
            // 
            this.groupBoxTesting.Controls.Add(this.dgvTestingSource);
            this.groupBoxTesting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTesting.Location = new System.Drawing.Point(0, 0);
            this.groupBoxTesting.Name = "groupBoxTesting";
            this.groupBoxTesting.Size = new System.Drawing.Size(201, 495);
            this.groupBoxTesting.TabIndex = 2;
            this.groupBoxTesting.TabStop = false;
            this.groupBoxTesting.Text = "Анализируемая выборка";
            // 
            // dgvTestingSource
            // 
            this.dgvTestingSource.AllowUserToAddRows = false;
            this.dgvTestingSource.AllowUserToDeleteRows = false;
            this.dgvTestingSource.AllowUserToResizeRows = false;
            this.dgvTestingSource.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTestingSource.BackgroundColor = System.Drawing.Color.White;
            this.dgvTestingSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTestingSource.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTestingSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTestingSource.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTestingSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTestingSource.Location = new System.Drawing.Point(3, 16);
            this.dgvTestingSource.Name = "dgvTestingSource";
            this.dgvTestingSource.ReadOnly = true;
            this.dgvTestingSource.RowHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvTestingSource.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTestingSource.Size = new System.Drawing.Size(195, 476);
            this.dgvTestingSource.TabIndex = 1;
            // 
            // btnTestingRun
            // 
            this.btnTestingRun.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnTestingRun.Location = new System.Drawing.Point(0, 495);
            this.btnTestingRun.Name = "btnTestingRun";
            this.btnTestingRun.Size = new System.Drawing.Size(201, 60);
            this.btnTestingRun.TabIndex = 3;
            this.btnTestingRun.Text = "Анализ";
            this.btnTestingRun.UseVisualStyleBackColor = true;
            this.btnTestingRun.Click += new System.EventHandler(this.btnTestingRun_Click);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.graphTesting);
            this.groupBox11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox11.Location = new System.Drawing.Point(0, 0);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(615, 555);
            this.groupBox11.TabIndex = 4;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "График анализируемой выборки";
            // 
            // graphTesting
            // 
            this.graphTesting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphTesting.Location = new System.Drawing.Point(3, 16);
            this.graphTesting.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.graphTesting.Name = "graphTesting";
            this.graphTesting.ScrollGrace = 0D;
            this.graphTesting.ScrollMaxX = 0D;
            this.graphTesting.ScrollMaxY = 0D;
            this.graphTesting.ScrollMaxY2 = 0D;
            this.graphTesting.ScrollMinX = 0D;
            this.graphTesting.ScrollMinY = 0D;
            this.graphTesting.ScrollMinY2 = 0D;
            this.graphTesting.Size = new System.Drawing.Size(609, 536);
            this.graphTesting.TabIndex = 3;
            this.graphTesting.UseExtendedPrintDialog = true;
            // 
            // tabTeaching
            // 
            this.tabTeaching.Controls.Add(this.splitContainer7);
            this.tabTeaching.Location = new System.Drawing.Point(4, 22);
            this.tabTeaching.Name = "tabTeaching";
            this.tabTeaching.Padding = new System.Windows.Forms.Padding(3);
            this.tabTeaching.Size = new System.Drawing.Size(826, 561);
            this.tabTeaching.TabIndex = 0;
            this.tabTeaching.Text = "Обучение модели";
            this.tabTeaching.UseVisualStyleBackColor = true;
            // 
            // splitContainer7
            // 
            this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer7.Location = new System.Drawing.Point(3, 3);
            this.splitContainer7.Name = "splitContainer7";
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.Controls.Add(this.groupBox7);
            this.splitContainer7.Panel1.Controls.Add(this.btnSampleRunAnalysis);
            // 
            // splitContainer7.Panel2
            // 
            this.splitContainer7.Panel2.Controls.Add(this.groupBox15);
            this.splitContainer7.Size = new System.Drawing.Size(820, 555);
            this.splitContainer7.SplitterDistance = 200;
            this.splitContainer7.TabIndex = 9;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.dgvLearningSource);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(0, 0);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox7.Size = new System.Drawing.Size(200, 506);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Данные для обучения";
            // 
            // dgvLearningSource
            // 
            this.dgvLearningSource.AllowUserToAddRows = false;
            this.dgvLearningSource.AllowUserToDeleteRows = false;
            this.dgvLearningSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLearningSource.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLearningSource.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvLearningSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvLearningSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLearningSource.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvLearningSource.Location = new System.Drawing.Point(2, 15);
            this.dgvLearningSource.Name = "dgvLearningSource";
            this.dgvLearningSource.RowHeadersVisible = false;
            this.dgvLearningSource.Size = new System.Drawing.Size(195, 486);
            this.dgvLearningSource.TabIndex = 5;
            // 
            // btnSampleRunAnalysis
            // 
            this.btnSampleRunAnalysis.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSampleRunAnalysis.Location = new System.Drawing.Point(0, 506);
            this.btnSampleRunAnalysis.Margin = new System.Windows.Forms.Padding(2);
            this.btnSampleRunAnalysis.Name = "btnSampleRunAnalysis";
            this.btnSampleRunAnalysis.Size = new System.Drawing.Size(200, 49);
            this.btnSampleRunAnalysis.TabIndex = 7;
            this.btnSampleRunAnalysis.Text = "Обучение";
            this.btnSampleRunAnalysis.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSampleRunAnalysis.UseVisualStyleBackColor = true;
            this.btnSampleRunAnalysis.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.graphInput);
            this.groupBox15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox15.Location = new System.Drawing.Point(0, 0);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(616, 555);
            this.groupBox15.TabIndex = 7;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "График обучающей выборки";
            // 
            // graphInput
            // 
            this.graphInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphInput.Location = new System.Drawing.Point(3, 16);
            this.graphInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.graphInput.Name = "graphInput";
            this.graphInput.ScrollGrace = 0D;
            this.graphInput.ScrollMaxX = 0D;
            this.graphInput.ScrollMaxY = 0D;
            this.graphInput.ScrollMaxY2 = 0D;
            this.graphInput.ScrollMinX = 0D;
            this.graphInput.ScrollMinY = 0D;
            this.graphInput.ScrollMinY2 = 0D;
            this.graphInput.Size = new System.Drawing.Size(610, 536);
            this.graphInput.TabIndex = 4;
            this.graphInput.UseExtendedPrintDialog = true;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabTeaching);
            this.tabControl.Controls.Add(this.tabAnalyse);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 24);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(834, 587);
            this.tabControl.TabIndex = 14;
            // 
            // groupBoxTeachingResultGraph
            // 
            this.groupBoxTeachingResultGraph.AutoSize = true;
            this.groupBoxTeachingResultGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTeachingResultGraph.Location = new System.Drawing.Point(0, 0);
            this.groupBoxTeachingResultGraph.Name = "groupBoxTeachingResultGraph";
            this.groupBoxTeachingResultGraph.Size = new System.Drawing.Size(614, 555);
            this.groupBoxTeachingResultGraph.TabIndex = 11;
            this.groupBoxTeachingResultGraph.TabStop = false;
            // 
            // graphTeachingResult
            // 
            this.graphTeachingResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphTeachingResult.Location = new System.Drawing.Point(3, 16);
            this.graphTeachingResult.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.graphTeachingResult.Name = "graphTeachingResult";
            this.graphTeachingResult.ScrollGrace = 0D;
            this.graphTeachingResult.ScrollMaxX = 0D;
            this.graphTeachingResult.ScrollMaxY = 0D;
            this.graphTeachingResult.ScrollMaxY2 = 0D;
            this.graphTeachingResult.ScrollMinX = 0D;
            this.graphTeachingResult.ScrollMinY = 0D;
            this.graphTeachingResult.ScrollMinY2 = 0D;
            this.graphTeachingResult.Size = new System.Drawing.Size(608, 536);
            this.graphTeachingResult.TabIndex = 3;
            this.graphTeachingResult.UseExtendedPrintDialog = true;
            // 
            // groupBoxTreeView
            // 
            this.groupBoxTreeView.Location = new System.Drawing.Point(0, 0);
            this.groupBoxTreeView.Name = "groupBoxTreeView";
            this.groupBoxTreeView.Size = new System.Drawing.Size(200, 100);
            this.groupBoxTreeView.TabIndex = 0;
            this.groupBoxTreeView.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(834, 611);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip1);
            this.Name = "MainForm";
            this.Text = "Анализатор больших данных";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabAnalyse.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxTesting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestingSource)).EndInit();
            this.groupBox11.ResumeLayout(false);
            this.tabTeaching.ResumeLayout(false);
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).EndInit();
            this.splitContainer7.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLearningSource)).EndInit();
            this.groupBox15.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileRootMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TeachingFileOpenMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem FileExitStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpRootMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpAboutMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem AnalyseRootMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AnalyseCompareMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AnalyzingFileOpenMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditRootMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditOptionsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AnalyseClearModelMenuItem;
        private System.Windows.Forms.TabPage tabAnalyse;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBoxTesting;
        private System.Windows.Forms.DataGridView dgvTestingSource;
        private System.Windows.Forms.Button btnTestingRun;
        private System.Windows.Forms.GroupBox groupBox11;
        private ZedGraph.ZedGraphControl graphTesting;
        private System.Windows.Forms.TabPage tabTeaching;
        private System.Windows.Forms.SplitContainer splitContainer7;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.DataGridView dgvLearningSource;
        private System.Windows.Forms.Button btnSampleRunAnalysis;
        private System.Windows.Forms.GroupBox groupBox15;
        private ZedGraph.ZedGraphControl graphInput;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.GroupBox groupBoxTeachingResultGraph;
        private ZedGraph.ZedGraphControl graphTeachingResult;
        private System.Windows.Forms.GroupBox groupBoxTreeView;
    }
}

