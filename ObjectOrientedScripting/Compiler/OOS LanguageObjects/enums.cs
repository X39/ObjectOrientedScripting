using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public enum Encapsulation
    {
        Private,
        Protected,
        Public,
        Static,
        NA   //NA
    }
    public enum VarType
    {
        Scalar,
        Bool,
        String,
        Auto,
        Void,
        Object
    }
    public enum IdentType
    {
        Name,
        GlobalAccess,
        RelativeAccess,
        GlobalAccess_Instance,
        RelativeAccess_Instance,
        NA
    }
}
