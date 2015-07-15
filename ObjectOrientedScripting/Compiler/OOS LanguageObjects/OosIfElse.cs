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

        public BaseLangObject Expression { get { return this.Children[0]; } set { this.Children[0] = value; value.setParent(this); } }
        private List<BaseLangObject> IfInstructions { get { return this.Children.GetRange(1, lastIfInstruction - 1); } }
        private List<BaseLangObject> ElseInstructions { get { return this.Children.GetRange(lastIfInstruction, this.Children.Count - lastIfInstruction); } }

        public OosIfElse()
        {
            lastIfInstruction = 0;
            this.addChild(null);
        }
        public void addIfChild(BaseLangObject blo)
        {
            this.addChild(blo);
            if (lastIfInstruction != this.Children.Count - 1)
                throw new Exception();
            lastIfInstruction++;
        }
        public void addElseChild(BaseLangObject blo)
        {
            this.addChild(blo);
        }

    }
}
