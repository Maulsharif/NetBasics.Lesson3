using System;
using System.IO;

namespace NetBasics.Lesson3
{

    class Program
    {

        static void Main(string[] args)
        {
            
            string startDir = @"C:\Users\Madina_Mauilsharipov\Desktop\tempDir";
            Predicate<string> filter1 = (res)=> res.Contains("txt");
            Predicate<string> filter2 = (res) => res.EndsWith("der");
         
            try
            {

                var res =  new FileSystemVisitor(startDir, filter1);
                res.Start += (sender, e) => { Console.WriteLine("START"); };
                res.Finish += (sender, e) => { Console.WriteLine("FINISH"); };
              
                res.FileFinded += (sender, e) => { Console.WriteLine($"---------------------------\nFILE finded:{e.fileName}"); };
                res.FilteredFileFinded += (sender, e) => { Console.WriteLine($"FILTERED FILE finded:{e.fileName}"); };
                res.DirectoryFinded += (sender, e) => { Console.WriteLine($"DIRECTORY finded:{e.dirName}"); };
                res.FilteredDirectoryFinded += (sender, e) => { Console.WriteLine($" FILTERED DIRECTORY finded:{e.dirName}"); };
                
                foreach (var item in res.GetAllFilesAndDirectories())
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
