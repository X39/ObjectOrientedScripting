using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class OosValue : BaseLangObject
    {
        string value;
        public string Value { get { return this.value; } set { this.value = value; } }
        public OosValue(string s)
        {
            this.value = s;
        }
    }
}
