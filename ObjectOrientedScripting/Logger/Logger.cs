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
    private static Logger _instance;
    public static Logger Instance { get { if (_instance == null) _instance = new Logger(); return _instance; } }
    private StreamWriter fstream;
    public Logger()
    {
        String filePath = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".log";
        this.fstream = new StreamWriter(new FileStream(filePath, FileMode.CreateNew));
    }
    ~Logger()
    {
        this.fstream.Close();
    }
    public void log(LogLevel l, string msg)
    {
        String line = logLevelTranslated[(int)l] + "\t" + msg;
        Console.WriteLine(line);
        fstream.WriteLine(line);
    }
    public void close()
    {
        this.fstream.Close();
    }
}
