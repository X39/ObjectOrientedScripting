using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class VariableAssignment : pBaseLangObject, Interfaces.iName, Interfaces.iHasType
    {
        public Ident Name { get { return ((Interfaces.iName)Parent).Name; } set { ((Interfaces.iName)Parent).Name = value; } }
        public string operation;
        public VarTypeObject ReferencedType
        {
            get
            {
                if (this.children.Count == 0)
                    return null;
                if (this.children[0] is Interfaces.iHasType)
                    return ((Interfaces.iHasType)this.children[0]).ReferencedType;
                else
                    return null;
            }
        }

        public VariableAssignment(pBaseLangObject parent) : base(parent) {}
        public override int doFinalize() { return 0; }
    }
}
