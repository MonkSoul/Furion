// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

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