using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Expression : pBaseLangObject
    {
        public pBaseLangObject lExpression { get { return this.children[0]; } set { this.children[0] = value; } }
        public pBaseLangObject rExpression { get { return this.children[1]; } set { this.children[1] = value; } }
        public VarTypeObject ExpressionType
        {
            get
            {
                VarTypeObject lType = null;
                VarTypeObject rType = null;
                VarTypeObject oType = null;
                if (lExpression is Expression)
                {
                    lType = ((Expression)lExpression).ExpressionType;
                }
                else if (lExpression is Variable)
                {
                    lType = ((Variable)lExpression).varType;
                }
                else if (lExpression is Ident)
                {
                    lType = ((Ident)lExpression).ReferencedType;
                }
                else if (lExpression is NewInstance)
                {
                    lType = ((Ident)lExpression.children[0]).ReferencedType;
                }
                else if (lExpression is SqfCall)
                {
                    throw new NotImplementedException();
                }
                if (rExpression != null)
                {
                    if (rExpression is Expression)
                    {
                        rType = ((Expression)rExpression).ExpressionType;
                    }
                    else if (rExpression is Variable)
                    {
                        rType = ((Variable)rExpression).varType;
                    }
                    else if (rExpression is Ident)
                    {
                        rType = ((Ident)rExpression).ReferencedType;
                    }
                    else if (rExpression is NewInstance)
                    {
                        rType = ((Ident)rExpression.children[0]).ReferencedType;
                    }
                    else if (rExpression is SqfCall)
                    {
                        throw new NotImplementedException();
                    }
                }
                switch (expOperator)
                {
                    case "&": case "&&": case "|": case "||":
                        if (lType.varType != VarType.Bool || rType.varType != VarType.Bool)
                            throw new Ex.TypeMissmatch(lType, rType, line, pos, expOperator);
                        oType = new VarTypeObject(VarType.Bool);
                        break;
                    case "==": case "===":
                        oType = new VarTypeObject(VarType.Bool);
                        break;
                    case "+": case "-": case "*": case "/":
                        if (lType.varType != VarType.Scalar || rType.varType != VarType.Scalar)
                            throw new Ex.TypeMissmatch(lType, rType, line, pos, expOperator);
                        oType = new VarTypeObject(VarType.Scalar);
                        break;
                    case ">": case ">=": case "<": case "<=":
                        if (lType.varType != VarType.Scalar || rType.varType != VarType.Scalar)
                            throw new Ex.TypeMissmatch(lType, rType, line, pos, expOperator);
                        oType = new VarTypeObject(VarType.Bool);
                        break;
                    case "":
                        return lType;
                    default:
                        throw new NotImplementedException();
                }
                return oType;
            }
        }
        public string expOperator;
        public bool negate;

        private int line;
        private int pos;

        public Expression(pBaseLangObject parent, int line, int pos) : base(parent)
        {
            this.children.Add(null);
            this.children.Add(null);
            negate = false;
            expOperator = "";
        }
        public override int doFinalize() { return 0; }
    }
}
