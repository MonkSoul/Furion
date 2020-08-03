using Fur.AppCore.Attributes;
using Fur.DatabaseAccessor.Contexts.Locators;

namespace Fur.DatabaseAccessor.MultipleTenants
{
    /// <summary>
    /// 多租户数据库上下文定位器
    /// </summary>
    [NonInflated]
    public class FurMultipleTenanDbContextLocator : IDbContextLocator { }
}