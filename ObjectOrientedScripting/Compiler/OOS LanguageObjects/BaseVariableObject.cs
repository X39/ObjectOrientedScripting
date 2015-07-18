﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class BaseVariableObject : BaseLangObject, Interfaces.iName, Interfaces.iNormalizedName
    {
        string name;
        public string Name { get { return this.name; } set { this.name = value; } }
        BaseLangObject value;
        public BaseLangObject Value { get { return this.value; } set { this.value = value; } }

        public BaseVariableObject()
        {
            value = null;
            name = "";
        }

        public string getNormalizedName()
        {
            if (Parent == null)
                return this.name;
            if (Parent is Interfaces.iNormalizedName)
            {
                return ((Interfaces.iNormalizedName)Parent).getNormalizedName() + "_var_" + this.name;
            }
            else
            {
                throw new Ex.InvalidParent();
            }
        }
    }
}
