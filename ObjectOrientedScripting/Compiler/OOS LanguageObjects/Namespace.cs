using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    class Namespace : IInstruction
    {
        private Namespace _parent;
        private List<IInstruction> _childs;
        private string _name;
        public string Name { get { return (this._parent.Name == null ? "" : this._parent.Name + "_") + this._name; } }
        private Namespace(Namespace parent, string name)
        {
            this._parent = parent;
            this._name = name;
            this._childs = new List<IInstruction>();
        }
        /**Prints out given instruction into StreamWriter as SQF. writer object is either a string or a StreamWriter*/
        void printInstructions(object writer, bool printTabs = true);
        /**Parses given string input specially for this element (example use: foreach(var foo in bar) would replace every occurance of foo with _x and every occurence of _x with __x or something like that)*/
        string parseInput(string input);
        /**returns parent IInstruction which owns this IInstruction (only will return null for the oos namespace object which is the root node for anything)*/
        IInstruction getParent();
        /**returns a list of child IInstructions with given type*/
        IInstruction[] getInstructions(Type t, bool recursiveUp = true, bool recursiveDown = false);
        /**returns first occurance of given type in tree or NULL if nothing was found*/
        IInstruction getFirstOf(Type t);
        /**Adds given instruction to child instruction list and checks if it is valid to own this instruction*/
        void addInstruction(IInstruction instr);
        /**returns current tab ammount*/
        int getTabs();
    }
}
