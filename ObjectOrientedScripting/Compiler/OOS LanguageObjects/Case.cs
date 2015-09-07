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

        public Case(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
            this.children.Add(null);
        }
        public override int doFinalize() { return 0; }
    }
}
