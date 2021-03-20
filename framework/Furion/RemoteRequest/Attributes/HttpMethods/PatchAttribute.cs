using Furion.DependencyInjection;
using System;
using System.Net.Http;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// HttpPatch 请求
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method)]
    public class PatchAttribute : HttpMethodBaseAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestUrl"></param>
        public PatchAttribute(string requestUrl) : base(requestUrl, HttpMethod.Patch)
        {
        }
    }
}