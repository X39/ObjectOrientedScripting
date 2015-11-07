using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Wrapper
{
    public interface ICompiler_1
    {
        Version getVersion();
        void setFlags(string[] strArr);
        void CheckSyntax(string filepath);
        void Compile(Project proj);
    }
}
