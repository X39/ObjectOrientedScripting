using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler.OOS_LanguageObjects
{
    public class Function : pBaseLangObject, Interfaces.iName, Interfaces.iFunction
    {
        public static int ObjectValueOffset { get { return 2; } }
        public Ident Name { get { return ((Ident)this.children[0]); } set { this.children[0] = value; } }
        public VarTypeObject varType;
        private int endMarker;
        private int endMarker2;
        public Encapsulation encapsulation;

        private List<pBaseLangObject> ArgListObjects { get { return this.children.GetRange(1, endMarker); } }
        private List<pBaseLangObject> BaseCalls { get { if (endMarker + 1 - endMarker2 == 0) return new List<pBaseLangObject>(); return this.children.GetRange(endMarker + 1, endMarker2); } }
        public List<pBaseLangObject> CodeInstructions { get { return this.children.GetRange(endMarker2 + 1, this.children.Count - (endMarker2 + 1)); } }
        public bool IsConstructor { get { if (this.Parent is Base) return false; return this.Name.OriginalValue == ((Interfaces.iName)this.Parent).Name.OriginalValue; } }
        public bool IsClassFunction { get { return this.encapsulation != Encapsulation.Static && this.encapsulation != Encapsulation.NA; } }

        public bool IsAsync { get; set; }
        public bool IsVirtual { get; set; }
        public bool IsExternal { get; set; }
        public bool IsInline { get; set; }
        public string SqfSuffix { get; internal set; }
        public string SqfVariableName
        {
            get
            {
                if (this.encapsulation == Encapsulation.NA)
                {
                    if (this.Name.OriginalValue == Wrapper.Compiler.thisVariableName)
                        return Wrapper.Compiler.thisVariableName;
                    else
                        return this.Name.OriginalValue;
                }
                else if (this.IsVirtual)
                {
                    var casted = (Interfaces.iGetIndex)this.Parent;

                    return " select " + (casted.getIndex(this.Name) + Function.ObjectValueOffset);
                }
                else if(this.IsInline)
                {
                    var memStream = new MemoryStream();
                    this.writeOut(new StreamWriter(memStream), null);
                    memStream.Seek(0, SeekOrigin.Begin);
                    var result = '{' + new StreamReader(memStream).ReadToEnd() + '}';
                    memStream.Close();
                    return result;
                }
                else
                {
                    string fqn = this.Name.FullyQualifiedName;
                    int index = fqn.LastIndexOf("::");
                    if (index >= 0)
                        fqn = fqn.Insert(index, "_fnc").Replace("::", "_");
                    return (fqn.StartsWith("fnc_") ? "Generic_" + fqn : fqn) + this.SqfSuffix;
                }
            }
        }

        /// <summary>
        /// Return type of this iFunction
        /// </summary>
        public VarTypeObject ReturnType { get { return this.varType; } }
        public VarTypeObject ReferencedType { get { return this.ReturnType; } }
        /// <summary>
        /// Returns a Template object which then can deref some unknown class conflicts in
        /// ArgList field
        /// </summary>
        public Template TemplateArguments { get { return null; } }
        /// <summary>
        /// Returns functions encapsulation
        /// </summary>
        public Encapsulation FunctionEncapsulation { get { return this.encapsulation; } }
        /// <summary>
        /// Returns the Arglist required for this iFunction
        /// </summary>
        public List<VarTypeObject> ArgList
        {
            get
            {
                List<VarTypeObject> retList = new List<VarTypeObject>();
                foreach (var it in this.ArgListObjects)
                {
                    if (it is Variable)
                    {
                        retList.Add(((Variable)it).varType);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                return retList;
            }
        }

        public Function(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
            varType = null;
            this.IsAsync = false;
            this.SqfSuffix = "";
        }
        public override int doFinalize()
        {
            int errCount = 0;
            if (this.IsAsync)
            {
                if (!this.varType.Equals(Wrapper.Compiler.InternalObjectVarTypes.VT_script))
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0021, this.Name.Line, this.Name.Pos, this.Name.File));
                    errCount++;
                }
            }
            else
            {
                if (this.ReturnType.varType != VarType.Void && !this.AlwaysReturns && !this.IsConstructor && !this.IsExternal)
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0021, this.Name.Line, this.Name.Pos, this.Name.File));
                    errCount++;
                }
            }
            if (this.IsConstructor)
            {
                var retList = this.getAllChildrenOf<Return>(true);
                if(!retList.TrueForAll(it => it.children.Count == 0))
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0025, this.Name.Line));
                    errCount++;
                }
                foreach(var it in retList)
                {
                    var val = new Value(it);
                    val.value = Wrapper.Compiler.thisVariableName;
                    it.addChild(val);
                }
            }
            switch(this.Name.OriginalValue)
            {
                case "preInit": case "postInit": case "preStart":
                    if(this.encapsulation != Encapsulation.Static)
                    {
                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0027, this.Name.Line));
                        errCount++;
                    }
                    break;
            }
            //ToDo: Find a way to reallow interfaces as arguments (then remove the following foreach
            foreach(var it in this.ArgListObjects)
            {
                if (it is Variable && ((Variable)it).ReferencedType.IsObject && ((Variable)it).ReferencedType.ident.LastIdent.ReferencedObject is oosInterface)
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, "Interfaces for function arguments are temporary disabled. Line: " + this.Name.Line);
                    errCount++;
                }
            }
            //up to here)
            var fncList = this.Parent.getAllChildrenOf<Function>();
            int index = 0;
            foreach(var it in fncList)
            {
                if (it == this)
                    break;
                if(it.Name.FullyQualifiedName.Equals(this.Name.FullyQualifiedName, StringComparison.OrdinalIgnoreCase) && it != this)
                {
                    index++;
                }
            }
            if (index > 0)
                this.SqfSuffix = index.ToString();
            return errCount;
        }
        public void markArgListEnd()
        {
            endMarker = this.children.Count - 1;
            markBaseCallEnd();
        }
        public void markBaseCallEnd()
        {
            endMarker2 = this.children.Count - 1;
        }
        public override string ToString()
        {
            return "fnc->" + this.Name.FullyQualifiedName;
        }
        public List<Return> ReturnCommands { get { return this.getAllChildrenOf<Return>(); } }
        public bool AlwaysReturns
        {
            get
            {
                if (this.ReturnCommands.Count > 0)
                    return true;
                var codeBlocks = this.getAllChildrenOf<Interfaces.iCodeBlock>();
                foreach (var it in codeBlocks)
                {
                    if (it.AlwaysReturns)
                        return true;
                }
                return false;
            }
        }

        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            if (this.IsExternal)
                return;
            if (this.IsInline)
            {
                if (cfg == null)
                    cfg = new SqfConfigObjects.SqfConfigFile("inline");
                else
                    return;
            }


            int index;
            string tab = new string('\t', this.getAllParentsOf<Interfaces.iCodeBlock>().Count);
            if (this.IsVirtual)
                tab += '\t';
            if (!this.IsVirtual)
            {
                var fqn = this.Name.FullyQualifiedName;

                var filePath = Wrapper.Compiler.ProjectFile.OutputFolder + fqn.Replace("::", "\\") + this.SqfSuffix;
                var fileFolderPath = filePath.Substring(0, filePath.LastIndexOf('\\'));
                if (!Directory.Exists(fileFolderPath))
                    Directory.CreateDirectory(fileFolderPath);
                if(sw == null)
                    sw = new System.IO.StreamWriter(filePath + ".sqf");

                int lIndex = fqn.LastIndexOf("::") - fqn.Count(c => c == ':') / 2 + 1;
                fqn = fqn.Replace("::", "_") + this.SqfSuffix;
                string lPath;
                string rPath;
                if (lIndex <= 0)
                {
                    lPath = "UNCATEGORIZED";
                    rPath = fqn;
                    Logger.Instance.log(Logger.LogLevel.WARNING, "Function '" + this.Name.FullyQualifiedName + "' at line " + this.Name.Line + " has no namespace");
                }
                else
                {
                    lPath = fqn.Substring(0, lIndex);
                    rPath = fqn.Substring(lIndex + 1);
                }
                cfg.setValue(lPath + '/' + "All" + '/' + rPath + '/' + "file", '"' + filePath.Substring(Wrapper.Compiler.ProjectFile.OutputFolder.Length) + ".sqf" + '"');
                cfg.setValue(lPath + '/' + "All" + '/' + rPath + '/' + "preInit", this.Name.OriginalValue == "preInit" ? "1" : "0");
                cfg.setValue(lPath + '/' + "All" + '/' + rPath + '/' + "postInit", this.Name.OriginalValue == "postInit" ? "1" : "0");
                cfg.setValue(lPath + '/' + "All" + '/' + rPath + '/' + "preStart", this.Name.OriginalValue == "preStart" ? "1" : "0");
                cfg.setValue(lPath + '/' + "All" + '/' + rPath + '/' + "recompile", "0");
                cfg.setValue(lPath + '/' + "All" + '/' + rPath + '/' + "ext", '"' + ".sqf" + '"');
                cfg.setValue(lPath + '/' + "All" + '/' + rPath + '/' + "headerType", "-1");
            }
            sw.WriteLine(tab + "scopeName \"" + Wrapper.Compiler.ScopeNames.function + "\";");
            var argList = this.ArgListObjects;
            if (argList.Count > 0 || this.encapsulation != Encapsulation.Static)
            {
                sw.Write(tab + "params [");
                bool printComma = false;
                if (this.encapsulation != Encapsulation.Static && !this.IsConstructor)
                {
                    sw.Write('"' + Wrapper.Compiler.thisVariableName + '"');
                    printComma = true;
                }
                foreach(var it in argList)
                {
                    if (printComma)
                        sw.Write(", ");
                    if (it is Variable)
                    {
                        sw.Write('"' + ((Variable)it).SqfVariableName + '"');
                        printComma = true;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                sw.WriteLine("];");
            }
            #region constructor printing
            if (this.IsConstructor)
            {
                /////////////////////////////////
                // OBJECT CONSTRUCTOR PRINTING //
                /////////////////////////////////
                sw.WriteLine(tab + "///////////////////////");
                sw.WriteLine(tab + "// START CONSTRUCTOR //");
                sw.WriteLine(tab + "///////////////////////");
                var classObject = this.getFirstOf<oosClass>();
                var objects = classObject.AllObjects;
                if (objects.Count > 0)
                {
                    sw.Write(tab + "private [");
                    index = 0;
                    foreach (var it in objects)
                    {
                        if (it is Interfaces.iFunction && !((Interfaces.iFunction)it).IsVirtual)
                            continue;
                        if (!(it is Variable) || ((Variable)it).Value != null)
                        {
                            if (index > 0)
                                sw.Write(", ");
                            sw.Write("\"" + Wrapper.Compiler.thisVariableName + (it is Function ? "fnc" : "var") + ((Interfaces.iName)it).Name.OriginalValue.Replace("::", "_") + (it is Function ? this.SqfSuffix : "") + "\"");
                            index++;
                        }
                    }
                    sw.WriteLine("];");
                }
                foreach (var it in objects)
                {
                    if (it == this)
                        continue;
                    if (it is Interfaces.iFunction && !((Interfaces.iFunction)it).IsVirtual)
                        continue;
                    if (it is Function)
                    {
                        sw.Write(tab + Wrapper.Compiler.thisVariableName + (it is Function ? "fnc" : "var") + ((Interfaces.iName)it).Name.OriginalValue.Replace("::", "_") + (it is Function ? this.SqfSuffix + " = {\r\n" : " = "));
                        it.writeOut(sw, cfg);
                        sw.WriteLine(tab + "};");
                    }
                    else
                    {
                        var val = ((Variable)it).Value;
                        if (val != null)
                        {
                            sw.Write(tab + Wrapper.Compiler.thisVariableName + (it is Function ? "fnc" : "var") + ((Interfaces.iName)it).Name.OriginalValue.Replace("::", "_") + (it is Function ? this.SqfSuffix + " = {\r\n" : " = "));
                            val.writeOut(sw, cfg);
                            sw.WriteLine(";");
                        }
                    }
                }
                sw.Write(tab + Wrapper.Compiler.thisVariableName + " = [");

                
                var classIdents = classObject.ParentClassesIdents;
                classIdents.Add(classObject.Name);

                //Representing classes (InstanceOf reference)
                sw.Write("[");
                index = 0;
                foreach (var it in classIdents)
                {
                    if (index > 0)
                        sw.Write(", ");
                    if (it is Ident)
                    {
                        index++;
                        sw.Write('"' + ((Interfaces.iClass)((Ident)it).ReferencedObject).Name.FullyQualifiedName + '"');
                    }
                    else
                    {
                        throw new Exception("please report to developer, unknown exception happened in function object creation");
                    }
                }
                sw.Write("], [");
                //Lookup register
                index = 0;
                foreach (var child in objects)
                {
                    if (child is Interfaces.iFunction && !((Interfaces.iFunction)child).IsVirtual)
                        continue;
                    if (child is Variable)
                    {
                        var variable = (Variable)child;
                        if (variable.IsClassVariable)
                        {
                            if (index > 0)
                                sw.Write(", ");
                            sw.Write('"' + ((Variable)child).Name.OriginalValue + '"');
                            index++;
                        }
                    }
                    else if (child is Interfaces.iFunction)
                    {
                        if (index > 0)
                            sw.Write(", ");
                        var fnc = (Interfaces.iFunction)child;
                        sw.Write('"' + ((Interfaces.iFunction)child).Name.OriginalValue + '"');
                        index++;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                sw.Write("]");
                foreach (var child in objects)
                {
                    if (child is Interfaces.iFunction)
                    {
                        var fnc = (Interfaces.iFunction)child;
                        if (fnc.IsVirtual)
                        {
                            if(index > 0)
                                sw.Write(", ");
                            sw.Write(Wrapper.Compiler.thisVariableName + "fnc" + ((Function)child).Name.OriginalValue.Replace("::", "_") + this.SqfSuffix);
                            index ++;
                        }
                    }
                    else if (child is Variable)
                    {
                        var variable = (Variable)child;
                        if (variable.IsClassVariable)
                        {
                            sw.Write(", ");
                            if (variable.Value == null)
                                sw.Write("nil");
                            else
                                sw.Write(Wrapper.Compiler.thisVariableName + "var" + ((Variable)child).Name.OriginalValue.Replace("::", "_"));
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                sw.WriteLine("];");
                //ToDo: add baseconstructor calls
                sw.WriteLine(tab + "///////////////////////");
                sw.WriteLine(tab + "// END   CONSTRUCTOR //");
                sw.WriteLine(tab + "///////////////////////");
            }
            #endregion
            Logger.Instance.log(Logger.LogLevel.DEBUG, "Printing out function '" + this.Name.FullyQualifiedName + this.SqfSuffix + "'s body");
            HelperClasses.PrintCodeHelpers.printPrivateArray(this, tab, sw, cfg);
            HelperClasses.PrintCodeHelpers.printCodeLines(this.CodeInstructions, tab, sw, cfg);
            if (this.IsConstructor)
            {
                sw.Write(tab + Wrapper.Compiler.thisVariableName);
            }
            if (!this.IsVirtual)
            {
                sw.Flush();
                if (!this.IsInline)
                    sw.Close();
            }
        }
        public override List<pBaseLangObject> getScopeItems(int scopeIndex)
        {
            switch (scopeIndex)
            {
                case 0:
                    return this.ArgListObjects;
                case 1:
                    return this.CodeInstructions;
                default:
                    return this.children;
            }
        }
    }
}
