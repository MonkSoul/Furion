// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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