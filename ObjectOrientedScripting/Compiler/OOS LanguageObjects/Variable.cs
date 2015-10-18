using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Variable : pBaseLangObject, Interfaces.iName, Interfaces.iHasType, Interfaces.iTemplate
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
        public pBaseLangObject Value { get { var valAssign = this.getAllChildrenOf<VariableAssignment>(); if (valAssign.Count > 0) return valAssign[0]; return null; } }
        public bool IsClassVariable { get { return this.encapsulation != Encapsulation.Static && this.encapsulation != Encapsulation.NA; } }
        public Template template { get; set; }
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
                    return " select 1 select " + res.Item1 + " select 1 select " + res.Item2;
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
            int errCount = 0;
            var varList = this.Parent.getAllChildrenOf<Variable>(false, this, 0);
            foreach(var it in varList)
            {
                if(it.Name.FullyQualifiedName == this.Name.FullyQualifiedName)
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0041, this.line, this.pos));
                    errCount++;
                }
            }
            if (this.varType.varType == VarType.Object || this.varType.varType == VarType.ObjectStrict)
            {
                this.varType.ident.finalize();
                if(this.varType.ident.ReferencedObject is oosInterface && this.varType.varType == VarType.ObjectStrict)
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0046, this.line, this.pos));
                    errCount++;
                }
            }
            var assign = this.getAllChildrenOf<VariableAssignment>();
            var newInstance = this.getAllChildrenOf<NewInstance>(true);
            if (newInstance.Count > 0)
            {
                this.template = newInstance[0].template;
            }
            if(assign.Count > 0)
            {
                var expList = assign[0].getAllChildrenOf<Expression>();
                var newArrayList = assign[0].getAllChildrenOf<NewArray>();
                if (expList.Count <= 0 && newArrayList.Count <= 0)
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0000, this.line, this.pos));
                    errCount++;
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
                        errCount++;
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
                        errCount++;
                    }
                }
            }
            return errCount;
        }
        public override string ToString()
        {
            return this.FullyQualifiedName;
        }
    }
}
