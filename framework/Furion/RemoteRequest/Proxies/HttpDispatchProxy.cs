using Furion.DependencyInjection;
using Furion.Extensions;
using Furion.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

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
        public override Task InvokeAsync(MethodInfo method, object[] args)
        {
            throw new NotImplementedException();
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
            // 获取方法真实返回值
            var returnType = method.GetMethodRealReturnType();

            // 判断是否贴了 [HttpMethod] 特性
            if (!method.IsDefined(typeof(HttpMethodAttribute), true)) throw new InvalidOperationException("The method is missing the [HttpMethod] annotation");

            // 获取请求方式
            var httpMethodAttribute = method.GetCustomAttribute<HttpMethodAttribute>(true);

            // 处理参数
            var methodParameters = method.GetParameters();
            var realUrl = httpMethodAttribute.Url;
            for (int i = 0; i < methodParameters.Length; i++)
            {
                var parameter = methodParameters[i];
                var value = args[i];
                if (value != null)
                {
                    realUrl = realUrl.Replace($"{{{parameter.Name}}}", args[i].ToString(), StringComparison.OrdinalIgnoreCase);
                }
            }

            // 创建请求消息对象
            var request = new HttpRequestMessage(httpMethodAttribute.Method, realUrl);

            // 获取请求报文头
            var headerAttributes = method.GetCustomAttributes<HeaderAttribute>(true);
            foreach (var headerAttribute in headerAttributes)
            {
                request.Headers.Add(headerAttribute.Key, headerAttribute.Value);
            }

            // 打印请求地址
            App.PrintToMiniProfiler(MiniProfilerCategory, "Beginning", $"{request.Method.Method} {request.RequestUri}");

            // 创建 HttpClient 对象
            var clientFactory = Services.GetService<IHttpClientFactory>();
            var httpClient = clientFactory.CreateClient();

            // 发送请求
            var response = await httpClient.SendAsync(request);

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

                return default;
            }
        }
    }
}