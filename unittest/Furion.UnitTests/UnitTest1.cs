using Xunit;

namespace Furion.UnitTests
{
    public class UnitTest1
    {
        public UnitTest1()
        {
            //var services = new ServiceCollection();
            //var serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public void Test1()
        {
            Assert.NotEqual("Furion", "Fur");
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(6)]
        public void MyFirstTheory(int value)
        {
            Assert.True(IsOdd(value));
        }

        private static bool IsOdd(int value)
        {
            return value % 2 == 1;
        }
    }
}