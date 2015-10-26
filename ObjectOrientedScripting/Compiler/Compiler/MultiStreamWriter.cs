using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler
{
    class MultiStreamWriter
    {
        List<StreamWriter> writers;
        public MultiStreamWriter()
        {
            writers = new List<StreamWriter>();
        }

        public void Add(StreamWriter writer)
        {
            writers.Add(writer);
        }
        public void Remove(StreamWriter writer)
        {
            writers.Remove(writer);
        }
        public void KillAll()
        {
            foreach (var it in writers)
                it.Close();
        }
        public void Write(string s)
        {
            foreach(var it in writers)
                it.Write(s);
        }
        public void WriteLine(string s = "")
        {
            foreach (var it in writers)
                it.WriteLine(s);
        }
        public void Flush()
        {
            foreach (var it in writers)
                it.Flush();
        }
    }
}
