using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class VirtualFunction : pBaseLangObject, Interfaces.iName
    {
        public Ident Name { get { return ((Ident)this.children[0]); } set { if (!value.IsSimpleIdentifier) throw new Ex.InvalidIdentType(value.getIdentType(), IdentType.Name); this.children[0] = value; } }
        public VarType functionVarType;
        public List<VarType> argTypes;


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
        public VirtualFunction(pBaseLangObject parent) : base(parent) 
        {
            argTypes = new List<VarType>();
            this.children.Add(null);
        }
        public override void doFinalize() { }
    }
}
