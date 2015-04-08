using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler.OOS_LanguageObjects
{
    public class Scope : IInstruction
    {
        private IInstruction _parent;
        private List<IInstruction> _childs;
        private bool _hasReturn;
        private Scope(IInstruction parent)
        {
            this._parent = parent;
            this._childs = new List<IInstruction>();
            this._hasReturn = false;
        }
        public static Scope parse(IInstruction parent, StreamReader toParse)
        {
            string codeLine = "";
            int ci;
            Scope scope = new Scope(parent);
            while(true)
            {
                ci = toParse.Read();
                if (ci < 0)
                    break;
                char c = (char)ci;
                if (char.IsControl(c))
                    continue;
                codeLine += c;
                if (c == '}') //End of Scope
                    break;
                //some optimization ... should improve parsing a little
                if (codeLine.Length == "native".Length && codeLine.StartsWith("native", StringComparison.OrdinalIgnoreCase))
                {
                    if (c == '\n')
                    {
                        codeLine.Trim();
                        scope.addInstruction(Native.parse(scope, codeLine));
                        codeLine = "";
                    }
                }
                if(c == ';' || c == '{')
                {//End of code line
                    codeLine.Trim();
                    if (codeLine.StartsWith("var", StringComparison.OrdinalIgnoreCase))
                    {
                        scope.addInstruction(LocalVariable.parse(scope, codeLine));
                    }
                    else if (codeLine.StartsWith("foreach", StringComparison.OrdinalIgnoreCase))
                    {
                        scope.addInstruction(ForEach.parse(toParse, scope, codeLine));
                    }
                    else if (codeLine.StartsWith("for", StringComparison.OrdinalIgnoreCase))
                    {
                        scope.addInstruction(For.parse(toParse, scope, codeLine));
                    }
                    else if (codeLine.StartsWith("do", StringComparison.OrdinalIgnoreCase) || codeLine.StartsWith("while", StringComparison.OrdinalIgnoreCase))
                    {
                        scope.addInstruction(While.parse(toParse, scope, codeLine));
                    }
                    else if (codeLine.StartsWith("switch", StringComparison.OrdinalIgnoreCase))
                    {
                        scope.addInstruction(Switch.parse(toParse, scope, codeLine));
                    }
                    else if (codeLine.StartsWith("if", StringComparison.OrdinalIgnoreCase))
                    {
                        scope.addInstruction(IfThen.parse(toParse, scope, codeLine));
                    }
                    else if (codeLine.StartsWith("return", StringComparison.OrdinalIgnoreCase))
                    {
                        scope.addInstruction(Return.parse(scope, codeLine));
                    }
                    else
                    {
                        //ToDo: current implementation is lazy ... there should be a better way to do so. Imrpove current routine
                        int equalityIndex = codeLine.IndexOf("=");
                        if (equalityIndex != -1 && codeLine[equalityIndex + 1] != '=')
                            scope.addInstruction(Assignment.parse(scope, codeLine));
                        else
                            scope.addInstruction(Expression.parse(scope, codeLine));
                    }
                    codeLine = "";
                }
            }
            return scope;
        }
        public string getScopeName()
        {
            Namespace n = (Namespace)this.getFirstOf(typeof(Namespace));
            if (n == null)
                throw new Exception("Could not resolve ScopeName as there is no parent namespace, compiler bug");
            if(this._parent is Function)
            {
                return n.Name + ((Function)this._parent).Name;
            }
            else
            {
                Function f = (Function)this.getFirstOf(typeof(Function));
                if (f == null)
                    throw new Exception("Could not resolve ScopeName as scope is not bound to a function, compiler bug");
                IInstruction instr = this;
                int counter = 0;
                while(f != instr)
                {
                    instr = instr.getParent();
                    counter++;
                }
                return n.Name + f.Name + new string('_', counter);
            }
        }
        /**Prints out given instruction into StreamWriter as SQF. writer object is either a string or a StreamWriter*/
        public void printInstructions(object writer, bool printTabs = true)
        {
            if (!(writer is System.IO.StreamWriter))
                throw new Exception("printInstruction expected a StreamWriter object but received a " + writer.GetType().Name + " object");
            if (this._hasReturn)
            {
                ((System.IO.StreamWriter)writer).Write((printTabs ? new string('\t', this.getTabs() - 2) : "") + "{\r\n");
                ((System.IO.StreamWriter)writer).Write((printTabs ? new string('\t', this.getTabs() - 1) : "") + "private[\"_return" + this.getScopeName() + "\"];\r\n");
                ((System.IO.StreamWriter)writer).Write((printTabs ? new string('\t', this.getTabs() - 1) : "") + "scopeName \"" + this.getScopeName() + "\";\r\n");
            }
            ((System.IO.StreamWriter)writer).Write((printTabs ? new string('\t', this.getTabs() - 1) : "") + "{\r\n");
            IInstruction[] localVariables = this.getInstructions(typeof(LocalVariable), false, false);
            string privateArray = "private[";
            foreach (LocalVariable instr in localVariables)
                privateArray += '"' + instr.getLocalScopeIdentifier() + "\", ";
            privateArray.Remove(privateArray.Length - 2);
            privateArray += "];\r\n";
            ((System.IO.StreamWriter)writer).Write((printTabs ? new string('\t', this.getTabs()) : "") + privateArray);
            foreach (IInstruction instr in this._childs)
                printInstructions(writer, printTabs);
            ((System.IO.StreamWriter)writer).Write((printTabs ? new string('\t', this.getTabs() - 1) : "") + "}\r\n");
            if (this._hasReturn)
            {
                ((System.IO.StreamWriter)writer).Write((printTabs ? new string('\t', this.getTabs() - 1) : "") + "_return" + this.getScopeName() + "\r\n");
                ((System.IO.StreamWriter)writer).Write((printTabs ? new string('\t', this.getTabs() - 2) : "") + "}\r\n");
            }
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
                foreach(IInstruction instr in this._childs)
                    result.AddRange(instr.getInstructions(t, recursiveUp, recursiveDown));
            else
                foreach (IInstruction instr in this._childs)
                    if (t.IsInstanceOfType(instr))
                        result.Add(this);
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
            if(!(instr is FunctionCall || instr is Assignment || instr is For || instr is ForEach || instr is IfThen || instr is While || instr is Return))
                throw new Exception("IInstruction of the type " + instr.GetType().Name + " is not allowed for a Scope");
            if (instr is Return)
            {
                if (this._parent is Function)
                {
                    this._hasReturn = true;
                }
                else
                {
                    Scope scope = (Scope)getFirstOf(typeof(Scope));
                    if (scope == null)
                        throw new Exception("Cannot find FunctionScope for return instruction");
                    scope._hasReturn = true;
                }
            }
            this._childs.Add(instr);
        }
        /**returns current tab ammount*/
        public int getTabs()
        {
            return this._parent.getTabs() + (this._hasReturn ? 2 : 1);
        }
    }
}
