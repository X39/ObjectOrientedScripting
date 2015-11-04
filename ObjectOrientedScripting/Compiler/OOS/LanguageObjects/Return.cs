using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Return : pBaseLangObject
    {
        private int line;
        public int Line { get { return this.line; } }
        private int pos;
        public int Pos { get { return this.pos; } }
        public Return(pBaseLangObject parent, int line, int pos) : base(parent)
        {
            this.line = line;
            this.pos = pos;
        }
        public override int doFinalize()
        {
            int errCount = 0;
            var fnc = this.getFirstOf<Function>();
            if(this.children.Count > 0)
            {
                var returnExpression = (Expression)this.children[0];
                if(!returnExpression.ReferencedType.Equals(fnc.varType))
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0022, this.line, this.pos));
                    errCount++;
                }
            }
            else if(fnc.varType.varType != VarType.Void)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0023, this.line, this.pos));
                errCount++;
            }
            return errCount;
        }

        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            string tab = new string('\t', this.getAllParentsOf<Interfaces.iCodeBlock>().Count);
            sw.Write(tab);
            foreach(var it in this.children)
            {
                it.writeOut(sw, cfg);
            }
            sw.Write(" breakOut \"" + Wrapper.Compiler.ScopeNames.function + "\"");
        }
    }
}
