using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects.HelperClasses
{
    class NamespaceResolver
    {
        public int LayerCount { get { throw new NotImplementedException(); } }
        public pBaseLangObject Reference { get { throw new NotImplementedException(); } }
        public static Base BaseClass { get; set; }
        public static implicit operator NamespaceResolver(string input)
        {
            return new NamespaceResolver(input);
        }
        public static implicit operator NamespaceResolver(Ident input)
        {
            return new NamespaceResolver(input.FullyQualifiedName);
        }
        public NamespaceResolver(string s)
        {
            throw new NotImplementedException();
        }
        public NamespaceResolver(string s, string extend)
        {
            throw new NotImplementedException();
        }

        public bool isInNamespace(NamespaceResolver nsr)
        {
            throw new NotImplementedException();
        }



        public static List<Interfaces.iFunction> getFunctionReferenceOfFQN(HelperClasses.NamespaceResolver nsr)
        {
            throw new NotImplementedException();
        }
        public static Interfaces.iClass getClassReferenceOfFQN(HelperClasses.NamespaceResolver nsr)
        {
            throw new NotImplementedException();
        }
        public static Variable getVariableReferenceOfFQN(HelperClasses.NamespaceResolver nsr, bool localVariables = true, pBaseLangObject blo = null)
        {
            throw new NotImplementedException();
        }
    }
}
