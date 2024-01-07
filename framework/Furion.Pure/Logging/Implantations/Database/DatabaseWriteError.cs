// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Logging;

/// <summary>
/// 数据库写入错误信息上下文
/// </summary>
[SuppressSniffer]
public sealed class DatabaseWriteError
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="exception">异常对象</param>
    internal DatabaseWriteError(Exception exception)
    {
        Exception = exception;
    }

    /// <summary>
    /// 引起数据库写入异常信息
    /// </summary>
    public Exception Exception { get; private set; }
}