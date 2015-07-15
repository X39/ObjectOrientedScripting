using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class OosFunctionCall : BaseLangObject, Interfaces.iName, Interfaces.iNormalizedName
    {
        string name;
        public string Name { get { return name; } set { name = value; } }
        public List<BaseLangObject> ArgList { get { return this.Children; } set { this.Children = value; } }

        public OosFunctionCall()
        {
            name = "";
        }

        public string getNormalizedName()
        {
            if (Parent == null)
                return this.name;
            Type parentType = Parent.GetType();
            if (parentType.Equals(typeof(Interfaces.iNormalizedName)))
            {
                return ((Interfaces.iNormalizedName)Parent).getNormalizedName() + "_" + this.name;
            }
            else
            {
                throw new Ex.InvalidParent();
            }
        }

    }
}
