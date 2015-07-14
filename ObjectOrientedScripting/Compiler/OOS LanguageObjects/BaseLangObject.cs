using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public partial class BaseLangObject
    {
        BaseLangObject parent;
        public BaseLangObject Parent { get { return parent; } set { parent = value; } }
        List<BaseLangObject> children;
        public List<BaseLangObject> Children { get { return children; } set { children = value; } }

        public BaseLangObject()
        {
            parent = null;
        }

        public void addChild(BaseLangObject obj)
        {
            children.Add(obj);
        }
        public void setParent(BaseLangObject obj)
        {
            parent = obj;
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
