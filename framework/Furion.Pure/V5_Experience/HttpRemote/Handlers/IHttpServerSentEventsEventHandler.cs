// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.HttpRemote;

/// <summary>
///     Server-Sent Events 事件处理程序
/// </summary>
public interface IHttpServerSentEventsEventHandler
{
    /// <summary>
    ///     用于在与事件源的连接打开时的操作
    /// </summary>
    void OnOpen();

    /// <summary>
    ///     用于在从事件源接收到数据时的操作
    /// </summary>
    /// <param name="serverSentEventsData">
    ///     <see cref="ServerSentEventsData" />
    /// </param>
    /// <returns>
    ///     <see cref="Task" />
    /// </returns>
    Task OnMessageAsync(ServerSentEventsData serverSentEventsData);

    /// <summary>
    ///     用于在事件源连接未能打开时的操作
    /// </summary>
    /// <param name="exception">
    ///     <see cref="Exception" />
    /// </param>
    void OnError(Exception exception);
}