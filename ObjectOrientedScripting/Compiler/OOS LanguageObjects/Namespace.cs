using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler.OOS_LanguageObjects
{
    public class Namespace : IInstruction
    {
        private string _namespaceName;
        private List<IInstruction> _instructions;
        public Namespace parentNamespace;
        public string NamespaceName
        {
            get { return (this.parentNamespace == null ? "" : this.parentNamespace.NamespaceName + "_") + this._namespaceName; }
            set { this._namespaceName = value; }
        }

        public Namespace(string name, Namespace parent)
        {
            this._namespaceName = name;
            this.parentNamespace = parent;
            this._instructions = new List<IInstruction>();
        }


        public void printInstructions(StreamWriter writer, bool printTabs = true)
        {
            foreach (IInstruction instr in this._instructions)
                instr.printInstructions(writer);
        }
        string parseInput(string input)
        {
            return input;
        }
        public int getNamespaceCount() { return (this.parentNamespace == null ? 0 : this.parentNamespace.getNamespaceCount()) + 1; }
        public IInstruction getParent() { return this.parentNamespace; }
    }
}
