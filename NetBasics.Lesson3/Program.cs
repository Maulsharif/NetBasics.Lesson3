using System;
using System.IO;

namespace NetBasics.Lesson3
{

    class Program
    {

        static void Main(string[] args)
        {
            
            string startDir = @"C:\Users\Madina_Mauilsharipov\Desktop\tempDir";
            
            Predicate<string> predicate = res => res.EndsWith(".txt");
            try
            {
                
                var res = FileSystemVisitor.GetAllFilesAndDirectories(startDir);
                foreach (var item in res)
               {
                   Console.WriteLine(item);
               }


            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

           
        }

    }

}
