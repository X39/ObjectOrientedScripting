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
            obj.setParent(obj);
        }
        public void setParent(BaseLangObject obj)
        {
            parent = obj;
        }
        public BaseLangObject getParent()
        {
            return this.parent;
        }


        public T getFirstOf<T>(Type t) where T: BaseLangObject
        {
            if (this.GetType().Equals(t))
                return (T)this;
            else
            {
                if (this.parent != null)
                    return this.parent.getFirstOf<T>(t);
                else
                    return null;
            }
        }

    }
}
