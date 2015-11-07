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
        public List<pBaseLangObject> CatchInstructions { get { return this.children.GetRange(endMarker + 1, this.children.Count - (endMarker + 1)); } }

        public TryCatch(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
        }
        public override int doFinalize()
        {
            //if (((Variable)variable).ReferencedType.varType != VarType.String)
            //{
            //    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0042, ((Variable)variable).Line, ((Variable)variable).Pos));
            //    return 1;
            //}
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
            var varListTry = this.getAllChildrenOf<Variable>(false, null, -1, 1);
            if (varListTry.Count > 0)
            {
                if (varListTry.Count == 1)
                    sw.Write("private ");
                else
                    sw.Write("private [");

                for (int i = 0; i < varListTry.Count; i++)
                {
                    var it = varListTry[i];
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
                if (varListTry.Count > 1)
                    sw.Write("]");
                sw.WriteLine(";");
            }
            foreach(var it in this.TryInstructions)
            {
                it.writeOut(sw, cfg);
                sw.WriteLine(";");
            }
            sw.WriteLine(tab + "}");
            sw.WriteLine(tab + "catch");
            sw.WriteLine(tab + "{");
            var varListCatch = this.getAllChildrenOf<Variable>(false, null, -1, 2);
            if (varListCatch.Count > 0)
            {
                if (varListCatch.Count == 1)
                    sw.Write("private ");
                else
                    sw.Write("private [");

                for (int i = 0; i < varListCatch.Count; i++)
                {
                    var it = varListCatch[i];
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
                if (varListCatch.Count > 1)
                    sw.Write("]");
                sw.WriteLine(";");
            }
            this.variable.writeOut(sw, cfg);
            sw.WriteLine(" = _exception;");
            foreach (var it in this.CatchInstructions)
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
