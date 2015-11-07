using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Template : pBaseLangObject
    {
        private int line;
        public int Line { get { return this.line; } }
        private int pos;
        public int Pos { get { return this.pos; } }
        public List<VarTypeObject> vtoList;
        public Template(pBaseLangObject parent, int line, int pos) : base(parent)
        {
            this.line = line;
            this.pos = pos;
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
