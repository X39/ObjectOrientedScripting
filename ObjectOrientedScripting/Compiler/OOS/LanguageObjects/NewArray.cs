using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class NewArray : pBaseLangObject, Interfaces.iHasType
    {

        public NewArray(pBaseLangObject parent) : base(parent)
        {
            this.ReferencedType = null;
        }
        public VarTypeObject ReferencedType { get; internal set; }
        public override int finalize()
        {
            if (this.IsFinalized)
                return 0;
            int errCount = 0;
            foreach (pBaseLangObject blo in children)
                if (blo != null)
                    errCount += blo.finalize();
            errCount += this.doFinalize();
            if (this is Interfaces.iTemplate && ((Interfaces.iTemplate)this).TemplateObject != null)
                errCount += ((Interfaces.iTemplate)this).TemplateObject.doFinalize();
            if (this is Interfaces.iHasType && ((Interfaces.iHasType)this).ReferencedType.IsObject)
                errCount += ((Interfaces.iHasType)this).ReferencedType.ident.finalize();
            this.IsFinalized = true;
            return errCount;
        }
        public override int doFinalize()
        {
            int errCount = 0;
            foreach(var it in this.children)
            {
                if(it is Expression)
                {
                    Expression exp = (Expression)it;
                    if (this.ReferencedType == null)
                        this.ReferencedType = exp.ReferencedType;
                    if (!this.ReferencedType.Equals(exp.ReferencedType))
                    {
                        Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0013, exp.Line, exp.Pos));
                        errCount++;
                    }
                }
                else
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.UNKNOWN));
                    errCount++;
                }
            }
            if (this.ReferencedType != null)
            {
                this.ReferencedType = new VarTypeObject(this.ReferencedType);
                switch(this.ReferencedType.varType)
                {
                    case VarType.Bool:
                        this.ReferencedType.varType = VarType.BoolArray;
                        break;
                    case VarType.Scalar:
                        this.ReferencedType.varType = VarType.ScalarArray;
                        break;
                    case VarType.String:
                        this.ReferencedType.varType = VarType.StringArray;
                        break;
                }
            }
            return errCount;
        }
    }
}
