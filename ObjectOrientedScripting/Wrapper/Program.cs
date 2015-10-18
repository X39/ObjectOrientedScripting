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
        static int Main(string[] args)
        {
            var exitCode = -1;
            if (args.Length == 0)
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, "No Parameter provided, use \"<programm> -help\" for help");
                Logger.Instance.close();
                return exitCode;
            }
            string path = "";
            bool anyKey = true;
            bool createEmptyProject = false;
            string checkSyntaxFile = "";
            string dllPath = "";
            bool exitAfterParamReading = false;
            List<string> compilerFlags = new List<string>();
            foreach (string s in args)
            {
                if (s.StartsWith("-"))
                {
                    int count = s.IndexOf('=');
                    if (count == -1)
                        count = s.Length;
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
                            exitAfterParamReading = true;
                            exitCode = 0;
                            break;
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
                                exitAfterParamReading = true;
                                break;
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
                                exitAfterParamReading = true;
                                break;
                            }
                            checkSyntaxFile = s.Substring(count + 1);
                            break;
                    }
                }
                else if(s.StartsWith("/"))
                {
                    compilerFlags.Add(s);
                }
                else
                {
                    path = s;
                }
            }
            if(exitAfterParamReading)
            {
                Logger.Instance.close();
                if (anyKey)
                {
                    Console.WriteLine("\nPress ANY key to continue");
                    Console.ReadKey();
                }
                return exitCode;
            }
            Logger.Instance.log(Logger.LogLevel.VERBOSE, "extended output is enabled");
            Logger.Instance.log(Logger.LogLevel.DEBUG, "Debug output is enabled");
            if (createEmptyProject)
            {

                try
                {
                    if (!Directory.Exists(path))
                    {
                        Logger.Instance.log(Logger.LogLevel.INFO, "Creating directory");
                        Directory.CreateDirectory(path);
                    }
                    if (!path.EndsWith("\\"))
                        path += '\\';
                    Logger.Instance.log(Logger.LogLevel.VERBOSE, "Creating project file at '" + path + "project.oosproj" + "'");
                    StreamWriter writer = new StreamWriter(path + "project.oosproj");
                    writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    writer.WriteLine("<root>");
                    writer.WriteLine("	<project>");
                    writer.WriteLine("		<title>testProject</title>");
                    writer.WriteLine("		<author>NA</author>");
                    writer.WriteLine("		<mainfile>./main.oos</mainfile>");
                    writer.WriteLine("		<outputfolder>./output/</outputfolder>");
                    writer.WriteLine("		<buildfolder>./build/</buildfolder>");
                    writer.WriteLine("	</project>");
                    writer.WriteLine("	<compiler version=\"0.5.2-ALPHA\" />");
                    writer.WriteLine("</root>");
                    writer.Close();
                    if (!File.Exists(path + "Main.oos"))
                    {
                        Logger.Instance.log(Logger.LogLevel.VERBOSE, "Creating main file at '" + path + "Main.oos" + "'");
                        File.Create(path + "Main.oos").Close();
                    }
                    if (!Directory.Exists(path + "output"))
                    {
                        Logger.Instance.log(Logger.LogLevel.VERBOSE, "Creating output directory at '" + path + "output" + "'");
                        Directory.CreateDirectory(path + "output");
                    }
                    if (!Directory.Exists(path + "build"))
                    {
                        Logger.Instance.log(Logger.LogLevel.VERBOSE, "Creating build directory at '" + path + "build" + "'");
                        Directory.CreateDirectory(path + "build");
                    }
                    Logger.Instance.log(Logger.LogLevel.INFO, "Created empty project for 0.5.0-ALPHA compiler");
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
                return exitCode;
            }
            if (!File.Exists(path))
            {
                Logger.Instance.log(Logger.LogLevel.ERROR, "Cannot open project file as it does not exists (typo?).");
                Logger.Instance.close();
                if (anyKey)
                {
                    Console.WriteLine("\nPress ANY key to continue");
                    Console.ReadKey();
                }
                return exitCode;
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
                return exitCode;
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
                return exitCode;
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
                    return exitCode;
                }
                if (dllPath == "")
                {
                    var executablePath = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
                    executablePath = executablePath.Substring(0, executablePath.LastIndexOf('\\'));
                    foreach (var f in Directory.EnumerateFiles(executablePath))
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
                    return exitCode;
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
                return exitCode;
            }
            if (checkSyntaxFile != "")
            {
                compiler.CheckSyntax(checkSyntaxFile);
            }
            else
            {
#if DEBUG
#else
                try
                {
#endif
                    compiler.setFlags(compilerFlags.ToArray());
                    Logger.Instance.log(Logger.LogLevel.INFO, "-----Starting preprocessing-----");
                    compiler.Preprocess(proj);
                    Logger.Instance.log(Logger.LogLevel.INFO, "-----Preprocessing is  done-----");
                    Logger.Instance.log(Logger.LogLevel.INFO, "-----  Starting compiling  -----");
                    compiler.Compile(proj);
                    Logger.Instance.log(Logger.LogLevel.INFO, "-----  Compiling is  done  -----");
                    Logger.Instance.log(Logger.LogLevel.INFO, "----- Starting translating -----");
                    compiler.Translate(proj);
                    Logger.Instance.log(Logger.LogLevel.INFO, "----- Translating is  done -----");
                    exitCode = 0;
#if DEBUG
#else
                }
                catch (Exception ex)
                {
                    Logger.Instance.log(Logger.LogLevel.ERROR, "Failed to generate project:");
                    Logger.Instance.log(Logger.LogLevel.CONTINUE, ex.Message);
                }
#endif
            }

            Logger.Instance.close();
            if (anyKey)
            {
                Console.WriteLine("\nPress ANY key to continue");
                Console.ReadKey();
            }
            return exitCode;
        }
    }
}
