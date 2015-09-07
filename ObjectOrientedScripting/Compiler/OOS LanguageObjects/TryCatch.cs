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
        private int ifEnd;

        public pBaseLangObject variable { get { return this.children[0]; } set { this.children[0] = value; } }
        public List<pBaseLangObject> TryInstructions { get { return this.children.GetRange(1, ifEnd); } }
        public List<pBaseLangObject> CatchInstructions { get { return this.children.GetRange(ifEnd, this.children.Count - ifEnd); } }

        public TryCatch(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
        }
        public override int doFinalize() { return 0; }
        public void markIfEnd()
        {
            ifEnd = this.children.Count;
        }
    }
}
