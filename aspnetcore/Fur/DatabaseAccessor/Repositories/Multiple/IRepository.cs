using Fur.DatabaseAccessor.Contexts.Locators;
using Fur.DatabaseAccessor.Models.Entities;

namespace Fur.DatabaseAccessor.Repositories.Multiple
{
    /// <summary>
    /// 泛型多上下文仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文标识</typeparam>
    public partial interface IRepository<TEntity, TDbContextLocator> : Repositories.IRepository<TEntity>
        where TEntity : class, IDbEntityBase, new()
        where TDbContextLocator : IDbContextLocator
    {
    }

    /// <summary>
    /// 多上下文仓储接口
    /// <para>也就是可以支持多个 <see cref="Microsoft.EntityFrameworkCore.DbContext"/> 的仓储</para>
    /// </summary>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    public partial interface IRepository<TDbContextLocator>
        where TDbContextLocator : IDbContextLocator
    {
        #region 获取泛型多上下文仓储接口 +  IRepository<TEntity, TDbContextLocator> Set<TEntity>(bool newScope = false)
        /// <summary>
        /// 获取泛型多上下文仓储接口
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="newScope">如果为false，则从服务容器中读取一个对象，没有就创建。如果设置为true，则每次都会创建新的实例</param>
        /// <returns><see cref="IRepository{TEntity, TDbContextLocator}"/></returns>
        IRepository<TEntity, TDbContextLocator> Set<TEntity>(bool newScope = false)
            where TEntity : class, IDbEntityBase, new();
        #endregion
    }
}