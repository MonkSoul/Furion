// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.UnifyResult;

/// <summary>
/// RESTful 风格结果集
/// </summary>
/// <typeparam name="T"></typeparam>
[SuppressSniffer]
public class RESTfulResult<T>
{
    /// <summary>
    /// 状态码
    /// </summary>
    public int? StatusCode { get; set; }

    /// <summary>
    /// 数据
    /// </summary>
    public T Data { get; set; }

    /// <summary>
    /// 执行成功
    /// </summary>
    public bool Succeeded { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public object Errors { get; set; }

    /// <summary>
    /// 附加数据
    /// </summary>
    public object Extras { get; set; }

    /// <summary>
    /// 时间戳
    /// </summary>
    public long Timestamp { get; set; }
}