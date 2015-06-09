using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler.OOS_LanguageObjects
{
    class New : IInstruction
    {
        private IInstruction _parent;
        private string _objectName;
        private Class _objectClass;
        private List<Expression> _values;

        private New(IInstruction parent, string objectName)
        {
            this._parent = parent;
            this._values = new List<Expression>();
            this._objectName = objectName;
            this._objectClass = null;
        }

        //IFinalizable
        public void finalize() { throw new NotImplementedException(); }
        //IInstruction
        public static New parse(IInstruction parent, string input)
        {
            if (!input.StartsWith("new ", StringComparison.OrdinalIgnoreCase))
                throw new Exception("NEW operation requires correct usage of the new keyword. Cannot parse: " + input);
            input = input.Substring("new ".Length);
            int indexOfResult = input.IndexOf('(');
            if (indexOfResult == 0)
            {
                if (input[0] == '(')
                    throw new Exception("Missing ObjectName. Cannot parse: " + input);
                else
                    throw new Exception("Syntax error, missing parameter bracket '('. Cannot parse: " + input);
            }
            string objectName = input.Substring(0, indexOfResult);
            input = input.Remove(indexOfResult);
            if (input.EndsWith(';'))
                input = input.Remove(input.Length - 1);
            if(!input.EndsWith(')'))
                throw new Exception("Syntax error, missing parameter bracket ')'. Cannot parse: " + input);
            New newObject = new New(parent, objectName);
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                switch(c)
                {
                    case ' ': case '\t': case '\r': case '\n':
                        break;
                    case '(': case ',':
                        int expressionEnd = Expression.findExpressionEnd(input.Substring(i));
                        string expression = input.Substring(i, expressionEnd);
                        newObject._values.Add(Expression.parse(newObject, expression));
                        i += expressionEnd;
                        break;
                    case ')':
                        if (i < input.Length - 1)
                            throw new Exception("Syntax error, unexpected EndOfOperation caused by ')' while parsing NEW operation. Cannot parse: " + input);
                        break;
                    default:
                        throw new Exception("Syntax error, unexpected char '" + c + "' while parsing NEW operation. Cannot parse: " + input);
                }
            }
            return newObject;
        }
        public void printInstructions(object writer, bool printTabs = true)
        {
            if (!(writer is System.IO.StreamWriter))
                throw new Exception("printInstruction expected a StreamWriter object but received a " + writer.GetType().Name + " object");
            ((System.IO.StreamWriter)writer).Write((printTabs ? new string('\t', this.getTabs()) : "") + '[');
            for (int i = 0; i < this._values.Count; i++)
            {
                if(i != 0)
                    ((System.IO.StreamWriter)writer).Write(", ");
                this._values[i].printInstructions(writer, false);
            }
            ((System.IO.StreamWriter)writer).Write("] call " + this._objectClass.getConstructorFunctionName());
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
