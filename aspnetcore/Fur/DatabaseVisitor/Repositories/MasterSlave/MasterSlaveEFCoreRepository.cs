using Autofac;
using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Identifiers;

namespace Fur.DatabaseVisitor.Repositories.MasterSlave
{
    /// <summary>
    /// 主从同步/读写分离仓储实例
    /// </summary>
    public class MasterSlaveEFCoreRepository : IMasterSlaveRepository
    {
        /// <summary>
        /// Autofac生命周期对象
        /// </summary>
        private readonly ILifetimeScope _lifetimeScope;

        #region 构造函数 + public MasterSlaveEFCoreRepository(ILifetimeScope lifetimeScope)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider">Autofac生命周期对象</param>
        public MasterSlaveEFCoreRepository(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }
        #endregion

        #region 获取主从同步/读写分离仓储接口 +public IMasterSlaveRepositoryOfT<TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier> Set<TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier>(bool newScope = false) where TEntity : class, IDbEntity, new() where TMasterDbContextIdentifier : IDbContextIdentifier where TSlaveDbContextIdentifier : IDbContextIdentifier
        /// <summary>
        /// 获取主从同步/读写分离仓储接口
        /// </summary>
        /// <typeparam name="TEntity">实体</typeparam>
        /// <typeparam name="TMasterDbContextIdentifier">主库数据库上下文标识类</typeparam>
        /// <typeparam name="TSlaveDbContextIdentifier">从库数据库上下文标识类</typeparam>
        /// <param name="newScope">是否创建新实例</param>
        /// <returns><see cref="IMasterSlaveRepositoryOfT{TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier}"/></returns>
        public IMasterSlaveRepositoryOfT<TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier> Set<TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier>(bool newScope = false)
              where TEntity : class, IDbEntity, new()
              where TMasterDbContextIdentifier : IDbContextIdentifier
              where TSlaveDbContextIdentifier : IDbContextIdentifier
        {
            if (newScope)
            {
                return _lifetimeScope.BeginLifetimeScope().Resolve<IMasterSlaveRepositoryOfT<TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier>>();
            }
            return _lifetimeScope.Resolve<IMasterSlaveRepositoryOfT<TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier>>();
        }
        #endregion
    }
}
