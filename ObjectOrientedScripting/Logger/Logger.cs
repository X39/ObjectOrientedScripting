using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Logger
{
    public enum LogLevel
    {
        INFO = 0,
        WARNING,
        ERROR,
        CONTINUE
    }
    private static readonly string[] logLevelTranslated = {
        "[INFO]   ",
        "[WARNING]",
        "[ERROR]  ",
        "         "
    };
    public static void log(LogLevel l, string msg)
    {
        Console.WriteLine(logLevelTranslated[(int)l] + "\t" + msg);
    }
}
