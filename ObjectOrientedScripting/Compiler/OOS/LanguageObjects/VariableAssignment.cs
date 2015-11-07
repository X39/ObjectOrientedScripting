using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class VariableAssignment : pBaseLangObject, Interfaces.iHasType
    {
        public AssignmentCharacters Operation { get; set; }
        public VarTypeObject ReferencedType
        {
            get
            {
                if (this.children.Count == 0)
                    return null;
                if (this.children[0] is Interfaces.iHasType)
                    return ((Interfaces.iHasType)this.children[0]).ReferencedType;
                else
                    return null;
            }
        }

        public VariableAssignment(pBaseLangObject parent) : base(parent) {}
        public override int doFinalize() { return 0; }

        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            if (this.Parent is Ident || this.Parent is AssignContainer)
            {
                Ident parent = this.Parent is AssignContainer ? ((AssignContainer)this.Parent).Name.NextWorkerIdent : (Ident)this.Parent;
                string varName = '(' + parent.WriteOutValue + ')';
                switch (this.Operation)
                {
                    case AssignmentCharacters.PlusOne:
                        sw.Write(varName + " + 1");
                        break;
                    case AssignmentCharacters.MinusOne:
                        sw.Write(varName + " - 1");
                        break;
                    case AssignmentCharacters.AdditionAssign:
                        sw.Write(varName + " + (");
                        foreach (var it in this.children)
                        {
                            it.writeOut(sw, cfg);
                        }
                        sw.Write(varName + ")");
                        break;
                    case AssignmentCharacters.SubstractionAssign:
                        sw.Write(varName + " - (");
                        foreach (var it in this.children)
                        {
                            it.writeOut(sw, cfg);
                        }
                        sw.Write(varName + ")");
                        break;
                    case AssignmentCharacters.MultiplicationAssign:
                        sw.Write(varName + " * (");
                        foreach (var it in this.children)
                        {
                            it.writeOut(sw, cfg);
                        }
                        sw.Write(varName + ")");
                        break;
                    case AssignmentCharacters.DivisionAssign:
                        sw.Write(varName + " / (");
                        foreach (var it in this.children)
                        {
                            it.writeOut(sw, cfg);
                        }
                        sw.Write(varName + ")");
                        break;
                    default:
                        foreach (var it in this.children)
                        {
                            it.writeOut(sw, cfg);
                        }
                        break;
                }
            }
            else if (this.Parent is Variable)
            {
                foreach(var it in this.children)
                {
                    it.writeOut(sw, cfg);
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
