using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Value : pBaseLangObject
    {
        public VarType varType;
        public string value;
        public Value(pBaseLangObject parent) : base(parent) 
        {
            varType = VarType.Void;
            value = "";
        }
        public override int doFinalize() { return 0; }
        public override string ToString()
        {
            return this.value;
        }
    }
}
