// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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
            : PlaceholderRegex().Replace(template, match => replacementSource[match.Groups[1].Value] ?? string.Empty);
    }

    /// <summary>
    ///     占位符匹配正则表达式
    /// </summary>
    /// <remarks>占位符格式：<c>{Key}</c>。</remarks>
    /// <returns>
    ///     <see cref="Regex" />
    /// </returns>
    [GeneratedRegex(@"\{(\w+)\}")]
    private static partial Regex PlaceholderRegex();
}