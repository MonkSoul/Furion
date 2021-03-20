using Furion.DependencyInjection;
using System;
using System.Net.Http;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 请求方法基类
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method)]
    public class HttpMethodBaseAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="method"></param>
        public HttpMethodBaseAttribute(string requestUrl, HttpMethod method)
        {
            RequestUrl = requestUrl;
            Method = method;
        }

        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// 请求谓词
        /// </summary>
        public HttpMethod Method { get; set; }
    }
}