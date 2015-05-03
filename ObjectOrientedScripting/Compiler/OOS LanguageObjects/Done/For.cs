﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler.OOS_LanguageObjects
{
    class For : IInstruction
    {
        private IInstruction _parent;
        private IInstruction _runAfter;
        private Expression _arg1;
        private Expression _arg2;
        private Expression _arg3;
        public For(IInstruction parent)
        {
            this._parent = parent;
            this._runAfter = null;
            this._arg1 = null;
            this._arg2 = null;
            this._arg3 = null;
        }

        public static For parse(StreamReader reader, IInstruction parent, string currentLine)
        {
            currentLine = currentLine.Remove(0, 3).Trim();
            currentLine = currentLine.Remove(0, 1).Trim();
            For f = new For(parent);
            f._arg1 = Expression.parse(f, currentLine.Substring(0, currentLine.IndexOf(';')));
            currentLine = currentLine.Remove(0, currentLine.IndexOf(';')).Trim();
            string sTmp = currentLine.Substring(currentLine.LastIndexOf(';'));
            sTmp = sTmp.Remove(sTmp.LastIndexOf(')')).Trim();
            currentLine = currentLine.Remove(currentLine.LastIndexOf(';')).Trim();
            f._arg2 = Expression.parse(f, currentLine);
            f._arg3 = Expression.parse(f, sTmp);
            f._runAfter = Scope.parse(f, reader);
            currentLine = currentLine.Remove(0, currentLine.IndexOf(';')).Trim();
            return f;
        }

        /**Prints out given instruction into StreamWriter as SQF. writer object is either a string or a StreamWriter*/
        public void printInstructions(object writer, bool printTabs = true)
        {
            if (!(writer is System.IO.StreamWriter))
                throw new Exception("printInstruction expected a StreamWriter object but received a " + writer.GetType().Name + " object");
            this._arg1.printInstructions(writer, printTabs);
            ((System.IO.StreamWriter)writer).Write("\r\n" + (printTabs ? new string('\t', this.getTabs()) : "") + "while {");
            this._arg2.printInstructions(writer, false);
            ((System.IO.StreamWriter)writer).Write("} do");
            ((System.IO.StreamWriter)writer).Write("\r\n" + (printTabs ? new string('\t', this.getTabs()) : "") + "{");
            this._runAfter.printInstructions(writer, printTabs);
            this._arg3.printInstructions(writer, printTabs);
            ((System.IO.StreamWriter)writer).Write("\r\n" + (printTabs ? new string('\t', this.getTabs()) : "") + "};");
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
            if (recursiveDown)
            {
                result.AddRange(this._runAfter.getInstructions(t, recursiveUp, recursiveDown));
                result.AddRange(this._arg1.getInstructions(t, recursiveUp, recursiveDown));
                result.AddRange(this._arg2.getInstructions(t, recursiveUp, recursiveDown));
                result.AddRange(this._arg3.getInstructions(t, recursiveUp, recursiveDown));
            }
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