using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    class Return : IInstruction
    {
        private IInstruction _parent;
        private IInstruction _value;
        private Return(IInstruction parent)
        {
            this._parent = parent;
        }
        //IFinalizable
        public void finalize() { }
        //IInstruction
        public static Return parse(IInstruction parent, string toParse)
        {
            Return r = new Return(parent);
            r._value = Expression.parse(r, toParse);
            return r;
        }
        public void printInstructions(object writer, bool printTabs = true)
        {
            //ToDo: Get FunctionScope and do breakTo "scopeName" to it
            if (!(writer is System.IO.StreamWriter))
                throw new Exception("printInstruction expected a StreamWriter object but received a " + writer.GetType().Name + " object");
            IInstruction scope = this.getFirstOf(typeof(Scope));
            if (scope == null)
                throw new Exception("return instruction is not bound to an upper scope");
            string scopeName = ((Scope)scope).getScopeName();
            ((System.IO.StreamWriter)writer).Write((printTabs ? new string('\t', this.getTabs()) : "") + "_return" + scopeName + " = ");
            _value.printInstructions(writer, false);
            ((System.IO.StreamWriter)writer).Write((printTabs ? new string('\t', this.getTabs()) : "") + "breakTo \"" + scopeName + "\";\r\n");
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
                result.AddRange(this._value.getInstructions(t, recursiveUp, recursiveDown));
            return result.ToArray();
        }
        public IInstruction getFirstOf(Type t)
        {
            IInstruction firstOccurance = this.getParent().getFirstOf(t);
            return (firstOccurance == null ? (t.IsInstanceOfType(this) ? this : null) : firstOccurance);
        }
        public void addInstruction(IInstruction instr)
        {
            throw new Exception("A return instruction cannot have multiple sub instructions");
        }
        public int getTabs()
        {
            return this._parent.getTabs();
        }
    }
}
