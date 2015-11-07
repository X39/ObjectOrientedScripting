using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Cast : pBaseLangObject, Interfaces.iHasType, Interfaces.iHasObject
    {
        public VarTypeObject varType;
        public Cast(pBaseLangObject parent) : base(parent) { }
        public bool isStaticCast;
        public override int doFinalize()
        {
            int errCount = 0;
            if (this.varType.ident != null)
                errCount += this.varType.ident.doFinalize();
            var childType = ((Ident)this.children[0]).ReferencedType;
            if (this.varType.varType == VarType.Object && childType.varType != VarType.ObjectStrict && childType.varType != VarType.Object)
            {//Non-Object to object
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0031, ((Ident)this.children[0]).Line, ((Ident)this.children[0]).Pos));
                errCount++;
            }
            if (this.isStaticCast && this.varType.varType == VarType.Object && childType.varType != VarType.ObjectStrict)
            {//Non-Object to object
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0032, ((Ident)this.children[0]).Line, ((Ident)this.children[0]).Pos));
                errCount++;
            }
            if ((this.varType.varType != VarType.Object && this.varType.varType != VarType.String) && childType.varType == VarType.Object)
            {//Object to scalar/boolean
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0033, ((Ident)this.children[0]).Line, ((Ident)this.children[0]).Pos));
                errCount++;
            }
            if (this.varType.Equals(childType))
            {//Same type
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0034, ((Ident)this.children[0]).Line, ((Ident)this.children[0]).Pos));
                errCount++;
            }
            if (this.isStaticCast && this.varType.varType == VarType.ObjectStrict && this.varType.ident.ReferencedObject is oosInterface)
            {//Same type
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0035, ((Ident)this.children[0]).Line, ((Ident)this.children[0]).Pos));
                errCount++;
            }
            if (!this.isStaticCast && (this.varType.varType == VarType.Bool || this.varType.varType == VarType.BoolArray || this.varType.varType == VarType.Scalar || this.varType.varType == VarType.ScalarArray || this.varType.varType == VarType.String || this.varType.varType == VarType.StringArray))
            {//Same type
                Logger.Instance.log(Logger.LogLevel.ERROR, ErrorStringResolver.resolve(ErrorStringResolver.LinkerErrorCode.LNK0036, ((Ident)this.children[0]).Line, ((Ident)this.children[0]).Pos));
                errCount++;
            }
            return errCount;
        }
        public pBaseLangObject ReferencedObject { get { return ((Ident)this.children[0]).ReferencedObject; } }
        public VarTypeObject ReferencedType { get { return varType; } }
        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            //ToDo: do the actual casting stuff
            string tab = this.Parent is Interfaces.iCodeBlock ? new string('\t', this.getAllParentsOf<Interfaces.iCodeBlock>().Count) : "";
            if (this.isStaticCast)
            {
                if (this.children.Count != 1 || !(this.children[0] is Ident))
                    throw new Exception();
                Ident ident = (Ident)this.children[0];
                switch (ident.ReferencedType.varType)
                {
                    case VarType.ObjectStrict:
                        {
                            sw.Write(tab + "[] call {");
                            sw.Write("(");
                            ident.writeOut(sw, cfg);
                            sw.Write(") set [2, ");
                            var result = HelperClasses.NamespaceResolver.getClassReferenceOfFQN(ident.ReferencedType.ident.LastIdent);
                            if (result != null)
                            {
                                var index = result is oosClass ? ((oosClass)result).getClassIndex(this.varType.ident) : -1;
                                if (index == -1)
                                    throw new Exception();
                                ident.writeOut(sw, cfg);
                                sw.Write(" select 1 select " + index + "]; ");
                                ident.writeOut(sw, cfg);
                            }
                            else
                            {
                                throw new Exception();
                            }
                            sw.Write("}");
                        } break;
                    case VarType.Bool:
                        switch(this.ReferencedType.varType)
                        {
                            case VarType.Scalar:
                                sw.Write("parseNumber ");
                                ident.writeOut(sw, cfg);
                                break;
                            case VarType.String:
                                sw.Write("str ");
                                ident.writeOut(sw, cfg);
                                break;
                            default:
                                ident.writeOut(sw, cfg);
                                break;
                        }
                        break;
                    case VarType.Scalar:
                        switch (this.ReferencedType.varType)
                        {
                            case VarType.Bool:
                                sw.Write("if(");
                                ident.writeOut(sw, cfg);
                                sw.Write(" > 0) then {true} else {false}");
                                break;
                            case VarType.String:
                                sw.Write("str ");
                                ident.writeOut(sw, cfg);
                                break;
                            default:
                                ident.writeOut(sw, cfg);
                                break;
                        }
                        break;
                    case VarType.String:
                        switch (this.ReferencedType.varType)
                        {
                            case VarType.Bool:
                                sw.Write("if(");
                                ident.writeOut(sw, cfg);
                                sw.Write(" == \"true\") then {true} else {false}");
                                break;
                            case VarType.Scalar:
                                sw.Write("parseNumber ");
                                ident.writeOut(sw, cfg);
                                break;
                            default:
                                ident.writeOut(sw, cfg);
                                break;
                        }
                        break;
                    default:
                        throw new Exception();
                }
            }
            else
            {
                sw.Write(tab + "[] call {");
                if (this.children.Count != 1 || !(this.children[0] is Ident))
                    throw new Exception();
                Ident ident = (Ident)this.children[0];
                switch (ident.ReferencedType.varType)
                {
                    case VarType.Object:
                        {
                            sw.WriteLine("private \"_index\"; ");
                            sw.Write(tab + "_index = (");
                            ident.writeOut(sw, cfg);
                            sw.WriteLine(") select 0 find \"" + this.varType.ident.FullyQualifiedName + "\"; ");
                            sw.WriteLine(tab + "if (_index == -1) then { throw \"InvalidCastException\"; }; ");
                            sw.Write(tab + '(');
                            ident.writeOut(sw, cfg);
                            sw.Write(") set [2, ");
                            var result = HelperClasses.NamespaceResolver.getClassReferenceOfFQN(ident.ReferencedType.ident.LastIdent);
                            if (result == null)
                                throw new Exception();

                            var index = result is oosClass ? ((oosClass)result).getClassIndex(this.varType.ident) : -1;
                            if (index == -1)
                                throw new Exception();
                            ident.writeOut(sw, cfg);
                            sw.Write(" select 1 select _index];");
                            ident.writeOut(sw, cfg);
                        } break;
                    case VarType.Bool: //ToDo: make an actual cast instead of just rewriting the output type during compile time
                        ident.writeOut(sw, cfg);
                        break;
                    case VarType.BoolArray: //ToDo: make an actual cast instead of just rewriting the output type during compile time
                        ident.writeOut(sw, cfg);
                        break;
                    case VarType.Scalar: //ToDo: make an actual cast instead of just rewriting the output type during compile time
                        ident.writeOut(sw, cfg);
                        break;
                    case VarType.ScalarArray: //ToDo: make an actual cast instead of just rewriting the output type during compile time
                        ident.writeOut(sw, cfg);
                        break;
                    case VarType.String:
                        sw.Write("str ");
                        ident.writeOut(sw, cfg);
                        break;
                    case VarType.StringArray: //ToDo: make an actual cast instead of just rewriting the output type during compile time
                        ident.writeOut(sw, cfg);
                        break;
                    default:
                        throw new Exception();
                }
                sw.Write("}");
            }
        }
    }
}
