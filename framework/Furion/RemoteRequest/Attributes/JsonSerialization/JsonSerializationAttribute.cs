using Furion.DependencyInjection;
using System;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// JSON 序列化提供器
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JsonSerializationAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="providerType"></param>
        public JsonSerializationAttribute(Type providerType)
        {
            ProviderType = providerType;
        }

        /// <summary>
        /// 提供器类型
        /// </summary>
        public Type ProviderType { get; set; }
    }
}