using System;
using System.IO;
using System.Xml;


namespace Wrapper
{
    public class Project
    {
        private string _projectTitle;
        public string ProjectTitle
        {
            set { this._projectTitle = value; }
            get { return this._projectTitle; }
        }
        private string _author;
        public string Author
        {
            set { this._author = value; }
            get { return this._author; }
        }
        private string _mainfile;
        public string Mainfile
        {
            set { this._mainfile = value; }
            get { return this._mainfile; }
        }
        private string _outputFolder;
        public string OutputFolder
        {
            set { this._outputFolder = value; }
            get { return this._outputFolder; }
        }

        private Project() {}
        static public Project openProject(string file)
        {
            if (file == null)
                throw new ArgumentException("Provided parameter 'file' is null");
            if (!File.Exists(file))
                throw new ArgumentException("Provided parameter 'file' is refering a not existing file");
            if (!file.EndsWith(".oosproj"))
                throw new ArgumentException("Provided parameter 'file' is refering a to a non .oosproj file");
            XmlDocument reader = new XmlDocument();
            reader.Load(file);
            XmlNode projectNode = reader.SelectSingleNode("/root/project");
            Project proj = new Project();
            foreach (XmlNode node in projectNode.ChildNodes)
            {
                     if (node.Name.Equals("title", StringComparison.OrdinalIgnoreCase))
                        proj.ProjectTitle = node.Value;
                else if (node.Name.Equals("author", StringComparison.OrdinalIgnoreCase))
                        proj.Author = node.Value;
                else if (node.Name.Equals("mainfile", StringComparison.OrdinalIgnoreCase))
                        proj.Mainfile = node.Value;
                else if (node.Name.Equals("outputfolder", StringComparison.OrdinalIgnoreCase))
                        proj.OutputFolder = node.Value;
            }

            return proj;
        }
    }
}
