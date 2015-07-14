using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects.Ex
{
    public class InvalidParent : Exception
    {
        public InvalidParent(string s = "") : base(s.Length == 0 ? "Parent is of invalid type" : s) { }
    }
}
