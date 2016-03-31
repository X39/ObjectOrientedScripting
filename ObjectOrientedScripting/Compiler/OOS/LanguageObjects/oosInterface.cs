using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class oosInterface : pBaseLangObject, Interfaces.iName,  Interfaces.iClass, Interfaces.iHasId, Interfaces.iGetIndex
    {
        //Is set in Compiler.cs before finalize is callen
        public static Variable GlobalInterfaceRegisterVariable;


        public Ident Name { get { return ((Ident)this.children[0]); } set { this.children[0] = value; } }
        public List<Ident> ExtendedClasses { get { return new List<Ident>(); } }
        public VarTypeObject VTO { get; set; }
        public int ID { get; set; }

        public oosInterface(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
        }
        public override int doFinalize() { return 0; }
        public override string ToString()
        {
            return "interface->" + this.Name.FullyQualifiedName;
        }
        public Interfaces.iOperatorFunction getOperatorFunction(OverridableOperator op)
        {
            return null;
        }

        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg) { }

        public int getIndex(Ident ident)
        {
            int index = 0;
            var refObj = ident.ReferencedObject;
            if (refObj is VirtualFunction)
            {
                var obj = (Interfaces.iName)refObj;
                for (int i = 0; i < this.children.Count; i++)
                {
                    var it = this.children[i];
                    if (it is Interfaces.iName && ((Interfaces.iName)it).Name.OriginalValue == obj.Name.OriginalValue)
                    {
                        if (it is VirtualFunction && obj is VirtualFunction && HelperClasses.ArgList.matchesArglist(((VirtualFunction)it).ArgList, ((VirtualFunction)obj).ArgList))
                        {
                            return index;
                        }
                    }
                    if (it is VirtualFunction && ((VirtualFunction)it).IsVirtual)
                        index++;
                }
            }
            throw new Exception();
        }
    }
}
