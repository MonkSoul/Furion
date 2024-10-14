// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Extensions;
using System.Text;

namespace Furion.Utilities;

/// <summary>
///     提供字符串实用方法
/// </summary>
public static class StringUtility
{
    /// <summary>
    ///     格式化键值集合摘要
    /// </summary>
    /// <param name="keyValues">键值集合</param>
    /// <param name="summary">摘要</param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    public static string? FormatKeyValuesSummary(IEnumerable<KeyValuePair<string, IEnumerable<string>>> keyValues,
        string? summary = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(keyValues);

        // 获取键值集合数量
        var keyValuePairs = keyValues as KeyValuePair<string, IEnumerable<string>>[] ?? keyValues.ToArray();
        var count = keyValuePairs.Length;

        // 空检查
        if (count == 0)
        {
            return null;
        }

        // 注册 CodePagesEncodingProvider，使得程序能够识别并使用 Windows 代码页中的各种编码
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        // 获取最长键名长度用于对齐键名字符串
        var totalByteCount = keyValuePairs.Max(h => h.Key.Length) + 5;

        // 初始化 StringBuilder 实例
        var stringBuilder = new StringBuilder();

        // 检查是否设置了摘要
        var hasSummary = !string.IsNullOrWhiteSpace(summary);

        // 逐条构建摘要信息
        var index = 0;
        foreach (var (key, value) in keyValuePairs)
        {
            // 检查是否包含摘要，如果有则添加制表符
            if (hasSummary)
            {
                stringBuilder.Append('\t');
            }

            stringBuilder.Append($"{(key + ':').PadStringToByteLength(totalByteCount)} {string.Join(", ", value)}");

            // 处理最后一行空行问题
            if (index < count - 1)
            {
                stringBuilder.Append("\r\n");
            }

            index++;
        }

        // 获取字符串
        var formatString = stringBuilder.ToString();

        return hasSummary ? $"{summary}: \r\n{formatString}" : formatString;
    }
}