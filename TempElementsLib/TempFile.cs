using System;
using System.IO;
using System.Text;
using TempElementsLib.Interfaces;

namespace TempElementsLib
{
    public class TempFile : ITempFile
    {
        public readonly FileStream fileStream;
        public readonly FileInfo fileInfo;
        public string FilePath => fileInfo.FullName;

        public bool IsDestroyed { get; protected set; }

        public TempFile()
        {
            IsDestroyed = false;
            var path = Path.GetTempFileName();
            fileInfo = new FileInfo(path);
            fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.ReadWrite);
            Console.WriteLine($"File {fileInfo.FullName} created");
        }

        public TempFile(string path)
        {
            IsDestroyed = false;
            var fullPath = Path.GetFullPath(path);  // zgłasza stosowny wyjątek gdy jest niepoprawna ścieżka (ArgumentException)
            fileInfo = new FileInfo(fullPath);
            fileStream = new FileStream(
                fileInfo.FullName, FileMode.OpenOrCreate,
                FileAccess.ReadWrite);
            Console.WriteLine($"File {fileInfo.FullName} created");
        }

        ~TempFile() { Dispose(false); }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            IsDestroyed = true;
        }

        public void Dispose(bool disposing)
        {
            if(disposing)
            {
                fileStream?.Dispose();
            }
            fileInfo?.Delete();
            Console.WriteLine($"File {fileInfo.FullName} deleted");
        }

        public void AddText(string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fileStream.Write(info, 0, info.Length);
            fileStream.Flush();
        }
    }
}
