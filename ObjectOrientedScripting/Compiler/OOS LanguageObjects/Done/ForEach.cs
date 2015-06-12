using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler.OOS_LanguageObjects
{
    class ForEach : IInstruction
    {
        private IInstruction _parent;
        private IInstruction _runAfter;
        private LocalVariable _variable;
        private Expression _content;


        private ForEach() { }

        //IFinalizable
        public void finalize() { }
        //IInstruction
        public static ForEach parse(StreamReader reader, IInstruction parent, string currentLine)
        {
            if (!currentLine.StartsWith("foreach", StringComparison.OrdinalIgnoreCase))
                throw new Exception("Parsing Error, ForEach does not starts with foreach");
            currentLine = currentLine.Substring("foreach".Length).Trim();
            if (!currentLine.StartsWith("(", StringComparison.OrdinalIgnoreCase))
                throw new Exception("Parsing Error, ForEach is missing brackets '('");
            currentLine = currentLine.Substring("(".Length).Trim();
            if (!currentLine.StartsWith("var ", StringComparison.OrdinalIgnoreCase))
                throw new Exception("Parsing Error, ForEach is lacking variable definition");
            string variableDefinition = currentLine.Substring("var ".Length).Trim();
            int i = variableDefinition.IndexOf("in");
            if(i == 0)
            {
                if (variableDefinition.StartsWith("in"))
                    throw new Exception("Parsing Error, ForEach variable is using a reserved keyword");
                else
                    throw new Exception("Parsing Error, ForEach is missing content definition");
            }
            variableDefinition = currentLine.Substring(0, i).Trim();
            currentLine = currentLine.Substring(i + "in".Length).Trim();
            ForEach forEach = new ForEach();
            forEach._parent = parent;
            forEach._variable = LocalVariable.parse(forEach, variableDefinition);
            if(forEach._variable.Instr is Assignment)
                throw new Exception("Parsing Error, ForEach variable is getting assigned");

            if (currentLine.EndsWith('{'))
               currentLine = currentLine.Remove(currentLine.Length - 1).Trim();
            if (currentLine.EndsWith(')'))
                currentLine = currentLine.Remove(currentLine.Length - 1).Trim();
            else
                throw new Exception("Parsing Error, ForEach is not getting closed proper");
            forEach._content = Expression.parse(forEach, currentLine);
            forEach._runAfter = Scope.parse(forEach, reader);
            return forEach;
        }
        public void printInstructions(object writer, bool printTabs = true)
        {
            if (!(writer is System.IO.StreamWriter))
                throw new Exception("printInstruction expected a StreamWriter object but received a " + writer.GetType().Name + " object");
            if (_runAfter is Scope)
            {
                ((Scope)this._runAfter).printInstructions(writer, true, false, null);
            }
            else
            {
                ((System.IO.StreamWriter)writer).Write("\r\n" + (printTabs ? new string('\t', this.getTabs()) : "") + "{\r\n");
                this._runAfter.printInstructions(writer, false);
                ((System.IO.StreamWriter)writer).Write("\r\n" + (printTabs ? new string('\t', this.getTabs()) : "") + "}");
            }
            ((System.IO.StreamWriter)writer).Write("\r\n" + (printTabs ? new string('\t', this.getTabs()) : "") + "foreach (");
            this._content.printInstructions(writer, false);
            ((System.IO.StreamWriter)writer).Write(");");
        }
        public string parseInput(string input)
        {
            IInstruction instr = this._variable.Instr;
            if(instr is Assignment)
            {
                instr = ((Assignment)instr).LeftHand;
            }
            string identifierName = ((Identifier)instr).Name;
            if(input.Equals("_x", StringComparison.OrdinalIgnoreCase))
            {
                return input + "_";
            }
            if (input.Equals(identifierName, StringComparison.OrdinalIgnoreCase))
            {
                return "_x";
            }
            return input;
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
                result.AddRange(this._runAfter.getInstructions(t, recursiveUp, recursiveDown));
                result.AddRange(this._variable.getInstructions(t, recursiveUp, recursiveDown));
                result.AddRange(this._content.getInstructions(t, recursiveUp, recursiveDown));
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
            this._runAfter.addInstruction(instr);
        }
        public int getTabs()
        {
            return this._parent.getTabs();
        }
    }
}
