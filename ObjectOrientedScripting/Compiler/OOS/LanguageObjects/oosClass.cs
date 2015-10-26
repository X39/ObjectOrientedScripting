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
        private List<pBaseLangObject> ParentClassesIdents { get { return this.children.GetRange(1, endMarker); } }
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
                if (((Ident)classIdents[i]).FullyQualifiedName.Equals(otherClassIdent.FullyQualifiedName))
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
                    if (((Function)it).Name.FullyQualifiedName.Equals(ident.FullyQualifiedName))
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
                    if (((Variable)it).Name.FullyQualifiedName.Equals(ident.FullyQualifiedName))
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


        public Interfaces.iOperatorFunction getOperatorFunction(OperatorFunctions op)
        {
            return null;
        }
    }
}
