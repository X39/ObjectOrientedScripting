using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class oosEnum : pBaseLangObject, Interfaces.iName, Interfaces.iHasType
    {
        public class EnumEntry : pBaseLangObject, Interfaces.iName
        {
            public EnumEntry(pBaseLangObject parent) : base(parent)
            {
                this.addChild(null);
                this.addChild(null);
            }
            public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
            {
            }
            public Ident Name { get { return ((Ident)this.children[0]); } set { this.children[0] = value; } }
            public Value Value { get { return ((Value)this.children[1]); } set { this.children[1] = value; } }
            public override string ToString()
            {
                return "enumEntry->" + this.Name.FullyQualifiedName + "<->" + (this.Value == null ? "NULL" : this.Value.ToString());
            }
        }
        public Ident Name { get { return ((Ident)this.children[0]); } set { this.children[0] = value; } }
        public oosEnum(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
            this.ReferencedType = new VarTypeObject(VarType.Void);
        }
        public override int doFinalize()
        {
            int errCount = 0;
            var list = this.getAllChildrenOf<EnumEntry>();
            VarTypeObject vto = null;
            int offset = 0;
            List<string> usedValues = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                var it = list[i];
                if(it.Value == null)
                {
                    Value val = new Value(it);
                    val.value = (i + offset).ToString();
                    it.Value = val;
                    it.Value.varType.varType = VarType.Scalar;
                }
                else
                {
                    if(it.Value.varType.varType == VarType.Scalar)
                    {
                        offset = int.Parse(it.Value.value) - i;
                    }
                }
                if (usedValues.Contains(it.Value.value))
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0048, it.Name.Line, it.Name.Pos, it.Name.File));
                    errCount++;
                }
                usedValues.Add(it.Value.value);
                if(vto == null)
                {
                    vto = it.Value.varType;
                }
                else if(!vto.Equals(it.Value.varType))
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0047, it.Name.Line, it.Name.Pos, it.Name.File));
                    errCount++;
                }
            }
            this.ReferencedType.ident = vto.ident;
            this.ReferencedType.varType = vto.varType;
            this.ReferencedType.TemplateObject = vto.TemplateObject;


            if (list.Count == 1)
            {
                Logger.Instance.log(Logger.LogLevel.WARNING, "Enum '" + this.Name.OriginalValue + "' only has a single value assigned to it");
            }

            return errCount;
        }
        public override string ToString()
        {
            return "enum->" + this.Name.FullyQualifiedName;
        }
        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {

        }

        public VarTypeObject ReferencedType { get; internal set; }
        public VarTypeObject RealType { get; internal set; }
    }
}
