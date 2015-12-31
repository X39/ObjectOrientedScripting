using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Cast : pBaseLangObject, Interfaces.iHasType, Interfaces.iHasObject
    {
        public VarTypeObject varType;
        public Cast(pBaseLangObject parent) : base(parent) { }
        public override int doFinalize()
        {
            int errCount = 0;
            if (this.varType.ident != null)
                errCount += this.varType.ident.doFinalize();
            var childType = ((Ident)this.children[0]).LastIdent.ReferencedType;
            if (this.varType.varType == VarType.Object && childType.varType != VarType.Object)
            {//Non-Object to object
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0031, ((Ident)this.children[0]).Line, ((Ident)this.children[0]).Pos));
                errCount++;
            }
            if (this.varType.varType != VarType.Object && childType.varType == VarType.Object)
            {//Object to non-object
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0033, ((Ident)this.children[0]).Line, ((Ident)this.children[0]).Pos));
                errCount++;
            }
            if (this.varType.Equals(childType))
            {//Same type
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0034, ((Ident)this.children[0]).Line, ((Ident)this.children[0]).Pos));
                errCount++;
            }
            return errCount;
        }
        public pBaseLangObject ReferencedObject { get { return ((Ident)this.children[0]).ReferencedObject; } }
        public VarTypeObject ReferencedType { get { return varType; } }
        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            //ToDo: do the actual casting stuff
            string tab = this.Parent is Interfaces.iCodeBlock ? new string('\t', this.getAllParentsOf<Interfaces.iCodeBlock>().Count) : "";
            if (this.children.Count != 1 || !(this.children[0] is Ident))
                throw new Exception();
            Ident ident = (Ident)this.children[0];
            switch (ident.LastIdent.ReferencedType.varType)
            {
                case VarType.Object:
                    ident.writeOut(sw, cfg);
                    break;
                case VarType.Bool:
                    switch(this.ReferencedType.varType)
                    {
                        case VarType.Scalar:
                            sw.Write("parseNumber ");
                            ident.writeOut(sw, cfg);
                            break;
                        default:
                            ident.writeOut(sw, cfg);
                            break;
                    }
                    break;
                case VarType.Scalar:
                    switch (this.ReferencedType.varType)
                    {
                        case VarType.Bool:
                            sw.Write("if(");
                            ident.writeOut(sw, cfg);
                            sw.Write(" > 0) then {true} else {false}");
                            break;
                        default:
                            ident.writeOut(sw, cfg);
                            break;
                    }
                    break;
                default:
                    throw new Exception();
            }
        }
    }
}
