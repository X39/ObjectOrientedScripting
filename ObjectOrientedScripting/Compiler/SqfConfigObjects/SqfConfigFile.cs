using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler.SqfConfigObjects
{
    public class SqfConfigFile : iSqfConfigChildren
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
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
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
        public void addParentalClass(string name)
        {
            var configClass = new SqfConfigClass(name);
            configClass.Children.AddRange(this.Children);
            this.children.Clear();
            this.addChild(configClass);
        }
        public void setValue(string path, string value)
        {
            string[] pathArray = path.Split(new char[] { '/' });
            iSqfConfigChildren cfgClass = this;
            bool flag;
            for (int i = 0; i < pathArray.Length - 1; i++)
            {
                string s = pathArray[i];
                flag = true;
                foreach(var it in cfgClass.Children)
                {
                    if(it.Name == s)
                    {
                        flag = false;
                        if(it is iSqfConfigChildren)
                        {
                            cfgClass = (iSqfConfigChildren)it;
                            break;
                        }
                        else
                        {
                            throw new Exception("Action would override existing field '" + s + "' with same name! Cannot continue.");
                        }
                    }
                }
                if(flag)
                {
                    var configClass = new SqfConfigClass(s);
                    cfgClass.addChild(configClass);
                    cfgClass = configClass;
                }
            }
            string fieldName = pathArray.Last();
            flag = true;
            foreach(var it in cfgClass.Children)
            {
                if (it.Name == fieldName)
                {
                    flag = false;
                    if(it is SqfConfigField)
                    {
                        ((SqfConfigField)it).value = value;
                        break;
                    }
                    else
                    {
                        throw new Exception("Action would override existing class with same name! Cannot continue.");
                    }
                }
            }
            if(flag)
            {
                var field = new SqfConfigField(fieldName, value);
                cfgClass.addChild(field);
            }
        }
    }
}
