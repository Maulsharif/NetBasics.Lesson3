using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NetBasics.Lesson3
{


    public static class FileSystemVisitor
    {
       
        public static IEnumerable<string> GetAllFilesAndDirectories(string str)
        {

            string shift = "..";
            str = str ?? throw new ArgumentNullException(nameof(str));
            DirectoryInfo root = new DirectoryInfo(str);

            if (!root.Exists) yield  break;
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;
            try
            {
                files = root.GetFiles("*.*");
            }
           
            catch (UnauthorizedAccessException ex)
            {
                //Console.WriteLine(e.Message);

            }
            catch (DirectoryNotFoundException ex)
            {

                //Console.WriteLine(e.Message);
            }
            if (files != null)
            {
                subDirs = root.GetDirectories();
                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    yield return shift+ dirInfo.Name;



                    foreach (string str1 in GetAllFilesAndDirectories(dirInfo.ToString()))
                    {
                        yield return  shift+ str1;

                    }

                }

                foreach (System.IO.FileInfo fi in files)
                {
                    yield return shift + fi.Name;

                }
            }
            else
            {
                yield break;
            }

        }
    }
}
