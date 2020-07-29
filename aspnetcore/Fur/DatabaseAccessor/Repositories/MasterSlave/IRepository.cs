using Fur.DatabaseAccessor.Contexts.Locators;
using Fur.DatabaseAccessor.Models.Entities;

namespace Fur.DatabaseAccessor.Repositories.MasterSlave
{
    /// <summary>
    /// 主从同步/读写分离仓储接口
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">主库数据库上下文定位器</typeparam>
    /// <typeparam name="TSlaveDbContextLocator">从库数据库上下文定位器</typeparam>
    public interface IRepository<TMasterDbContextLocator, TSlaveDbContextLocator>
        where TMasterDbContextLocator : IDbContextLocator
        where TSlaveDbContextLocator : IDbContextLocator
    {
        #region 获取主从同步/读写分离仓储接口 + IRepositoryOfT<TEntity, TMasterDbContextLocator, TSlaveDbContextLocator> Set<TEntity>(bool newScope = false)
        /// <summary>
        /// 获取主从同步/读写分离仓储接口
        /// </summary>
        /// <typeparam name="TEntity">实体</typeparam>
        /// <param name="newScope">是否创建新实例</param>
        /// <returns><see cref="IRepositoryOfT{TEntity, TMasterDbContextLocator, TSlaveDbContextLocator}"/></returns>
        IRepositoryOfT<TEntity, TMasterDbContextLocator, TSlaveDbContextLocator> Set<TEntity>(bool newScope = false)
            where TEntity : class, IDbEntityBase, new();
        #endregion
    }
}