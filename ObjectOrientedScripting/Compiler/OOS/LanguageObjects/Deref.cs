using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Deref : pBaseLangObject, Interfaces.iHasType
    {

        public Deref(pBaseLangObject parent, int line, int pos, string file) : base(parent)
        {
            Ident ident = new Ident(null, "string", line, pos, file);
            this.ReferencedType = new VarTypeObject(ident);
            this.Line = line;
            this.Pos = pos;
            this.File = file;
        }

        public int Line { get; internal set; }
        public int Pos { get; internal set; }
        public string File { get; internal set; }
        public VarTypeObject ReferencedType { get; internal set; }

        public override int doFinalize()
        {
            int errCount = 0;
            foreach (var it in this.children)
            {
                if(it is Ident)
                {
                    Ident ident = (Ident)it;
                    var refObj = ident.LastIdent.ReferencedObject;
                    if(refObj is Function)
                    {
                        Function fnc = (Function)refObj;
                        if(fnc.IsVirtual)
                        {
                            Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0053, this.Line, this.Pos, this.File));
                            errCount++;
                        }
                    }
                    else if(refObj is Variable)
                    {
                        Variable obj = (Variable)refObj;
                        if(obj.encapsulation != Encapsulation.Static)
                        {
                            Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0054, this.Line, this.Pos, this.File));
                            errCount++;
                        }
                    }
                    else
                    {
                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0052, this.Line, this.Pos, this.File));
                        errCount++;
                    }
                }
                else
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.UNKNOWN, this.Line, this.Pos, this.File));
                    errCount++;
                }
            }
            return errCount;
        }

        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            sw.Write('"');
            foreach(var it in this.children)
            {
                it.writeOut(sw, cfg);
            }
            sw.Write('"');
        }
    }
}
