using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class VirtualFunction : pBaseLangObject, Interfaces.iName, Interfaces.iFunction
    {
        public Ident Name { get { return ((Ident)this.children[0]); } set { this.children[0] = value; } }
        public VarTypeObject varType;
        public List<VarTypeObject> argTypes;

        /// <summary>
        /// Return type of this iFunction
        /// </summary>
        public VarTypeObject ReturnType { get { return this.varType; } }
        public VarTypeObject ReferencedType { get { return this.ReturnType; } }
        /// <summary>
        /// Returns a Template object which then can deref some unknown class conflicts in
        /// ArgList field
        /// </summary>
        public Template TemplateArguments { get { return null; } }
        /// <summary>
        /// Returns functions encapsulation
        /// </summary>
        public Encapsulation FunctionEncapsulation { get { return Encapsulation.Public; } }
        /// <summary>
        /// Returns the Arglist required for this iFunction
        /// </summary>
        public List<VarTypeObject> ArgList { get { return this.argTypes; } }
        public bool IsAsync { get; set; }
        public VirtualFunction(pBaseLangObject parent) : base(parent) 
        {
            argTypes = new List<VarTypeObject>();
            this.children.Add(null);
            varType = null;
        }
        public override int doFinalize() { return 0; }
        public override string ToString()
        {
            return "vFnc->" + this.Name.FullyQualifiedName;
        }

        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg) { }
        public List<Return> ReturnCommands { get { return new List<Return>(); } }
        public bool AlwaysReturns { get { return true; } }
    }
}
