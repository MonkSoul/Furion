using Furion.DependencyInjection;
using System;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 配置请求客户端
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method)]
    public class ClientAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        public ClientAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 客户端名称
        /// </summary>
        public string Name { get; set; }
    }
}