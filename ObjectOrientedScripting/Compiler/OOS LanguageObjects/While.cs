using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler.OOS_LanguageObjects
{
    class While : IInstruction
    {
        private IInstruction _parent;
        private Expression _condition;
        private IInstruction _loopContent;
        private While(IInstruction parent)
        {
            this._parent = parent;
        }
        //IFinalizable
        public void finalize() { throw new NotImplementedException(); }
        //IInstruction
        public static While parse(StreamReader reader, IInstruction parent, string currentLine) { throw new NotImplementedException(); }
        public void printInstructions(object writer, bool printTabs = true)
        {
            if (!(writer is System.IO.StreamWriter))
                throw new Exception("printInstruction expected a StreamWriter object but received a " + writer.GetType().Name + " object");
            ((System.IO.StreamWriter)writer).Write("\r\n" + (printTabs ? new string('\t', this.getTabs()) : "") + "while {");
            this._condition.printInstructions(writer, false);
            ((System.IO.StreamWriter)writer).Write("} do");
            ((System.IO.StreamWriter)writer).Write("\r\n" + (printTabs ? new string('\t', this.getTabs()) : "") + "{");
            if (typeof(Scope).IsInstanceOfType(this._loopContent))
                ((Scope)this._loopContent).printInstructions(writer, printTabs, false, null);
            else
                this._loopContent.printInstructions(writer, printTabs);
            ((System.IO.StreamWriter)writer).Write("\r\n" + (printTabs ? new string('\t', this.getTabs()) : "") + "};");
        }
        public string parseInput(string input)
        {
            return this._parent.parseInput(input);
        }
        public void setParent(IInstruction parent)
        {
            if (parent is Scope)
                this._parent = parent;
            else
                throw new Exception("Changing parent to non-Scope is not allowed for '" + typeof(Assignment).Name + "' Objects.");
        }
        public IInstruction getParent()
        {
            return this._parent;
        }
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
                result.AddRange(this._condition.getInstructions(t, recursiveUp, recursiveDown));
                result.AddRange(this._loopContent.getInstructions(t, recursiveUp, recursiveDown));
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
