using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class NativeOperator : NativeInstruction, Interfaces.iOperatorFunction
    {
        public NativeOperator(pBaseLangObject parent, int line, int pos) : base(parent, line, pos)
        {
            this.addChild(null);
            VTO = null;
        }
        public override int doFinalize()
        {
            int errCount = 0;
            if (VTO.ident != null)
                errCount += VTO.ident.finalize();
            return errCount;
        }
        public bool IsConstructor { get { return false; } }

        /// <summary>
        /// Return type of this iFunction
        /// </summary>
        public VarTypeObject ReturnType { get { return this.VTO; } }
        public VarTypeObject ReferencedType { get { return this.ReturnType; } }
        /// <summary>
        /// Returns a Template object which then can deref some unknown class conflicts in
        /// ArgList field
        /// </summary>
        public Template TemplateArguments { get { return this.getFirstOf<Native>().TemplateObject; } }
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
        public bool IsAsync { get { return false; } }

        public Ident Name { get { return (Ident)this.children[0]; } set { this.children[0] = value; } }
        public override string ToString()
        {
            return "nOp->" + Enum.GetName(typeof(OverridableOperator), this.OperatorType);
        }
        private OverridableOperator opType;
        public OverridableOperator OperatorType { get { return opType; } set { this.Name = new Ident(this, Enum.GetName(typeof(OverridableOperator), value), this.Line, this.Pos); this.opType = value; } }

        public List<Return> ReturnCommands { get { return new List<Return>(); } }
        public bool AlwaysReturns { get { return true; } }

    }
}
