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
        public Template template { get { return this.varType.template; } set { this.varType.template = value; } }
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
                    return "GLOBAL" + this.Name.FullyQualifiedName.Replace("::", "_");
                }
                else
                {
                    var casted = (Interfaces.iGetVariableIndex)this.Parent;
                    var res = casted.getVariableIndex(this.Name);
                    return " select 1 select " + res.Item1 + " select 1 select " + res.Item2;
                }
            }
        }

        private int line;
        public int Line { get { return this.line; } }
        private int pos;
        public int Pos { get { return this.pos; } }

        public Variable(pBaseLangObject parent, int pos, int line) : base(parent)
        {
            this.addChild(null);
            varType = null;
            this.line = line;
            this.pos = pos;
        }
        public override int doFinalize() {
            int errCount = 0;
            return errCount;
        }
        public override string ToString()
        {
            return "var->" + this.Name.FullyQualifiedName;
        }
    }
}
