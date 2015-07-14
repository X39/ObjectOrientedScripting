using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wrapper;
using Compiler;
using Compiler.OOS_LanguageObjects;
using System.IO;

namespace Wrapper
{
    public class Compiler : ICompiler
    {
        public Version getVersion()
        {
            return new Version("0.1.0-a");
        }
        #region Translating
        public void Translate(Project proj)
        {
            //Read compiled file
            StreamReader reader = new StreamReader(proj.Buildfolder + "_compile_.obj");
            reader.Close();

            //Write namespace tree to disk
            //TODO: write namespace tree to disk
        }
        #endregion
        #region Compiling
        public void Compile(Project proj)
        {
            /*
            //Make sure the build directory exists and create it if needed
            if (!Directory.Exists(proj.Buildfolder))
                Directory.CreateDirectory(proj.Buildfolder);
            //Check if result file is existing, create it if it is not
            if (!File.Exists(proj.Buildfolder + "_compile_.obj"))
                File.Create(proj.Buildfolder + "_compile_.obj");
             */
            File.Move(proj.Buildfolder + "_preprocess_.obj", proj.Buildfolder + "_compile_.obj");
            Logger.Instance.log(Logger.LogLevel.WARNING, "Compile is not supported by this compiler version");
        }
        #endregion
        #region PreProcessing
        public void Preprocess(Project proj)
        {
            //Make sure the build directory exists and create it if needed
            if (!Directory.Exists(proj.Buildfolder))
                Directory.CreateDirectory(proj.Buildfolder);
            //Check if result file is existing, create it if it is not
            if (!File.Exists(proj.Buildfolder + "_preprocess_.obj"))
                File.Create(proj.Buildfolder + "_preprocess_.obj");

            //Prepare some stuff needed for preprocessing
            StreamWriter writer = new StreamWriter(proj.Buildfolder + "_preprocess_.obj", false, Encoding.Unicode, 1024);
            Dictionary<string, PPDefine> defines = new Dictionary<string, PPDefine>();
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
                s = s.Trim();
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
                    catch(Exception ex)
                    {
                        //Catch possible exceptions from define parsing
                        Logger.Instance.log(Logger.LogLevel.ERROR, "Experienced some error while parsing existing defines.");
                        Logger.Instance.log(Logger.LogLevel.CONTINUE, ex.Message + ". file: " + filePath + ". linenumber: " + filelinenumber);
                        reader.Close();
                        return false;
                    }
                    writer.WriteLine(s);
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
                        string newFile = proj.ProjectPath + afterDefine.Trim(new char[] { '"', '\'', ' ' });

                        //make sure we have no self reference here
                        if (newFile.Equals(filePath, StringComparison.OrdinalIgnoreCase))
                        {
                            //Ohhh no ... some problem in OSI layer 8
                            reader.Close();
                            throw new Exception("Include contains self reference. file: " + filePath + ". linenumber: " + filelinenumber);
                        }
                        //process the file before continuing with this
                        if (!preprocessFile(ifdefs, defines, proj, newFile, writer))
                        {
                            //A sub file encountered an error, so stop here to prevent useles waste of ressources
                            reader.Close();
                            return false;
                        }
                        break;
                    case "#define":
                        //The user wants to define something here
                        while(s.EndsWith("\\"))
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
                        if(index < 0)
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
