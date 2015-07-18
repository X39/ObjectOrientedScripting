using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.SqfConfigObjects
{
    public interface iSqfConfig
    {
        string Name { get; set; }
        void addChild(iSqfConfig obj);
    }
}
