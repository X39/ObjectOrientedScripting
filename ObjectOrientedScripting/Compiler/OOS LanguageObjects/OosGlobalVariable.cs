using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class OosClassVariable : BaseVariableObject
    {
        ClassEncapsulation encapsulation;
        public ClassEncapsulation Encapsulation { get { return encapsulation; } set { encapsulation = value; } }
    }
}
