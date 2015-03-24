using System;
using System.IO;


namespace ObjectOrientedSQF
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

        private Project()
	    {

	    }
        static Project openProject(string file)
        {
            if (file == null)
                throw new ArgumentException("Provided parameter 'file' is null");
            if (!File.Exists(file))
                throw new ArgumentException("Provided parameter 'file' is refering a not existing file");
            if(!file.EndsWith(".oos.proj"))
                throw new ArgumentException("Provided parameter 'file' is refering a to a non .oos.proj file");
            
            Project proj = new Project();

        }
    }
}
