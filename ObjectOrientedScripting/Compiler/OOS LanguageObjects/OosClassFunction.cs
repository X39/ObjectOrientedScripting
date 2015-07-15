using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class OosClassFunction : BaseFunctionObject
    {
        ClassEncapsulation encapsulation;
        public ClassEncapsulation Encapsulation { get { return encapsulation; } set { encapsulation = value; } }
        bool overrideBase;
        public bool OverrideBase { get { return this.overrideBase; } set { this.overrideBase = value; } }
    }
}
