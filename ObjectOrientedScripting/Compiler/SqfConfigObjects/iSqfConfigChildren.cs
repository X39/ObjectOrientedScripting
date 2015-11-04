using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler.SqfConfigObjects
{
    public interface iSqfConfigChildren : iSqfConfig
    {
        List<iSqfConfig> Children { get; }
    }
}
