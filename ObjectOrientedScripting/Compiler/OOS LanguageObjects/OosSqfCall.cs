using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class OosSqfCall : BaseLangObject
    {
        int lastLArg;
        string instructionName;
        public string InstructionName { get { return instructionName; } set { instructionName = value; } }
        public List<BaseLangObject> LArgs { get { return this.Children.GetRange(1, lastLArg); } }
        public List<BaseLangObject> RArgs { get { return this.Children.GetRange(lastLArg + 1, this.Children.Count - (lastLArg + 1)); } }

        public OosSqfCall()
        {
            instructionName = "";
            lastLArg = 0;
        }
        public void markEnd()
        {
            lastLArg = this.Children.Count - 1;
        }
    }
}
