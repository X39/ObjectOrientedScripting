using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class OosQuickAssignment : OosVariableAssignment
    {
        QuickAssignmentTypes quickAssignmentType;
        public QuickAssignmentTypes QuickAssignmentType { get { return quickAssignmentType; } set { quickAssignmentType = value; } }

        public OosQuickAssignment()
        {
            quickAssignmentType = QuickAssignmentTypes.PlusPlus;
        }
    }
}
