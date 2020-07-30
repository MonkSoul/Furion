namespace Fur.DatabaseAccessor.MultipleTenants.Options
{
    /// <summary>
    /// 多租户配置选项
    /// </summary>
    public enum FurMultipleTenantOptions
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