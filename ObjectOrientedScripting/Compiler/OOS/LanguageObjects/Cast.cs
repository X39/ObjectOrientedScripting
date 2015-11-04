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
        public bool isStaticCast;
        public override int doFinalize()
        {
            int errCount = 0;
            //if (this.varType.ident != null)
            //    errCount += this.varType.ident.doFinalize();
            //var childType = ((Ident)this.children[0]).ReferencedType;
            //if (this.varType.varType == VarType.Object && childType.varType != VarType.ObjectStrict && childType.varType != VarType.Object)
            //{//Non-Object to object
            //    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0015, ((Ident)this.children[0]).Line, ((Ident)this.children[0]).Pos));
            //    errCount++;
            //}
            //if (this.isStaticCast && this.varType.varType == VarType.Object && childType.varType != VarType.ObjectStrict)
            //{//Non-Object to object
            //    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0025, ((Ident)this.children[0]).Line, ((Ident)this.children[0]).Pos));
            //    errCount++;
            //}
            //if ((this.varType.varType != VarType.Object && this.varType.varType != VarType.String) && childType.varType == VarType.Object)
            //{//Object to scalar/boolean
            //    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0016, ((Ident)this.children[0]).Line, ((Ident)this.children[0]).Pos));
            //    errCount++;
            //}
            //if (this.varType.Equals(childType))
            //{//Same type
            //    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0031, ((Ident)this.children[0]).Line, ((Ident)this.children[0]).Pos));
            //    errCount++;
            //}
            //if (this.isStaticCast && this.varType.varType == VarType.ObjectStrict && this.varType.ident.ReferencedObject is oosInterface)
            //{//Same type
            //    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0031, ((Ident)this.children[0]).Line, ((Ident)this.children[0]).Pos));
            //    errCount++;
            //}
            //if (!this.isStaticCast && (this.varType.varType == VarType.Bool || this.varType.varType == VarType.BoolArray || this.varType.varType == VarType.Scalar || this.varType.varType == VarType.ScalarArray || this.varType.varType == VarType.String || this.varType.varType == VarType.StringArray))
            //{//Same type
            //    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0048, ((Ident)this.children[0]).Line, ((Ident)this.children[0]).Pos));
            //    errCount++;
            //}
            return errCount;
        }
        public pBaseLangObject ReferencedObject { get { return ((Ident)this.children[0]).ReferencedObject; } }
        public VarTypeObject ReferencedType { get { return varType; } }
        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            throw new NotImplementedException();
        }
    }
}
