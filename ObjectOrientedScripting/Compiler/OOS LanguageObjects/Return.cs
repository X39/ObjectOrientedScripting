﻿using System;
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
        private Return(IInstruction parent, IInstruction value)
        {
            this._parent = parent;
            this._value = value;
        }
        public static Return parse(IInstruction parent, string toParse)
        {
            return null;
        }
        /**Prints out given instruction into StreamWriter as SQF. writer object is either a string or a StreamWriter*/
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
                throw new Exception("Cannot move up AND down at the same time");
            if (t.IsInstanceOfType(this))
                result.Add(this);
            if (recursiveUp)
                this._parent.getInstructions(t, recursiveUp, recursiveDown);
            //if (recursiveDown)
            //    result.AddRange(new IInstruction[] { });
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
            throw new Exception("An Identifier cannot have sub instructions");
        }
        /**returns current tab ammount*/
        public int getTabs()
        {
            return this._parent.getTabs();
        }
    }
}