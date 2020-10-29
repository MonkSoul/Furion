namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 基于多个数据库多租户模式
    /// </summary>
    public interface IMultiTenantOnDatabase : IPrivateMultiTenant
    {
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <returns></returns>
        string GetDatabaseConnectionString();
    }
}