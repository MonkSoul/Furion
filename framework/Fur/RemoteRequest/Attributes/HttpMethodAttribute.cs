using Fur.DependencyInjection;
using System;
using System.Net.Http;

namespace Fur.RemoteRequest
{
    /// <summary>
    /// HTTP 请求谓词基类
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class HttpMethodAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="url"></param>
        public HttpMethodAttribute(string url)
        {
            Url = url;
        }

        /// <summary>
        /// 远程地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 指定客户端命名
        /// </summary>
        public string Client { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public HttpMethod Method { get; protected set; }
    }
}