using Fur.DatabaseAccessor.Contexts.Identifiers;
using Fur.DatabaseAccessor.Models.Entities;

namespace Fur.DatabaseAccessor.Repositories.MasterSlave
{
    /// <summary>
    /// 主从同步/读写分离仓储接口
    /// </summary>
    public interface IRepository
    {
        #region 获取主从同步/读写分离仓储接口 + IMasterSlaveRepositoryOfT<TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier> Set<TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier>(bool newScope = false) where TEntity : class, IDbEntity, new() where TMasterDbContextIdentifier : IDbContextIdentifier where TSlaveDbContextIdentifier : IDbContextIdentifier

        /// <summary>
        /// 获取主从同步/读写分离仓储接口
        /// </summary>
        /// <typeparam name="TEntity">实体</typeparam>
        /// <typeparam name="TMasterDbContextIdentifier">主库数据库上下文标识类</typeparam>
        /// <typeparam name="TSlaveDbContextIdentifier">从库数据库上下文标识类</typeparam>
        /// <param name="newScope">是否创建新实例</param>
        /// <returns><see cref="IRepositoryOfT{TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier}"/></returns>
        IRepositoryOfT<TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier> Set<TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier>(bool newScope = false)
            where TEntity : class, IDbEntityBase, new()
            where TMasterDbContextIdentifier : IDbContextIdentifier
            where TSlaveDbContextIdentifier : IDbContextIdentifier;

        #endregion 获取主从同步/读写分离仓储接口 + IMasterSlaveRepositoryOfT<TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier> Set<TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier>(bool newScope = false) where TEntity : class, IDbEntity, new() where TMasterDbContextIdentifier : IDbContextIdentifier where TSlaveDbContextIdentifier : IDbContextIdentifier
    }
}