﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class oosClass : pBaseLangObject, Interfaces.iName
    {
        private Ident name;
        public Ident Name { get { return name; } set { if (!name.IsSimpleIdentifier) throw new Ex.InvalidIdentType(value.getIdentType(), IdentType.Name); name = value; } }
        private List<pBaseLangObject> parentClasses;
        
        public string FullyQualifiedName
        {
            get
            {
                string s = "";
                List<Interfaces.iName> parentList = new List<Interfaces.iName>();
                pBaseLangObject curParent = Parent;
                while (curParent != null)
                {
                    if (curParent is Interfaces.iName)
                        parentList.Add((Interfaces.iName)curParent);
                    curParent = curParent.Parent;
                }
                parentList.Reverse();
                foreach (Interfaces.iName it in parentList)
                    s += it.Name.OriginalValue;
                s += this.Name.OriginalValue;
                return s;
            }
        }
        public oosClass(pBaseLangObject parent) : base(parent)
        {
            this.parentClasses = new List<pBaseLangObject>();
        }
        virtual void doFinalize() {}

        public void addParentClass(pBaseLangObject blo)
        {
            this.parentClasses.Add(blo);
        }
    }
}
