using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Ident : pBaseLangObject, Interfaces.iName
    {
        public Ident Name { get { return this; } set { throw new Exception("Cannot set Ident of an Ident"); } }
        private string originalValue;
        public string OriginalValue { get { return this.originalValue; } }
        public bool IsSimpleIdentifier { get { return !(this.originalValue.Contains("::") || this.originalValue.Contains('.')); } }
        public bool IsGlobalIdentifier { get { return this.originalValue.StartsWith("::"); } }
        public bool IsRelativeIdentifier { get { return this.originalValue.Contains("::"); } }
        public bool HasInstanceAccess { get { return this.originalValue.Contains('.'); } }
        public bool IsSelfReference { get { return this.FullyQualifiedName.Contains("::this."); } }
        public string FullyQualifiedName
        {
            get
            {
                Interfaces.iName obj = this.getFirstOf<oosClass>();
                if (obj == null)
                    obj = this.getFirstOf<Namespace>();
                if (this.IsGlobalIdentifier)
                    return this.originalValue;
                else
                    if (obj == null)
                        return "::" + this.originalValue;
                    else
                    return obj.FullyQualifiedName + "::" + this.originalValue;
            }
        }
        private pBaseLangObject referencedObject;
        public pBaseLangObject ReferencedObject { get { return referencedObject; } }
        private VarTypeObject referencedType;
        public VarTypeObject ReferencedType { get { return referencedType; } }

        private int line;
        private int pos;

        public Ident(pBaseLangObject parent, string origVal, int line, int pos) : base(parent) 
        {
            this.originalValue = origVal;
            referencedObject = null;
            referencedType = new VarTypeObject(VarType.Void);
            this.line = line;
            this.pos = pos;
        }
        public override int doFinalize()
        {
            int errCount = 0;
            //Existing Object
            if(this.IsSelfReference)
            {
                this.referencedObject = this.getFirstOf<oosClass>();
                this.referencedType = new VarTypeObject(this);
                if (this.referencedObject == null)
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0002, this.line, this.pos));
                    errCount++;
                }
            }
            else if (this.getAllChildrenOf<FunctionCall>().Count > 0)
            {
                if (this.HasInstanceAccess)
                {
                    var fqn = this.FullyQualifiedName;
                    var variable = getVariableReferenceOfFQN(fqn.Substring(fqn.IndexOf('.')));
                    if (variable == null)
                    {
                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0003, this.line, this.pos));
                        errCount++;
                    }
                    else
                    {
                        var tuple = getFunctionReferenceOfFQN(variable.varType.ident.FullyQualifiedName);
                        if (tuple == null)
                        {
                            Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0004, this.line, this.pos));
                            errCount++;
                        }
                        else if (tuple.Item1 != null)
                        {
                            this.referencedObject = tuple.Item1;
                            this.referencedType = tuple.Item1.varType;
                        }
                        else if (tuple.Item2 != null)
                        {
                            this.referencedObject = tuple.Item2;
                            this.referencedType = tuple.Item2.varType;
                        }
                        else
                        {
                            Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0005, this.line, this.pos));
                            errCount++;
                        }
                    }
                }
                else
                {
                    var tuple = getFunctionReferenceOfFQN(this.FullyQualifiedName);
                    if (tuple == null)
                    {
                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0006, this.line, this.pos));
                        errCount++;
                    }
                    else if (tuple.Item1 != null)
                    {
                        this.referencedObject = tuple.Item1;
                        this.referencedType = tuple.Item1.varType;
                    }
                    else if (tuple.Item2 != null)
                    {
                        this.referencedObject = tuple.Item2;
                        this.referencedType = tuple.Item2.varType;
                    }
                    else
                    {
                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0007, this.line, this.pos));
                        errCount++;
                    }
                }
            }
            else if (this.Parent is Expression)
            {
                var obj = getVariableReferenceOfFQN(this.FullyQualifiedName);
                if (obj == null)
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0008, this.line, this.pos));
                    errCount++;
                }
                else
                {
                    this.referencedObject = obj;
                    this.referencedType = obj.varType;
                }
            }
            //Theoretical Object
            else if (this.Parent is NewInstance)
            {
                var varList = this.getAllChildrenOf<oosClass>(true);
                foreach (var it in varList)
                {
                    if (it.FullyQualifiedName == this.FullyQualifiedName)
                    {
                        this.referencedObject = it;
                        this.referencedType = new VarTypeObject(this);
                        break;
                    }
                }
                if (this.referencedObject == null)
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0009, this.line, this.pos));
                    errCount++;
                }
            }
            return errCount;
        }
        private Tuple<Function, VirtualFunction> getFunctionReferenceOfFQN(string fqn)
        {
            var varList = this.getFirstOf<Base>().getAllChildrenOf<Function>(true, this);
            foreach (var it in varList)
            {
                var fqn2 = it.Name.FullyQualifiedName;
                if (fqn2 == fqn)
                    return new Tuple<Function,VirtualFunction>(it, null);
                if(fqn2.StartsWith(fqn) && fqn.EndsWith(fqn2.Remove(0, fqn.Length)))
                    return new Tuple<Function, VirtualFunction>(it, null);
            }
           var varList2 = this.getFirstOf<Base>().getAllChildrenOf<VirtualFunction>(true, this);
           foreach (var it in varList2)
           {
               if (it.Name.FullyQualifiedName == fqn)
               {
                   return new Tuple<Function, VirtualFunction>(null, it);
               }
           }
           return null;
        }
        private Variable getVariableReferenceOfFQN(string fqn)
        {
            var varList = this.getFirstOf<Base>().getAllChildrenOf<Variable>(true, this);
            var newList = new List<Variable>(varList);
            foreach (var it in varList)
                if (it.encapsulation == Encapsulation.NA)
                    newList.Remove(it);
            varList = this.getFirstOf<Function>().getAllChildrenOf<Variable>(true, this);
            varList.AddRange(newList);
            foreach (var it in varList)
            {
                if (it.Name.FullyQualifiedName == fqn)
                {
                    return it;
                }
            }
            return null;
        }
        public IdentType getIdentType()
        {
            if (IsSimpleIdentifier)
                return IdentType.Name;
            if (IsGlobalIdentifier)
                if (HasInstanceAccess)
                    return IdentType.GlobalAccess_Instance;
                else
                    return IdentType.GlobalAccess;
            if (IsRelativeIdentifier)
                if (HasInstanceAccess)
                    return IdentType.RelativeAccess_Instance;
                else
                    return IdentType.RelativeAccess;
            throw new Exception("Unknown error, please report to the developer");
        }
    }
}
