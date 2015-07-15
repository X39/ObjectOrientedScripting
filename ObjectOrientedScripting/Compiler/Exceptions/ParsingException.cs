using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects.Ex
{
    public class ParsingException : Exception
    {
        public ParsingException(string s = "") : base(s.Length == 0 ? "One or more errors have been found!" : s) { }
    }
}
