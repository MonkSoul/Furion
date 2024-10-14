// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.HttpRemote;

/// <summary>
///     长轮询事件处理程序
/// </summary>
public interface IHttpLongPollingEventHandler
{
    /// <summary>
    ///     用于在长轮询时接收到数据时的操作
    /// </summary>
    /// <param name="httpResponseMessage">
    ///     <see cref="HttpResponseMessage" />
    /// </param>
    /// <returns>
    ///     <see cref="Task" />
    /// </returns>
    Task OnDataReceivedAsync(HttpResponseMessage httpResponseMessage);
}