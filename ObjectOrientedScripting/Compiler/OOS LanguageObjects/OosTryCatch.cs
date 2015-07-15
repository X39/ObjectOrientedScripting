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

        public BaseLangObject CatchVariable { get { return this.Children[0]; } set { this.Children[0] = value; value.setParent(this); } }
        private List<BaseLangObject> TryInstructions { get { return this.Children.GetRange(1, lastTryInstruction - 1); } }
        private List<BaseLangObject> CatchInstructions { get { return this.Children.GetRange(lastTryInstruction, this.Children.Count - lastTryInstruction); } }

        public OosTryCatch()
        {
            lastTryInstruction = 0;
            this.addChild(null);
        }
        public void addTryChild(BaseLangObject blo)
        {
            this.addChild(blo);
            if (lastTryInstruction != this.Children.Count - 1)
                throw new Exception();
            lastTryInstruction++;
        }
        public void addCatchChild(BaseLangObject blo)
        {
            this.addChild(blo);
        }

    }
}
