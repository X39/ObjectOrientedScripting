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
    public enum OverridableOperator
    {
        ArrayAccess,
        LessThenLessThen,
        GreaterThenGreaterThen,
        ExplicitEquals
    }
    public enum AssignmentCharacters
    {
        SimpleAssign,           // <ident> = <value>
        PlusOne,                // <ident> ++
        MinusOne,               // <ident> --
        AdditionAssign,         // <ident> += <value>
        SubstractionAssign,     // <ident> -= <value>
        MultiplicationAssign,   // <ident> *= <value>
        DivisionAssign          // <ident> /= <value>
    }
    public class VarTypeObject : Interfaces.iTemplate
    {
        private Template template;
        public Template TemplateObject
        {
            get { return this.template; }
            set { if (value != null) this.template = value; }
        }
        public bool IsObject { get { return this.varType == VarType.Object || this.varType == VarType.ObjectStrict; } }
        public VarTypeObject(Ident i, bool isStrict = false, Template template = null)
        {
            this.ident = i;
            this.varType = isStrict ? VarType.ObjectStrict : VarType.Object;
            this.template = template;

            if(this.template == null && (this.ident.ReferencedObject is Interfaces.iTemplate))
            {
                this.template = ((Interfaces.iTemplate)this.ident.ReferencedObject).TemplateObject;
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
        public VarTypeObject(VarTypeObject vto)
        {
            this.ident = vto.ident;
            this.varType = vto.varType;
            template = vto.template;
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
        public override int GetHashCode() { return base.GetHashCode(); }
    }
}
