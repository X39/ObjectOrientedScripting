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
        private Identifier _leftHand;
        private IInstruction _rightHand;
        public Identifier LeftHand { get { return this._leftHand; } }
        public IInstruction RightHand { get { return this._rightHand; } }
        private Assignment(IInstruction parent)
        {
            this._parent = parent;
            this._leftHand = null;
            this._rightHand = null;
        }
        //IFinalizable
        public void finalize() { }
        //IInstruction
        public static Assignment parse(IInstruction parent, string input)
        {
            if (!input.Contains('='))
                throw new Exception("An Assignment requires the AssignmentOperator '='. Cannot parse: " + input);
            string leftHand = input.Substring(0, input.IndexOf('='));
            string rightHand = input.Substring(input.IndexOf('='));
            Assignment ass = new Assignment(parent);
            ass._leftHand = Identifier.parse(ass, leftHand);
            ass._rightHand = Expression.parse(ass, input);
            return ass;
        }
        public void printInstructions(object writer, bool printTabs = true)
        {
            if (!(writer is System.IO.StreamWriter))
                throw new Exception("printInstruction expected a StreamWriter object but received a " + writer.GetType().Name + " object");

            this._leftHand.printInstructions(writer, printTabs);
            ((System.IO.StreamWriter)writer).Write(" = ");
            this._rightHand.printInstructions(writer, false);
            ((System.IO.StreamWriter)writer).Write(';');
        }
        public string parseInput(string input)
        {
            return this._parent.parseInput(input);
        }
        public IInstruction getParent()
        {
            return this._parent;
        }
        public IInstruction[] getInstructions(Type t, bool recursiveUp = true, bool recursiveDown = false)
        {
            List<IInstruction> result = new List<IInstruction>();
            if (recursiveUp && recursiveDown)
                return this.getFirstOf(typeof(Namespace)).getInstructions(t, false, true);
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
        public IInstruction getFirstOf(Type t)
        {
            IInstruction firstOccurance = this.getParent().getFirstOf(t);
            return (firstOccurance == null ? (t.IsInstanceOfType(this) ? this : null) : firstOccurance);
        }
        public void addInstruction(IInstruction instr)
        {
            throw new Exception("An Identifier cannot have sub instructions");
        }
        public int getTabs()
        {
            return this._parent.getTabs();
        }
    }
}
