using Furion.DependencyInjection;
using System;

namespace Furion.DynamicApiController
{
    /// <summary>
    /// 将 Action 所有参数 [FromQuery] 化
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method)]
    public sealed class QueryParametersAttribute : Attribute
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public QueryParametersAttribute()
        {
        }
    }
}