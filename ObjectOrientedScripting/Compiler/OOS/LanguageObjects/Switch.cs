using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Switch : pBaseLangObject, Interfaces.iHasType, Interfaces.iCodeBlock
    {
        public pBaseLangObject expression { get { return this.children[0]; } set { this.children[0] = value; } }
        public List<pBaseLangObject> CodeInstructions { get { return this.children.GetRange(1, this.children.Count - 1); } }
        public VarTypeObject ReferencedType { get { return ((Expression)expression).ReferencedType; } }
        public Case DefaultCase { get; internal set; }
        public Switch(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
            this.DefaultCase = null;
        }
        public override int doFinalize()
        {
            int errCount = 0;
            var caseList = this.getAllChildrenOf<Case>();
            bool flag = false;
            foreach (var it in caseList)
            {
                if (it.Cases.Count == 0)
                {
                    if (flag)
                    {
                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0020, it.Line, it.Pos));
                        errCount++;
                    }
                    else
                    {
                        flag = true;
                        this.DefaultCase = it;
                    }
                }
            }
            //ToDo: Check for duplicated case
            return errCount;
        }

        public List<Return> ReturnCommands
        {
            get { return new List<Return>(); }
        }


        public bool AlwaysReturns
        {
            get
            {
                var caseList = this.getAllChildrenOf<Case>();
                if(!caseList.TrueForAll(it => it.AlwaysReturns))
                    return false;
                return this.DefaultCase != null;
            }
        }

        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            string tab = new string('\t', this.getAllParentsOf<Interfaces.iCodeBlock>().Count);
            sw.Write("switch ");
            this.expression.writeOut(sw, cfg);
            sw.WriteLine(" do");
            sw.WriteLine(tab + "{");
            foreach (var it in this.CodeInstructions)
            {
                it.writeOut(sw, cfg);
                sw.WriteLine(";");
            }
            sw.Write(tab + "}");
        }
    }
}
