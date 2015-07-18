using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class OosIfElse : BaseLangObject
    {
        int ifInstructionCount;

        public BaseLangObject Expression { get { return this.Children[0]; } set { this.Children[0] = value; if (value != null) value.setParent(this); } }
        public List<BaseLangObject> IfInstructions { get { return this.Children.GetRange(1, ifInstructionCount); } }
        public List<BaseLangObject> ElseInstructions { get { return this.Children.GetRange(ifInstructionCount + 1, this.Children.Count - (ifInstructionCount + 1)); } }
        public bool HasElse { get { return this.ElseInstructions.Count > 0; } }

        public OosIfElse()
        {
            ifInstructionCount = 1;
            this.addChild(null);
        }
        public void markEnd()
        {
            ifInstructionCount = this.Children.Count - 1;
        }

    }
}
