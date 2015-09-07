using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Throw : pBaseLangObject
    {
        public Throw(pBaseLangObject parent) : base(parent) { }
        public override int doFinalize() { return 0; }
    }
}
