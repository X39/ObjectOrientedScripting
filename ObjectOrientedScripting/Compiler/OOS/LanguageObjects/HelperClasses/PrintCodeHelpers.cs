using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects.HelperClasses
{
    class PrintCodeHelpers
    {
        public static void printCodeLines(List<pBaseLangObject> instructions, string tab, System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            foreach (var it in instructions)
            {
                if (it is Ident)
                    sw.Write(tab + '\t');
                it.writeOut(sw, cfg);
                sw.WriteLine(";");
            }
        }
    }
}
