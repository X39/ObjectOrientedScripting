using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class NativeOperator : NativeInstruction
    {
        public string Operator { get; set; }

        public NativeOperator(pBaseLangObject parent, int line, int pos) : base(parent, line, pos)
        {
            VTO = null;
        }
        public override int doFinalize()
        {
            int errCount = 0;
            if (VTO.ident != null)
                errCount += VTO.ident.finalize();
            return errCount;
        }
    }
}
