using Furion.DependencyInjection;
using System;

namespace Furion.Mvc.Audits
{
    /// <summary>
    /// 排除审计敏感数据
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
    public sealed class DisableAuditingAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DisableAuditingAttribute()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="placeholder"></param>
        public DisableAuditingAttribute(string placeholder)
        {
            Placeholder = placeholder;
        }

        /// <summary>
        /// 占位符
        /// </summary>
        public string Placeholder { get; set; }
    }
}