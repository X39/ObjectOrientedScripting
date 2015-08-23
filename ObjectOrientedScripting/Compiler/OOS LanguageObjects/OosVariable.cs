using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class OosVariable : BaseLangObject, Interfaces.iName
    {
        string name;
        public string Name { get { return this.name; } set { this.name = value; } }
        public bool HasObjectAccess
        {
            get
            {
                return this.name.Contains('.');
            }
        }
        public bool HasNamespace
        {
            get
            {
                return this.name.Contains("::");
            }
        }
        public bool HasThisKeyword
        {
            get
            {
                return this.name.StartsWith("this.");
            }
        }

        public bool IsLocal
        {
            get
            {
                return Name.StartsWith("_");
            }
        }

        public string FunctionName
        {
            get
            {
                return this.name.Remove(0, this.name.IndexOf('.') + 1);
            }
        }

        public string NamespaceName
        {
            get
            {
                var index = this.name.IndexOf('.');
                string s;
                if (index == -1)
                    s = this.name;
                else
                    s = this.name.Remove(index);
                return s;
            }
        }
        public string NormalizedNamespaceName
        {
            get
            {
                string s = NamespaceName;
                int index = s.LastIndexOf("::");
                if (index != -1)
                {
                    if (this.Parent is OosObjectCreation)
                        s = s.Replace("::", "_") + "_fnc_";
                    else
                        s = (s.Substring(0, index) + "_fnc" + s.Substring(index)).Replace("::", "_");
                }
                if (s.StartsWith("_"))
                    s = s.Remove(0, 1);
                
                return "oos_" + s;
            }
        }
        public OosVariable(string s)
        {
            this.name = s;
        }
    }
}
