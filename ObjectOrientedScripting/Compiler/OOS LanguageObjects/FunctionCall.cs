using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class FunctionCall : pBaseLangObject, Interfaces.iName, Interfaces.iHasType, Interfaces.iHasObject
    {
        public Ident Name { get { return ((Interfaces.iName)Parent).Name; } set { ((Interfaces.iName)Parent).Name = value; } }
        public string FullyQualifiedName { get { return ((Interfaces.iName)Parent).FullyQualifiedName; } }

        public pBaseLangObject ReferencedObject { get { return ((Ident)this.Parent).ReferencedObject; } }
        public VarTypeObject ReferencedType { get { return ((Ident)this.Parent).ReferencedType; } }

        public FunctionCall(pBaseLangObject parent) : base(parent) {}
        public override int doFinalize()
        {
            int errCount = 0;
            Ident ident = (Ident)this.Parent;
            if (ident.ReferencedObject == null)
                return 0;
            Function fnc = (Function)ident.ReferencedObject;
            var argList = fnc.ArgList;
            if(argList.Count != this.children.Count)
            {
                if (argList.Count > this.children.Count)
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0017, ident.Line, ident.Pos));
                    errCount++;
                }
                else
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0018, ident.Line, ident.Pos));
                    errCount++;
                }
            }
            for(var i = 0; i < argList.Count; i++)
            {
                if (i > argList.Count || i > this.children.Count)
                    break;
                Variable v = (Variable)argList[i];
                Expression e = (Expression)this.children[i];
                if(!v.varType.Equals(e.ReferencedType))
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0019, e.Line, e.Pos));
                    errCount++;
                }
            }
            return errCount;
        }
    }
}
