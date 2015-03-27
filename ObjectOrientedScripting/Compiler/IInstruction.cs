using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public interface IInstruction
    {
        /**Prints out given instruction into StreamWriter as SQF. writer object is either a string or a StreamWriter*/
        void printInstructions(object writer, bool printTabs = true);
        /**Parses given string input specially for this element (example use: foreach(var foo in bar) would replace every occurance of foo with _x and every occurence of _x with __x or something like that)*/
        string parseInput(string input);
        /**returns parent IInstruction which owns this IInstruction (only will return null for the oos namespace object which is the root node for anything)*/
        IInstruction getParent();
        /**returns a list of child IInstructions with given type*/
        IInstruction[] getChildInstructions(Type t, bool recursive = true);
        /**Adds given instruction to child instruction list and checks if it is valid to own this instruction*/
        void addInstruction(IInstruction instr);
    }
}
