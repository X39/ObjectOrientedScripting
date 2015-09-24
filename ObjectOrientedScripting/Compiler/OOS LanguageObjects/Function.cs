using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Function : pBaseLangObject, Interfaces.iName
    {
        public Ident Name { get { return ((Ident)this.children[0]); } set { if (!value.IsSimpleIdentifier) throw new Ex.InvalidIdentType(value.getIdentType(), IdentType.Name); this.children[0] = value; } }
        public VarTypeObject varType;
        private int endMarker;
        public Encapsulation encapsulation;

        public List<pBaseLangObject> ArgList { get { return this.children.GetRange(1, endMarker); } }
        public List<pBaseLangObject> CodeInstructions { get { return this.children.GetRange(endMarker + 1, this.children.Count - (endMarker + 1)); } }
        public bool IsConstructor { get { if (this.Parent is Base) return false; return this.Name.OriginalValue == ((Interfaces.iName)this.Parent).Name.OriginalValue; } }
        public bool IsClassFunction { get { return this.encapsulation != Encapsulation.Static && this.encapsulation != Encapsulation.NA; } }

        public string FullyQualifiedName { get { return this.Parent + "::" + this.Name.OriginalValue; } }
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
                    return fqn.Insert(fqn.LastIndexOf("::"), "_fnc").Replace("::", "_").Substring(1);
                }
                else
                {
                    var casted = (Interfaces.iGetFunctionIndex)this.Parent;
                    var res = casted.getFunctionIndex(this.Name);
                    return " select 1 select " + res.Item1 + " select " + res.Item2;
                }
            }
        }
        public Function(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
            varType = null;
        }
        public override int doFinalize() { return 0; }
        public void markArgListEnd()
        {
            endMarker = this.children.Count - 1;
        }
        public override string ToString()
        {
            return this.FullyQualifiedName;
        }
    }
}
