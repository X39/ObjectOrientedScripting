using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Function : pBaseLangObject, Interfaces.iName, Interfaces.iFunction
    {
        public Ident Name { get { return ((Ident)this.children[0]); } set { this.children[0] = value; } }
        public VarTypeObject varType;
        private int endMarker;
        public Encapsulation encapsulation;

        private List<pBaseLangObject> ArgListObjects { get { return this.children.GetRange(1, endMarker); } }
        public List<pBaseLangObject> CodeInstructions { get { return this.children.GetRange(endMarker + 1, this.children.Count - (endMarker + 1)); } }
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
            return errCount;
        }
        public void markArgListEnd()
        {
            endMarker = this.children.Count - 1;
        }
        public override string ToString()
        {
            return "fnc->" + this.Name.FullyQualifiedName;
        }
    }
}
