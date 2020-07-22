using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Identifiers;

namespace Fur.DatabaseVisitor.Repositories.MasterSlave
{
    /// <summary>
    /// 主从同步/读写分离仓储接口
    /// </summary>
    public interface IMasterSlaveRepository
    {
        #region 获取主从同步/读写分离仓储接口 + IMasterSlaveRepositoryOfT<TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier> Set<TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier>(bool newScope = false) where TEntity : class, IDbEntity, new() where TMasterDbContextIdentifier : IDbContextIdentifier where TSlaveDbContextIdentifier : IDbContextIdentifier

        /// <summary>
        /// 获取主从同步/读写分离仓储接口
        /// </summary>
        /// <typeparam name="TEntity">实体</typeparam>
        /// <typeparam name="TMasterDbContextIdentifier">主库数据库上下文标识类</typeparam>
        /// <typeparam name="TSlaveDbContextIdentifier">从库数据库上下文标识类</typeparam>
        /// <param name="newScope">是否创建新实例</param>
        /// <returns><see cref="IMasterSlaveRepositoryOfT{TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier}"/></returns>
        IMasterSlaveRepositoryOfT<TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier> Set<TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier>(bool newScope = false)
            where TEntity : class, IDbEntity, new()
            where TMasterDbContextIdentifier : IDbContextIdentifier
            where TSlaveDbContextIdentifier : IDbContextIdentifier;

        #endregion 获取主从同步/读写分离仓储接口 + IMasterSlaveRepositoryOfT<TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier> Set<TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier>(bool newScope = false) where TEntity : class, IDbEntity, new() where TMasterDbContextIdentifier : IDbContextIdentifier where TSlaveDbContextIdentifier : IDbContextIdentifier
    }
}