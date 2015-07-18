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
        public Version getVersion()
        {
            return new Version("0.1.0-a");
        }
        #region Translating
        public void Translate(Project proj)
        {
            //Read compiled file

            Scanner scanner = new Scanner(proj.Buildfolder + "_compile_.obj");
            Parser parser = new Parser(scanner);
            parser.Parse();
            OosContainer container;
            parser.getBaseContainer(out container);
            SqfConfigFile file = new SqfConfigFile("config.cpp");
            SqfConfigClass cfgClass = new SqfConfigClass("cfgFunctions");
            file.addChild(cfgClass);
            WriteOutTree(container, proj.OutputFolder, cfgClass, null);
        }
        public void WriteOutTree(BaseLangObject container, string path, iSqfConfig configObj, StreamWriter writer)
        {
            string curPath = path;
            #region OosNamespace
            if (container is OosNamespace)
            {
                OosNamespace obj = (OosNamespace)container;
                if (curPath.EndsWith("\\"))
                    curPath += obj.Name;
                else
                    curPath += '\\' + obj.Name;
                Directory.CreateDirectory(curPath);
                SqfConfigClass cfgClass = null;
                SqfConfigClass cfgClass_GenericFunctions = null;
                foreach (BaseLangObject container2 in obj.Children)
                {
                    if (!(container2 is OosNamespace))
                    {
                        if (cfgClass == null)
                        {
                            cfgClass = new SqfConfigClass(obj.getNormalizedName());
                            configObj.addChild(cfgClass);
                        }
                        if (container2 is OosGlobalFunction)
                        {
                            if (cfgClass_GenericFunctions == null)
                            {
                                cfgClass_GenericFunctions = new SqfConfigClass("generic");
                                cfgClass.addChild(cfgClass_GenericFunctions);
                            }
                            WriteOutTree(container2, curPath, cfgClass_GenericFunctions, writer);
                        }
                        else if (container2 is OosGlobalVariable)
                        {
                            continue; //Will be added in last step
                        }
                        else if (container2 is OosClass)
                        {
                            WriteOutTree(container2, curPath, cfgClass, writer);
                        }
                        else
                        {
                            throw new Exception("Non-Registered exception, if you ever experience this pls create a bug. Compiler.WriteOutTree");
                        }
                    }
                    else
                    {
                        WriteOutTree(container2, curPath, configObj, writer);
                    }
                }
            }
            #endregion
            #region OosClass
            else if (container is OosClass)
            {
                OosClass obj = (OosClass)container;
                if (curPath.EndsWith("\\"))
                    curPath += obj.Name;
                else
                    curPath += '\\' + obj.Name;
                Directory.CreateDirectory(curPath);
                SqfConfigClass cfgClass = new SqfConfigClass(obj.Name);
                configObj.addChild(cfgClass);
                BaseFunctionObject constructor = null;
                List<OosClassFunction> classFunctions = new List<OosClassFunction>();
                List<OosClassVariable> classVariables = new List<OosClassVariable>();
                foreach (BaseLangObject container2 in obj.Children)
                {
                    if (container2 is OosClass)
                    {
                        WriteOutTree(container2, curPath, configObj, writer);
                    }
                    else if (container2 is OosGlobalFunction)
                    {
                        WriteOutTree(container2, curPath, cfgClass, writer);
                    }
                    else if (container2 is OosClassFunction)
                    {
                        if (((BaseFunctionObject)container2).Name == "constructor")
                            if (constructor != null)
                                throw new Exception("Non-Registered exception, if you ever experience this pls create a bug. Compiler.WriteOutTree");
                            else
                                constructor = (BaseFunctionObject)container2;
                        else
                            classFunctions.Add((OosClassFunction)container2);
                    }
                    else if (container2 is OosClassVariable)
                    {
                        classVariables.Add((OosClassVariable)container2);
                    }
                    else if (container2 is OosGlobalVariable)
                    {
                        continue; //Will be added in last step
                    }
                    else
                    {
                        throw new Exception("Non-Registered exception, if you ever experience this pls create a bug. Compiler.WriteOutTree");
                    }
                }
                //Handle constructor manually (as it has obviously more to do then the generic DoSomething functions ... or do you want a non-functional object do you?)
                string constructorPath = curPath + '\\' + "___constructor___.sqf";
                StreamWriter newWriter = new StreamWriter(constructorPath);
                cfgClass.addChild(new SqfConfigField("___constructor___", "file = \"" + constructorPath + "\"; preInit = 0; postInit = 0; recompile = 0; ext = \".sqf\";"));
                newWriter.WriteLine("private \"_obj\";");
                newWriter.Write("_obj = [\n\t[nil");
                foreach (OosClassVariable container2 in classVariables)
                    newWriter.Write(",\"" + container2.Name + '"');
                foreach (OosClassFunction container2 in classFunctions)
                    newWriter.Write(",\"" + container2.Name + '"');
                newWriter.Write("],\n\t[\n\t\t{throw \"UNKNOWN FUNCTION\";}");
                foreach (OosClassVariable container2 in classVariables)
                {
                    newWriter.Write(',');
                    WriteOutTree(container2, curPath, cfgClass, newWriter);
                }
                foreach (OosClassFunction container2 in classFunctions)
                {
                    newWriter.Write(",\n\t\t{\n");
                    WriteOutTree(container2, curPath, cfgClass, newWriter);
                    newWriter.Write("\n\t\t}");
                }
                newWriter.Write("],[");
                newWriter.Write('"' + obj.Name + '"');
                newWriter.Write(", [\"" + obj.Name + '"');
                foreach (var container2 in obj.ParentClasses)
                    newWriter.Write(",\"" + container2 + '"');
                newWriter.Write(']');
                newWriter.Write("]];");
                WriteOutTree(constructor, curPath, cfgClass, newWriter);
                newWriter.Flush();
                newWriter.Close();
            }
            #endregion
            #region OosGlobalFunction
            else if (container is OosGlobalFunction)
            {//TODO
                OosGlobalFunction obj = (OosGlobalFunction)container;
                if (curPath.EndsWith("\\"))
                    curPath += obj.Name;
                else
                    curPath += '\\' + obj.Name + ".sqf";
                writer = new StreamWriter(curPath);
                if (obj.Name == "preInit" || obj.Name == "postInit")
                {
                    if (obj.Name == "preInit")
                    {
                        var cont = obj.getFirstOf<OosContainer>();
                        var l = cont.getAllChildrenOf<OosGlobalVariable>(true);
                        foreach (var o in l)
                        {
                            writer.Write("if(isNil\"" + o.getNormalizedName() + "\") then {missionNamespace setVariable[\"" + o.getNormalizedName() + "\",");
                            WriteOutTree(o.Value, curPath, configObj, writer);
                            writer.Write("];};");
                        }
                        configObj.addChild(new SqfConfigField(obj.getNormalizedName(), "file = \"" + curPath + "\"; preInit = 1; postInit = 0; recompile = 0; ext = \".sqf\";"));
                    }
                    else
                        configObj.addChild(new SqfConfigField(obj.getNormalizedName(), "file = \"" + curPath + "\"; preInit = 0; postInit = 1; recompile = 0; ext = \".sqf\";"));
                }
                else
                    configObj.addChild(new SqfConfigField(obj.getNormalizedName(), "file = \"" + curPath + "\"; preInit = 0; postInit = 0; recompile = 0; ext = \".sqf\";"));
                List<OosLocalVariable> vars = obj.getAllChildrenOf<OosLocalVariable>();
                writer.Write("private[");
                for (int i = 0; i < vars.Count; i++)
                {
                    var v = vars[i];
                    writer.Write(i == 0 ? '"' + v.Name + '"' : ",\"" + v.Name + '"');
                }
                writer.WriteLine("];");
                foreach (BaseLangObject container2 in obj.Children)
                    
                writer.Flush();
                writer.Close();
            }
            #endregion
            #region OosClassFunction
            else if (container is OosClassFunction)
            {//TODO
                OosClassFunction obj = (OosClassFunction)container;
                List<OosLocalVariable> vars = obj.getAllChildrenOf<OosLocalVariable>();
                writer.Write("private[");
                for (int i = 0; i < vars.Count; i++)
                {
                    var v = vars[i];
                    writer.Write(i == 0 ? '"' + v.Name + '"' : ",\"" + v.Name + '"');
                }
                writer.WriteLine("];");
                foreach (BaseLangObject container2 in obj.Children)
                    WriteOutTree(container2, curPath, configObj, writer);
            }
            #endregion
            #region OosClassVariable
            else if (container is OosClassVariable)
            {//TODO
            }
            #endregion
            #region OosBreak
            else if (container is OosBreak)
            {//TODO
            }
            #endregion
            #region OosSwitch
            else if (container is OosSwitch)
            {//TODO
            }
            #endregion
            #region OosCase
            else if (container is OosCase)
            {//TODO
            }
            #endregion
            #region OosExpression
            else if (container is OosExpression)
            {//TODO
            }
            #endregion
            #region OosForLoop
            else if (container is OosForLoop)
            {//TODO
            }
            #endregion
            #region OosFunctionCall
            else if (container is OosFunctionCall)
            {//TODO
            }
            #endregion
            #region OosIfElse
            else if (container is OosIfElse)
            {//TODO
            }
            #endregion
            #region OosInstanceOf
            else if (container is OosInstanceOf)
            {//TODO
            }
            #endregion
            #region OosLocalVariable
            else if (container is OosLocalVariable)
            {//TODO
            }
            #endregion
            #region OosObjectCreation
            else if (container is OosObjectCreation)
            {//TODO
            }
            #endregion
            #region OosLocalVariable
            else if (container is OosLocalVariable)
            {//TODO
            }
            #endregion
            #region OosQuickAssignment
            else if (container is OosQuickAssignment)
            {//TODO
            }
            #endregion
            #region OosReturn
            else if (container is OosReturn)
            {//TODO
            }
            #endregion
            #region OosTryCatch
            else if (container is OosTryCatch)
            {//TODO
            }
            #endregion
            #region OosThrow
            else if (container is OosThrow)
            {//TODO
            }
            #endregion
            #region OosValue
            else if (container is OosValue)
            {//TODO
            }
            #endregion
            #region OosVariable
            else if (container is OosVariable)
            {//TODO
            }
            #endregion
            #region OosVariableAssignment
            else if (container is OosVariableAssignment)
            {//TODO
            }
            #endregion
            #region OosWhileLoop
            else if (container is OosWhileLoop)
            {//TODO
            }
            #endregion
            else
            {
                //throw new Exception("Non-Registered exception, if you ever experience this pls create a bug. Compiler.WriteOutTree");
            }
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
             */
            var filePath = proj.Buildfolder + "_preprocess_.obj";
            var newPath = proj.Buildfolder + "_compile_.obj";
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    File.Copy(filePath, newPath, true);
                    Logger.Instance.log(Logger.LogLevel.WARNING, "Compile is not supported by this compiler version");
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
                    catch (Exception ex)
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
                        try
                        {
                            if (!preprocessFile(ifdefs, defines, proj, newFile, writer))
                            {
                                //A sub file encountered an error, so stop here to prevent useles waste of ressources
                                reader.Close();
                                return false;
                            }
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
