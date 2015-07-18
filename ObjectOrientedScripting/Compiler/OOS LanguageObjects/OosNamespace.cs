using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class OosNamespace : BaseLangObject, Interfaces.iName, Interfaces.iNormalizedName
    {
        string name;
        public string Name { get { return name; } set { name = value; } }

        public OosNamespace()
        {
            name = "";
        }

        public string getNormalizedName()
        {
            if (Parent == null)
                return this.name;
            if (Parent is OosNamespace)
            {
                return ((OosNamespace)Parent).getNormalizedName() + "_" + this.name;
            }
            else
            {
                throw new Ex.InvalidParent();
            }
        }
    }
}
