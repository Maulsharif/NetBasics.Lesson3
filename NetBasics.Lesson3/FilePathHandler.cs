using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NetBasics.Lesson3
{
    public class FilePathHandler
    {
        
        public FileInfo[]  GetAllFiles(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            return info.GetFiles("*.*");
        }

        public DirectoryInfo[] GetDAllirectories(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            return info.GetDirectories();
        }
    }
}
