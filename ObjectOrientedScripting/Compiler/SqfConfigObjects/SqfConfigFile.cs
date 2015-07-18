using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler.SqfConfigObjects
{
    public class SqfConfigFile : iSqfConfig
    {
        List<iSqfConfig> children;
        string name;
        public string Name { get { return this.name; } set { this.name = value; } }
        public List<iSqfConfig> Children { get { return this.children; } }
        public SqfConfigFile(string name)
        {
            this.children = new List<iSqfConfig>();
            this.name = name;
        }
        public void addChild(iSqfConfig obj)
        {
            if (obj is SqfConfigField)
                throw new Exception("Not Added Exception, if you ever experience this create a bug. SqfConfigFile");
            this.children.Add(obj);
        }
        public void writeOut(string path)
        {
            if (path.EndsWith("\\"))
                path += this.Name;
            else
                path += '\\' + this.Name;
            StreamWriter writer = new StreamWriter(path);
            write(writer, 0);
            writer.Flush();
            writer.Close();
        }
        public void write(StreamWriter writer, int tabCount = 0)
        {
            foreach (iSqfConfig c in children)
                c.write(writer, 0);
        }
    }
}
