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
        public static While parse(IInstruction parent, string currentLine) { throw new NotImplementedException(); }
        /**Prints out given instruction into StreamWriter as SQF. writer object is either a string or a StreamWriter*/
        public void printInstructions(object writer, bool printTabs = true) { throw new NotImplementedException(); }
        /**Parses given string input specially for this element (example use: foreach(var foo in bar) would replace every occurance of foo with _x and every occurence of _x with __x or something like that)*/
        public string parseInput(string input) { throw new NotImplementedException(); }
        /**returns parent IInstruction which owns this IInstruction (only will return null for the oos namespace object which is the root node for anything)*/
        public IInstruction getParent() { throw new NotImplementedException(); }
        /**returns a list of child IInstructions with given type*/
        public IInstruction[] getInstructions(Type t, bool recursiveUp = true, bool recursiveDown = false) { throw new NotImplementedException(); }
        /**returns first occurance of given type in tree or NULL if nothing was found*/
        public IInstruction getFirstOf(Type t) { throw new NotImplementedException(); }
        /**Adds given instruction to child instruction list and checks if it is valid to own this instruction*/
        public void addInstruction(IInstruction instr) { throw new NotImplementedException(); }
        /**returns current tab ammount*/
        public int getTabs() { throw new NotImplementedException(); }
    }
}
