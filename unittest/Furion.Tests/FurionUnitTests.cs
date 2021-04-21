using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Extensions.Contracts;

namespace Furion.Tests
{
    [TestClass]
    public class FurionUnitTests
    {
        [ContractTestCase]
        public void TestCases()
        {
            "²âÊÔ Furion ¿ò¼ÜÊ¾·¶".Test(() =>
            {
                Assert.AreEqual("Fur", "Furion");
            });
        }
    }
}