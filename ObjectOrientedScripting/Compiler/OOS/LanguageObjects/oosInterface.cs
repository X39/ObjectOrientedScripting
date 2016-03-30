using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class oosInterface : pBaseLangObject, Interfaces.iName,  Interfaces.iClass, Interfaces.iHasId
    {
        public Ident Name { get { return ((Ident)this.children[0]); } set { this.children[0] = value; } }
        public List<Ident> ExtendedClasses { get { return new List<Ident>(); } }
        public VarTypeObject VTO { get; set; }
        public int ID { get; set; }

        public oosInterface(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
        }
        public override int doFinalize() { return 0; }
        public override string ToString()
        {
            return "interface->" + this.Name.FullyQualifiedName;
        }
        public Interfaces.iOperatorFunction getOperatorFunction(OverridableOperator op)
        {
            return null;
        }

        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg) { }
    }
}
