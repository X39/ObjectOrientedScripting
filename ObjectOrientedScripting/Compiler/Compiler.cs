using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wrapper;
using Compiler;
using Compiler.SqfConfigObjects;
using Compiler.OOS_LanguageObjects;
using System.IO;

namespace Wrapper
{
    public class Compiler : ICompiler
    {
        private class ContainerClass
        {
            public ContainerClass(object value, int purpose, pBaseLangObject sender)
            {
                this.Value = value;
                this.Purpose = purpose;
                this.Sender = sender;
            }
            public object Value { get; set; }
            public int Purpose { get; set; }
            public pBaseLangObject Sender { get; set; }
        }

        string configFileName;
        bool addFunctionsClass;
        bool outputFolderCleanup;
        List<PPDefine> flagDefines;
        public static readonly string endl = "\r\n";
        public static string thisVariableName = "___obj___";
        public string stdLibPath;
        private List<string> includedFiles;
        public Compiler()
        {
            configFileName = "config.cpp";
            stdLibPath = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            stdLibPath = stdLibPath.Substring(0, stdLibPath.LastIndexOf('\\')) + "\\stdLibrary\\";
            addFunctionsClass = true;
            outputFolderCleanup = true;
            flagDefines = new List<PPDefine>();
            SqfCall.readSupportInfoList();
            includedFiles = new List<string>();
        }
        public void setFlags(string[] strArr)
        {
            foreach (var s in strArr)
            {
                int count = s.IndexOf('=');
                if (count == -1)
                    count = s.Length;
                string switchstring = s.Substring(1, count - 1);
                switch (switchstring.ToUpper())
                {
                    case "CLFN":
                        if (count == -1)
                        {
                            Logger.Instance.log(Logger.LogLevel.ERROR, "Missing output file");
                            Logger.Instance.close();
                            throw new Exception("Missing output file");
                        }
                        configFileName = s.Substring(count + 1);
                        break;
                    case "NFNC":
                        addFunctionsClass = false;
                        break;
                    case "DEFINE":
                        flagDefines.Add(new PPDefine('#' + s.Substring(count + 1)));
                        break;
                    case "NOCLEANUP":
                        outputFolderCleanup = false;
                        break;
                    case "THISVAR":
                        thisVariableName = s.Substring(count + 1);
                        break;
                    case "STDLIBPATH":
                        stdLibPath = s.Substring(count + 1).Replace('/', '\\');
                        if (!stdLibPath.EndsWith("\\"))
                            stdLibPath += '\\';
                        break;
                    default:
                        Logger.Instance.log(Logger.LogLevel.WARNING, "Unknown flag '" + s + "' for compiler version '" + this.getVersion().ToString() + "'");
                        break;
                }
            }
        }
        public Version getVersion()
        {
            return new Version("0.5.0-ALPHA");
        }
        public void CheckSyntax(string filepath)
        {
            Scanner scanner = new Scanner(filepath);
            Parser parser = new Parser(scanner);
            parser.Parse();
        }
        #region Translating
        private void cleanupRecursive(string path)
        {
            foreach (var it in Directory.EnumerateDirectories(path))
            {
                cleanupRecursive(it);
                Directory.Delete(it);
            }
            foreach (var it in Directory.EnumerateFiles(path))
            {
                Logger.Instance.log(Logger.LogLevel.VERBOSE, "Deleting '" + it + "'");
                File.Delete(it);
            }
        }
        public void Translate(Project proj)
        {
            if(outputFolderCleanup)
            {
                Logger.Instance.log(Logger.LogLevel.VERBOSE, "Cleaning up output dir");
                cleanupRecursive(proj.OutputFolder);
            }
            //Read compiled file
            Scanner scanner = new Scanner(proj.Buildfolder + "_compile_.obj");
            Base baseObject = new Base();
            Parser parser = new Parser(scanner);
            parser.BaseObject = baseObject;
            parser.Parse();
            //OosContainer container;
            //parser.getBaseContainer(out container);
            int errCount = parser.errors.count + parser.BaseObject.finalize();
            if (errCount > 0)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, "Errors found (" + errCount + "), cannot continue with Translating!");
                return;
            }
            SqfConfigFile file = new SqfConfigFile(configFileName);
            iSqfConfig cfgClass = file;
            if (addFunctionsClass)
            {
                cfgClass = new SqfConfigClass("cfgFunctions");
                file.addChild(cfgClass);
            }
            if (!Directory.Exists(proj.OutputFolder))
            {
                Directory.CreateDirectory(proj.OutputFolder);
            }
            WriteOutTree(proj, parser.BaseObject, proj.OutputFolder, cfgClass, null);
            //Create config.cpp file
            file.writeOut(proj.OutputFolder);
        }
        private void updateTabcount(ref string s, ref int tabCount, int change)
        {
            tabCount += change;
            s = new string('\t', tabCount);
        }
        public void WriteOutTree(Project proj, pBaseLangObject container, string path, iSqfConfig configObj, StreamWriter writer, int tabCount = 0, object extraParam = null)
        {
        }
        #endregion
        #region Compiling
        public void Compile(Project proj)
        {
            Logger.Instance.log(Logger.LogLevel.WARNING, "Compile is not supported by this compiler version, thus its just a plain \"CheckSyntax\" ... im sorry :(");
            Scanner scanner = new Scanner(proj.Buildfolder + "_preprocess_.obj");
            Parser parser = new Parser(scanner);
            parser.Parse();
            //OosContainer container;
            //parser.getBaseContainer(out container);
            int errCount = parser.errors.count;
            if (errCount > 0)
                throw new Exception("Errors found (" + errCount + "), cannot continue with Compile!");
            var filePath = proj.Buildfolder + "_preprocess_.obj";
            var newPath = proj.Buildfolder + "_compile_.obj";
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    File.Copy(filePath, newPath, true);
                    break;
                }
                catch (IOException e)
                {
                    System.Threading.Thread.Sleep(500);
                    if (i == 2)
                        throw e;
                    continue;
                }
            }
        }
        #endregion
        #region PreProcessing
        public void Preprocess(Project proj)
        {
            //Make sure the build directory exists and create it if needed
            if (!Directory.Exists(proj.Buildfolder))
                Directory.CreateDirectory(proj.Buildfolder);
            //Prepare some stuff needed for preprocessing
            StreamWriter writer = null;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    writer = new StreamWriter(proj.Buildfolder + "_preprocess_.obj", false, Encoding.UTF8, 1024);
                    break;
                }
                catch (IOException e)
                {
                    Logger.Instance.log(Logger.LogLevel.WARNING, e.Message + " Trying again (" + (i + 1) + "/3)");
                    System.Threading.Thread.Sleep(500);
                    if (i == 2)
                        throw e;
                    continue;
                }
            }
            Dictionary<string, PPDefine> defines = new Dictionary<string, PPDefine>();
            foreach (var it in flagDefines)
                defines.Add(it.Name, it);
            List<preprocessFile_IfDefModes> ifdefs = new List<preprocessFile_IfDefModes>();
            //Start actual preprocessing
            preprocessFile(ifdefs, defines, proj, proj.Mainfile, writer);

            //Close the file writer
            writer.Flush();
            writer.Close();
        }
        private enum preprocessFile_IfDefModes
        {
            TRUE = 0,
            FALSE,
            IGNORE
        }
        private bool preprocessFile(List<preprocessFile_IfDefModes> ifdefs, Dictionary<string, PPDefine> defines, Project proj, string filePath, StreamWriter writer)
        {
            //Open given file
            StreamReader reader = new StreamReader(filePath);

            //Prepare some variables needed for the entire processing periode in this function
            string s;
            uint filelinenumber = 0;
            while ((s = reader.ReadLine()) != null)
            {
                filelinenumber++;
                //skip empty lines
                if (string.IsNullOrWhiteSpace(s))
                    continue;
                //Remove left & right whitespaces and tabs from current string
                string sTrimmed = s.TrimStart();
                string leading = s.Substring(0, s.Length - sTrimmed.Length);
                s = sTrimmed;
                if (s[0] != '#')
                {//Current line is no define, thus handle it normally (find & replace)
                    //Make sure we are not inside of an ifdef/ifndef that disallows further processing of following lines
                    int i = ifdefs.Count - 1;
                    if (i >= 0 && ifdefs[i] != preprocessFile_IfDefModes.TRUE)
                        continue;
                    try
                    {
                        //Let every define check if it is inside of current line
                        foreach (PPDefine def in defines.Values)
                            s = def.replace(s);
                    }
                    catch (Exception ex)
                    {
                        //Catch possible exceptions from define parsing
                        Logger.Instance.log(Logger.LogLevel.ERROR, "Experienced some error while parsing existing defines.");
                        Logger.Instance.log(Logger.LogLevel.CONTINUE, ex.Message + ". file: " + filePath + ". linenumber: " + filelinenumber);
                        reader.Close();
                        return false;
                    }
                    writer.WriteLine(leading + s);
                    continue;
                }
                //We DO have a define here
                //get end of the define name
                int spaceIndex = s.IndexOf(' ');
                if (spaceIndex < 0)
                    spaceIndex = s.Length;
                //set some required variables for the switch
                int index = -1;
                int index2 = -1;
                //get text AFTER the define
                string afterDefine = s.Substring(spaceIndex).TrimStart();

                //Check which define was used
                switch (s.Substring(0, spaceIndex))
                {
                    default:
                        throw new Exception("Encountered unknown define '" + s.Substring(0, spaceIndex) + "'");
                    case "#include":
                        //We are supposed to include a new file at this spot so lets do it
                        //Beautify the filepath so we can work with it
                        afterDefine.Trim();
                        string newFile;
                        if (afterDefine.StartsWith("<") && afterDefine.EndsWith(">"))
                        {
                            newFile = stdLibPath + afterDefine.Trim(new char[] { '<', '>' });
                        }
                        else
                        {
                            newFile = proj.ProjectPath + afterDefine.Trim(new char[] { '"', '\'', ' ' });
                        }
                        //make sure we have no self reference here
                        if (newFile.Equals(filePath, StringComparison.OrdinalIgnoreCase))
                        {
                            //Ohhh no ... some problem in OSI layer 8
                            reader.Close();
                            throw new Exception("Include contains self reference. file: " + filePath + ". linenumber: " + filelinenumber);
                        }
                        //Check if file was already included
                        if (includedFiles.Contains(newFile))
                            break;
                        //process the file before continuing with this
                        try
                        {
                            if (!preprocessFile(ifdefs, defines, proj, newFile, writer))
                            {
                                //A sub file encountered an error, so stop here to prevent useles waste of ressources
                                reader.Close();
                                return false;
                            }
                            includedFiles.Add(newFile);
                        }
                        catch (Exception e)
                        {
                            throw new Exception(e.Message + ", from " + filePath);
                        }
                        break;
                    case "#define":
                        //The user wants to define something here
                        while (s.EndsWith("\\"))
                        {
                            afterDefine += reader.ReadLine();
                            filelinenumber++;
                        }
                        //Get the two possible characters index that can be encountered after a define
                        index = afterDefine.IndexOf(' ');
                        index2 = afterDefine.IndexOf('(');
                        //check which one is found first
                        if (index < 0 || (index2 < index && index2 >= 0))
                            index = afterDefine.IndexOf('(');
                        //check that we really got a define with a value here, if not just take the entire length as no value is needed and only value provided
                        if (index < 0)
                            index = afterDefine.Length;
                        if (defines.ContainsKey(afterDefine.Substring(0, index)))
                        {
                            //Redefining something is not allowed, so throw an error here
                            reader.Close();
                            throw new Exception("Redefining a define is not allowed! Use #undefine to undef something. file: " + filePath + ". linenumber: " + filelinenumber);
                        }
                        //FINALLY add the define
                        defines.Add(afterDefine.Substring(0, index), new PPDefine(afterDefine));
                        break;
                    case "#undefine":
                        //just remove straigth
                        defines.Remove(s.Substring(spaceIndex).Trim());
                        break;
                    case "#ifdef":
                        //do required stuff for define ifs
                        if (defines.ContainsKey(afterDefine))
                            ifdefs.Add(ifdefs.Count == 0 || ifdefs[ifdefs.Count - 1] == preprocessFile_IfDefModes.TRUE ? preprocessFile_IfDefModes.TRUE : preprocessFile_IfDefModes.IGNORE);
                        else
                            ifdefs.Add(ifdefs.Count == 0 || ifdefs[ifdefs.Count - 1] == preprocessFile_IfDefModes.TRUE ? preprocessFile_IfDefModes.FALSE : preprocessFile_IfDefModes.IGNORE);
                        break;
                    case "#ifndef":
                        //do required stuff for define ifs
                        if (defines.ContainsKey(afterDefine))
                            ifdefs.Add(ifdefs.Count == 0 || ifdefs[ifdefs.Count - 1] == preprocessFile_IfDefModes.TRUE ? preprocessFile_IfDefModes.FALSE : preprocessFile_IfDefModes.IGNORE);
                        else
                            ifdefs.Add(ifdefs.Count == 0 || ifdefs[ifdefs.Count - 1] == preprocessFile_IfDefModes.TRUE ? preprocessFile_IfDefModes.TRUE : preprocessFile_IfDefModes.IGNORE);
                        break;
                    case "#else":
                        //do required stuff for define ifs
                        index = ifdefs.Count - 1;
                        if (index < 0)
                        {
                            reader.Close();
                            throw new Exception("unexpected #else. file: " + filePath + ". linenumber: " + filelinenumber);
                        }
                        //swap the value of currents if scope to the correct value
                        ifdefs[index] = (ifdefs[index] == preprocessFile_IfDefModes.TRUE ? preprocessFile_IfDefModes.FALSE : (ifdefs[index] == preprocessFile_IfDefModes.FALSE ? preprocessFile_IfDefModes.TRUE : preprocessFile_IfDefModes.IGNORE));
                        break;
                    case "#endif":
                        //do required stuff for define ifs
                        index = ifdefs.Count - 1;
                        if (index < 0)
                        {
                            reader.Close();
                            throw new Exception("unexpected #endif. file: " + filePath + ". linenumber: " + filelinenumber);
                        }
                        //remove current if scope
                        ifdefs.RemoveAt(index);
                        break;
                }
            }
            reader.Close();
            return true;
        }
        #endregion
    }
}
