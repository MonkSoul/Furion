// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.FriendlyException;

/// <summary>
/// 异常元数据特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Field)]
public sealed class ErrorCodeItemMetadataAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="errorMessage">错误消息</param>
    /// <param name="args">格式化参数</param>
    public ErrorCodeItemMetadataAttribute(string errorMessage, params object[] args)
    {
        ErrorMessage = errorMessage;
        Args = args;
    }

    /// <summary>
    /// 错误消息
    /// </summary>
    public string ErrorMessage { get; set; }

    /// <summary>
    /// 错误码
    /// </summary>
    public object ErrorCode { get; set; }

    /// <summary>
    /// 格式化参数
    /// </summary>
    public object[] Args { get; set; }
}