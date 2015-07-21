using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Wrapper
{
    public interface ICompiler
    {
        Version getVersion();
        void CheckSyntax(string filepath);
        void Translate(Project proj);
        void Compile(Project proj);
        void Preprocess(Project proj);
    }
}
