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

using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Furion.Extensions;

/// <summary>
///     <see cref="string" /> 拓展类
/// </summary>
internal static partial class StringExtensions
{
    /// <summary>
    ///     将字符串首字母转换为小写
    /// </summary>
    /// <param name="input">
    ///     <see cref="string" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    internal static string? ToLowerFirstLetter(this string? input)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(input))
        {
            return input;
        }

        // 初始化字符串构建器
        var stringBuilder = new StringBuilder(input);

        // 设置字符串构建器首个字符为小写
        stringBuilder[0] = char.ToLower(stringBuilder[0]);

        return stringBuilder.ToString();
    }

    /// <summary>
    ///     将字符串进行转义
    /// </summary>
    /// <param name="input">
    ///     <see cref="string" />
    /// </param>
    /// <param name="escape">是否转义字符串</param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    internal static string? EscapeDataString(this string? input, bool escape)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(input))
        {
            return input;
        }

        return !escape ? input : Uri.EscapeDataString(input);
    }

    /// <summary>
    ///     检查字符串是否存在于给定的集合中
    /// </summary>
    /// <param name="input">
    ///     <see cref="string" />
    /// </param>
    /// <param name="collection">
    ///     <see cref="IEnumerable{T}" />
    /// </param>
    /// <param name="comparer">
    ///     <see cref="IEqualityComparer" />
    /// </param>
    /// <returns>
    ///     <see cref="bool" />
    /// </returns>
    internal static bool IsIn(this string? input, IEnumerable<string?> collection,
        IEqualityComparer? comparer = null)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(collection);

        // 使用默认或提供的比较器
        comparer ??= EqualityComparer<string>.Default;

        return input is null ? collection.Any(u => u is null) : collection.Any(u => comparer.Equals(input, u));
    }

    /// <summary>
    ///     解析 URL 中的查询字符串为键值对列表
    /// </summary>
    /// <param name="urlQuery">URL 中的查询字符串</param>
    /// <returns>
    ///     <see cref="List{T}" />
    /// </returns>
    internal static List<KeyValuePair<string, string?>> ParseUrlQueryParameters(this string urlQuery)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(urlQuery);

        if (string.IsNullOrWhiteSpace(urlQuery))
        {
            return [];
        }

        var pairs = urlQuery.TrimStart('?').Split('&');
        return (from pair in pairs
            select pair.Split('=')
            into keyValue
            where keyValue.Length == 2
            select new KeyValuePair<string, string?>(keyValue[0], keyValue[1])).ToList();
    }

    /// <summary>
    ///     基于 GBK 编码将字符串右填充至指定的字节数
    /// </summary>
    /// <remarks>调用之前需确保上下文存在 <c>Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);</c> 代码。</remarks>
    /// <param name="output">字符串</param>
    /// <param name="totalByteCount">目标字节数</param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    internal static string? PadStringToByteLength(this string? output, int totalByteCount)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(output))
        {
            return output;
        }

        // 小于或等于 0 检查
        if (totalByteCount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalByteCount),
                "Total byte count must be greater than zero.");
        }

        // 获取 GBK 编码实例
        var coding = Encoding.GetEncoding("gbk");

        // 获取字符串的字节数组
        var bytes = coding.GetBytes(output);
        var currentByteCount = bytes.Length;

        // 如果当前字节长度已经等于或超过目标字节长度，则直接返回原字符串
        if (currentByteCount >= totalByteCount)
        {
            return output;
        }

        // 计算需要添加的空格数量
        var spaceBytes = coding.GetByteCount(" ");
        var paddingSpaces = (totalByteCount - currentByteCount) / spaceBytes;

        // 确保填充不会超出范围
        if (currentByteCount + (paddingSpaces * spaceBytes) > totalByteCount)
        {
            paddingSpaces--;
        }

        // 创建新的字符串并进行填充
        var paddedChars = new char[output.Length + paddingSpaces];
        output.CopyTo(0, paddedChars, 0, output.Length);

        // 填充剩余部分
        for (var i = output.Length; i < output.Length + paddingSpaces; i++)
        {
            paddedChars[i] = ' ';
        }

        return new string(paddedChars);
    }

    /// <summary>
    ///     替换字符串中的占位符为实际值
    /// </summary>
    /// <param name="template">包含占位符的模板字符串</param>
    /// <param name="replacementSource">
    ///     <see cref="IDictionary{TKey,TValue}" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    internal static string? ReplacePlaceholders(this string? template, IDictionary<string, string?> replacementSource)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(replacementSource);

        return template is null
            ? null
            : PlaceholderRegex().Replace(template,
                match => replacementSource.TryGetValue(match.Groups[1].Value.Trim(), out var replacement)
                    // 如果找到匹配则替换
                    ? replacement ?? string.Empty
                    // 否则返回原始字符串
                    : match.ToString());
    }

    /// <summary>
    ///     替换字符串中的占位符为实际值
    /// </summary>
    /// <param name="template">包含占位符的模板字符串</param>
    /// <param name="replacementSource">
    ///     <see cref="object" />
    /// </param>
    /// <param name="modelName">模板字符串中对象名；默认值为：<c>model</c>。</param>
    /// <param name="bindingFlags">
    ///     <see cref="BindingFlags" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    internal static string? ReplacePlaceholders(this string? template, object replacementSource,
        string modelName = "model",
        BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(replacementSource);

        return template is null
            ? null
            : PlaceholderRegex().Replace(template,
                match =>
                {
                    // 获取模板解析后的值
                    var replacement =
                        replacementSource.GetPropertyValueFromPath(match.Groups[1].Value.Trim(), out var isMatch,
                            modelName, bindingFlags);

                    return isMatch
                        // 如果找到匹配则替换
                        ? replacement?.ToCultureString(CultureInfo.InvariantCulture) ?? string.Empty
                        // 否则返回原始字符串
                        : match.ToString();
                });
    }

    /// <summary>
    ///     占位符匹配正则表达式
    /// </summary>
    /// <remarks>占位符格式：<c>{Key}</c> 或 <c>{Key.Property}</c> 或 {Key.Property.NestProperty}。</remarks>
    /// <returns>
    ///     <see cref="Regex" />
    /// </returns>
    [GeneratedRegex(@"\{\s*(\w+\s*(\.\s*\w+\s*)*)\s*\}")]
    private static partial Regex PlaceholderRegex();
}