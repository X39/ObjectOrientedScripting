using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects.Ex
{
    public class TypeMissmatch : Exception
    {
        private VarTypeObject lType;
        private VarTypeObject rType;
        private int line;
        private int pos;
        private string on;
        public VarTypeObject LType { get { return this.lType; } }
        public VarTypeObject RType { get { return this.rType; } }
        public int Line { get { return this.line; } }
        public int Pos { get { return this.pos; } }
        public string On { get { return this.on; } }
        public TypeMissmatch(VarTypeObject t1, VarTypeObject t2, int line = -1, int pos = -1, string on = "")
            : base("Type Missmatch" + (on == "" ? on : " on '" + on + "'") + ", LType = " + (t1.varType != VarType.Object ? Enum.GetName(typeof(VarType), t1.varType) : t1.ident.FullyQualifiedName) + " RType = " + (t2.varType != VarType.Object ? Enum.GetName(typeof(VarType), t2.varType) : t2.ident.FullyQualifiedName))
        {
            this.lType = t1;
            this.rType = t2;
            this.line = line;
            this.pos = pos;
            this.on = on;
        }
    }
}
