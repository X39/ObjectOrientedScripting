using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class oosInterface : pBaseLangObject, Interfaces.iName, Interfaces.iGetFunctionIndex, Interfaces.iGetVariableIndex, Interfaces.iClass
    {
        public Ident Name { get { return ((Ident)this.children[0]); } set { this.children[0] = value; } }
        public List<Ident> ExtendedClasses { get { return new List<Ident>(); } }
        public VarTypeObject VTO { get; set; }
        
        public oosInterface(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
        }
        public override int doFinalize() { return 0; }
        public override string ToString()
        {
            return "interface->" + this.Name.FullyQualifiedName;
        }
        public Tuple<int, int> getFunctionIndex(Ident ident)
        {
            Tuple<int, int> tuple = null;
            for (int i = 0; i < this.children.Count; i++)
            {
                var it = this.children[i];
                if (it is Function)
                {
                    if (((Function)it).Name.FullyQualifiedName.Equals(ident.FullyQualifiedName))
                    {
                        tuple = new Tuple<int, int>(0, i);
                        break;
                    }
                }
            }
            return tuple == null ? new Tuple<int, int>(-1, -1) : tuple;
        }
        public Tuple<int, int> getVariableIndex(Ident ident)
        {
            Tuple<int, int> tuple = null;
            for (int i = 0; i < this.children.Count; i++)
            {
                var it = this.children[i];
                if (it is Variable)
                {
                    if (((Variable)it).Name.FullyQualifiedName.Equals(ident.FullyQualifiedName))
                    {
                        tuple = new Tuple<int, int>(0, i);
                        break;
                    }
                }
            }
            return tuple == null ? new Tuple<int, int>(-1, -1) : tuple;
        }


        public Interfaces.iOperatorFunction getOperatorFunction(OperatorFunctions op)
        {
            return null;
        }
    }
}
