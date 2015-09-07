using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Variable : pBaseLangObject, Interfaces.iName
    {
        public Ident Name
        {
            get { return ((Ident)this.children[0]); }
            set
            {
                if (this.encapsulation == Encapsulation.NA && !value.IsSimpleIdentifier)
                    throw new Ex.InvalidIdentType(value.getIdentType(), IdentType.Name);
                this.children[0] = value;
            }
        }
        public VarTypeObject varType;
        public Encapsulation encapsulation;
        public string FullyQualifiedName
        {
            get
            {
                if (this.encapsulation == Encapsulation.NA)
                    return this.Name.OriginalValue;
                string s = "";
                List<Interfaces.iName> parentList = new List<Interfaces.iName>();
                pBaseLangObject curParent = Parent;
                while (curParent != null)
                {
                    if(curParent is Interfaces.iName)
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

        private int line;
        private int pos;

        public Variable(pBaseLangObject parent, int line, int pos) : base(parent)
        {
            this.addChild(null);
            varType = null;
            this.line = line;
            this.pos = pos;
        }
        public override int doFinalize() {
            var assign = this.getAllChildrenOf<VariableAssignment>();
            if(assign.Count > 0)
            {
                var expList = assign[0].getAllChildrenOf<Expression>();
                if (expList.Count <= 0)
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0000, this.line, this.pos));
                    return 1;
                }
                var expression = expList[0];
                var type = expression.ExpressionType;
                if(this.varType.varType == VarType.Auto)
                {
                    this.varType = type;
                }
                if(!this.varType.Equals(type))
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0001, this.line, this.pos));
                    return 1;
                }
            }
            return 0;
        }
    }
}
