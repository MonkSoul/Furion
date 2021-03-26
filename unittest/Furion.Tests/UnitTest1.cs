using Furion.DataEncryption;

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
            "≤‚ ‘ Furion øÚº‹ æ∑∂".Test(() =>
            {
                Assert.AreEqual("Furion", "Furion");
            });
        }


        /// <summary>
        /// ≤‚ ‘º”√‹
        /// </summary>
        [TestMethod]
        public void TestDataEncryption()
        {
            var a = "123465".ToMd5();

            var b1 = "123465".ToAesEncry();
            var b2 = b1.ToAesDecry();

            var b3 = "123465".ToAesEncry("abc123");
            var b4 = b3.ToAesDecry("abc123");

            var c1 = "123123".ToDescEncry();
            var c2 = c1.ToDescDecry();

            var c3 = "123123".ToDescEncry("abc123");
            var c4 = c3.ToDescDecry("abc123");

        }

    }
}