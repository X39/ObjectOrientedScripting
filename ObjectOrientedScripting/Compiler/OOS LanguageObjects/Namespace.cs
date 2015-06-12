using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler.OOS_LanguageObjects
{
    class Namespace : IInstruction
    {
        private Namespace _parent;
        private List<IInstruction> _childs;
        private string _name;
        public string Name { get { return (this._parent.Name == null ? "" : this._parent.Name + "_") + this._name; } }
        private Namespace(Namespace parent, string name)
        {
            this._parent = parent;
            this._name = name;
            this._childs = new List<IInstruction>();
        }
        //IFinalizable
        public void finalize() { throw new NotImplementedException(); }
        //IInstruction
        public static Namespace parse(StreamReader reader, IInstruction parent, string currentLine) { throw new NotImplementedException(); }
        public void printInstructions(object writer, bool printTabs = true) { throw new NotImplementedException(); }
        public string parseInput(string input) { throw new NotImplementedException(); }
        public void setParent(IInstruction parent) { throw new NotImplementedException(); }
        public IInstruction getParent() { throw new NotImplementedException(); }
        public IInstruction[] getInstructions(Type t, bool recursiveUp = true, bool recursiveDown = false) { throw new NotImplementedException(); }
        public IInstruction getFirstOf(Type t) { throw new NotImplementedException(); }
        public void addInstruction(IInstruction instr) { throw new NotImplementedException(); }
        public int getTabs() { throw new NotImplementedException(); }
    }
}
