using Xunit;
using Xunit.Abstractions;

namespace Furion.UnitTests;

public class SampleTests
{
    /// <summary>
    /// 输出日志
    /// </summary>
    private readonly ITestOutputHelper Output;

    private readonly ISystemService _sysService;

    public SampleTests(ITestOutputHelper tempOutput, ISystemService sysService)
    {
        Output = tempOutput;
        _sysService = sysService;
    }

    [Fact]
    public void TestRootService()
    {
        Assert.NotNull(App.RootServices);
    }

    [Fact]
    public void Test_String_Equal()
    {
        Output.WriteLine("输出一条日志");
        Assert.NotEqual("Furion", "Fur");
    }

    [Theory]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(7)]
    public void Test_Numbers_Is_Odd(int value)
    {
        Assert.True(IsOdd(value));
    }

    [Fact]
    public void Test_Dependency_Injection()
    {
        Assert.Equal("Furion", _sysService.GetName());
    }

    private static bool IsOdd(int value)
    {
        return value % 2 == 1;
    }
}