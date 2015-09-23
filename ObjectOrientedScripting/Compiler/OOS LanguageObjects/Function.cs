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
        private int argListEnd;
        public Encapsulation encapsulation;

        public List<pBaseLangObject> ArgList { get { return this.children.GetRange(1, argListEnd); } }
        public List<pBaseLangObject> CodeInstructions { get { return this.children.GetRange(argListEnd, this.children.Count - argListEnd); } }
        public bool IsConstructor { get { return this.Name.OriginalValue == ((Interfaces.iName)this.Parent).Name.OriginalValue; } }

        public string FullyQualifiedName { get { return this.Parent + "::" + this.Name.OriginalValue; } }
        public Function(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
            varType = null;
        }
        public override int doFinalize() { return 0; }
        public void markArgListEnd()
        {
            argListEnd = this.children.Count - 1;
        }
        public override string ToString()
        {
            return this.FullyQualifiedName;
        }
    }
}
