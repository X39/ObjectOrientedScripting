using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace Compiler.OOS_LanguageObjects
{
    public class SqfCall : pBaseLangObject, Interfaces.iName, Interfaces.iHasType
    {
        private class SupportInfoObject
        {
            public string SqfCommand;
            public bool hasL;
            public bool hasR;
            public VarType outType;
            public enum type
            {
                b,
                u,
                n,
                t
            }
            public SupportInfoObject(string command, VarType type, bool hasR = false, bool hasL = false)
            {
                this.SqfCommand = command;
                this.outType = type;
                this.hasL = hasL;
                this.hasR = hasR;
            }

            public static Predicate<SupportInfoObject> bySqfCommand(string s)
            {
                return delegate(SupportInfoObject sio)
                {
                    return sio.SqfCommand == s;
                };
            }

        }
        private static List<SupportInfoObject> supportInfoList;
        public static void readSupportInfoList()
        {
            if (supportInfoList != null)
                throw new Exception("SupportInfoList cannot be read twice!");
            supportInfoList = new List<SupportInfoObject>();
            var assembly = Assembly.GetExecutingAssembly();
            //var resourceName = ;
            using (var reader = new StringReader(Compiler.Properties.Resources.SQF_SupportInfo))
            {
                string line = "";
                int lineIndex = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    lineIndex++;
                    string[] splitString = line.Split(':');
                    string command = "";
                    if (splitString[0] == "t")
                        continue;
                    if (splitString[0] == "u")
                        command = splitString[2].Split(' ')[0];
                    else if (splitString[0] == "b")
                        command = splitString[2].Split(' ')[1];
                    else
                        command = splitString[2];
                    switch (splitString[1].ToLower())
                    {
                        default:
                            throw new Exception("Unknown returnType encountered while parsing SupportInfo.txt, Line: " + lineIndex);
                        case "nil":
                            supportInfoList.Add(new SupportInfoObject(command, VarType.Void, splitString[0] == "u" || splitString[0] == "b", splitString[0] == "b"));
                            break;
                        case "bool":
                            supportInfoList.Add(new SupportInfoObject(command, VarType.Bool, splitString[0] == "u" || splitString[0] == "b", splitString[0] == "b"));
                            break;
                        case "bool[]":
                            supportInfoList.Add(new SupportInfoObject(command, VarType.BoolArray, splitString[0] == "u" || splitString[0] == "b", splitString[0] == "b"));
                            break;
                        case "scalar":
                            supportInfoList.Add(new SupportInfoObject(command, VarType.Scalar, splitString[0] == "u" || splitString[0] == "b", splitString[0] == "b"));
                            break;
                        case "scalar[]":
                            supportInfoList.Add(new SupportInfoObject(command, VarType.ScalarArray, splitString[0] == "u" || splitString[0] == "b", splitString[0] == "b"));
                            break;
                        case "string":
                            supportInfoList.Add(new SupportInfoObject(command, VarType.String, splitString[0] == "u" || splitString[0] == "b", splitString[0] == "b"));
                            break;
                        case "string[]":
                            supportInfoList.Add(new SupportInfoObject(command, VarType.StringArray, splitString[0] == "u" || splitString[0] == "b", splitString[0] == "b"));
                            break;
                        case "other":
                            supportInfoList.Add(new SupportInfoObject(command, VarType.Other, splitString[0] == "u" || splitString[0] == "b", splitString[0] == "b"));
                            break;
                    }
                }
            }
        }

        public Ident Name { get { return ((Ident)this.children[0]); } set { if (!value.IsSimpleIdentifier) throw new Ex.InvalidIdentType(value.getIdentType(), IdentType.Name); this.children[0] = value; } }
        public string FullyQualifiedName { get { return this.Name.OriginalValue; } }

        public List<pBaseLangObject> LArgs { get { return this.children.GetRange(1, endMarker); } }
        public List<pBaseLangObject> RArgs { get { return this.children.GetRange(endMarker + 1, this.children.Count - (endMarker + 1)); } }

        private int endMarker;

        private VarTypeObject referencedType;
        public VarTypeObject ReferencedType { get { return referencedType; } }

        public SqfCall(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
        }
        public override int doFinalize()
        {
            int errCount = 0;
            var sio = supportInfoList.Find(SupportInfoObject.bySqfCommand(this.Name.OriginalValue.ToLower()));
            if(sio == null)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0024, this.Name.Line, this.Name.Pos));
                return 1;
            }
            var lArgs = LArgs;
            var rArgs = RArgs;
            if (lArgs.Count == 0 && sio.hasL)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0020, this.Name.Line, this.Name.Pos));
                errCount++;
            }
            else if (lArgs.Count != 0 && !sio.hasL)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0021, this.Name.Line, this.Name.Pos));
                errCount++;
            }
            if (rArgs.Count == 0 && sio.hasR)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0022, this.Name.Line, this.Name.Pos));
                errCount++;
            }
            else if (rArgs.Count != 0 && !sio.hasR)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0023, this.Name.Line, this.Name.Pos));
                errCount++;
            }
            this.referencedType = new VarTypeObject(sio.outType);
            return 0;
        }
        public void markEnd()
        {
            this.endMarker = this.children.Count - 1;
        }
        public static void readSupportInfoList(string pathToFile)
        {
            supportInfoList = new List<SupportInfoObject>();
        }
    }
}
