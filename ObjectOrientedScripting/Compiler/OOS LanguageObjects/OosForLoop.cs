using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class OosForLoop : BaseLangObject
    {
        public BaseLangObject Arg1 { get { return this.Children[0]; } set { this.Children[0] = value; if(value != null) value.setParent(this); } }
        public BaseLangObject Arg2 { get { return this.Children[1]; } set { this.Children[1] = value; if(value != null) value.setParent(this); } }
        public BaseLangObject Arg3 { get { return this.Children[2]; } set { this.Children[2] = value; if(value != null) value.setParent(this); } }
        private List<BaseLangObject> Instructions { get { return this.Children.GetRange(3, this.Children.Count - 3); } }

        public OosForLoop()
        {
            this.addChild(null);
            this.addChild(null);
            this.addChild(null);
        }

    }
}
