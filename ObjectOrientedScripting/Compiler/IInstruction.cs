using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public interface IInstruction : IFinalizable
    {
        /// <summary>
        /// Prints out given instruction into StreamWriter as SQF. writer object is either a string or a StreamWriter
        /// </summary>
        /// <param name="writer">Contains either a string or a StreamWriter depending on the object</param>
        /// <param name="printTabs">Sets wether this IInstruction should print instructions</param>
        void printInstructions(object writer, bool printTabs = true);
        /// <summary>
        /// Parses given string input specially for this element
        /// Should refer to parents parseInput function if not used
        /// 
        /// example use:
        /// foreach(var foo in bar) would replace every occurance of foo with _x
        /// and every occurence of _x with __x or something like that
        /// </summary>
        /// <param name="input">A variable name</param>
        /// <returns>The new variable name</returns>
        string parseInput(string input);
        /// <summary>
        /// returns parent IInstruction which owns this IInstruction (only will return null for the oos namespace object which is the root node for everything)
        /// </summary>
        /// <returns>Objects parent IInstruction</returns>
        IInstruction getParent();
        /// <summary>
        /// Iterates current IInstruction tree up/down and returns given type
        /// </summary>
        /// <param name="t"></param>
        /// <param name="recursiveUp"></param>
        /// <param name="recursiveDown"></param>
        /// <returns>Array of child IInstructions with given type</returns>
        IInstruction[] getInstructions(Type t, bool recursiveUp = true, bool recursiveDown = false);
        /// <summary>
        /// Searches for the first occurance in current IInstruction tree of given type
        /// </summary>
        /// <param name="t">type to search for</param>
        /// <returns>Either the first object hit while moving the tree upward or null</returns>
        IInstruction getFirstOf(Type t);
        /// <summary>
        /// Adds given instruction to child instruction list and checks if it is valid to own this instruction
        /// </summary>
        /// <param name="instr">IInstruction to add</param>
        void addInstruction(IInstruction instr);
        /// <summary>
        /// "Calculate" current tab ammount
        /// </summary>
        /// <returns>Ammount of tab for current scope layer</returns>
        int getTabs();
    }
}
