using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class NativeFunction : NativeInstruction, Interfaces.iName
    {
        private Ident name;
        public Ident Name { get { return name; } set { if (!value.IsSimpleIdentifier) throw new Ex.InvalidIdentType(value.getIdentType(), IdentType.Name); name = value; } }
        public string FullyQualifiedName { get { return this.Name.FullyQualifiedName; } }

        public NativeFunction(pBaseLangObject parent, int line, int pos) : base(parent, line, pos)
        {
        }
        public override int doFinalize()
        {
            int errCount = 0;
            errCount += name.finalize();
            if (VTO.ident != null)
                errCount += VTO.ident.finalize();
            return errCount;
        }
    }
}
