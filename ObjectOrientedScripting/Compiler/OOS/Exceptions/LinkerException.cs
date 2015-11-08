using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects.Ex
{
    public class LinkerException : Exception
    {
        public ErrorStringResolver.LinkerErrorCode ErrorCode { get; internal set; }
        public LinkerException(ErrorStringResolver.LinkerErrorCode lec = ErrorStringResolver.LinkerErrorCode.UNKNOWN, int line = -1, int pos = -1, string file = default(string)) : base(ErrorStringResolver.resolve(lec, line, pos, file))
        {
            this.ErrorCode = lec;
        }
    }
}
