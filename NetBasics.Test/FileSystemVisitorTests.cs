using Moq;
using NetBasics.Lesson3;
using NUnit.Framework;
using System;
using System.IO;

namespace NetBasics.Test
{
   
    [TestFixture]
    public class Tests
    {

        [Test]
        public void GetAllFilesAndDirectoriesNullorWrongPath()
        {
           Assert.That(() => FileSystemVisitor.FileSystemInitializer(null), Throws.InstanceOf<ArgumentNullException>());
           Assert.That(() => FileSystemVisitor.FileSystemInitializer("gfhf"), Throws.InstanceOf<DirectoryNotFoundException>());
        }

        [Test]
        public void GetAllFilesAndDirectoriesCheckPredicate()
        {

            string dir = @"C:\Users\Madina_Mauilsharipov\source\repos\NetBasics.Lesson3\NetBasics.Test\TestDir\";
            string test = $"....{dir}Movies\\film1.txt";
            var res = FileSystemVisitor.GetAllFilesAndDirectories(dir, res => res.EndsWith(".txt"));
            string check = "";
            foreach (var item in res)
            {
                check = item;
                break;
            }
            Assert.AreEqual(check, test);
        }

    }
}