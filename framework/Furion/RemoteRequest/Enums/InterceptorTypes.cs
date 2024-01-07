// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.ComponentModel;

namespace Furion.RemoteRequest;

/// <summary>
/// 拦截类型
/// </summary>
[SuppressSniffer]
public enum InterceptorTypes
{
    /// <summary>
    /// 创建 HttpClient 拦截
    /// </summary>
    [Description("创建 HttpClient 拦截")]
    Initiate,

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    [Description("HttpClient 拦截")]
    Client,

    /// <summary>
    /// HttpRequestMessage 拦截
    /// </summary>
    [Description("HttpRequestMessage 拦截")]
    Request,

    /// <summary>
    /// HttpResponseMessage 拦截
    /// </summary>
    [Description("HttpResponseMessage 拦截")]
    Response,

    /// <summary>
    /// 异常拦截
    /// </summary>
    [Description("异常拦截")]
    Exception
}