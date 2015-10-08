using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects.Ex
{
    public class InvalidIdentType : Exception
    {
        private IdentType current;
        private IdentType expected;
        public IdentType Current { get { return this.current; } }
        public IdentType Expected { get { return this.expected; } }
        public InvalidIdentType(IdentType c, IdentType e = IdentType.NA)
            : base(
                "Invalid IdentType. Got '" + Enum.GetName(typeof(IdentType), c) + "'" +
                (e == IdentType.NA ? "'" : "' expected: '" + Enum.GetName(typeof(IdentType), c) + "'")
                )
        {
            this.current = c;
            this.expected = e;
        }
    }
}
