using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class TryCatch : pBaseLangObject, Interfaces.iCodeBlock
    {
        public VarType functionVarType;
        private int endMarker;

        public pBaseLangObject variable { get { return this.children[0]; } set { this.children[0] = value; } }
        public List<pBaseLangObject> TryInstructions { get { return this.children.GetRange(1, endMarker); } }
        public List<pBaseLangObject> CatchInstructions
        {
            get
            {
                List<pBaseLangObject> objList = new List<pBaseLangObject>();
                objList.Add(this.variable);
                objList.AddRange(this.children.GetRange(endMarker + 1, this.children.Count - (endMarker + 1)));
                return objList;
            }
        }

        public TryCatch(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
        }
        public override int doFinalize()
        {
            if (!((Variable)variable).ReferencedType.IsKindOf(Wrapper.Compiler.InternalObjectVarTypes.VT_Exception))
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0056, ((Variable)variable).Line, ((Variable)variable).Pos));
                return 1;
            }
            return 0;
        }
        public void markEnd()
        {
            endMarker = this.children.Count - 1;
        }

        public List<Return> ReturnCommands
        {
            get { return this.getAllChildrenOf<Return>(); }
        }

        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            string tab = new string('\t', this.Parent.getAllParentsOf<Interfaces.iCodeBlock>().Count);
            sw.WriteLine(tab + "try");
            sw.WriteLine(tab + "{");
            HelperClasses.PrintCodeHelpers.printPrivateArray(this, tab, sw, cfg, 1);
            HelperClasses.PrintCodeHelpers.printCodeLines(this.TryInstructions, tab, sw, cfg);
            sw.WriteLine(tab + "}");
            sw.WriteLine(tab + "catch");
            sw.WriteLine(tab + "{");
            HelperClasses.PrintCodeHelpers.printPrivateArray(this, tab, sw, cfg, 2);
            this.variable.writeOut(sw, cfg);
            sw.WriteLine(" = _exception;");
            HelperClasses.PrintCodeHelpers.printCodeLines(this.CatchInstructions, tab, sw, cfg);
            sw.Write(tab + "}");
        }
        public bool AlwaysReturns
        {
            get
            {
                bool flag = false;
                foreach (var it in this.TryInstructions)
                {
                    if (it is Return || (it is Interfaces.iCodeBlock && ((Interfaces.iCodeBlock)it).AlwaysReturns))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                    return false;
                foreach (var it in this.CatchInstructions)
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
            switch(scopeIndex)
            {
                case 0:
                    var list = new List<pBaseLangObject>();
                    list.Add(this.variable);
                    return list;
                case 1:
                    return this.TryInstructions;
                case 2:
                    return this.CatchInstructions;
                default:
                    return this.children;
            }
        }
    }
}
