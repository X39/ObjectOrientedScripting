using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Assignment : IInstruction
    {
        private IInstruction _parent;
        private IInstruction _leftHand;
        private IInstruction _rightHand;
        public IInstruction LeftHand { get { return this._leftHand; } }
        public IInstruction RightHand { get { return this._rightHand; } }
        private Assignment(IInstruction parent)
        {
            this._parent = parent;
            this._leftHand = null;
            this._rightHand = null;
        }
        public static Assignment parse(IInstruction parent, string input)
        {
            if (!input.Contains('='))
                throw new Exception("An Assignment requires the AssignmentOperator '='. Cannot parse: " + input);
            string leftHand = input.Substring(0, input.IndexOf('='));
            string rightHand = input.Substring(input.IndexOf('='));
            Assignment ass = new Assignment(parent);
            ass._leftHand = Identifier.parse(ass, leftHand);
            //ToDo: Parse rightHand
            return ass;
        }
        /**Prints out given instruction into StreamWriter as SQF. writer object is either a string or a StreamWriter*/
        public void printInstructions(object writer, bool printTabs = true)
        {
            if (!(writer is System.IO.StreamWriter))
                throw new Exception("printInstruction expected a StreamWriter object but received a " + writer.GetType().Name + " object");

            this._leftHand.printInstructions(writer, printTabs);
            ((System.IO.StreamWriter)writer).Write(" = ");
            this._rightHand.printInstructions(writer, false);
            ((System.IO.StreamWriter)writer).Write(';');
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
                result.AddRange(this._leftHand.getInstructions(t, recursiveUp, recursiveDown));
                result.AddRange(this._rightHand.getInstructions(t, recursiveUp, recursiveDown));
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
