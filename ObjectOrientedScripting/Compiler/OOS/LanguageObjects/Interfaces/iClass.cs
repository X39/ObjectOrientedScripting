using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects.Interfaces
{
    public interface iClass : iName
    {
        List<Ident> ExtendedClasses { get; }
        VarTypeObject VTO { get; }
        iOperatorFunction getOperatorFunction(OverridableOperator op);
    }
}
