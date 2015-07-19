using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class OosNative : BaseLangObject
    {
        public string nativeCode;

        public OosNative()
        {
            nativeCode = "";
        }
    }
}
