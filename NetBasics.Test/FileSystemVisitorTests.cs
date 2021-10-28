using Moq;
using NetBasics.Lesson3;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NetBasics.Test
{

    [TestFixture]
    public class Tests
    {

        [Test]
        public void ConstructorExceptionCheck()
        {
            Assert.Throws<ArgumentNullException>(() => new FileSystemVisitor(null));
            Assert.Throws<ArgumentNullException>(() => new FileSystemVisitor(null, null));
            Assert.Throws<ArgumentNullException>(() => new FileSystemVisitor(null, (res) => res.Contains(".")));
            Assert.Throws<ArgumentNullException>(() => new FileSystemVisitor(@"C:\", null));

        }

        [Test]
        public void CheckDirElementsNumber()
        {
            string dir = @"../../../TestDir";
            var fileSystemVisitor = new FileSystemVisitor(dir);
            var fileSystemVisitorRes = fileSystemVisitor.GetAllFilesAndDirectories();
            int count = GetnumberOfElements(fileSystemVisitorRes);
            Assert.AreEqual(count, 2);
        }

     
        public int GetnumberOfElements(IEnumerable<string> dir)
        {
            int n = 0;
            foreach (var item in dir)
            {
                n++;
            }
            return n;
           
        }
    }

}