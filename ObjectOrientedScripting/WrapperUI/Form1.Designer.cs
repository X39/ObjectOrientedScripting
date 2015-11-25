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
            this.tcDefinesRessources = new System.Windows.Forms.TabControl();
            this.tpDefines = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbCompilerFlags = new System.Windows.Forms.ListBox();
            this.btnCompilerFlagAdd = new System.Windows.Forms.Button();
            this.btnCompilerflagSet = new System.Windows.Forms.Button();
            this.tbDefineRealString = new System.Windows.Forms.TextBox();
            this.tpRessources = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lbRessources = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tbRessourcesOutPath = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbRessourcesInPath = new System.Windows.Forms.TextBox();
            this.btnAddRessource = new System.Windows.Forms.Button();
            this.btnSetRessource = new System.Windows.Forms.Button();
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
            this.tbProjectSrcFolder = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbCompilerVersion = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSaveFile = new System.Windows.Forms.Button();
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
            this.tabControl1.SuspendLayout();
            this.tabCompile.SuspendLayout();
            this.tcDefinesRessources.SuspendLayout();
            this.tpDefines.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tpRessources.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.gbProjectInformations.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.tabCompile.Controls.Add(this.tcDefinesRessources);
            this.tabCompile.Controls.Add(this.gbProjectInformations);
            this.tabCompile.Controls.Add(this.btnSaveFile);
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
            // tcDefinesRessources
            // 
            this.tcDefinesRessources.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcDefinesRessources.Controls.Add(this.tpDefines);
            this.tcDefinesRessources.Controls.Add(this.tpRessources);
            this.tcDefinesRessources.Enabled = false;
            this.tcDefinesRessources.Location = new System.Drawing.Point(8, 241);
            this.tcDefinesRessources.Name = "tcDefinesRessources";
            this.tcDefinesRessources.SelectedIndex = 0;
            this.tcDefinesRessources.Size = new System.Drawing.Size(472, 170);
            this.tcDefinesRessources.TabIndex = 9;
            // 
            // tpDefines
            // 
            this.tpDefines.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.tpDefines.Controls.Add(this.splitContainer1);
            this.tpDefines.Location = new System.Drawing.Point(4, 22);
            this.tpDefines.Name = "tpDefines";
            this.tpDefines.Padding = new System.Windows.Forms.Padding(3);
            this.tpDefines.Size = new System.Drawing.Size(464, 144);
            this.tpDefines.TabIndex = 0;
            this.tpDefines.Text = "Defines";
            this.tpDefines.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
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
            this.splitContainer1.Size = new System.Drawing.Size(458, 138);
            this.splitContainer1.SplitterDistance = 152;
            this.splitContainer1.TabIndex = 0;
            // 
            // lbCompilerFlags
            // 
            this.lbCompilerFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCompilerFlags.FormattingEnabled = true;
            this.lbCompilerFlags.Location = new System.Drawing.Point(0, 0);
            this.lbCompilerFlags.Name = "lbCompilerFlags";
            this.lbCompilerFlags.Size = new System.Drawing.Size(152, 138);
            this.lbCompilerFlags.TabIndex = 0;
            this.lbCompilerFlags.SelectedIndexChanged += new System.EventHandler(this.lbCompilerFlags_SelectedIndexChanged);
            this.lbCompilerFlags.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbCompilerFlags_KeyDown);
            // 
            // btnCompilerFlagAdd
            // 
            this.btnCompilerFlagAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCompilerFlagAdd.Location = new System.Drawing.Point(159, 112);
            this.btnCompilerFlagAdd.Name = "btnCompilerFlagAdd";
            this.btnCompilerFlagAdd.Size = new System.Drawing.Size(140, 23);
            this.btnCompilerFlagAdd.TabIndex = 2;
            this.btnCompilerFlagAdd.Text = "Add Flag";
            this.btnCompilerFlagAdd.UseVisualStyleBackColor = true;
            this.btnCompilerFlagAdd.Click += new System.EventHandler(this.btnCompilerFlagAdd_Click);
            // 
            // btnCompilerflagSet
            // 
            this.btnCompilerflagSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCompilerflagSet.Enabled = false;
            this.btnCompilerflagSet.Location = new System.Drawing.Point(3, 112);
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
            this.tbDefineRealString.Size = new System.Drawing.Size(296, 103);
            this.tbDefineRealString.TabIndex = 0;
            // 
            // tpRessources
            // 
            this.tpRessources.Controls.Add(this.splitContainer2);
            this.tpRessources.Location = new System.Drawing.Point(4, 22);
            this.tpRessources.Name = "tpRessources";
            this.tpRessources.Padding = new System.Windows.Forms.Padding(3);
            this.tpRessources.Size = new System.Drawing.Size(464, 144);
            this.tpRessources.TabIndex = 1;
            this.tpRessources.Text = "Ressources";
            this.tpRessources.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lbRessources);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer2.Panel2.Controls.Add(this.btnAddRessource);
            this.splitContainer2.Panel2.Controls.Add(this.btnSetRessource);
            this.splitContainer2.Size = new System.Drawing.Size(458, 138);
            this.splitContainer2.SplitterDistance = 152;
            this.splitContainer2.TabIndex = 1;
            // 
            // lbRessources
            // 
            this.lbRessources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbRessources.FormattingEnabled = true;
            this.lbRessources.Location = new System.Drawing.Point(0, 0);
            this.lbRessources.Name = "lbRessources";
            this.lbRessources.Size = new System.Drawing.Size(152, 138);
            this.lbRessources.TabIndex = 0;
            this.lbRessources.SelectedIndexChanged += new System.EventHandler(this.lbRessources_SelectedIndexChanged);
            this.lbRessources.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbRessources_KeyDown);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.tbRessourcesOutPath, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tbRessourcesInPath, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(8, 8);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(289, 98);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // tbRessourcesOutPath
            // 
            this.tbRessourcesOutPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbRessourcesOutPath.Location = new System.Drawing.Point(33, 29);
            this.tbRessourcesOutPath.Name = "tbRessourcesOutPath";
            this.tbRessourcesOutPath.Size = new System.Drawing.Size(253, 20);
            this.tbRessourcesOutPath.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "In";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Out";
            // 
            // tbRessourcesInPath
            // 
            this.tbRessourcesInPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbRessourcesInPath.Location = new System.Drawing.Point(33, 3);
            this.tbRessourcesInPath.Name = "tbRessourcesInPath";
            this.tbRessourcesInPath.Size = new System.Drawing.Size(253, 20);
            this.tbRessourcesInPath.TabIndex = 5;
            this.tbRessourcesInPath.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tbRessourcesInPath_MouseDoubleClick);
            // 
            // btnAddRessource
            // 
            this.btnAddRessource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddRessource.Location = new System.Drawing.Point(159, 112);
            this.btnAddRessource.Name = "btnAddRessource";
            this.btnAddRessource.Size = new System.Drawing.Size(140, 23);
            this.btnAddRessource.TabIndex = 2;
            this.btnAddRessource.Text = "Add Ressource";
            this.btnAddRessource.UseVisualStyleBackColor = true;
            this.btnAddRessource.Click += new System.EventHandler(this.btnAddRessource_Click);
            // 
            // btnSetRessource
            // 
            this.btnSetRessource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetRessource.Enabled = false;
            this.btnSetRessource.Location = new System.Drawing.Point(3, 112);
            this.btnSetRessource.Name = "btnSetRessource";
            this.btnSetRessource.Size = new System.Drawing.Size(150, 23);
            this.btnSetRessource.TabIndex = 1;
            this.btnSetRessource.Text = "Set Ressource";
            this.btnSetRessource.UseVisualStyleBackColor = true;
            this.btnSetRessource.Click += new System.EventHandler(this.btnSetRessource_Click);
            // 
            // gbProjectInformations
            // 
            this.gbProjectInformations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbProjectInformations.Controls.Add(this.tableLayoutPanel1);
            this.gbProjectInformations.Enabled = false;
            this.gbProjectInformations.Location = new System.Drawing.Point(11, 32);
            this.gbProjectInformations.Name = "gbProjectInformations";
            this.gbProjectInformations.Size = new System.Drawing.Size(463, 203);
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
            this.tableLayoutPanel1.Controls.Add(this.tbProjectSrcFolder, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.tbCompilerVersion, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(457, 184);
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
            // tbProjectSrcFolder
            // 
            this.tbProjectSrcFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbProjectSrcFolder.Location = new System.Drawing.Point(94, 133);
            this.tbProjectSrcFolder.Name = "tbProjectSrcFolder";
            this.tbProjectSrcFolder.Size = new System.Drawing.Size(360, 20);
            this.tbProjectSrcFolder.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Source Folder";
            // 
            // tbCompilerVersion
            // 
            this.tbCompilerVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCompilerVersion.Location = new System.Drawing.Point(94, 159);
            this.tbCompilerVersion.Name = "tbCompilerVersion";
            this.tbCompilerVersion.Size = new System.Drawing.Size(360, 20);
            this.tbCompilerVersion.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 156);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Compiler Version";
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
            this.tcDefinesRessources.ResumeLayout(false);
            this.tpDefines.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tpRessources.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.gbProjectInformations.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
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
        private System.Windows.Forms.TextBox tbProjectSrcFolder;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbCompilerVersion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl tcDefinesRessources;
        private System.Windows.Forms.TabPage tpDefines;
        private System.Windows.Forms.TabPage tpRessources;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox lbRessources;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox tbRessourcesOutPath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnAddRessource;
        private System.Windows.Forms.Button btnSetRessource;
        private System.Windows.Forms.TextBox tbRessourcesInPath;
    }
}

