using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class For : pBaseLangObject, Interfaces.iCodeBlock, Interfaces.iBreakable
    {
        public pBaseLangObject forArg1 { get { return this.children[0]; } set { this.children[0] = value; } }
        public pBaseLangObject forArg2 { get { return this.children[1]; } set { this.children[1] = value; } }
        public pBaseLangObject forArg3 { get { return this.children[2]; } set { this.children[2] = value; } }
        public List<pBaseLangObject> CodeInstructions { get { return this.children.GetRange(3, this.children.Count - 3); } }
        public string BreakScope { get { return Wrapper.Compiler.ScopeNames.loop; } }

        public For(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
            this.children.Add(null);
            this.children.Add(null);
        }
        public override int doFinalize() { return 0; }

        public List<Return> ReturnCommands
        {
            get { return this.getAllChildrenOf<Return>(); }
        }

        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            //ToDo: Fix the wrong private of the forArg1 variable from inside of the loop to parent codeblock
            string tab = new string('\t', this.Parent.getAllParentsOf<Interfaces.iCodeBlock>().Count);

            this.forArg1.writeOut(sw, cfg);
            sw.WriteLine(";");
            sw.Write(tab + "while {");
            this.forArg2.writeOut(sw, cfg);
            sw.WriteLine("} do");
            sw.WriteLine(tab + "{");
            var varList = this.getAllChildrenOf<Variable>();
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
            this.forArg3.writeOut(sw, cfg);
            sw.WriteLine(";");
            foreach (var it in this.CodeInstructions)
            {
                it.writeOut(sw, cfg);
                sw.WriteLine(";");
            }
            sw.Write(tab + "}");
        }


        public bool AlwaysReturns
        {
            get
            {
                if (this.ReturnCommands.Count > 0)
                    return true;
                var codeBlocks = this.getAllChildrenOf<Interfaces.iCodeBlock>();
                foreach (var it in codeBlocks)
                {
                    if (it.AlwaysReturns)
                        return true;
                }
                return false;
            }
        }
    }
}
