using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Variable : pBaseLangObject, Interfaces.iName
    {
        public Ident Name
        {
            get { return ((Ident)this.children[0]); }
            set
            {
                if (this.encapsulation == Encapsulation.NA && !value.IsSimpleIdentifier)
                    throw new Ex.InvalidIdentType(value.getIdentType(), IdentType.Name);
                this.children[0] = value;
            }
        }
        public VarType varType;
        public Encapsulation encapsulation;
        public string FullyQualifiedName
        {
            get
            {
                if (this.encapsulation == Encapsulation.NA)
                    return this.Name.OriginalValue;
                string s = "";
                List<Interfaces.iName> parentList = new List<Interfaces.iName>();
                pBaseLangObject curParent = Parent;
                while (curParent != null)
                {
                    if(curParent is Interfaces.iName)
                        parentList.Add((Interfaces.iName)curParent);
                    curParent = curParent.Parent;
                }
                parentList.Reverse();
                foreach (Interfaces.iName it in parentList)
                    s += it.Name.OriginalValue;
                s += this.Name.OriginalValue;
                return s;
            }
        }

        public Variable(pBaseLangObject parent) : base(parent)
        {
            this.addChild(null);
        }
        public override void doFinalize() { }
    }
}
