using Fur.ApplicationBase.Attributes;
using Fur.DatabaseAccessor.Contexts.Identifiers;

namespace Fur.DatabaseAccessor.MultipleTenants
{
    /// <summary>
    /// 多租户数据库上下文标识类
    /// </summary>
    [NonWrapper]
    public class FurMultipleTenantDbContextIdentifier : IDbContextIdentifier { }
}