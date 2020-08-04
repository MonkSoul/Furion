namespace Fur.DatabaseAccessor.Providers
{
    public interface IMultipleTenantOnDatabaseProvider : IMultipleTenantProvider
    {
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <returns>连接字符串</returns>
        string GetConnectionString();
    }
}