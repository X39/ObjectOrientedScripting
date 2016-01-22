using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Wrapper;
using System.Diagnostics;


namespace WrapperUI
{
    public partial class Form1 : Form
    {
        private Project proj;
        private bool changesMade;
        public Form1(string projectPath)
        {
            changesMade = false;
            InitializeComponent();
            if (!string.IsNullOrEmpty(projectPath))
                if (File.Exists(projectPath))
                    this.tbProjPath.Text = projectPath;
                else
                    this.tbGenPath.Text = projectPath;
            if (projectPath != "")
                this.LoadFile(true);
        }

        internal void LoadFile(bool surpressErrors)
        {
            string path = this.tbProjPath.Text;
            if(!File.Exists(path))
            {
                if (!surpressErrors)
                    MessageBox.Show("The file '" + path + "' is not existing.", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tbProjPath.Text = "";
                return;
            }
            try
            {
                proj = Project.openProject(path);
            }
            catch(Exception ex)
            {
                if (!surpressErrors)
                    MessageBox.Show(ex.Message + "\nWhile opening file '" + path + "'", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tbProjPath.Text = "";
                return;
            }
            tbProjectTitle.Text = proj.ProjectTitle;
            tbProjectAuthor.Text = proj.Author;
            tbProjectMainFile.Text = proj.Mainfile;
            tbProjectOutFolder.Text = proj.OutputFolder;
            tbProjectBuildFolder.Text = proj.Buildfolder;
            tbProjectSrcFolder.Text = proj.SrcFolder;
            tbCompilerVersion.Text = proj.CompilerVersion.ToString();

            lbCompilerFlags.Items.Clear();
            foreach (var it in proj.Defines)
            {
                this.lbCompilerFlags.Items.Add(it);
            }
            lbRessources.Items.Clear();
            foreach (var it in proj.Ressources)
            {
                this.lbRessources.Items.Add(it);
            }
            btnSetRessource.Enabled = false;
            btnCompilerflagSet.Enabled = false;
            this.btnSaveFile.Enabled = true;
            tcDefinesRessources.Enabled = true;
            gbProjectInformations.Enabled = true;
            btnDoCompile.Enabled = true;
            changesMade = false;
        }

        private void lbCompilerFlags_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(lbCompilerFlags.SelectedItem is Project.Define))
                return;
            tbDefineRealString.Text = ((Project.Define)lbCompilerFlags.SelectedItem).toReal();
            btnCompilerflagSet.Enabled = true;
        }

