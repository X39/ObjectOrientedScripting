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
        private List<IInstruction> _instructions;
        public Namespace currentNamespace;
        public IInstruction parentInstruction;

        public Scope(IInstruction parent, Namespace name)
        {
            this._instructions = new List<IInstruction>();
            this.parentInstruction = parent;
        }


        public void addInstruction(IInstruction instr)
        {
            if (instr is Namespace)
                throw new Exception("Namespaces are not allowed to be inside of Scopes");
            this._instructions.Add(instr);
        }
        public void printInstructions(StreamWriter writer, bool printTabs = true)
        {
            foreach (IInstruction instr in this._instructions)
            {
                writer.Write(new string('\t', this.getTabCount()));
                instr.printInstructions(writer, false);
                writer.WriteLine(';');
            }
        }

        public int getTabCount() { return this.currentNamespace.getNamespaceCount(); }
        public string getGlobalPrefix() { return this.currentNamespace.NamespaceName; }
        public IInstruction getParent() { return this.parentInstruction; }
    }
}
