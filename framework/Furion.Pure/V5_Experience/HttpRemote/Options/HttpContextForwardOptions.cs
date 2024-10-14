// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Microsoft.AspNetCore.Http;

namespace Furion.HttpRemote;

/// <summary>
///     <see cref="HttpContext" /> 转发配置选项
/// </summary>
public sealed class HttpContextForwardOptions
{
    /// <summary>
    ///     是否转发响应状态码
    /// </summary>
    /// <remarks>默认值为：<c>true</c>。</remarks>
    public bool ForwardStatusCode { get; set; } = true;

    /// <summary>
    ///     是否转发响应标头
    /// </summary>
    /// <remarks>默认值为：<c>true</c>。</remarks>
    public bool ForwardResponseHeaders { get; set; } = true;

    /// <summary>
    ///     是否转发响应内容 <c>Content-Disposition</c> 标头
    /// </summary>
    public bool ForwardContentDispositionHeader { get; set; } = true;

    /// <summary>
    ///     用于在转发响应之前执行自定义操作
    /// </summary>
    public Action<HttpContext, HttpResponseMessage>? OnForwarding { get; set; }
}