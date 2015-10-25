using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Base : pBaseLangObject
    {
        public Base() : base(null) { }
        public virtual int finalize()
        {
            HelperClasses.NamespaceResolver.BaseClass = this;
            return base.finalize();
        }
        public override int doFinalize() { return 0; }
    }
}
