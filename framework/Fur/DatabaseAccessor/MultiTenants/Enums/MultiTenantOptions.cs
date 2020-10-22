using Fur.DependencyInjection;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 多租户选项
    /// </summary>
    [SkipScan]
    public enum MultiTenantPattern
    {
        /// <summary>
        /// 默认选项
        /// </summary>
        None,

        /// <summary>
        /// 基于表的多租户实现方式
        /// </summary>
        OnTable,

        /// <summary>
        /// 基于架构的多租户实现方式
        /// </summary>
        OnSchema,

        /// <summary>
        /// 基于数据库的多租户实现方式
        /// </summary>
        OnDatabase
    }
}