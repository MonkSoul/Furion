using Fur.DatabaseAccessor.Contexts.Locators;
using Fur.DatabaseAccessor.Models.Entities;
using Fur.DatabaseAccessor.Repositories.Multiple;

namespace Fur.DatabaseAccessor.Repositories.MasterSlave
{
    /// <summary>
    /// 泛型 主从同步/读写分离仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TMasterDbContextLocator">主库数据库上下文定位器</typeparam>
    /// <typeparam name="TSlaveDbContextLocator">从库数据库上下文定位器</typeparam>
    public interface IRepository<TEntity, TMasterDbContextLocator, TSlaveDbContextLocator>
        where TEntity : class, IDbEntityBase, new()
        where TMasterDbContextLocator : IDbContextLocator
        where TSlaveDbContextLocator : IDbContextLocator
    {
        /// <summary>
        /// 主库
        /// </summary>
        Multiple.IRepository<TEntity, TMasterDbContextLocator> Master { get; }

        /// <summary>
        /// 从库
        /// </summary>
        Multiple.IRepository<TEntity, TSlaveDbContextLocator> Slave { get; }
    }

    /// <summary>
    /// 主从同步/读写分离仓储接口
    /// </summary>
    /// <typeparam name="TMasterDbContextLocator">主库数据库上下文定位器</typeparam>
    /// <typeparam name="TSlaveDbContextLocator">从库数据库上下文定位器</typeparam>
    public interface IRepository<TMasterDbContextLocator, TSlaveDbContextLocator>
        where TMasterDbContextLocator : IDbContextLocator
        where TSlaveDbContextLocator : IDbContextLocator
    {
        #region 获取主从同步/读写分离仓储接口 + IRepository<TEntity, TMasterDbContextLocator, TSlaveDbContextLocator> Set<TEntity>(bool newScope = false)
        /// <summary>
        /// 获取主从同步/读写分离仓储接口
        /// </summary>
        /// <typeparam name="TEntity">实体</typeparam>
        /// <param name="newScope">是否创建新实例</param>
        /// <returns><see cref="IRepository{TEntity, TMasterDbContextLocator, TSlaveDbContextLocator}"/></returns>
        IRepository<TEntity, TMasterDbContextLocator, TSlaveDbContextLocator> Set<TEntity>(bool newScope = false)
            where TEntity : class, IDbEntityBase, new();
        #endregion
    }
}