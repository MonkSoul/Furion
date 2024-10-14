// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Text.RegularExpressions;

namespace Furion.HttpRemote;

/// <summary>
///     HTTP 远程请求模块帮助类
/// </summary>
internal static partial class Helpers
{
    /// <summary>
    ///     从 <see cref="Uri" /> 中解析文件名
    /// </summary>
    /// <param name="uri">
    ///     <see cref="Uri" />
    /// </param>
    /// <returns>
    ///     <see cref="string" />
    /// </returns>
    internal static string? GetFileNameFromUri(Uri? uri)
    {
        // 空检查
        if (uri is null)
        {
            return null;
        }

        // 获取 URL 的绝对路径
        var path = uri.AbsolutePath;

        // 使用 / 分割路径，并获取最后一个部分作为潜在的文件名
        var parts = path.Split('/');
        var fileName = parts.Length > 0 ? parts[^1] : string.Empty;

        // 检查文件名是否为空或仅由点组成
        if (string.IsNullOrEmpty(fileName) || fileName.Trim('.').Length == 0)
        {
            return string.Empty;
        }

        // 查找文件名中的查询字符串开始位置。如果存在查询字符串，则去除它
        var queryStartIndex = fileName.IndexOf('?');
        if (queryStartIndex != -1)
        {
            fileName = fileName[..queryStartIndex];
        }

        // 检查文件名是否包含有效的扩展名
        var lastDotIndex = fileName.LastIndexOf('.');
        if (lastDotIndex == -1 || lastDotIndex == fileName.Length - 1)
        {
            return string.Empty;
        }

        // 使用 UTF-8 解码文件名
        return Uri.UnescapeDataString(fileName);
    }

    /// <summary>
    ///     解析 HTTP 谓词
    /// </summary>
    /// <param name="httpMethod">HTTP 谓词</param>
    /// <returns>
    ///     <see cref="HttpMethod" />
    /// </returns>
    internal static HttpMethod ParseHttpMethod(string? httpMethod)
    {
        // 空检查
        ArgumentException.ThrowIfNullOrWhiteSpace(httpMethod);

        return HttpMethod.Parse(httpMethod);
    }

    /// <summary>
    ///     验证字符串是否是 <c>application/x-www-form-urlencoded</c> 格式
    /// </summary>
    /// <param name="output">字符串</param>
    /// <returns>
    ///     <see cref="bool" />
    /// </returns>
    internal static bool IsFormUrlEncodedFormat(string output)
    {
        // 空检查
        ArgumentException.ThrowIfNullOrWhiteSpace(output);

        return FormUrlEncodedFormatRegex().IsMatch(output);
    }

    /// <summary>
    ///     <c>application/x-www-form-urlencoded</c> 格式正则表达式
    /// </summary>
    /// <returns>
    ///     <see cref="Regex" />
    /// </returns>
    [GeneratedRegex(
        @"^(?:(?:[a-zA-Z0-9-._~]+|%(?:[0-9A-Fa-f]{2}))+=(?:[a-zA-Z0-9-._~]*|%(?:[0-9A-Fa-f]{2}))+)(?:&(?:[a-zA-Z0-9-._~]+|%(?:[0-9A-Fa-f]{2}))+=(?:[a-zA-Z0-9-._~]*|%(?:[0-9A-Fa-f]{2}))+)*$")]
    private static partial Regex FormUrlEncodedFormatRegex();
}