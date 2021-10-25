using System;
using System.Collections.Generic;
using System.IO;

namespace NetBasics.Lesson3
{
    
    class Program
    {
       
        static void Main(string[] args)
        {
            string startDir = @"C:\Users\Madina_Mauilsharipov\Desktop\tempDir";

            var res = FileSystemVisitor.GetAllFilesAndDirectories(startDir);

            foreach (var item in res)
            {
                Console.WriteLine(item);
            }

        }

    }

}
