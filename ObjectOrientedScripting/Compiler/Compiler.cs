using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wrapper;
using Compiler;
using System.IO;

namespace Wrapper
{
    public class Compiler : ICompiler
    {
        public Version getVersion()
        {
            return new Version("1.0.0-a1");
        }
        public void Translate(Project proj)
        {
            throw new NotImplementedException();
        }
        public void Compile(Project proj)
        {
            Logger.log(Logger.LogLevel.INFO, "Warning: Compile is not supported by this compiler version");
        }
        public void Preprocess(Project proj)
        {
            if (!Directory.Exists(proj.Buildfolder))
                Directory.CreateDirectory(proj.Buildfolder);
            if (!File.Exists(proj.Buildfolder + "_preprocess_.obj"))
                File.Create(proj.Buildfolder + "_preprocess_.obj");
            StreamWriter writer = new StreamWriter(proj.Buildfolder + "_preprocess_.obj", false, Encoding.Unicode, 1024);
            Dictionary<string, PPDefine> defines = new Dictionary<string, PPDefine>();
            List<preprocessFile_IfDefModes> ifdefs = new List<preprocessFile_IfDefModes>();
            preprocessFile(ifdefs, defines, proj, proj.Mainfile, writer);
            writer.Flush();
            writer.Close();
        }
        private enum preprocessFile_IfDefModes
        {
            TRUE = 0,
            FALSE,
            IGNORE
        }
        private void preprocessFile(List<preprocessFile_IfDefModes> ifdefs, Dictionary<string, PPDefine> defines, Project proj, string filePath, StreamWriter writer)
        {
            StreamReader reader = new StreamReader(filePath);
            string s;
            uint filelinenumber = 0;
            while ((s = reader.ReadLine()) != null)
            {
                filelinenumber++;
                if (string.IsNullOrWhiteSpace(s))
                    continue;
                s = s.Trim();
                int spaceIndex = s.IndexOf(' ');
                if (spaceIndex < 0)
                    spaceIndex = s.Length;
                if (s[0] != '#')
                {
                    int i = ifdefs.Count - 1;
                    if (i >= 0 && ifdefs[i] != preprocessFile_IfDefModes.TRUE)
                        continue;
                    foreach (PPDefine def in defines.Values)
                        s = def.replace(s);
                    writer.WriteLine(s);
                    continue;
                }
                int index = -1;
                int index2 = -1;
                string afterDefine = s.Substring(spaceIndex).TrimStart();
                switch (s.Substring(0, spaceIndex))
                {
                    case "#include":
                        string newFile = proj.ProjectPath + afterDefine.Trim(new char[] { '"', '\'', ' ' });
                        if (newFile.Equals(filePath, StringComparison.OrdinalIgnoreCase))
                            throw new Exception("Include contains self reference. file: " + filePath + ". linenumber: " + filelinenumber);
                        preprocessFile(ifdefs, defines, proj, newFile, writer);
                        break;
                    case "#define":
                        while(s.EndsWith("\\"))
                        {
                            afterDefine += reader.ReadLine();
                            filelinenumber++;
                        }
                        if(index2 < 0)
                            index2 = index = afterDefine.IndexOf(' ');
                        index2 = afterDefine.IndexOf('(');
                        if (index < 0 || (index2 < index && index2 >= 0))
                            index2 = index = afterDefine.IndexOf('(');
                        if(index < 0)
                            index = afterDefine.Length;
                        defines.Add(afterDefine.Substring(0, index), new PPDefine(afterDefine));
                        break;
                    case "#undefine":
                        defines.Remove(s.Substring(spaceIndex).Trim());
                        break;
                    case "#ifdef":
                        if (defines.ContainsKey(afterDefine))
                            ifdefs.Add(ifdefs.Count == 0 || ifdefs[ifdefs.Count - 1] == preprocessFile_IfDefModes.TRUE ? preprocessFile_IfDefModes.TRUE : preprocessFile_IfDefModes.IGNORE);
                        else
                            ifdefs.Add(ifdefs.Count == 0 || ifdefs[ifdefs.Count - 1] == preprocessFile_IfDefModes.TRUE ? preprocessFile_IfDefModes.FALSE : preprocessFile_IfDefModes.IGNORE);
                        break;
                    case "#ifndef":
                        if (defines.ContainsKey(afterDefine))
                            ifdefs.Add(ifdefs.Count == 0 || ifdefs[ifdefs.Count - 1] == preprocessFile_IfDefModes.TRUE ? preprocessFile_IfDefModes.FALSE : preprocessFile_IfDefModes.IGNORE);
                        else
                            ifdefs.Add(ifdefs.Count == 0 || ifdefs[ifdefs.Count - 1] == preprocessFile_IfDefModes.TRUE ? preprocessFile_IfDefModes.TRUE : preprocessFile_IfDefModes.IGNORE);
                        break;
                    case "#else":
                        index = ifdefs.Count - 1;
                        if (index < 0)
                            throw new Exception("unexpected #else. file: " + filePath + ". linenumber: " + filelinenumber);
                        ifdefs[index] = (ifdefs[index] == preprocessFile_IfDefModes.TRUE ? preprocessFile_IfDefModes.FALSE : (ifdefs[index] == preprocessFile_IfDefModes.FALSE ? preprocessFile_IfDefModes.TRUE : preprocessFile_IfDefModes.IGNORE));
                        break;
                    case "#endif":
                        index = ifdefs.Count - 1;
                        if (index < 0)
                            throw new Exception("unexpected #endif. file: " + filePath + ". linenumber: " + filelinenumber);
                        ifdefs.RemoveAt(index);
                        break;
                }
            }
            reader.Close();
        }
    }
}
