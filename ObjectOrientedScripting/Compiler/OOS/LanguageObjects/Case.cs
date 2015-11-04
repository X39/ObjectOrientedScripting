using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Case : pBaseLangObject, Interfaces.iCodeBlock
    {
        int endMarker;
        public List<pBaseLangObject> Cases { get { return this.children.GetRange(0, endMarker); } }
        public List<pBaseLangObject> CodeInstructions { get { return this.children.GetRange(endMarker, this.children.Count - (endMarker)); } }

        public void markEnd()
        {
            endMarker = this.children.Count;
        }
        private int line;
        public int Line { get { return this.line; } }
        private int pos;
        public int Pos { get { return this.pos; } }
        public Case(pBaseLangObject parent, int line, int pos) : base(parent)
        {
            this.line = line;
            this.pos = pos;
        }
        public override int doFinalize()
        {
            int errCount = 0;
            var switchElement = this.Parent.getFirstOf<Switch>();
            foreach (var expression in this.Cases)
            {
                if (expression != null)
                {
                    if (!((Expression)expression).ReferencedType.Equals(switchElement.ReferencedType))
                    {
                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0024, this.line, this.pos));
                        errCount++;
                    }
                }
            }
            return errCount;
        }

        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            string tab = new string('\t', this.getAllParentsOf<Interfaces.iCodeBlock>().Count);
            var caseList = this.Cases;
            if (caseList.Count == 0)
            {
                sw.WriteLine(tab + "default {");
            }
            else
            {
                foreach (var it in caseList)
                {
                    sw.Write(tab + "case ");
                    it.writeOut(sw, cfg);
                    sw.Write(":");
                }
                sw.WriteLine("{");
            }
            foreach(var it in this.CodeInstructions)
            {
                it.writeOut(sw, cfg);
                sw.WriteLine(";");
            }
            var lastInstruction = CodeInstructions.Last();
            if (!(lastInstruction is Break))
            {
                lastInstruction.writeOut(sw, cfg);
                sw.WriteLine(";");
            }
            sw.Write(tab + "}");
        }

        public List<Return> ReturnCommands
        {
            get { return this.getAllChildrenOf<Return>(); }
        }


        public bool AlwaysReturns
        {
            get
            {
                if (this.ReturnCommands.Count > 0)
                    return true;
                var codeBlocks = this.getAllChildrenOf<Interfaces.iCodeBlock>();
                foreach(var it in codeBlocks)
                {
                    if (it.AlwaysReturns)
                        return true;
                }
                return false;
            }
        }
    }
}
