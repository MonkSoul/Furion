using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace Furion.IntegrationTests;

/// <summary>
/// 全局对象 App 测试
/// </summary>
public class GlobalAppTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly ITestOutputHelper Output;
    private readonly WebApplicationFactory<Program> _factory;

    public GlobalAppTests(ITestOutputHelper tempOutput,
        WebApplicationFactory<Program> factory)
    {
        Output = tempOutput;
        _factory = factory;
    }

    /// <summary>
    /// 测试各个服务提供器是否一致
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    [Theory]
    [InlineData("/GlobalAppTests/TestServiceProvider")]
    public async Task Test_ServiceProvider(string url)
    {
        using var httpClient = _factory.CreateClient();
        using var response = await httpClient.PostAsync($"{url}", default);

        var content = await response.Content.ReadAsStringAsync();
        Output.WriteLine(content);
        response.EnsureSuccessStatusCode();

        Assert.True(bool.Parse(content));
    }
}