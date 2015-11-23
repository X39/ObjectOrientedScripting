using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Value : pBaseLangObject
    {
        public VarTypeObject varType;
        public string value;
        public Value(pBaseLangObject parent) : base(parent) 
        {
            varType = new VarTypeObject(VarType.Void);
            value = "";
        }
        public override int doFinalize() { return 0; }
        public override string ToString()
        {
            return this.value;
        }
        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            sw.Write(this.value);
        }
    }
}
