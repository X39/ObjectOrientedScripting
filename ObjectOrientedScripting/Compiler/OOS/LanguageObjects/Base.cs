using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Base : pBaseLangObject
    {
        public Base() : base(null)
        {
            {
                oosClass objectClass = new oosClass(this);
                objectClass.Name = new Ident(objectClass, "object", -1, -1, "");
                objectClass.Name.IsGlobalIdentifier = true;
                objectClass.markEnd();
                objectClass.markExtendsEnd();
                this.addChild(objectClass);
                {
                    Function toStringFunction = new Function(objectClass);
                    toStringFunction.Name = new Ident(toStringFunction, "toString", -1, -1, "");
                    toStringFunction.IsVirtual = true;
                    toStringFunction.varType = new VarTypeObject(VarType.String);
                    toStringFunction.encapsulation = Encapsulation.Public;
                    toStringFunction.markArgListEnd();
                    objectClass.addChild(toStringFunction);
                    {
                        Return returnInstruction = new Return(toStringFunction, -1, -1, "");
                        toStringFunction.addChild(returnInstruction);
                        {
                            Expression expression = new Expression(returnInstruction, -1, -1, "");
                            returnInstruction.addChild(expression);
                            {
                                SqfCall sqfCall = new SqfCall(expression);
                                sqfCall.Name = new Ident(sqfCall, "str", -1, -1, "");
                                sqfCall.markEnd();
                                sqfCall.addChild(new Ident(sqfCall, "this", -1, -1, ""));
                                expression.lExpression = sqfCall;
                            }
                        }
                    }
                }
            }
        }
        public override int finalize()
        {
            HelperClasses.NamespaceResolver.BaseClass = this;
            return base.finalize();
        }
        public override int doFinalize() { return 0; }

        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            foreach(var it in this.children)
            {
                it.writeOut(sw, cfg);
            }
        }
    }
}
