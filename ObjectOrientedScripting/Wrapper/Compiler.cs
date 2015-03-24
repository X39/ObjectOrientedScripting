using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ObjectOrientedScripting
{
    interface Compiler
    {
        Version getVersion();
        void Run(Project proj);
    }
}
