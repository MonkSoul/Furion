// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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
    /// <returns></returns>
    public static HttpFile Create(string name, byte[] bytes, string fileName = default)
    {
        return new HttpFile
        {
            Name = name,
            Bytes = bytes,
            FileName = fileName
        };
    }

    /// <summary>
    /// 创建 HttpFile 类
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fileStream"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static HttpFile Create(string name, Stream fileStream, string fileName = default)
    {
        return new HttpFile
        {
            Name = name,
            FileStream = fileStream,
            FileName = fileName
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
}