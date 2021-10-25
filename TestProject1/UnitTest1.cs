using NetBasics.Lesson3;
using System;
using Xunit;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void CheckConstructor()
        {
            var service = FileSystemVisitor.GetEnumerator(null);
            Assert.NotNull(service);
            Assert.Throws<ArgumentNullException>(() =>  service);
        }

    }
}
