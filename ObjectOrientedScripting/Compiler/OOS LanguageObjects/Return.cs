using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Return : pBaseLangObject
    {
        public Return(pBaseLangObject parent) : base(parent) { }
        public override void doFinalize() { }
    }
}
