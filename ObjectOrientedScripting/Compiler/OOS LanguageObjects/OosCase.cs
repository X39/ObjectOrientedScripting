using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class OosCase : BaseLangObject
    {
        public BaseLangObject Value { get { return this.Children[0]; } set { this.Children[0] = value; if (value != null) value.setParent(this); } }
        public List<BaseLangObject> Instructions { get { return this.Children.GetRange(1, this.Children.Count - 1); } }
        public bool IsDefault { get { return this.Value == null; } }

        public OosCase()
        {
            this.addChild(null);
        }

    }
}
