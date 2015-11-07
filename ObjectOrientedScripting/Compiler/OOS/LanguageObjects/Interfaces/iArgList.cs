using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects.Interfaces
{
    public interface iArgList
    {
        /// <summary>
        /// Returns the Arglist required for this iFunction
        /// </summary>
        List<VarTypeObject> ArgList { get; }
    }
}
