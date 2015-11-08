using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Template : pBaseLangObject
    {
        public int Line { get; internal set; }
        public int Pos { get; internal set; }
        public string File { get; internal set; }
        public List<VarTypeObject> vtoList;
        public Template(pBaseLangObject parent, int line, int pos, string file) : base(parent)
        {
            this.Line = line;
            this.Pos = pos;
            this.File = file;
            vtoList = new List<VarTypeObject>();
        }
        public override int doFinalize()
        {
            int errCount = 0;
            foreach(var it in this.vtoList)
            {
                if(it.ident != null)
                {
                    errCount += it.ident.finalize();
                }
            }
            return errCount;
        }
        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg) { }
    }
}
