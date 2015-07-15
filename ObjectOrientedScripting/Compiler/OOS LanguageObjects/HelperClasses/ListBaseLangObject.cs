using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects.HelperClasses
{
    public class ListBaseLangObject
    {
        List<BaseLangObject> list;

        public ListBaseLangObject()
        {
            list = new List<BaseLangObject>();
        }

        public void Add(BaseLangObject o)
        {
            list.Add(o);
        }
        public List<BaseLangObject> getList()
        {
            return list;
        }
    }
}
