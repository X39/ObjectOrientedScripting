using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class oosClass : pBaseLangObject, Interfaces.iGetFunctionIndex, Interfaces.iGetVariableIndex, Interfaces.iClass
    {
        private int endMarker;
        private int endMarkerParents;
        private VarTypeObject vto;
        private List<oosClass> parentClasses;
        private List<oosInterface> parentInterfaces;


        public Ident Name { get { return ((Ident)this.children[0]); } set { this.children[0] = value; vto = new VarTypeObject(value, true, null); } }
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
        public List<Function> ThisFunctions
        {
            get
            {
                List<Function> fncList = new List<Function>();
                var thisFncList = this.getAllChildrenOf<Function>();
                foreach (var it in thisFncList)
                {
                    if (it.encapsulation == Encapsulation.Public || it.encapsulation == Encapsulation.Protected || it.encapsulation == Encapsulation.Private)
                    {
                        fncList.Add(it);
                    }
                }
                return fncList;
            }
        }
        public List<Function> AllFunctions
        {
            get
            {
                List<Function> fncList = new List<Function>();
                fncList.AddRange(this.InheritanceFunctions);
                fncList.AddRange(this.ThisFunctions);
                return fncList;
            }
        }
        public List<Function> InheritanceFunctions
        {
            get
            {
                List<Function> fncList = new List<Function>();
                foreach (var it in parentClasses)
                {
                    fncList.AddRange(it.AllFunctions);
                }
                for (int i = 0; i < fncList.Count; i++)
                {
                    var it = fncList[i];
                    foreach (var it2 in this.ThisFunctions)
                    {
                        if (it.Name.OriginalValue.Equals(it2.Name.OriginalValue))
                        {
                            if (it2.Override)
                            {
                                fncList.RemoveAt(i);
                            }
                        }
                    }
                }
                return fncList;
            }
        }
        public List<Variable> ThisVariables
        {
            get
            {
                List<Variable> varList = new List<Variable>();
                var thisVarList = this.getAllChildrenOf<Variable>();
                foreach (var it in thisVarList)
                {
                    if (it.encapsulation == Encapsulation.Public || it.encapsulation == Encapsulation.Protected || it.encapsulation == Encapsulation.Private)
                    {
                        varList.Add(it);
                    }
                }
                return varList;
            }
        }
        public List<Variable> InheritanceVariables
        {
            get
            {
                List<Variable> varList = new List<Variable>();
                foreach (var it in parentClasses)
                {
                    varList.AddRange(it.AllVariables);
                }
                return varList;
            }
        }
        public List<Variable> AllVariables
        {
            get
            {
                List<Variable> varList = new List<Variable>();
                varList.AddRange(this.ThisVariables);
                varList.AddRange(this.InheritanceVariables);
                return varList;
            }
        }

        
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
                    this.parentClasses.Add(parentClass);
                    this.parentClasses.AddRange(parentClass.parentClasses);
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
            var inheritanceFunctions = this.InheritanceFunctions;
            foreach (var it in this.AllFunctions)
            {
                var origVal = it.Name.OriginalValue;
                if (functionNameList.FirstOrDefault(checkString => checkString.Equals(origVal)) != null)
                {
                    var parentFnc = inheritanceFunctions.FirstOrDefault(checkValue => checkValue.Name.OriginalValue == origVal);
                    if (parentFnc == null)
                    {
                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0039, it.Name.Line, it.Name.Pos));
                        errCount++;
                    }
                    else
                    {
                        if (!it.Override)
                        {
                            Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0040, it.Name.Line, it.Name.Pos));
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
                                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0041, it.Name.Line, it.Name.Pos));
                                    errCount++;
                                }
                                else
                                {
                                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0042, it.Name.Line, it.Name.Pos));
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
                                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0044, it.Name.Line, it.Name.Pos));
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
        public Tuple<int, int> getFunctionIndex(Ident ident)
        {
            return this.getFunctionIndex(ident, true);
        }
        public int getClassIndex(Ident otherClassIdent)
        {
            var classIdents = this.ParentClassesIdents;
            classIdents.Add(this.Name);
            for(int i = 0; i < classIdents.Count; i++)
            {
                if (((Ident)classIdents[i]).ReferencedType.ident.LastIdent.FullyQualifiedName.Equals(otherClassIdent.LastIdent.FullyQualifiedName))
                    return i;
            }
            return -1;
        }
        public Tuple<int, int> getFunctionIndex(Ident ident, bool allowDeepSearch)
        {
            Tuple<int, int> tuple = null;
            for (int i = 0; i < this.children.Count; i++)
            {
                var it = this.children[i];
                if(it is Function)
                {
                    if (((Function)it).Name.FullyQualifiedName.Equals(ident.LastIdent.FullyQualifiedName))
                    {
                        tuple = new Tuple<int, int>(this.parentClasses.Count + this.parentInterfaces.Count, i);
                        break;
                    }
                }
            }
            if(allowDeepSearch && tuple == null)
            {
                for (int i = 0; i < this.parentClasses.Count; i++)
                {
                    var it = this.parentClasses[i];
                    tuple = it.getFunctionIndex(ident);
                    if (tuple.Item1 != -1)
                    {
                        tuple = new Tuple<int, int>(i, tuple.Item2);
                        break;
                    }
                    else
                    {
                        tuple = null;
                    }
                }
                for (int i = 0; i < this.parentInterfaces.Count; i++)
                {
                    var it = this.parentInterfaces[i];
                    tuple = it.getFunctionIndex(ident);
                    if (tuple.Item1 != -1)
                    {
                        tuple = new Tuple<int, int>(this.parentClasses.Count + i, tuple.Item2);
                        break;
                    }
                    else
                    {
                        tuple = null;
                    }
                }
            }
            return tuple == null ? new Tuple<int, int>(-1, -1) : tuple;
        }
        public Tuple<int, int> getVariableIndex(Ident ident)
        {
            return this.getVariableIndex(ident, true);
        }
        public Tuple<int, int> getVariableIndex(Ident ident, bool allowDeepSearch)
        {
            Tuple<int, int> tuple = null;
            for (int i = 0; i < this.children.Count; i++)
            {
                var it = this.children[i];
                if (it is Variable)
                {
                    if (((Variable)it).Name.FullyQualifiedName.Equals(ident.LastIdent.FullyQualifiedName))
                    {
                        tuple = new Tuple<int, int>(this.parentClasses.Count + this.parentInterfaces.Count, i);
                        break;
                    }
                }
            }
            if (allowDeepSearch && tuple == null)
            {
                for (int i = 0; i < this.parentClasses.Count; i++)
                {
                    var it = this.parentClasses[i];
                    tuple = it.getVariableIndex(ident, false);
                    if (tuple.Item1 != -1)
                    {
                        tuple = new Tuple<int, int>(i, tuple.Item2);
                        break;
                    }
                    else
                    {
                        tuple = null;
                    }
                }
                for (int i = 0; i < this.parentInterfaces.Count; i++)
                {
                    var it = this.parentInterfaces[i];
                    tuple = it.getVariableIndex(ident);
                    if (tuple.Item1 != -1)
                    {
                        tuple = new Tuple<int, int>(this.parentClasses.Count + i, tuple.Item2);
                        break;
                    }
                    else
                    {
                        tuple = null;
                    }
                }
            }
            return tuple == null ? new Tuple<int, int>(-1, -1) : tuple;
        }


        public Interfaces.iOperatorFunction getOperatorFunction(OverridableOperator op)
        {
            return null;
        }
        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            foreach (var it in ClassContent)
            {
                if ((it is Interfaces.iFunction && ((Interfaces.iFunction)it).FunctionEncapsulation != Encapsulation.Static) && !(it is Function && ((Function)it).IsConstructor))
                    continue;
                if(it is Variable)
                    continue;
                it.writeOut(sw, cfg);
            }
        }
    }
}