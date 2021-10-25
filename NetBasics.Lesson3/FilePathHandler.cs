using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NetBasics.Lesson3
{
    public class FilePathHandler
    {
        public IEnumerable<string> GetFiles(string path)
        {
            return Directory.EnumerateFiles(path);
        }

        public IEnumerable<string> GetDirectories(string path)
        {
            return Directory.EnumerateDirectories(path);
        }
    }
}
