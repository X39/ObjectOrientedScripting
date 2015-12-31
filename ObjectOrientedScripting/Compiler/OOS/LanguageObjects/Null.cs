using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Null : pBaseLangObject, Interfaces.iHasType
    {
        public int Line { get; internal set; }
        public int Pos { get; internal set; }
        public string File { get; internal set; }
        public VarTypeObject ReferencedType { get; internal set; }
        public Null(pBaseLangObject parent, int line, int pos, string file) : base(parent)
        {
            this.Line = line;
            this.Pos = pos;
            this.File = file;
            ReferencedType = new VarTypeObject(VarType.NullObject);
        }
        public override int doFinalize()
        {
            return 0;
        }

        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            throw new Exception();
        }


    }
}
