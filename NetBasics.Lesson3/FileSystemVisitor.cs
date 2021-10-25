using System;
using System.Collections.Generic;
using System.IO;

namespace NetBasics.Lesson3
{
    public static class FileSystemVisitor
    {
        public static IEnumerable<string> GetEnumerator(DirectoryInfo root)
        {
            if (root == null) yield break; 
            
            string shift = "..";
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

                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    yield return shift + dirInfo.FullName;

                    foreach (string str1 in GetEnumerator(dirInfo))
                    {
                        yield return shift + str1;
                    }
                }

                foreach (FileInfo fi in files)
                {
                    yield return shift + fi.FullName;
                }
            }
            else
            {
                yield break;
            }
        }

        public static IEnumerable<string> FileSystemInitializer(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

            DirectoryInfo root = new DirectoryInfo(path);

            if(! root.Exists) throw new DirectoryNotFoundException(nameof(root));
            
             return  GetEnumerator(root);

        }

        public static IEnumerable<string> GetAllFilesAndDirectories(string path, Predicate<string> filter)
        {
            var res = FileSystemInitializer(path);
            foreach (var item in res)
            {
                if (filter(item))
                {
                    yield return item;
                }

            }

        }
        public static IEnumerable<string> GetAllFilesAndDirectories(string path)
        {
            var res = FileSystemInitializer(path);
            foreach (var item in res)
            {
               
                    yield return item;

            }

        }

    }

}
