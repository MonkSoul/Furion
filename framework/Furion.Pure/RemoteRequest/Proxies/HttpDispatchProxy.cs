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

using Furion.ClayObject.Extensions;
using Furion.DataValidation;
using Furion.Extensions;
using Furion.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace Furion.RemoteRequest;

/// <summary>
/// 远程请求实现类（以下代码还需进一步优化性能，启动时把所有扫描缓存起来）
/// </summary>
[SuppressSniffer]
public class HttpDispatchProxy : AspectDispatchProxy, IDispatchProxy
{
    /// <summary>
    /// 被代理对象
    /// </summary>
    public object Target { get; set; }

    /// <summary>
    /// 服务提供器
    /// </summary>
    public IServiceProvider Services { get; set; }

    /// <summary>
    /// 拦截同步方法
    /// </summary>
    /// <param name="method"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public override object Invoke(MethodInfo method, object[] args)
    {
        throw new NotSupportedException("Please use asynchronous operation mode.");
    }

    /// <summary>
    /// 拦截异步无返回方法
    /// </summary>
    /// <param name="method"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async override Task InvokeAsync(MethodInfo method, object[] args)
    {
        var httpRequestPart = BuildHttpRequestPart(method, args);
        _ = await httpRequestPart.SendAsync();
    }

    /// <summary>
    /// 拦截异步带返回方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="method"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public override Task<T> InvokeAsyncT<T>(MethodInfo method, object[] args)
    {
        var httpRequestPart = BuildHttpRequestPart(method, args);
        var result = httpRequestPart.SendAsAsync<T>();
        return result;
    }

    /// <summary>
    /// 构建 HttpClient 请求部件
    /// </summary>
    /// <param name="method"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    private HttpRequestPart BuildHttpRequestPart(MethodInfo method, object[] args)
    {
        // 判断方法是否是远程代理请求方法
        if (!method.IsDefined(typeof(HttpMethodBaseAttribute), true)) throw new InvalidOperationException($"{method.Name} is not a valid request proxy method.");

        // 解析方法参数及参数值
        var parameters = method.GetParameters().Select((u, i) => new MethodParameterInfo
        {
            Parameter = u,
            Name = u.Name,
            Value = args[i]
        });

        // 获取请求配置
        var httpMethodBase = method.GetCustomAttribute<HttpMethodBaseAttribute>(true);

        // 创建请求配置对象
        var httpRequestPart = new HttpRequestPart();
        httpRequestPart.SetRequestUrl(httpMethodBase.RequestUrl)
                      .SetContentType(httpMethodBase.ContentType)
                      .SetHttpVersion(httpMethodBase.HttpVersion)
                      .SetContentEncoding(Encoding.GetEncoding(httpMethodBase.Encoding))
                      .SetHttpMethod(httpMethodBase.Method)
                      .SetTemplates(parameters.ToDictionary(u => u.Name, u => u.Value))
                      .SetRequestScoped(Services)
                      .WithGZip(httpMethodBase.WithGZip)
                      .WithEncodeUrl(httpMethodBase.WithEncodeUrl);

        // 设置请求客户端
        var clientAttribute = method.GetFoundAttribute<ClientAttribute>(true);
        if (clientAttribute != null) httpRequestPart.SetClient(clientAttribute.Name);

        // 设置请求报文头
        SetHeaders(method, parameters, httpRequestPart);

        // 设置 Url 地址参数
        SetQueries(parameters, httpRequestPart, httpMethodBase.IgnoreNullValueQueries);

        // 设置 Body 信息
        SetBody(parameters, httpRequestPart);

        // 设置验证
        SetValidation(parameters);

        // 设置序列化提供器
        SetJsonSerialization(method, parameters, httpRequestPart);

        // 配置全局拦截
        CallGlobalInterceptors(httpRequestPart, method.DeclaringType);

        // 调用单个方法拦截
        CallMethodInterceptors(parameters, httpRequestPart);

        // 设置重试
        var retryPolicyAttribute = method.GetFoundAttribute<RetryPolicyAttribute>(true);
        if (retryPolicyAttribute != null) httpRequestPart.SetRetryPolicy(retryPolicyAttribute.NumRetries, retryPolicyAttribute.RetryTimeout);

        return httpRequestPart;
    }

