using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects.Interfaces
{
    public interface iFunction : iName, iArgList, iCodeBlock, iHasType
    {
        /// <summary>
        /// Return type of this iFunction
        /// </summary>
        VarTypeObject ReturnType { get; }
        /// <summary>
        /// Returns a Template object which then can deref some unknown class conflicts in
        /// ArgList field
        /// </summary>
        Template TemplateArguments { get; }
        /// <summary>
        /// Returns functions encapsulation
        /// </summary>
        Encapsulation FunctionEncapsulation { get; }
        bool IsAsync { get; }
        bool IsConstructor { get; }
    }
}
