﻿using System;
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
            public VarTypeObject outType;
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
                this.outType = new VarTypeObject(type);
                this.hasL = hasL;
                this.hasR = hasR;
            }
            public SupportInfoObject(string command, Ident type, bool hasR = false, bool hasL = false)
            {
                this.SqfCommand = command;
                this.outType = new VarTypeObject(type);
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
            using (var reader = new StringReader(Compiler.Properties.Resources.SQF_SupportInfo))
            {
                string line = "";
                int lineIndex = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    lineIndex++;
                    string[] splitString = line.Split('#');
                    string command = "";
                    if (splitString[0] == "t")
                        continue;
                    if (splitString[0] == "u")
                        command = splitString[2].Split(' ')[0];
                    else if (splitString[0] == "b")
                        command = splitString[2].Split(' ')[1];
                    else
                        command = splitString[2];
                    command = command.ToLower();
                    if (splitString[1].StartsWith("::"))
                    {
                        var anotherSplit = splitString[1].Split(new char[] { ':' });
                        Ident ident = null;
                        foreach (var s in anotherSplit)
                        {
                            if (!string.IsNullOrEmpty(s))
                            {
                                Ident tmpIdent = new Ident(ident, s, -1, -1, "");
                                tmpIdent.Access = Ident.AccessType.Namespace;
                                if(ident != null)
                                {
                                    ident.addChild(tmpIdent);
                                    ident = tmpIdent;
                                }
                                else
                                {
                                    tmpIdent.IsGlobalIdentifier = true;
                                    ident = tmpIdent;
                                }
                            }
                        }
                        ident.Access = Ident.AccessType.NA;
                        supportInfoList.Add(new SupportInfoObject(command, ident.getLastOf<Ident>(), splitString[0] == "u" || splitString[0] == "b", splitString[0] == "b"));
                    }
                    else
                    {
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
                            case "other":
                                supportInfoList.Add(new SupportInfoObject(command, VarType.Other, splitString[0] == "u" || splitString[0] == "b", splitString[0] == "b"));
                                break;
                        }
                    }
                }
            }
        }

        public Ident Name { get { return ((Ident)this.children[0]); } set { this.children[0] = value; } }

        public List<pBaseLangObject> LArgs { get { return this.children.GetRange(1, endMarker); } }
        public List<pBaseLangObject> RArgs { get { return this.children.GetRange(endMarker + 1, this.children.Count - (endMarker + 1)); } }

        private int endMarker;

        public bool HasAs { get; set; }
        public VarTypeObject ReferencedType { get; set; }

        public SqfCall(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
            this.ReferencedType = new VarTypeObject(VarType.Void);
        }

        public override int finalize()
        {
            if (this.IsFinalized)
                return 0;
            int errCount = 0;
            foreach (pBaseLangObject blo in children)
                if (blo != null)
                    errCount += blo.finalize();
            if (this is Interfaces.iTemplate && ((Interfaces.iTemplate)this).TemplateObject != null)
                errCount += ((Interfaces.iTemplate)this).TemplateObject.finalize();
            errCount += this.doFinalize();
            if (this is Interfaces.iHasType && ((Interfaces.iHasType)this).ReferencedType.IsObject)
                errCount += ((Interfaces.iHasType)this).ReferencedType.ident.finalize();
            this.IsFinalized = true;
            return errCount;
        }

        public override int doFinalize()
        {
            int errCount = 0;
            var sio = supportInfoList.Find(SupportInfoObject.bySqfCommand(this.Name.OriginalValue.ToLower()));
            if (sio == null)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0019, this.Name.Line, this.Name.Pos));
                return 1;
            }
            var lArgs = LArgs;
            var rArgs = RArgs;
            if (lArgs.Count == 0 && sio.hasL)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0015, this.Name.Line, this.Name.Pos));
                errCount++;
            }
            else if (lArgs.Count != 0 && !sio.hasL)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0016, this.Name.Line, this.Name.Pos));
                errCount++;
            }
            if (rArgs.Count == 0 && sio.hasR)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0017, this.Name.Line, this.Name.Pos));
                errCount++;
            }
            else if (rArgs.Count != 0 && !sio.hasR)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0018, this.Name.Line, this.Name.Pos));
                errCount++;
            }
            if (!this.HasAs)
            {
                this.ReferencedType.ident = sio.outType.ident;
                this.ReferencedType.varType = sio.outType.varType;
                this.ReferencedType.TemplateObject = sio.outType.TemplateObject;
                if (sio.outType.IsObject)
                {
                    return sio.outType.ident.finalize();
                }
            }
            return 0;
        }
        public void markEnd()
        {
            this.endMarker = this.children.Count - 1;
        }
        public override void writeOut(StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            string tab = this.Parent is Interfaces.iCodeBlock ? new string('\t', this.getAllParentsOf<Interfaces.iCodeBlock>().Count) : "";
            
            var lArgs = this.LArgs;
            var rArgs = this.RArgs;
            sw.Write(tab);
            if (lArgs.Count > 0)
            {
                if (lArgs.Count == 1)
                {
                    sw.Write("(");
                    lArgs[0].writeOut(sw, cfg);
                    sw.Write(") ");
                }
                else
                {
                    sw.Write("[");
                    int index = 0;
                    foreach (var it in lArgs)
                    {
                        if (index > 0)
                            sw.Write(", ");
                        index++;
                        it.writeOut(sw, cfg);
                    }
                    sw.Write("] ");
                }
            }
            sw.Write(this.Name.OriginalValue);
            if (rArgs.Count > 0)
            {
                if (rArgs.Count == 1)
                {
                    sw.Write(" (");
                    rArgs[0].writeOut(sw, cfg);
                    sw.Write(")");
                }
                else
                {
                    sw.Write(" [");
                    int index = 0;
                    foreach (var it in rArgs)
                    {
                        if (index > 0)
                            sw.Write(", ");
                        index++;
                        it.writeOut(sw, cfg);
                    }
                    sw.Write("]");
                }
            }
        }
    }
}

