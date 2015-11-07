using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class IfElse : pBaseLangObject, Interfaces.iCodeBlock
    {
        public VarType functionVarType;
        private int endMarker;

        public pBaseLangObject expression { get { return this.children[0]; } set { this.children[0] = value; } }
        public bool HasElse { get { return this.ElseInstructions.Count > 0; } }
        public List<pBaseLangObject> IfInstructions { get { return this.children.GetRange(1, endMarker); } }
        public List<pBaseLangObject> ElseInstructions { get { return this.children.GetRange(endMarker + 1, this.children.Count - (endMarker + 1)); } }

        public IfElse(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
        }
        public override int doFinalize() { return 0; }
        public void markIfEnd()
        {
            endMarker = this.children.Count - 1;
        }
        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            string tab = new string('\t', this.Parent.getAllParentsOf<Interfaces.iCodeBlock>().Count);
            sw.Write(tab + "if(");
            this.expression.writeOut(sw, cfg);
            sw.WriteLine(") then");
            sw.WriteLine(tab + "{");
            var varListFirstScope = this.getAllChildrenOf<Variable>(false, null, -1, 1);
            if (varListFirstScope.Count > 0)
            {
                if (varListFirstScope.Count == 1)
                    sw.Write("private ");
                else
                    sw.Write("private [");

                for (int i = 0; i < varListFirstScope.Count; i++)
                {
                    var it = varListFirstScope[i];
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
                if (varListFirstScope.Count > 1)
                    sw.Write("]");
                sw.WriteLine(";");
            }
            foreach (var it in this.IfInstructions)
            {
                it.writeOut(sw, cfg);
                sw.WriteLine(";");
            }
            sw.Write(tab + "}");
            if (this.ElseInstructions.Count > 0)
            {
                sw.WriteLine("\n" + tab + "else");
                sw.WriteLine(tab + "{");
                var varListSecondScope = this.getAllChildrenOf<Variable>(false, null, -1, 2);
                if (varListSecondScope.Count > 0)
                {
                    if (varListSecondScope.Count == 1)
                        sw.Write("private ");
                    else
                        sw.Write("private [");

                    for (int i = 0; i < varListSecondScope.Count; i++)
                    {
                        var it = varListSecondScope[i];
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
                    if (varListSecondScope.Count > 1)
                        sw.Write("]");
                    sw.WriteLine(";");
                }
                foreach (var it in this.ElseInstructions)
                {
                    it.writeOut(sw, cfg);
                    sw.WriteLine(";");
                }
                sw.Write(tab + "}");
            }
        }

        public List<Return> ReturnCommands
        {
            get { return this.getAllChildrenOf<Return>(); }
        }
        public bool AlwaysReturns
        {
            get
            {
                if (!this.HasElse)
                    return false;
                bool flag = false;
                foreach (var it in this.IfInstructions)
                {
                    if (it is Return || (it is Interfaces.iCodeBlock && ((Interfaces.iCodeBlock)it).AlwaysReturns))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                    return false;
                foreach (var it in this.IfInstructions)
                {
                    if (it is Return || (it is Interfaces.iCodeBlock && ((Interfaces.iCodeBlock)it).AlwaysReturns))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public override List<pBaseLangObject> getScopeItems(int scopeIndex)
        {
            switch (scopeIndex)
            {
                case 0:
                    var list = new List<pBaseLangObject>();
                    list.Add(this.expression);
                    return list;
                case 1:
                    return this.IfInstructions;
                case 2:
                    return this.ElseInstructions;
                default:
                    return this.children;
            }
        }
    }
}
