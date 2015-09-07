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

        public List<pBaseLangObject> ArgList { get { return this.children.GetRange(0, argListEnd); } }
        public List<pBaseLangObject> CodeInstructions { get { return this.children.GetRange(argListEnd, this.children.Count - argListEnd); } }

        public string FullyQualifiedName
        {
            get
            {
                string s = "";
                List<Interfaces.iName> parentList = new List<Interfaces.iName>();
                pBaseLangObject curParent = Parent;
                while (curParent != null)
                {
                    if (curParent is Interfaces.iName)
                        parentList.Add((Interfaces.iName)curParent);
                    curParent = curParent.Parent;
                }
                parentList.Reverse();
                foreach (Interfaces.iName it in parentList)
                    s += it.Name;
                s += "_fnc_" + this.Name.OriginalValue;
                return s;
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
            argListEnd = this.children.Count;
        }
    }
}
