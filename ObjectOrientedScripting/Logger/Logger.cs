﻿using System;
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
    public LogLevel LoggingLevel { get { return minLogLevel; } set { minLogLevel = value; } }
    public Logger()
    {
        String filePath = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".log";
        lastLogLevel = LogLevel.CONTINUE;
        minLogLevel = LogLevel.INFO;
    }
    ~Logger()
    {
        this.close();
    }
    public void setLogFile(string path)
    {
        if (this.fstream != null)
            this.close();
        try
        {
            this.fstream = new StreamWriter(new FileStream(path == "" ? DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".log" : path, FileMode.CreateNew));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Could not initiate the logger: " + ex.Message);
            this.fstream = null;
        }
    }
    public void log(LogLevel l, string msg)
    {
        if (l != LogLevel.CONTINUE)
            lastLogLevel = l;
        if (lastLogLevel < minLogLevel)
            return;
        String line = logLevelTranslated[(int)l] + "\t" + msg;
        Console.WriteLine(line);
        if (this.fstream != null)
            fstream.WriteLine(line);
    }
    public void close()
    {
        if (this.fstream == null)
            return;
        this.fstream.Close();
        this.fstream = null;
    }
}
