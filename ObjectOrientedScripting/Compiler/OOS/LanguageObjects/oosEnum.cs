using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class oosEnum : pBaseLangObject, Interfaces.iName
    {
        public Ident Name { get { return ((Ident)this.children[0]); } set { this.children[0] = value; } }
        public oosEnum(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
        }
        public override int doFinalize()
        {
            int errCount = 0;
            return errCount;
        }
        public override string ToString()
        {
            return "enum->" + this.Name.FullyQualifiedName;
        }
        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {

        }
    }
}
