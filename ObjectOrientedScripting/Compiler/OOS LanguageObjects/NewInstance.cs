using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class NewInstance : pBaseLangObject, Interfaces.iName, Interfaces.iHasType, Interfaces.iHasObject
    {

        public Ident Name { get { return ((Ident)this.children[0]); } set { this.children[0] = value; } }
        public string FullyQualifiedName { get { return ((Ident)this.children[0]).FullyQualifiedName; } }
        public VarTypeObject ReferencedType { get { return ((Ident)this.children[0]).ReferencedType; } }
        public pBaseLangObject ReferencedObject { get { return ((Ident)this.children[0]).ReferencedObject; } }
        public NewInstance(pBaseLangObject parent) : base(parent)
        {
            this.addChild(null);
        }
        public override int doFinalize() { return 0; }
    }
}
