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
        string configFileName;
        bool addFunctionsClass;
        List<PPDefine> flagDefines;
        public static readonly string endl = "\r\n";
        public static readonly string thisVariableName = "___obj___";
        public Compiler()
        {
            configFileName = "config.cpp";
            addFunctionsClass = true;
            flagDefines = new List<PPDefine>();
            SqfCall.readSupportInfoList();
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
                        addFunctionsClass = false;
                        flagDefines.Add(new PPDefine('#' + s.Substring(count + 1)));
                        break;
                    default:
                        Logger.Instance.log(Logger.LogLevel.WARNING, "Unknown flag '" + s + "' for compiler version '" + this.getVersion().ToString() + "'");
                        break;
                }
            }
        }
        public Version getVersion()
        {
            return new Version("0.4.0-ALPHA");
        }
        public void CheckSyntax(string filepath)
        {
            Scanner scanner = new Scanner(filepath);
            Parser parser = new Parser(scanner);
            parser.Parse();
        }
        #region Translating
        public void Translate(Project proj)
        {
            //Read compiled file
            Scanner scanner = new Scanner(proj.Buildfolder + "_compile_.obj");
            Parser parser = new Parser(scanner);
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
            //ToDo: change "private" array so that it just privates on current scope level
            string curPath = path;
            string tab = new string('\t', tabCount);
            if (container == null)
            {
                //Just skip null objects and give a warning
                Logger.Instance.log(Logger.LogLevel.WARNING, "Experienced NULL object during WriteOutTree. Output file: " + path);
            }
            #region object ArrayAccess
            else if (container is ArrayAccess)
            {
                var obj = (ArrayAccess)container;
                writer.Write(" select ");
                foreach (var it in obj.children)
                {
                    WriteOutTree(proj, it, path, configObj, writer, tabCount);
                }
            }
            #endregion
            #region object Base
            else if (container is Base)
            {
                var obj = (Base)container;
                var objConfigClass = new SqfConfigClass("Generic");
                configObj.addChild(objConfigClass);
                foreach (var it in obj.children)
                {
                    if (it is Function)
                        WriteOutTree(proj, it, path, objConfigClass, writer, tabCount);
                    else
                        WriteOutTree(proj, it, path, configObj, writer, tabCount);
                }
            }
            #endregion
            #region object Namespace
            else if (container is Namespace)
            {
                var obj = (Namespace)container;
                var objConfigClass = new SqfConfigClass(obj.FullyQualifiedName.Replace("::", "_").Substring(1));
                configObj.addChild(objConfigClass);
                path += '\\' + obj.Name.OriginalValue;
                Logger.Instance.log(Logger.LogLevel.VERBOSE, "Creating directory '" + path + "'");
                Directory.CreateDirectory(path);
                foreach (var it in obj.children)
                {
                    if (it is Variable)
                    {
                        //Skip
                    }
                    else if(it is Function)
                        WriteOutTree(proj, it, path, objConfigClass, writer, tabCount);
                    else
                        WriteOutTree(proj, it, path, configObj, writer, tabCount);

                }
            }
            #endregion
            #region object oosClass
            else if (container is oosClass)
            {
                var obj = (oosClass)container;
                var objConfigClass = new SqfConfigClass(obj.FullyQualifiedName.Replace("::", "_").Substring(1));
                configObj.addChild(objConfigClass);
                path += '\\' + obj.Name.OriginalValue;
                Logger.Instance.log(Logger.LogLevel.VERBOSE, "Creating directory '" + path + "'");
                Directory.CreateDirectory(path);
                foreach (var it in obj.ClassContent)
                {
                    if (it is Variable)
                    {
                        //Skip
                    }
                    else
                    {
                        WriteOutTree(proj, it, path, objConfigClass, writer, tabCount);
                    }
                }
            }
            #endregion
            #region object Function
            else if (container is Function)
            {
                var obj = (Function)container;
                int index = 0;
                path += '\\' + obj.Name.OriginalValue + ".sqf";
                writer = new StreamWriter(path);
                var objConfigClass = new SqfConfigClass(obj.Name.OriginalValue);
                configObj.addChild(objConfigClass);
                if (obj.encapsulation == Encapsulation.NA)
                {
                    throw new Exception("Function has Encapsulation.NA on encapsulation field, please report to developer");
                }
                else if (obj.encapsulation == Encapsulation.Static || obj.IsConstructor)
                {
                    objConfigClass.addChild(new SqfConfigField("file", '"' + path.Substring(proj.OutputFolder.Length) + '"'));
                    objConfigClass.addChild(new SqfConfigField("preInit", obj.Name.OriginalValue.StartsWith("preInit", StringComparison.OrdinalIgnoreCase) ? "1" : "0"));
                    objConfigClass.addChild(new SqfConfigField("postInit", obj.Name.OriginalValue.StartsWith("postInit", StringComparison.OrdinalIgnoreCase) ? "1" : "0"));
                    objConfigClass.addChild(new SqfConfigField("preStart", obj.Name.OriginalValue.StartsWith("preStart", StringComparison.OrdinalIgnoreCase) ? "1" : "0"));
                    objConfigClass.addChild(new SqfConfigField("recompile", "0"));
                    objConfigClass.addChild(new SqfConfigField("ext", "\".sqf\""));
                    objConfigClass.addChild(new SqfConfigField("headerType", "0"));
                }
                if(obj.IsConstructor)
                {//Print object structure
                    //ToDo: generate reference arrays for the class functions/variables
                    //ToDo: print classes with the references
                    var classObject = obj.getFirstOf<oosClass>();
                    writer.WriteLine(tab + thisVariableName + " = [");
                    updateTabcount(ref tab, ref tabCount, 1);

                    //Write out parents info
                    var parentIdents = classObject.ParentClassesIdents;
                    writer.WriteLine(tab + "[");
                    updateTabcount(ref tab, ref tabCount, 1);
                    index = 0;
                    foreach (var it in parentIdents)
                    {
                        if (index > 0)
                            writer.WriteLine(",");
                        if (it is Ident)
                        {

                            writer.Write('"' + ((Ident)it).FullyQualifiedName + '"');
                        }
                        else
                        {
                            throw new Exception("Function has Encapsulation.NA on encapsulation field, please report to developer");
                        }
                    }
                    updateTabcount(ref tab, ref tabCount, -1);
                    writer.WriteLine(endl + tab + "],");

                    //Write out the different classes
                    writer.WriteLine(tab + "[");
                    updateTabcount(ref tab, ref tabCount, 1);
                    index = 0;
                    foreach (var it in parentIdents)
                    {
                        if (index > 0)
                            writer.WriteLine(",");
                        if (it is Ident)
                        {

                            writer.Write('"' + ((Ident)it).FullyQualifiedName + '"');
                        }
                        else
                        {
                            throw new Exception("Function has Encapsulation.NA on encapsulation field, please report to developer");
                        }
                    }
                    updateTabcount(ref tab, ref tabCount, -1);
                    writer.WriteLine(endl + tab + "],");
                    

                    updateTabcount(ref tab, ref tabCount, -1);
                    writer.WriteLine(tab + "]");
                }
                var argList = obj.ArgList;
                if (obj.IsClassFunction && !obj.IsConstructor)
                {
                    var tmpList = new List<pBaseLangObject>();
                    var variable = new Variable(obj, -1, -1);
                    var ident = new Ident(variable, thisVariableName, -1, -1);
                    variable.Name = ident;
                    variable.encapsulation = Encapsulation.NA;
                    variable.varType = new VarTypeObject(obj.Name);
                    tmpList.Add(variable);
                    tmpList.AddRange(argList);
                    argList = tmpList;
                }
                if (argList.Count > 0)
                {
                    writer.WriteLine("params [");
                    updateTabcount(ref tab, ref tabCount, 1);
                    index = 0;
                    foreach (var it in argList)
                    {
                        if (it is Variable)
                        {
                            if (index > 0)
                                writer.WriteLine(",");
                            var variable = (Variable)it;
                            writer.Write(tab + "\"_" + variable.Name.OriginalValue + "\"");
                            index++;
                        }
                        else
                            throw new Exception("Function has non-Variable object in arglist, please report to developer");
                    }
                    updateTabcount(ref tab, ref tabCount, -1);
                    writer.WriteLine(endl + "];");
                }
                var pVarList = obj.getAllChildrenOf<Variable>(true);
                foreach (var it in obj.ArgList)
                {
                    if (it is Variable)
                        pVarList.Remove((Variable)it);
                }

                if (pVarList.Count > 0)
                {
                    writer.WriteLine("private [");
                    updateTabcount(ref tab, ref tabCount, 1);
                    index = 0;
                    foreach (var it in pVarList)
                    {
                        if (it is Variable)
                        {
                            if (index > 0)
                                writer.WriteLine(",");
                            var variable = (Variable)it;
                            writer.Write(tab + "\"_" + variable.Name.OriginalValue + "\"");
                            index++;
                        }
                        else
                            throw new Exception("Function has non-Variable object in arglist, please report to developer");
                    }
                    updateTabcount(ref tab, ref tabCount, -1);
                    writer.WriteLine(endl + "];");
                }
                writer.WriteLine("scopeName \"function\";");
                foreach (var it in obj.CodeInstructions)
                {
                    WriteOutTree(proj, it, path, configObj, writer, tabCount);
                    writer.WriteLine(";");
                }
                writer.Flush();
                writer.Close();
            }
            #endregion
            #region object Break
            else if (container is Break)
            {
                var obj = (Break)container;
                updateTabcount(ref tab, ref tabCount, 1);
                foreach (var it in obj.children)
                {
                    WriteOutTree(proj, it, path, configObj, writer, tabCount);
                }
                updateTabcount(ref tab, ref tabCount, -1);
                writer.Write(tab + "breakOut \"loop\"");
            }
            #endregion
            #region object Expression
            else if (container is Expression)
            {
                var obj = (Expression)container;
                if (obj.rExpression != null || !(obj.Parent is Function || obj.Parent is IfElse || obj.Parent is Case || obj.Parent is While || obj.Parent is Switch || obj.Parent is For || obj.Parent is TryCatch || obj.Parent is NewArray))
                {
                    if (!(obj.Parent is Expression) || ((Expression)obj.Parent).rExpression != obj)
                        writer.Write("(");
                }
                WriteOutTree(proj, obj.lExpression, path, configObj, writer, tabCount);
                if (obj.rExpression != null)
                {
                    writer.Write(") " + obj.expOperator + " (");
                    WriteOutTree(proj, obj.rExpression, path, configObj, writer, tabCount);
                }
                if (obj.rExpression != null || !(obj.Parent is Function || obj.Parent is IfElse || obj.Parent is Case || obj.Parent is While || obj.Parent is Switch || obj.Parent is For || obj.Parent is TryCatch || obj.Parent is NewArray))
                {
                    if (!(obj.Parent is Expression) || ((Expression)obj.Parent).rExpression != obj)
                        writer.Write(")");
                }
            }
            #endregion
            #region object For
            else if (container is For)
            {
                //ToDo: find out why the hell the variables are freaking around
                var obj = (For)container;
                WriteOutTree(proj, obj.forArg1, path, configObj, writer, tabCount);
                writer.WriteLine(";");
                writer.Write(tab + "while {");
                WriteOutTree(proj, obj.forArg2, path, configObj, writer, tabCount);
                writer.WriteLine("} do");
                writer.WriteLine(tab + "{");
                updateTabcount(ref tab, ref tabCount, 1);
                writer.Write(tab);
                WriteOutTree(proj, obj.forArg3, path, configObj, writer, tabCount);
                writer.WriteLine(";");
                foreach (var it in obj.CodeInstructions)
                {
                    WriteOutTree(proj, it, path, configObj, writer, tabCount);
                    writer.WriteLine(";");
                }
                writer.Write("}");
            }
            #endregion
            #region object FunctionCall
            else if (container is FunctionCall)
            {
                var obj = (FunctionCall)container;
                if (obj.Name.ReferencedObject is Function)
                {
                    var fnc = (Function)obj.Name.ReferencedObject;
                    if (extraParam is Ident)
                    {
                        if (obj.children.Count > 0)
                        {
                            writer.WriteLine(tab + "[");
                            updateTabcount(ref tab, ref tabCount, 1);
                            int index = 0;
                            foreach (var it in obj.children)
                            {
                                if (index > 0)
                                    writer.WriteLine(",");
                                writer.Write(tab);
                                WriteOutTree(proj, it, path, configObj, writer, tabCount);
                                index++;
                            }
                            updateTabcount(ref tab, ref tabCount, -1);
                            writer.Write(endl + tab + "] call ");
                        }
                        else
                        {
                            writer.Write("[] call ");
                        }
                    }
                    if (fnc.encapsulation == Encapsulation.NA)
                    {
                        throw new Exception("ShouldNeverEverHappen Exception, developer fucked up writeOutTree -.-' BLAME HIM!!!!!");
                    }
                    else if(fnc.encapsulation == Encapsulation.Static || fnc.IsConstructor)
                    {
                        if (extraParam is Ident)
                            WriteOutTree(proj, (pBaseLangObject)extraParam, path, configObj, writer, tabCount, obj);
                        else
                            writer.Write(fnc.SqfVariableName);
                    }
                    else
                    {
                        if (extraParam is Ident)
                            WriteOutTree(proj, (pBaseLangObject)extraParam, path, configObj, writer, tabCount, obj);
                        writer.Write(fnc.SqfVariableName);
                    }
                }
                else
                {
                    throw new Exception("ShouldNeverEverHappen Exception, developer fucked up writeOutTree -.-' BLAME HIM!!!!!");
                }
            }
            #endregion
            #region object Cast
            else if (container is Cast)
            {
                //ToDo: do the actual casting stuff
                var obj = (Cast)container;
                foreach (var it in obj.children)
                {
                    WriteOutTree(proj, it, path, configObj, writer, tabCount);
                }
            }
            #endregion
            #region object Ident
            else if (container is Ident)
            {
                if (container.Parent is Namespace ||
                    container.Parent is oosClass ||
                    container.Parent is VirtualFunction ||
                    container.Parent is Variable ||
                    container.Parent is oosInterface ||
                    container.Parent is SqfCall)
                {
                    return;
                }
                var obj = (Ident)container;
                var instruction = obj.Instruction;
                var nextIdent = obj.NextIdent;
                var refObject = obj.ThisReferencedObject;
                bool flag = false;
                if (!(obj.Parent is Ident) && extraParam == null)
                {
                    var fncCall = obj.NextFunctionCall;
                    if (fncCall != null)
                    {
                        var refObj = fncCall.ReferencedObject;
                        if (refObj is Function)
                        {
                            var fnc = (Function)refObj;
                            //if (fnc.IsClassFunction && !fnc.IsConstructor)
                            //{
                                flag = true;
                                WriteOutTree(proj, fncCall, path, configObj, writer, tabCount, obj);
                            //}
                        }
                        else
                        {
                            throw new Exception("ShouldNeverEverHappen Exception, developer fucked up writeOutTree -.-' BLAME HIM!!!!!");
                        }
                    }
                }
                if (flag)
                {
                    //Do nothing
                }
                else if (refObject is Variable)
                {
                    var variable = (Variable)refObject;
                    if (instruction == null || instruction is Ident)
                    {
                        if (nextIdent != null)
                            WriteOutTree(proj, nextIdent, path, configObj, writer, tabCount);
                        WriteOutTree(proj, variable, path, configObj, writer, tabCount, true);
                        if (instruction is Ident)
                            WriteOutTree(proj, instruction, path, configObj, writer, tabCount);
                    }
                    else if (instruction is VariableAssignment)
                    {
                        WriteOutTree(proj, variable, path, configObj, writer, tabCount, true);
                        if (nextIdent == null)
                        {
                            if (variable.encapsulation == Encapsulation.NA || variable.encapsulation == Encapsulation.Static)
                            {
                                writer.Write(" = ");
                                WriteOutTree(proj, instruction, path, configObj, writer, tabCount);
                            }
                            else
                            {
                                writer.Write(" set [0, ");
                                WriteOutTree(proj, instruction, path, configObj, writer, tabCount);
                                writer.Write("]");
                            }
                        }
                        else
                        {
                            writer.Write(" select 0 ");
                            WriteOutTree(proj, nextIdent, path, configObj, writer, tabCount);
                        }
                    }
                    else if (instruction is FunctionCall)
                    {
                        WriteOutTree(proj, variable, path, configObj, writer, tabCount, false);
                        if (nextIdent != null)
                            WriteOutTree(proj, nextIdent, path, configObj, writer, tabCount);
                    }
                    else if (instruction is ArrayAccess)
                    {
                        WriteOutTree(proj, variable, path, configObj, writer, tabCount, false);
                        WriteOutTree(proj, instruction, path, configObj, writer, tabCount);
                        if (nextIdent != null)
                            WriteOutTree(proj, nextIdent, path, configObj, writer, tabCount);
                    }
                    else
                    {
                        throw new Exception("ShouldNeverEverHappen Exception, developer fucked up writeOutTree -.-' BLAME HIM!!!!!");
                    }
                }
                else if (refObject is Function)
                {
                    Function fnc = (Function)refObject;
                    if (instruction == null || instruction is Ident)
                    {
                        throw new Exception("ShouldNeverEverHappen Exception, developer fucked up writeOutTree -.-' BLAME HIM!!!!!");
                    }
                    else if (instruction is FunctionCall && (!fnc.IsClassFunction || fnc.IsConstructor))
                    {
                        WriteOutTree(proj, instruction, path, configObj, writer, tabCount);
                        if (nextIdent != null)
                            WriteOutTree(proj, nextIdent, path, configObj, writer, tabCount, obj);
                    }
                    else if (instruction is FunctionCall && fnc.IsClassFunction && extraParam != null)
                    {
                        WriteOutTree(proj, instruction, path, configObj, writer, tabCount, obj);
                        if (nextIdent != null)
                            WriteOutTree(proj, nextIdent, path, configObj, writer, tabCount);
                    }
                    else
                    {
                        //MAGIC WORKING SHIT, nothing to do here anymore (debuging is kinda funny btw.)
                    }
                }
                else if (refObject is oosClass)
                {
                    writer.Write(thisVariableName + " ");
                    WriteOutTree(proj, nextIdent, path, configObj, writer, tabCount);
                }
                else
                {
                    throw new Exception("ShouldNeverEverHappen Exception, developer fucked up writeOutTree -.-' BLAME HIM!!!!!");
                }
            }
            #endregion
            #region object IfElse
            else if (container is IfElse)
            {
                var obj = (IfElse)container;
                writer.Write(tab + "if (");
                WriteOutTree(proj, obj.expression, path, configObj, writer, 0);
                writer.WriteLine(") then");
                writer.WriteLine(tab + "{");
                updateTabcount(ref tab, ref tabCount, 1);
                foreach (var it in obj.IfInstructions)
                {
                    WriteOutTree(proj, it, path, configObj, writer, tabCount);
                    writer.WriteLine(";");
                }
                updateTabcount(ref tab, ref tabCount, -1);
                if (obj.HasElse)
                {
                    writer.WriteLine(tab + "}");
                    writer.WriteLine(tab + "else");
                    writer.WriteLine(tab + "{");
                    updateTabcount(ref tab, ref tabCount, 1);
                    foreach (var it in obj.ElseInstructions)
                    {
                        WriteOutTree(proj, it, path, configObj, writer, tabCount);
                        writer.WriteLine(";");
                    }
                    updateTabcount(ref tab, ref tabCount, -1);
                }
                writer.Write(tab + "}");
            }
            #endregion
            #region object InstanceOf
            else if (container is InstanceOf)
            {
                var obj = (InstanceOf)container;
                writer.Write("((");
                WriteOutTree(proj, obj.LIdent, path, configObj, writer, tabCount);
                writer.Write(" select 0) find (");
                writer.Write('"' + obj.RIdent.FullyQualifiedName + '"');
                writer.Write(") != -1)");
            }
            #endregion
            #region object NewArray
            else if (container is NewArray)
            {
                var obj = (NewArray)container;
                var index = 0;
                writer.Write("[");
                foreach (var it in obj.children)
                {
                    if (index > 0)
                        writer.Write(", ");
                    WriteOutTree(proj, it, path, configObj, writer, 0);
                    index++;
                }
                writer.Write("]");
            }
            #endregion
            #region object NewInstance
            else if (container is NewInstance)
            {
                var obj = (NewInstance)container;
                foreach (var it in obj.children)
                {
                    WriteOutTree(proj, it, path, configObj, writer, tabCount);
                }
            }
            #endregion
            #region object Return
            else if (container is Return)
            {
                var obj = (Return)container;
                writer.Write(tab);
                foreach (var it in obj.children)
                {
                    WriteOutTree(proj, it, path, configObj, writer, tabCount);
                }
                writer.Write(" breakOut \"function\"");
            }
            #endregion
            #region object SqfCall
            else if (container is SqfCall)
            {
                var obj = (SqfCall)container;
                var lArgs = obj.LArgs;
                var rArgs = obj.RArgs;
                if (lArgs.Count > 0)
                {
                    if (lArgs.Count == 1)
                    {
                        WriteOutTree(proj, lArgs[0], path, configObj, writer, tabCount);
                        writer.Write(" ");
                    }
                    else
                    {
                        writer.WriteLine(tab + "[");
                        updateTabcount(ref tab, ref tabCount, 1);
                        int index = 0;
                        foreach (var it in lArgs)
                        {
                            if (index > 0)
                                writer.WriteLine(",");
                            index++;
                            WriteOutTree(proj, it, path, configObj, writer, tabCount);
                        }
                        updateTabcount(ref tab, ref tabCount, -1);
                        writer.WriteLine(endl + tab + "] ");
                    }
                }
                else
                {
                    writer.Write(tab);
                }
                writer.Write(obj.Name.OriginalValue);
                if (rArgs.Count > 0)
                {
                    if (rArgs.Count == 1)
                    {
                        writer.Write(" ");
                        WriteOutTree(proj, rArgs[0], path, configObj, writer, tabCount);
                    }
                    else
                    {
                        writer.WriteLine(tab + " [");
                        int index = 0;
                        foreach (var it in rArgs)
                        {
                            if (index > 0)
                                writer.WriteLine(",");
                            index++;
                            WriteOutTree(proj, it, path, configObj, writer, tabCount);
                        }
                        writer.Write(endl + tab + "]");
                    }
                }
            }
            #endregion
            #region object Switch
            else if (container is Switch)
            {
                var obj = (Switch)container;
                writer.Write(tab + "switch(");
                WriteOutTree(proj, obj.expression, path, configObj, writer, tabCount);
                writer.WriteLine(") do");
                writer.WriteLine(tab + "{");
                updateTabcount(ref tab, ref tabCount, 1);
                foreach (var it in obj.CodeInstructions)
                {
                    WriteOutTree(proj, it, path, configObj, writer, tabCount);
                }
                updateTabcount(ref tab, ref tabCount, -1);
                writer.Write(tab + "}");
            }
            #endregion
            #region object Case
            else if (container is Case)
            {
                var obj = (Case)container;
                if (obj.expression == null)
                {
                    writer.WriteLine(tab + "default: {");
                }
                else
                {
                    writer.Write(tab + "case ");
                    WriteOutTree(proj, obj.expression, path, configObj, writer, 0);
                    writer.WriteLine(": {");
                }
                updateTabcount(ref tab, ref tabCount, 1);
                foreach (var it in obj.CodeInstructions)
                {
                    WriteOutTree(proj, it, path, configObj, writer, tabCount);
                    writer.WriteLine(";");
                }
                updateTabcount(ref tab, ref tabCount, -1);
                writer.WriteLine(tab + "};");
            }
            #endregion
            #region object Throw
            else if (container is Throw)
            {
                var obj = (Throw)container;
                writer.Write(tab + "throw ");
                foreach (var it in obj.children)
                {
                    WriteOutTree(proj, it, path, configObj, writer, tabCount);
                }
            }
            #endregion
            #region object TryCatch
            else if (container is TryCatch)
            {
                var obj = (TryCatch)container;
                writer.WriteLine(tab + "try");
                writer.WriteLine(tab + "{");
                updateTabcount(ref tab, ref tabCount, 1);
                foreach (var it in obj.TryInstructions)
                {
                    WriteOutTree(proj, it, path, configObj, writer, tabCount);
                    writer.WriteLine(";");
                }
                updateTabcount(ref tab, ref tabCount, -1);
                writer.WriteLine(tab + "}");
                writer.WriteLine(tab + "catch");
                writer.WriteLine(tab + "{");
                updateTabcount(ref tab, ref tabCount, 1);
                WriteOutTree(proj, obj.variable, path, configObj, writer, tabCount);
                writer.WriteLine(";");
                foreach (var it in obj.CatchInstructions)
                {
                    WriteOutTree(proj, it, path, configObj, writer, tabCount);
                    writer.WriteLine(";");
                }
                updateTabcount(ref tab, ref tabCount, -1);
                writer.Write(tab + "}");
            }
            #endregion
            #region object Value
            else if (container is Value)
            {
                var obj = (Value)container;
                foreach (var it in obj.children)
                {
                    WriteOutTree(proj, it, path, configObj, writer, tabCount);
                }
                writer.Write(obj.value);
            }
            #endregion
            #region object Variable
            else if (container is Variable)
            {
                var obj = (Variable)container;
                if (obj.Parent is oosClass || obj.Parent is Namespace || obj.Parent is Ident || extraParam != null)
                {
                    foreach (var it in obj.children)
                    {
                        if (it is Ident)
                        {
                            writer.Write(obj.SqfVariableName);
                        }
                        else
                        {
                            if (!(extraParam is bool))
                                WriteOutTree(proj, it, path, configObj, writer, tabCount);
                        }
                    }
                }
                else if (obj.Parent is Function || obj.Parent is For)
                {
                    foreach (var it in obj.children)
                    {
                        if (it is Ident)
                        {
                            var ident = (Ident)it;
                            writer.Write(tab + "_" + ident.OriginalValue + " = ");
                        }
                        else
                        {
                            WriteOutTree(proj, it, path, configObj, writer, tabCount);
                        }
                    }
                }
                else if (obj.Parent is TryCatch)
                {
                    foreach (var it in obj.children)
                    {
                        if (it is Ident)
                        {
                            var ident = (Ident)it;
                            writer.Write(tab + "_" + ident.OriginalValue + " = _exception");
                        }
                        else
                        {
                            WriteOutTree(proj, it, path, configObj, writer, tabCount);
                        }
                    }
                }
                else
                {
                    throw new Exception("ShouldNeverEverHappen Exception, developer fucked up writeOutTree -.-' BLAME HIM!!!!!");
                }
            }
            #endregion
            #region object VariableAssignment
            else if (container is VariableAssignment)
            {
                var obj = (VariableAssignment)container;
                foreach (var it in obj.children)
                {
                    WriteOutTree(proj, it, path, configObj, writer, tabCount);
                }
            }
            #endregion
            #region object While
            else if (container is While)
            {
                var obj = (While)container;
                writer.Write(tab + "while {");
                WriteOutTree(proj, obj.expression, path, configObj, writer, tabCount);
                writer.WriteLine("} do");
                writer.WriteLine(tab + "{");
                updateTabcount(ref tab, ref tabCount, 1);
                foreach (var it in obj.CodeInstructions)
                {
                    WriteOutTree(proj, it, path, configObj, writer, tabCount);
                    writer.WriteLine(";");
                }
                updateTabcount(ref tab, ref tabCount, -1);
                writer.Write("}");
            }
            #endregion
            #region object oosInterface
            else if (container is oosInterface)
            {
                //Interfaces are just logical structures in OOS, thus nothing gets created here
            }
            #endregion
            #region object VirtualFunction
            else if (container is VirtualFunction)
            {
                //VirtualFunctions are just logical structures in OOS, thus nothing gets created here
            }
            #endregion
            else
            {
                throw new Exception("ShouldNeverEverHappen Exception, developer fucked up writeOutTree -.-' BLAME HIM!!!!!");
            }
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
                string sTrimmed = s.Trim();
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
