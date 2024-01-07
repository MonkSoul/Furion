// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Runtime.Serialization;

namespace Furion.ViewEngine;

/// <summary>
/// 视图引擎异常类
/// </summary>
[SuppressSniffer]
public class ViewEngineException : Exception
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public ViewEngineException()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected ViewEngineException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message"></param>
    public ViewEngineException(string message) : base(message)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public ViewEngineException(string message, Exception innerException) : base(message, innerException)
    {
    }
}