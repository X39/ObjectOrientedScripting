using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class OosVariableAssignment : BaseLangObject
    {
        AssignmentOperators assignmentOperator;
        public AssignmentOperators AssignmentOperator { get { return assignmentOperator; } set { assignmentOperator = value; } }
        public BaseLangObject Variable { get { return this.Children[0]; } set { this.Children[0] = value; if(value != null) value.setParent(this); } }
        public BaseLangObject Value { get { return this.Children[1]; } set { this.Children[1] = value; if(value != null) value.setParent(this); } }
        string arrayPosition;
        public string ArrayPosition { get { return arrayPosition; } set { arrayPosition = value; } }

        public OosVariableAssignment()
        {
            this.addChild(null);
            this.addChild(null);
            arrayPosition = "";
        }
    }
}
