using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace Wrapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, "No Parameter provided, use \"<programm> -help\" for help");
                Logger.Instance.close();
                return;
            }
            string path = "";
            bool anyKey = true;
            bool createEmptyProject = false;
            string checkSyntaxFile = "";
            string dllPath = "";
            foreach (string s in args)
            {
                if (s.StartsWith("-"))
                {
                    int count = s.IndexOf('=');
                    if (count == -1)
                        count = s.Length - 1;
                    string switchstring = s.Substring(1, count - 1);
                    switch (switchstring)
                    {
                        case "help":
                            Logger.Instance.log(Logger.LogLevel.INFO, "Usage: <EXECUTABLE> [<PARAMS>] <PATH>");
                            Logger.Instance.log(Logger.LogLevel.CONTINUE, "     -help         Outputs this help page");
                            Logger.Instance.log(Logger.LogLevel.CONTINUE, "     -v            Enables VERBOSE logging mode");
                            Logger.Instance.log(Logger.LogLevel.CONTINUE, "     -d            Enables DEBUG logging mode");
                            Logger.Instance.log(Logger.LogLevel.CONTINUE, "     -a            Automation mode (no ANY key message)");
                            Logger.Instance.log(Logger.LogLevel.CONTINUE, "     -gen          Generates empty project at path");
                            Logger.Instance.log(Logger.LogLevel.CONTINUE, "     -sc=<FILE>    checks the syntax of the file");
                            Logger.Instance.log(Logger.LogLevel.CONTINUE, "     -dll=<FILE>   forces given dll for project");
                            Logger.Instance.log(Logger.LogLevel.CONTINUE, "     -log[=<FILE>] writes log output to file");
                            Logger.Instance.close();
                            if (anyKey)
                            {
                                Console.WriteLine("\nPress ANY key to continue");
                                Console.ReadKey();
                            }
                            return;
                        case "v":
                            if (Logger.Instance.LoggingLevel > Logger.LogLevel.VERBOSE)
                                Logger.Instance.LoggingLevel = Logger.LogLevel.VERBOSE;
                            break;
                        case "d":
                            if (Logger.Instance.LoggingLevel > Logger.LogLevel.DEBUG)
                                Logger.Instance.LoggingLevel = Logger.LogLevel.DEBUG;
                            break;
                        case "a":
                            anyKey = false;
                            break;
                        case "gen":
                            createEmptyProject = true;
                            break;
                        case "dll":
                            if (count == -1)
                            {
                                Logger.Instance.log(Logger.LogLevel.ERROR, "No path to DLL provided");
                                Logger.Instance.close();
                                if (anyKey)
                                {
                                    Console.WriteLine("\nPress ANY key to continue");
                                    Console.ReadKey();
                                }
                                return;
                            }
                            dllPath = s.Substring(count + 1);
                            break;
                        case "log":
                            string logfile = "";
                            if (count != -1)
                                logfile = s.Substring(count + 1);
                            Logger.Instance.setLogFile(logfile);
                            break;
                        case "sc":
                            if (count == -1)
                            {
                                Logger.Instance.log(Logger.LogLevel.ERROR, "No file provided for syntax checking");
                                Logger.Instance.close();
                                if (anyKey)
                                {
                                    Console.WriteLine("\nPress ANY key to continue");
                                    Console.ReadKey();
                                }
                                return;
                            }
                            checkSyntaxFile = s.Substring(count + 1);
                            break;
                    }
                }
                else
                {
                    path = s;
                }
            }
            Logger.Instance.log(Logger.LogLevel.VERBOSE, "extended output is enabled");
            Logger.Instance.log(Logger.LogLevel.DEBUG, "Debug output is enabled");
            if (!File.Exists(path))
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, "Cannot open project file as it does not exists (typo?).");
                Logger.Instance.close();
                if (anyKey)
                {
                    Console.WriteLine("\nPress ANY key to continue");
                    Console.ReadKey();
                }
                return;
            }
            if (checkSyntaxFile != "" && !File.Exists(checkSyntaxFile))
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, "Cannot open checkSyntax as it does not exists (typo?).");
                Logger.Instance.close();
                if (anyKey)
                {
                    Console.WriteLine("\nPress ANY key to continue");
                    Console.ReadKey();
                }
                return;
            }
            if (createEmptyProject)
            {

                try
                {
                    if (!Directory.Exists(path))
                    {
                        Logger.Instance.log(Logger.LogLevel.INFO, "Creating directory");
                        Directory.CreateDirectory(path);
                    }
                    Logger.Instance.log(Logger.LogLevel.INFO, "Creating project file");
                    StreamWriter writer = new StreamWriter(path + "poject.oosproj");
                    writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    writer.WriteLine("<root>");
                    writer.WriteLine("	<project>");
                    writer.WriteLine("		<title>testProject</title>");
                    writer.WriteLine("		<author>NA</author>");
                    writer.WriteLine("		<mainfile>./main.oos</mainfile>");
                    writer.WriteLine("		<outputfolder>./output/</outputfolder>");
                    writer.WriteLine("		<buildfolder>./build/</buildfolder>");
                    writer.WriteLine("	</project>");
                    writer.WriteLine("	<compiler version=\"0.2.0-ALPHA\" />");
                    writer.WriteLine("</root>");
                    writer.Close();
                    if (!File.Exists(path + "Main.oos"))
                    {
                        Logger.Instance.log(Logger.LogLevel.INFO, "Creating main file");
                        File.Create(path + "Main.oos").Close();
                    }
                    if (!Directory.Exists(path + "output"))
                    {
                        Logger.Instance.log(Logger.LogLevel.INFO, "Creating output directory");
                        Directory.CreateDirectory(path + "output");
                    }
                    if (!Directory.Exists(path + "build"))
                    {
                        Logger.Instance.log(Logger.LogLevel.INFO, "Creating build directory");
                        Directory.CreateDirectory(path + "build");
                    }
                    Logger.Instance.log(Logger.LogLevel.INFO, "Created empty project for 0.1.0-ALPHA compiler");
                }
                catch (Exception ex)
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, ex.Message);
                }
                Logger.Instance.close();
                if (anyKey)
                {
                    Console.WriteLine("\nPress ANY key to continue");
                    Console.ReadKey();
                }
                return;
            }
            Project proj;
            ICompiler compiler;
            try
            {
                proj = Project.openProject(path);
            }
            catch (Exception ex)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, "Failed to open Project file:");
                Logger.Instance.log(Logger.LogLevel.CONTINUE, ex.Message);
                Logger.Instance.close();
                if (anyKey)
                {
                    Console.WriteLine("\nPress ANY key to continue");
                    Console.ReadKey();
                }
                return;
            }
            try
            {
                //ToDo: Dont pick "default" compiler and instead pick by version number (need to scan filesystem for this + have a compiler rdy for usage ...)
                string compilerPath = "";
                string compilerVersion = proj.CompilerVersion.ToString();
                if (compilerVersion == "")
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, "Compiler version of given project is either not set or empty");
                    Logger.Instance.close();
                    if (anyKey)
                    {
                        Console.WriteLine("\nPress ANY key to continue");
                        Console.ReadKey();
                    }
                    return;
                }
                if (dllPath == "")
                {
                    foreach (var f in Directory.EnumerateFiles("./"))
                    {
                        if (f.Contains(compilerVersion))
                        {
                            compilerPath = f;
                            break;
                        }
                    }
                }
                else
                {
                    compilerPath = dllPath;
                }
                if (compilerPath == "")
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, "Coult not locate compiler dll with version number " + proj.CompilerVersion);
                    Logger.Instance.close();
                    if (anyKey)
                    {
                        Console.WriteLine("\nPress ANY key to continue");
                        Console.ReadKey();
                    }
                    return;
                }
                Assembly assembly = Assembly.LoadFrom(compilerPath);
                Type type = assembly.GetType("Wrapper.Compiler", true);
                compiler = (ICompiler)Activator.CreateInstance(type);

            }
            catch (Exception ex)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, "Failed to load compiler:");
                Logger.Instance.log(Logger.LogLevel.CONTINUE, ex.Message);
                Logger.Instance.close();
                if (anyKey)
                {
                    Console.WriteLine("\nPress ANY key to continue");
                    Console.ReadKey();
                }
                return;
            }
            if (checkSyntaxFile != "")
            {
                compiler.CheckSyntax(checkSyntaxFile);
            }
            else
            {
                try
                {
                    Logger.Instance.log(Logger.LogLevel.INFO, "-----Starting preprocessing-----");
                    compiler.Preprocess(proj);
                    Logger.Instance.log(Logger.LogLevel.INFO, "-----Preprocessing is  done-----");
                    Logger.Instance.log(Logger.LogLevel.INFO, "-----  Starting compiling  -----");
                    compiler.Compile(proj);
                    Logger.Instance.log(Logger.LogLevel.INFO, "-----  Compiling is  done  -----");
                    Logger.Instance.log(Logger.LogLevel.INFO, "----- Starting translating -----");
                    compiler.Translate(proj);
                    Logger.Instance.log(Logger.LogLevel.INFO, "----- Translating is  done -----");
                }
                catch (Exception ex)
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, "Failed to generate project:");
                    Logger.Instance.log(Logger.LogLevel.CONTINUE, ex.Message);
                }
            }

            Logger.Instance.close();
            if (anyKey)
            {
                Console.WriteLine("\nPress ANY key to continue");
                Console.ReadKey();
            }
        }
    }
}
