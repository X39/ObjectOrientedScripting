using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class OosIfElse : BaseLangObject
    {
        int lastIfInstruction;

        public BaseLangObject Expression { get { return this.Children[0]; } set { this.Children[0] = value; if(value != null) value.setParent(this); } }
        private List<BaseLangObject> IfInstructions { get { return this.Children.GetRange(1, lastIfInstruction - 1); } }
        private List<BaseLangObject> ElseInstructions { get { return this.Children.GetRange(lastIfInstruction, this.Children.Count - lastIfInstruction); } }

        public OosIfElse()
        {
            lastIfInstruction = 1;
            this.addChild(null);
        }
        public void markEnd()
        {
            lastIfInstruction = this.Children.Count - lastIfInstruction;
        }

    }
}
