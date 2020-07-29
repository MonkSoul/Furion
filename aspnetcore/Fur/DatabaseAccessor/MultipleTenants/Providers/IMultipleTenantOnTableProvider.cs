using System;

namespace Fur.DatabaseAccessor.MultipleTenants.Providers
{
    /// <summary>
    /// 基于表的多租户实现提供器
    /// </summary>
    public interface IMultipleTenantOnTableProvider : IMultipleTenantProvider
    {
        /// <summary>
        /// 获取多租户Id
        /// </summary>
        /// <returns>多租户Id</returns>
        Guid GetTenantId();
    }
}
