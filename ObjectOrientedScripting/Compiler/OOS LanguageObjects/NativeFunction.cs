using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class NativeFunction : NativeInstruction, Interfaces.iFunction
    {
        private Ident name;
        public Ident Name { get { return name; } set { name = value; } }

        /// <summary>
        /// Return type of this iFunction
        /// </summary>
        public VarTypeObject ReturnType { get { return this.VTO; } }
        /// <summary>
        /// Returns a Template object which then can deref some unknown class conflicts in
        /// ArgList field
        /// </summary>
        public Template TemplateArguments { get { return this.getFirstOf<Native>().template; } }
        /// <summary>
        /// Returns functions encapsulation
        /// </summary>
        public Encapsulation FunctionEncapsulation { get { return Encapsulation.Public; } }
        /// <summary>
        /// Returns the Arglist required for this iFunction
        /// </summary>
        public List<VarTypeObject> ArgList
        {
            get
            {
                List<VarTypeObject> retList = new List<VarTypeObject>();
                foreach (var it in this.children)
                {
                    if (it is Variable)
                    {
                        retList.Add(((Variable)it).varType);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                return retList;
            }
        }
        public bool IsAsync { get { return false; } }

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
        public override string ToString()
        {
            return "nFnc->" + this.Name.FullyQualifiedName;
        }
    }
}
