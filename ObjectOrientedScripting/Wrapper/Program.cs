using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ObjectOrientedScripting
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach(string s in args)
                Console.WriteLine(s);
            if (args.Length == 0)
                return;
            if (!File.Exists(args[0]))
                return;
            Project proj = Project.openProject(args[0]);
            //ToDo: Select Compiler
        }
    }
}
