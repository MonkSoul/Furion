// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.HttpRemote;

/// <summary>
///     HTTP 文件传输事件处理程序
/// </summary>
public interface IHttpFileTransferEventHandler
{
    /// <summary>
    ///     用于处理在文件开始传输时的操作
    /// </summary>
    void OnTransferStarted();

    /// <summary>
    ///     用于处理在文件传输完成时的操作
    /// </summary>
    /// <param name="duration">总耗时（毫秒）</param>
    void OnTransferCompleted(long duration);

    /// <summary>
    ///     用于处理在文件传输发生异常时的操作
    /// </summary>
    /// <param name="exception">
    ///     <see cref="Exception" />
    /// </param>
    void OnTransferFailed(Exception exception);

    /// <summary>
    ///     用于传输进度发生变化时的操作
    /// </summary>
    /// <param name="fileTransferProgress">
    ///     <see cref="FileTransferProgress" />
    /// </param>
    /// <returns>
    ///     <see cref="Task" />
    /// </returns>
    Task OnProgressChangedAsync(FileTransferProgress fileTransferProgress);
}