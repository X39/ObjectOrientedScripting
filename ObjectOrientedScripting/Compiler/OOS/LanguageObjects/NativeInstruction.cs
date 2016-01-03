using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Compiler.OOS_LanguageObjects
{
    public class NativeInstruction : pBaseLangObject
    {
        public int Line { get; internal set; }
        public int Pos { get; internal set; }
        public string File { get; internal set; }
        VarTypeObject vto;
        public VarTypeObject VTO
        {
            get { return vto; }
            set { vto = value; }
        }
        public string Code { get; set; }
        public bool IsSimple { get; set; }

        public NativeInstruction(pBaseLangObject parent, int line, int pos, string file) : base(parent)
        {
            this.Line = line;
            this.Pos = pos;
            this.File = file;
        }
        public virtual string getCode(string[] argList)
        {
            if (this.children.Count == 0 && argList.Length == 0)
                return Code;
            if(argList.Length < this.children.Count - (this.Parent is Native ? 0 : 1))
            {
                throw new Exception("Should never happen exception got raised. Please report to developer!");
            }
            string outString;
            if (this.IsSimple)
            {
                outString = Code;
                if (this.Parent is Native)
                    outString = Regex.Replace(outString, "\\b" + "_this" + "\\b", argList[0].Trim());
                if (this.children.Count > 0 && children[0] is Ident)
                {
                    if (this.Parent is Native)
                    {
                        for (int i = 1; i < argList.Length; i++)
                        {
                            outString = Regex.Replace(outString, "\\b" + ((Variable)this.children[i]).Name.OriginalValue + "\\b", argList[i].Trim());
                        }
                    }
                    else
                    {
                        for (int i = 0; i < argList.Length; i++)
                        {
                            outString = Regex.Replace(outString, "\\b" + ((Variable)this.children[i + 1]).Name.OriginalValue + "\\b", argList[i].Trim());
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < argList.Length; i++)
                    {
                        outString = Regex.Replace(outString, "\\b" + ((Variable)this.children[i]).Name.OriginalValue + "\\b", argList[i].Trim());
                    }
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
                tmp = Regex.Replace(tmp, "\\b" + "_this" + "\\b", "(_this select 0)");
                int offset = (this.children.Count > 0 && children[0] is Ident ? 1 : 0);
                for (int i = offset; i < this.children.Count; i++)
                {
                    tmp = Regex.Replace(tmp, "\\b" + ((Variable)this.children[i]).Name.OriginalValue + "\\b", "(_this select " + (i - offset) + ")");
                }
                outString += tmp + "}";
            }
            return outString;
        }
        public override int doFinalize()
        {
            return 0;
        }
        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg) { }
    }
}
