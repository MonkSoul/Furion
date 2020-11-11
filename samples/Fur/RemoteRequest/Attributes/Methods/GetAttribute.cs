using Fur.DependencyInjection;
using System;
using System.Net.Http;

namespace Fur.RemoteRequest
{
    /// <summary>
    /// HttpGet 请求
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class GetAttribute : HttpMethodAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="url"></param>
        public GetAttribute(string url) : base(url)
        {
            base.Method = HttpMethod.Get;
        }
    }
}