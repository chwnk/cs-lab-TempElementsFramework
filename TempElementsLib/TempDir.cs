using System;
using System.IO;
using TempElementsLib.Interfaces;

namespace TempElementsLib
{
    public class TempDir : ITempDir
    {
        public readonly DirectoryInfo directoryInfo;
        public string DirPath { get; }

        public bool IsEmpty => directoryInfo.GetFiles().Length == 0;

        public bool IsDestroyed { get; protected set; }

        public TempDir() {
            IsDestroyed = false;
            DirPath = Path.GetTempPath() + (Guid.NewGuid()).ToString();
            directoryInfo = new DirectoryInfo(DirPath);
            directoryInfo.Create();
            Console.WriteLine($"Directory {directoryInfo.FullName} created");
        }

        public TempDir(string path)
        {
            IsDestroyed = false;
            DirPath = Path.GetFullPath(path);
            directoryInfo = new DirectoryInfo(DirPath);
            directoryInfo.Create();
            Console.WriteLine($"Directory {directoryInfo.FullName} created");
        }

        ~TempDir() { Dispose(); }

        public void Dispose()
        {
            directoryInfo?.Delete();
            GC.SuppressFinalize(this);
            IsDestroyed = true;
            Console.WriteLine($"File {directoryInfo.FullName} deleted");
        }

        public void Empty() {
            foreach(FileInfo file in directoryInfo.GetFiles())
            {
                file.Delete();
            }
        }
    }
}
