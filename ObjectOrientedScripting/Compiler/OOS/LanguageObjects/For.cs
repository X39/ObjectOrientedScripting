using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class For : pBaseLangObject, Interfaces.iCodeBlock
    {
        public pBaseLangObject forArg1 { get { return this.children[0]; } set { this.children[0] = value; } }
        public pBaseLangObject forArg2 { get { return this.children[1]; } set { this.children[1] = value; } }
        public pBaseLangObject forArg3 { get { return this.children[2]; } set { this.children[2] = value; } }
        public List<pBaseLangObject> CodeInstructions { get { return this.children.GetRange(3, this.children.Count - 3); } }

        public For(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
            this.children.Add(null);
            this.children.Add(null);
        }
        public override int doFinalize() { return 0; }

        public List<Return> ReturnCommands
        {
            get { return this.getAllChildrenOf<Return>(); }
        }
    }
}
