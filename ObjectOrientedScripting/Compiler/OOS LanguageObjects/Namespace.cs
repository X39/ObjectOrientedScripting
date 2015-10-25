using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Namespace : pBaseLangObject, Interfaces.iName
    {
        public Ident Name { get { return ((Ident)this.children[0]); } set { this.children[0] = value; } }
        public Namespace(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
        }
        public override int doFinalize() { return 0; }
        public override string ToString()
        {
            return "ns->" + this.Name.FullyQualifiedName;
        }
    }
}
