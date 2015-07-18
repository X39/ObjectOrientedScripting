﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.SqfConfigObjects
{
    public class SqfConfigClass : iSqfConfig
    {
        List<iSqfConfig> children;
        string name;
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
    }
}
