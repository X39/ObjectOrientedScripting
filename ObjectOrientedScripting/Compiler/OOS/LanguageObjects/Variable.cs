using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Variable : pBaseLangObject, Interfaces.iName, Interfaces.iHasType, Interfaces.iTemplate
    {
        public Ident Name { get { return ((Ident)this.children[0]); } set { this.children[0] = value; } }
        public VarTypeObject varType;
        public VarTypeObject ReferencedType { get { return this.varType; } }
        public Encapsulation encapsulation;
        public pBaseLangObject Value { get { var valAssign = this.getAllChildrenOf<VariableAssignment>(); if (valAssign.Count > 0) return valAssign[0]; return null; } }
        public bool IsClassVariable { get { return this.encapsulation != Encapsulation.Static && this.encapsulation != Encapsulation.NA; } }
        public Template TemplateObject { get { return this.varType.TemplateObject; } set { this.varType.TemplateObject = value; } }
        public string SqfVariableName
        {
            get
            {
                if (this.encapsulation == Encapsulation.NA)
                {
                    if (this.Name.OriginalValue == Wrapper.Compiler.thisVariableName)
                        return Wrapper.Compiler.thisVariableName;
                    else
                        return "_" + this.Name.OriginalValue;
                }
                else if (this.encapsulation == Encapsulation.Static)
                {
                    return "GVAR_" + this.Name.FullyQualifiedName.Replace("::", "_");
                }
                else
                {
                    var casted = (Interfaces.iGetIndex)this.Parent;
                    var res = casted.getIndex(this.Name);
                    return (res + Function.ObjectValueOffset).ToString();
                }
            }
        }

        public int Line { get; internal set; }
        public int Pos { get; internal set; }
        public string File { get; internal set; }

        public Variable(pBaseLangObject parent, int line, int pos, string file) : base(parent)
        {
            this.addChild(null);
            varType = null;
            this.Line = line;
            this.Pos = pos;
            this.File = file;
        }
        public override int finalize()
        {
            if (this.IsFinalized)
                return 0;
            int errCount = 0;
            foreach (pBaseLangObject blo in children)
                if (blo != null)
                    errCount += blo.finalize();
            errCount += this.doFinalize();
            if (this is Interfaces.iTemplate && ((Interfaces.iTemplate)this).TemplateObject != null)
                errCount += ((Interfaces.iTemplate)this).TemplateObject.doFinalize();
            if (this is Interfaces.iHasType && ((Interfaces.iHasType)this).ReferencedType.IsObject)
                errCount += ((Interfaces.iHasType)this).ReferencedType.ident.finalize();
            this.IsFinalized = true;
            return errCount;
        }
        public override int doFinalize()
        {
            int errCount = 0;
            var assignList = this.getAllChildrenOf<VariableAssignment>();
            //Make sure that we not got an auto without an assign here
            if (this.varType.varType == VarType.Auto && assignList.Count == 0 && !(this.Parent is ForEach))
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0007, this.Line, this.Pos, this.File));
                errCount++;
            }
            //Check if we got an assign and validate the value after that
            if (assignList.Count > 0)
            {
                var assign = assignList[0];
                var assignType = assign.ReferencedType;
                var thisType = this.varType;

                if (thisType.varType == VarType.Auto)
                {
                    thisType.varType = assignType.varType;
                    thisType.ident = assignType.ident;
                    thisType.TemplateObject = assignType.TemplateObject;
                }
                else
                {
                    if (!assignType.Equals(thisType))
                    {
                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0008, this.Line, this.Pos, this.File));
                        errCount++;
                    }
                }
            }
            else if (this.varType.varType == VarType.Auto && this.Parent is ForEach)
            {
                var obj = (ForEach)this.Parent;
                var thisType = this.varType;
                if (obj.Variable.LastIdent.ReferencedType.Equals(HelperClasses.NamespaceResolver.createNSR("::array")))
                {
                    thisType.varType = obj.Variable.LastIdent.ReferencedType.TemplateObject.vtoList[0].varType;
                    thisType.ident = obj.Variable.LastIdent.ReferencedType.TemplateObject.vtoList[0].ident;
                    thisType.TemplateObject = obj.Variable.LastIdent.ReferencedType.TemplateObject.vtoList[0].TemplateObject;
                }
                else
                {
                    thisType.varType = obj.Variable.LastIdent.ReferencedType.varType;
                    thisType.ident = obj.Variable.LastIdent.ReferencedType.ident;
                    thisType.TemplateObject = obj.Variable.LastIdent.ReferencedType.TemplateObject;
                    Logger.Instance.log(Logger.LogLevel.ERROR, "Currently only the array object is allowed for foreach");
                    errCount++;
                }
            }
            //Check variable is not yet existing in above scopes
            switch (this.encapsulation)
            {
                case Encapsulation.NA:
                    {
                        var fnc = this.getFirstOf<Interfaces.iFunction>();
                        if (fnc != null)
                        {
                            bool flag = false;
                            foreach (var it in ((pBaseLangObject)fnc).getAllChildrenOf<Variable>(true, this))
                            {
                                if (it.Name.FullyQualifiedName == this.Name.FullyQualifiedName)
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (flag)
                            {
                                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0009, this.Line, this.Pos, this.File));
                                errCount++;
                            }
                        }
                        var cls = this.getFirstOf<Interfaces.iClass>();
                        if (cls != null && (fnc == null || fnc.FunctionEncapsulation != Encapsulation.Static))
                        {
                            bool flag = false;
                            foreach (var it in ((pBaseLangObject)cls).getAllChildrenOf<Variable>())
                            {
                                if (it.Name.FullyQualifiedName == this.Name.FullyQualifiedName)
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (flag)
                            {
                                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0051, this.Line, this.Pos, this.File));
                                errCount++;
                            }
                        }
                    }
                    break;
                default:
                    {
                        var classRef = this.getFirstOf<Interfaces.iClass>();
                        if (classRef != null)
                        {
                            if (!((pBaseLangObject)classRef).getAllChildrenOf<Variable>(true, this).TrueForAll(it => it.Name.FullyQualifiedName != this.Name.FullyQualifiedName))
                            {
                                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0010, this.Line, this.Pos, this.File));
                                errCount++;
                            }
                        }
                        else
                        {
                            var topObject = this.getFirstOf<Base>();
                            if (!topObject.getAllChildrenOf<Variable>(true, this).TrueForAll(it => it.Name.FullyQualifiedName != this.Name.FullyQualifiedName))
                            {
                                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0011, this.Line, this.Pos, this.File));
                                errCount++;
                            }
                        }
                    }
                    break;
            }
            return errCount;
        }
        public override string ToString()
        {
            return "var->" + this.Name.FullyQualifiedName;
        }

        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            if (sw == null)
                return;
            if (this.children.Count == 0)
                return;
            string tab = new string('\t', this.getAllParentsOf<Interfaces.iCodeBlock>().Count);
            var assignList = this.getAllChildrenOf<VariableAssignment>();
            sw.Write(tab + this.SqfVariableName);
            if (assignList.Count > 0)
            {
                sw.Write(" = ");
                assignList[0].writeOut(sw, cfg);
            }
        }
    }
}
