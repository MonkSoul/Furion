// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.SensitiveDetection;

/// <summary>
/// 脱敏词汇（脱敏）提供器
/// </summary>
public interface ISensitiveDetectionProvider
{
    /// <summary>
    /// 返回所有脱敏词汇
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<string>> GetWordsAsync();

    /// <summary>
    /// 判断脱敏词汇是否有效（支持自定义算法）
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    Task<bool> VaildedAsync(string text);

    /// <summary>
    /// 替换敏感词汇
    /// </summary>
    /// <param name="text"></param>
    /// <param name="transfer"></param>
    /// <returns></returns>
    Task<string> ReplaceAsync(string text, char transfer = '*');

    /// <summary>
    /// 查找脱敏词汇
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    Task<Dictionary<string, List<int>>> FoundSensitiveWordsAsync(string text);
}