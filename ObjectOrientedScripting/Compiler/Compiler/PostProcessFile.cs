using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Wrapper;

namespace Compiler
{
    public class PostProcessFile
    {
        private MemoryStream fileStream;
        public MemoryStream FileStream { get { return this.fileStream; } }
        private MemoryStream fullFileStream;
        public MemoryStream FullFileStream { get { return this.fullFileStream; } }
        private string filePath;
        public string FilePath { get { return filePath; } }
        private string name;
        public string Name { get { return name; } }
        
        public PostProcessFile(string path, string name)
        {
            this.fileStream = new MemoryStream();
            this.fullFileStream = new MemoryStream();
            this.filePath = path;
            this.name = name;
        }
        public void resetPosition()
        {
            this.fileStream.Seek(0, SeekOrigin.Begin);
            this.fullFileStream.Seek(0, SeekOrigin.Begin);
        }
        public override string ToString()
        {
            return filePath;
        }
    }
}
