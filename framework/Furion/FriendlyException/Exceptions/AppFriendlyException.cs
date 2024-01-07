// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;

namespace Furion.FriendlyException;

/// <summary>
/// 自定义友好异常类
/// </summary>
[SuppressSniffer]
public class AppFriendlyException : Exception
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public AppFriendlyException() : base()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message"></param>
    /// <param name="errorCode"></param>
    public AppFriendlyException(string message, object errorCode) : base(message)
    {
        ErrorMessage = message;
        ErrorCode = OriginErrorCode = errorCode;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message"></param>
    /// <param name="errorCode"></param>
    /// <param name="innerException"></param>
    public AppFriendlyException(string message, object errorCode, Exception innerException) : base(message, innerException)
    {
        ErrorMessage = message;
        ErrorCode = OriginErrorCode = errorCode;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public AppFriendlyException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    /// <summary>
    /// 错误码
    /// </summary>
    public object ErrorCode { get; set; }

    /// <summary>
    /// 错误码（没被复写过的 ErrorCode ）
    /// </summary>
    public object OriginErrorCode { get; set; }

    /// <summary>
    /// 错误消息（支持 Object 对象）
    /// </summary>
    public object ErrorMessage { get; set; }

    /// <summary>
    /// 状态码
    /// </summary>
    public int StatusCode { get; set; } = StatusCodes.Status500InternalServerError;

    /// <summary>
    /// 是否是数据验证异常
    /// </summary>
    public bool ValidationException { get; set; } = false;

    /// <summary>
    /// 额外数据
    /// </summary>
    public new object Data { get; set; }
}