using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler.OOS_LanguageObjects
{
    public class FunctionCall : pBaseLangObject, Interfaces.iName, Interfaces.iHasType, Interfaces.iHasObject, Interfaces.iArgList
    {
        public Ident Name { get { return (Ident)Parent; } set { throw new Exception(); } }
        public string FullyQualifiedName { get { return ((Ident)Parent).FullyQualifiedName; } }

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
            string variableName = this.Name.WriteOutValue;
            if(this.ReferencedObject is Interfaces.iFunction)
            {
                Interfaces.iFunction fnc = (Interfaces.iFunction)this.ReferencedObject;
                bool flag = false;
                if (fnc is NativeInstruction)
                {
                    var nIns = (NativeInstruction)fnc;
                    List<string> stringList = new List<string>();
                    if (!string.IsNullOrEmpty(variableName))
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
                        if (fnc.IsVirtual)
                        {
                            sw.Write(']' + (!fnc.IsAsync ? " call (" : " spawn (") + '(' + variableName + ')' + ((Function)fnc).SqfVariableName + ')');
                        }
                        else
                        {
                            sw.Write(']' + (!fnc.IsAsync ? " call " : " spawn ") + ((Function)fnc).SqfVariableName);
                        }
                    }
                    else
                    {
                        throw new Exception();
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
