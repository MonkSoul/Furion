namespace Fur.DatabaseAccessor.MultipleTenants.Providers
{
    /// <summary>
    /// 多租户提供器依赖接口
    /// <para>启用租户模式必须实现该接口</para>
    /// </summary>
    public interface IMultipleTenantProvider
    {
        #region 获取租户Id + int GetTenantId()
        /// <summary>
        /// 获取租户Id
        /// </summary>
        /// <returns>租户Id</returns>
        int GetTenantId();
        #endregion
    }
}