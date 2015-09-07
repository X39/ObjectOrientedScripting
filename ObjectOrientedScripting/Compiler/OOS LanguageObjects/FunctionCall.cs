using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class FunctionCall : pBaseLangObject, Interfaces.iName
    {
        public Ident Name { get { return ((Interfaces.iName)Parent).Name; } set { ((Interfaces.iName)Parent).Name = value; } }
        public string FullyQualifiedName { get { return ((Interfaces.iName)Parent).FullyQualifiedName; } }

        public FunctionCall(pBaseLangObject parent) : base(parent) {}
        public override int doFinalize() { return 0; }
    }
}
