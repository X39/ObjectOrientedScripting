﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class OosExpression : BaseLangObject
    {
        ExpressionOperator op;
        public ExpressionOperator Op { get { return op; } set { op = value; } }
        public BaseLangObject LInstruction { get { return this.Children[0]; } set { this.Children[0] = value; } }
        public BaseLangObject RInstruction { get { return this.Children[1]; } set { this.Children[1] = value; } }
        public OosExpression()
        {
            this.addChild(null);
            this.addChild(null);
            op = ExpressionOperator.NA;
        }
    }
}
