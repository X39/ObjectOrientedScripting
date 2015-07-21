using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public enum ClassEncapsulation
    {
        PRIVATE,
        PUBLIC
    }
    public enum AssignmentOperators
    {
        PlusEquals,
        MinusEquals,
        MultipliedEquals,
        DividedEquals,
        Equals
    }
    public enum ExpressionOperator
    {
        AndAnd,
        And,
        OrOr,
        Or,
        ExplicitEquals,
        Equals,
        Plus,
        Minus,
        Multiplication,
        Division,
        Larger,
        LargerEquals,
        Smaller,
        SmallerEquals,
        NA
    }
    public enum QuickAssignmentTypes
    {
        PlusPlus,
        MinusMinus
    }
}
