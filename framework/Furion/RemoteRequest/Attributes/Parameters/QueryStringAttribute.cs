using Furion.DependencyInjection;
using System;

namespace Furion.RemoteRequest
{
    /// <summary>
    /// 配置查询参数
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Parameter)]
    public class QueryStringAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryStringAttribute()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="alias"></param>
        public QueryStringAttribute(string alias)
        {
            Alias = alias;
        }

        /// <summary>
        /// 参数别名
        /// </summary>
        public string Alias { get; set; }
    }
}