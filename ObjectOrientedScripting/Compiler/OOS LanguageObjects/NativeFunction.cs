using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class NativeFunction : NativeInstruction, Interfaces.iName
    {
        private int line;
        public int Line { get { return this.line; } }
        private int pos;
        public int Pos { get { return this.pos; } }
        public VarTypeObject varType;
        private Ident name;
        public Ident Name { get { return name; } set { if (!value.IsSimpleIdentifier) throw new Ex.InvalidIdentType(value.getIdentType(), IdentType.Name); name = value; } }
        public string FullyQualifiedName { get { return this.Name.FullyQualifiedName; } }

        public NativeFunction(pBaseLangObject parent, int line, int pos) : base(parent, line, pos)
        {
            varType = null;
        }
        public override int doFinalize()
        {
            int errCount = 0;
            errCount += name.finalize();
            if (varType.ident != null)
                errCount += varType.ident.finalize();
            return errCount;
        }
    }
}
