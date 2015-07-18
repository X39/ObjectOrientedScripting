using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.SqfConfigObjects
{
    public class SqfConfigField : iSqfConfig
    {
        string name;
        public string Name { get { return this.name; } set { this.name = value; } }
        public string value;
        public SqfConfigField(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
        public void addChild(iSqfConfig obj)
        {
            throw new Exception("Not Added Exception, if you ever experience this create a bug. SqfConfigField");
        }
    }
}
