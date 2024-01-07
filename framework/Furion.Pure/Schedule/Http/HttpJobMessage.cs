// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Text.Json.Serialization;

namespace Furion.Schedule;

/// <summary>
/// HTTP 作业消息
/// </summary>
[SuppressSniffer]
public sealed class HttpJobMessage
{
    /// <summary>
    /// 请求地址
    /// </summary>
    public string RequestUri { get; set; }

    /// <summary>
    /// 请求方法
    /// </summary>
    public HttpMethod HttpMethod { get; set; } = HttpMethod.Get;

    /// <summary>
    /// 请求头
    /// </summary>
    public Dictionary<string, string> Headers { get; set; } = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// 请求报文体
    /// </summary>
    public string Body { get; set; }

    /// <summary>
    /// 请求客户端名称
    /// </summary>
    public string ClientName { get; set; } = nameof(HttpJob);

    /// <summary>
    /// 确保请求成功，否则抛异常
    /// </summary>
    public bool EnsureSuccessStatusCode { get; set; } = true;

    /// <summary>
    /// 作业组名称
    /// </summary>
    [JsonIgnore]
    public string GroupName { get; set; }

    /// <summary>
    /// 描述信息
    /// </summary>
    [JsonIgnore]
    public string Description { get; set; }
}