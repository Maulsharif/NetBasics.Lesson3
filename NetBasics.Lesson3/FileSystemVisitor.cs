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
        public event EventHandler<FileSystemEventArgs> FileAccessDenied;
        public event EventHandler<FileSystemEventArgs> FileFinded;
        public event EventHandler<FileSystemEventArgs> FilteredFileFinded;
        public event EventHandler<DirectorySystemEventArgs> DirectoryFinded;
        public event EventHandler<DirectorySystemEventArgs> FilteredDirectoryFinded;

        public FileSystemVisitor( string root)
        {
            if(string.IsNullOrEmpty(root))
            {
                throw new ArgumentNullException(nameof(root));  
            }

            if(!Directory.Exists(root))
            {
                throw new DirectoryNotFoundException($"{root} directory doesn't exist");
            }
            _root = root;
        }
        public FileSystemVisitor(string root, Predicate<string> filter)
        {
            if (string.IsNullOrEmpty(root))
            {
                throw new ArgumentNullException(nameof(root));
            }

            if (!Directory.Exists(root))
            {
                throw new DirectoryNotFoundException($"{root} directory doesn't exist");
            }

            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            _root = root;
            _filter = filter;
        }


        private IEnumerable<string> GetEnumerator(DirectoryInfo rootDir)
        {
           
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            try
            {
                files = rootDir.GetFiles("*.*");
            }

            catch (UnauthorizedAccessException ex)
            {
                var fileEvent = new FileSystemEventArgs { fileName = ex.Message };
                OnFileAccessDenied(fileEvent);
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
                    var dirEvent = new DirectorySystemEventArgs { dirName = dirInfo.FullName };
                    OnDirectoryFinded(dirEvent);
                    if (dirEvent.stop) { yield break; }

                    if (!dirEvent.remove && _filter(dirInfo.FullName))
                    {  
                        yield return  dirInfo.FullName;
                        var filteredDirEvent = new DirectorySystemEventArgs { dirName = dirInfo.FullName };
                        OnFilteredDirectoryFinded(filteredDirEvent);
                    }
                 
                    foreach (string str1 in GetEnumerator(dirInfo))
                    {
                        if (_filter(str1))  yield return  str1;
                          
                    }
                }
                foreach (FileInfo fi in files)
                {
                    var fileEvent = new FileSystemEventArgs { fileName = fi.FullName };
                    OnFileFinded(fileEvent);
                    if (fileEvent.stop) yield break;
                    if (!fileEvent.remove && _filter(fi.FullName))
                    {
                        yield return  fi.FullName;
                        var fileteredFileEvent = new FileSystemEventArgs { fileName = fi.FullName };
                        OnFilteredFileFinded(fileteredFileEvent);
                    }
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
        public virtual void OnDirectoryFinded(DirectorySystemEventArgs args)
        {

            DirectoryFinded?.Invoke(this, args);
        }
        public virtual void OnFilteredDirectoryFinded(DirectorySystemEventArgs args)
        {

            FilteredDirectoryFinded?.Invoke(this, args);
        }
        public virtual void OnFileAccessDenied(FileSystemEventArgs args)
        {

            FileAccessDenied?.Invoke(this, args);
        }


    }

}
