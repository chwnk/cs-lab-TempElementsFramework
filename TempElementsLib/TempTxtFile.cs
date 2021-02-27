using System;
using System.IO;

namespace TempElementsLib
{
    public class TempTxtFile : TempFile
    {
        public readonly StreamReader streamReader;
        public readonly StreamWriter streamWriter;

        public TempTxtFile() : base(Path.GetTempPath() + Guid.NewGuid() + ".txt")
        {
            streamReader = new StreamReader(fileStream);
            streamWriter = new StreamWriter(fileStream);
        }

        public TempTxtFile(string path) : base(path)
        {
            if(fileInfo.Extension != path)
            {
                throw new ArgumentException();
            }
            streamReader = new StreamReader(fileStream);
            streamWriter = new StreamWriter(fileStream);
        }

        ~TempTxtFile() { Dispose(false); }

        public string ReadLine => streamReader.ReadLine();
        public string ReadAllText()
        {
            streamReader.DiscardBufferedData();
            streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
            return streamReader.ReadToEnd();
        }

        public void Write(string t)
        {
            streamWriter.Write(t);
            streamWriter.Flush();
        }

        public void WriteLine(string t)
        {
            streamWriter.WriteLine(t);
            streamWriter.Flush();
        }
    }
}
