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
        public Compiler()
        {
            configFileName = "config.cpp";
            addFunctionsClass = true;
        }
        public void setFlags(string[] strArr)
        {
            foreach (var s in strArr)
            {
                int count = s.IndexOf('=');
                if (count == -1)
                    count = s.Length;
                string switchstring = s.Substring(1, count - 1);
                switch (switchstring)
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
            OosContainer container;
            parser.getBaseContainer(out container);
            if (parser.errors.count > 0)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, "Errors found, cannot continue with Translating!");
                return;
            }
            SqfConfigFile file = new SqfConfigFile(configFileName);
            iSqfConfig cfgClass = file;
            if (addFunctionsClass)
            {
                cfgClass = new SqfConfigClass("cfgFunctions");
                file.addChild(cfgClass);
            }
            WriteOutTree(proj, container, proj.OutputFolder, cfgClass, null);
            //Create config.cpp file
            file.writeOut(proj.OutputFolder);
        }
        public void WriteOutTree(Project proj, BaseLangObject container, string path, iSqfConfig configObj, StreamWriter writer, int tabCount = 0)
        {
            string curPath = path;
            string tab = new string('\t', tabCount);
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
                foreach (BaseLangObject blo in obj.Children)
                {
                    if (!(blo is OosNamespace) && !(blo is OosClass))
                    {
                        if (cfgClass == null)
                        {
                            cfgClass = new SqfConfigClass(obj.getNormalizedName());
                            configObj.addChild(cfgClass);
                        }
                        if (blo is OosGlobalFunction)
                        {
                            if (cfgClass_GenericFunctions == null)
                            {
                                cfgClass_GenericFunctions = new SqfConfigClass("generic");
                                cfgClass.addChild(cfgClass_GenericFunctions);
                            }
                            WriteOutTree(proj, blo, curPath, cfgClass_GenericFunctions, writer);
                        }
                        else if (blo is OosGlobalVariable)
                        {
                            continue; //Will be added in last step
                        }
                        else
                        {
                            throw new Exception("Non-Registered exception, if you ever experience this pls create a bug. Compiler.WriteOutTree");
                        }
                    }
                    else
                    {
                        WriteOutTree(proj, blo, curPath, configObj, writer);
                    }
                }
            }
            #endregion
            #region OosClass
            else if (container is OosClass)
            {//ToDo: Find all base classes and merge the functions into this one
                OosClass obj = (OosClass)container;

                //Get parent classes
                List<OosClass> parentClasses = new List<OosClass>();
                List<BaseFunctionObject> constructorsParentClasses = new List<BaseFunctionObject>();
                var allClasses = obj.getFirstOf<OosContainer>().getAllChildrenOf<OosClass>(true);
                foreach (var c in allClasses)
                    if (obj.ParentClasses.Contains(c.Name))
                        parentClasses.Add(c);

                if (curPath.EndsWith("\\"))
                    curPath += obj.Name;
                else
                    curPath += '\\' + obj.Name;
                Directory.CreateDirectory(curPath);
                SqfConfigClass cfgClass = new SqfConfigClass(obj.getNormalizedName());
                configObj.addChild(cfgClass);
                configObj = cfgClass;
                cfgClass = new SqfConfigClass(obj.Name);
                configObj.addChild(cfgClass);
                BaseFunctionObject constructor = null;
                List<OosClassFunction> classFunctions = new List<OosClassFunction>();
                List<OosClassVariable> classVariables = new List<OosClassVariable>();
                foreach (BaseLangObject blo in obj.Children)
                {
                    if (blo is OosClass)
                    {
                        WriteOutTree(proj, blo, curPath, configObj, writer);
                    }
                    else if (blo is OosGlobalFunction)
                    {
                        WriteOutTree(proj, blo, curPath, cfgClass, writer);
                    }
                    else if (blo is OosClassFunction)
                    {
                        if (((BaseFunctionObject)blo).Name == "constructor")
                            if (constructor != null)
                                throw new Exception("Non-Registered exception, if you ever experience this pls create a bug. Compiler.WriteOutTree");
                            else
                                constructor = (BaseFunctionObject)blo;
                        else
                            classFunctions.Add((OosClassFunction)blo);
                    }
                    else if (blo is OosClassVariable)
                    {
                        classVariables.Add((OosClassVariable)blo);
                    }
                    else if (blo is OosGlobalVariable)
                    {
                        continue; //Will be added in last step
                    }
                    else
                    {
                        throw new Exception("Non-Registered exception, if you ever experience this pls create a bug. Compiler.WriteOutTree");
                    }
                }
                foreach (var c in parentClasses)
                {
                    BaseFunctionObject parentsConstructor = null;
                    foreach (BaseLangObject blo in c.Children)
                    {
                        if (blo is OosClassFunction)
                        {
                            if (((BaseFunctionObject)blo).Name == "constructor")
                                if (parentsConstructor != null)
                                    throw new Exception("Non-Registered exception, if you ever experience this pls create a bug. Compiler.WriteOutTree");
                                else
                                    parentsConstructor = (BaseFunctionObject)blo;
                            else
                                classFunctions.Add((OosClassFunction)blo);
                        }
                        else if (blo is OosClassVariable)
                        {
                            classVariables.Add((OosClassVariable)blo);
                        }
                    }
                    constructorsParentClasses.Add(parentsConstructor);
                }
                //Handle constructor manually (as it has obviously more to do then the generic DoSomething functions ... or do you want a non-functional object do you?)
                string constructorPath = curPath + '\\' + "class____constructor.sqf";
                StreamWriter newWriter = new StreamWriter(constructorPath);
                Logger.Instance.log(Logger.LogLevel.VERBOSE, "Handling file:");
                Logger.Instance.log(Logger.LogLevel.CONTINUE, constructorPath);
                var tmpCfgClass = new SqfConfigClass("class____constructor");
                cfgClass.addChild(tmpCfgClass);
                tmpCfgClass.addChild(new SqfConfigField("file", "\"" + constructorPath.Substring(proj.OutputFolder.Length) + "\""));
                tmpCfgClass.addChild(new SqfConfigField("recompile", "0"));
                tmpCfgClass.addChild(new SqfConfigField("ext", "\".sqf\""));
                tmpCfgClass.addChild(new SqfConfigField("preInit", "0"));
                tmpCfgClass.addChild(new SqfConfigField("postInit", "0"));



                newWriter.WriteLine("private \"_obj\";");
                newWriter.Write("_obj = [\n\t[");
                int objectIdentifiersCount = 0;
                foreach (OosClassVariable blo in classVariables)
                {
                    newWriter.Write((objectIdentifiersCount > 0 ? "," : "") + (blo.Encapsulation == ClassEncapsulation.PRIVATE ? "nil" : '"' + blo.Name + '"'));
                    objectIdentifiersCount++;
                }
                foreach (OosClassFunction blo in classFunctions)
                {
                    newWriter.Write((objectIdentifiersCount > 0 ? "," : "") + (blo.Encapsulation == ClassEncapsulation.PRIVATE ? "nil" : '"' + blo.Name + '"'));
                    objectIdentifiersCount++;
                }
                newWriter.Write("],\n\t[");
                objectIdentifiersCount = 0;
                foreach (OosClassVariable blo in classVariables)
                {
                    newWriter.Write((objectIdentifiersCount > 0 ? ",\"" : "\"") + blo.Name + '"');
                    objectIdentifiersCount++;
                }
                foreach (OosClassFunction blo in classFunctions)
                {
                    newWriter.Write((objectIdentifiersCount > 0 ? ",\"" : "\"") + blo.Name + '"');
                    objectIdentifiersCount++;
                }
                newWriter.Write("],\n\t[\n\t\t{throw \"UNKNOWN FUNCTION\";}");
                foreach (OosClassVariable blo in classVariables)
                {
                    newWriter.Write(",\n\t\t");
                    WriteOutTree(proj, blo, curPath, cfgClass, newWriter, 3);
                }
                foreach (OosClassFunction blo in classFunctions)
                {
                    newWriter.Write(",\n\t\t{\n");
                    WriteOutTree(proj, blo, curPath, cfgClass, newWriter, 3);
                    newWriter.Write("\n\t\t}");
                }
                newWriter.Write("\n\t],\n\t[\"" + obj.Name + "\", [\"" + obj.Name + '"');
                foreach (var blo in obj.ParentClasses)
                    newWriter.Write(",\"" + blo + '"');
                newWriter.WriteLine("]]");
                newWriter.WriteLine("];");
                foreach (var bfo in constructorsParentClasses)
                    WriteOutTree(proj, bfo, curPath, cfgClass, newWriter);
                WriteOutTree(proj, constructor, curPath, cfgClass, newWriter);
                newWriter.WriteLine("_obj");
                newWriter.Flush();
                newWriter.Close();
            }
            #endregion
            #region OosGlobalFunction
            else if (container is OosGlobalFunction)
            {
                OosGlobalFunction obj = (OosGlobalFunction)container;
                if (curPath.EndsWith("\\"))
                    curPath += obj.Name;
                else
                    curPath += '\\' + obj.Name + ".sqf";
                writer = new StreamWriter(curPath);
                Logger.Instance.log(Logger.LogLevel.VERBOSE, "Handling file:");
                Logger.Instance.log(Logger.LogLevel.CONTINUE, curPath);
                var s = obj.getNormalizedName();
                var retIndex = s.IndexOf("fnc_");
                var tmpCfgClass = new SqfConfigClass(s.Substring(retIndex < 0 ? 0 : retIndex + 4));
                configObj.addChild(tmpCfgClass);
                tmpCfgClass.addChild(new SqfConfigField("file", "\"" + curPath.Substring(proj.OutputFolder.Length) + "\""));
                Logger.Instance.log(Logger.LogLevel.VERBOSE, "Configfile dir:");
                Logger.Instance.log(Logger.LogLevel.CONTINUE, curPath.Substring(proj.OutputFolder.Length));
                tmpCfgClass.addChild(new SqfConfigField("recompile", "0"));
                tmpCfgClass.addChild(new SqfConfigField("ext", "\".sqf\""));
                if (obj.Name == "preInit" || obj.Name == "postInit")
                {
                    if (obj.Name == "preInit")
                    {
                        Logger.Instance.log(Logger.LogLevel.VERBOSE, "Located PreInit function");
                        var cont = obj.getFirstOf<OosContainer>();
                        var l = cont.getAllChildrenOf<OosGlobalVariable>(true);
                        foreach (var o in l)
                        {
                            writer.Write("if(isNil\"" + o.getNormalizedName() + "\") then {missionNamespace setVariable[\"" + o.getNormalizedName() + "\",");
                            Logger.Instance.log(Logger.LogLevel.VERBOSE, "Writing out GlobalVariable '" + o.getNormalizedName() + "'");

                            if (o.Value == null)
                            {
                                writer.Write("nil");
                            }
                            else
                            {
                                WriteOutTree(proj, o.Value, curPath, configObj, writer);
                                Logger.Instance.log(Logger.LogLevel.CONTINUE, "with the value '" + o.getNormalizedName() + "'");
                            }
                            writer.WriteLine("];};");
                        }
                        tmpCfgClass.addChild(new SqfConfigField("preInit", "1"));
                        tmpCfgClass.addChild(new SqfConfigField("postInit", "0"));
                    }
                    else
                    {
                        Logger.Instance.log(Logger.LogLevel.VERBOSE, "Located PostInit function");
                        tmpCfgClass.addChild(new SqfConfigField("preInit", "0"));
                        tmpCfgClass.addChild(new SqfConfigField("postInit", "1"));
                    }
                }
                else
                {
                    tmpCfgClass.addChild(new SqfConfigField("preInit", "0"));
                    tmpCfgClass.addChild(new SqfConfigField("postInit", "0"));
                }
                List<OosLocalVariable> vars = obj.getAllChildrenOf<OosLocalVariable>();
                writer.Write("private[");
                int privateCounter = 0;
                foreach (var v in vars)
                {
                    writer.Write(privateCounter == 0 ? '"' + v.Name + '"' : ",\"" + v.Name + '"');
                    privateCounter++;
                }
                writer.WriteLine("];");
                writer.WriteLine("scopeName \"fnc\";");
                writer.Write(tab + "params [\"_obj\"");
                int argCounter = 0;
                foreach (var v in obj.ArgList)
                {
                    writer.Write(argCounter == 0 ? '"' + v + '"' : ",\"" + v + '"');
                    argCounter++;
                }
                writer.WriteLine("];");
                foreach (BaseLangObject blo in obj.Children)
                {
                    WriteOutTree(proj, blo, curPath, configObj, writer, tabCount);
                    if (blo is OosNative)
                        writer.WriteLine();
                    else
                        writer.WriteLine(";");
                }
                writer.Flush();
                writer.Close();
            }
            #endregion
            #region OosClassFunction
            else if (container is OosClassFunction)
            {
                OosClassFunction obj = (OosClassFunction)container;
                List<OosLocalVariable> vars = obj.getAllChildrenOf<OosLocalVariable>();
                writer.Write(tab + "private[");
                int privateCounter = 0;
                foreach (var v in vars)
                {
                    writer.Write(privateCounter == 0 ? '"' + v.Name + '"' : ",\"" + v.Name + '"');
                    privateCounter++;
                }
                writer.WriteLine("];");
                writer.WriteLine(tab + "scopeName \"fnc\";");
                writer.Write(tab + "params [\"_obj\"");
                foreach (var v in obj.ArgList)
                {
                    writer.Write(",\"" + v + '"');
                }
                writer.WriteLine("];");
                foreach (BaseLangObject blo in obj.Children)
                {
                    WriteOutTree(proj, blo, curPath, configObj, writer, tabCount);
                    if (blo is OosNative)
                        writer.WriteLine();
                    else
                        writer.WriteLine(";");
                }
            }
            #endregion
            #region OosClassVariable
            else if (container is OosClassVariable)
            {
                var obj = (OosClassVariable)container;
                if (obj.HasValue)
                {
                    WriteOutTree(proj, obj.Value, path, configObj, writer, tabCount);
                }
                else
                {
                    writer.Write("nil");
                }
            }
            #endregion
            #region OosBreak
            else if (container is OosBreak)
            {
                writer.Write(tab + "breakOut \"breakable\"");
            }
            #endregion
            #region OosSwitch
            else if (container is OosSwitch)
            {
                var obj = (OosSwitch)container;
                writer.Write(tab + "switch (");
                WriteOutTree(proj, obj.Expression, path, configObj, writer, tabCount);
                writer.WriteLine(") do");
                writer.WriteLine(tab + "{");
                foreach (var blo in obj.Instructions)
                {
                    WriteOutTree(proj, blo, path, configObj, writer, tabCount + 1);
                    writer.WriteLine();
                }
                writer.Write(tab + "}");
            }
            #endregion
            #region OosCase
            else if (container is OosCase)
            {
                var obj = (OosCase)container;
                writer.Write(tab);
                if (obj.IsDefault)
                {
                    writer.WriteLine("default {");
                }
                else
                {
                    writer.Write("case ");
                    WriteOutTree(proj, obj.Value, path, configObj, writer, tabCount);
                    writer.WriteLine(": {");
                }
                foreach (var blo in obj.Instructions)
                {
                    WriteOutTree(proj, blo, curPath, configObj, writer, tabCount + 1);
                    if (blo is OosNative)
                        writer.WriteLine();
                    else
                        writer.WriteLine(";");
                }
                writer.Write(tab + "};");
            }
            #endregion
            #region OosExpression
            else if (container is OosExpression)
            {
                var obj = (OosExpression)container;
                if (obj.Negate)
                    writer.Write("!(");
                else
                    writer.Write("(");
                WriteOutTree(proj, obj.LInstruction, path, configObj, writer, 0);
                writer.Write(")");
                switch (obj.Op)
                {
                    case ExpressionOperator.And:
                        writer.Write(" && (");
                        WriteOutTree(proj, obj.RInstruction, path, configObj, writer, 0);
                        writer.Write(")");
                        break;
                    case ExpressionOperator.AndAnd:
                        writer.Write(" && {");
                        WriteOutTree(proj, obj.RInstruction, path, configObj, writer, 0);
                        writer.Write("}");
                        break;
                    case ExpressionOperator.Or:
                        writer.Write(" || (");
                        WriteOutTree(proj, obj.RInstruction, path, configObj, writer, 0);
                        writer.Write(")");
                        break;
                    case ExpressionOperator.OrOr:
                        writer.Write(" || {");
                        WriteOutTree(proj, obj.RInstruction, path, configObj, writer, 0);
                        writer.Write("}");
                        break;
                    case ExpressionOperator.Equals:
                        writer.Write(" == (");
                        WriteOutTree(proj, obj.RInstruction, path, configObj, writer, 0);
                        writer.Write(")");
                        break;
                    case ExpressionOperator.ExplicitEquals:
                        writer.Write(" isEqualTo (");
                        WriteOutTree(proj, obj.RInstruction, path, configObj, writer, 0);
                        writer.Write(")");
                        break;
                    case ExpressionOperator.Minus:
                        writer.Write(" - (");
                        WriteOutTree(proj, obj.RInstruction, path, configObj, writer, 0);
                        writer.Write(")");
                        break;
                    case ExpressionOperator.Plus:
                        writer.Write(" + (");
                        WriteOutTree(proj, obj.RInstruction, path, configObj, writer, 0);
                        writer.Write(")");
                        break;
                    case ExpressionOperator.Multiplication:
                        writer.Write(" * (");
                        WriteOutTree(proj, obj.RInstruction, path, configObj, writer, 0);
                        writer.Write(")");
                        break;
                    case ExpressionOperator.Division:
                        writer.Write(" / (");
                        WriteOutTree(proj, obj.RInstruction, path, configObj, writer, 0);
                        writer.Write(")");
                        break;
                    case ExpressionOperator.Larger:
                        writer.Write(" > (");
                        WriteOutTree(proj, obj.RInstruction, path, configObj, writer, 0);
                        writer.Write(")");
                        break;
                    case ExpressionOperator.LargerEquals:
                        writer.Write(" >= (");
                        WriteOutTree(proj, obj.RInstruction, path, configObj, writer, 0);
                        writer.Write(")");
                        break;
                    case ExpressionOperator.Smaller:
                        writer.Write(" < (");
                        WriteOutTree(proj, obj.RInstruction, path, configObj, writer, 0);
                        writer.Write(")");
                        break;
                    case ExpressionOperator.SmallerEquals:
                        writer.Write(" <= (");
                        WriteOutTree(proj, obj.RInstruction, path, configObj, writer, 0);
                        writer.Write(")");
                        break;
                }
            }
            #endregion
            #region OosForLoop
            else if (container is OosForLoop)
            {
                var obj = (OosForLoop)container;
                WriteOutTree(proj, obj.Arg1, path, configObj, writer, tabCount);
                writer.WriteLine(';');
                writer.Write(tab + "while {");
                WriteOutTree(proj, obj.Arg2, path, configObj, writer, tabCount);
                writer.WriteLine("} do");
                writer.WriteLine(tab + "{");
                writer.WriteLine(tab + "\tscopeName \"breakable\";");
                foreach (var blo in obj.Instructions)
                {
                    WriteOutTree(proj, blo, curPath, configObj, writer, tabCount + 1);
                    if (blo is OosNative)
                        writer.WriteLine();
                    else
                        writer.WriteLine(";");
                }
                writer.Write(tab + '\t');
                WriteOutTree(proj, obj.Arg3, path, configObj, writer, tabCount);
                writer.WriteLine(';');
                writer.Write(tab + "}");
            }
            #endregion
            #region OosFunctionCall & OosObjectCreation
            else if (container is OosFunctionCall)
            {
                var obj = (OosFunctionCall)container;
                writer.Write(obj.Parent is OosExpression ? "[" : tab + "[");
                var argList = obj.ArgList;
                int argCounter = 0;
                OosVariable ident = (OosVariable)obj.Identifier;
                if (ident.HasObjectAccess)
                {
                    if (ident.HasThisKeyword)
                        writer.Write("_obj");
                    else if (ident.IsLocal)
                        writer.Write(ident.NamespaceName);
                    else
                        writer.Write(ident.NormalizedNamespaceName);
                    argCounter++;
                }
                foreach (var o in argList)
                {
                    writer.Write(argCounter == 0 ? " " : ", ");
                    WriteOutTree(proj, o, path, configObj, writer, tabCount + 1);
                    argCounter++;
                }
                writer.Write("] call ");
                WriteOutTree(proj, obj.Identifier, path, configObj, writer, tabCount + 1);
                if (container is OosObjectCreation)
                    writer.Write("class____constructor");
            }
            #endregion
            #region OosIfElse
            else if (container is OosIfElse)
            {
                var obj = (OosIfElse)container;
                writer.Write(tab + "if(");
                WriteOutTree(proj, obj.Expression, path, configObj, writer, tabCount);
                writer.WriteLine(") then");
                writer.WriteLine(tab + "{");
                foreach (var blo in obj.IfInstructions)
                {
                    WriteOutTree(proj, blo, path, configObj, writer, tabCount + 1);
                    if (blo is OosNative)
                        writer.WriteLine();
                    else
                        writer.WriteLine(";");
                }
                writer.Write(tab + "}");
                if (obj.HasElse)
                {
                    writer.WriteLine();
                    writer.WriteLine(tab + "else");
                    writer.WriteLine(tab + "{");
                    foreach (var blo in obj.ElseInstructions)
                    {
                        WriteOutTree(proj, blo, path, configObj, writer, tabCount + 1);
                        if (blo is OosNative)
                            writer.WriteLine();
                        else
                            writer.WriteLine(";");
                    }
                    writer.Write(tab + "}");
                }
            }
            #endregion
            #region OosInstanceOf
            else if (container is OosInstanceOf)
            {
                var obj = (OosInstanceOf)container;
                WriteOutTree(proj, obj.RArgument, path, configObj, writer, tabCount);
                writer.Write(" in ((");
                WriteOutTree(proj, obj.LArgument, path, configObj, writer, tabCount);
                writer.Write(" select 3) select 1)");//select META class informations --> select class instances
            }
            #endregion
            #region OosLocalVariable
            else if (container is OosLocalVariable)
            {
                var obj = (OosLocalVariable)container;
                if (obj.HasValue)
                {
                    writer.Write(tab + obj.Name);
                    writer.Write(" = ");
                    WriteOutTree(proj, obj.Value, path, configObj, writer, tabCount);
                }
            }
            #endregion
            #region OosQuickAssignment
            else if (container is OosQuickAssignment)
            {
                var obj = (OosQuickAssignment)container;
                writer.Write(tab);
                WriteOutTree(proj, obj.Variable, path, configObj, writer, tabCount);
                if (obj.IsArrayAssignment)
                {
                    writer.Write(" set [" + obj.ArrayPosition + ",");
                    switch (obj.QuickAssignmentType)
                    {
                        case QuickAssignmentTypes.MinusMinus: // --
                            WriteOutTree(proj, obj.Variable, path, configObj, writer, tabCount);
                            writer.Write(" + 1");
                            break;
                        case QuickAssignmentTypes.PlusPlus: // ++
                            WriteOutTree(proj, obj.Variable, path, configObj, writer, tabCount);
                            writer.Write(" - 1");
                            break;
                    }
                    writer.WriteLine("]");
                }
                else
                {
                    writer.Write(" = ");
                    switch (obj.QuickAssignmentType)
                    {
                        case QuickAssignmentTypes.MinusMinus: // --
                            WriteOutTree(proj, obj.Variable, path, configObj, writer, tabCount);
                            writer.Write(" + 1");
                            break;
                        case QuickAssignmentTypes.PlusPlus: // ++
                            WriteOutTree(proj, obj.Variable, path, configObj, writer, tabCount);
                            writer.Write(" - 1");
                            break;
                    }
                }
            }
            #endregion
            #region OosReturn
            else if (container is OosReturn)
            {
                var obj = (OosReturn)container;
                writer.Write(tab);
                WriteOutTree(proj, obj.Expression, path, configObj, writer, tabCount);
                writer.Write(" breakOut \"fnc\"");
            }
            #endregion
            #region OosNative
            else if (container is OosNative)
            {
                var obj = (OosNative)container;
                writer.Write(tab + obj.nativeCode);
            }
            #endregion
            #region OosTryCatch
            else if (container is OosTryCatch)
            {
                var obj = (OosTryCatch)container;
                writer.WriteLine(tab + "try");
                writer.WriteLine(tab + "{");
                foreach (var blo in obj.TryInstructions)
                {
                    WriteOutTree(proj, blo, curPath, configObj, writer, tabCount + 1);
                    if (blo is OosNative)
                        writer.WriteLine();
                    else
                        writer.WriteLine(";");
                }
                writer.WriteLine(tab + "}");
                writer.WriteLine(tab + "catch");
                writer.WriteLine(tab + "{");
                var varAssignment = new OosVariableAssignment();
                varAssignment.Variable = obj.CatchVariable;
                varAssignment.Value = new OosValue("_exception");
                varAssignment.AssignmentOperator = AssignmentOperators.Equals;
                WriteOutTree(proj, varAssignment, curPath, configObj, writer, tabCount + 1);
                writer.WriteLine(';');
                foreach (var blo in obj.CatchInstructions)
                {
                    WriteOutTree(proj, blo, curPath, configObj, writer, tabCount + 1);
                    if (blo is OosNative)
                        writer.WriteLine();
                    else
                        writer.WriteLine(";");
                }
                writer.Write(tab + "}");
            }
            #endregion
            #region OosThrow
            else if (container is OosThrow)
            {
                var obj = (OosThrow)container;
                writer.Write(tab + "throw ");
                WriteOutTree(proj, obj.Expression, path, configObj, writer, tabCount);
            }
            #endregion
            #region OosIsSet
            else if (container is OosIsSet)
            {
                var obj = (OosIsSet)container;
                writer.Write(tab + "isNil {");
                WriteOutTree(proj, obj.Expression, path, configObj, writer, tabCount);
                writer.Write("}");
            }
            #endregion
            #region OosValue
            else if (container is OosValue)
            {
                var obj = (OosValue)container;
                writer.Write(obj.Value);
            }
            #endregion
            #region OosSqfCall
            else if (container is OosSqfCall)
            {
                var obj = (OosSqfCall)container;
                var lArgs = obj.LArgs;
                var rArgs = obj.RArgs;
                var counter = 0;

                if (!(obj.Parent is OosExpression))
                    writer.Write(tab);
                if (lArgs.Count > 0)
                {
                    if (lArgs.Count > 1)
                        writer.Write('[');
                    counter = 0;
                    foreach (var o in lArgs)
                    {
                        if (counter != 0)
                            writer.Write(',');
                        WriteOutTree(proj, o, path, configObj, writer, tabCount);
                    }
                    if (lArgs.Count > 1)
                        writer.Write(']');
                }
                writer.Write(obj.InstructionName);
                if (rArgs.Count > 0)
                {
                    if (rArgs.Count > 1)
                        writer.Write('[');
                    counter = 0;
                    foreach (var o in rArgs)
                    {
                        if (counter != 0)
                            writer.Write(',');
                        WriteOutTree(proj, o, path, configObj, writer, tabCount);
                    }
                    if (rArgs.Count > 1)
                        writer.Write(']');
                }
            }
            #endregion
            #region OosVariable
            else if (container is OosVariable)
            {
                var obj = (OosVariable)container;
                char tableAccess = obj.getFirstOf<OosClass>() != null ? '1' : '0';

                if (obj.HasObjectAccess)
                {
                    if (obj.HasThisKeyword)
                    {
                        writer.Write("((_obj select 2) select (((_obj select " + tableAccess + ") find \"" + obj.FunctionName + "\") + 1))");
                    }
                    else if (obj.HasNamespace)
                    {
                        writer.Write("((" + obj.NormalizedNamespaceName + " select 2) select (((" + obj.NormalizedNamespaceName + " select " + tableAccess + ") find \"" + obj.FunctionName + "\") + 1))");
                    }
                    else if (obj.IsLocal)
                    {
                        writer.Write("((" + obj.NamespaceName + " select 2) select (((" + obj.NamespaceName + " select " + tableAccess + ") find \"" + obj.FunctionName + "\") + 1))");
                    }
                    else
                    {
                        throw new Exception("Non-registered exception was raised, if you ever experience this then please create a bug. Compiler.WriteOutTree");
                    }
                }
                else if (obj.HasNamespace)
                {
                    writer.Write(obj.NormalizedNamespaceName);
                }
                else
                {
                    writer.Write(obj.Name);
                }
            }
            #endregion
            #region OosVariableAssignment
            else if (container is OosVariableAssignment)
            {
                var obj = (OosVariableAssignment)container;
                writer.Write(tab);
                var ident = (OosVariable)obj.Variable;
                if (ident.HasObjectAccess && !obj.IsArrayAssignment)
                {
                    char tableAccess = obj.getFirstOf<OosClass>() != null ? '1' : '0';
                    if (ident.HasThisKeyword)
                        writer.Write("(_obj select 2) set [((_obj select " + tableAccess + ") find \"" + ident.FunctionName + "\") + 1, ");
                    else if (ident.HasNamespace)
                        writer.Write("(" + ident.NormalizedNamespaceName + " select 2) set [((" + ident.NormalizedNamespaceName + " select " + tableAccess + ") find \"" + ident.FunctionName + "\") + 1, ");
                    else if (ident.IsLocal)
                        writer.Write("(" + ident.NamespaceName + " select 2) set [((" + ident.NamespaceName + " select " + tableAccess + ") find \"" + ident.FunctionName + "\") + 1, ");
                    else
                        throw new Exception("Non-registered exception was raised, if you ever experience this then please create a bug. Compiler.WriteOutTree");
                }
                else
                {
                    WriteOutTree(proj, obj.Variable, path, configObj, writer, tabCount);
                    if (obj.IsArrayAssignment)
                        writer.Write(" set [" + obj.ArrayPosition + ",");
                    else
                        writer.Write(" = ");
                }
                switch (obj.AssignmentOperator)
                {
                    case AssignmentOperators.MultipliedEquals: // *=
                        WriteOutTree(proj, obj.Variable, path, configObj, writer, tabCount);
                        writer.Write(" * ");
                        WriteOutTree(proj, obj.Value, path, configObj, writer, tabCount);
                        break;
                    case AssignmentOperators.DividedEquals: // /=
                        WriteOutTree(proj, obj.Variable, path, configObj, writer, tabCount);
                        writer.Write(" / ");
                        WriteOutTree(proj, obj.Value, path, configObj, writer, tabCount);
                        break;
                    case AssignmentOperators.PlusEquals: // +=
                        WriteOutTree(proj, obj.Variable, path, configObj, writer, tabCount);
                        writer.Write(" + ");
                        WriteOutTree(proj, obj.Value, path, configObj, writer, tabCount);
                        break;
                    case AssignmentOperators.MinusEquals: // -=
                        WriteOutTree(proj, obj.Variable, path, configObj, writer, tabCount);
                        writer.Write(" - ");
                        WriteOutTree(proj, obj.Value, path, configObj, writer, tabCount);
                        break;
                    default: // =
                        WriteOutTree(proj, obj.Value, path, configObj, writer, tabCount);
                        break;
                }
                if (obj.IsArrayAssignment || ident.HasObjectAccess)
                    writer.Write("]");
            }
            #endregion
            #region OosWhileLoop
            else if (container is OosWhileLoop)
            {
                var obj = (OosWhileLoop)container;
                writer.Write(tab + "while {");
                WriteOutTree(proj, obj.Expression, path, configObj, writer, tabCount);
                writer.WriteLine("} do");
                writer.WriteLine(tab + "{");
                writer.WriteLine(tab + "\tscopeName \"breakable\";");
                foreach (var blo in obj.Instructions)
                {
                    WriteOutTree(proj, blo, curPath, configObj, writer, tabCount + 1);
                    if (blo is OosNative)
                        writer.WriteLine();
                    else
                        writer.WriteLine(";");
                }
                writer.Write(tab + "}");
            }
            #endregion
            else if (container == null)
            {
                //Just skip null objects and give a warning
                Logger.Instance.log(Logger.LogLevel.WARNING, "Experienced NULL object during WriteOutTree. Output file: " + path);
            }
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
