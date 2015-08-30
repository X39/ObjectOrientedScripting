using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Compiler.OOS_LanguageObjects
{
    public class OosClass : BaseLangObject, Interfaces.iName, Interfaces.iNormalizedName
    {
        string name;
        public string Name { get { return name; } set { name = value; } }
        List<string> parentClasses;
        public List<string> ParentClasses { get { return parentClasses; } set { parentClasses = value; } }

        public OosClass()
        {
            parentClasses = new List<string>();
            name = "";
        }

        public string getNormalizedName()
        {
            if (Parent == null)
                return this.name;
            else if (Parent is OosNamespace || Parent is OosClass)
            {
                var retString = ((Interfaces.iNormalizedName)Parent).getNormalizedName() + "_" + this.name;
                return retString;
            }
            else
            {
                throw new Ex.InvalidParent();
            }
        }
    }
}
