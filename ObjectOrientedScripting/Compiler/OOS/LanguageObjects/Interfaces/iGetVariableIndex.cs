using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects.Interfaces
{
    public interface iGetVariableIndex
    {
        Tuple<int, int> getVariableIndex(Ident ident);
    }
}
