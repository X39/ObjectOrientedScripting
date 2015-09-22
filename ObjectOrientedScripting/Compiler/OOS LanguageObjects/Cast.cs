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
            var childType = ((Ident)this.children[0]).ReferencedType;
            if (this.varType.varType == VarType.Object && childType.varType != VarType.Object)
            {//Non-Object to object
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0015, ((Ident)this.children[0]).Line, ((Ident)this.children[0]).Pos));
                return 1;
            }
            if ((this.varType.varType != VarType.Object && this.varType.varType != VarType.String) && childType.varType == VarType.Object)
            {//Object to scalar/boolean
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0016, ((Ident)this.children[0]).Line, ((Ident)this.children[0]).Pos));
                return 1;
            }
            return 0;
        }
        public pBaseLangObject ReferencedObject { get { return ((Ident)this.children[0]).ReferencedObject; } }
        public VarTypeObject ReferencedType { get { return varType; } }
    }
}
