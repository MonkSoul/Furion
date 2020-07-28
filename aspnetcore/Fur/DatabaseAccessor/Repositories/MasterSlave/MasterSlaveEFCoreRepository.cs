using Autofac;
using Fur.DatabaseAccessor.Contexts.Locators;
using Fur.DatabaseAccessor.Models.Entities;

namespace Fur.DatabaseAccessor.Repositories.MasterSlave
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

        #region 获取主从同步/读写分离仓储接口 +public IRepositoryOfT<TEntity, TMasterDbContextLocator, TSlaveDbContextLocator> Set<TEntity, TMasterDbContextLocator, TSlaveDbContextLocator>(bool newScope = false)
        /// <summary>
        /// 获取主从同步/读写分离仓储接口
        /// </summary>
        /// <typeparam name="TEntity">实体</typeparam>
        /// <typeparam name="TMasterDbContextLocator">主库数据库上下文定位器</typeparam>
        /// <typeparam name="TSlaveDbContextLocator">从库数据库上下文定位器</typeparam>
        /// <param name="newScope">是否创建新实例</param>
        /// <returns><see cref="IRepositoryOfT{TEntity, TMasterDbContextLocator, TSlaveDbContextLocator}"/></returns>
        public IRepositoryOfT<TEntity, TMasterDbContextLocator, TSlaveDbContextLocator> Set<TEntity, TMasterDbContextLocator, TSlaveDbContextLocator>(bool newScope = false)
              where TEntity : class, IDbEntityBase, new()
              where TMasterDbContextLocator : IDbContextLocator
              where TSlaveDbContextLocator : IDbContextLocator
        {
            if (newScope)
            {
                return _lifetimeScope.BeginLifetimeScope().Resolve<IRepositoryOfT<TEntity, TMasterDbContextLocator, TSlaveDbContextLocator>>();
            }
            return _lifetimeScope.Resolve<IRepositoryOfT<TEntity, TMasterDbContextLocator, TSlaveDbContextLocator>>();
        }

        #endregion
    }
}