using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler.OOS_LanguageObjects
{
    public abstract partial class pBaseLangObject
    {
        public List<pBaseLangObject> children;
        private pBaseLangObject parent;
        public pBaseLangObject Parent { get { return this.parent; } set { this.parent = value; } }
        public bool HasScope { get { return false; } }
        public int ScopeCount { get { return 0; } }
        public bool IsFinalized { get; internal set; }

        public virtual List<pBaseLangObject> getScopeItems(int scopeIndex)
        {
            if (scopeIndex < 0)
                return this.children;
            return new List<pBaseLangObject>();
        }

        public pBaseLangObject(pBaseLangObject parent)
        {
            this.children = new List<pBaseLangObject>();
            this.parent = parent;
        }

        public void addChild(pBaseLangObject blo)
        {
            if(blo == null || !this.children.Contains(blo))
                this.children.Add(blo);
        }
        public virtual int finalize()
        {
            if (this.IsFinalized)
                return 0;
            int errCount = 0;
            foreach (pBaseLangObject blo in children)
                if(blo != null)
                    errCount += blo.finalize();
            if (this is Interfaces.iTemplate && ((Interfaces.iTemplate)this).TemplateObject != null)
                errCount += ((Interfaces.iTemplate)this).TemplateObject.finalize();

            if (this is Interfaces.iHasType && ((Interfaces.iHasType)this).ReferencedType != null && ((Interfaces.iHasType)this).ReferencedType.IsObject)
                errCount += ((Interfaces.iHasType)this).ReferencedType.ident.finalize();
            errCount += this.doFinalize();
            this.IsFinalized = true;
            return errCount;
        }
        public T getFirstOf<T>(bool allowThis = true)
        {
            if (allowThis && this is T)
            {
                return (T)(object)this;
            }
            else
            {
                if (this.parent != null)
                    return this.parent.getFirstOf<T>();
                else
                    return default(T);
            }
        }
        public List<T> getAllParentsOf<T>()
        {
            List<T> retList = new List<T>();
            if (this is T)
                retList.Add((T)(object)this);
            if (this.parent != null)
                retList.AddRange(this.parent.getAllParentsOf<T>());
            return retList;
        }
        public int countTo<T>()
        {
            if (this is T)
            {
                return 1;
            }
            else
            {
                if (this.parent != null)
                    return 1 + this.parent.countTo<T>();
                else
                    return 1;
            }
        }
        public T getLastOf<T>()
        {
            if (this.parent == null)
                return default(T);
            if(this.parent is T)
                return this.parent.getLastOf<T>();
            if (this is T)
                return (T)(object)this;
            return default(T);
        }
        public List<T> getAllChildrenOf<T>(bool fullSearch = false, object stopObject = null, int deepness = -1, int scopeIndex = -1)
        {
            List<T> l = new List<T>();
            private_getAllChildrenOf<T>(l, fullSearch, stopObject, deepness, scopeIndex);
            return l;
        }
        private bool private_getAllChildrenOf<T>(List<T> l, bool fullSearch, object stopObject, int deepness, int scopeIndex)
        {
            if (deepness == 0)
                return false;
            foreach (var obj in this.getScopeItems(scopeIndex))
            {
                if (obj == null)
                    continue;
                if (obj == stopObject)
                    return true;
                if (obj is T)
                    l.Add((T)(object)obj);
                if (fullSearch)
                    if (obj.private_getAllChildrenOf<T>(l, fullSearch, stopObject, deepness - 1, scopeIndex))
                        return true;
            }
            return false;
        }
        public bool isTypeInChildTree<T>()
        {
            foreach (var obj in this.children)
            {
                if (obj is T)
                    return true;
                else
                    if (obj.isTypeInChildTree<T>())
                        return true;
            }
            return false;
        }

        public virtual int doFinalize() { return 0; }
        public abstract void writeOut(StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg);
    }
}
