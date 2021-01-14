using Furion.DependencyInjection;
using System;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 客户端配置
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = false)]
    public sealed class ClientAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ClientAttribute()
        {
        }

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