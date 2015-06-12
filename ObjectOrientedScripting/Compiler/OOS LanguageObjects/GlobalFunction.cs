using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler.OOS_LanguageObjects
{
    class GlobalFunction : IFunction
    {
        private Scope _scope;
        private string _functionName;
        private IInstruction _parent;

        private GlobalFunction(IInstruction parent, string functionName)
        {
            this._parent = parent;
            this._scope = null;
            this._functionName = functionName;
        }
        //IFinalizable
        public void finalize() { throw new NotImplementedException(); }
        //IInstruction
        public static GlobalFunction parse(IInstruction parent, StreamReader reader, string currentLine)
        {
            if (!currentLine.StartsWith("static ", StringComparison.OrdinalIgnoreCase))
                throw new Exception("Global function declaration without static keyword is not allowed. currentLine: " + currentLine);
            currentLine = currentLine.Substring("static".Length).Trim();
            if (!currentLine.StartsWith("function ", StringComparison.OrdinalIgnoreCase))
                throw new Exception("Declaration without purpose detected (example: 'static function foobar() {}'). currentLine: " + currentLine);
            currentLine = currentLine.Substring("function".Length).Trim();
            string functionName = currentLine.Substring(0, currentLine.IndexOf('('));
            currentLine = currentLine.Substring(currentLine.IndexOf('('));
            if (!System.Text.RegularExpressions.Regex.IsMatch(functionName, @"^[_a-zA-Z][_a-zA-Z0-9]+$"))
                throw new Exception("Function name '" + functionName + @"' is not allowed (REGEX match failed '^[_a-zA-Z][_a-zA-Z0-9]+$'). currentLine: " + currentLine);
            List<IInstruction> arguments = new List<IInstruction>();
            GlobalFunction globalFunction = new GlobalFunction(parent, functionName);
            while(true)
            {
                string s = currentLine.Substring(0, currentLine.IndexOfAny(new char[] { ',', ')' }));
                currentLine = currentLine.Substring(s.Length);
                if (s.Length == 0)
                    break;
                if (!System.Text.RegularExpressions.Regex.IsMatch(functionName, @"^[_a-zA-Z][_a-zA-Z0-9]+$"))
                    throw new Exception("Variable name '" + s + @"' is not allowed (REGEX match failed '^[_a-zA-Z][_a-zA-Z0-9]+$'). currentLine: " + currentLine);
                arguments.Add(LocalVariable.parse(globalFunction, "var _" + s + ";"));
            }
            Scope scope = Scope.parse(globalFunction, reader, arguments);
            globalFunction._scope = scope;
            return globalFunction;
        }
        public void printInstructions(object writer, bool printTabs = true) { throw new NotImplementedException(); }
        public string parseInput(string input) { throw new NotImplementedException(); }
        public IInstruction getParent() { throw new NotImplementedException(); }
        public IInstruction[] getInstructions(Type t, bool recursiveUp = true, bool recursiveDown = false) { throw new NotImplementedException(); }
        public IInstruction getFirstOf(Type t) { throw new NotImplementedException(); }
        public void addInstruction(IInstruction instr) { throw new NotImplementedException(); }
        public int getTabs() { throw new NotImplementedException(); }
        //IFunction
        public string getName() { throw new NotImplementedException(); }
    }
}
