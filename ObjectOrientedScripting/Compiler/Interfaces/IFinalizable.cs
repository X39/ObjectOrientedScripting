using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    interface IFinalizable
    {
        /// <summary>
        /// Will be callen to finalize an object as last step of some process
        /// </summary>
        void finalize();
    }
}
