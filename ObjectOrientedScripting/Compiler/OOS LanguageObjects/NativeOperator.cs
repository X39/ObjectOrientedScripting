using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class NativeOperator : NativeInstruction
    {
        private int line;
        public int Line { get { return this.line; } }
        private int pos;
        public int Pos { get { return this.pos; } }
        public VarTypeObject varType;
        private Ident name;

        public string Operator { get; set; }

        public NativeOperator(pBaseLangObject parent, int line, int pos) : base(parent, line, pos)
        {
            varType = null;
        }
        public override int doFinalize()
        {
            int errCount = 0;
            errCount += name.finalize();
            if (varType.ident != null)
                errCount += varType.ident.finalize();
            return errCount;
        }
    }
}
