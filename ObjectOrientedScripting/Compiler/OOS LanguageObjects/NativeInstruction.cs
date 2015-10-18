using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class NativeInstruction : pBaseLangObject
    {
        private int line;
        public int Line { get { return this.line; } }
        private int pos;
        public int Pos { get { return this.pos; } }

        public VarTypeObject VTO { get; set; }
        public string Code { get; set; }
        public bool IsSimple { get; set; }

        public NativeInstruction(pBaseLangObject parent, int line, int pos) : base(parent)
        {
            this.line = line;
            this.pos = pos;
        }
        public string getCode(string[] argList)
        {
            if (this.children.Count == 0 && argList.Length == 0)
                return Code;
            if(argList.Length < this.children.Count)
            {
                throw new Exception("Should never happen exception got raised. Please report to developer!");
            }
            string outString;
            if (this.IsSimple)
            {
                outString = Code;
                outString = outString.Replace("_this", argList[0]);
                for(int i = 1; i < argList.Length; i++)
                {
                    outString = outString.Replace(((Variable)this.children[i - 1]).Name.OriginalValue, argList[i]);
                }
            }
            else
            {
                outString = "[";
                for (int i = 0; i < argList.Length; i++)
                {
                    if (i > 0)
                        outString += ',';
                    outString += argList[i];
                }
                outString += "] call {";
                string tmp = Code;
                tmp = tmp.Replace("_this", "(_this select 0)");
                for (int i = 0; i < this.children.Count; i++)
                {
                    tmp = tmp.Replace(((Variable)this.children[i]).Name.OriginalValue, "(_this select " + (i + 1) + ")");
                }
                outString += tmp + "}";
            }
            return outString;
        }
        public override int doFinalize()
        {
            return 0;
        }
    }
}
