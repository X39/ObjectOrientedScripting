using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class BaseFunctionObject : BaseLangObject, Interfaces.iName, Interfaces.iNormalizedName
    {
        string name;
        public string Name { get { return name; } set { name = value; } }
        List<string> argList;
        public List<string> ArgList { get { return argList; } set { argList = value; } }
        List<string> varList;
        public List<string> VarList { get { return varList; } set { varList = value; } }

        public BaseFunctionObject()
        {
            argList = new List<string>();
            varList = new List<string>();
            name = "";
        }

        public string getNormalizedName()
        {
            if (Parent == null)
                return this.name;
            if (Parent is OosContainer)
            {
                return this.name;
            }
            else if (Parent is Interfaces.iNormalizedName)
            {
                return ((Interfaces.iNormalizedName)Parent).getNormalizedName() + "_fnc_" + this.name;
            }
            else
            {
                throw new Ex.InvalidParent();
            }
        }
    }
}
