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
                if (it is Variable && ((Variable)it).getAllChildrenOf<VariableAssignment>().Count == 0)
                    continue;
                it.writeOut(sw, cfg);
                sw.WriteLine(";");
            }
        }
        public static void printPrivateArray(pBaseLangObject obj, string tab, System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg, int scopeIndex = -1)
        {
            var varList = obj.getAllChildrenOf<Variable>(false, null, -1, scopeIndex);
            var forLoopList = obj.getAllChildrenOf<For>(false, null, -1, scopeIndex);
            foreach(var it in forLoopList)
            {
                if(it.forArg1 != null && it.forArg1 is Variable)
                {
                    varList.Add((Variable)it.forArg1);
                }
            }
            if (varList.Count > 0)
            {
                if (varList.Count == 1)
                    sw.Write(tab + '\t' + "private ");
                else
                    sw.Write(tab + '\t' + "private [");

                for (int i = 0; i < varList.Count; i++)
                {
                    var it = varList[i];
                    if (i != 0)
                    {
                        sw.Write(", ");
                    }
                    if (it is Variable)
                    {
                        sw.Write('"' + ((Variable)it).SqfVariableName + '"');
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                if (varList.Count > 1)
                    sw.Write("]");
                sw.WriteLine(";");
            }
        }
    }
}
