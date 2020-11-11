using Fur.DependencyInjection;
using System;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// Sql 执行代理基特性
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method)]
    public class SqlProxyAttribute : Attribute
    {
        /// <summary>
        /// 数据库上下文定位器
        /// </summary>
        public Type DbContextLocator { get; set; }
    }
}