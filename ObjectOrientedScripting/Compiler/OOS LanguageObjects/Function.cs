﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Function : pBaseLangObject, Interfaces.iName
    {
        private Ident name;
        public Ident Name { get { return name; } set { if (!name.IsSimpleIdentifier) throw new Ex.InvalidIdentType(value.getIdentType(), IdentType.Name); name = value; } }
        public VarType functionVarType;
        private int argListEnd;

        public readonly List<pBaseLangObject> ArgList { get { return this.children.GetRange(0, argListEnd); } }
        public readonly List<pBaseLangObject> CodeInstructions { get { return this.children.GetRange(argListEnd, this.children.Count - argListEnd); } }

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
                    s += it.Name;
                s += "_fnc_" + this.Name.OriginalValue;
                return s;
            }
        }
        public Function(pBaseLangObject parent) : base(parent) { }
        virtual void doFinalize() {}
        public void markArgListEnd()
        {
            argListEnd = this.children.Count;
        }
    }
}
