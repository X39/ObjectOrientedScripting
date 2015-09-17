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
        NA   //NA
    }
    public enum VarType
    {
        Scalar,
        Bool,
        String,
        Auto,
        Void,
        Object,
        Other
    }
    public class VarTypeObject
    {
        public VarTypeObject(Ident i)
        {
            this.ident = i;
            this.varType = VarType.Object;
        } 
        public VarTypeObject(VarType v)
        {
            this.ident = null;
            this.varType = v;
        }
        public Ident ident;
        public VarType varType;
        public override bool Equals(object obj)
        {
            if (!(obj is VarTypeObject))
                return false;
            if (((VarTypeObject)obj).varType != this.varType)
                return false;
            return ((VarTypeObject)obj).ident.FullyQualifiedName.Equals(this.ident.FullyQualifiedName);
        }
    }
    public enum IdentType
    {
        Name,
        GlobalAccess,
        RelativeAccess,
        GlobalAccess_Instance,
        RelativeAccess_Instance,
        NA
    }
}
