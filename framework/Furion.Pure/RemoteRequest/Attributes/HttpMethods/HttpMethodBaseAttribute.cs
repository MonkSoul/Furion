// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.RemoteRequest;

/// <summary>
/// 请求方法基类
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Method)]
public class HttpMethodBaseAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="method"></param>
    public HttpMethodBaseAttribute(HttpMethod method)
    {
        Method = method;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="requestUrl"></param>
    /// <param name="method"></param>
    public HttpMethodBaseAttribute(string requestUrl, HttpMethod method)
        : this(method)
    {
        RequestUrl = requestUrl;
    }

    /// <summary>
    /// 请求地址
    /// </summary>
    public string RequestUrl { get; set; }

    /// <summary>
    /// 请求谓词
    /// </summary>
    public HttpMethod Method { get; set; }

    /// <summary>
    /// 支持 GZip 压缩
    /// </summary>
    public bool WithGZip { get; set; } = false;

    /// <summary>
    /// 内容类型
    /// </summary>
    public string ContentType { get; set; } = "application/json";

    /// <summary>
    /// 内容编码
    /// </summary>
    public string Encoding { get; set; } = "UTF-8";

    /// <summary>
    /// 是否对 Url 进行 Uri.EscapeDataString
    /// </summary>
    public bool WithEncodeUrl { get; set; } = true;

    /// <summary>
    /// 忽略值为 null 的 Url 参数
    /// </summary>
    public bool IgnoreNullValueQueries { get; set; }

    /// <summary>
    /// Http 版本
    /// </summary>
    public string HttpVersion { get; set; } = "1.1";
}