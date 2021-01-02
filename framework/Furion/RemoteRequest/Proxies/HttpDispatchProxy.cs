using Furion.DependencyInjection;
using Furion.Extensions;
using Furion.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
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
        /// MiniProfiler 分类名
        /// </summary>
        private const string MiniProfilerCategory = "httpRequest";

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
            throw new NotImplementedException();
        }

        /// <summary>
        /// 拦截异步方法
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public override async Task InvokeAsync(MethodInfo method, object[] args)
        {
            // 发送请求
            var response = await SendAsync(method, args);

            // 判断是否请求成功
            if (response.IsSuccessStatusCode)
            {
                // 打印成功消息
                App.PrintToMiniProfiler(MiniProfilerCategory, "Succeeded");
            }
            else
            {
                // 读取错误数据
                var errorMessage = await response.Content.ReadAsStringAsync();

                // 打印失败消息
                App.PrintToMiniProfiler(MiniProfilerCategory, "Failed", errorMessage, isError: true);

                // 抛出请求异常
                throw new HttpRequestException(errorMessage);
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
            // 发送请求
            var response = await SendAsync(method, args);

            // 判断是否请求成功
            if (response.IsSuccessStatusCode)
            {
                // 读取数据并序列化返回
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<T>(responseStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                // 打印成功消息
                App.PrintToMiniProfiler(MiniProfilerCategory, "Succeeded");

                return result;
            }
            else
            {
                // 读取错误数据
                var errorMessage = await response.Content.ReadAsStringAsync();

                // 打印失败消息
                App.PrintToMiniProfiler(MiniProfilerCategory, "Failed", errorMessage, isError: true);

                // 抛出请求异常
                throw new HttpRequestException(errorMessage);
            }
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> SendAsync(MethodInfo method, object[] args)
        {
            var (request, httpMethodAttribute) = BuildHttpRequestMessage(method, args);

            // 创建 HttpClient 对象
            var clientFactory = Services.GetService<IHttpClientFactory>();
            var httpClient = string.IsNullOrEmpty(httpMethodAttribute.ClientName)
                                        ? clientFactory.CreateClient()
                                        : clientFactory.CreateClient(httpMethodAttribute.ClientName);

            // 发送请求
            var response = await httpClient.SendAsync(request);

            // 调用响应拦截器
            var responseInterceptor = method.ReflectedType.GetMethod("ResponseInterceptor");
            if (responseInterceptor != null)
            {
                response = responseInterceptor.Invoke(null, new[] { response }) as HttpResponseMessage;
            }
            return response;
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
            var methodParameters = method.GetParameters().Select((p, i) => new ParameterValue
            {
                Parameter = p,
                Value = args[i],
                IsUrlParameter = p.IsDefined(typeof(QueryAttribute), true) || (p.ParameterType.IsRichPrimitive() && !p.IsDefined(typeof(RequestParameterAttribute), true)),
                IsBodyParameter = p.IsDefined(typeof(BodyAttribute), true) || (!p.ParameterType.IsRichPrimitive() && !p.IsDefined(typeof(RequestParameterAttribute), true))
            }).ToDictionary(u => u.Parameter.Name, u => u);

            // 处理 Url 地址参数
            urlAddress = HandleUrlParameters(urlAddress, methodParameters);

            // 打印请求地址
            App.PrintToMiniProfiler(MiniProfilerCategory, "Beginning", $"{httpMethodAttribute.Method} {urlAddress}");

            // 创建请求消息对象
            var request = new HttpRequestMessage(httpMethodAttribute.Method, urlAddress);

            // 设置请求头
            SetHttpRequestHeaders(method, request);

            // 设置请求体
            SetHttpRequestBody(httpMethodAttribute, methodParameters, request);

            // 调用请求拦截器
            var requestInterceptor = method.ReflectedType.GetMethod("RequestInterceptor");
            if (requestInterceptor != null)
            {
                request = requestInterceptor.Invoke(null, new[] { request }) as HttpRequestMessage;
            }

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
        /// <param name="httpMethodAttribute"></param>
        /// <param name="methodParameters"></param>
        /// <param name="request"></param>
        private static void SetHttpRequestBody(HttpMethodAttribute httpMethodAttribute, Dictionary<string, ParameterValue> methodParameters, HttpRequestMessage request)
        {
            // 排除 GET/DELETE 请求
            if (httpMethodAttribute.Method == HttpMethod.Get || httpMethodAttribute.Method == HttpMethod.Delete) return;

            // 获取所有非基元类型，该类型当作 Body 参数
            var bodyParameters = methodParameters.Where(u => u.Value.IsBodyParameter);

            if (bodyParameters.Any())
            {
                var bodyJson = JsonSerializer.Serialize(bodyParameters.First().Value.Value, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                });
                request.Content = new StringContent(bodyJson, Encoding.UTF8, "application/json");

                // 打印请求地址
                App.PrintToMiniProfiler(MiniProfilerCategory, "Body", bodyJson);
            }
        }
    }
}