using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Case : pBaseLangObject
    {
        public pBaseLangObject expression { get { return this.children[0]; } set { this.children[0] = value; } }
        public pBaseLangObject endOfCase { get { return this.children[1]; } set { this.children[1] = value; } }
        public List<pBaseLangObject> CodeInstructions { get { return this.children.GetRange(2, this.children.Count - 2); } }

        private int line;
        public int Line { get { return this.line; } }
        private int pos;
        public int Pos { get { return this.pos; } }
        public Case(pBaseLangObject parent, int line, int pos) : base(parent)
        {
            this.children.Add(null);
            this.children.Add(null);
            this.line = line;
            this.pos = pos;
        }
        public override int doFinalize()
        {
            int errCount = 0;
            var switchElement = this.Parent.getFirstOf<Switch>();
            if (expression != null)
            {
                if (!((Expression)expression).ReferencedType.Equals(switchElement.ReferencedType))
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0044, this.line, this.pos));
                    errCount++;
                }
            }
            return errCount;
        }
    }
}
