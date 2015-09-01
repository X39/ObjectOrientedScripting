﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class TryCatch : pBaseLangObject
    {
        public VarType functionVarType;
        private int ifEnd;

        public pBaseLangObject expression { get { return this.children[0]; } set { this.children[0] = value; } }
        public readonly List<pBaseLangObject> IfInstructions { get { return this.children.GetRange(1, ifEnd); } }
        public readonly List<pBaseLangObject> ElseInstructions { get { return this.children.GetRange(ifEnd, this.children.Count - ifEnd); } }

        public TryCatch(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
        }
        public override void doFinalize() { }
        public void markIfEnd()
        {
            ifEnd = this.children.Count;
        }
    }
}
