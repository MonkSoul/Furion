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

using Furion.JsonSerialization;
using System.Text;

namespace Furion.RemoteRequest;

/// <summary>
/// Http 请求对象组装部件
/// </summary>
public sealed partial class HttpRequestPart
{
    /// <summary>
    /// 静态缺省请求部件
    /// </summary>
    public static HttpRequestPart Default()
    {
        return new();
    }

    /// <summary>
    /// 请求地址
    /// </summary>
    public string RequestUrl { get; private set; }

    /// <summary>
    /// Url 地址模板
    /// </summary>
    public IDictionary<string, object> Templates { get; private set; }

    /// <summary>
    /// 请求方式
    /// </summary>
    public HttpMethod Method { get; private set; }

    /// <summary>
    /// 请求报文头
    /// </summary>
    public IDictionary<string, object> Headers { get; private set; }

    /// <summary>
    /// 查询参数
    /// </summary>
    public IDictionary<string, object> Queries { get; private set; }

    /// <summary>
    /// 忽略值为 null 的 Url 参数
    /// </summary>
    public bool IgnoreNullValueQueries { get; private set; }

    /// <summary>
    /// 客户端名称
    /// </summary>
    public string ClientName { get; private set; }

    /// <summary>
    /// 客户端 BaseAddress
    /// </summary>
    public string BaseAddress { get; private set; }

    /// <summary>
    /// 客户端提供者
    /// </summary>
    public Func<HttpClient> ClientProvider { get; private set; }

    /// <summary>
    /// 请求报文 Body 参数
    /// </summary>
    public object Body { get; private set; }

    /// <summary>
    /// 请求报文 Body 内容类型
    /// </summary>
    public string ContentType { get; private set; } = "application/json";

    /// <summary>
    /// 内容编码
    /// </summary>
    public Encoding ContentEncoding { get; private set; } = Encoding.UTF8;

    /// <summary>
    /// 上传文件
    /// </summary>
    public List<HttpFile> Files { get; private set; } = new();

    /// <summary>
    /// JSON 序列化提供器
    /// </summary>
    public Type JsonSerializerProvider { get; private set; } = typeof(SystemTextJsonSerializerProvider);

    /// <summary>
    /// JSON 序列化配置选项
    /// </summary>
    public object JsonSerializerOptions { get; private set; }

    /// <summary>
    /// 是否启用模型验证
    /// </summary>
    public (bool Enabled, bool IncludeNull) ValidationState { get; private set; } = (false, false);

    /// <summary>
    /// 构建请求对象拦截器
    /// </summary>
    public List<Action<HttpClient, HttpRequestMessage>> RequestInterceptors { get; private set; } = new List<Action<HttpClient, HttpRequestMessage>>();

    /// <summary>
    /// 创建客户端对象拦截器
    /// </summary>
    public List<Action<HttpClient>> HttpClientInterceptors { get; private set; } = new List<Action<HttpClient>>();

    /// <summary>
    /// 请求成功拦截器
    /// </summary>
    public List<Action<HttpClient, HttpResponseMessage>> ResponseInterceptors { get; private set; } = new List<Action<HttpClient, HttpResponseMessage>>();

    /// <summary>
    /// 请求异常拦截器
    /// </summary>
    public List<Action<HttpClient, HttpResponseMessage, string>> ExceptionInterceptors { get; private set; } = new List<Action<HttpClient, HttpResponseMessage, string>>();

    /// <summary>
    /// 设置请求作用域
    /// </summary>
    public IServiceProvider RequestScoped { get; private set; }

    /// <summary>
    /// 设置重试策略
    /// </summary>
    public (int NumRetries, int RetryTimeout)? RetryPolicy { get; private set; }

    /// <summary>
    /// 支持 GZip 压缩/反压缩
    /// </summary>
    public bool SupportGZip { get; private set; } = false;

    /// <summary>
    /// 是否对 Url 进行 Uri.EscapeDataString
    /// </summary>
    public bool EncodeUrl { get; private set; } = true;

    /// <summary>
    /// 设置 Http 请求版本
    /// </summary>
    public string HttpVersion { get; private set; } = "1.1";
}