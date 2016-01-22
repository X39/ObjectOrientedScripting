using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace Wrapper
{
    public class Project
    {
        public class Ressource
        {
            public string InPath { get; set; }
            public string OutPath { get; set; }
            public Ressource(string inPath, string outPath, Project proj = null)
            {
                if (inPath.StartsWith("./") || inPath.StartsWith(".\\"))
                {
                    if (proj == null)
                        throw new Exception("Dot path found but no Project argument provided");
                    this.InPath = proj.ProjectPath + inPath.Substring(2).Replace('/', '\\');
                }
                else
                {
                    this.InPath = inPath;
                }
                if (outPath.StartsWith("./") || outPath.StartsWith(".\\"))
                {
                    if (proj == null)
                        throw new Exception("Dot path found but no Project argument provided");
                    this.OutPath = proj.ProjectPath + outPath.Substring(2).Replace('/', '\\');
                }
                else
                {
                    this.OutPath = outPath;
                }
            }
            public Ressource(XmlNode node)
            {
                this.InPath = "";
                this.OutPath = "";

                foreach (XmlAttribute att in node.Attributes)
                {
                    switch (att.Name.ToLower())
                    {
                        case "in":
                            this.InPath = att.Value;
                            break;
                        case "out":
                            this.OutPath = att.Value;
                            break;
                    }
                }

                if (string.IsNullOrEmpty(this.InPath) || string.IsNullOrEmpty(this.OutPath))
                    throw new Exception();
            }
            public override string ToString()
            {
                return this.OutPath;
            }
        }
        public class Define
        {
            public string Name { get; set; }
            public string ReplaceText { get; set; }
            public List<string> ArgList { get; set; }
            public Define(string name, string replaceText = "", List<string> argList = null)
            {
                this.Name = name;
                this.ReplaceText = replaceText;
                if (argList == null)
                    this.ArgList = new List<string>();
                else
                    this.ArgList = argList;
            }
            public Define(XmlNode node)
            {
                this.Name = "";
                this.ReplaceText = "";
                this.ArgList = new List<string>();

                foreach (XmlAttribute att in node.Attributes)
                {
                    switch (att.Name.ToLower())
                    {
                        case "name":
                            this.Name = att.Value;
                            break;
                    }
                }
                foreach (XmlNode subNode in node.ChildNodes)
                {
                    switch (subNode.Name.ToLower())
                    {
                        case "replace":
                            this.ReplaceText = subNode.InnerText.Replace('\r', ' ').Replace('\n', ' ').Trim();
                            break;
                        case "argument":
                            foreach(XmlAttribute att in subNode.Attributes)
                            {
                                switch(att.Name.ToLower())
                                {
                                    case "name":
                                        this.ArgList.Add(att.Value);
                                        break;
                                }
                            }
                            break;
                    }
                }

                if (this.Name == "")
                    throw new Exception();
            }
            public override string ToString()
            {
                if (this.ArgList.Count == 0)
                    return this.Name;
                else
                {
                    string s = "";
                    foreach(var it in this.ArgList)
                    {
                        s += (string.IsNullOrEmpty(s) ? "" : ", ") + it;
                    }
                    return this.Name + '(' + s + ')';
                }
            }
            public string toReal()
            {
                string s = "";
                foreach (var it in this.ArgList)
                {
                    s += (string.IsNullOrEmpty(s) ? "" : ", ") + it;
                }
                s = "#define " + this.Name + (s == "" ? "" : '(' + s + ')');
                s += ' ' + ReplaceText;

                return s;
            }
            public static Define fromReal(string input, Define existing = default(Define))
            {
                if (input.StartsWith("#define "))
                    input = input.Substring("#define ".Length);
                else
                    throw new Exception("Invalid 'Real' string input.\nMissing '#define' in the begining");


                input = input.Replace('\r', ' ').Replace('\n', ' ').Trim();
                //Get the two possible characters index that can be encountered after a define
                int index = input.IndexOf(' ');
                int index2 = input.IndexOf('(');
                //check which one is found first
                if (index < 0 || (index2 < index && index2 >= 0))
                    index = input.IndexOf('(');
                //check that we really got a define with a value here, if not just take the entire length as no value is needed and only value provided
                if (index < 0)
                    index = input.Length;


                int spaceIndex = input.IndexOf(' ');
                if (spaceIndex == -1)
                    //No Space found thus we just have a define here without anything else
                    if (existing == default(Define))
                    {
                        return new Define(input);
                    }
                    else
                    {
                        existing.ReplaceText = "";
                        existing.ArgList.Clear();
                        existing.Name = input;
                        return existing;
                    }
                int bracketsIndex = input.IndexOf('(');
                if (spaceIndex < bracketsIndex || bracketsIndex == -1)
                    //first bracket was found after first space OR is not existing thus we have a simple define with a replace value here
                    if (existing == default(Define))
                    {
                        return new Define(input.Remove(spaceIndex), input.Remove(0, spaceIndex + 1));
                    }
                    else
                    {
                        existing.ReplaceText = input.Remove(0, spaceIndex + 1);
                        existing.ArgList.Clear();
                        existing.Name = input.Remove(spaceIndex);
                        return existing;
                    }
                //we got a define with arguments here

                string argumentsString = input.Remove(0, bracketsIndex + 1);
                argumentsString = argumentsString.Remove(argumentsString.IndexOf(')'));
                List<string> arguments = new List<string>(argumentsString.Split(','));
                for (int i = 0; i < arguments.Count; i++)
                {
                    arguments[i] = arguments[i].Trim();
                }
                string tmp = input.Remove(bracketsIndex);
                bracketsIndex = input.IndexOf(") ");
                if (bracketsIndex == -1)
                    throw new Exception("Missing character to close argument list ')' or no value for argument define");

                if (existing == default(Define))
                {
                    return new Define(tmp, input.Remove(0, bracketsIndex + 2), arguments);
                }
                else
                {
                    existing.ReplaceText = input.Remove(0, bracketsIndex + 2);
                    existing.ArgList = arguments;
                    existing.Name = tmp;
                    return existing;
                }
            }
        }
        public string ProjectPath { set;  get; }
        public string ProjectTitle { set; get; }
        public string Author { set; get; }
        private string _mainfile;
        public string Mainfile
        {
            set
            {
                if (value.Length > 2 && value[1] == ':' && (value[2] == '/' || value[2] == '\\'))
                    this._mainfile = value.Replace('/', '\\');
                else if (value.StartsWith("./") || value.StartsWith(".\\"))
                    this._mainfile = this.ProjectPath + value.Substring(2).Replace('/', '\\');
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
                    this._outputFolder = this.ProjectPath + value.Substring(2).Replace('/', '\\');
                else
                    throw new ArgumentException("FilePath needs to be either full path (D:/) or relative (./)");
                if (!this._outputFolder.EndsWith("\\"))
                {
                    this._outputFolder += '\\';
                }
            }
            get { return this._outputFolder; }
        }
        private string _srcFolder;
        public string SrcFolder
        {
            set
            {
                if (value.Length > 2 && value[1] == ':' && (value[2] == '/' || value[2] == '\\'))
                    this._srcFolder = value.Replace('/', '\\');
                else if (value.StartsWith("./") || value.StartsWith(".\\"))
                    this._srcFolder = this.ProjectPath + value.Substring(2).Replace('/', '\\');
                else
                    throw new ArgumentException("FilePath needs to be either full path (D:/) or relative (./)");
                if(!this._srcFolder.EndsWith("\\"))
                {
                    this._srcFolder += '\\';
                }
            }
            get { return this._srcFolder; }
        }
        private string _buildfolder;
        public string Buildfolder
        {
            set
            {
                if (value.Length > 2 && value[1] == ':' && (value[2] == '/' || value[2] == '\\'))
                    this._buildfolder = value.Replace('/', '\\');
                else if (value.StartsWith("./") || value.StartsWith(".\\"))
                    this._buildfolder = this.ProjectPath + value.Substring(2).Replace('/', '\\');
                else
                    throw new ArgumentException("FilePath needs to be either full path (D:/) or relative (./)");
                if (!this._buildfolder.EndsWith("\\"))
                {
                    this._buildfolder += '\\';
                }
            }
            get { return this._buildfolder; }
        }
        public Version CompilerVersion { set; get; }
        public List<Define> Defines { get; set; }
        public List<Ressource> Ressources { get; set; }

        private Project()
        {
            this.CompilerVersion = null;
            this.Defines = new List<Define>();
            this.Ressources = new List<Ressource>();
        }
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
            XmlNode compilerNode = reader.SelectSingleNode("/root/compiler");
            XmlNode ressourcesNode = reader.SelectSingleNode("/root/ressources");
            Project proj = new Project();
            proj.ProjectPath = file.Remove((file.Contains("\\") ? file.LastIndexOf('\\') : file.LastIndexOf('/')) + 1);
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
                else if (node.Name.Equals("srcfolder", StringComparison.OrdinalIgnoreCase))
                    proj.SrcFolder = node.InnerText;
            }
            foreach (XmlAttribute att in compilerNode.Attributes)
            {
                if (att.Name.Equals("version", StringComparison.OrdinalIgnoreCase))
                    proj.CompilerVersion = new Version(att.Value);
            }
            foreach (XmlNode node in compilerNode.ChildNodes)
            {
                switch (node.Name.ToLower())
                {
                    case "define":
                        proj.Defines.Add(new Define(node));
                        break;
                }
            }
            if (ressourcesNode != null)
            {
                foreach (XmlNode node in ressourcesNode.ChildNodes)
                {
                    switch (node.Name.ToLower())
                    {
                        case "res":
                            proj.Ressources.Add(new Ressource(node));
                            break;
                    }
                }
            }

            //Upgrade from < 0.7.0
            if (string.IsNullOrEmpty(proj.SrcFolder))
            {
                proj.SrcFolder = proj.ProjectPath;
            }
            return proj;
        }

        public void writeToFile(string filepath)
        {

            XDocument doc = new XDocument(new XElement("root"));
            XElement root = doc.Root;
            {//<project>
                var projectNode = new XElement("project");
                root.Add(projectNode);
                XElement node;
                string s;


                //<title>this.ProjectTitle</title>
                node = new XElement("title");
                node.Value = this.ProjectTitle;
                projectNode.Add(node);


                //<author>this.Author</author>
                node = new XElement("author");
                node.Value = this.Author;
                projectNode.Add(node);


                //<mainfile>this.Mainfile</mainfile>
                node = new XElement("mainfile");
                s = this.Mainfile;
                if (s.StartsWith(this.ProjectPath))
                    s = ".\\" + s.Substring(this.ProjectPath.Length);
                node.Value = s;
                projectNode.Add(node);


                //<outputfolder>this.OutputFolder</outputfolder>
                node = new XElement("outputfolder");
                s = this.OutputFolder;
                if (s.StartsWith(this.ProjectPath))
                    s = ".\\" + s.Substring(this.ProjectPath.Length);
                node.Value = s;
                projectNode.Add(node);


                //<buildfolder>this.Buildfolder</buildfolder>
                node = new XElement("buildfolder");
                s = this.Buildfolder;
                if (s.StartsWith(this.ProjectPath))
                    s = ".\\" + s.Substring(this.ProjectPath.Length);
                node.Value = s;
                projectNode.Add(node);


                //<srcfolder>this.SrcFolder</srcfolder>
                node = new XElement("srcfolder");
                s = this.SrcFolder;
                if (s.StartsWith(this.ProjectPath))
                    s = ".\\" + s.Substring(this.ProjectPath.Length);
                node.Value = s;
                projectNode.Add(node);


            }//</project>
            {//<compiler version="VERSION">
                XElement node;
                var compilerNode = new XElement("compiler");
                root.Add(compilerNode);
                compilerNode.Add(new XAttribute("version", this.CompilerVersion.ToString()));

                foreach (var it in this.Defines)
                {
                    //<define name="DEFINENAME">
                    var defineNode = new XElement("define");
                    compilerNode.Add(defineNode);
                    defineNode.Add(new XAttribute("name", it.Name));

                    //<replace>TEXT</replace>
                    node = new XElement("replace");
                    node.Value = it.ReplaceText;
                    defineNode.Add(node);

                    foreach (var arg in it.ArgList)
                    {
                        //<argument name="NAME" />
                        node = new XElement("argument");
                        defineNode.Add(node);
                        node.Add(new XAttribute("name", it.Name));
                    }
                    //</define>
                }


            }///<compiler>
            {//<ressources>
                string s;
                var ressourcesNode = new XElement("ressources");
                root.Add(ressourcesNode);

                foreach (var it in this.Ressources)
                {
                    //<res in="PATH" out="PATH" />
                    var resNode = new XElement("res");
                    ressourcesNode.Add(resNode);
                    s = it.InPath;
                    if (s.StartsWith(this.ProjectPath))
                        s = ".\\" + s.Substring(this.ProjectPath.Length);
                    else if (s.Length > 2 && s[1] != ':' && !s.StartsWith(".\\"))
                        s = ".\\" + s;
                    resNode.Add(new XAttribute("in", s));
                    s = it.OutPath;
                    if (s.StartsWith(this.ProjectPath))
                        s = ".\\" + s.Substring(this.ProjectPath.Length);
                    else if (s.Length > 2 && s[1] != ':' && !s.StartsWith(".\\"))
                        s = ".\\" + s;
                    resNode.Add(new XAttribute("out", s));
                }


            }///<ressources>

            doc.Save(filepath);
        }
    }
}
