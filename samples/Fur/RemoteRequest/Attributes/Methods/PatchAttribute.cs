using Fur.DependencyInjection;
using System;
using System.Net.Http;

namespace Fur.RemoteRequest
{
    /// <summary>
    /// HttpPatch 请求
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class PatchAttribute : HttpMethodAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="url"></param>
        public PatchAttribute(string url) : base(url)
        {
            base.Method = HttpMethod.Patch;
        }
    }
}