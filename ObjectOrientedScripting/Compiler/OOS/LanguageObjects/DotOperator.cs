
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class DotOperator : pBaseLangObject
    {

        public DotOperator(pBaseLangObject parent, int line, int pos, string file) : base(parent)
        {
            this.Line = line;
            this.Pos = pos;
            this.File = file;
        }

        public int Line { get; internal set; }
        public int Pos { get; internal set; }
        public string File { get; internal set; }
        public VarTypeObject ReferencedType { get; internal set; }

        public override int doFinalize()
        {
            return 0;
        }

        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            throw new NotImplementedException();
        }
    }
}
