namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 基于数据库表的多租户模式
    /// </summary>
    public interface IMultiTenantOnTable : IPrivateMultiTenant
    {
        /// <summary>
        /// 获取租户Id
        /// </summary>
        /// <returns></returns>
        object GetTenantId();
    }
}