// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.FriendlyException;

/// <summary>
/// 异常复写特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public sealed class IfExceptionAttribute : Attribute
{
    /// <summary>
    /// 默认构造函数
    /// </summary>
    public IfExceptionAttribute()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="errorCode">错误编码</param>
    /// <param name="args">格式化参数</param>
    public IfExceptionAttribute(object errorCode, params object[] args)
    {
        ErrorCode = errorCode;
        Args = args;
    }

    /// <summary>
    /// 捕获特定异常类型异常（用于全局异常捕获）
    /// </summary>
    /// <param name="exceptionType"></param>
    public IfExceptionAttribute(Type exceptionType)
    {
        ExceptionType = exceptionType;
    }

    /// <summary>
    /// 错误编码
    /// </summary>
    public object ErrorCode { get; set; }

    /// <summary>
    /// 异常类型
    /// </summary>
    public Type ExceptionType { get; set; }

    /// <summary>
    /// 错误消息
    /// </summary>
    public string ErrorMessage { get; set; }

    /// <summary>
    /// 格式化参数
    /// </summary>
    public object[] Args { get; set; }
}