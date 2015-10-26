using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class NewArray : pBaseLangObject, Interfaces.iHasType
    {

        public NewArray(pBaseLangObject parent) : base(parent)
        {
            this.referencedType = null;
        }
        private VarTypeObject referencedType;
        public VarTypeObject ReferencedType { get { return this.referencedType; } }
        public override int doFinalize()
        {
            int errCount = 0;
            foreach(var it in this.children)
            {
                if(it is Expression)
                {
                    Expression exp = (Expression)it;
                    if (referencedType == null)
                        this.referencedType = exp.ReferencedType;
                    if (!referencedType.Equals(exp.ReferencedType))
                    {
                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0012, exp.Line, exp.Pos));
                        errCount++;
                    }
                }
                else
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.UNKNOWN));
                    errCount++;
                }
            }
            return errCount;
        }
    }
}
