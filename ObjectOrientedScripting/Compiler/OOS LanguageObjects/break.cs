using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Break : pBaseLangObject
    {
        public Break(pBaseLangObject parent) : base(parent) {}
        virtual void doFinalize() {}
    }
}
