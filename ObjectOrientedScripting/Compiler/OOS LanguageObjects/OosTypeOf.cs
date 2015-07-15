using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class OosTypeOf : BaseLangObject
    {
        public BaseLangObject Argument { get { return this.Children[0]; } set { this.Children[0] = value; value.setParent(this); } }

        public OosTypeOf()
        {
            this.addChild(null);
        }
    }
}
