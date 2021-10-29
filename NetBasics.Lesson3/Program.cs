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
          
            try
            {

                var res =  new FileSystemVisitor(startDir,filter1);
                res.Start += (sender, e) => { Console.WriteLine("Start"); };
                res.Finish += (sender, e) => { Console.WriteLine("Finish"); };
                res.FileFinded += (sender, e) => { Console.WriteLine($"Before filter file finded:{e.fileName}"); };


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
