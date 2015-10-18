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
            List<pBaseLangObject> argList;
            if (ident.ReferencedObject is Function)
            {
                Function fnc = (Function)ident.ReferencedObject;
                argList = fnc.ArgList;
            }
            else if(ident.ReferencedObject is NativeInstruction)
            {
                NativeInstruction ni = (NativeInstruction)ident.ReferencedObject;
                argList = ni.children;
            }
            else
            {
                throw new Exception("NOPE! Should not happen exception happened ... please report to dev...");
            }
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
            var template = ident.ReferencedObject.getFirstOf<Interfaces.iTemplate>();
            for(var i = 0; i < argList.Count; i++)
            {
                if (i > argList.Count || i > this.children.Count)
                    break;
                Variable v = (Variable)argList[i];
                Expression e = (Expression)this.children[i];
                if ((v.varType.varType == VarType.Object || v.varType.varType == VarType.ObjectStrict) && template != null)
                {
                    bool flag = false;
                    for (int j = 0; j < template.template.vtoList.Count; j++ )
                    {
                        var it = template.template.vtoList[j];
                        if (v.varType.ident.OriginalValue.Equals(it.ident.OriginalValue))
                        {
                            var varIdent = this.getLastOf<Ident>();
                            var template2 = ((Interfaces.iTemplate)varIdent.ReferencedObject).template;
                            if (!(varIdent.ReferencedObject is Interfaces.iTemplate))
                            {
                                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.UNKNOWN, e.Line, e.Pos));
                                errCount++;
                            }
                            else if (template2 != null && template2.vtoList.Count > j)
                            {
                                var it2 = template2.vtoList[j];
                                if (!e.ReferencedType.Equals(it2))
                                {
                                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0049, e.Line, e.Pos));
                                    errCount++;
                                }
                                else
                                {
                                    flag = true;
                                }
                            }
                        }
                    }
                    if(!flag)
                    {
                        if (!v.varType.Equals(e.ReferencedType))
                        {
                            Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0019, e.Line, e.Pos));
                            errCount++;
                        }
                    }
                }
                else
                {
                    if (!v.varType.Equals(e.ReferencedType))
                    {
                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.ErrorCodeEnum.C0019, e.Line, e.Pos));
                        errCount++;
                    }
                }

            }
            return errCount;
        }
    }
}
