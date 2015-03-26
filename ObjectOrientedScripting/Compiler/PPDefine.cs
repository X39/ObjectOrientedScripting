using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class PPDefine
    {
        private string _name;
        private string[] _arguments;
        private string _value;

        public PPDefine(string s)
        {
            int spaceIndex = s.IndexOf(' ');
            if(spaceIndex == -1)
            {//No Space found thus we just have a define here without anything else
                _name = s;
                _arguments = new string[0];
                _value = "";
                return;
            }
            int bracketsIndex = s.IndexOf('(');
            if (spaceIndex < bracketsIndex || bracketsIndex == -1)
            {//first bracket was found after first space OR is not existing thus we have a simple define with a replace value here
                _name = s.Remove(spaceIndex);
                _arguments = new string[0];
                _value = s.Remove(0, spaceIndex + 1);
                return;
            }
            //we got a define with arguments here
            string argumentsString = s.Remove(0, bracketsIndex + 1);
            argumentsString = argumentsString.Remove(argumentsString.IndexOf(')'));
            _arguments = argumentsString.Split(',');
            for (int i = 0; i < _arguments.Length; i++)
            {
                _arguments[i] = _arguments[i].Trim();
            }
            _name = s.Remove(bracketsIndex);
            bracketsIndex = s.IndexOf(") ");
            if (bracketsIndex == -1)
                throw new Exception("Missing character to close argument list ')' or no value for argument define");
            _value = s.Remove(0, bracketsIndex + 2);
        }
        public PPDefine(string name, string argumentString, string value)
        {
            _name = name;
            _arguments = argumentString.Trim(new char[] { '(', ')' }).Split(',');
            _value = value;
            for (int i = 0; i < _arguments.Length; i++)
            {
                _arguments[i] = _arguments[i].Trim();
            }
        }

        public string replace(string input)
        {
            string output = "";
            string word = "";
            for(int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (!char.IsLetterOrDigit(c))
                {
                    output += word + c;
                    word = "";
                    continue;
                }
                word += c;
                if(word.Equals(_name, StringComparison.Ordinal))
                {
                    if(_arguments.Length == 0)
                    {
                        word = _value;
                        output += word;
                        word = "";
                        continue;
                    }
                    i++;
                    if (input[i] != '(')
                        throw new Exception("encountered unexpected character while preprocessing, expected '(' but got '" + c + "'");
                    string curValue = _value;
                    int curArg = 0;
                    word = "";
                    int counter = 1;
                    char ignoreStringChar = '\0';
                    bool ignoreString = false;
                    int backSlashCounter = 0;
                    for (i++; i < input.Length; i++)
                    {
                        c = input[i];
                        if (c == ',' && counter == 1 && !ignoreString)
                        {
                            if (curArg >= _arguments.Length)
                                throw new Exception("encountered unexpected extra argument in define while preprocessing, allowed count is " + _arguments.Length);
                            curValue = curValue.Replace(_arguments[curArg], word);
                            word = "";
                            curArg++;
                            continue;
                        }
                        if (c == ')' && counter == 1 && !ignoreString)
                        {
                            if (curArg >= _arguments.Length)
                                throw new Exception("encountered unexpected extra argument in define while preprocessing, allowed count is " + _arguments.Length);
                            curValue = curValue.Replace(_arguments[curArg], word);
                            word = "";
                            counter--;
                            break;
                        }
                        word += c;
                        if (ignoreString)
                        {
                            if (c == ignoreStringChar && backSlashCounter % 2 == 0)
                            {
                                ignoreString = false;
                                ignoreStringChar = '\0';
                                continue;
                            }
                            if (c == '\\')
                                backSlashCounter++;
                        }
                        else
                        {
                            if(c == '\'' || c == '"')
                            {
                                ignoreString = true;
                                ignoreStringChar = c;
                                continue;
                            }
                            if (c == '(' || c == '{')
                            {
                                counter++;
                                continue;
                            }
                            if (c == ')' || c == '}')
                            {
                                counter--;
                                continue;
                            }
                        }
                    }
                    if (counter != 0)
                        throw new Exception("Missing defines arguemnts end character");
                    output += curValue;
                }
            }
            output += word;
            return output;
        }
    }
}
