using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects.Interfaces
{
    public interface iFunction : iName
    {
        VarTypeObject ReturnType { get; }
        Template TemplateArguments { get; }
        Encapsulation FunctionEncapsulation { get; }
        List<pBaseLangObject> ArgList { get; }
    }
}
