// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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