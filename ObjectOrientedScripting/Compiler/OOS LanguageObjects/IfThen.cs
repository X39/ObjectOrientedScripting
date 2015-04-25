using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler.OOS_LanguageObjects
{
    public class IfThen : IInstruction
    {
        private IInstruction _parent;
        private Scope _ifScope;
        private Expression _condition;
        private Scope _elseScope;
        public IfThen(IInstruction parent)
        {
            this._parent = parent;
        }
        public static IfThen parse(StreamReader reader, IInstruction parent, string currentLine)
        {
            throw new NotImplementedException();
        }
        /**Prints out given instruction into StreamWriter as SQF. writer object is either a string or a StreamWriter*/
        public void printInstructions(object writer, bool printTabs = true)
        {
            if (!(writer is System.IO.StreamWriter))
                throw new Exception("printInstruction expected a StreamWriter object but received a " + writer.GetType().Name + " object");
            ((System.IO.StreamWriter)writer).Write((printTabs ? new string('\t', this.getTabs()) : "") + "if ( ");
            this._condition.printInstructions(writer, printTabs);
            ((System.IO.StreamWriter)writer).Write(" ) then\r\n");
            this._ifScope.printInstructions(writer, printTabs);
            if (_elseScope.hasInstructions())
            {
                ((System.IO.StreamWriter)writer).Write((printTabs ? new string('\t', this.getTabs()) : "") + "else");
                this._elseScope.printInstructions(writer, printTabs);
            }
            ((System.IO.StreamWriter)writer).Write(";");
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
                return this.getFirstOf(typeof(Namespace)).getInstructions(t, false, true);
            if (t.IsInstanceOfType(this))
                result.Add(this);
            if (recursiveUp)
                this._parent.getInstructions(t, recursiveUp, recursiveDown);
            if (recursiveDown)
            {
                result.AddRange(this._ifScope.getInstructions(t, recursiveUp, recursiveDown));
                result.AddRange(this._condition.getInstructions(t, recursiveUp, recursiveDown));
                result.AddRange(this._elseScope.getInstructions(t, recursiveUp, recursiveDown));
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
            throw new Exception("IfThen cannot have sub instructions");
        }
        /**returns current tab ammount*/
        public int getTabs()
        {
            return this._parent.getTabs();
        }
    }
}
