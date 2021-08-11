using Xunit;

namespace Furion.UnitTests
{
    public class SampleTests
    {
        [Fact]
        public void TestRootService()
        {
            Assert.NotNull(App.RootServices);
        }

        [Fact]
        public void Test_String_Equal()
        {
            Assert.NotEqual("Furion", "Fur");
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(6)]
        public void Test_Numbers_Is_Odd(int value)
        {
            Assert.True(IsOdd(value));
        }

        [Fact]
        public void Test_Dependency_Injection()
        {
            Assert.Equal("Furion", App.GetService<ISystemService>().GetName());
        }

        private static bool IsOdd(int value)
        {
            return value % 2 == 1;
        }
    }
}