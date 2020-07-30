namespace Fur.DatabaseAccessor.MultipleTenants.Providers
{
    /// <summary>
    /// 基于架构的多租户实现提供器
    /// </summary>
    public interface IMultipleTenantOnSchemaProvider : IMultipleTenantProvider
    {
        /// <summary>
        /// 获取架构名称
        /// </summary>
        /// <returns>架构名称</returns>
        string GetSchema();
    }
}