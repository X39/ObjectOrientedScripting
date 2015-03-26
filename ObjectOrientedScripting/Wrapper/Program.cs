using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace Wrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach(string s in args)
                Console.WriteLine(s);
            if (args.Length == 0)
            {
                Logger.log(Logger.LogLevel.ERROR, "No Parameter provided");
                return;
            }
            if (!File.Exists(args[0]))
            {
                Logger.log(Logger.LogLevel.ERROR, "Cannot open not existing file");
                return;
            }
            Project proj;
            ICompiler compiler;
            try
            {
                proj = Project.openProject(args[0]);
            }
            catch (Exception ex)
            {
                Logger.log(Logger.LogLevel.ERROR, "Failed to open Project file:");
                Logger.log(Logger.LogLevel.CONTINUE, ex.Message);
                return;
            }
            try
            {
                //ToDo: Dont pick "default" compiler and instead pick by version number (need to scan filesystem for this + have a compiler rdy for usage ...)
                Assembly assembly = Assembly.LoadFrom(@"D:\GitHub\ObjectOrientedScripting\ObjectOrientedScripting\Compiler\bin\Debug\Compiler.dll");
                Type type = assembly.GetType("Wrapper.Compiler", true);
                compiler = (ICompiler)Activator.CreateInstance(type);
            }
            catch(Exception ex)
            {
                Logger.log(Logger.LogLevel.ERROR, "Failed to load compiler:");
                Logger.log(Logger.LogLevel.CONTINUE, ex.Message);
                return;
            }
            try
            {
                Logger.log(Logger.LogLevel.INFO, "-----Starting preprocessing-----");
                compiler.Preprocess(proj);
                Logger.log(Logger.LogLevel.INFO, "-----Preprocessing is  done-----");
                Logger.log(Logger.LogLevel.INFO, "-----  Starting compiling  -----");
                compiler.Compile(proj);
                Logger.log(Logger.LogLevel.INFO, "-----  Compiling is  done  -----");
                Logger.log(Logger.LogLevel.INFO, "----- Starting translating -----");
                compiler.Translate(proj);
                Logger.log(Logger.LogLevel.INFO, "----- Translating is  done -----");
            }
            catch (Exception ex)
            {
                Logger.log(Logger.LogLevel.ERROR, "Failed to patch project:");
                Logger.log(Logger.LogLevel.CONTINUE, ex.Message);
            }
            Console.WriteLine("\nPress ENTER to continue");
            Console.Read();
        }
    }
}
