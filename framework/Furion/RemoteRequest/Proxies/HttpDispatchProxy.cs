using Furion.DataValidation;
using Furion.DependencyInjection;
using Furion.Extensions;
using Furion.Reflection;
using Furion.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 请求拦截代理
    /// </summary>
    [SkipScan]
    public class HttpDispatchProxy : AspectDispatchProxy, IDispatchProxy
    {
        /// <summary>
        /// 实例对象
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
        /// 拦截异步方法
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public override async Task InvokeAsync(MethodInfo method, object[] args)
        {
            // 验证参数
            ValidateMethodParameters(method, args);

            // 发送请求
            var (response, _) = await SendAsync(method, args);

            // 判断是否请求成功
            if (response.IsSuccessStatusCode)
            {
                // 打印成功消息
                App.PrintToMiniProfiler(Penetrates.MiniProfilerCategory, "Succeeded");
            }
            else
            {
                // 判断是否需要抛出异常
                if (method.IsDefined(typeof(SafetyAttribute), true) || method.ReflectedType.IsDefined(typeof(SafetyAttribute))) return;

                throw (await Penetrates.CreateRequestException(response));
            }
        }

        /// <summary>
        /// 拦截异步带返回值方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public override async Task<T> InvokeAsyncT<T>(MethodInfo method, object[] args)
        {
            // 验证参数
            ValidateMethodParameters(method, args);

            // 发送请求
            var (response, httpMethodAttribute) = await SendAsync(method, args);

            // 判断是否请求成功
            if (response.IsSuccessStatusCode)
            {
                // 打印成功消息
                App.PrintToMiniProfiler(Penetrates.MiniProfilerCategory, "Succeeded");

                // 处理返回值类型
                switch (httpMethodAttribute.ResponseType)
                {
                    // 对象类型或流类型
                    case ResponseType.Object:
                    case ResponseType.Stream:
                        var responseStream = await response.Content.ReadAsStreamAsync();
                        // 流类型
                        if (httpMethodAttribute.ResponseType == ResponseType.Stream) return (T)(object)responseStream;
                        // 对象类型
                        else
                        {
                            var result = await JsonSerializer.DeserializeAsync<T>(responseStream, JsonSerializerUtility.GetDefaultJsonSerializerOptions());
                            // 释放流
                            await responseStream.DisposeAsync();
                            return result;
                        }

                    // 文本类型
                    case ResponseType.Text:
                        var responseText = await response.Content.ReadAsStringAsync();
                        return (T)(object)responseText;

                    // Byte 数组类型
                    case ResponseType.ByteArray:
                        var responseByteArray = await response.Content.ReadAsByteArrayAsync();
                        return (T)(object)responseByteArray;

                    // 无效类型
                    default: throw new InvalidCastException("Invalid response type setting.");
                }
            }
            else
            {
                // 判断是否需要抛出异常
                if (method.IsDefined(typeof(SafetyAttribute), true) || method.ReflectedType.IsDefined(typeof(SafetyAttribute))) return default;

                throw (await Penetrates.CreateRequestException(response));
            }
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private async Task<(HttpResponseMessage, HttpMethodAttribute)> SendAsync(MethodInfo method, object[] args)
        {
            var (request, httpMethodAttribute) = BuildHttpRequestMessage(method, args);

            // 创建 HttpClient 对象
            var clientFactory = Services.GetService<IHttpClientFactory>();
            if (clientFactory == null) throw new InvalidOperationException("Please register for RemoteRequest service first: services.AddRemoteRequest();");

            // 获取客户端特性
            var clientAttribute = !method.IsDefined(typeof(ClientAttribute), true)
                ? (!method.ReflectedType.IsDefined(typeof(ClientAttribute), true)
                    ? default
                    : method.ReflectedType.GetCustomAttribute<ClientAttribute>(true))
                : method.GetCustomAttribute<ClientAttribute>(true);

            var httpClient = string.IsNullOrEmpty(clientAttribute?.Name)
                                        ? clientFactory.CreateClient()
                                        : clientFactory.CreateClient(clientAttribute?.Name);

            // 调用客户端请求拦截器
            var httpClientInterceptor = method.ReflectedType.GetMethod("HttpClientInterceptor");
            if (httpClientInterceptor != null)
            {
                httpClient = httpClientInterceptor.Invoke(null, new object[] { httpClient, method, args }) as HttpClient;
            }

            // 发送请求
            var response = await httpClient.SendAsync(request);

            // 调用响应拦截器
            var responseInterceptor = method.ReflectedType.GetMethod("ResponseInterceptor");
            if (responseInterceptor != null)
            {
                response = responseInterceptor.Invoke(null, new object[] { response, method, args }) as HttpResponseMessage;
            }
            return (response, httpMethodAttribute);
        }

        /// <summary>
        /// 构建 HttpRequestMessage 对象
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static (HttpRequestMessage, HttpMethodAttribute) BuildHttpRequestMessage(MethodInfo method, object[] args)
        {
            // 判断是否贴了 [HttpMethod] 特性
            if (!method.IsDefined(typeof(HttpMethodAttribute), true))
                throw new InvalidOperationException("The method is missing the [HttpMethod] annotation.");

            // 获取请求方式
            var httpMethodAttribute = method.GetCustomAttribute<HttpMethodAttribute>(true);

            // 拼接 Url 地址
            string urlAddress = CombineUrlAddress(method, httpMethodAttribute);

            // 获取方法参数
            var methodParameters = method.GetParameters()
                .Select((p, i) => new ParameterValue
                {
                    Parameter = p,
                    Value = args[i],
                    IsUrlParameter = p.IsDefined(typeof(QueryAttribute), true) || (p.ParameterType.IsRichPrimitive() && !p.IsDefined(typeof(RequestParameterAttribute), true)),
                    IsBodyParameter = p.IsDefined(typeof(BodyAttribute), true) || (!p.ParameterType.IsRichPrimitive() && !p.IsDefined(typeof(RequestParameterAttribute), true))
                }).ToDictionary(u => u.Parameter.Name, u => u);

            // 处理 Url 地址参数
            urlAddress = HandleUrlParameters(urlAddress, methodParameters);

            // 创建请求消息对象
            var request = new HttpRequestMessage(httpMethodAttribute.Method, urlAddress);

            // 设置请求头
            SetHttpRequestHeaders(method, request);

            // 设置请求体
            SetHttpRequestBody(method, httpMethodAttribute, methodParameters, request);

            // 调用请求拦截器
            var requestInterceptor = method.ReflectedType.GetMethod("RequestInterceptor");
            if (requestInterceptor != null)
            {
                request = requestInterceptor.Invoke(null, new object[] { request, method, args }) as HttpRequestMessage;
            }

            // 打印请求地址
            App.PrintToMiniProfiler(Penetrates.MiniProfilerCategory, "Beginning", $"{request.Method} {request.RequestUri}");

            // 返回
            return (request, httpMethodAttribute);
        }

        /// <summary>
        /// 拼接 Url 地址
        /// </summary>
        /// <param name="method"></param>
        /// <param name="httpMethodAttribute"></param>
        /// <returns></returns>
        private static string CombineUrlAddress(MethodInfo method, HttpMethodAttribute httpMethodAttribute)
        {
            var urlAddress = httpMethodAttribute.Url;

            // 拼接url地址
            if (!urlAddress.StartsWith("http:") && !urlAddress.StartsWith("https:"))
            {
                // 获取主机地址和端口（需要缓存）
                var hostAttribute = !method.IsDefined(typeof(HostAttribute), true)
                    ? (!method.ReflectedType.IsDefined(typeof(HostAttribute), true)
                        ? default
                        : method.ReflectedType.GetCustomAttribute<HostAttribute>(true))
                    : method.GetCustomAttribute<HostAttribute>(true);

                if (hostAttribute != null && !string.IsNullOrEmpty(hostAttribute.BaseAddress))
                {
                    urlAddress = hostAttribute.BaseAddress
                        + (hostAttribute.Port == 0 ? string.Empty : $":{hostAttribute.Port}")
                        + Regex.Replace("/" + urlAddress, @"\/{2,}", "/");
                }
            }

            return urlAddress;
        }

        /// <summary>
        /// 展开参数
        /// </summary>
        private const string objectParameterPattern = @"\{\.{3}(?<p>\w+)\}";

        /// <summary>
        /// 单个参数正则表达式
        /// </summary>
        private const string singleParameterPattern = @"\{(?<p>\w+)\}";

        /// <summary>
        /// 处理 Url 地址参数（还得处理手动处理参数情况）
        /// </summary>
        /// <param name="urlAddress"></param>
        /// <param name="methodParameters"></param>
        /// <returns></returns>
        private static string HandleUrlParameters(string urlAddress, Dictionary<string, ParameterValue> methodParameters)
        {
            // 获取所有基元类型，含数组类型
            var urlParameters = methodParameters.Where(u => u.Value.IsUrlParameter);

            // 如果没有参数，则直接返回
            if (!urlParameters.Any()) return urlAddress;

            // 处理 Url 地址参数
            if (Regex.IsMatch(urlAddress, objectParameterPattern) || Regex.IsMatch(urlAddress, singleParameterPattern))
            {
                // 获取所有匹配 {...xxx} 的参数
                var multiParameters = Regex.Matches(urlAddress, objectParameterPattern)
                                                          .Select(u => new
                                                          {
                                                              Name = u.Groups["p"].Value,
                                                              ParameterValue = methodParameters[u.Groups["p"].Value]
                                                          });

                // 获取所有匹配 {xxx} 的参数
                var singleParameters = Regex.Matches(urlAddress, singleParameterPattern)
                                                           .Select(u => new
                                                           {
                                                               Name = u.Groups["p"].Value,
                                                               ParameterValue = methodParameters[u.Groups["p"].Value]
                                                           });

                // 处理对象类型
                foreach (var parameter in multiParameters)
                {
                    var parameterValue = parameter.ParameterValue.Value;
                    var parameterType = parameterValue != null ? parameterValue.GetType() : parameter.ParameterValue.Parameter.ParameterType;

                    // 处理对象或数组类型
                    var valueItems = parameterType.IsArray
                        ? (((IList)parameterValue).Cast<object>()
                                                  .Select(u => $"{parameter.Name}={HttpUtility.UrlEncode(u == null ? string.Empty : u.ToString())}"))
                        : (parameterType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                        .Select(u => $"{u.Name}={HttpUtility.UrlEncode(u.GetValue(parameterValue) == null ? string.Empty : u.GetValue(parameterValue).ToString())}"))
                        .ToArray();

                    urlAddress = urlAddress.Replace($"{{...{parameter.Name}}}", string.Join("&", valueItems));
                }

                // 处理单个单个类型参数
                foreach (var parameter in singleParameters)
                {
                    urlAddress = urlAddress.Replace($"{{{parameter.Name}}}", HttpUtility.UrlEncode(parameter.ParameterValue.Value == null ? string.Empty : parameter.ParameterValue.Value.ToString()));
                }
            }

            return urlAddress;
        }

        /// <summary>
        /// 设置请求头
        /// </summary>
        /// <param name="method"></param>
        /// <param name="request"></param>
        private static void SetHttpRequestHeaders(MethodInfo method, HttpRequestMessage request)
        {
            // 获取请求报文头
            var headerAttributes = method.GetCustomAttributes<HeaderAttribute>(true)
                    .Union(method.ReflectedType.GetCustomAttributes<HeaderAttribute>(true));

            foreach (var headerAttribute in headerAttributes)
            {
                request.Headers.Add(headerAttribute.Key, headerAttribute.Value);
            }
        }

        /// <summary>
        /// 设置请求体
        /// </summary>
        /// <param name="method"></param>
        /// <param name="httpMethodAttribute"></param>
        /// <param name="methodParameters"></param>
        /// <param name="request"></param>
        private static void SetHttpRequestBody(MethodInfo method, HttpMethodAttribute httpMethodAttribute, Dictionary<string, ParameterValue> methodParameters, HttpRequestMessage request)
        {
            // 排除 GET/Head 请求
            if (httpMethodAttribute.Method == HttpMethod.Get || httpMethodAttribute.Method == HttpMethod.Head) return;

            // 获取所有非基元类型，该类型当作 Body 参数
            var bodyParameters = methodParameters.Where(u => u.Value.IsBodyParameter);

            if (bodyParameters.Any())
            {
                // 获取 body 参数
                var bodyArgs = bodyParameters.First().Value.Value;

                // 获取内容类型
                var mediaTypeHeaderAttribute = method.IsDefined(typeof(MediaTypeHeaderAttribute))
                    ? method.GetCustomAttribute<MediaTypeHeaderAttribute>()
                    : new MediaTypeHeaderAttribute(MediaTypeNames.Application.Json);

                Penetrates.SetHttpRequestBody(request, bodyArgs, httpMethodAttribute.HttpContentType, httpMethodAttribute.PropertyNamingPolicy, mediaTypeHeaderAttribute.Value);
            }
        }

        /// <summary>
        /// 验证方法参数
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        private static void ValidateMethodParameters(MethodInfo method, object[] args)
        {
            // 如果方法没有参数，则跳过验证
            var parameters = method.GetParameters();
            if (parameters.Length == 0) return;

            for (int i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                var parameterType = parameter.ParameterType;
                var value = args[i];

                // 判断是否是 Class 类型 或匿名类型
                if (parameterType.IsClass || parameterType.IsAnonymous())
                {
                    // 如果值为空且贴有非空特性
                    if (value == null)
                    {
                        if (parameter.IsDefined(typeof(RequiredAttribute))) throw new ArgumentNullException(parameter.Name);
                        else continue;
                    }

                    // 验证里面特性
                    var result = DataValidator.TryValidateObject(value);
                    if (!result.IsValid) throw new ArgumentException(JsonSerializerUtility.Serialize(result.ValidationResults));
                }
                // 否则验证值
                else
                {
                    if (!parameter.IsDefined(typeof(ValidationAttribute))) continue;

                    // 验证值
                    var result = DataValidator.TryValidateValue(value, parameter.GetCustomAttributes<ValidationAttribute>().ToArray());
                    if (!result.IsValid) throw new ArgumentException(JsonSerializerUtility.Serialize(result.ValidationResults));
                }
            }
        }
    }
}