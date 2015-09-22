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
        public List<T> getAllChildrenOf<T>(bool fullSearch = false, object stopObject = null) where T : pBaseLangObject
        {
            List<T> l = new List<T>();
            private_getAllChildrenOf<T>(l, fullSearch, stopObject);
            return l;
        }
        private bool private_getAllChildrenOf<T>(List<T> l, bool fullSearch, object stopObject) where T : pBaseLangObject
        {
            foreach (var obj in this.children)
            {
                if (obj == null)
                    continue;
                if (obj == stopObject)
                    return true;
                if (obj is T)
                    l.Add((T)obj);
                if (fullSearch)
                    if (obj.private_getAllChildrenOf<T>(l, fullSearch, stopObject))
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
