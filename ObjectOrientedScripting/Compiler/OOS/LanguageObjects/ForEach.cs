using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class ForEach : pBaseLangObject, Interfaces.iCodeBlock, Interfaces.iBreakable
    {
        public Expression Variable { get { return (Expression)this.children[0]; } set { this.children[0] = value; if (!value.HasIdent) throw new Exception("INVALID OPERATION"); } }
        public Variable Itterator { get { return (Variable)this.children[1]; } set { this.children[1] = value; } }
        public List<pBaseLangObject> CodeInstructions { get { return this.children.GetRange(2, this.children.Count - 2); } }
        public string BreakScope { get { return Wrapper.Compiler.ScopeNames.loop; } }

        public ForEach(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
            this.children.Add(null);
        }
        private static bool warningSurpression = false;
        public override int doFinalize()
        {
            if (!Variable.Ident.LastIdent.ReferencedType.IsObject && !Variable.Ident.LastIdent.ReferencedType.ident.LastIdent.ReferencedObject.isType("::array"))
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, "Currently only arrays are allowed in ForEach");
                return 1;
            }
            //ToDo: implement the type check
            if (!warningSurpression)
            {
                warningSurpression = true;
                Logger.Instance.log(Logger.LogLevel.WARNING, "foreach currently is not able to fully validate type safety! Thus OOS cannot confirm types are considered as safe.");
            }
            return 0;
        }

        public List<Return> ReturnCommands
        {
            get { return this.getAllChildrenOf<Return>(); }
        }

        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            string tab = new string('\t', this.Parent.getAllParentsOf<Interfaces.iCodeBlock>().Count);
            if ((Variable.Ident.LastIdent.ReferencedType.IsObject && Variable.Ident.LastIdent.ReferencedType.ident.LastIdent.ReferencedObject.isType("::array")) || (Variable is Interfaces.iHasType && ((Interfaces.iHasType)Variable).ReferencedType.IsArray))
            {
                sw.WriteLine(tab + '{');
                HelperClasses.PrintCodeHelpers.printPrivateArray(this, tab, sw, cfg);
                if (this.getAllChildrenOf<Break>(true).Count > 0)
                {
                    sw.WriteLine(tab + '\t' + "scopeName \"" + this.BreakScope + "\";");
                }
                sw.WriteLine(tab + '\t' + Itterator.SqfVariableName + " = _x;");
                HelperClasses.PrintCodeHelpers.printCodeLines(this.CodeInstructions, tab, sw, cfg);
                sw.Write(tab + "} foreach ");
                Variable.writeOut(sw, cfg);
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