        private void btnCompilerflagSet_Click(object sender, EventArgs e)
        {
            try
            {
                changesMade = true;
                var define = ((Project.Define)lbCompilerFlags.SelectedItem);
                Project.Define.fromReal(tbDefineRealString.Text, (Project.Define)lbCompilerFlags.SelectedItem);
                int index = lbCompilerFlags.Items.IndexOf(define);
                lbCompilerFlags.Items.RemoveAt(index);
                lbCompilerFlags.Items.Insert(index, define);
                lbCompilerFlags.SelectedItem = define;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception was raised", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCompilerFlagAdd_Click(object sender, EventArgs e)
        {
            try
            {
                changesMade = true;
                var define = Project.Define.fromReal(tbDefineRealString.Text);
                proj.Defines.Add(define);
                lbCompilerFlags.Items.Add(define);
                lbCompilerFlags.SelectedItem = define;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception was raised", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            try
            {
                proj.ProjectTitle = tbProjectTitle.Text;
                proj.Author = tbProjectAuthor.Text;
                if (!File.Exists(tbProjectMainFile.Text))
                    throw new Exception("Main file '" + tbProjectMainFile.Text + "' is not existing.");
                proj.Mainfile = tbProjectMainFile.Text;
                proj.OutputFolder = tbProjectOutFolder.Text;
                proj.Buildfolder = tbProjectBuildFolder.Text;
                proj.SrcFolder = tbProjectSrcFolder.Text;
                proj.CompilerVersion = tbCompilerVersion.Text;
                
                proj.writeToFile(tbProjPath.Text);
                changesMade = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception was raised", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSetProjectPath_Click(object sender, EventArgs e)
        {
            if (changesMade)
            {
                var dlgRes = MessageBox.Show("There are unsaved changes.\nDo you want to save them now?", "You might forgot something?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                switch (dlgRes)
                {
                    case DialogResult.Yes:
                        this.btnSaveFile_Click(sender, null);
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbProjPath.Text = openFileDialog1.FileName;
                this.LoadFile(false);
            }
        }

        private void btnSetGenPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbGenPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void lbCompilerFlags_KeyDown(object sender, KeyEventArgs e)
        {
            if (lbCompilerFlags.SelectedItem == null)
                return;
            if(e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                changesMade = true;
                proj.Defines.Remove((Project.Define)lbCompilerFlags.SelectedItem);
                lbCompilerFlags.Items.Remove(lbCompilerFlags.SelectedItem);
            }
        }

        private void btnGenProject_Click(object sender, EventArgs e)
        {
            var executablePath = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            executablePath = executablePath.Substring(0, executablePath.LastIndexOf('\\'));
            Process p = new Process();
            p.StartInfo.FileName = executablePath + "\\Wrapper.exe";
            p.StartInfo.Arguments = "-a -gen \"" + tbGenPath.Text + '"';
            p.Start();
            p.WaitForExit();
            MessageBox.Show("Done");
            this.tabControl1.SelectedIndex = 0;
            tbProjPath.Text = tbGenPath.Text + '\\' + "project.oosproj";
            LoadFile(true);
        }

        private void btnDoCompile_Click(object sender, EventArgs e)
        {
            btnSaveFile_Click(sender, e);
            //ToDo: Analyze output
            //http://stackoverflow.com/questions/285760/how-to-spawn-a-process-and-capture-its-stdout-in-net
            var executablePath = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            executablePath = executablePath.Substring(0, executablePath.LastIndexOf('\\'));
            Process p = new Process();
            p.StartInfo.FileName = executablePath + "\\Wrapper.exe";
            p.StartInfo.Arguments = '"' + tbProjPath.Text + '"';
            p.Start();
            p.WaitForExit();
        }

        private void tbRessourcesInPath_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbRessourcesInPath.Text = openFileDialog1.FileName;
            }
        }

        private void btnAddRessource_Click(object sender, EventArgs e)
        {
            try
            {
                changesMade = true;
                var res = new Project.Ressource(tbRessourcesInPath.Text, tbRessourcesOutPath.Text, proj);
                proj.Ressources.Add(res);
                lbRessources.Items.Add(res);
                lbRessources.SelectedItem = res;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception was raised", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSetRessource_Click(object sender, EventArgs e)
        {
            try
            {
                changesMade = true;
                var res = ((Project.Ressource)lbRessources.SelectedItem);
                proj.Ressources.Remove(res);
                int index = lbRessources.Items.IndexOf(res);
                lbRessources.Items.RemoveAt(index);
                res = new Project.Ressource(tbRessourcesInPath.Text, tbRessourcesOutPath.Text, proj);
                proj.Ressources.Add(res);
                lbRessources.Items.Insert(index, res);
                lbRessources.SelectedItem = res;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception was raised", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbRessources_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(lbRessources.SelectedItem is Project.Ressource))
                return;
            tbRessourcesInPath.Text = ((Project.Ressource)lbRessources.SelectedItem).InPath;
            tbRessourcesOutPath.Text = ((Project.Ressource)lbRessources.SelectedItem).OutPath;
            btnSetRessource.Enabled = true;
        }

        private void lbRessources_KeyDown(object sender, KeyEventArgs e)
        {
            if (lbRessources.SelectedItem == null)
                return;
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                changesMade = true;
                proj.Ressources.Remove((Project.Ressource)lbRessources.SelectedItem);
                lbRessources.Items.Remove(lbRessources.SelectedItem);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(changesMade)
            {
                var dlgRes = MessageBox.Show("There are unsaved changes.\nDo you want to save them now?", "You might forgot something?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                switch(dlgRes)
                {
                    case DialogResult.Yes:
                        this.btnSaveFile_Click(sender, null);
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void tbProjectTitle_TextChanged(object sender, EventArgs e)
        {
            changesMade = true;
        }

        private void tbProjectAuthor_TextChanged(object sender, EventArgs e)
        {
            changesMade = true;
        }

        private void tbProjectMainFile_TextChanged(object sender, EventArgs e)
        {
            changesMade = true;
        }

        private void tbProjectOutFolder_TextChanged(object sender, EventArgs e)
        {
            changesMade = true;
        }

        private void tbProjectBuildFolder_TextChanged(object sender, EventArgs e)
        {
            changesMade = true;
        }

        private void tbProjectSrcFolder_TextChanged(object sender, EventArgs e)
        {
            changesMade = true;
        }

        private void tbCompilerVersion_TextChanged(object sender, EventArgs e)
        {
            changesMade = true;
        }
    }
}
