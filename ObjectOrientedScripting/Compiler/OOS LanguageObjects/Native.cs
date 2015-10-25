using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Native : pBaseLangObject, Interfaces.iName, Interfaces.iTemplate
    {
        private int line;
        public int Line { get { return this.line; } }
        public Template template { get; set; }
        private int pos;
        public int Pos { get { return this.pos; } }
        public Ident Name { get { return ((Ident)this.children[0]); } set { this.children[0] = value; } }
        public Native(pBaseLangObject parent, int line, int pos) : base(parent)
        {
            this.addChild(null);
            this.line = line;
            this.pos = pos;
        }
        public override int doFinalize() { return 0; }
    }
}
