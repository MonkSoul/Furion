namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 基于数据库架构的多租户模式
    /// </summary>
    public interface IMultiTenantOnSchema : IPrivateMultiTenant
    {
        /// <summary>
        /// 获取数据库架构名称
        /// </summary>
        /// <returns></returns>
        string GetSchemaName();
    }
}