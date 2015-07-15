using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class OosTryCatch : BaseLangObject
    {
        int lastTryInstruction;

        public BaseLangObject CatchVariable { get { return this.Children[0]; } set { this.Children[0] = value; if(value != null) value.setParent(this); } }
        private List<BaseLangObject> TryInstructions { get { return this.Children.GetRange(1, lastTryInstruction - 1); } }
        private List<BaseLangObject> CatchInstructions { get { return this.Children.GetRange(lastTryInstruction, this.Children.Count - lastTryInstruction); } }

        public OosTryCatch()
        {
            lastTryInstruction = 1;
            this.addChild(null);
        }
        public void markEnd()
        {
            lastTryInstruction = this.Children.Count - lastTryInstruction;
        }

    }
}
