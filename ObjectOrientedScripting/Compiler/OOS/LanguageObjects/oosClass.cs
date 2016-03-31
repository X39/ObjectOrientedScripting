using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class oosClass : pBaseLangObject, Interfaces.iGetIndex, Interfaces.iClass, Interfaces.iHasId
    {
        //Is set in Compiler.cs before finalize is callen
        public static Variable GlobalClassRegisterVariable;


        private int endMarker;
        private int endMarkerParents;
        private VarTypeObject vto;
        private List<oosClass> parentClasses;
        private List<oosInterface> parentInterfaces;
        public int ID { get; set; }


        public Ident Name { get { return ((Ident)this.children[0]); } set { this.children[0] = value; vto = new VarTypeObject(value, null); } }
        public List<Ident> ExtendedClasses
        {
            get
            {
                List<Ident> retList = new List<Ident>();
                foreach (var it in this.ParentClassesIdents)
                {
                    if (it is Ident)
                        retList.Add((Ident)it);
                    else
                        throw new Exception();
                }
                return retList;
            }
        }

        public VarTypeObject VTO { get { return this.vto; } }
        public List<pBaseLangObject> ParentClassesIdents { get { return this.children.GetRange(1, endMarker); } }
        public List<pBaseLangObject> ClassContent { get { return this.children.GetRange(endMarker + 1, this.children.Count - (endMarker + 1)); } }
        public List<pBaseLangObject> AllObjects { get; internal set; }

        public oosClass(pBaseLangObject parent) : base(parent)
        {
            this.parentClasses = new List<oosClass>();
            this.parentInterfaces = new List<oosInterface>();
            this.children.Add(null);
        }
        public void markEnd()
        {
            this.endMarker = this.children.Count - 1;
        }
        public void markExtendsEnd()
        {
            this.endMarkerParents = this.children.Count - 1;
        }
        public override int doFinalize()
        {
            int errCount = 0;
            for (int i = 0; i < ParentClassesIdents.Count; i++)
            {
                var it = ParentClassesIdents[i];
                if (!(it is Ident))
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.UNKNOWN, Name.Line, Name.Pos));
                    errCount++;
                    continue;
                }
                if (((Ident)it).ReferencedObject is oosClass)
                {
                    if (i >= this.endMarkerParents)
                    {
                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0037, Name.Line, Name.Pos));
                        errCount++;
                    }
                    var parentClass = (oosClass)((Ident)it).ReferencedObject;
                    this.parentClasses.AddRange(parentClass.parentClasses);
                    this.parentClasses.Add(parentClass);
                }
                else if (((Ident)it).ReferencedObject is oosInterface)
                {
                    if (i < this.endMarkerParents)
                    {
                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0038, Name.Line, Name.Pos));
                        errCount++;
                    }
                    this.parentInterfaces.Add((oosInterface)((Ident)it).ReferencedObject);
                }
                else
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.UNKNOWN, Name.Line, Name.Pos));
                    errCount++;
                }
            }
            var functionNameList = new List<string>();
            this.AllObjects = new List<pBaseLangObject>();
            var allFunctions = new List<Function>();
            var inheritanceFunctions = new List<Function>();
            var allVariables = new List<Variable>();
            var classes = new List<oosClass>(this.parentClasses);
            classes.Add(this);
            foreach (var classObj in classes)
            {
                foreach (var classChild in classObj.children)
                {
                    if (classChild is Function || classChild is Variable)
                    {
                        bool flag = true;
                        if(classChild is Function && ((Function)classChild).IsVirtual)
                        {
                            inheritanceFunctions.Add((Function)classChild);
                            for(int i = 0; i < this.AllObjects.Count; i++)
                            {
                                var it = this.AllObjects[i];
                                if (it is Function && ((Function)it).Name.OriginalValue.Equals(((Function)classChild).Name.OriginalValue) && HelperClasses.ArgList.matchesArglist(((Function)it).ArgList, ((Function)classChild).ArgList))
                                {
                                    if(!((Function)classChild).IsVirtual)
                                    {
                                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0039, ((Function)classChild).Name.Line));
                                        errCount++;
                                    }
                                    this.AllObjects[i] = classChild;
                                    flag = false;
                                }
                            }
                        }

                        if(flag)
                        {
                            if(classChild is Function )
                            {
                                if (!((Function)classChild).IsConstructor || ((Function)classChild).Parent == this)
                                {
                                    this.AllObjects.Add(classChild);
                                    allFunctions.Add((Function)classChild);
                                }
                            }
                            else if(classChild is Variable)
                            {
                                this.AllObjects.Add(classChild);
                                allVariables.Add((Variable)classChild);
                            }
                        }
                    }
                }
            }



            foreach (var it in allFunctions)
            {
                var origVal = it.Name.OriginalValue;
                if (it.IsConstructor)
                    continue;
                if (functionNameList.FirstOrDefault(checkString => checkString.Equals(origVal)) != null)
                {
                    var parentFnc = inheritanceFunctions.FirstOrDefault(checkValue => checkValue.Name.OriginalValue == origVal);
                    if (parentFnc == null)
                    {
                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0039, it.Name.Line, it.Name.Pos, it.Name.File));
                        errCount++;
                    }
                    else
                    {
                        if (!it.IsVirtual)
                        {
                            Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0040, it.Name.Line, it.Name.Pos, it.Name.File));
                            errCount++;
                        }
                        else
                        {
                            var argList = parentFnc.ArgList;
                            var itArgList = it.ArgList;
                            if (argList.Count != itArgList.Count)
                            {
                                if (argList.Count > itArgList.Count)
                                {
                                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0041, it.Name.Line, it.Name.Pos, it.Name.File));
                                    errCount++;
                                }
                                else
                                {
                                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0042, it.Name.Line, it.Name.Pos, it.Name.File));
                                    errCount++;
                                }
                            }
                            for (var i = 0; i < argList.Count; i++)
                            {
                                if (i > argList.Count || i > itArgList.Count)
                                    break;
                                var v = argList[i];
                                var e = itArgList[i];
                                if (!v.Equals(e))
                                {
                                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0043));
                                    errCount++;
                                }
                            }
                            if (!it.varType.Equals(parentFnc.varType))
                            {
                                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0044, it.Name.Line, it.Name.Pos, it.Name.File));
                                errCount++;
                            }
                        }
                    }
                }
                functionNameList.Add(it.Name.OriginalValue);
            }
            //ToDo: Check interface functions are implemented
            return errCount;
        }

        public void addParentClass(Ident blo)
        {
            this.children.Add(blo);
        }
        public override string ToString()
        {
            return "class->" + this.Name.FullyQualifiedName;
        }

        public int getIndex(Ident ident)
        {
            int index = 0;
            var refObj = ident.ReferencedObject;
            if (refObj is Function)
            {
                var obj = (Interfaces.iName)refObj;
                for (int i = 0; i < this.AllObjects.Count; i++)
                {
                    var it = this.AllObjects[i];
                    if (it is Interfaces.iName && ((Interfaces.iName)it).Name.OriginalValue == obj.Name.OriginalValue)
                    {
                        if (it is Function && obj is Function && HelperClasses.ArgList.matchesArglist(((Function)it).ArgList, ((Function)obj).ArgList))
                        {
                            return index;
                        }
                    }
                    if (it is Function && ((Function)it).IsVirtual)
                        index++;
                }
            }
            else if(refObj is Variable)
            {
                var obj = (Interfaces.iName)refObj;
                for (int i = 0; i < this.AllObjects.Count; i++)
                {
                    var it = this.AllObjects[i];
                    if (it is Interfaces.iName && ((Interfaces.iName)it).Name.OriginalValue == obj.Name.OriginalValue)
                    {
                        if (it is Variable)
                        {
                            return index;
                        }
                    }
                    if (it is Variable)
                        index++;
                }
            }
            throw new Exception();
        }

        public Interfaces.iOperatorFunction getOperatorFunction(OverridableOperator op)
        {
            return null;
        }
        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            foreach (var it in ClassContent)
            {
                if (it is Interfaces.iFunction && ((Interfaces.iFunction)it).IsVirtual)
                    continue;
                if(it is Variable)
                    continue;
                it.writeOut(sw, cfg);
            }
        }
    }
}