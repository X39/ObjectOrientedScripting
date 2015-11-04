using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Ident : pBaseLangObject, Interfaces.iHasType, Interfaces.iHasObject
    {
        public enum IdenType
        {
            VariableAccess,
            ArrayAccess,
            FunctionCall,
            NamespaceAccess,
            ThisVar,
            TemplateVar,
            SqfCommandName
        }
        public enum AccessType
        {
            Instance,
            Namespace,
            NA
        }

        public bool IsSimpleIdentifier { get { return !(this.Parent is Ident) && this.Access == AccessType.NA && this.getAllChildrenOf<Ident>().Count == 0; } }
        private bool isGlobalIdentifier;
        public bool IsGlobalIdentifier { get { return this.isGlobalIdentifier; } set { if (value && this.Parent is Ident) throw new Exception("Double Namespace Access experienced"); this.isGlobalIdentifier = value; } }
        public bool IsRelativeIdentifier { get { return this.Access == AccessType.Namespace; } }
        public bool IsSelfReference { get { return this.originalValue == "this"; } }
        public bool IsAnonymousIdent
        {
            get
            {
                var templateList = this.getAllParentsOf<Interfaces.iTemplate>();
                if (this.Parent is Template)
                {
                    return true;
                }
                else if (templateList.Count > 0 && !(this.Parent is Ident) && this.getAllChildrenOf<Ident>().Count == 0)
                {
                    foreach (var template in templateList)
                    {
                        if (template.TemplateObject == null)
                            continue;
                        foreach (var it in template.TemplateObject.vtoList)
                        {
                            if (it.ident != null && it.ident.originalValue == this.originalValue)
                                return true;
                        }
                    }
                }
                return false;
            }
        }
        public string FullyQualifiedName
        {
            get
            {
                var templateList = this.getAllParentsOf<Interfaces.iTemplate>();
                if (this.Parent is Template)
                {
                    return this.originalValue;
                }
                else if (templateList.Count > 0 && !(this.Parent is Ident) && this.getAllChildrenOf<Ident>().Count == 0)
                {
                    foreach (var template in templateList)
                    {
                        if (template.TemplateObject == null)
                            continue;
                        foreach (var it in template.TemplateObject.vtoList)
                        {
                            if (it.ident != null && it.ident.originalValue == this.originalValue)
                                return this.originalValue;
                        }
                    }
                }
                if (this.IsSelfReference && this.ReferencedObject != null && this.ReferencedObject is Interfaces.iName)
                {
                    return ((Interfaces.iName)this.ReferencedObject).Name.FullyQualifiedName;
                }
                else
                {
                    if (this.Parent is Ident && (((Ident)this.Parent).Access == AccessType.Instance) && ((Ident)this.Parent).ReferencedType != null)
                    {
                        return ((Ident)this.Parent).ReferencedType.ident.FullyQualifiedName + "." + this.originalValue;
                    }
                    string result = "";
                    pBaseLangObject curobject = this;
                    do
                    {
                        if (
                            !(curobject is Interfaces.iName ||
                            curobject is Ident) ||
                            curobject is SqfCall ||
                            (!string.IsNullOrEmpty(result) && (curobject is Variable || curobject is Interfaces.iFunction || curobject is AssignContainer))
                            )
                        {
                            //Do nothing
                        }
                        else if (curobject is Template || (curobject is Interfaces.iFunction && ((Interfaces.iFunction)curobject).ReturnType.ident == this))
                        {
                            curobject = curobject.Parent;
                        }
                        else if (curobject is Ident)
                        {
                            if (curobject.Parent is Interfaces.iName && ((Interfaces.iName)curobject.Parent).Name == this && !(curobject.Parent is NewInstance))
                            {
                                //Do nothing
                            }
                            else if (((Ident)curobject).IsSelfReference)
                            {
                                result = ((Ident)curobject).FullyQualifiedName + (((Ident)curobject).Access == AccessType.Namespace ? "::" : ((Ident)curobject).Access == AccessType.Instance ? "." : "") + result;
                                return result.TrimEnd(new char[] { ':', '.' });
                            }
                            else
                            {
                                result = (((Ident)curobject).IsSelfReference ? ((Ident)curobject).FullyQualifiedName : ((Ident)curobject).OriginalValue) +
                                    (((Ident)curobject).Access == AccessType.Namespace ? "::" : ((Ident)curobject).Access == AccessType.Instance ? "." : "") + result;
                            }
                            if (((Ident)curobject).isGlobalIdentifier)
                                break;
                        }
                        else if (curobject is Interfaces.iName)
                        {
                            var tmpIdent = ((Interfaces.iName)curobject).Name;
                            result = tmpIdent.OriginalValue + "::" + result;
                        }
                        else
                        {
                            throw new Exception();
                        }
                        curobject = curobject.Parent;
                    } while (!(curobject is Base) && curobject != null);
                    return result.TrimEnd(new char[] { ':', '.' });
                }
            }
        }

        private string originalValue;
        private pBaseLangObject referencedObject;
        private VarTypeObject referencedType;
        private IdenType type;

        public pBaseLangObject ReferencedObject { get { return referencedObject; } }
        public VarTypeObject ReferencedType { get { return referencedType; } }
        public IdenType Type { get { return this.type; } }
        public string OriginalValue { get { return this.originalValue; } }
        public int Line { get; internal set; }
        public int Pos { get; internal set; }
        public AccessType Access { get; set; }

        public Ident LastIdent
        {
            get
            {
                var list = this.getAllChildrenOf<Ident>();
                if (list.Count > 0)
                    return list[0].LastIdent;
                else
                    return this;
            }
        }
        public Ident(pBaseLangObject parent, string origVal, int line, int pos) : base(parent)
        {
            this.originalValue = origVal;
            referencedObject = null;
            referencedType = new VarTypeObject(VarType.Void);
            this.Line = line;
            this.Pos = pos;
            this.Access = AccessType.NA;
            this.isGlobalIdentifier = false;
        }
        private bool isFirstFinalize = true;
        public override int finalize()
        {
            if (this.IsFinalized)
                return 0;
            int errCount = 0;
            if (this.isFirstFinalize)
            {
                this.isFirstFinalize = false;
                foreach (pBaseLangObject blo in children)
                    if (blo != null)
                        errCount += blo.finalize();
                if (this is Interfaces.iTemplate && ((Interfaces.iTemplate)this).TemplateObject != null)
                    errCount += ((Interfaces.iTemplate)this).TemplateObject.finalize();
                if (this is Interfaces.iHasType && ((Interfaces.iHasType)this).ReferencedType.IsObject)
                    errCount += ((Interfaces.iHasType)this).ReferencedType.ident.finalize();
            }
            else
            {
                this.IsFinalized = true;
                errCount += this.doFinalize();
                foreach (pBaseLangObject blo in children)
                    if (blo is Ident)
                        errCount += blo.finalize();
            }
            if (!(this.Parent is Ident))
                this.finalize(); 
            return errCount;
        }
        public override int doFinalize()
        {
            int errCount = 0;
            Ident parentIdent = (this.Parent is Ident ? (Ident)this.Parent : default(Ident));
            //Check what we have here (possible outcomes: variableacces, arrayaccess, functioncall, namespaceaccess)
            #region typeDetection
            type = IdenType.NamespaceAccess;
            var fncCalls = this.getAllChildrenOf<FunctionCall>();
            var arrAccess = this.getAllChildrenOf<ArrayAccess>();
            if (this.Parent is Template)
            {
                type = IdenType.TemplateVar;
            }
            else if (this.Parent is SqfCall && ((SqfCall)this.Parent).Name == this)
            {
                type = IdenType.SqfCommandName;
            }
            else if (this.IsSelfReference)
            {
                type = IdenType.ThisVar;
            }
            else if (fncCalls.Count > 0)
            {
                type = IdenType.FunctionCall;
            }
            else if (arrAccess.Count > 0)
            {
                type = IdenType.ArrayAccess;
            }
            else if (parentIdent == null && this.Access == AccessType.NA)
            {
                if (this.Parent is Interfaces.iClass || (this.Parent is Interfaces.iFunction && ((Interfaces.iFunction)this.Parent).ReturnType.ident == this))
                    type = IdenType.NamespaceAccess;
                else
                    type = IdenType.VariableAccess;
            }
            else if (parentIdent != null && (
                   (parentIdent.Access == AccessType.Namespace && this.Access == AccessType.NA) ||
                   this.Access == AccessType.Instance ||
                   (parentIdent.Access == AccessType.Instance && this.Access == AccessType.NA)
               ))
            {
                var ntr = HelperClasses.NamespaceResolver.createNSR(this);
                if (ntr.Reference is Interfaces.iClass)
                    type = IdenType.NamespaceAccess;
                else
                    type = IdenType.VariableAccess;
            }
            else if (this.Access == AccessType.Namespace)
            {
                type = IdenType.NamespaceAccess;
            }
            else if (this.Access == AccessType.Instance && parentIdent == null)
            {
                type = IdenType.VariableAccess;
            }
            else
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.UNKNOWN, this.Line, this.Pos));
                errCount++;
            }
            #endregion
            //And process it then ((unless its a simple ident, then we do not want to process ... here any further))
            if (this.IsSimpleIdentifier && ((this.Parent is Interfaces.iName && ((Interfaces.iName)this.Parent).Name == this) || (this.Parent is Interfaces.iHasType && !(this.Parent is Expression) && ((Interfaces.iHasType)this.Parent).ReferencedType.ident == this)))
            {
                this.referencedObject = this.Parent;
                if (this.Parent is Interfaces.iHasType)
                    this.referencedType = ((Interfaces.iHasType)this.Parent).ReferencedType;
                else //todo: try to replace with proper refObject type
                    this.referencedType = new VarTypeObject(this, true, (this.Parent is Interfaces.iTemplate ? ((Interfaces.iTemplate)this.Parent).TemplateObject : null) );
            }
            else
            {
                switch (type)
                {
                    #region ThisVar
                    case IdenType.ThisVar:
                        {
                            Interfaces.iClass curObject = this.getFirstOf<Interfaces.iClass>();
                            this.referencedObject = (pBaseLangObject)curObject;
                            this.referencedType = curObject.VTO;
                        }
                        break;
                    #endregion
                    #region TemplateVar
                    case IdenType.TemplateVar:
                        {
                            this.referencedObject = (pBaseLangObject)this.getFirstOf<Interfaces.iClass>();
                            this.referencedType = ((Interfaces.iClass)this.referencedObject).VTO;
                        }
                        break;
                    #endregion
                    #region SqfCommandName
                    case IdenType.SqfCommandName:
                        {
                            this.referencedObject = this.Parent;
                            this.referencedType = ((SqfCall)this.Parent).ReferencedType;
                        }
                        break;
                    #endregion
                    #region VariableAccess & ArrayAccess
                    case IdenType.VariableAccess:
                    case IdenType.ArrayAccess:
                        {
                            var variable = HelperClasses.NamespaceResolver.getVariableReferenceOfFQN(HelperClasses.NamespaceResolver.createNSR(this,true), true, this);
                            if (variable == null)
                            {
                                variable = HelperClasses.NamespaceResolver.getVariableReferenceOfFQN(this, false, this);
                            }
                            if (variable == null)
                            {
                                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0012, this.Line, this.Pos));
                                errCount++;
                            }
                            this.referencedObject = variable;
                            //Set type to variable type
                            this.referencedType = variable.varType;

                            if (type == IdenType.ArrayAccess)
                            {
                                if (variable.ReferencedType.IsObject)
                                {
                                    //Check if given object is implementing the ArrayAccess operator
                                    if (variable.ReferencedType.ident.LastIdent.referencedObject is Interfaces.iClass)
                                    {
                                        Interfaces.iClass classRef = (Interfaces.iClass)variable.ReferencedType.ident.LastIdent.referencedObject;
                                        Interfaces.iOperatorFunction opFnc = classRef.getOperatorFunction(OverridableOperator.ArrayAccess);
                                        if (opFnc == null)
                                        {
                                            Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0005, this.Line, this.Pos));
                                            errCount++;
                                        }
                                        else
                                        {
                                            this.referencedType = opFnc.ReturnType;
                                            if (variable.TemplateObject != null)
                                            {
                                                var templateList = ((pBaseLangObject)opFnc).getAllParentsOf<Interfaces.iTemplate>();
                                                foreach (var it in templateList)
                                                {
                                                    var tmp = HelperClasses.ArgList.resolveVarTypeObject(opFnc.ReturnType, it.TemplateObject, variable.TemplateObject);
                                                    if (tmp != opFnc.ReturnType)
                                                    {
                                                        this.referencedType = tmp;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.UNKNOWN, this.Line, this.Pos));
                                        errCount++;
                                    }
                                }
                                else
                                {
                                    //just check if this is an array type
                                    switch (this.referencedType.varType)
                                    {
                                        case VarType.BoolArray:
                                        case VarType.ScalarArray:
                                        case VarType.StringArray:
                                            break;
                                        default:
                                            Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0006, this.Line, this.Pos));
                                            errCount++;
                                            break;
                                    }
                                }
                            }
                        }
                        break;
                    #endregion
                    #region FunctionCall
                    case IdenType.FunctionCall:
                        {
                            List<Interfaces.iFunction> fncList;
                            var fncCall = fncCalls[0];
                            var newInstance = this.getFirstOf<NewInstance>();
                            var fqn = this.FullyQualifiedName;
                            if (parentIdent != null && parentIdent.ReferencedObject is Variable)
                            {
                                //if (((Variable)parentIdent.ReferencedObject).ReferencedType.IsObject)
                                //    fqn = ((Interfaces.iClass)((Variable)parentIdent.ReferencedObject).ReferencedType.ident.LastIdent.ReferencedObject).Name.LastIdent.FullyQualifiedName + "." + this.originalValue;
                                //else
                                    fqn = parentIdent.ReferencedType.ident.LastIdent.referencedType.ident.LastIdent.FullyQualifiedName + "." + this.originalValue;
                            }
                            if (newInstance == null)
                            {
                                fncList = HelperClasses.NamespaceResolver.getFunctionReferenceOfFQN(HelperClasses.NamespaceResolver.createNSR(fqn));
                            }
                            else
                            {
                                fncList = HelperClasses.NamespaceResolver.getFunctionReferenceOfFQN(HelperClasses.NamespaceResolver.createNSR(fqn + "::" + this.originalValue));
                            }
                            if (fncList.Count == 0)
                            {
                                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.UNKNOWN, this.Line, this.Pos));
                                errCount++;
                            }
                            else
                            {
                                //Search the correct function in the possible matches
                                Interfaces.iFunction fnc = null;
                                foreach (var it in fncList)
                                {
                                    if (HelperClasses.ArgList.matchesArglist(it.ArgList, fncCall.ArgList, (parentIdent.ReferencedObject is Variable ? (Variable)parentIdent.ReferencedObject : null)))
                                    {
                                        fnc = it;
                                        break;
                                    }
                                }
                                //Raise new linker issue if we could not locate a matching function
                                if (fnc == null)
                                {
                                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0002, this.Line, this.Pos));
                                    errCount++;
                                }
                                else
                                {
                                    if(fnc is Function && ((Function)fnc).IsConstructor && this.getFirstOf<NewInstance>() == null)
                                    {
                                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0026, this.Line, this.Pos));
                                        errCount++;
                                    }
                                    //Ref the object to the function
                                    this.referencedObject = (pBaseLangObject)fnc;
                                    //Ref the type to the return type
                                    this.referencedType = fnc.ReturnType;

                                    //As last step make sure we got the correct encapsulation here
                                    var enc = fnc.FunctionEncapsulation;
                                    if (enc != Encapsulation.Static && enc != Encapsulation.Public)
                                    {
                                        var parentClass = this.getFirstOf<Interfaces.iClass>();
                                        HelperClasses.NamespaceResolver fncNsr = fnc.Name;
                                        if (enc == Encapsulation.Private)
                                        {
                                            //Private encapsulation just requires checking the current class we are operating in
                                            if (!fncNsr.isInNamespace(parentClass.Name))
                                            {
                                                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0003, this.Line, this.Pos));
                                                errCount++;
                                            }
                                        }
                                        else
                                        {
                                            //Protected we need to check ALL extended classes ...
                                            var classes = parentClass.ExtendedClasses;
                                            bool flag = false;
                                            foreach (var it in classes)
                                            {
                                                if (fncNsr.isInNamespace(it))
                                                {
                                                    flag = true;
                                                    break;
                                                }
                                            }
                                            if (!flag)
                                            {
                                                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0004, this.Line, this.Pos));
                                                errCount++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    #endregion
                    #region NamespaceAccess
                    case IdenType.NamespaceAccess:
                        {
                            var nsr = HelperClasses.NamespaceResolver.createNSR(this, this.IsAnonymousIdent);
                            var reference = nsr.Reference;
                            this.referencedObject = reference;
                            if (reference is Interfaces.iClass)
                                this.referencedType = ((Interfaces.iClass)reference).VTO;
                            else if (reference is Interfaces.iFunction)
                                this.referencedType = ((Interfaces.iFunction)reference).ReturnType;
                            else
                                this.referencedType = null;
                        }
                        break;
                    #endregion
                }
            }


            return errCount;
        }
        public override string ToString()
        {
            return "ident->(" + this.originalValue + ")" + this.FullyQualifiedName;
        }
        public override bool Equals(object obj)
        {
            if (obj is Ident)
            {
                return ((Ident)obj).originalValue == this.originalValue;
            }
            else
            {
                return false;
            }
        }
        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            throw new NotImplementedException();
        }
    }
}
