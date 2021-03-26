using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Extensions.Contracts;

namespace Furion.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            "²âÊÔ Furion ¿ò¼ÜÊ¾·¶".Test(() =>
            {
                Assert.AreEqual("Furion", "Furion");
            });
        }
    }
}