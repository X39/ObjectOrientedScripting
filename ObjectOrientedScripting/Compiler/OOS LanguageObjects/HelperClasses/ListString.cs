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

        public void add(string o)
        {
            list.Add(o);
        }
        public List<string> getList()
        {
            return list;
        }
    }
}
