using Fur.DatabaseAccessor.Contexts.Locators;
using Fur.DatabaseAccessor.Models.Entities;

namespace Fur.DatabaseAccessor.Repositories.Multiple
{
    /// <summary>
    /// 泛型多上下文仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文标识</typeparam>
    public partial interface IRepositoryOfT<TEntity, TDbContextLocator> : IRepositoryOfT<TEntity>
        where TEntity : class, IDbEntityBase, new()
        where TDbContextLocator : IDbContextLocator
    {
    }
}