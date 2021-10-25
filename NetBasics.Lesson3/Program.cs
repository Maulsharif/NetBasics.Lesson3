using System;
using System.IO;

namespace NetBasics.Lesson3
{

    class Program
    {

        static void Main(string[] args)
        {
            
            string startDir = @"C:\Users\Madina_Mauilsharipov\Desktop\tempDir";
            
            Predicate<string> filter1 = res => res.EndsWith(".txt");
          
            try
            {

                var res = FileSystemVisitor.GetAllFilesAndDirectories(startDir, filter1);
                foreach (var item in res)
               {
                   Console.WriteLine(item);
               }


            }
            catch (ArgumentNullException ex)
            {

                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


        }

    }

}
