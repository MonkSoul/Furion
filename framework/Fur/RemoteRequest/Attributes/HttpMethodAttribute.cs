using Fur.DependencyInjection;
using System;

namespace Fur.RemoteRequest
{
    /// <summary>
    /// HTTP 请求谓词基类
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method)]
    public class HttpMethodAttribute : Attribute
    {
        /// <summary>
        /// 远程地址
        /// </summary>
        public string Url { get; set; }
    }
}