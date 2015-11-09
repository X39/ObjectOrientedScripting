namespace WrapperUI
{
    partial class Form1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCompile = new System.Windows.Forms.TabPage();
            this.gbProjectInformations = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tbProjectBuildFolder = new System.Windows.Forms.TextBox();
            this.tbProjectOutFolder = new System.Windows.Forms.TextBox();
            this.tbProjectMainFile = new System.Windows.Forms.TextBox();
            this.tbProjectAuthor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbProjectTitle = new System.Windows.Forms.TextBox();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.gbCompilerFlags = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbCompilerFlags = new System.Windows.Forms.ListBox();
            this.btnCompilerFlagAdd = new System.Windows.Forms.Button();
            this.btnCompilerflagSet = new System.Windows.Forms.Button();
            this.tbDefineRealString = new System.Windows.Forms.TextBox();
            this.btnDoCompile = new System.Windows.Forms.Button();
            this.btnSetProjectPath = new System.Windows.Forms.Button();
            this.tbProjPath = new System.Windows.Forms.TextBox();
            this.tabGenProject = new System.Windows.Forms.TabPage();
            this.btnGenProject = new System.Windows.Forms.Button();
            this.btnSetGenPath = new System.Windows.Forms.Button();
            this.tbGenPath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label6 = new System.Windows.Forms.Label();
            this.tbCompilerVersion = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabCompile.SuspendLayout();
            this.gbProjectInformations.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.gbCompilerFlags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabGenProject.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabCompile);
            this.tabControl1.Controls.Add(this.tabGenProject);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(496, 474);
            this.tabControl1.TabIndex = 0;
            // 
            // tabCompile
            // 
            this.tabCompile.Controls.Add(this.gbProjectInformations);
            this.tabCompile.Controls.Add(this.btnSaveFile);
            this.tabCompile.Controls.Add(this.gbCompilerFlags);
            this.tabCompile.Controls.Add(this.btnDoCompile);
            this.tabCompile.Controls.Add(this.btnSetProjectPath);
            this.tabCompile.Controls.Add(this.tbProjPath);
            this.tabCompile.Location = new System.Drawing.Point(4, 22);
            this.tabCompile.Name = "tabCompile";
            this.tabCompile.Size = new System.Drawing.Size(488, 448);
            this.tabCompile.TabIndex = 0;
            this.tabCompile.Text = "Compile";
            this.tabCompile.UseVisualStyleBackColor = true;
            // 
            // gbProjectInformations
            // 
            this.gbProjectInformations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbProjectInformations.Controls.Add(this.tableLayoutPanel1);
            this.gbProjectInformations.Enabled = false;
            this.gbProjectInformations.Location = new System.Drawing.Point(11, 32);
            this.gbProjectInformations.Name = "gbProjectInformations";
            this.gbProjectInformations.Size = new System.Drawing.Size(463, 170);
            this.gbProjectInformations.TabIndex = 8;
            this.gbProjectInformations.TabStop = false;
            this.gbProjectInformations.Text = "Project Informations";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.13129F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.86871F));
            this.tableLayoutPanel1.Controls.Add(this.tbProjectBuildFolder, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbProjectOutFolder, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbProjectMainFile, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbProjectAuthor, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbProjectTitle, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbCompilerVersion, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(457, 151);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tbProjectBuildFolder
            // 
            this.tbProjectBuildFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbProjectBuildFolder.Location = new System.Drawing.Point(94, 107);
            this.tbProjectBuildFolder.Name = "tbProjectBuildFolder";
            this.tbProjectBuildFolder.Size = new System.Drawing.Size(360, 20);
            this.tbProjectBuildFolder.TabIndex = 9;
            // 
            // tbProjectOutFolder
            // 
            this.tbProjectOutFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbProjectOutFolder.Location = new System.Drawing.Point(94, 81);
            this.tbProjectOutFolder.Name = "tbProjectOutFolder";
            this.tbProjectOutFolder.Size = new System.Drawing.Size(360, 20);
            this.tbProjectOutFolder.TabIndex = 8;
            // 
            // tbProjectMainFile
            // 
            this.tbProjectMainFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbProjectMainFile.Location = new System.Drawing.Point(94, 55);
            this.tbProjectMainFile.Name = "tbProjectMainFile";
            this.tbProjectMainFile.Size = new System.Drawing.Size(360, 20);
            this.tbProjectMainFile.TabIndex = 7;
            // 
            // tbProjectAuthor
            // 
            this.tbProjectAuthor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbProjectAuthor.Location = new System.Drawing.Point(94, 29);
            this.tbProjectAuthor.Name = "tbProjectAuthor";
            this.tbProjectAuthor.Size = new System.Drawing.Size(360, 20);
            this.tbProjectAuthor.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Author";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Main File";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Output Folder";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Build Folder";
            // 
            // tbProjectTitle
            // 
            this.tbProjectTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbProjectTitle.Location = new System.Drawing.Point(94, 3);
            this.tbProjectTitle.Name = "tbProjectTitle";
            this.tbProjectTitle.Size = new System.Drawing.Size(360, 20);
            this.tbProjectTitle.TabIndex = 5;
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveFile.Enabled = false;
            this.btnSaveFile.Location = new System.Drawing.Point(405, 3);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(75, 23);
            this.btnSaveFile.TabIndex = 7;
            this.btnSaveFile.Text = "Save file";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // gbCompilerFlags
            // 
            this.gbCompilerFlags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCompilerFlags.Controls.Add(this.splitContainer1);
            this.gbCompilerFlags.Enabled = false;
            this.gbCompilerFlags.Location = new System.Drawing.Point(8, 208);
            this.gbCompilerFlags.Name = "gbCompilerFlags";
            this.gbCompilerFlags.Size = new System.Drawing.Size(472, 203);
            this.gbCompilerFlags.TabIndex = 6;
            this.gbCompilerFlags.TabStop = false;
            this.gbCompilerFlags.Text = "Compiler flags";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lbCompilerFlags);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnCompilerFlagAdd);
            this.splitContainer1.Panel2.Controls.Add(this.btnCompilerflagSet);
            this.splitContainer1.Panel2.Controls.Add(this.tbDefineRealString);
            this.splitContainer1.Size = new System.Drawing.Size(466, 184);
            this.splitContainer1.SplitterDistance = 155;
            this.splitContainer1.TabIndex = 0;
            // 
            // lbCompilerFlags
            // 
            this.lbCompilerFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCompilerFlags.FormattingEnabled = true;
            this.lbCompilerFlags.Location = new System.Drawing.Point(0, 0);
            this.lbCompilerFlags.Name = "lbCompilerFlags";
            this.lbCompilerFlags.Size = new System.Drawing.Size(155, 184);
            this.lbCompilerFlags.TabIndex = 0;
            this.lbCompilerFlags.SelectedIndexChanged += new System.EventHandler(this.lbCompilerFlags_SelectedIndexChanged);
            this.lbCompilerFlags.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbCompilerFlags_KeyDown);
            // 
            // btnCompilerFlagAdd
            // 
            this.btnCompilerFlagAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCompilerFlagAdd.Location = new System.Drawing.Point(159, 158);
            this.btnCompilerFlagAdd.Name = "btnCompilerFlagAdd";
            this.btnCompilerFlagAdd.Size = new System.Drawing.Size(145, 23);
            this.btnCompilerFlagAdd.TabIndex = 2;
            this.btnCompilerFlagAdd.Text = "Add Flag";
            this.btnCompilerFlagAdd.UseVisualStyleBackColor = true;
            this.btnCompilerFlagAdd.Click += new System.EventHandler(this.btnCompilerFlagAdd_Click);
            // 
            // btnCompilerflagSet
            // 
            this.btnCompilerflagSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCompilerflagSet.Enabled = false;
            this.btnCompilerflagSet.Location = new System.Drawing.Point(3, 158);
            this.btnCompilerflagSet.Name = "btnCompilerflagSet";
            this.btnCompilerflagSet.Size = new System.Drawing.Size(150, 23);
            this.btnCompilerflagSet.TabIndex = 1;
            this.btnCompilerflagSet.Text = "Set Flag";
            this.btnCompilerflagSet.UseVisualStyleBackColor = true;
            this.btnCompilerflagSet.Click += new System.EventHandler(this.btnCompilerflagSet_Click);
            // 
            // tbDefineRealString
            // 
            this.tbDefineRealString.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDefineRealString.Location = new System.Drawing.Point(3, 3);
            this.tbDefineRealString.Multiline = true;
            this.tbDefineRealString.Name = "tbDefineRealString";
            this.tbDefineRealString.Size = new System.Drawing.Size(301, 149);
            this.tbDefineRealString.TabIndex = 0;
            // 
            // btnDoCompile
            // 
            this.btnDoCompile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDoCompile.Enabled = false;
            this.btnDoCompile.Location = new System.Drawing.Point(8, 417);
            this.btnDoCompile.Name = "btnDoCompile";
            this.btnDoCompile.Size = new System.Drawing.Size(472, 23);
            this.btnDoCompile.TabIndex = 5;
            this.btnDoCompile.Text = "Compile existing project";
            this.btnDoCompile.UseVisualStyleBackColor = true;
            this.btnDoCompile.Click += new System.EventHandler(this.btnDoCompile_Click);
            // 
            // btnSetProjectPath
            // 
            this.btnSetProjectPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetProjectPath.Location = new System.Drawing.Point(322, 3);
            this.btnSetProjectPath.Name = "btnSetProjectPath";
            this.btnSetProjectPath.Size = new System.Drawing.Size(75, 23);
            this.btnSetProjectPath.TabIndex = 4;
            this.btnSetProjectPath.Text = "Load File";
            this.btnSetProjectPath.UseVisualStyleBackColor = true;
            this.btnSetProjectPath.Click += new System.EventHandler(this.btnSetProjectPath_Click);
            // 
            // tbProjPath
            // 
            this.tbProjPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProjPath.Location = new System.Drawing.Point(8, 3);
            this.tbProjPath.Name = "tbProjPath";
            this.tbProjPath.ReadOnly = true;
            this.tbProjPath.Size = new System.Drawing.Size(308, 20);
            this.tbProjPath.TabIndex = 3;
            // 
            // tabGenProject
            // 
            this.tabGenProject.Controls.Add(this.btnGenProject);
            this.tabGenProject.Controls.Add(this.btnSetGenPath);
            this.tabGenProject.Controls.Add(this.tbGenPath);
            this.tabGenProject.Location = new System.Drawing.Point(4, 22);
            this.tabGenProject.Name = "tabGenProject";
            this.tabGenProject.Size = new System.Drawing.Size(488, 448);
            this.tabGenProject.TabIndex = 1;
            this.tabGenProject.Text = "Generate Project";
            this.tabGenProject.UseVisualStyleBackColor = true;
            // 
            // btnGenProject
            // 
            this.btnGenProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenProject.Location = new System.Drawing.Point(8, 417);
            this.btnGenProject.Name = "btnGenProject";
            this.btnGenProject.Size = new System.Drawing.Size(472, 23);
            this.btnGenProject.TabIndex = 2;
            this.btnGenProject.Text = "Generate new empty project";
            this.btnGenProject.UseVisualStyleBackColor = true;
            this.btnGenProject.Click += new System.EventHandler(this.btnGenProject_Click);
            // 
            // btnSetGenPath
            // 
            this.btnSetGenPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetGenPath.Location = new System.Drawing.Point(410, 3);
            this.btnSetGenPath.Name = "btnSetGenPath";
            this.btnSetGenPath.Size = new System.Drawing.Size(75, 23);
            this.btnSetGenPath.TabIndex = 1;
            this.btnSetGenPath.Text = "Set Path";
            this.btnSetGenPath.UseVisualStyleBackColor = true;
            this.btnSetGenPath.Click += new System.EventHandler(this.btnSetGenPath_Click);
            // 
            // tbGenPath
            // 
            this.tbGenPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbGenPath.Location = new System.Drawing.Point(8, 3);
            this.tbGenPath.Name = "tbGenPath";
            this.tbGenPath.ReadOnly = true;
            this.tbGenPath.Size = new System.Drawing.Size(396, 20);
            this.tbGenPath.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "project.oosproj";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Compiler Version";
            // 
            // tbCompilerVersion
            // 
            this.tbCompilerVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCompilerVersion.Location = new System.Drawing.Point(94, 133);
            this.tbCompilerVersion.Name = "tbCompilerVersion";
            this.tbCompilerVersion.Size = new System.Drawing.Size(360, 20);
            this.tbCompilerVersion.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 474);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(512, 512);
            this.Name = "Form1";
            this.Text = "WrapperUI";
            this.tabControl1.ResumeLayout(false);
            this.tabCompile.ResumeLayout(false);
            this.tabCompile.PerformLayout();
            this.gbProjectInformations.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.gbCompilerFlags.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabGenProject.ResumeLayout(false);
            this.tabGenProject.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCompile;
        private System.Windows.Forms.TabPage tabGenProject;
        private System.Windows.Forms.TextBox tbGenPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnDoCompile;
        private System.Windows.Forms.Button btnSetProjectPath;
        private System.Windows.Forms.Button btnGenProject;
        private System.Windows.Forms.Button btnSetGenPath;
        private System.Windows.Forms.GroupBox gbCompilerFlags;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lbCompilerFlags;
        private System.Windows.Forms.Button btnCompilerFlagAdd;
        private System.Windows.Forms.Button btnCompilerflagSet;
        private System.Windows.Forms.TextBox tbDefineRealString;
        public System.Windows.Forms.TextBox tbProjPath;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox gbProjectInformations;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbProjectBuildFolder;
        private System.Windows.Forms.TextBox tbProjectOutFolder;
        private System.Windows.Forms.TextBox tbProjectMainFile;
        private System.Windows.Forms.TextBox tbProjectAuthor;
        private System.Windows.Forms.TextBox tbProjectTitle;
        private System.Windows.Forms.TextBox tbCompilerVersion;
        private System.Windows.Forms.Label label6;
    }
}

