using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Native : pBaseLangObject, Interfaces.iName, Interfaces.iTemplate, Interfaces.iClass
    {
        private int line;
        public int Line { get { return this.line; } }
        public Template TemplateObject { get; set; }
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

        public List<Ident> ExtendedClasses
        {
            get { return new List<Ident>(); }
        }

        public VarTypeObject VTO { get; set; }

        public Interfaces.iOperatorFunction getOperatorFunction(OverridableOperator op)
        {
            var opFncList = this.getAllChildrenOf<Interfaces.iOperatorFunction>();
            foreach(var it in opFncList)
            {
                if(it.OperatorType == op)
                {
                    return it;
                }
            }
            return null;
        }
        public override string ToString()
        {
            return "native->" + this.Name.FullyQualifiedName;
        }
        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg) { }
    }
}
