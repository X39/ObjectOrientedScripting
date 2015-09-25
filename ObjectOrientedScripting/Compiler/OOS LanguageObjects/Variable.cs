using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Variable : pBaseLangObject, Interfaces.iName, Interfaces.iHasType
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
        public VarTypeObject ReferencedType { get { return this.varType; } }
        public Encapsulation encapsulation;
        public string FullyQualifiedName { get { return this.Parent + "::" + this.Name.OriginalValue; } }
        public string SqfVariableName
        {
            get
            {
                if (this.encapsulation == Encapsulation.NA)
                {
                    if (this.Name.OriginalValue == Wrapper.Compiler.thisVariableName)
                        return Wrapper.Compiler.thisVariableName;
                    else
                        return "_" + this.Name.OriginalValue;
                }
                else if (this.encapsulation == Encapsulation.Static)
                {
                    return "GLOBAL" + this.Name.FullyQualifiedName.Replace("::", "_");
                }
                else
                {
                    var casted = (Interfaces.iGetVariableIndex)this.Parent;
                    var res = casted.getVariableIndex(this.Name);
                    return " select 1 select " + res.Item1 + " select " + res.Item2;
                }
            }
        }

        private int line;
        public int Line { get { return this.line; } }
        private int pos;
        public int Pos { get { return this.pos; } }

        public Variable(pBaseLangObject parent, int pos, int line) : base(parent)
        {
            this.addChild(null);
            varType = null;
            this.line = line;
            this.pos = pos;
        }
        public override int doFinalize() {
            //ToDo: make sure we got no doubleDefine in here
            if(this.varType.varType == VarType.Object || this.varType.varType == VarType.ObjectStrict)
                this.varType.ident.finalize();
            var assign = this.getAllChildrenOf<VariableAssignment>();
            if(assign.Count > 0)
            {
                var expList = assign[0].getAllChildrenOf<Expression>();
                var newArrayList = assign[0].getAllChildrenOf<NewArray>();
                if (expList.Count <= 0 && newArrayList.Count <= 0)
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0000, this.line, this.pos));
                    return 1;
                }
                if (newArrayList.Count <= 0)
                {
                    var expression = expList[0];
                    var type = expression.ReferencedType;
                    if (this.varType.varType == VarType.Auto)
                    {
                        this.varType = type;
                    }
                    if (!this.varType.Equals(type))
                    {
                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0001, this.line, this.pos));
                        return 1;
                    }
                }
                else
                {
                    var arr = newArrayList[0];
                    var type = arr.ReferencedType;
                    if (this.varType.varType == VarType.Auto)
                    {
                        this.varType = type;
                    }
                    if (!this.varType.Equals(type))
                    {
                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0001, this.line, this.pos));
                        return 1;
                    }
                }
            }
            return 0;
        }
        public override string ToString()
        {
            return this.FullyQualifiedName;
        }
    }
}
