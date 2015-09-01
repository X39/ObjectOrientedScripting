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
        public void finalize()
        {
            foreach (pBaseLangObject blo in children)
                blo.finalize();
            this.doFinalize();
        }
        public T getFirstOf<T>() where T : pBaseLangObject
        {
            if (this is T)
                return (T)this;
            else
            {
                if (this.parent != null)
                    return this.parent.getFirstOf<T>();
                else
                    return null;
            }
        }
        public List<T> getAllChildrenOf<T>(bool fullSearch = false) where T : pBaseLangObject
        {
            List<T> l = new List<T>();
            foreach (var obj in this.children)
            {
                if (obj is T)
                    l.Add((T)obj);
                if (fullSearch)
                    l.AddRange(obj.getAllChildrenOf<T>());
            }
            return l;
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

        public virtual void doFinalize();
    }
}
