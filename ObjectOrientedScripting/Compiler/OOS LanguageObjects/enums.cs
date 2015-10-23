using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public enum Encapsulation
    {
        Private,
        Protected,
        Public,
        Static,
        NA
    }
    public enum VarType
    {
        Scalar,
        Bool,
        String,
        Auto,
        Void,
        Object,
        ObjectStrict,
        Other,
        ScalarArray,
        BoolArray,
        StringArray
    }
    public class VarTypeObject : Interfaces.iTemplate
    {
        Template te;
        public Template template
        {
            get { return this.te; }
            set { if(value != null) this.te = value; }
        }
        public bool IsObject { get { return this.varType == VarType.Object || this.varType == VarType.ObjectStrict; } }
        public VarTypeObject(Ident i, bool isStrict = false, Template template = null)
        {
            this.ident = i;
            this.varType = isStrict ? VarType.ObjectStrict : VarType.Object;
            this.template = template;

            if(this.template == null && (this.ident.ThisReferencedObject is Interfaces.iTemplate))
            {
                this.template = ((Interfaces.iTemplate)this.ident.ReferencedObject).template;
            }
        }
        public VarTypeObject(VarType v)
        {
            this.ident = null;
            this.varType = v;
            if (v == VarType.ObjectStrict || v == VarType.Object)
                throw new Exception("TODO: Allow anonymous objects");
            template = null;
            //TODO: Allow anonymous objects
        }
        public Ident ident;
        public VarType varType;
        public override bool Equals(object obj)
        {
            if (!(obj is VarTypeObject))
                return false;
            if (((VarTypeObject)obj).varType != this.varType)
                return false;
            if (this.varType != VarType.Object && this.varType != VarType.ObjectStrict)
                return true;
            return ((VarTypeObject)obj).ident.FullyQualifiedName.Equals(this.ident.FullyQualifiedName);
        }
    }
}
