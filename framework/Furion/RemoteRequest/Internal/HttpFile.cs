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

namespace Furion.RemoteRequest;

/// <summary>
/// 远程请求文件类
/// </summary>
[SuppressSniffer]
public sealed class HttpFile
{
    /// <summary>
    /// 创建 HttpFile 类
    /// </summary>
    /// <param name="name"></param>
    /// <param name="bytes"></param>
    /// <param name="fileName"></param>
    /// <param name="escape"></param>
    /// <returns></returns>
    public static HttpFile Create(string name, byte[] bytes, string fileName = default, bool escape = true)
    {
        return new HttpFile
        {
            Name = name,
            Bytes = bytes,
            FileName = fileName,
            Escape = escape
        };
    }

    /// <summary>
    /// 创建 HttpFile 类
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fileStream"></param>
    /// <param name="fileName"></param>
    /// <param name="escape"></param>
    /// <returns></returns>
    public static HttpFile Create(string name, Stream fileStream, string fileName = default, bool escape = true)
    {
        return new HttpFile
        {
            Name = name,
            FileStream = fileStream,
            FileName = fileName,
            Escape = escape
        };
    }

    /// <summary>
    /// 添加多个文件
    /// </summary>
    /// <param name="name"></param>
    /// <param name="items"></param>
    /// <returns></returns>
    public static HttpFile[] CreateMultiple(string name, params (byte[] bytes, string fileName)[] items)
    {
        var files = new List<HttpFile>();
        if (items == null || items.Length == 0) return files.ToArray();

        foreach (var (bytes, fileName) in items)
        {
            files.Add(Create(name, bytes, fileName));
        }

        return files.ToArray();
    }

    /// <summary>
    /// 添加多个文件
    /// </summary>
    /// <param name="name"></param>
    /// <param name="items"></param>
    /// <returns></returns>
    public static HttpFile[] CreateMultiple(string name, params (Stream fileStream, string fileName)[] items)
    {
        var files = new List<HttpFile>();
        if (items == null || items.Length == 0) return files.ToArray();

        foreach (var (fileStream, fileName) in items)
        {
            files.Add(Create(name, fileStream, fileName));
        }

        return files.ToArray();
    }

    /// <summary>
    /// 表单名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 文件名
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// 文件字节数组
    /// </summary>
    public byte[] Bytes { get; set; }

    /// <summary>
    /// 文件流
    /// </summary>
    public Stream FileStream { get; set; }

    /// <summary>
    /// 是否对表单名/文件名进行转义
    /// </summary>
    /// <remarks>默认 true</remarks>
    public bool Escape { get; set; } = true;
}