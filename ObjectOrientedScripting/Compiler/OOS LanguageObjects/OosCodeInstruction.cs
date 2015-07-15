using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class OosCodeInstruction : BaseLangObject
    {
        public BaseLangObject Instruction { get { return this.Children[0]; } set { this.Children[0] = value; value.setParent(this); } }
        string suffix;
        public string Suffix { get { return suffix; } set { suffix = value; } }
        public OosCodeInstruction()
        {
            this.addChild(null);
            suffix = "";
        }
    }
}
