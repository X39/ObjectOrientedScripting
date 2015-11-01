using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class NewInstance : pBaseLangObject, Interfaces.iName, Interfaces.iHasType, Interfaces.iHasObject, Interfaces.iTemplate
    {

        public Ident Name { get { return ((Ident)this.children[0]); } set { this.children[0] = value; } }
        public string FullyQualifiedName { get { return ((Ident)this.children[0]).LastIdent.FullyQualifiedName; } }
        public VarTypeObject ReferencedType { get; internal set; }
        public pBaseLangObject ReferencedObject { get { return ((Ident)this.children[0]).LastIdent.ReferencedObject; } }
        public Template TemplateObject { get; set; }
        public NewInstance(pBaseLangObject parent) : base(parent)
        {
            this.addChild(null);
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
            ReferencedType = new VarTypeObject(((Ident)this.children[0]).LastIdent.ReferencedType);
            if(this.TemplateObject != null)
            {
                this.ReferencedType.TemplateObject = this.TemplateObject;
            }
            return 0;
        }
    }
}
