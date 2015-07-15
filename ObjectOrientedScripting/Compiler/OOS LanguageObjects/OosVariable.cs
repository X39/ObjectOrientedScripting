using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class OosVariable : BaseLangObject, Interfaces.iName
    {
        string name;
        public string Name { get { return this.name; } set { this.name = value; } }
        public OosVariable(string s)
        {
            this.name = s;
        }
    }
}
