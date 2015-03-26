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
            //Itterate through EVERY character (as we cant simply use the normal string.replace function we have to do it by ourself ...)
            for(int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                //check if our current character is a letter or number or _
                if (!char.IsLetterOrDigit(c) && c != '_')
                {
                    //reset current word as we did not had the current define here and encountered a word terminator
                    output += word + c;
                    word = "";
                    continue;
                }
                //add current character to current word
                word += c;
                //check if current word matches our define
                if(word.Equals(_name, StringComparison.Ordinal))
                {
                    //it matched our define
                    //check if our current define has arguments and directly replace the word with current content if it has no
                    if(_arguments.Length == 0)
                    {
                        word = _value;
                        output += word;
                        word = "";
                        continue;
                    }
                    //we do have arguments so lets continue

                    i++;
                    //make sure our word has arguments attached and throw an exception if not
                    if (input[i] != '(')
                        throw new Exception("encountered unexpected character while preprocessing, expected '(' but got '" + c + "'");
                    //some variables to set before the for loop
                    string curValue = _value;
                    int curArg = 0;
                    word = "";
                    int counter = 1;
                    char ignoreStringChar = '\0';
                    bool ignoreString = false;
                    int backSlashCounter = 0;

                    //Lets go and parse everything we can find that could be a part of our define
                    for (i++; i < input.Length; i++)
                    {
                        c = input[i];
                        //check if we have an argument seperator here
                        if (c == ',' && counter == 1 && !ignoreString)
                        {
                            //throw an expection if we have too many arguments for this define
                            if (curArg >= _arguments.Length)
                                throw new Exception("encountered unexpected extra argument in define while preprocessing, allowed count is " + _arguments.Length);
                            curValue = curValue.Replace(_arguments[curArg], word);
                            word = "";
                            curArg++;
                            continue;
                        }
                        //check for the end of arguments here
                        if (c == ')' && counter == 1 && !ignoreString)
                        {
                            //throw an expection if we have too many arguments for this define
                            if (curArg >= _arguments.Length)
                                throw new Exception("encountered unexpected extra argument in define while preprocessing, allowed count is " + _arguments.Length);
                            curValue = curValue.Replace(_arguments[curArg], word);
                            word = "";
                            counter--;
                            break;
                        }
                        //add current character to current argument word
                        word += c;
                        if (ignoreString)
                        {
                            //We are currently inside a string so lets ignore every possible character that could annoy us
                            if (c == ignoreStringChar && backSlashCounter % 2 == 0)
                            {
                                ignoreString = false;
                                ignoreStringChar = '\0';
                                continue;
                            }
                            //Add +1 to the backSlashCounter so we can make sure that we wont accidently exit the string too early and reset it if we dont have a backslash here
                            if (c == '\\')
                                backSlashCounter++;
                            else
                                backSlashCounter = 0;
                        }
                        else
                        {
                            //Check for string mode
                            if(c == '\'' || c == '"')
                            {
                                ignoreString = true;
                                ignoreStringChar = c;
                                continue;
                            }
                            //Check if we have another capsulation here, if we do add +1 to the counter
                            if (c == '(' || c == '{')
                            {
                                counter++;
                                continue;
                            }
                            //End of an capsulation, if it is remove -1 from the counter
                            if (c == ')' || c == '}')
                            {
                                counter--;
                                continue;
                            }
                        }
                    }
                    //we exited the define with an invalid number of capsulations ... seems like something moved wrong here!
                    if (counter != 0)
                        throw new Exception("Missing defines arguemnts end character in current line");
                    output += curValue;
                }
            }
            output += word;
            return output;
        }
    }
}
