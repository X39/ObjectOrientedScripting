using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class oosClass : pBaseLangObject, Interfaces.iName, Interfaces.iGetFunctionIndex, Interfaces.iGetVariableIndex
    {
        public Ident Name { get { return ((Ident)this.children[0]); } set { if (!value.IsSimpleIdentifier) throw new Ex.InvalidIdentType(value.getIdentType(), IdentType.Name); this.children[0] = value; } }
        int endMarker;
        public List<pBaseLangObject> ParentClassesIdents { get { return this.children.GetRange(1, endMarker); } }
        public List<pBaseLangObject> ClassContent { get { return this.children.GetRange(endMarker + 1, this.children.Count - (endMarker + 1)); } }
        private List<oosClass> parentClasses;
        private List<oosInterface> parentInterfaces;
        
        public string FullyQualifiedName
        {
            get
            {
                string s = "::";
                List<Interfaces.iName> parentList = new List<Interfaces.iName>();
                pBaseLangObject curParent = Parent;
                while (curParent != null)
                {
                    if (curParent is Interfaces.iName)
                        parentList.Add((Interfaces.iName)curParent);
                    curParent = curParent.Parent;
                }
                parentList.Reverse();
                foreach (Interfaces.iName it in parentList)
                    s += it.Name.OriginalValue + "::";
                s += this.Name.OriginalValue;
                return s;
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
        public override int doFinalize()
        {
            int errCount = 0;
            foreach (var it in ParentClassesIdents)
            {
                if (!(it is Ident))
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.UNKNOWN, Name.Line, Name.Pos));
                    errCount++;
                    continue;
                }
                if (((Ident)it).ReferencedObject is oosClass)
                {
                    var parentClass = (oosClass)((Ident)it).ReferencedObject;
                    this.parentClasses.Add(parentClass);
                    this.parentClasses.AddRange(parentClass.parentClasses);
                }
                else if (((Ident)it).ReferencedObject is oosInterface)
                {
                    this.parentInterfaces.Add((oosInterface)((Ident)it).ReferencedObject);
                }
                else
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.UNKNOWN, Name.Line, Name.Pos));
                    errCount++;
                }
            }
            return errCount;
        }

        public void addParentClass(Ident blo)
        {
            this.children.Add(blo);
        }
        public override string ToString()
        {
            return this.FullyQualifiedName;
        }
        public Tuple<int, int> getFunctionIndex(Ident ident)
        {
            return this.getFunctionIndex(ident, true);
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
                    tuple = it.getFunctionIndex(ident, false);
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
    }
}
