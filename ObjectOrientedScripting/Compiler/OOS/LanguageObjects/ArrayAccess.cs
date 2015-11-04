using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class ArrayAccess : pBaseLangObject
    {

        public ArrayAccess(pBaseLangObject parent) : base(parent) { }
        public override int doFinalize() { return 0; }
        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            throw new NotImplementedException();
        }
    }
}
