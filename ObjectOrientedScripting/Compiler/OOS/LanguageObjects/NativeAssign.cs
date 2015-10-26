using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class NativeAssign : NativeInstruction, Interfaces.iName
    {
        private Ident name;
        public Ident Name { get { return name; } set { name = value; } }
        public NativeAssign(pBaseLangObject parent, int line, int pos) : base(parent, line, pos)
        {
            if (parent is Native)
            {
                this.name = ((Native)parent).Name;
                this.VTO = new VarTypeObject(this.name);
            }
            else
            {
                throw new Exception("Never NEVER ever this should happen! If it does, report to dev.");
            }
        }
        public override int doFinalize()
        {
            int errCount = 0;
            if (VTO.ident != null)
                errCount += VTO.ident.finalize();
            return errCount;
        }
    }
}