    /// <summary>
    /// 设置 Url 地址参数
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="httpRequestPart"></param>
    /// <param name="ignoreNullValue"></param>
    private static void SetQueries(IEnumerable<MethodParameterInfo> parameters, HttpRequestPart httpRequestPart, bool ignoreNullValue)
    {
        // 配置 Url 地址参数
        var queryParameters = parameters.Where(u => u.Parameter.IsDefined(typeof(QueryStringAttribute), true));
        var parameterQueries = new Dictionary<string, object>();
        foreach (var item in queryParameters)
        {
            var queryStringAttribute = item.Parameter.GetCustomAttribute<QueryStringAttribute>();
            if (item.Value != null)
            {
                var valueType = item.Value.GetType();

                // 处理时间类型
                if (valueType == typeof(DateTime)
                    || valueType == typeof(DateTimeOffset)
                    || Nullable.GetUnderlyingType(valueType) == typeof(DateTime)    // DateTime?
                    || Nullable.GetUnderlyingType(valueType) == typeof(DateTimeOffset)) // DateTimeOffset?
                {
                    dynamic actValue = item.Value;
                    parameterQueries.Add(queryStringAttribute.Alias ?? item.Name, actValue.ToString(queryStringAttribute.Format));

                    continue;
                }
                // 处理基元类型
                else if (valueType.IsRichPrimitive()
                     && (!valueType.IsArray || valueType == typeof(string)))
                {
                    parameterQueries.Add(queryStringAttribute.Alias ?? item.Name, item.Value);
                }
                // 处理集合类型
                else if (valueType.IsArray
                    || (typeof(IEnumerable).IsAssignableFrom(valueType)
                        && valueType.IsGenericType && valueType.GenericTypeArguments.Length == 1))
                {
                    var valueList = ((IEnumerable)item.Value)?.Cast<object>();
                    parameterQueries.Add(queryStringAttribute.Alias ?? item.Name, valueList);
                }
                // 处理类类型
                else
                {
                    parameterQueries.AddOrUpdate(item.Value.ToDictionary());
                }
            }
        }

        // 设置 Url 参数
        httpRequestPart.SetQueries(parameterQueries, ignoreNullValue);
    }

    /// <summary>
    /// 设置 Body 参数
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="httpRequestPart"></param>
    private static void SetBody(IEnumerable<MethodParameterInfo> parameters, HttpRequestPart httpRequestPart)
    {
        // 配置 Body 参数，只取第一个
        var bodyParameter = parameters.FirstOrDefault(u => u.Parameter.IsDefined(typeof(BodyAttribute), true));
        if (bodyParameter != null)
        {
            var bodyAttribute = bodyParameter.Parameter.GetCustomAttribute<BodyAttribute>(true);
            object value;

            // 处理值类型，基元类型
            if (bodyParameter.Parameter.ParameterType.IsValueType || bodyParameter.Parameter.ParameterType.IsPrimitive)
            {
                value = new Dictionary<string, string>
                {
                    { bodyParameter.Parameter.Name, bodyParameter.Value?.ToString()}
                };
            }
            // 对象类型
            else value = bodyParameter.Value;

            httpRequestPart.SetBody(value, bodyAttribute.ContentType, Encoding.GetEncoding(bodyAttribute.Encoding));
        }

        // 查找所有 HttpFile 和 HttpFile 集合类型的参数
        var filesParameters = parameters.Where(u => u.Parameter.ParameterType == typeof(HttpFile)
            || u.Parameter.ParameterType == typeof(HttpFile[])
            || (u.Parameter.ParameterType.IsGenericType && typeof(IEnumerable).IsAssignableFrom(u.Parameter.ParameterType) && u.Parameter.ParameterType.GenericTypeArguments[0] == typeof(HttpFile)));

        if (filesParameters != null)
        {
            var files = new List<HttpFile>();

            foreach (var item in filesParameters)
            {
                if (item.Value != null)
                {
                    // 处理 HttpFile[] 类型
                    if (item.Parameter.ParameterType.IsArray)
                    {
                        files.AddRange((HttpFile[])item.Value);
                    }
                    // 处理 IList<HttpFile> IEnumerable<HttpFile> 类型
                    else if (typeof(IEnumerable).IsAssignableFrom(item.Parameter.ParameterType))
                    {
                        files.AddRange(((IEnumerable)item.Value).Cast<HttpFile>());
                    }
                    // 处理单个类型
                    else
                    {
                        files.Add((HttpFile)item.Value);
                    }
                }
            }

            httpRequestPart.SetFiles(files.ToArray());
        }
    }

