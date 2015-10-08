using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Throw : pBaseLangObject
    {
        public Throw(pBaseLangObject parent) : base(parent) { }
        public override int doFinalize()
        {
            if (((Expression)this.children[0]).ReferencedType.varType != VarType.String)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0043, ((Expression)this.children[0]).Line, ((Expression)this.children[0]).Pos));
                return 1;
            }
            return 0;

        }
    }
}
