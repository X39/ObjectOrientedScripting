﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wrapper;
using Compiler;
using Compiler.SqfConfigObjects;
using Compiler.OOS_LanguageObjects;
using Compiler.OOS_LanguageObjects.HelperClasses;
using System.IO;

namespace Wrapper
{
    public class Compiler : ICompiler_1
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

        private string configFileName;
        private bool addFunctionsClass;
        private bool outputFolderCleanup;
        private int printOutMode;
        private List<PPDefine> flagDefines;
        public static readonly string endl = "\r\n";
        public static string thisVariableName = "___obj___";
        public static string stdLibPath;
        private List<string> includedFiles;

        public static Project ProjectFile { get; internal set; }
        public struct ScopeNames
        {
            public static readonly string function = "_FNCSCOPE_";
            public static readonly string loop = "_LOOPSCOPE_";
            public static readonly string oosCase = "_CASE_";
        }
        public Compiler()
        {
            configFileName = "config.cpp";
            stdLibPath = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            stdLibPath = stdLibPath.Substring(0, stdLibPath.LastIndexOf('\\')) + "\\stdLibrary\\";
            addFunctionsClass = true;
            outputFolderCleanup = true;
            printOutMode = 0;
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
                    case "PRINTMODE":
                        switch(s.Substring(count + 1))
                        {
                            case "NONE": case "0":
                                printOutMode = 0;
                                break;
                            case "NEEDED": case "1":
                                printOutMode = 1;
                                break;
                            case "PARTIAL": case "2":
                                printOutMode = 2;
                                break;
                            case "ALL": case "3":
                                printOutMode = 3;
                                break;
                            default:
                                throw new Exception("Unknown PRINTMODE, valid ones are: NONE|0  NEEDED|1  PARTIAL|2  ALL|3");
                        }
                        break;
                    default:
                        Logger.Instance.log(Logger.LogLevel.WARNING, "Unknown flag '" + s + "' for compiler version '" + this.getVersion().ToString() + "'");
                        break;
                }
            }
        }
        public Version getVersion()
        {
            return new Version("0.6.1-ALPHA");
        }
        public void CheckSyntax(string filepath)
        {
            Scanner scanner = new Scanner(filepath);
            Parser parser = new Parser(scanner);
            parser.Parse();
        }
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
        private void updateTabcount(ref string s, ref int tabCount, int change)
        {
            tabCount += change;
            s = new string('\t', tabCount);
        }
        public void Compile(Project proj)
        {
            ProjectFile = proj;
            //Make sure the build directory exists and create it if needed
            if (!Directory.Exists(proj.Buildfolder))
                Directory.CreateDirectory(proj.Buildfolder);
            if (!Directory.Exists(proj.OutputFolder))
                Directory.CreateDirectory(proj.OutputFolder);

            //Prepare some stuff needed for preprocessing
            Dictionary<string, PPDefine> defines = new Dictionary<string, PPDefine>();
            foreach (var it in flagDefines)
                defines.Add(it.Name, it);
            List<preprocessFile_IfDefModes> ifdefs = new List<preprocessFile_IfDefModes>();
            List<PostProcessFile> ppFiles = new List<PostProcessFile>();
            //do the preprocessing
            preprocessFile(ifdefs, defines, proj, proj.Mainfile, proj.Mainfile.Substring(proj.Mainfile.LastIndexOf('\\') + 1), ppFiles);
            var ppMainFile = ppFiles[0];

            if (outputFolderCleanup)
            {
                Logger.Instance.log(Logger.LogLevel.VERBOSE, "Cleaning up output dir");
                cleanupRecursive(proj.OutputFolder);
            }

            int errCount = 0;
            //Check the syntax of all files in ppFiles
            foreach(var it in ppFiles)
            {
                //if (!noPrintOut)
                //{
                //    var stream = File.Create(proj.Buildfolder + it.Name + ".obj");
                //    it.resetPosition();
                //    it.FileStream.WriteTo(stream);
                //    stream.Flush();
                //    stream.Close();
                //}
                Scanner scanner = new Scanner(it.FileStream);
                Base baseObject = new Base();
                Parser p = new Parser(scanner);
                Parser.UsedFiles = new List<string>();
                p.BaseObject = baseObject;
                p.Parse();
                if (p.errors.count > 0)
                {
                    errCount += p.errors.count;
                    Logger.Instance.log(Logger.LogLevel.ERROR, "In file '" + it.Name + "'");
                }
                if (printOutMode > 0)
                {
                    if (printOutMode == 1 && it == ppMainFile)
                    {
                        var stream = File.Create(proj.Buildfolder + it.Name + ".objF");
                        it.resetPosition();
                        it.FullFileStream.WriteTo(stream);
                        stream.Flush();
                        stream.Close();
                    }
                    if (printOutMode >= 2)
                    {
                        var stream = File.Create(proj.Buildfolder + it.Name + ".obj");
                        it.resetPosition();
                        it.FileStream.WriteTo(stream);
                        stream.Flush();
                        stream.Close();
                        if (printOutMode >= 3)
                        {
                            var stream2 = File.Create(proj.Buildfolder + it.Name + ".objF");
                            it.resetPosition();
                            it.FullFileStream.WriteTo(stream2);
                            stream2.Flush();
                            stream2.Close();
                        }
                        else if(it == ppMainFile)
                        {
                            var stream2 = File.Create(proj.Buildfolder + it.Name + ".objF");
                            it.resetPosition();
                            it.FullFileStream.WriteTo(stream2);
                            stream2.Flush();
                            stream2.Close();
                        }
                    }
                }
                it.resetPosition();
            }
            if(errCount > 0)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, "Errors found (" + errCount + "), cannot continue with Translating!");
                return;
            }


            //process the actual file


            Base oosTreeBase = new Base();
            NamespaceResolver.BaseClass = oosTreeBase;
            Parser parser = new Parser(new Scanner(ppMainFile.FullFileStream));
            Parser.UsedFiles = new List<string>();
            parser.BaseObject = oosTreeBase;
            parser.Parse();
            errCount = parser.errors.count + parser.BaseObject.finalize();
            if (errCount > 0)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, "Errors found (" + errCount + "), cannot continue with Comnpiling!");
                return;
            }
            SqfConfigFile configFile = new SqfConfigFile(configFileName);
            oosTreeBase.writeOut(null, configFile);
            configFile.writeOut(proj.OutputFolder);
        }
        private enum preprocessFile_IfDefModes
        {
            TRUE = 0,
            FALSE,
            IGNORE
        }
        private bool preprocessFile(List<preprocessFile_IfDefModes> ifdefs, Dictionary<string, PPDefine> defines, Project proj, string filePath, string name, List<PostProcessFile> ppFiles)
        {
            //Open given file
            StreamReader reader = new StreamReader(filePath);
            PostProcessFile ppFile = new PostProcessFile(filePath, name);
            ppFiles.Add(ppFile);
            StreamWriter writer = new StreamWriter(ppFile.FileStream);

            //Prepare some variables needed for the entire processing periode in this function
            string s;
            uint filelinenumber = 0;
            while ((s = reader.ReadLine()) != null)
            {
                filelinenumber++;
                //skip empty lines
                if (string.IsNullOrWhiteSpace(s))
                {
                    writer.WriteLine();
                    continue;
                }
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

                writer.WriteLine();

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
                            writer.Close();
                            throw new Exception("Include contains self reference. file: " + filePath + ". linenumber: " + filelinenumber);
                        }
                        //Check if file was already included
                        if (includedFiles.Contains(newFile))
                            break;
                        //process the file before continuing with this
                        try
                        {
                            if (!preprocessFile(ifdefs, defines, proj, newFile, afterDefine.Trim(new char[] { '<', '>', '"', '\'', ' ' }), ppFiles))
                            {
                                //A sub file encountered an error, so stop here to prevent useles waste of ressources
                                reader.Close();
                                writer.Close();
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
                            writer.WriteLine();
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
                            writer.Close();
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
                            writer.Close();
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
                            writer.Close();
                            throw new Exception("unexpected #endif. file: " + filePath + ". linenumber: " + filelinenumber);
                        }
                        //remove current if scope
                        ifdefs.RemoveAt(index);
                        break;
                }
            }
            reader.Close();
            writer.Flush();
            ppFile.resetPosition();
            return true;
        }
    }
}