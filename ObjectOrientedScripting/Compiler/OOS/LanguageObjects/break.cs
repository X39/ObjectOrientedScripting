using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Break : pBaseLangObject
    {
        public Break(pBaseLangObject parent) : base(parent) {}
        public override int doFinalize() { return 0; }

        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            string tab = new string('\t', this.getAllParentsOf<Interfaces.iCodeBlock>().Count);
            var breakable = this.getFirstOf<Interfaces.iBreakable>();
            sw.Write(tab + "breakOut \"" + breakable.BreakScope + "\"");
        }
    }
}
