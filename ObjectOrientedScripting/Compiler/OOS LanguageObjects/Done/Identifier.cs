using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Identifier : IInstruction
    {
        private IInstruction _parent;
        private string _name;
        public string Name { get { return this._name; } }
        public readonly string[] reservedIdentifiers = 
        {
            "native",
            "if",
            "else",
            "for",
            "foreach",
            "do",
            "switch",
            "case",
            "default",
            "return",
            "namespace",
            "class",
            "function",
            "var",
            "static",
            "public",
            "private",
            "protected",
            "try",
            "catch",
            "throw"
        };
        private Identifier(IInstruction parent, string value)
        {
            this._parent = parent;
            value = this.parseInput(value);
            if (reservedIdentifiers.Contains(value, StringComparer.OrdinalIgnoreCase))
                throw new Exception("'" + value + "' is a reserved term and thus not allowed for identifiers");
            this._name = this.parseInput(value);
        }
        //IFinalizable
        public void finalize()
        {
            this._name = this._parent.parseInput(this._name);
        }
        //IInstruction
        public static Identifier parse(IInstruction parent, string toParse)
        {
            string name = toParse.Trim();
            if (!System.Text.RegularExpressions.Regex.IsMatch(name, @"^[_a-zA-Z][_a-zA-Z0-9]+$"))
                throw new Exception("Identifier '" + name + "' contains not allowed characters for identifierts, allowed REGEX: ^[_a-zA-Z][_a-zA-Z0-9]+$");
            //maybe ToDo: Validate that representing variable is existing (requires Variable items to be implemented)
            return new Identifier(parent, name);
        }
        public void printInstructions(object writer, bool printTabs = true)
        {
            if (!(writer is System.IO.StreamWriter))
                throw new Exception("printInstruction expected a StreamWriter object but received a " + writer.GetType().Name + " object");
            ((System.IO.StreamWriter)writer).Write((printTabs ? new string('\t', this.getTabs()) : "") + this.parseInput(this._name));
        }
        public string parseInput(string input)
        {
            return this.getParent().parseInput(input);
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
            throw new Exception("An Identifier cannot have sub instructions");
        }
        public int getTabs()
        {
            return this._parent.getTabs();
        }
    }
}
