using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Expression : pBaseLangObject, Interfaces.iHasType
    {
        public VarTypeObject ReferencedType { get; internal set; }
        public bool negate;
        public bool hasBrackets;


        public int Line { get; internal set; }
        public int Pos { get; internal set; }
        public string File { get; internal set; }

        public List<string> expressionOperators;
        public List<pBaseLangObject> expressionObjects { get { return this.children; } }

        public Expression(pBaseLangObject parent, bool negate, bool hasBrackets, int line, int pos, string file) : base(parent)
        {
            this.negate = negate;
            this.hasBrackets = hasBrackets;
            this.Line = line;
            this.Pos = pos;
            this.File = file;
            this.ReferencedType = null;
            this.expressionOperators = new List<string>();
        }
        public override int doFinalize()
        {
            int errCount = 0;
            this.ReferencedType = new VarTypeObject(VarType.Void);
            VarTypeObject vto = this.ReferencedType;
            if(this.expressionObjects.Count == 1)
            {
                var obj = this.expressionObjects[0];
                if(obj is Interfaces.iHasType)
                {
                    if (obj is Ident)
                    {
                        vto.copyFrom(((Ident)obj).LastIdent.ReferencedType);
                    }
                    else
                    {
                        vto.copyFrom(((Interfaces.iHasType)obj).ReferencedType);
                    }
                }
                else if(obj is Value)
                {
                    vto.copyFrom(((Value)obj).varType);
                }
                else
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.UNKNOWN, this.Line, this.Pos, this.File));
                    errCount++;
                }
            }
            else
            {
                int i = 0;
                foreach(var op in this.expressionOperators)
                {
                    var lArg = this.expressionObjects[i];
                    var rArg = this.expressionObjects[++i];
                    VarTypeObject lType;
                    VarTypeObject rType;
                    if (lArg is Interfaces.iHasType)
                    {
                        lType = ((Interfaces.iHasType)lArg).ReferencedType;
                        if(lArg is Ident)
                        {
                            lType = ((Ident)lArg).LastIdent.ReferencedType;
                        }
                    }
                    else if (lArg is Value)
                    {
                        lType = ((Value)lArg).varType;
                    }
                    else
                    {
                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.UNKNOWN, this.Line, this.Pos, this.File));
                        errCount++;
                        continue;
                    }
                    if (rArg is Interfaces.iHasType)
                    {
                        rType = ((Interfaces.iHasType)rArg).ReferencedType;
                        if (rArg is Ident)
                        {
                            rType = ((Ident)rArg).LastIdent.ReferencedType;
                        }
                    }
                    else if (rArg is Value)
                    {
                        rType = ((Value)rArg).varType;
                    }
                    else
                    {
                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.UNKNOWN, this.Line, this.Pos, this.File));
                        errCount++;
                        continue;
                    }

                    switch (op)
                    {
                        case "&": case "&&": case "|": case "||":
                            if (lType.varType != VarType.Bool || rType.varType != VarType.Bool)
                            {
                                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0014, this.Line, this.Pos, this.File));
                                errCount++;
                                break;
                            }
                            vto.copyFrom(new VarTypeObject(VarType.Bool));
                            break;
                        case "==":
                            vto.copyFrom(new VarTypeObject(VarType.Bool));
                            break;
                        case "+": case "-": case "*": case "/":
                            if (lType.varType != VarType.Scalar || rType.varType != VarType.Scalar)
                            {
                                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0014, this.Line, this.Pos, this.File));
                                errCount++;
                                break;
                            }
                            vto.copyFrom(new VarTypeObject(VarType.Scalar));
                            break;
                        case ">": case ">=": case "<":  case "<=":
                            if (lType.varType != VarType.Scalar || rType.varType != VarType.Scalar)
                            {
                                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0014, this.Line, this.Pos, this.File));
                                errCount++;
                                break;
                            }
                            vto.copyFrom(new VarTypeObject(VarType.Bool));
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }
            if(vto.IsObject)
            {
                errCount += vto.ident.finalize();
            }
            return errCount;
        }
        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            if (this.negate)
                sw.Write('!');
            if (this.negate || this.hasBrackets || this.expressionOperators.Count > 0)
                sw.Write('(');
            int i = 0;
            this.expressionObjects[0].writeOut(sw, cfg);
            foreach (var op in this.expressionOperators)
            {
                var lArg = this.expressionObjects[i];
                var rArg = this.expressionObjects[++i];
                if (op == "==")
                {
                    sw.Write(' ' + "isEqualTo" + ' ');
                    rArg.writeOut(sw, cfg);
                }
                else if (op == "&&" || op == "||")
                {
                    sw.Write(' ' + op + " {");
                    rArg.writeOut(sw, cfg);
                    sw.Write('}');
                }
                else
                {
                    sw.Write(' ' + op + ' ');
                    rArg.writeOut(sw, cfg);
                }
            }
            if (this.negate || this.hasBrackets || this.expressionOperators.Count > 0)
                sw.Write(')');
        }
    }
}