    /// <summary>
    /// 设置验证
    /// </summary>
    /// <param name="parameters"></param>
    private static void SetValidation(IEnumerable<MethodParameterInfo> parameters)
    {
        // 验证参数，查询所有配置验证特性的参数，排除 Body 验证
        var validateParameters = parameters.Where(u => u.Parameter.IsDefined(typeof(ValidationAttribute), true) && !u.Parameter.IsDefined(typeof(BodyAttribute), true));
        foreach (var item in validateParameters)
        {
            // 处理空值
            var isRequired = item.Parameter.IsDefined(typeof(RequiredAttribute), true);
            if (isRequired && item.Value == null) throw new InvalidOperationException($"{item.Name} can not be null.");

            // 判断是否是基元类型
            if (item.Parameter.ParameterType.IsRichPrimitive())
            {
                var validationAttributes = item.Parameter.GetCustomAttributes<ValidationAttribute>(true);
                item.Value?.Validate(validationAttributes.ToArray());
            }
            else item.Value?.Validate();
        }
    }

    /// <summary>
    /// 设置序列化
    /// </summary>
    /// <param name="method"></param>
    /// <param name="parameters"></param>
    /// <param name="httpRequestPart"></param>
    private static void SetJsonSerialization(MethodInfo method, IEnumerable<MethodParameterInfo> parameters, HttpRequestPart httpRequestPart)
    {
        // 判断方法是否自定义序列化选项
        var jsonSerializerOptions = parameters.FirstOrDefault(u => u.Parameter.IsDefined(typeof(JsonSerializerOptionsAttribute), true))?.Value
                                                    // 获取静态方法且贴有 [JsonSerializerOptions] 特性的缺省配置
                                                    ?? method.DeclaringType.GetMethods()
                                                                           .FirstOrDefault(u => u.IsDefined(typeof(JsonSerializerOptionsAttribute), true))
                                                                           ?.Invoke(null, null);

        // 查询自定义序列化提供器，如果没找到，默认 SystemTextJsonSerializerProvider
        var jsonSerializerProvider = method.GetFoundAttribute<JsonSerializationAttribute>(true)?.ProviderType;
        httpRequestPart.SetJsonSerialization(jsonSerializerProvider, jsonSerializerOptions);
    }

    /// <summary>
    /// 调用全局拦截
    /// </summary>
    /// <param name="httpRequestPart"></param>
    /// <param name="declaringType"></param>
    private static void CallGlobalInterceptors(HttpRequestPart httpRequestPart, Type declaringType)
    {
        // 获取所有静态方法且贴有 [Interceptor] 特性，支持查找继承接口
        var interceptorMethods = declaringType.GetInterfaces()
                                           .Where(i => i != typeof(IHttpDispatchProxy))
                                           .Concat(new[] { declaringType })
                                           .SelectMany(t => t.GetMethods()
                                                              .Where(u => u.IsDefined(typeof(InterceptorAttribute), true)));

        foreach (var method in interceptorMethods)
        {
            // 获取拦截器类型
            var interceptor = method.GetCustomAttributes<InterceptorAttribute>().First();
            switch (interceptor.Type)
            {
                // Client 创建拦截
                case InterceptorTypes.Initiate:
                    var clientProvider = (Func<HttpClient>)Delegate.CreateDelegate(typeof(Func<HttpClient>), method);
                    httpRequestPart.SetClient(clientProvider);
                    break;
                // 加载 Client 配置拦截
                case InterceptorTypes.Client:
                    var onClientCreating = (Action<HttpClient>)Delegate.CreateDelegate(typeof(Action<HttpClient>), method);
                    httpRequestPart.OnClientCreating(onClientCreating);
                    break;
                // 加载请求拦截
                case InterceptorTypes.Request:
                    var onRequesting = (Action<HttpClient, HttpRequestMessage>)Delegate.CreateDelegate(typeof(Action<HttpClient, HttpRequestMessage>), method);
                    httpRequestPart.OnRequesting(onRequesting);
                    break;
                // 加载响应拦截
                case InterceptorTypes.Response:
                    var onResponsing = (Action<HttpClient, HttpResponseMessage>)Delegate.CreateDelegate(typeof(Action<HttpClient, HttpResponseMessage>), method);
                    httpRequestPart.OnResponsing(onResponsing);
                    break;
                // 加载异常拦截
                case InterceptorTypes.Exception:
                    var onException = (Action<HttpClient, HttpResponseMessage, string>)Delegate.CreateDelegate(typeof(Action<HttpClient, HttpResponseMessage, string>), method);
                    httpRequestPart.OnException(onException);
                    break;

                default: break;
            }
        }
    }

