using Furion.SensitiveDetection;
using Microsoft.Extensions.Caching.Distributed;

namespace Furion.Application;

public class TestSensitiveDetection : IDynamicApiController
{
    private readonly ISensitiveDetectionProvider _sensitiveDetectionProvider;

    public TestSensitiveDetection(ISensitiveDetectionProvider sensitiveDetectionProvider)
    {
        _sensitiveDetectionProvider = sensitiveDetectionProvider;
    }

    /// <summary>
    /// 获取所有脱敏词汇
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<string>> GetWordsAsync()
    {
        return await _sensitiveDetectionProvider.GetWordsAsync();
    }

    /// <summary>
    /// 判断是否是正常的词汇
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public async Task<bool> VaildedAsync(string text)
    {
        return await _sensitiveDetectionProvider.VaildedAsync(text);
    }

    /// <summary>
    /// 替换非正常词汇
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public async Task<string> ReplaceAsync(string text)
    {
        return await _sensitiveDetectionProvider.ReplaceAsync(text, '*');
    }

    /// <summary>
    /// 返回敏感的词汇和位置
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public async Task<Dictionary<string, List<int>>> FoundSensitiveWordsAsync(string text)
    {
        return await _sensitiveDetectionProvider.FoundSensitiveWordsAsync(text);
    }
}