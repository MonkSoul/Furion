using Furion.DependencyInjection;
using System;
using System.Net.Http;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// HttpDelete 请求
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method)]
    public class DeleteAttribute : HttpMethodBaseAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestUrl"></param>
        public DeleteAttribute(string requestUrl) : base(requestUrl, HttpMethod.Delete)
        {
        }
    }
}