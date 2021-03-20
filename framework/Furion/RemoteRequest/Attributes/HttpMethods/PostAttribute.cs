using Furion.DependencyInjection;
using System;
using System.Net.Http;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// HttpPost 请求
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method)]
    public class PostAttribute : HttpMethodBaseAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestUrl"></param>
        public PostAttribute(string requestUrl) : base(requestUrl, HttpMethod.Post)
        {
        }
    }
}