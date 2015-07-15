using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class OosInstanceOf : BaseLangObject
    {
        public BaseLangObject LArgument { get { return this.Children[0]; } set { this.Children[0] = value; value.setParent(this); } }
        public BaseLangObject RArgument { get { return this.Children[1]; } set { this.Children[1] = value; value.setParent(this); } }

        public OosInstanceOf()
        {
            this.addChild(null);
            this.addChild(null);
        }

    }
}
