using Fur.DependencyInjection;
using System;
using System.Net.Http;

namespace Fur.RemoteRequest
{
    /// <summary>
    /// HttpDelete 请求
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DeleteAttribute : HttpMethodAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="url"></param>
        public DeleteAttribute(string url) : base(url)
        {
            base.Method = HttpMethod.Delete;
        }
    }
}