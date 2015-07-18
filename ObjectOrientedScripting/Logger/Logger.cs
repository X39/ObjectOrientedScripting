using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public class Logger
{
    public enum LogLevel
    {
        DEBUG = 0,
        VERBOSE,
        INFO,
        WARNING,
        ERROR,
        CONTINUE
    }
    private static readonly string[] logLevelTranslated = {
        "[DEBUG]  ",
        "[VERBOSE]",
        "[INFO]   ",
        "[WARNING]",
        "[ERROR]  ",
        "         "
    };
    private static Logger _instance;
    public static Logger Instance { get { if (_instance == null) _instance = new Logger(); return _instance; } }
    private StreamWriter fstream;
    private LogLevel lastLogLevel;
    private LogLevel minLogLevel;
    public Logger()
    {
        String filePath = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".log";
        this.fstream = new StreamWriter(new FileStream(filePath, FileMode.CreateNew));
        lastLogLevel = LogLevel.CONTINUE;
        minLogLevel = LogLevel.INFO;
    }
    ~Logger()
    {
        this.fstream.Close();
    }
    public void log(LogLevel l, string msg)
    {
        if (l != LogLevel.CONTINUE)
            lastLogLevel = l;
        if (lastLogLevel < minLogLevel)
            return;
        String line = logLevelTranslated[(int)l] + "\t" + msg;
        Console.WriteLine(line);
        fstream.WriteLine(line);
    }
    public void close()
    {
        this.fstream.Close();
    }
}
