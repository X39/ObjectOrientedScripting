using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class TryCatch : pBaseLangObject
    {
        public VarType functionVarType;
        private int endMarker;

        public pBaseLangObject variable { get { return this.children[0]; } set { this.children[0] = value; } }
        public List<pBaseLangObject> TryInstructions { get { return this.children.GetRange(1, endMarker); } }
        public List<pBaseLangObject> CatchInstructions { get { return this.children.GetRange(endMarker + 1, this.children.Count - (endMarker + 1)); } }

        public TryCatch(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
        }
        public override int doFinalize()
        {
            if (((Variable)variable).ReferencedType.varType != VarType.String)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0042, ((Variable)variable).Line, ((Variable)variable).Pos));
                return 1;
            }
            return 0;
        }
        public void markIfEnd()
        {
            endMarker = this.children.Count - 1;
        }
    }
}
