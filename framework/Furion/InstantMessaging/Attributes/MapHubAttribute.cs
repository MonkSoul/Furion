using Furion.DependencyInjection;
using System;

namespace Furion.InstantMessaging
{
    /// <summary>
    /// 即时通信集线器配置特性
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Class)]
    public sealed class MapHubAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pattern"></param>
        public MapHubAttribute(string pattern)
        {
            Pattern = pattern;
        }

        /// <summary>
        /// 配置终点路由地址
        /// </summary>
        public string Pattern { get; set; }
    }
}