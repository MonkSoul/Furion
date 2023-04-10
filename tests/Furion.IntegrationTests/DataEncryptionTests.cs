using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace Furion.IntegrationTests;

/// <summary>
/// 数据加解密集成测试
/// </summary>
public class DataEncryptionTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly ITestOutputHelper Output;
    private readonly WebApplicationFactory<Program> _factory;

    public DataEncryptionTests(ITestOutputHelper tempOutput,
        WebApplicationFactory<Program> factory)
    {
        Output = tempOutput;
        _factory = factory;
    }

    /// <summary>
    /// 测试 MD5 加密及大小写设置
    /// </summary>
    /// <param name="url"></param>
    /// <param name="text"></param>
    /// <param name="uppercase"></param>
    /// <returns></returns>
    [Theory]
    [InlineData("/DataEncryptionTests/TestMD5Encrypt", "Furion", true)]
    [InlineData("/DataEncryptionTests/TestMD5Encrypt", "Furion", false)]
    public async Task Test_MD5_Encrypt(string url, string text, bool uppercase)
    {
        using var httpClient = _factory.CreateClient();
        using var response = await httpClient.PostAsync($"{url}/{text}/{uppercase}", default);

        var content = await response.Content.ReadAsStringAsync();
        Output.WriteLine(content);
        response.EnsureSuccessStatusCode();

        Assert.Equal(uppercase ? "08C5318BD8A478A88E6D594A0142AE6C" : "08c5318bd8a478a88e6d594a0142ae6c", content);
    }

    /// <summary>
    /// 测试 MD5 对比
    /// </summary>
    /// <param name="url"></param>
    /// <param name="text"></param>
    /// <param name="hash"></param>
    /// <param name="uppercase"></param>
    /// <returns></returns>
    [Theory]
    [InlineData("/DataEncryptionTests/TestMD5Compare", "Furion", "08C5318BD8A478A88E6D594A0142AE6C", true)]
    [InlineData("/DataEncryptionTests/TestMD5Compare", "Furion", "08c5318bd8a478a88e6d594a0142ae6c", false)]
    public async Task Test_MD5_Compare(string url, string text, string hash, bool uppercase)
    {
        using var httpClient = _factory.CreateClient();
        using var response = await httpClient.PostAsync($"{url}/{text}/{hash}/{uppercase}", default);

        var content = await response.Content.ReadAsStringAsync();
        Output.WriteLine(content);
        response.EnsureSuccessStatusCode();

        Assert.True(bool.Parse(content));
    }
}