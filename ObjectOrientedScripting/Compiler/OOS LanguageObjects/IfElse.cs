using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class IfElse : pBaseLangObject
    {
        public VarType functionVarType;
        private int endMarker;

        public pBaseLangObject expression { get { return this.children[0]; } set { this.children[0] = value; } }
        public bool HasElse { get { return this.ElseInstructions.Count > 0; } }
        public List<pBaseLangObject> IfInstructions { get { return this.children.GetRange(1, endMarker); } }
        public List<pBaseLangObject> ElseInstructions { get { return this.children.GetRange(endMarker + 1, this.children.Count - (endMarker + 1)); } }

        public IfElse(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
        }
        public override int doFinalize() { return 0; }
        public void markIfEnd()
        {
            endMarker = this.children.Count - 1;
        }
    }
}
