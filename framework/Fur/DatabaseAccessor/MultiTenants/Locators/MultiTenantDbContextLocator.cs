using Fur.DependencyInjection;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 多租户数据库上下文定位器
    /// </summary>
    [SkipScan]
    public sealed class MultiTenantDbContextLocator : IDbContextLocator
    {
    }
}