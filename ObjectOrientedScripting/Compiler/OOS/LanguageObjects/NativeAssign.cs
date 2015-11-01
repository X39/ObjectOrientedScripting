using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class NativeAssign : NativeInstruction, Interfaces.iFunction
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

        public VarTypeObject ReturnType { get { return this.VTO; } }
        public Template TemplateArguments { get { return this.getFirstOf<Native>().TemplateObject; } }
        public Encapsulation FunctionEncapsulation { get { return Encapsulation.Public; } }
        public bool IsAsync { get { return false; } }
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
                    else if (it is Ident)
                    {
                        //Do nothing as we got the Name here
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                return retList;
            }
        }
        public List<Return> ReturnCommands { get { return new List<Return>(); } }
        public VarTypeObject ReferencedType { get { return this.ReturnType; } }
    }
}
