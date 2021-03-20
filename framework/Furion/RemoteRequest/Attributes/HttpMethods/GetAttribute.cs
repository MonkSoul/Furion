using Furion.DependencyInjection;
using System;
using System.Net.Http;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// HttpGet 请求
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method)]
    public class GetAttribute : HttpMethodBaseAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestUrl"></param>
        public GetAttribute(string requestUrl) : base(requestUrl, HttpMethod.Get)
        {
        }
    }
}