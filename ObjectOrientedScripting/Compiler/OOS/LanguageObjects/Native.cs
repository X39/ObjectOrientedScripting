using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.OOS_LanguageObjects
{
    public class Native : pBaseLangObject, Interfaces.iName, Interfaces.iTemplate, Interfaces.iClass
    {
        public int Line { get; internal set; }
        public int Pos { get; internal set; }
        public string File { get; internal set; }
        public Template TemplateObject { get; set; }
        public Ident Name { get { return ((Ident)this.children[0]); } set { this.children[0] = value; } }
        private List<Ident> parentIdents;
        public Native(pBaseLangObject parent, int line, int pos, string file) : base(parent)
        {
            this.addChild(null);
            this.Line = line;
            this.Pos = pos;
            this.File = file;
            this.parentIdents = new List<Ident>();
        }
        public override int doFinalize()
        {
            int errCount = 0;
            foreach(var it in this.parentIdents)
            {
                errCount += it.finalize();
            }
            if(errCount == 0)
            {
                foreach(var it in this.parentIdents)
                {
                    Native nClass = (Native)it.LastIdent.ReferencedObject;
                    foreach(var child in nClass.children)
                    {
                        if (child is NativeFunction)
                        {
                            var nf = (NativeFunction)child;
                            var nf2 = new NativeFunction(this, nf.Line, nf.Pos, nf.File);
                            nf2.Code = nf.Code;
                            nf2.IsSimple = nf.IsSimple;
                            nf2.children = nf.children;
                            nf2.VTO = new VarTypeObject(nf.VTO);
                            nf2.Name = new Ident(this, nf.Name.OriginalValue, nf.Name.Line, nf.Name.Pos, nf.Name.File);
                            this.addChild(nf2);
                            nf2.finalize();
                        }
                        else if(child is NativeOperator)
                        {
                            var no = (NativeOperator)child;
                            var no2 = new NativeOperator(this, no.Line, no.Pos, no.File);
                            no2.Code = no.Code;
                            no2.IsSimple = no.IsSimple;
                            no2.children = no.children;
                            no2.OperatorType = no.OperatorType;
                            no2.VTO = new VarTypeObject(no.VTO);
                            no2.Name = new Ident(this, no.Name.OriginalValue, no.Name.Line, no.Name.Pos, no.Name.File);
                            this.addChild(no2);
                            no2.finalize();
                        }
                    }
                }
            }
            return errCount;
        }

        public List<Ident> ExtendedClasses
        {
            get { return new List<Ident>(); }
        }

        public VarTypeObject VTO { get; set; }
        public void addParent(Ident ident)
        {
            this.parentIdents.Add(ident);
        }
        public Interfaces.iOperatorFunction getOperatorFunction(OverridableOperator op)
        {
            var opFncList = this.getAllChildrenOf<Interfaces.iOperatorFunction>();
            foreach(var it in opFncList)
            {
                if(it.OperatorType == op)
                {
                    return it;
                }
            }
            return null;
        }
        public override string ToString()
        {
            return "native->" + this.Name.FullyQualifiedName;
        }
        public override void writeOut(System.IO.StreamWriter sw, SqfConfigObjects.SqfConfigFile cfg) { }
    }
}
