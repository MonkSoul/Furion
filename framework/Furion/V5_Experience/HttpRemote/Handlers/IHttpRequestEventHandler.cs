// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.HttpRemote;

/// <summary>
///     HTTP 远程请求事件处理程序
/// </summary>
public interface IHttpRequestEventHandler
{
    /// <summary>
    ///     用于处理在发送 HTTP 请求之前的操作
    /// </summary>
    /// <param name="httpRequestMessage">
    ///     <see cref="HttpRequestMessage" />
    /// </param>
    void OnPreSendRequest(HttpRequestMessage httpRequestMessage);

    /// <summary>
    ///     用于处理在发送 HTTP 请求之后的操作
    /// </summary>
    /// <param name="httpResponseMessage">
    ///     <see cref="HttpResponseMessage" />
    /// </param>
    void OnPostSendRequest(HttpResponseMessage httpResponseMessage);

    /// <summary>
    ///     用于处理在发送 HTTP 请求发生异常时的操作
    /// </summary>
    /// <param name="exception">
    ///     <see cref="Exception" />
    /// </param>
    /// <param name="httpResponseMessage">
    ///     <see cref="HttpResponseMessage" />
    /// </param>
    void OnSendRequestFailed(Exception exception, HttpResponseMessage? httpResponseMessage);
}