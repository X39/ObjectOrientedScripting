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
            if (!((Expression)this.children[0]).ReferencedType.IsKindOf(Wrapper.Compiler.InternalObjectVarTypes.VT_Exception))
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0057, ((Expression)this.children[0]).Line, ((Expression)this.children[0]).Pos));
                return 1;
            }
            return 0;
        }

        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            string tab = new string('\t', this.getAllParentsOf<Interfaces.iCodeBlock>().Count);
            sw.Write(tab + "throw ");
            foreach(var it in this.children)
            {
                it.writeOut(sw, cfg);
            }
        }
    }
}
