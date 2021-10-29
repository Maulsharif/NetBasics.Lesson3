﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NetBasics.Lesson3
{
    //Флаги для файлов
    public class FileSystemEventArgs
    {
        public string fileName;
        public bool stop;
        public bool isRemoved;

    }

    //Флаги для папок
    public class DirectorySystemEventArgs
    {
        public string dirName;
        public bool stop;
        public bool isRemoved;

    }
}
