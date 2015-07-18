using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class BaseLangObject
    {
        BaseLangObject parent;
        public BaseLangObject Parent { get { return parent; } set { parent = value; } }
        List<BaseLangObject> children;
        public List<BaseLangObject> Children { get { return children; } set { children = value; } }

        public BaseLangObject()
        {
            parent = null;
            children = new List<BaseLangObject>();
        }

        public void addChild(BaseLangObject obj)
        {
            children.Add(obj);
            if (obj == null)
                return;
            if (obj.getParent() != null)
                throw new Exception("ToBeCreated, Please create a bug message if you experience this exception");
            obj.setParent(this);
        }
        public void setParent(BaseLangObject obj)
        {
            parent = obj;
        }
        public BaseLangObject getParent()
        {
            return this.parent;
        }


        public T getFirstOf<T>() where T: BaseLangObject
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

        public List<T> getAllChildrenOf<T>(bool fullSearch = false) where T : BaseLangObject
        {
            List<T> l = new List<T>();
            foreach (var obj in this.Children)
            {
                if (obj is T)
                    l.Add((T)obj);
                if (fullSearch)
                    l.AddRange(obj.getAllChildrenOf<T>());
            }
            return l;
        }
        public bool isTypeInChildTree<T>() where T : BaseLangObject
        {
            foreach (var obj in this.Children)
            {
                if (obj is T)
                    return true;
                else
                    if (obj.isTypeInChildTree<T>())
                        return true;
            }
            return false;
        }

    }
}
