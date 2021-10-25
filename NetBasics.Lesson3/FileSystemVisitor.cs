using System;
using System.Collections.Generic;
using System.IO;

namespace NetBasics.Lesson3
{
    public static class FileSystemVisitor
    {
        public static IEnumerable<string> GetEnumerator(DirectoryInfo root)
        {
            if (root == null)  throw new
              ArgumentNullException(nameof(root));
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
                    yield return shift + dirInfo.Name;

                    foreach (string str1 in GetAllFilesAndDirectories(dirInfo.ToString()))
                    {
                        yield return shift + str1;
                    }
                }

                foreach (FileInfo fi in files)
                {
                    yield return shift + fi.Name;
                }
            }
            else
            {
                yield break;
            }
        }

        public static IEnumerable<string> GetAllFilesAndDirectories(string path)
        { 
            DirectoryInfo root = new DirectoryInfo(path);
            if(root.Exists)
            {
                var res = GetEnumerator(root);
                foreach (var item in res)
                {
                    yield return item;
                }

            }
            else
            {
                yield break;
            }

        }

        public static IEnumerable<string> GetAllFilesAndDirectories(string filePath, Predicate<string> filter)
        {
            //todo: check string is null or empty
            filter = filter ?? throw new ArgumentNullException(nameof(filter));
            DirectoryInfo root = new DirectoryInfo(filePath);
            if (root.Exists)
            {
                var res = GetEnumerator(root);
                foreach (var item in res)
                {
                    if(filter(item))
                    {
                        yield return item;
                    }

                }

            }
            else
            {
                yield break;
            }

        }

    }

}
