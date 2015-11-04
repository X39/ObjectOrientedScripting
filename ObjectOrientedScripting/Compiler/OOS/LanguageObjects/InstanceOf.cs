using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class InstanceOf : pBaseLangObject, Interfaces.iHasType
    {
        public pBaseLangObject LIdent { get { return (pBaseLangObject)this.children[0]; } set { this.children[0] = value; } }
        public Ident RIdent { get { return (Ident)this.children[1]; } set { this.children[1] = value; } }
        public InstanceOf(pBaseLangObject parent) : base(parent)
        {
            this.addChild(null);
            this.addChild(null);
        }
        public VarTypeObject ReferencedType { get { return new VarTypeObject(VarType.Bool); } }
        public override int doFinalize()
        {
            int errCount = 0;
            //if(LIdent is Cast)
            //{
            //    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0028, ((Ident)((Cast)LIdent).children[0]).Line, ((Ident)((Cast)LIdent).children[0]).Pos));
            //    errCount++;
            //}
            //else if (LIdent is Ident)
            //{
            //    var varType = ((Ident)LIdent).ReferencedType;
            //    if (varType.varType != VarType.Object && varType.varType != VarType.ObjectStrict)
            //    {
            //        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0026, ((Ident)LIdent).Line, ((Ident)LIdent).Pos));
            //        errCount++;
            //    }
            //}
            //else
            //{
            //    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.UNKNOWN, RIdent.Line, RIdent.Pos));
            //    errCount++;
            //}
            //var refObject = RIdent.ReferencedObject;
            //if (!(refObject is oosClass || refObject is oosInterface))
            //{
            //    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0027, RIdent.Line, RIdent.Pos));
            //    errCount++;
            //}
            return errCount;
        }

        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            throw new NotImplementedException();
        }
    }
}