    /// <summary>
    /// 调用单个方法拦截
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="httpRequestPart"></param>
    private static void CallMethodInterceptors(IEnumerable<MethodParameterInfo> parameters, HttpRequestPart httpRequestPart)
    {
        // 添加方法拦截器
        var Interceptors = parameters.Where(u => u.Parameter.IsDefined(typeof(InterceptorAttribute), true));
        foreach (var item in Interceptors)
        {
            // 获取拦截器类型
            var interceptor = item.Parameter.GetCustomAttribute<InterceptorAttribute>();
            switch (interceptor.Type)
            {
                // Client 创建拦截
                case InterceptorTypes.Initiate:
                    if (item.Value is Func<HttpClient> clientProvider)
                    {
                        httpRequestPart.SetClient(clientProvider);
                    }
                    break;
                // 加载 Client 配置拦截
                case InterceptorTypes.Client:
                    if (item.Value is Action<HttpClient> onClientCreating)
                    {
                        httpRequestPart.OnClientCreating(onClientCreating);
                    }
                    break;
                // 加载请求拦截
                case InterceptorTypes.Request:
                    if (item.Value is Action<HttpClient, HttpRequestMessage> onRequesting)
                    {
                        httpRequestPart.OnRequesting(onRequesting);
                    }
                    break;
                // 加载响应拦截
                case InterceptorTypes.Response:
                    if (item.Value is Action<HttpClient, HttpResponseMessage> onResponsing)
                    {
                        httpRequestPart.OnResponsing(onResponsing);
                    }
                    break;
                // 加载异常拦截
                case InterceptorTypes.Exception:
                    if (item.Value is Action<HttpClient, HttpResponseMessage, string> onException)
                    {
                        httpRequestPart.OnException(onException);
                    }
                    break;

                default: break;
            }
        }
    }

    /// <summary>
    /// 设置请求报文头
    /// </summary>
    /// <param name="method"></param>
    /// <param name="parameters"></param>
    /// <param name="httpRequestPart"></param>
    private static void SetHeaders(MethodInfo method, IEnumerable<MethodParameterInfo> parameters, HttpRequestPart httpRequestPart)
    {
        var declaringType = method.DeclaringType;

        // 获取声明类请求报文头
        var declaringTypeHeaders = (declaringType.IsDefined(typeof(HeadersAttribute), true)
            ? declaringType.GetCustomAttributes<HeadersAttribute>(true)
            : Array.Empty<HeadersAttribute>()).ToDictionary(u => u.Key, u => u.Value);

        // 获取方法请求报文头
        var methodHeaders = (method.IsDefined(typeof(HeadersAttribute), true)
            ? method.GetCustomAttributes<HeadersAttribute>(true)
            : Array.Empty<HeadersAttribute>()).ToDictionary(u => u.Key, u => u.Value);

        // 获取参数请求报文头
        var headerParameters = parameters.Where(u => u.Parameter.IsDefined(typeof(HeadersAttribute), true));
        var parameterHeaders = new Dictionary<string, object>();
        foreach (var item in headerParameters)
        {
            var headersAttribute = item.Parameter.GetCustomAttribute<HeadersAttribute>(true);
            if (item.Value != null)
            {
                // 处理参数传入 [Headers] IDictionary<string, object>  情况
                if (item.Value is IDictionary<string, object> dicHeaders)
                {
                    foreach (var (key, value) in dicHeaders)
                    {
                        parameterHeaders.Add(key, value);
                    }
                }
                else
                {
                    parameterHeaders.Add(headersAttribute.Key ?? item.Name, item.Value);
                }
            }
        }

        // 合并所有请求报文头
        var headers = declaringTypeHeaders.AddOrUpdate(methodHeaders)
                                                              .AddOrUpdate(parameterHeaders);
        httpRequestPart.SetHeaders(headers);
    }
}