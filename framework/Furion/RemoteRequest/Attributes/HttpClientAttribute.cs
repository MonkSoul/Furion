using Furion.DependencyInjection;
using System;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 配置请求客户端
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HttpClientAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        public HttpClientAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 客户端名称
        /// </summary>
        public string Name { get; set; }
    }
}