using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler.OOS_LanguageObjects
{
    public class ArrayAccess : pBaseLangObject
    {

        public ArrayAccess(pBaseLangObject parent) : base(parent) { }
        public override int doFinalize() { return 0; }
        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg)
        {
            if(this.Parent is Ident)
            {
                var parentIdent = (Ident)this.Parent;
                if(parentIdent.ReferencedObject is Variable)
                {
                    var variable = (Variable)parentIdent.ReferencedObject;
                    bool printSelect = true;
                    if(variable.ReferencedType.IsObject)
                    {
                        if (variable.ReferencedType.ident.ReferencedObject is Interfaces.iClass)
                        {
                            var c = (Interfaces.iClass)variable.ReferencedType.ident.ReferencedObject;
                            var fnc = c.getOperatorFunction(OverridableOperator.ArrayAccess);
                            if (fnc != null)
                            {
                                printSelect = false;
                                string variableName = ((Ident)this.Parent).WriteOutValue;
                                #region Copied from FunctionCall

                                bool flag = false;
                                if (fnc is NativeInstruction)
                                {
                                    var nIns = (NativeInstruction)fnc;
                                    List<string> stringList = new List<string>();
                                    stringList.Add(variableName);
                                    foreach (var it in this.children)
                                    {
                                        using (MemoryStream memStream = new MemoryStream())
                                        {
                                            StreamWriter memStreamWriter = new StreamWriter(memStream);
                                            it.writeOut(memStreamWriter, cfg);
                                            memStreamWriter.Flush();
                                            memStream.Seek(0, SeekOrigin.Begin);
                                            stringList.Add(new StreamReader(memStream).ReadToEnd());
                                        }
                                    }
                                    sw.Write(nIns.getCode(stringList.ToArray()));
                                }
                                else
                                {
                                    if (fnc.FunctionEncapsulation == Encapsulation.Static || fnc.IsConstructor)
                                    {
                                        sw.Write('[');
                                    }
                                    else
                                    {
                                        sw.Write('[' + variableName);
                                        flag = true;
                                    }
                                    foreach (var it in this.children)
                                    {
                                        if (flag)
                                            sw.Write(", ");
                                        else
                                            flag = true;
                                        it.writeOut(sw, cfg);
                                    }
                                    if (fnc is Function)
                                    {
                                        if (fnc.FunctionEncapsulation == Encapsulation.Static || fnc.IsConstructor)
                                        {
                                            sw.Write(']' + " call " + ((Function)fnc).SqfVariableName);
                                        }
                                        else
                                        {
                                            sw.Write(']' + " call " + '(' + variableName + ')' + ((Function)fnc).SqfVariableName);
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception();
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                    if (printSelect)
                    {
                        if(((Ident)this.Parent).HasCallWrapper)
                        {
                            sw.Write(((Ident)this.Parent).WriteOutValue);
                        }
                        sw.Write(" select ");
                        foreach(var it in this.children)
                        {
                            it.writeOut(sw, cfg);
                        }
                    }
                }
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
