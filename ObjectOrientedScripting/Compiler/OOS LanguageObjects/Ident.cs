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
            NamespaceAccess
        }
        public enum AccessType
        {
            Instance,
            Namespace,
            NA
        }

        public bool IsSimpleIdentifier { get { return !(this.Parent is Ident) && this.Access == AccessType.NA; } }
        private bool isGlobalIdentifier;
        public bool IsGlobalIdentifier { get { return this.isGlobalIdentifier; } set { if (this.Parent is Ident) throw new Exception("Double Namespace Access experienced"); this.isGlobalIdentifier = value; } }
        public bool IsRelativeIdentifier { get { return this.Access == AccessType.Namespace; } }
        public bool IsSelfReference { get { return this.FullyQualifiedName.Contains("::this"); } }
        public string FullyQualifiedName
        {
            get
            {
                string res = "";
                var lastIdent = this.getLastOf<Ident>();
                if (lastIdent.isGlobalIdentifier)
                {
                    res = "::";
                }
                else
                {
                    if(lastIdent.Parent is Interfaces.iName)
                    {
                        var refIdent = ((Interfaces.iName)lastIdent.Parent).Name;
                        res = refIdent.FullyQualifiedName;
                    }
                }
                var curIdent = this;
                List<Ident> identList = new List<Ident>();
                while (curIdent != null)
                {
                    identList.Add(curIdent);
                    curIdent = curIdent.getFirstOf<Ident>();
                }
                for (int i = identList.Count - 1; i >= 0; i--)
                {
                    var it = identList[i];
                    Ident parentIdent = (it.Parent is Ident ? (Ident)it.Parent : null);
                    res += it.originalValue;
                    switch (it.Access)
                    {
                        case AccessType.Instance:
                            if (parentIdent == null)
                            {
                                throw new Exception();
                            }
                            else
                            {
                                if(parentIdent.ReferencedType.IsObject)
                                {
                                    res = parentIdent.ReferencedType.ident.FullyQualifiedName + "::" + this.originalValue;
                                }
                                else
                                {
                                    throw new Exception();
                                }
                            }
                            break;
                        case AccessType.Namespace:
                            res += "::";
                            break;
                    }
                }
                return res;
            }
        }

        private string originalValue;
        private pBaseLangObject referencedObject;
        private VarTypeObject referencedType;
        private IdenType type;
        private int line;
        private int pos;

        public pBaseLangObject ReferencedObject { get { return referencedObject; } }
        public VarTypeObject ReferencedType { get { return referencedType; } }
        public IdenType Type { get { return this.type; } }
        public string OriginalValue { get { return this.originalValue; } }
        public int Line { get { return this.line; } }
        public int Pos { get { return this.pos; } }
        public AccessType Access { get; set; }


        public Ident(pBaseLangObject parent, string origVal, int line, int pos) : base(parent)
        {
            this.originalValue = origVal;
            referencedObject = null;
            referencedType = new VarTypeObject(VarType.Void);
            this.line = line;
            this.pos = pos;
            this.Access = AccessType.NA;
            this.isGlobalIdentifier = false;
        }
        public override int doFinalize()
        {
            int errCount = 0;
            //Make sure ::foo::bar.foo::bar is not possible
            Ident parentIdent = (this.Parent is Ident ? (Ident)this.Parent : default(Ident));
            if (parentIdent == null && !this.IsSimpleIdentifier)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0000, this.Line, this.Pos));
                errCount++;
            }
            //Check what we have here (possible outcomes: variableacces, arrayaccess, functioncall, namespaceaccess)
            type = IdenType.NamespaceAccess;
            var fncCalls = this.getAllChildrenOf<FunctionCall>();
            var arrAccess = this.getAllChildrenOf<ArrayAccess>();
            if(fncCalls.Count > 0)
            {
                type = IdenType.FunctionCall;
            }
            else if (arrAccess.Count > 0)
            {
                type = IdenType.ArrayAccess;
            }
            else if (parentIdent == null && this.Access == AccessType.NA)
            {
                type = IdenType.VariableAccess;
            }
            else if (parentIdent.Access == AccessType.Namespace && this.Access == AccessType.NA || this.Access == AccessType.Instance)
            {
                var ntr = new HelperClasses.NamespaceResolver(this.FullyQualifiedName);
                if (ntr.Reference is Interfaces.iClass)
                    type = IdenType.NamespaceAccess;
                else
                    type = IdenType.VariableAccess;
            }
            else if (parentIdent == null)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.UNKNOWN, this.Line, this.Pos));
                errCount++;
            }
            //And process it then
            switch(type)
            {
                #region VariableAccess & ArrayAccess
                case IdenType.VariableAccess:
                case IdenType.ArrayAccess:
                    {
                        var variable = HelperClasses.NamespaceResolver.getVariableReferenceOfFQN(this.FullyQualifiedName, true, this);
                        if (variable == null)
                        {
                            variable = HelperClasses.NamespaceResolver.getVariableReferenceOfFQN(this.FullyQualifiedName, false, this);
                        }
                        this.referencedObject = variable;
                        //Set type to variable type
                        this.referencedType = variable.varType;

                        if(type == IdenType.ArrayAccess)
                        {
                            if (this.referencedType.IsObject)
                            {
                                //Check if given object is implementing the ArrayAccess operator
                                if (this.referencedType.ident.referencedObject is Interfaces.iClass)
                                {
                                    Interfaces.iClass classRef = (Interfaces.iClass)this.referencedType.ident.referencedObject;
                                    Interfaces.iOperatorFunction opFnc = classRef.getOperatorFunction(OperatorFunctions.ArrayAccess);
                                    if (opFnc == null)
                                    {
                                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0005, this.Line, this.Pos));
                                        errCount++;
                                    }
                                    else
                                    {
                                        this.referencedType = opFnc.ReturnType;
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
                                switch(this.referencedType.varType)
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
                        if(parentIdent == null)
                        {
                            fncList = HelperClasses.NamespaceResolver.getFunctionReferenceOfFQN(this.FullyQualifiedName);
                        }
                        else
                        {
                            fncList = HelperClasses.NamespaceResolver.getFunctionReferenceOfFQN(new HelperClasses.NamespaceResolver(parentIdent.ReferencedType.ident.FullyQualifiedName, this.originalValue));
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
                                if(HelperClasses.ArgList.matchesArglist(it.ArgList, fncCall.ArgList))
                                {
                                    fnc = it;
                                    break;
                                }
                            }
                            //Raise new linker issue if we could not locate a matching function
                            if(fnc == null)
                            {
                                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0002, this.Line, this.Pos));
                                errCount++;
                            }
                            else
                            {
                                //Ref the object to the function
                                this.referencedObject = (pBaseLangObject)fnc;
                                //Ref the type to the return type
                                this.referencedType = fnc.ReturnType;

                                //As last step make sure we got the correct encapsulation here
                                var enc = fnc.FunctionEncapsulation;
                                if(enc != Encapsulation.Static && enc != Encapsulation.Public)
                                {
                                    var parentClass = this.getFirstOf<Interfaces.iClass>();
                                    HelperClasses.NamespaceResolver fncNsr = fnc.Name.FullyQualifiedName;
                                    if(enc == Encapsulation.Private)
                                    {
                                        //Private encapsulation just requires checking the current class we are operating in
                                        if (!fncNsr.isInNamespace(parentClass.Name.FullyQualifiedName))
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
                                        foreach(var it in classes)
                                        {
                                            if(fncNsr.isInNamespace(it.FullyQualifiedName))
                                            {
                                                flag = true;
                                                break;
                                            }
                                        }
                                        if(!flag)
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
                        var nsr = new HelperClasses.NamespaceResolver(this.FullyQualifiedName);
                        var reference = nsr.Reference;
                        this.referencedObject = reference;
                        if (reference is Interfaces.iClass)
                            this.referencedType = ((Interfaces.iClass)reference).VTO;
                        else
                            this.referencedType = null;
                    }
                    break;
                #endregion
            }


            return errCount;
        }
        public override string ToString()
        {
            return this.FullyQualifiedName;
        }
    }
}
