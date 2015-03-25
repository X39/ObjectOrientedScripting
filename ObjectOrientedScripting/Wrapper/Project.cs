using System;
using System.IO;
using System.Xml;


namespace Wrapper
{
    public class Project
    {
        private string _projectPath;
        public string ProjectPath
        {
            set { this._projectPath = value; }
            get { return this._projectPath; }
        }
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
            set
            {
                if(value.Length > 2 && value[1] == ':' && (value[2] == '/' || value[2] == '\\'))
                    this._mainfile = value.Replace('/', '\\');
                else if (value.StartsWith("./") || value.StartsWith(".\\"))
                    this._mainfile = _projectPath + value.Substring(2).Replace('/', '\\');
                else
                    throw new ArgumentException("FilePath needs to be either full path (D:/) or relative (./)");
            }
            get { return this._mainfile; }
        }
        private string _outputFolder;
        public string OutputFolder
        {
            set
            {
                if (value.Length > 2 && value[1] == ':' && (value[2] == '/' || value[2] == '\\'))
                    this._outputFolder = value.Replace('/', '\\');
                else if (value.StartsWith("./") || value.StartsWith(".\\"))
                    this._outputFolder = _projectPath + value.Substring(2).Replace('/', '\\');
                else
                    throw new ArgumentException("FilePath needs to be either full path (D:/) or relative (./)");
            }
            get { return this._outputFolder; }
        }
        private string _buildfolder;
        public string Buildfolder
        {
            set
            {
                if (value.Length > 2 && value[1] == ':' && (value[2] == '/' || value[2] == '\\'))
                    this._buildfolder = value.Replace('/', '\\');
                else if (value.StartsWith("./") || value.StartsWith(".\\"))
                    this._buildfolder = _projectPath + value.Substring(2).Replace('/', '\\');
                else
                    throw new ArgumentException("FilePath needs to be either full path (D:/) or relative (./)");
            }
            get { return this._buildfolder; }
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
            proj._projectPath = file.Remove((file.Contains("\\") ? file.LastIndexOf('\\') : file.LastIndexOf('/')) + 1);
            foreach (XmlNode node in projectNode.ChildNodes)
            {
                     if (node.Name.Equals("title", StringComparison.OrdinalIgnoreCase))
                         proj.ProjectTitle = node.InnerText;
                else if (node.Name.Equals("author", StringComparison.OrdinalIgnoreCase))
                         proj.Author = node.InnerText;
                else if (node.Name.Equals("mainfile", StringComparison.OrdinalIgnoreCase))
                         proj.Mainfile = node.InnerText;
                else if (node.Name.Equals("outputfolder", StringComparison.OrdinalIgnoreCase))
                         proj.OutputFolder = node.InnerText;
                else if (node.Name.Equals("buildfolder", StringComparison.OrdinalIgnoreCase))
                         proj.Buildfolder = node.InnerText;
            }
            return proj;
        }
    }
}
