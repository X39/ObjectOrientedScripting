using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler.SqfConfigObjects
{
    class SqfConfigClass : iSqfConfigChildren
    {
        List<iSqfConfig> children;
        string name;
        public List<iSqfConfig> Children { get { return this.children; } }
        public string Name { get { return this.name; } set { this.name = value; } }
        public SqfConfigClass(string name)
        {
            this.children = new List<iSqfConfig>();
            this.name = name;
        }
        public void addChild(iSqfConfig obj)
        {
            if (obj is SqfConfigFile)
                throw new Exception("Not Added Exception, if you ever experience this create a bug. SqfConfigClass");
            this.children.Add(obj);
        }
        public int countChildren()
        {
            int count = 0;
            foreach (var it in this.children)
            {
                if (it is SqfConfigClass)
                {
                    count += ((SqfConfigClass)it).countChildren();
                }
                else
                    count++;
            }
            return count;
        }
        public void write(StreamWriter writer, int tabCount = 0)
        {
            if (this.countChildren() == 0)
                return;
            string tab = new string('\t', tabCount);
            writer.WriteLine(tab + "class " + name);
            writer.WriteLine(tab + "{");
            foreach (iSqfConfig c in children)
            {
                c.write(writer, tabCount + 1);
            }
            writer.WriteLine(tab + "};");
        }
    }
}
