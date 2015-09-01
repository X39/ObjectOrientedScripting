using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class SqfCall : pBaseLangObject, Interfaces.iName
    {
        public Ident Name { get { return ((Ident)this.children[0]); } set { if (!value.IsSimpleIdentifier) throw new Ex.InvalidIdentType(value.getIdentType(), IdentType.Name); this.children[0] = value; } }
        public string FullyQualifiedName { get { return this.Name.OriginalValue; } }

        public readonly List<pBaseLangObject> LArgs { get { return this.children.GetRange(0, endMarker); } }
        public readonly List<pBaseLangObject> RArgs { get { return this.children.GetRange(endMarker, this.children.Count - endMarker); } }

        private int endMarker;


        public SqfCall(pBaseLangObject parent) : base(parent)
        {
            this.children.Add(null);
        }
        public override void doFinalize() { }
        public void markEnd()
        {
            this.endMarker = this.children.Count;
        }
    }
}
