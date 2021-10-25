using NUnit.Framework;
using System;
using System.IO;

namespace NetBasics.Lesson3.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void GetEnumerator_Null_Return_Exception()
        {
           var res =  FileSystemVisitor.GetEnumerator(null);
           
            Assert.That(() => res, Throws.InstanceOf<ArgumentNullException>());

        }


        
    }
}