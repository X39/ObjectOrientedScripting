using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler.SqfConfigObjects
{
    class SqfConfigField : iSqfConfig
    {
        string name;
        bool isArray;
        public string Name { get { return this.name; } set { this.name = value; } }
        public string value;
        public SqfConfigField(string name, string value, bool isArray = false)
        {
            this.name = name;
            this.value = value;
            this.isArray = isArray;
        }
        public void addChild(iSqfConfig obj)
        {
            throw new Exception("Not Added Exception, if you ever experience this create a bug. SqfConfigField");
        }
        public void write(StreamWriter writer, int tabCount = 0)
        {
            string tab = new string('\t', tabCount);
            writer.WriteLine(tab + name + (isArray ? "[] = " : " = ") + value + ";");
        }
    }
}
