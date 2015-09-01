using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Expression : pBaseLangObject
    {
        public pBaseLangObject lExpression { get { return this.children[0]; } set { this.children[0] = value; } }
        public pBaseLangObject rExpression { get { return this.children[1]; } set { this.children[1] = value; } }

        public string expOperator;
        public bool negate;


        public Expression(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
            this.children.Add(null);
            negate = false;
            expOperator = "";
        }
        public override void doFinalize() { }
    }
}
