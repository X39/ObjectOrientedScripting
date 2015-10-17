using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public partial class pBaseLangObject
    {
        public List<pBaseLangObject> children;
        private pBaseLangObject parent;
        public pBaseLangObject Parent { get { return this.parent; } set { this.parent = value; } }
        public bool HasScope { get { return false; } }
        public int ScopeCount { get { return 0; } }
        public List<pBaseLangObject> getScopeItems(int scopeIndex)
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
            this.children.Add(blo);
        }
        public virtual int finalize()
        {
            int errCount = 0;
            foreach (pBaseLangObject blo in children)
                if(blo != null)
                    errCount += blo.finalize();
            if (this is Interfaces.iTemplate && ((Interfaces.iTemplate)this).template != null)
                errCount += ((Interfaces.iTemplate)this).template.doFinalize();
            errCount += this.doFinalize();
            return errCount;
        }
        public T getFirstOf<T>() where T : pBaseLangObject
        {
            if (this is T)
            {
                return (T)this;
            }
            else
            {
                if (this.parent != null)
                    return this.parent.getFirstOf<T>();
                else
                    return null;
            }
        }
        public int countTo<T>() where T : pBaseLangObject
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
        public T getLastOf<T>() where T : pBaseLangObject
        {
            if (this.parent == null)
                return null;
            if(this.parent is T)
                return this.parent.getLastOf<T>();
            if (this is T)
                return (T)this;
            return null;
        }
        public List<T> getAllChildrenOf<T>(bool fullSearch = false, object stopObject = null, int deepness = -1, int scopeIndex = -1) where T : pBaseLangObject
        {
            List<T> l = new List<T>();
            private_getAllChildrenOf<T>(l, fullSearch, stopObject, deepness, scopeIndex);
            return l;
        }
        private bool private_getAllChildrenOf<T>(List<T> l, bool fullSearch, object stopObject, int deepness, int scopeIndex) where T : pBaseLangObject
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
                    l.Add((T)obj);
                if (fullSearch)
                    if (obj.private_getAllChildrenOf<T>(l, fullSearch, stopObject, deepness - 1, scopeIndex))
                        return true;
            }
            return false;
        }
        public bool isTypeInChildTree<T>() where T : pBaseLangObject
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
    }
}
