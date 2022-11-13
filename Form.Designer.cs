namespace RaidCompGenerator
{
    partial class Form
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fIleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.raidCompDataGridView = new System.Windows.Forms.DataGridView();
            this.Group1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Group2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Group3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Group4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Group5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelRaidComp = new System.Windows.Forms.Label();
            this.raidDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comboBoxRaidGroups = new System.Windows.Forms.ComboBox();
            this.textBoxRaidGroupCount = new System.Windows.Forms.TextBox();
            this.labelRaidGroups = new System.Windows.Forms.Label();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.buttonRegenerate = new System.Windows.Forms.Button();
            this.textBoxRandomSeed = new System.Windows.Forms.TextBox();
            this.labelRandomSeed = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.labelIterations = new System.Windows.Forms.Label();
            this.textBoxIterations = new System.Windows.Forms.TextBox();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.raidCompDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.raidDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "saveFileDialog";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fIleToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(726, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fIleToolStripMenuItem
            // 
            this.fIleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fIleToolStripMenuItem.Name = "fIleToolStripMenuItem";
            this.fIleToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fIleToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.openToolStripMenuItem.Text = "Import";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.saveToolStripMenuItem.Text = "Export";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // raidCompDataGridView
            // 
            this.raidCompDataGridView.AllowUserToAddRows = false;
            this.raidCompDataGridView.AllowUserToDeleteRows = false;
            this.raidCompDataGridView.AllowUserToResizeColumns = false;
            this.raidCompDataGridView.AllowUserToResizeRows = false;
            this.raidCompDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.raidCompDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Group1,
            this.Group2,
            this.Group3,
            this.Group4,
            this.Group5});
            this.raidCompDataGridView.Enabled = false;
            this.raidCompDataGridView.Location = new System.Drawing.Point(12, 40);
            this.raidCompDataGridView.MultiSelect = false;
            this.raidCompDataGridView.Name = "raidCompDataGridView";
            this.raidCompDataGridView.RowHeadersVisible = false;
            this.raidCompDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.raidCompDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.raidCompDataGridView.Size = new System.Drawing.Size(703, 135);
            this.raidCompDataGridView.TabIndex = 2;
            // 
            // Group1
            // 
            this.Group1.HeaderText = "Group 1";
            this.Group1.MaxInputLength = 30;
            this.Group1.Name = "Group1";
            this.Group1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Group1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Group1.Width = 140;
            // 
            // Group2
            // 
            this.Group2.HeaderText = "Group 2";
            this.Group2.MaxInputLength = 30;
            this.Group2.Name = "Group2";
            this.Group2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Group2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Group2.Width = 140;
            // 
            // Group3
            // 
            this.Group3.HeaderText = "Group 3";
            this.Group3.MaxInputLength = 30;
            this.Group3.Name = "Group3";
            this.Group3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Group3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Group3.Width = 140;
            // 
            // Group4
            // 
            this.Group4.HeaderText = "Group 4";
            this.Group4.MaxInputLength = 30;
            this.Group4.Name = "Group4";
            this.Group4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Group4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Group4.Width = 140;
            // 
            // Group5
            // 
            this.Group5.HeaderText = "Group 5";
            this.Group5.MaxInputLength = 30;
            this.Group5.Name = "Group5";
            this.Group5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Group5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Group5.Width = 140;
            // 
            // labelRaidComp
            // 
            this.labelRaidComp.AutoSize = true;
            this.labelRaidComp.Location = new System.Drawing.Point(9, 24);
            this.labelRaidComp.Name = "labelRaidComp";
            this.labelRaidComp.Size = new System.Drawing.Size(103, 13);
            this.labelRaidComp.TabIndex = 3;
            this.labelRaidComp.Text = "Desired Composition";
            // 
            // raidDataGridView
            // 
            this.raidDataGridView.AllowUserToAddRows = false;
            this.raidDataGridView.AllowUserToDeleteRows = false;
            this.raidDataGridView.AllowUserToResizeColumns = false;
            this.raidDataGridView.AllowUserToResizeRows = false;
            this.raidDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.raidDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.raidDataGridView.Enabled = false;
            this.raidDataGridView.Location = new System.Drawing.Point(12, 238);
            this.raidDataGridView.MultiSelect = false;
            this.raidDataGridView.Name = "raidDataGridView";
            this.raidDataGridView.RowHeadersVisible = false;
            this.raidDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.raidDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.raidDataGridView.Size = new System.Drawing.Size(703, 135);
            this.raidDataGridView.TabIndex = 4;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Group 1";
            this.dataGridViewTextBoxColumn1.MaxInputLength = 30;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 140;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Group 2";
            this.dataGridViewTextBoxColumn2.MaxInputLength = 30;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 140;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Group 3";
            this.dataGridViewTextBoxColumn3.MaxInputLength = 30;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 140;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Group 4";
            this.dataGridViewTextBoxColumn4.MaxInputLength = 30;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 140;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Group 5";
            this.dataGridViewTextBoxColumn5.MaxInputLength = 30;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 140;
            // 
            // comboBoxRaidGroups
            // 
            this.comboBoxRaidGroups.FormattingEnabled = true;
            this.comboBoxRaidGroups.Location = new System.Drawing.Point(12, 211);
            this.comboBoxRaidGroups.Name = "comboBoxRaidGroups";
            this.comboBoxRaidGroups.Size = new System.Drawing.Size(121, 21);
            this.comboBoxRaidGroups.TabIndex = 5;
            this.comboBoxRaidGroups.SelectedValueChanged += new System.EventHandler(this.comboBoxRaidGroups_SelectedValueChanged);
            // 
            // textBoxRaidGroupCount
            // 
            this.textBoxRaidGroupCount.Location = new System.Drawing.Point(84, 185);
            this.textBoxRaidGroupCount.Name = "textBoxRaidGroupCount";
            this.textBoxRaidGroupCount.Size = new System.Drawing.Size(49, 20);
            this.textBoxRaidGroupCount.TabIndex = 6;
            this.textBoxRaidGroupCount.Text = "3";
            // 
            // labelRaidGroups
            // 
            this.labelRaidGroups.AutoSize = true;
            this.labelRaidGroups.Location = new System.Drawing.Point(9, 188);
            this.labelRaidGroups.Name = "labelRaidGroups";
            this.labelRaidGroups.Size = new System.Drawing.Size(69, 13);
            this.labelRaidGroups.TabIndex = 7;
            this.labelRaidGroups.Text = "Raid Groups:";
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(443, 184);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(80, 22);
            this.buttonGenerate.TabIndex = 8;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // buttonRegenerate
            // 
            this.buttonRegenerate.Location = new System.Drawing.Point(443, 209);
            this.buttonRegenerate.Name = "buttonRegenerate";
            this.buttonRegenerate.Size = new System.Drawing.Size(80, 22);
            this.buttonRegenerate.TabIndex = 9;
            this.buttonRegenerate.Text = "Regenerate";
            this.buttonRegenerate.UseVisualStyleBackColor = true;
            this.buttonRegenerate.Click += new System.EventHandler(this.buttonRegenerate_Click);
            // 
            // textBoxRandomSeed
            // 
            this.textBoxRandomSeed.Location = new System.Drawing.Point(337, 185);
            this.textBoxRandomSeed.Name = "textBoxRandomSeed";
            this.textBoxRandomSeed.Size = new System.Drawing.Size(100, 20);
            this.textBoxRandomSeed.TabIndex = 10;
            // 
            // labelRandomSeed
            // 
            this.labelRandomSeed.AutoSize = true;
            this.labelRandomSeed.Location = new System.Drawing.Point(253, 188);
            this.labelRandomSeed.Name = "labelRandomSeed";
            this.labelRandomSeed.Size = new System.Drawing.Size(78, 13);
            this.labelRandomSeed.TabIndex = 11;
            this.labelRandomSeed.Text = "Random Seed:";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(529, 185);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(185, 20);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 12;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // labelIterations
            // 
            this.labelIterations.AutoSize = true;
            this.labelIterations.Location = new System.Drawing.Point(139, 188);
            this.labelIterations.Name = "labelIterations";
            this.labelIterations.Size = new System.Drawing.Size(53, 13);
            this.labelIterations.TabIndex = 14;
            this.labelIterations.Text = "Iterations:";
            // 
            // textBoxIterations
            // 
            this.textBoxIterations.Location = new System.Drawing.Point(198, 185);
            this.textBoxIterations.Name = "textBoxIterations";
            this.textBoxIterations.Size = new System.Drawing.Size(49, 20);
            this.textBoxIterations.TabIndex = 13;
            this.textBoxIterations.Text = "100";
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 386);
            this.Controls.Add(this.labelIterations);
            this.Controls.Add(this.textBoxIterations);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.labelRandomSeed);
            this.Controls.Add(this.textBoxRandomSeed);
            this.Controls.Add(this.buttonRegenerate);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.labelRaidGroups);
            this.Controls.Add(this.textBoxRaidGroupCount);
            this.Controls.Add(this.comboBoxRaidGroups);
            this.Controls.Add(this.raidDataGridView);
            this.Controls.Add(this.labelRaidComp);
            this.Controls.Add(this.raidCompDataGridView);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Form";
            this.Text = "Raid Composition Generator";
            this.Load += new System.EventHandler(this.Form_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.raidCompDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.raidDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fIleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.DataGridView raidCompDataGridView;
        private System.Windows.Forms.Label labelRaidComp;
        private System.Windows.Forms.DataGridView raidDataGridView;
        private System.Windows.Forms.ComboBox comboBoxRaidGroups;
        private System.Windows.Forms.TextBox textBoxRaidGroupCount;
        private System.Windows.Forms.Label labelRaidGroups;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Group1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Group2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Group3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Group4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Group5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Button buttonRegenerate;
        private System.Windows.Forms.TextBox textBoxRandomSeed;
        private System.Windows.Forms.Label labelRandomSeed;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Label labelIterations;
        private System.Windows.Forms.TextBox textBoxIterations;
    }
}

