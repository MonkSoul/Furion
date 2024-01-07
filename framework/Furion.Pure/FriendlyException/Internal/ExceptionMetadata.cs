// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.FriendlyException;

/// <summary>
/// 异常元数据
/// </summary>
[SuppressSniffer]
public sealed class ExceptionMetadata
{
    /// <summary>
    /// 状态码
    /// </summary>
    public int StatusCode { get; internal set; }

    /// <summary>
    /// 错误码
    /// </summary>
    public object ErrorCode { get; internal set; }

    /// <summary>
    /// 错误码（没被复写过的 ErrorCode ）
    /// </summary>
    public object OriginErrorCode { get; internal set; }

    /// <summary>
    /// 错误对象（信息）
    /// </summary>
    public object Errors { get; internal set; }

    /// <summary>
    /// 额外数据
    /// </summary>
    public object Data { get; internal set; }
}