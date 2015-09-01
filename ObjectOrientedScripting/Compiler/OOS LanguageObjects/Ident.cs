using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Ident : pBaseLangObject, Interfaces.iName
    {
        public Ident Name { get { return this; } set { throw new Exception("Cannot set Ident of an Ident"); } }
        public string FullyQualifiedName { get { return this.Name.OriginalValue; } }
        private string originalValue;
        public string OriginalValue { get { return this.originalValue; } }
        public bool IsSimpleIdentifier { get { return !(this.originalValue.Contains("::") || this.originalValue.Contains('.')); } }
        public bool IsGlobalIdentifier { get { return this.originalValue.StartsWith("::"); } }
        public bool IsRelativeIdentifier { get { return this.originalValue.Contains("::"); } }
        public bool HasInstanceAccess { get { return this.originalValue.Contains('.'); } }
        

        public Ident(pBaseLangObject parent, string origVal) : base(parent) 
        {
            this.originalValue = origVal;
        }
        public override void doFinalize() { }
        public IdentType getIdentType()
        {
            if (IsSimpleIdentifier)
                return IdentType.Name;
            if (IsGlobalIdentifier)
                if (HasInstanceAccess)
                    return IdentType.GlobalAccess_Instance;
                else
                    return IdentType.GlobalAccess;
            if (IsRelativeIdentifier)
                if (HasInstanceAccess)
                    return IdentType.RelativeAccess_Instance;
                else
                    return IdentType.RelativeAccess;
            throw new Exception("Unknown error, please report to the developer");
        }
    }
}
