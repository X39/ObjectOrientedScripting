using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler.OOS_LanguageObjects
{
    class Class : IInstruction
    {
        /// <summary>
        /// Returns the constructor name in SQF
        /// Example:
        /// oos_fnc_class_oos_namespace1_foobar____constructor___
        /// </summary>
        /// <returns>String containing the constructor function name</returns>
        public string getConstructorFunctionName() { throw new NotImplementedException(); }
        /// <summary>
        /// Returns the fully qualified name as string
        /// Example:
        /// OOS::NAMESPACE1::FOOBAR
        /// </summary>
        /// <returns>String containing the FQN</returns>
        public string getFullyQualifiedName() { throw new NotImplementedException(); }
        //IFinalizable
        public void finalize() { throw new NotImplementedException(); }
        //IInstruction
        public static Class parse(StreamReader reader, IInstruction parent, string currentLine) { throw new NotImplementedException(); }
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
