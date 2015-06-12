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
        //IFinalizable
        public void finalize() { }
        //IInstruction
        public static IfThen parse(StreamReader reader, IInstruction parent, string currentLine, IfThen oldIf = null)
        {
            if (oldIf == null)
            {
                IfThen ifthen = new IfThen(parent);
                currentLine = currentLine.Remove(0, 2).Trim();
                ifthen._condition = Expression.parse(ifthen, currentLine);
                ifthen._ifScope = Scope.parse(ifthen, reader);
                return ifthen;
            }
            else
            {
                //ToDo: Implement Else If
                currentLine = currentLine.Remove(0, 4).Trim();
                oldIf._elseScope = Scope.parse(oldIf, reader);
                return oldIf;
            }
        }
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
        public IInstruction getFirstOf(Type t)
        {
            IInstruction firstOccurance = this.getParent().getFirstOf(t);
            return (firstOccurance == null ? (t.IsInstanceOfType(this) ? this : null) : firstOccurance);
        }
        public void addInstruction(IInstruction instr)
        {
            throw new Exception("IfThen cannot have sub instructions");
        }
        public int getTabs()
        {
            return this._parent.getTabs();
        }
    }
}
