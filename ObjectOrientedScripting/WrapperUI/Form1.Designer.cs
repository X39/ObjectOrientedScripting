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
            this.tabGenProject = new System.Windows.Forms.TabPage();
            this.tbGenPath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnSetGenPath = new System.Windows.Forms.Button();
            this.btnGenProject = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSetProjectPath = new System.Windows.Forms.Button();
            this.tbProjPath = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbCompilerFlags = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnCompilerflagSet = new System.Windows.Forms.Button();
            this.btnCompilerFlagAdd = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabCompile.SuspendLayout();
            this.tabGenProject.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.tabControl1.Size = new System.Drawing.Size(496, 218);
            this.tabControl1.TabIndex = 0;
            // 
            // tabCompile
            // 
            this.tabCompile.Controls.Add(this.groupBox1);
            this.tabCompile.Controls.Add(this.button1);
            this.tabCompile.Controls.Add(this.btnSetProjectPath);
            this.tabCompile.Controls.Add(this.tbProjPath);
            this.tabCompile.Location = new System.Drawing.Point(4, 22);
            this.tabCompile.Name = "tabCompile";
            this.tabCompile.Size = new System.Drawing.Size(488, 192);
            this.tabCompile.TabIndex = 0;
            this.tabCompile.Text = "Compile";
            this.tabCompile.UseVisualStyleBackColor = true;
            // 
            // tabGenProject
            // 
            this.tabGenProject.Controls.Add(this.btnGenProject);
            this.tabGenProject.Controls.Add(this.btnSetGenPath);
            this.tabGenProject.Controls.Add(this.tbGenPath);
            this.tabGenProject.Location = new System.Drawing.Point(4, 22);
            this.tabGenProject.Name = "tabGenProject";
            this.tabGenProject.Size = new System.Drawing.Size(488, 192);
            this.tabGenProject.TabIndex = 1;
            this.tabGenProject.Text = "Generate Project";
            this.tabGenProject.UseVisualStyleBackColor = true;
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
            this.openFileDialog1.FileName = "openFileDialog1";
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
            // 
            // btnGenProject
            // 
            this.btnGenProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenProject.Location = new System.Drawing.Point(8, 161);
            this.btnGenProject.Name = "btnGenProject";
            this.btnGenProject.Size = new System.Drawing.Size(472, 23);
            this.btnGenProject.TabIndex = 2;
            this.btnGenProject.Text = "Generate new empty project";
            this.btnGenProject.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(8, 161);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(472, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Compile existing project";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnSetProjectPath
            // 
            this.btnSetProjectPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetProjectPath.Location = new System.Drawing.Point(410, 3);
            this.btnSetProjectPath.Name = "btnSetProjectPath";
            this.btnSetProjectPath.Size = new System.Drawing.Size(75, 23);
            this.btnSetProjectPath.TabIndex = 4;
            this.btnSetProjectPath.Text = "Set Path";
            this.btnSetProjectPath.UseVisualStyleBackColor = true;
            // 
            // tbProjPath
            // 
            this.tbProjPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProjPath.Location = new System.Drawing.Point(8, 3);
            this.tbProjPath.Name = "tbProjPath";
            this.tbProjPath.ReadOnly = true;
            this.tbProjPath.Size = new System.Drawing.Size(396, 20);
            this.tbProjPath.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Location = new System.Drawing.Point(8, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(472, 126);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Compiler flags";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Size = new System.Drawing.Size(466, 107);
            this.splitContainer1.SplitterDistance = 155;
            this.splitContainer1.TabIndex = 0;
            // 
            // lbCompilerFlags
            // 
            this.lbCompilerFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCompilerFlags.FormattingEnabled = true;
            this.lbCompilerFlags.Location = new System.Drawing.Point(0, 0);
            this.lbCompilerFlags.Name = "lbCompilerFlags";
            this.lbCompilerFlags.Size = new System.Drawing.Size(155, 107);
            this.lbCompilerFlags.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(301, 62);
            this.textBox1.TabIndex = 0;
            // 
            // btnCompilerflagSet
            // 
            this.btnCompilerflagSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCompilerflagSet.Enabled = false;
            this.btnCompilerflagSet.Location = new System.Drawing.Point(2, 80);
            this.btnCompilerflagSet.Name = "btnCompilerflagSet";
            this.btnCompilerflagSet.Size = new System.Drawing.Size(154, 23);
            this.btnCompilerflagSet.TabIndex = 1;
            this.btnCompilerflagSet.Text = "Set Flag";
            this.btnCompilerflagSet.UseVisualStyleBackColor = true;
            // 
            // btnCompilerFlagAdd
            // 
            this.btnCompilerFlagAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCompilerFlagAdd.Location = new System.Drawing.Point(159, 80);
            this.btnCompilerFlagAdd.Name = "btnCompilerFlagAdd";
            this.btnCompilerFlagAdd.Size = new System.Drawing.Size(145, 23);
            this.btnCompilerFlagAdd.TabIndex = 2;
            this.btnCompilerFlagAdd.Text = "Add Flag";
            this.btnCompilerFlagAdd.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 218);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(512, 256);
            this.Name = "Form1";
            this.Text = "WrapperUI";
            this.tabControl1.ResumeLayout(false);
            this.tabCompile.ResumeLayout(false);
            this.tabCompile.PerformLayout();
            this.tabGenProject.ResumeLayout(false);
            this.tabGenProject.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCompile;
        private System.Windows.Forms.TabPage tabGenProject;
        private System.Windows.Forms.TextBox tbGenPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSetProjectPath;
        private System.Windows.Forms.TextBox tbProjPath;
        private System.Windows.Forms.Button btnGenProject;
        private System.Windows.Forms.Button btnSetGenPath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lbCompilerFlags;
        private System.Windows.Forms.Button btnCompilerFlagAdd;
        private System.Windows.Forms.Button btnCompilerflagSet;
        private System.Windows.Forms.TextBox textBox1;
    }
}

