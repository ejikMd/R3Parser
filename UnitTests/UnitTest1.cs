using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var t = decimal.Parse("45.49336279");

            Assert.Equals(t, 45.49336279);
        }
    }
}
