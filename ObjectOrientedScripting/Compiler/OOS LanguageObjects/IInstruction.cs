using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler.OOS_LanguageObjects
{
    public interface IInstruction
    {
        IInstruction getParent();
        void printInstructions(StreamWriter writer, bool printTabs = true);
        string parseInput(string input);
    }
}
