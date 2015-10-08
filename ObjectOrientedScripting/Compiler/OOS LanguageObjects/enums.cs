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
    public class VarTypeObject
    {
        public VarTypeObject(Ident i, bool isStrict = false)
        {
            this.ident = i;
            this.varType = isStrict ? VarType.ObjectStrict : VarType.Object;
        } 
        public VarTypeObject(VarType v)
        {
            this.ident = null;
            this.varType = v;
            if (v == VarType.ObjectStrict || v == VarType.Object)
                throw new Exception("TODO: Allow anonymous objects");
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
    public enum IdentType
    {
        Name,
        GlobalAccess,
        RelativeAccess,
        NA
    }
}
