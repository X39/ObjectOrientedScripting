using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Variable : IInstruction
    {
        private IInstruction _parent;
        private IInstruction _assignment;
        private IInstruction _identifier;
        public IInstruction Identifier { get { return this._identifier; } }
        protected Variable() {}
        protected Variable(IInstruction parent)
        {
            this._parent = parent;
        }
        public static Variable parse(IInstruction parent, string toParse)
        {
            if (!toParse.StartsWith("var ", StringComparison.OrdinalIgnoreCase))
                throw new Exception("No variable assignment");
            toParse.Remove(0, "var ".Length);
            string instructionName = toParse.Substring(toParse.IndexOfAny(new char[] { ';', '=' }));
            Variable var = new Variable(parent);
            Identifier ident = Compiler.OOS_LanguageObjects.Identifier.parse(var, instructionName);
            Assignment assign = null;
            if(toParse.Contains('='))
                assign = Assignment.parse(var, toParse);
            var._identifier = ident;
            var._assignment = assign;
            return var;
        }
        /**Prints out given instruction into StreamWriter as SQF. writer object is either a string or a StreamWriter*/
        public void printInstructions(object writer, bool printTabs = true)
        {
            if (!(writer is System.IO.StreamWriter))
                throw new Exception("printInstruction expected a StreamWriter object but received a " + writer.GetType().Name + " object");
            if(this._assignment != null)
                this._assignment.printInstructions(writer, printTabs);
        }
        /**Parses given string input specially for this element (example use: foreach(var foo in bar) would replace every occurance of foo with _x and every occurence of _x with __x or something like that)*/
        public string parseInput(string input)
        {
            return this._parent.parseInput(input);
        }
        /**returns parent IInstruction which owns this IInstruction (only will return null for the oos namespace object which is the root node for anything)*/
        public IInstruction getParent()
        {
            return this._parent;
        }
        /**returns a list of child IInstructions with given type*/
        public IInstruction[] getInstructions(Type t, bool recursiveUp = true, bool recursiveDown = false)
        {
            List<IInstruction> result = new List<IInstruction>();
            if (recursiveUp && recursiveDown)
                throw new Exception("Cannot move up AND down at the same time");
            if (t.IsInstanceOfType(this))
                result.Add(this);
            if (recursiveUp)
                this._parent.getInstructions(t, recursiveUp, recursiveDown);
            if (recursiveDown)
            {
                result.AddRange(this._identifier.getInstructions(t, recursiveUp, recursiveDown));
                if(this._assignment != null)
                    result.AddRange(this._assignment.getInstructions(t, recursiveUp, recursiveDown));
            }
            return result.ToArray();
        }
        /**returns first occurance of given type in tree or NULL if nothing was found*/
        public IInstruction getFirstOf(Type t)
        {
            IInstruction firstOccurance = this.getParent().getFirstOf(t);
            return (firstOccurance == null ? (t.IsInstanceOfType(this) ? this : null) : firstOccurance);
        }
        /**Adds given instruction to child instruction list and checks if it is valid to own this instruction*/
        public void addInstruction(IInstruction instr)
        {
            throw new Exception("An Identifier cannot have sub instructions");
        }
        /**returns current tab ammount*/
        public int getTabs()
        {
            return this._parent.getTabs();
        }
    }
}
