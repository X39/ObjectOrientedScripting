using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class LocalVariable : Variable
    {
        private IInstruction _parent;
        private IInstruction _instr;
        public static LocalVariable parse(IInstruction parent, string input)
        {
            if (!input.StartsWith("var ", StringComparison.OrdinalIgnoreCase))
                throw new Exception("A LocalVariable needs to start with the 'var' term");
            string ident = "_" + input.Substring("var ".Length);
            LocalVariable localVar = new LocalVariable();
            localVar._parent = parent;
            if (ident.Contains('='))
                localVar._instr = Assignment.parse(localVar, ident);
            else if (ident.Contains(';'))
                localVar._instr = Identifier.parse(localVar, ident.Remove(ident.Length - 1));
            else
                throw new Exception("Unknown LocalVariable syntax. Expected 'var <identifier> [= <value>];' Got: " + input);
            return localVar;
        }
        public Identifier getVariableIdentifier()
        {
            if (this._instr is Assignment)
                return ((Assignment)this._instr).LeftHand;
            else
                return (Identifier)this._instr;
        }
        public string getLocalScopeIdentifier()
        {
            if (this._instr is Assignment)
                if (((Assignment)this._instr).LeftHand is Identifier)
                    return ((Identifier)((Assignment)this._instr).LeftHand).Name;
                else
                    throw new Exception("Assignment leftHand is not as expected Identifier, thus this LocalVariable is invalid");
            else
                return ((Identifier)this._instr).Name;
        }
        /**Prints out given instruction into StreamWriter as SQF. writer object is either a string or a StreamWriter*/
        public void printInstructions(object writer, bool printTabs = true)
        {
            if (this._instr is Assignment)
                this._instr.printInstructions(writer, printTabs);
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
                result.AddRange(this._instr.getInstructions(t, recursiveUp, recursiveDown));
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
            throw new Exception("A LocalVariable cannot have sub instructions");
        }
        /**returns current tab ammount*/
        public int getTabs()
        {
            return this._parent.getTabs();
        }
    }
}
