using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects.HelperClasses
{
    public class ListString
    {
        List<string> list;

        public ListString()
        {
            list = new List<string>();
        }

        public void Add(string o)
        {
            list.Add(o);
        }
        public List<string> getList()
        {
            return list;
        }
    }
}
