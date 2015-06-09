using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Native : IInstruction
    {
        private IInstruction _parent;
        private string _value;
        private Native(IInstruction parent, string value)
        {
            this._parent = parent;
            this._value = value;
        }
        //IFinalizable
        public void finalize() { throw new NotImplementedException(); }
        //IInstruction
        public static Native parse(IInstruction parent, string toParse)
        {
            toParse = toParse.Trim();
            if (toParse.StartsWith("native", StringComparison.OrdinalIgnoreCase))
                toParse = toParse.Remove("native".Length);
            return new Native(parent, toParse);
        }
        public void printInstructions(object writer, bool printTabs = true)
        {
            ((System.IO.StreamWriter)writer).Write((printTabs ? new string('\t', this.getTabs()) : "") + this._value);
        }
        public string parseInput(string input)
        {
            return input;
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
            //if (recursiveDown)
            //    result.AddRange(new IInstruction[] { });
            return result.ToArray();
        }
        public IInstruction getFirstOf(Type t)
        {
            IInstruction firstOccurance = this.getParent().getFirstOf(t);
            return (firstOccurance == null ? (t.IsInstanceOfType(this) ? this : null) : firstOccurance);
        }
        public void addInstruction(IInstruction instr)
        {
            throw new Exception("Native SQF cannot have sub instructions");
        }
        public int getTabs()
        {
            return this._parent.getTabs();
        }
    }
}
