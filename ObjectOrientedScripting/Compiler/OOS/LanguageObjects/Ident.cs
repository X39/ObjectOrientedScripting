using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        public bool IsAnonymousIdent { get { return this.getClassTemplate() != null; } }
        private bool IsNamespaceAccess()
        {
            if (this.IsAnonymousIdent)
                return true;
            var nsr = HelperClasses.NamespaceResolver.createNSR(this, false);
            if (nsr == null)
                return false;
            var reference = nsr.Reference;
            if (reference is Interfaces.iClass)
                return true;
            else if (reference is Interfaces.iFunction)
                return true;
            else
                return false;
        }
        private Template getClassTemplate()
        {
            var templateList = this.getAllParentsOf<Interfaces.iTemplate>();
            if (this.Parent is Template && this.Parent.Parent is Interfaces.iClass)
            {
                return (Template)this.Parent;
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
                            return template.TemplateObject;
                    }
                }
            }
            return null;
        }
        public string FullyQualifiedName
        {
            get
            {
                if(this.IsSelfReference && this.children.Count == 0)
                {
                    return Wrapper.Compiler.thisVariableName;
                }
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
                if (this.IsSelfReference)
                {
                    var tmp = this.getFirstOf<Interfaces.iClass>();
                    if (tmp == null)
                        throw new Exception();
                    return tmp.Name.FullyQualifiedName;
                }
                else
                {
                    if (this.Parent is Ident && (((Ident)this.Parent).Access == AccessType.Instance) && ((Ident)this.Parent).ReferencedType != null && !(((Ident)this.Parent).ReferencedObject is oosEnum))
                    {
                        return ((Ident)this.Parent).ReferencedType.ident.LastIdent.FullyQualifiedName + "." + this.originalValue;
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
                            if (curobject.Parent is Interfaces.iName && ((Interfaces.iName)curobject.Parent).Name == this && !(curobject.Parent is NewInstance) && !((Ident)curobject).isGlobalIdentifier)
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
                                    (((Ident)curobject).Access == AccessType.Namespace ? "::" : ((Ident)curobject).Access == AccessType.Instance ? "." : ":") + result;
                            }
                            if (((Ident)curobject).isGlobalIdentifier)
                                break;
                        }
                        else if((curobject is NewInstance))
                        {
                            
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
                    result = result.TrimEnd(new char[] { ':', '.' });
                    //if (this.getLastOf<Ident>().Parent is Function)
                    //{
                    //    result += ((Function)this.getLastOf<Ident>().Parent).SqfSuffix;
                    //}
                    return result;
                }
            }
        }

        private string originalValue;
        private IdenType type;

        public pBaseLangObject ReferencedObject { get; internal set; }
        public VarTypeObject ReferencedType { get; internal set; }
        public IdenType Type { get { return this.type; } }
        public string OriginalValue { get { return this.originalValue; } }
        public int Line { get; internal set; }
        public int Pos { get; internal set; }
        public string File { get; internal set; }
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
        public Ident NextWorkerIdent
        {
            get
            {
                var list = this.getAllChildrenOf<Ident>();
                if (list.Count == 0)
                    return this;
                if (this.children.Count > 1)
                    return list[0].NextWorkerIdent;
                return list[0].NextWorkerIdent;
            }
        }
        public bool IsPureIdent { get { return this.getAllChildrenOf<FunctionCall>(true).Count == 0 && this.getAllChildrenOf<ArrayAccess>(true).Count == 0; } }
        public Ident NextIdent
        {
            get
            {
                var list = this.getAllChildrenOf<Ident>();
                if (list.Count == 0)
                    return null;
                return list[0];
            }
        }
        public Ident(pBaseLangObject parent, string origVal, int line, int pos, string file) : base(parent)
        {
            this.originalValue = origVal;
            this.ReferencedObject = null;
            this.ReferencedType = new VarTypeObject(VarType.Void);
            this.Line = line;
            this.Pos = pos;
            this.File = file;
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
            if (this.Parent is Template && !(this.Parent.Parent is Ident))
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
            else if(IsNamespaceAccess())
            {
                type = IdenType.NamespaceAccess;
            }
            else if (parentIdent == null && this.Access == AccessType.NA)
            {
                if (this.Parent is Interfaces.iClass || (this.Parent is Interfaces.iFunction && ((Interfaces.iFunction)this.Parent).ReturnType.ident == this) || this.Parent is Value || this.Parent is Variable)
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
                if(ntr == null)
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0012, this.Line, this.Pos, this.File));
                    errCount++;
                    return errCount;
                }
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
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.UNKNOWN, this.Line, this.Pos, this.File));
                errCount++;
            }
            #endregion
            //And process it then ((unless its a simple ident, then we do not want to process ... here any further))
            if (
                this.IsSimpleIdentifier &&
                (
                    (this.Parent is Interfaces.iName && ((Interfaces.iName)this.Parent).Name == this) ||
                    (this.Parent is Interfaces.iHasType && !(this.Parent is Expression) && ((Interfaces.iHasType)this.Parent).ReferencedType.ident == this)
                ) &&
                !(this.Parent is AssignContainer) &&
                !(this.Parent is NewInstance) &&
                !(this.Parent is Interfaces.iHasType && ((Interfaces.iHasType)this.Parent).ReferencedType.ident == this)
            )
            {
                this.ReferencedObject = this.Parent;
                if (this.Parent is Interfaces.iHasType)
                    this.ReferencedType = ((Interfaces.iHasType)this.Parent).ReferencedType;
                else //todo: try to replace with proper refObject type
                    this.ReferencedType = new VarTypeObject(this, (this.Parent is Interfaces.iTemplate ? ((Interfaces.iTemplate)this.Parent).TemplateObject : null));
            }
            else
            {
                switch (type)
                {
                    #region ThisVar
                    case IdenType.ThisVar:
                        {
                            Interfaces.iClass curObject = this.getFirstOf<Interfaces.iClass>();
                            this.ReferencedObject = (pBaseLangObject)curObject;
                            this.ReferencedType = curObject.VTO;
                        }
                        break;
                    #endregion
                    #region TemplateVar
                    case IdenType.TemplateVar:
                        {
                            this.ReferencedObject = (pBaseLangObject)this.getFirstOf<Interfaces.iClass>();
                            this.ReferencedType = ((Interfaces.iClass)this.ReferencedObject).VTO;
                        }
                        break;
                    #endregion
                    #region SqfCommandName
                    case IdenType.SqfCommandName:
                        {
                            this.ReferencedObject = this.Parent;
                            this.ReferencedType = ((SqfCall)this.Parent).ReferencedType;
                        }
                        break;
                    #endregion
                    #region VariableAccess & ArrayAccess
                    case IdenType.VariableAccess:
                    case IdenType.ArrayAccess:
                        {
                            var nsr = HelperClasses.NamespaceResolver.createNSR(this);
                            if(nsr != null && nsr.IsValid && (nsr.Reference is oosEnum.EnumEntry || nsr.Reference is oosEnum))
                            {
                                if (nsr.Reference is oosEnum.EnumEntry)
                                {
                                    var entry = (oosEnum.EnumEntry)nsr.Reference;
                                    this.ReferencedObject = entry;
                                    this.ReferencedType = ((oosEnum)entry.Parent).ReferencedType;
                                    break;
                                }
                                else if(nsr.Reference is oosEnum)
                                {
                                    var e = (oosEnum)nsr.Reference;
                                    this.ReferencedObject = e;
                                    this.ReferencedType = e.ReferencedType;
                                    break;
                                }
                            }
                            var variable = HelperClasses.NamespaceResolver.getVariableReferenceOfFQN(HelperClasses.NamespaceResolver.createNSR(this, true), true, this);
                            if (variable == null)
                            {
                                variable = HelperClasses.NamespaceResolver.getVariableReferenceOfFQN(this, false, this);
                            }
                            if (variable == null)
                            {
                                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0012, this.Line, this.Pos, this.File));
                                errCount++;
                            }
                            this.ReferencedObject = variable;
                            //Set type to variable type
                            this.ReferencedType = variable.varType;

                            if (type == IdenType.ArrayAccess)
                            {
                                if (variable.ReferencedType.IsObject)
                                {
                                    //Check if given object is implementing the ArrayAccess operator
                                    if (variable.ReferencedType.ident.LastIdent.ReferencedObject is Interfaces.iClass)
                                    {
                                        Interfaces.iClass classRef = (Interfaces.iClass)variable.ReferencedType.ident.LastIdent.ReferencedObject;
                                        Interfaces.iOperatorFunction opFnc = classRef.getOperatorFunction(OverridableOperator.ArrayAccess);
                                        if (opFnc == null)
                                        {
                                            Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0005, this.Line, this.Pos, this.File));
                                            errCount++;
                                        }
                                        else
                                        {
                                            this.ReferencedType = opFnc.ReturnType;
                                            if (variable.TemplateObject != null)
                                            {
                                                var templateList = ((pBaseLangObject)opFnc).getAllParentsOf<Interfaces.iTemplate>();
                                                foreach (var it in templateList)
                                                {
                                                    var tmp = HelperClasses.ArgList.resolveVarTypeObject(opFnc.ReturnType, it.TemplateObject, variable.TemplateObject);
                                                    if (tmp != opFnc.ReturnType)
                                                    {
                                                        this.ReferencedType = tmp;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.UNKNOWN, this.Line, this.Pos, this.File));
                                        errCount++;
                                    }
                                }
                                else
                                {
                                    //just check if this is an array type
                                    switch (this.ReferencedType.varType)
                                    {
                                        case VarType.BoolArray:
                                            this.ReferencedType = new VarTypeObject(this.ReferencedType);
                                            this.ReferencedType.varType = VarType.Bool;
                                            break;
                                        case VarType.ScalarArray:
                                            this.ReferencedType = new VarTypeObject(this.ReferencedType);
                                            this.ReferencedType.varType = VarType.Scalar;
                                            break;
                                        default:
                                            Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0006, this.Line, this.Pos, this.File));
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
                            var newInstance = this.getFirstOf<NewInstance>(true, typeof(Ident));
                            var fqn = this.FullyQualifiedName;
                            if (parentIdent != null && parentIdent.ReferencedObject is Variable)
                            {
                                //if (((Variable)parentIdent.ReferencedObject).ReferencedType.IsObject)
                                //    fqn = ((Interfaces.iClass)((Variable)parentIdent.ReferencedObject).ReferencedType.ident.LastIdent.ReferencedObject).Name.LastIdent.FullyQualifiedName + "." + this.originalValue;
                                //else
                                fqn = parentIdent.ReferencedType.ident.LastIdent.ReferencedType.ident.LastIdent.FullyQualifiedName + "." + this.originalValue;
                            }
                            if (newInstance == null)
                            {
                                fncList = HelperClasses.NamespaceResolver.getFunctionReferenceOfFQN(HelperClasses.NamespaceResolver.createNSR(fqn));
                            }
                            else
                            {
                                fncList = HelperClasses.NamespaceResolver.getFunctionReferenceOfFQN(HelperClasses.NamespaceResolver.createNSR(fqn + "." + this.originalValue));
                            }
                            if (fncList.Count == 0)
                            {
                                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0001, this.Line, this.Pos, this.File));
                                errCount++;
                            }
                            else
                            {
                                //Search the correct function in the possible matches
                                Interfaces.iFunction fnc = null;
                                foreach (var it in fncList)
                                {
                                    if (HelperClasses.ArgList.matchesArglist(it.ArgList, fncCall.ArgList, (parentIdent != null && parentIdent.ReferencedObject is Variable ? (Variable)parentIdent.ReferencedObject : null)))
                                    {
                                        fnc = it;
                                        break;
                                    }
                                }
                                //Raise new linker issue if we could not locate a matching function
                                if (fnc == null)
                                {
                                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0002, this.Line, this.Pos, this.File));
                                    errCount++;
                                }
                                else
                                {
                                    if (fnc is Function && ((Function)fnc).IsConstructor && this.getFirstOf<NewInstance>() == null)
                                    {
                                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0026, this.Line, this.Pos, this.File));
                                        errCount++;
                                    }
                                    //Ref the object to the function
                                    this.ReferencedObject = (pBaseLangObject)fnc;
                                    //Ref the type to the return type
                                    this.ReferencedType = fnc.ReturnType;

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
                                                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0003, this.Line, this.Pos, this.File));
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
                                                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0004, this.Line, this.Pos, this.File));
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
                            if (this.IsAnonymousIdent)
                            {
                                this.ReferencedObject = getClassTemplate();
                                this.ReferencedObject = null;
                            }
                            else
                            {
                                var nsr = HelperClasses.NamespaceResolver.createNSR(this);
                                if (nsr == null)
                                {
                                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0046, this.Line, this.Pos, this.File));
                                    errCount++;
                                }
                                else
                                {
                                    var reference = nsr.Reference;
                                    this.ReferencedObject = reference;
                                    if (reference is Interfaces.iClass)
                                        this.ReferencedType = ((Interfaces.iClass)reference).VTO;
                                    else if (reference is Interfaces.iFunction)
                                        this.ReferencedType = ((Interfaces.iFunction)reference).ReturnType;
                                    else
                                        this.ReferencedType = null;
                                }
                            }
                        }
                        break;
                    #endregion
                }
            }


            return errCount;
        }
        public override string ToString()
        {
            string s = "ident->";
            try
            {
                s = "ident->(" + this.originalValue + ")" + this.FullyQualifiedName;
            }
            catch(Exception ex)
            {
                s += ex.Message;
            }
            return s;
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
        public bool HasCallWrapper
        {
            get
            {
                if (this.Parent is Ident && ((Ident)this.Parent).Type != IdenType.NamespaceAccess)
                    return ((Ident)this.Parent).HasCallWrapper;
                if (this.Type == IdenType.NamespaceAccess)
                    return false;
                var functionCalls = this.getAllChildrenOf<FunctionCall>(true, null, -1, -1, new Type[] { typeof(Ident), typeof(FunctionCall)});
                var arrayAccesss = this.getAllChildrenOf<ArrayAccess>(true, null, -1, -1, new Type[] { typeof(Ident), typeof(FunctionCall) });
                return functionCalls.Count + arrayAccesss.Count > 1;
            }
        }
        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            bool assignToTmp = this.HasCallWrapper;
            bool callWrapper = !(this.Parent is Ident && ((Ident)this.Parent).Type != IdenType.NamespaceAccess) && assignToTmp;
            if (callWrapper)
            {
                sw.Write("[] call {private \"___tmp___\"; ");
                if (this.ReferencedObject is Variable)
                {
                    sw.Write("___tmp___ = (" + ((Variable)this.ReferencedObject).SqfVariableName + ");");
                }
                else
                {
                    sw.Write("___tmp___ = (" + this.WriteOutValue + ");");
                }
            }


            if (this.IsSimpleIdentifier && this.children.Count == 0)
            {
                if (this.ReferencedObject is Variable)
                {
                    Variable variable = (Variable)this.ReferencedObject;
                    sw.Write(variable.SqfVariableName);
                }
                else if(this.ReferencedObject is oosClass && this.IsSelfReference)
                {
                    sw.Write(Wrapper.Compiler.thisVariableName);
                }
                else
                {
                    throw new Exception();
                }
            }
            else if (this.children.Count == 0)
            {
                sw.Write(this.WriteOutValue);
            }
            else
            {
                foreach (var it in this.children)
                {
                    var ms = new MemoryStream();
                    var sw2 = new StreamWriter(ms);
                    if (it is Ident)
                        continue;
                    it.writeOut(sw2, cfg);
                    sw2.Flush();
                    ms.Seek(0, SeekOrigin.Begin);
                    string output = new StreamReader(ms).ReadToEnd();
                    ms.Close();
                    if (assignToTmp)
                    {
                        sw.Write(" ___tmp___ = (");
                        sw.Write(output);
                        sw.Write(");");
                    }
                    else
                    {
                        sw.Write(output);
                    }
                }
                foreach (var it in this.children)
                {
                    if (it is Ident)
                        it.writeOut(sw, cfg);
                }
            }

            if (callWrapper)
            {
                if(this.LastIdent.ReferencedType.varType == VarType.Void)
                    sw.Write('}');
                else
                    sw.Write(" ___tmp___}");
            }

        }
        public string WriteOutValue
        {
            get
            {
                string s = this.Parent is Ident ? ((Ident)this.Parent).WriteOutValue : "";
                switch (this.Type)
                {
                    case IdenType.FunctionCall:
                    case IdenType.ArrayAccess:
                    case IdenType.VariableAccess:
                        var refObj = this.ReferencedObject;
                        if (this.type == IdenType.FunctionCall)
                        {
                            if (((Interfaces.iFunction)refObj).FunctionEncapsulation == Encapsulation.Static || ((Interfaces.iFunction)refObj).IsConstructor || !this.HasCallWrapper)
                                break;
                            refObj = ((Ident)this.Parent).ReferencedObject;
                        }
                        if(refObj is Variable)
                        {
                            var variable = (Variable)refObj;
                            if (this.HasCallWrapper)
                            {
                                if (this.type == IdenType.FunctionCall)
                                    break;
                                s = "___tmp___";
                            }
                            if (s == "")
                                s += variable.SqfVariableName;
                            if (variable.Parent is Interfaces.iClass)
                            {
                                AssignContainer ac = this.getFirstOf<AssignContainer>();
                                if(ac == null || (ac.Name != this && !ac.Name.getAllChildrenOf<Ident>(true).Contains(this)))
                                    s = '(' + s + " select " + variable.SqfVariableName + ')';
                            }
                        }
                        else if (refObj is oosEnum.EnumEntry)
                        {
                            var entry = refObj as oosEnum.EnumEntry;
                            s += entry.Value.value;
                        }
                        else if (refObj is Interfaces.iFunction)
                        {
                            if (s.Contains("___tmp___"))
                                s = "___tmp___";
                        }
                        break;
                    case IdenType.ThisVar:
                        return Wrapper.Compiler.thisVariableName;
                }
                return s;
            }
        }
    }
}
