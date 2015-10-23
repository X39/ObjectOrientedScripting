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

        public bool IsSimpleIdentifier { get { return !this.originalValue.Contains("::"); } }
        public bool IsGlobalIdentifier { get { return this.originalValue.StartsWith("::"); } }
        public bool IsRelativeIdentifier { get { return this.originalValue.Contains("::"); } }
        public bool IsSelfReference { get { return this.FullyQualifiedName.Contains("::this"); } }
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
                            return (obj == null ? "" : obj.Name.FullyQualifiedName) + "::" + this.originalValue;
                        }
                        if (this.Parent is NativeInstruction)
                        {
                            obj = this.getFirstOf<Native>();
                            return (obj == null ? "" : obj.Name.FullyQualifiedName) + "::" + this.originalValue;
                        }
                        else
                        {
                            return obj.Name.FullyQualifiedName + "::" + this.originalValue;
                        }
                    }
                }
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
            else if (this.IsSimpleIdentifier)
            {
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
                #region VariableAccess
                case IdenType.VariableAccess:
                    {

                    }
                    break;
                #endregion
                #region ArrayAccess
                case IdenType.ArrayAccess:
                    {

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
                            fncList = HelperClasses.NamespaceResolver.getFunctionReferenceOfFQN(this, this.FullyQualifiedName);
                        }
                        else
                        {
                            fncList = HelperClasses.NamespaceResolver.getFunctionReferenceOfFQN(this, new HelperClasses.NamespaceResolver(parentIdent.ReferencedType.ident.FullyQualifiedName, this.originalValue));
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
                                if(HelperClasses.ArgList.matchesArglist(it.ArgList, fncCall.children))
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
