using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Switch : pBaseLangObject
    {
        public pBaseLangObject expression { get { return this.children[0]; } set { this.children[0] = value; } }
        public List<pBaseLangObject> CodeInstructions { get { return this.children.GetRange(1, this.children.Count - 1); } }

        public Switch(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
        }
        public override void doFinalize() { }
    }
}
