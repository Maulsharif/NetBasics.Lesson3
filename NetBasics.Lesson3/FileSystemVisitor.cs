using System;
using System.Collections.Generic;
using System.IO;

namespace NetBasics.Lesson3
{
    public  class FileSystemVisitor
    {
        private readonly string _root;

        private readonly Predicate<string> _filter = (x) => true;

        public event EventHandler<EventArgs> Start;
        public event EventHandler<EventArgs> Finish;
        public event EventHandler<FileSystemEventArgs> FileFinded;
        public event EventHandler<FileSystemEventArgs> FilteredFileFinded;

        public FileSystemVisitor( string root)
        {
            if(string.IsNullOrEmpty(root))
            {
                throw new ArgumentNullException(nameof(root));  
            }

            if(!Directory.Exists(root))
            {
                throw new DirectoryNotFoundException(nameof(root));
            }
            _root = root;
        }
        public FileSystemVisitor(string root, Predicate<string> filter):this(root)
        {
           
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            _root = root;
            _filter = filter;
        }


        private IEnumerable<string> GetEnumerator(DirectoryInfo rootDir)
        {
            string shift = "..";
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            try
            {
                files = rootDir.GetFiles("*.*");
            }

            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (files != null)
            {
                subDirs = rootDir.GetDirectories();
                
                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    if (_filter(dirInfo.FullName)) yield return shift + dirInfo.FullName;
                  

                    foreach (string str1 in GetEnumerator(dirInfo))
                    {
                        if (_filter(str1))  yield return shift + str1;
                          
                    }
                }
                foreach (FileInfo fi in files)
                {
                    var fileEvent = new FileSystemEventArgs { fileName = fi.FullName };
                    OnFileFinded(fileEvent);

                    if (fileEvent.stop) yield break;
                    if ( !fileEvent.remove &&_filter(fi.FullName)) yield return shift + fi.FullName;

                }
            }
            else
            {
                yield break;
            }
        }

        public  IEnumerable<string> GetAllFilesAndDirectories()
        {
            DirectoryInfo rootDir = new DirectoryInfo(_root);
            var res = GetEnumerator(rootDir);
            OnStart(new EventArgs());
            foreach (var item in res)
            {
                yield return item;
    
            }

            OnFinish(new EventArgs());

        }


        public virtual void OnStart(EventArgs args)
        {

            Start?.Invoke(this, args);
        }
        public virtual void OnFinish(EventArgs args)
        {

            Finish?.Invoke(this, args);
        }
        public virtual void OnFileFinded(FileSystemEventArgs args)
        {

            FileFinded?.Invoke(this, args);
        }
        public virtual void OnFilteredFileFinded(FileSystemEventArgs args)
        {

            FilteredFileFinded?.Invoke(this, args);
        }


    }

}
