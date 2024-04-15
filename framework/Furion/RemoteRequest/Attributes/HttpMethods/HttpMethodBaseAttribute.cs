// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

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