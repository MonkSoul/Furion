using Furion.DependencyInjection;
using System;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 远程请求参数拦截器
    /// </summary>
    /// <remarks>如果贴在静态方法中，则为全局拦截</remarks>
    [SkipScan, AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public class InterceptorAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type"></param>
        public InterceptorAttribute(InterceptorTypes type)
        {
            Type = type;
        }

        /// <summary>
        /// 拦截类型
        /// </summary>
        public InterceptorTypes Type { get; set; }
    }
}