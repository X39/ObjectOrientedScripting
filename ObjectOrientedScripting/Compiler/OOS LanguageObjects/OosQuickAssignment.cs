using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class OosQuickAssignment : OosVariableAssignment
    {
        string name;
        public string Name { get { return this.name; } set { this.name = value; } }
        QuickAssignmentTypes quickAssignmentType;
        public QuickAssignmentTypes QuickAssignmentType { get { return quickAssignmentType; } set { quickAssignmentType = value; } }

        public OosQuickAssignment(string s)
        {
            this.name = s;
            quickAssignmentType = QuickAssignmentTypes.PlusPlus;
        }
    }
}
