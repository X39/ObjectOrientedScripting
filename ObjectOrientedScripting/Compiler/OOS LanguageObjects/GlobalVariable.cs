using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler.OOS_LanguageObjects
{
    public class GlobalVariable : IVariable
    {
        //IFinalizable
        public void finalize() { throw new NotImplementedException(); }
        //IInstruction
        public static GlobalVariable parse(StreamReader reader, string currentLine) { throw new NotImplementedException(); }
        public void printInstructions(object writer, bool printTabs = true) { throw new NotImplementedException(); }
        public string parseInput(string input) { throw new NotImplementedException(); }
        public IInstruction getParent() { throw new NotImplementedException(); }
        public IInstruction[] getInstructions(Type t, bool recursiveUp = true, bool recursiveDown = false) { throw new NotImplementedException(); }
        public IInstruction getFirstOf(Type t) { throw new NotImplementedException(); }
        public void addInstruction(IInstruction instr) { throw new NotImplementedException(); }
        public int getTabs() { throw new NotImplementedException(); }
        //IVariable
        public Identifier getVariableIdentifier() { throw new NotImplementedException(); }
    }
}
