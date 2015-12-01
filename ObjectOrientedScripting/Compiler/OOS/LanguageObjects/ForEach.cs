using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class ForEach : pBaseLangObject, Interfaces.iCodeBlock, Interfaces.iBreakable
    {
        public Ident Variable { get { return (Ident)this.children[0]; } set { this.children[0] = value; } }
        public Variable Itterator { get { return (Variable)this.children[1]; } set { this.children[1] = value; } }
        public List<pBaseLangObject> CodeInstructions { get { return this.children.GetRange(2, this.children.Count - 2); } }
        public string BreakScope { get { return Wrapper.Compiler.ScopeNames.loop; } }

        public ForEach(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
            this.children.Add(null);
        }
        public override int doFinalize()
        {
            if (!Variable.LastIdent.ReferencedType.IsObject && !Variable.LastIdent.ReferencedType.ident.LastIdent.ReferencedObject.isType("::array"))
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, "Currently only arrays are allowed in ForEach");
                return 1;
            }
            //ToDo: implement the type check
            Logger.Instance.log(Logger.LogLevel.WARNING, "foreach currently is not able to fully validate type safety! Thus OOS cannot confirm types are considered as safe.");
            return 0;
        }

        public List<Return> ReturnCommands
        {
            get { return this.getAllChildrenOf<Return>(); }
        }

        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            string tab = new string('\t', this.Parent.getAllParentsOf<Interfaces.iCodeBlock>().Count);
            if ((Variable.LastIdent.ReferencedType.IsObject && Variable.LastIdent.ReferencedType.ident.LastIdent.ReferencedObject.isType("::array")) || (Variable is Interfaces.iHasType && ((Interfaces.iHasType)Variable).ReferencedType.IsArray))
            {
                sw.WriteLine(tab + '{');
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
                if (this.getAllChildrenOf<Break>(true).Count > 0)
                {
                    sw.WriteLine(tab + "scopeName \"" + this.BreakScope + "\";");
                }
                sw.WriteLine(tab + '\t' + Itterator.SqfVariableName + " = _x;");
                foreach (var it in this.CodeInstructions)
                {
                    if (it is Ident)
                        sw.Write(tab + '\t');
                    it.writeOut(sw, cfg);
                    sw.WriteLine(";");
                }
                sw.Write(tab + "} foreach (");
                Variable.writeOut(sw, cfg);
                sw.WriteLine(");");
            }
            else
            {
                throw new Exception();
            }
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
