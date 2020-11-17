using Furion.DependencyInjection;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 默认数据库上下文定位器
    /// </summary>
    [SkipScan]
    public sealed class MasterDbContextLocator : IDbContextLocator
    {
    }
}