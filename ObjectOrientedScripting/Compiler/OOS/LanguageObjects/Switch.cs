using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Switch : pBaseLangObject, Interfaces.iHasType
    {
        public pBaseLangObject expression { get { return this.children[0]; } set { this.children[0] = value; } }
        public List<pBaseLangObject> CodeInstructions { get { return this.children.GetRange(1, this.children.Count - 1); } }
        public VarTypeObject ReferencedType { get { return ((Expression)expression).ReferencedType; } }
        public Switch(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
        }
        public override int doFinalize()
        {
            int errCount = 0;
            //var caseList = this.getAllChildrenOf<Case>();
            //bool flag = false;
            //foreach (var it in caseList)
            //{
            //    if (it.expression == null)
            //    {
            //        if (flag)
            //        {
            //            Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0045, it.Line, it.Pos));
            //            errCount++;
            //        }
            //        else
            //        {
            //            flag = true;
            //        }
            //    }
            //}
            return errCount;
        }
    }
}
