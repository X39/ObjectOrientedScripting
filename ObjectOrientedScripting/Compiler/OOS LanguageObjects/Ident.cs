using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Ident : pBaseLangObject, Interfaces.iName, Interfaces.iHasType, Interfaces.iHasObject
    {
        public Ident Name { get { return this; } set { throw new Exception("Cannot set Ident of an Ident"); } }
        private string originalValue;
        public string OriginalValue { get { return this.originalValue; } }
        public bool IsSimpleIdentifier { get { return !this.originalValue.Contains("::"); } }
        public bool IsGlobalIdentifier { get { return this.originalValue.StartsWith("::"); } }
        public bool IsRelativeIdentifier { get { return this.originalValue.Contains("::"); } }
        public bool IsSelfReference { get { return this.FullyQualifiedName.Contains("::this"); } }
        public pBaseLangObject Instruction
        {
            get
            {
                pBaseLangObject firstChild = (this.children.Count > 0 ? this.children[0] : null);
                if (firstChild is Ident)
                    return ((Ident)firstChild).Instruction;
                else
                    return firstChild;
            }
        }
        public Ident NextIdent
        {
            get
            {
                pBaseLangObject firstChild = (this.children.Count > 0 ? this.children[0] : null);
                if (firstChild is Ident)
                {
                    return (Ident)firstChild;
                }
                else
                {
                    if (this.children.Count > 1)
                    {
                        if (this.children[1] is Ident)
                        {
                            return (Ident)this.children[1];
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public FunctionCall NextFunctionCall
        {
            get
            {
                pBaseLangObject firstChild = (this.children.Count > 0 ? this.children[0] : null);
                if(firstChild == null)
                {
                    return null;
                }
                else if (firstChild is Ident)
                {
                    return ((Ident)firstChild).NextFunctionCall;
                }
                else
                {
                    if (firstChild is FunctionCall)
                    {
                        return (FunctionCall)firstChild;
                    }
                    else if (firstChild is Ident)
                    {
                        return ((Ident)firstChild).NextFunctionCall;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
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
                {
                    if (obj == null)
                    {
                        return "::" + this.originalValue;
                    }
                    else
                    {
                        if (this.Parent is oosClass || this.Parent is oosInterface)
                        {
                            obj = this.getFirstOf<Namespace>();
                            return (obj == null ? "" : obj.FullyQualifiedName) + "::" + this.originalValue;
                        }
                        else
                        {
                            return obj.FullyQualifiedName + "::" + this.originalValue;
                        }
                    }
                }
            }
        }
        public string CurrentNamespace
        {
            get
            {
                Interfaces.iName obj = this.getFirstOf<oosClass>();
                if (obj == null)
                    obj = this.getFirstOf<Namespace>();
                if (this.IsGlobalIdentifier)
                    return this.originalValue.Substring(0, this.originalValue.LastIndexOf("::"));
                else
                    if (obj == null)
                        return "::";
                    else
                        return obj.FullyQualifiedName;
            }
        }
        public string Append { set { this.originalValue += value; } }
        private pBaseLangObject referencedObject;
        private pBaseLangObject thisReferencedObject;
        public pBaseLangObject ReferencedObject { get { return referencedObject; } }
        public pBaseLangObject ThisReferencedObject { get { return thisReferencedObject; } }
        private VarTypeObject referencedType;
        private VarTypeObject thisReferencedType;
        public VarTypeObject ReferencedType { get { return referencedType; } }
        public VarTypeObject ThisReferencedType { get { return thisReferencedType; } }

        private int line;
        public int Line { get { return this.line; } }
        private int pos;
        public int Pos { get { return this.pos; } }

        public Ident(pBaseLangObject parent, string origVal, int line, int pos) : base(parent) 
        {
            this.originalValue = origVal;
            referencedObject = null;
            referencedType = new VarTypeObject(VarType.Void);
            this.line = line;
            this.pos = pos;
            isFirstFinalize = true;
        }
        private bool isFirstFinalize;
        public override int finalize()
        {
            int errCount = 0;
            errCount += this.doFinalize();
            isFirstFinalize = false;
            foreach (pBaseLangObject blo in children)
                if (blo != null && blo is Ident)
                    errCount += blo.finalize();
            errCount += this.doFinalize();
            foreach (pBaseLangObject blo in children)
                if (blo != null && !(blo is Ident))
                    errCount += blo.finalize();
            return errCount;
        }
        public override int doFinalize()
        {
            int errCount = 0;
            pBaseLangObject firstChild = (this.children.Count > 0 ? this.children[0] : null);
            if (isFirstFinalize)
            {
                if (this.Parent is SqfCall)
                    return errCount;
                else if(this.Parent is oosClass)
                {
                    if(this.FullyQualifiedName.Equals(((oosClass)this.Parent).Name.FullyQualifiedName))
                    {
                        this.referencedObject = this.Parent;
                        this.referencedType = new VarTypeObject(this);
                        this.thisReferencedObject = this.Parent;
                        this.thisReferencedType = new VarTypeObject(this);
                        return errCount;
                    }
                    else
                    {
                        string fqn = CurrentNamespace;
                        fqn = fqn.Substring(0, fqn.LastIndexOf("::"));
                        fqn += "::" + this.originalValue;
                        var tuple = getClassReferenceOfFQN(fqn);
                        if (tuple == null)
                        {
                            Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0029, this.line, this.pos));
                            errCount++;
                        }
                        else if (tuple.Item1 != null)
                        {
                            this.referencedObject = tuple.Item1;
                            this.referencedType = new VarTypeObject(tuple.Item1.Name);
                            this.thisReferencedObject = tuple.Item1;
                            this.thisReferencedType = new VarTypeObject(tuple.Item1.Name);
                        }
                        else if (tuple.Item2 != null)
                        {
                            this.referencedObject = tuple.Item2;
                            this.referencedType = new VarTypeObject(tuple.Item2.Name);
                            this.thisReferencedObject = tuple.Item2;
                            this.thisReferencedType = new VarTypeObject(tuple.Item2.Name);
                        }
                        else
                        {
                            Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0029, this.line, this.pos));
                            errCount++;
                        }
                        return errCount;
                    }
                }
                else if (this.Parent is Namespace || this.Parent is VirtualFunction || this.Parent is Variable || this.Parent is Function || this.Parent is oosInterface)
                {
                    this.referencedObject = this.Parent;
                    this.referencedType = new VarTypeObject(this);
                    this.thisReferencedObject = this.Parent;
                    this.thisReferencedType = new VarTypeObject(this);
                    return errCount;
                }
                if (this.IsSelfReference)
                {
                    var fnc = this.getFirstOf<Function>();
                    if (fnc.encapsulation == Encapsulation.Static || fnc.encapsulation == Encapsulation.NA)
                    {
                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0009, this.line, this.pos));
                        errCount++;
                    }
                    else
                    {
                        this.referencedObject = this.getFirstOf<oosClass>();
                        this.referencedType = new VarTypeObject(this);
                        this.thisReferencedObject = this.getFirstOf<oosClass>();
                        this.thisReferencedType = new VarTypeObject(this);
                        if (this.referencedObject == null)
                        {
                            Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0002, this.line, this.pos));
                            errCount++;
                        }
                    }
                }
                else
                {
                    if (this.Parent is Ident)
                    {
                        var refType = ((Ident)this.Parent).ReferencedType;
                        if (refType.varType != VarType.Object && refType.varType != VarType.ObjectStrict)
                        {
                            Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0014, this.line, this.pos));
                            errCount++;
                        }
                        else
                        {
                            var obj = refType.ident.ReferencedObject;
                            if (firstChild is FunctionCall)
                            {
                                var tuple = getFunctionReferenceOfFQN(((Interfaces.iName)obj).Name.FullyQualifiedName + "::" + this.originalValue);
                                if (tuple == null)
                                {
                                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0004, this.line, this.pos));
                                    errCount++;
                                }
                                else if (tuple.Item1 != null)
                                {
                                    this.referencedObject = tuple.Item1;
                                    this.referencedType = tuple.Item1.varType;
                                    this.thisReferencedObject = tuple.Item1;
                                    this.thisReferencedType = tuple.Item1.varType;
                                }
                                else if (tuple.Item2 != null)
                                {
                                    this.referencedObject = tuple.Item2;
                                    this.referencedType = tuple.Item2.varType;
                                    this.thisReferencedObject = tuple.Item2;
                                    this.thisReferencedType = tuple.Item2.varType;
                                }
                                else
                                {
                                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0005, this.line, this.pos));
                                    errCount++;
                                }
                            }
                            else if (firstChild == null || firstChild is ArrayAccess || firstChild is Cast || firstChild is Ident || firstChild is VariableAssignment)
                            {
                                Variable variable;
                                if(obj is Variable)
                                    variable = getVariableReferenceOfFQN(((Variable)obj).ReferencedType.ident.FullyQualifiedName + "::" + this.originalValue, false);
                                else
                                    variable = getVariableReferenceOfFQN(((Interfaces.iName)obj).Name.FullyQualifiedName + "::" + this.originalValue, false); ;
                                if (variable == null)
                                {
                                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0003, this.line, this.pos));
                                    errCount++;
                                }
                                else
                                {
                                    this.referencedObject = variable;
                                    this.referencedType = variable.ReferencedType;
                                    this.thisReferencedObject = variable;
                                    this.thisReferencedType = variable.ReferencedType;
                                }
                            }
                            else
                            {
                                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.UNKNOWN, this.line, this.pos));
                                errCount++;
                            }
                        }
                    }
                    else
                    {
                        if (firstChild is FunctionCall)
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
                                this.thisReferencedObject = tuple.Item1;
                                this.thisReferencedType = tuple.Item1.varType;
                            }
                            else if (tuple.Item2 != null)
                            {
                                this.referencedObject = tuple.Item2;
                                this.referencedType = tuple.Item2.varType;
                                this.thisReferencedObject = tuple.Item2;
                                this.thisReferencedType = tuple.Item2.varType;
                            }
                            else
                            {
                                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0007, this.line, this.pos));
                                errCount++;
                            }
                        }
                        else if (firstChild == null || firstChild is ArrayAccess || firstChild is Cast || firstChild is Ident || firstChild is VariableAssignment)
                        {
                            var variable = getVariableReferenceOfFQN(this.FullyQualifiedName);
                            if (variable == null)
                            {
                                var tuple = getClassReferenceOfFQN(this.FullyQualifiedName);
                                if (tuple == null)
                                {
                                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0008, this.line, this.pos));
                                    errCount++;
                                }
                                else if (tuple.Item1 != null)
                                {
                                    this.referencedObject = tuple.Item1;
                                    this.referencedType = new VarTypeObject(tuple.Item1.Name);
                                    this.thisReferencedObject = tuple.Item1;
                                    this.thisReferencedType = new VarTypeObject(tuple.Item1.Name);
                                }
                                else if (tuple.Item2 != null)
                                {
                                    this.referencedObject = tuple.Item2;
                                    this.referencedType = new VarTypeObject(tuple.Item2.Name);
                                    this.thisReferencedObject = tuple.Item2;
                                    this.thisReferencedType = new VarTypeObject(tuple.Item2.Name);
                                }
                                else
                                {
                                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0008, this.line, this.pos));
                                    errCount++;
                                }
                            }
                            else
                            {
                                
                                if(variable.Parent is oosClass && variable.encapsulation != Encapsulation.Static)
                                {
                                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0030, this.line, this.pos));
                                    errCount++;
                                }
                                this.referencedObject = variable;
                                this.referencedType = variable.ReferencedType;
                                this.thisReferencedObject = variable;
                                this.thisReferencedType = variable.ReferencedType;
                            }
                        }
                        else
                        {
                            Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.UNKNOWN, this.line, this.pos));
                            errCount++;
                        }
                    }
                }
            }
            else
            {
                if (firstChild != null && firstChild is Ident)
                {
                    this.referencedObject = ((Ident)firstChild).ReferencedObject;
                    this.referencedType = ((Ident)firstChild).ReferencedType;
                }
            }
            return errCount;
        }
        private Tuple<Function, VirtualFunction> getFunctionReferenceOfFQN(string fqn)
        {
            var varList = this.getFirstOf<Base>().getAllChildrenOf<Function>(true);
            foreach (var it in varList)
            {
                var fqn2 = it.Name.FullyQualifiedName;
                if (fqn2 == fqn)
                    return new Tuple<Function, VirtualFunction>(it, null);
                if (fqn2.StartsWith(fqn) && fqn.EndsWith(fqn2.Remove(0, fqn.Length)))
                    return new Tuple<Function, VirtualFunction>(it, null);
            }
            var varList2 = this.getFirstOf<Base>().getAllChildrenOf<VirtualFunction>(true);
            foreach (var it in varList2)
            {
                if (it.Name.FullyQualifiedName == fqn)
                {
                    return new Tuple<Function, VirtualFunction>(null, it);
                }
            }
            return null;
        }
        private Tuple<oosClass, oosInterface> getClassReferenceOfFQN(string fqn)
        {
            var varList = this.getFirstOf<Base>().getAllChildrenOf<oosClass>(true);
            foreach (var it in varList)
            {
                var fqn2 = it.Name.FullyQualifiedName;
                if (fqn2 == fqn)
                    return new Tuple<oosClass, oosInterface>(it, null);
                if (fqn2.StartsWith(fqn) && fqn.EndsWith(fqn2.Remove(0, fqn.Length)))
                    return new Tuple<oosClass, oosInterface>(it, null);
            }
            var varList2 = this.getFirstOf<Base>().getAllChildrenOf<oosInterface>(true, this);
            foreach (var it in varList2)
            {
                if (it.Name.FullyQualifiedName == fqn)
                {
                    return new Tuple<oosClass, oosInterface>(null, it);
                }
            }
            return null;
        }
        private Variable getVariableReferenceOfFQN(string fqn, bool localVariables = true)
        {
            var varList = this.getFirstOf<Base>().getAllChildrenOf<Variable>(true);
            var newList = new List<Variable>(varList);
            foreach (var it in varList)
                if (it.encapsulation == Encapsulation.NA)
                    newList.Remove(it);
            if (localVariables)
            {
                varList = this.getFirstOf<Function>().getAllChildrenOf<Variable>(true, this);
                varList.AddRange(newList);
            }
            else
            {
                varList = newList;
            }
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
                    return IdentType.GlobalAccess;
            if (IsRelativeIdentifier)
                    return IdentType.RelativeAccess;
            throw new Exception("Unknown error, please report to the developer");
        }
        public override string ToString()
        {
            return this.FullyQualifiedName;
        }
    }
}
