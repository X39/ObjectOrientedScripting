using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Logger
{
    public enum LogLevel
    {
        INFO     = "[INFO]   ",
        WARNING  = "[WARNING]",
        ERROR    = "[ERROR]  ",
        CONTINUE = "         "
    }
    public static void log(LogLevel l, string msg)
    {
        Console.WriteLine(l + "\t" + msg);
    }
}
