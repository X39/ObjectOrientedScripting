using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class FunctionCall : pBaseLangObject, Interfaces.iName, Interfaces.iHasType, Interfaces.iHasObject, Interfaces.iArgList
    {
        public Ident Name { get { return ((Interfaces.iName)Parent).Name; } set { ((Interfaces.iName)Parent).Name = value; } }
        public string FullyQualifiedName { get { return ((Interfaces.iName)Parent).Name.FullyQualifiedName; } }

        public pBaseLangObject ReferencedObject { get { return ((Ident)this.Parent).ReferencedObject; } }
        public VarTypeObject ReferencedType { get { return ((Ident)this.Parent).ReferencedType; } }

        /// <summary>
        /// Returns the Arglist required for this iFunction
        /// </summary>
        public List<VarTypeObject> ArgList
        {
            get
            {
                List<VarTypeObject> retList = new List<VarTypeObject>();
                foreach (var it in this.children)
                {
                    if (it is Interfaces.iHasType)
                    {
                        retList.Add(((Interfaces.iHasType)it).ReferencedType);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                return retList;
            }
        }

        public FunctionCall(pBaseLangObject parent) : base(parent) {}
        public override int doFinalize()
        {
            int errCount = 0;
            return errCount;
        }
        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            throw new NotImplementedException();
        }
    }
}
