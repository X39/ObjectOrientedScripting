using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler.OOS_LanguageObjects
{
    class Expression : IInstruction
    {
        /// <summary>
        /// Searches for the end index of an expression in given string
        /// </summary>
        /// <param name="s">String that should contain an expression</param>
        /// <returns>0 if there is no expression inside of the string or the proper index that marks the end</returns>
        public static int findExpressionEnd(string s) { throw new NotImplementedException(); }

        //IFinalizable
        public void finalize() { throw new NotImplementedException(); }
        //IInstruction
        public static Expression parse(IInstruction parent, string currentLine) { throw new NotImplementedException(); }
        public void printInstructions(object writer, bool printTabs = true) { throw new NotImplementedException(); }
        public string parseInput(string input) { throw new NotImplementedException(); }
        public IInstruction getParent() { throw new NotImplementedException(); }
        public IInstruction[] getInstructions(Type t, bool recursiveUp = true, bool recursiveDown = false) { throw new NotImplementedException(); }
        public IInstruction getFirstOf(Type t) { throw new NotImplementedException(); }
        public void addInstruction(IInstruction instr) { throw new NotImplementedException(); }
        public int getTabs() { throw new NotImplementedException(); }
    }
}
