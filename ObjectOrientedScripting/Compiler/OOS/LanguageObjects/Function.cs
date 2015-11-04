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
        public Ident Name { get { return ((Ident)this.children[0]); } set { this.children[0] = value; } }
        public VarTypeObject varType;
        private int endMarker;
        private int endMarker2;
        public Encapsulation encapsulation;

        private List<pBaseLangObject> ArgListObjects { get { return this.children.GetRange(1, endMarker); } }
        private List<pBaseLangObject> BaseCalls { get { if (endMarker + 1 - endMarker2 == 0) return new List<pBaseLangObject>(); return this.children.GetRange(endMarker + 1, endMarker2); } }
        public List<pBaseLangObject> CodeInstructions { get { return this.children.GetRange(endMarker2 + 1, this.children.Count - (endMarker2 + 1)); } }
        public bool IsConstructor { get { if (this.Parent is Base) return false; return this.Name.OriginalValue == ((Interfaces.iName)this.Parent).Name.OriginalValue; } }
        private bool isOverride;
        public bool Override { get { return this.isOverride; } set { this.isOverride = value; } }
        public bool IsClassFunction { get { return this.encapsulation != Encapsulation.Static && this.encapsulation != Encapsulation.NA; } }

        public bool IsAsync { get; set; }

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
                else if (this.encapsulation == Encapsulation.Static || this.IsConstructor)
                {
                    string fqn = this.Name.FullyQualifiedName;
                    if (this.IsConstructor)
                        fqn += "::" + this.Name.OriginalValue;
                    fqn = fqn.Insert(fqn.LastIndexOf("::"), "_fnc").Replace("::", "_").Substring(1);
                    return fqn.StartsWith("fnc_") ? "Generic_" + fqn : fqn;
                }
                else
                {
                    var casted = (Interfaces.iGetFunctionIndex)this.Parent;
                    var res = casted.getFunctionIndex(this.Name);
                    return " select 1 select " + res.Item1 + " select 1 select " + res.Item2;
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
        }
        public override int doFinalize()
        {
            int errCount = 0;
            if (this.ReturnType.varType != VarType.Void && !this.AlwaysReturns && !this.IsConstructor)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0021, this.Name.Line));
                errCount++;
            }
            if(this.IsConstructor)
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
            int index;
            {
                var fqn = this.Name.FullyQualifiedName;

                var filePath = Wrapper.Compiler.ProjectFile.OutputFolder + fqn.Replace("::", "\\");
                var fileFolderPath = filePath.Substring(0, filePath.LastIndexOf('\\'));
                if (!Directory.Exists(fileFolderPath))
                    Directory.CreateDirectory(fileFolderPath);
                sw = new System.IO.StreamWriter(filePath);

                fqn = fqn.Replace("::", "_");
                var lPath = fqn.Substring(0, fqn.LastIndexOf('_'));
                var rPath = fqn.Substring(fqn.LastIndexOf('_') + 1);
                cfg.setValue(lPath + '/' + "All" + '/' + rPath + '/' + "file", '"' + filePath.Substring(Wrapper.Compiler.ProjectFile.OutputFolder.Length) + '"');
                cfg.setValue(lPath + '/' + "All" + '/' + rPath + '/' + "preInit", this.Name.OriginalValue == "preInit" ? "1" : "0");
                cfg.setValue(lPath + '/' + "All" + '/' + rPath + '/' + "postInit", this.Name.OriginalValue == "postInit" ? "1" : "0");
                cfg.setValue(lPath + '/' + "All" + '/' + rPath + '/' + "preStart", this.Name.OriginalValue == "preStart" ? "1" : "0");
                cfg.setValue(lPath + '/' + "All" + '/' + rPath + '/' + "recompile", "0");
                cfg.setValue(lPath + '/' + "All" + '/' + rPath + '/' + "ext", '"' + ".sqf" + '"');
                cfg.setValue(lPath + '/' + "All" + '/' + rPath + '/' + "headerType", "-1");
            }
            sw.WriteLine("scopeName \"" + Wrapper.Compiler.ScopeNames.function + "\";");
            var argList = this.ArgListObjects;
            if (argList.Count > 0)
            {
                sw.Write("params [");
                for (int i = 0; i < argList.Count; i++)
                {
                    var it = argList[i];
                    if (i != 0)
                    {
                        sw.Write(", ");
                    }
                    if(it is Variable)
                    {
                        sw.Write('"' + ((Variable)it).SqfVariableName + '"');
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                sw.WriteLine("];");
            }
            var localVarList = this.getAllChildrenOf<Variable>(false, null, -1, 1);
            if (localVarList.Count > 0)
            {
                if (localVarList.Count == 1)
                    sw.Write("private ");
                else
                    sw.Write("private [");

                for (int i = 0; i < localVarList.Count; i++)
                {
                    var it = localVarList[i];
                    if (i != 0)
                    {
                        sw.Write(", ");
                    }
                    if (it is Variable)
                    {
                        sw.Write('"' + ((Variable)it).SqfVariableName + '"');
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                if (localVarList.Count > 1)
                    sw.Write("]");
                sw.WriteLine(";");
            }
            if (this.IsConstructor)
            {
                /////////////////////////////////
                // OBJECT CONSTRUCTOR PRINTING //
                /////////////////////////////////
                var classObject = this.getFirstOf<oosClass>();
                var varList = classObject.AllVariables;
                var fncList = classObject.AllFunctions;
                if (varList.Count > 0 || fncList.Count > 0)
                {
                    sw.Write("private [");
                    index = 0;
                    foreach (var it in varList)
                    {
                        if (index > 0)
                            sw.Write(",");
                        sw.Write("\"" + Wrapper.Compiler.thisVariableName + "var" + it.Name.OriginalValue.Replace("::", "_") + "\"");
                        index++;
                    }
                    foreach (var it in fncList)
                    {
                        if (((Function)it).IsConstructor)
                            continue;
                        if (index > 0)
                            sw.Write(",");
                        sw.Write("\"" + Wrapper.Compiler.thisVariableName + "fnc" + it.Name.OriginalValue.Replace("::", "_") + "\"");
                        index++;
                    }
                    sw.WriteLine("];");
                }
                sw.WriteLine("//Object '" + this.Name + "' variables");
                foreach (var it in varList)
                {
                    sw.Write(Wrapper.Compiler.thisVariableName + "var" + it.Name.OriginalValue.Replace("::", "_") + " = [");
                    var val = it.Value;
                    if (val == null)
                        sw.Write("nil");
                    else
                        val.writeOut(sw, cfg);
                    sw.WriteLine("];");
                }
                sw.WriteLine("//Object '" + this.Name + "' functions");
                foreach (var it in fncList)
                {
                    if (((Function)it).IsConstructor)
                        continue;
                    sw.WriteLine(Wrapper.Compiler.thisVariableName + "fnc" + it.Name.OriginalValue.Replace("::", "_") + " = {");
                    it.writeOut(sw, cfg);
                    sw.WriteLine("};");
                }
                sw.Write(Wrapper.Compiler.thisVariableName + " = [");

                //Write out parents info
                var classIdents = classObject.ParentClassesIdents;
                classIdents.Add(classObject.Name);

                sw.Write("[");
                index = 0;
                foreach (var it in classIdents)
                {
                    if (index > 0)
                        sw.Write(",");
                    if (it is Ident)
                    {
                        index++;
                        sw.Write('"' + ((Ident)it).FullyQualifiedName + '"');
                    }
                    else
                    {
                        throw new Exception("please report to developer, unknown exception happened in function object creation");
                    }
                }
                sw.Write("],[");
                index = 0;
                foreach (var it in classIdents)
                {
                    if (index > 0)
                        sw.Write(",");
                    if (it is Ident)
                    {
                        var refObj = ((Ident)it).ReferencedObject;
                        if (refObj is oosClass)
                        {
                            index++;
                            var cObj = (oosClass)refObj;
                            int index2 = 0;
                            //LookupRegister
                            sw.Write("[[");
                            foreach (var child in cObj.children)
                            {
                                if (index2 > 0)
                                    sw.Write(",");
                                if (child is Function)
                                {
                                    if (((Function)child).IsClassFunction && !((Function)child).IsConstructor)
                                        sw.Write('"' + ((Function)child).Name.FullyQualifiedName + '"');
                                    else
                                        sw.Write("nil");
                                }
                                else if (child is VirtualFunction)
                                {
                                    sw.Write('"' + ((VirtualFunction)child).Name.FullyQualifiedName + '"');
                                }
                                else if (child is Variable)
                                {

                                    if (((Variable)child).IsClassVariable)
                                        sw.Write('"' + ((Variable)child).Name.FullyQualifiedName + '"');
                                    else
                                        sw.Write("nil");
                                }
                                else
                                {
                                    sw.Write("nil");
                                }
                                index2++;
                            }
                            sw.Write("],[");
                            index2 = 0;
                            foreach (var child in cObj.children)
                            {
                                if (index2 > 0)
                                    sw.Write(",");
                                if (child is Function)
                                {
                                    if (((Function)child).IsClassFunction && !((Function)child).IsConstructor)
                                        sw.Write(Wrapper.Compiler.thisVariableName + "fnc" + ((Function)child).Name.OriginalValue.Replace("::", "_"));
                                    else
                                        sw.Write("nil");
                                }
                                else if (child is VirtualFunction)
                                {
                                    sw.Write(Wrapper.Compiler.thisVariableName + "var" + ((VirtualFunction)child).Name.OriginalValue.Replace("::", "_"));
                                }
                                else if (child is Variable)
                                {

                                    if (((Variable)child).IsClassVariable)
                                        sw.Write(Wrapper.Compiler.thisVariableName + "var" + ((Variable)child).Name.OriginalValue.Replace("::", "_"));
                                    else
                                        sw.Write("nil");
                                }
                                else
                                {
                                    sw.Write("nil");
                                }
                                index2++;
                            }
                            sw.Write("],[");
                            sw.Write('"' + ((Ident)it).FullyQualifiedName + '"');
                            sw.Write("]]");
                        }
                        else if (refObj is oosInterface)
                        {
                            index++;
                            sw.Write("nil");
                        }
                        else
                        {
                            throw new Exception("please report to developer, unknown exception happened in function object creation cast refObj");
                        }
                    }
                    else
                    {
                        throw new Exception("Function has Encapsulation.NA on encapsulation field, please report to developer");
                    }
                }
                sw.Write("],");
                //Current active class
                sw.Write("nil,");
                //Reserved for future meta informations
                sw.Write("[]");

                sw.WriteLine("];");
                //ToDo: add baseconstructor calls
                sw.WriteLine("//Actual constructor starts here, we just printed the object till here :)");
            }
            foreach (var it in this.CodeInstructions)
            {
                it.writeOut(sw, cfg);
            }
            if (this.IsConstructor)
            {
                sw.Write(Wrapper.Compiler.thisVariableName);
            }
            sw.Flush();
            sw.Close();
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
