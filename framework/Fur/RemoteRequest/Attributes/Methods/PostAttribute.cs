using Fur.DependencyInjection;
using System;
using System.Net.Http;

namespace Fur.RemoteRequest
{
    /// <summary>
    /// HttpPost 请求
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class PostAttribute : HttpMethodAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="url"></param>
        public PostAttribute(string url) : base(url)
        {
            base.Method = HttpMethod.Post;
        }
    }
}