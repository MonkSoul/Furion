using Fur.DatabaseAccessor.Contexts.Identifiers;
using Fur.DatabaseAccessor.Models.Entities;

namespace Fur.DatabaseAccessor.Repositories.Multiple
{
    /// <summary>
    /// 非泛型多上下文的仓储接口
    /// <para>也就是可以支持多个 <see cref="Microsoft.EntityFrameworkCore.DbContext"/> 的仓储</para>
    /// </summary>
    public partial interface IRepository
    {
        #region 获取泛型多上下文仓储接口 +  IMultipleDbContextRepositoryOfT<TEntity, TDbContextIdentifier> Set<TEntity, TDbContextIdentifier>(bool newScope = false) where TEntity : class, IDbEntity, new() where TDbContextIdentifier : IDbContextIdentifier

        /// <summary>
        /// 获取泛型多上下文仓储接口
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TDbContextIdentifier">数据上下文标识类。参见：<see cref="FurDbContextIdentifier"/></typeparam>
        /// <param name="newScope">如果为false，则从服务容器中读取一个对象，没有就创建。如果设置为true，则每次都会创建新的实例</param>
        /// <returns><see cref="IRepositoryOfT{TEntity, TDbContextIdentifier}"/></returns>
        IRepositoryOfT<TEntity, TDbContextIdentifier> Set<TEntity, TDbContextIdentifier>(bool newScope = false)
            where TEntity : class, IDbEntity, new()
            where TDbContextIdentifier : IDbContextIdentifier;

        #endregion 获取泛型多上下文仓储接口 +  IMultipleDbContextRepositoryOfT<TEntity, TDbContextIdentifier> Set<TEntity, TDbContextIdentifier>(bool newScope = false) where TEntity : class, IDbEntity, new() where TDbContextIdentifier : IDbContextIdentifier
    }
}