using Autofac;
using Fur.DatabaseAccessor.Contexts.Locators;
using Fur.DatabaseAccessor.Models.Entities;

namespace Fur.DatabaseAccessor.Repositories.MasterSlave
{
    /// <summary>
    /// 泛型 主从同步/读写分离仓储实例
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TMasterDbContextLocator">主库数据库上下文定位器</typeparam>
    /// <typeparam name="TSlaveDbContextLocator">从库数据库上下文定位器</typeparam>
    public class EFCoreRepository<TEntity, TMasterDbContextLocator, TSlaveDbContextLocator> : IRepository<TEntity, TMasterDbContextLocator, TSlaveDbContextLocator>
        where TEntity : class, IDbEntityBase, new()
        where TMasterDbContextLocator : IDbContextLocator
        where TSlaveDbContextLocator : IDbContextLocator
    {
        public EFCoreRepository(
            Multiple.IRepository<TEntity, TMasterDbContextLocator> masterRepository
            , Multiple.IRepository<TEntity, TSlaveDbContextLocator> slaveRepository)
        {
            Master = masterRepository;
            Slave = slaveRepository;
        }

        /// <summary>
        /// 主库
        /// </summary>
        public Multiple.IRepository<TEntity, TMasterDbContextLocator> Master { get; }

        /// <summary>
        /// 从库
        /// </summary>
        public Multiple.IRepository<TEntity, TSlaveDbContextLocator> Slave { get; }
    }

    /// <summary>
    /// 主从同步/读写分离仓储实例
    /// </summary>
    public class EFCoreRepository<TMasterDbContextLocator, TSlaveDbContextLocator> : IRepository<TMasterDbContextLocator, TSlaveDbContextLocator>
        where TMasterDbContextLocator : IDbContextLocator
        where TSlaveDbContextLocator : IDbContextLocator
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
        public EFCoreRepository(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        #endregion

        #region 获取主从同步/读写分离仓储接口 +public IRepository<TEntity, TMasterDbContextLocator, TSlaveDbContextLocator> Set<TEntity>(bool newScope = false)
        /// <summary>
        /// 获取主从同步/读写分离仓储接口
        /// </summary>
        /// <typeparam name="TEntity">实体</typeparam>
        /// <param name="newScope">是否创建新实例</param>
        /// <returns><see cref="IRepository{TEntity, TMasterDbContextLocator, TSlaveDbContextLocator}"/></returns>
        public IRepository<TEntity, TMasterDbContextLocator, TSlaveDbContextLocator> Set<TEntity>(bool newScope = false)
              where TEntity : class, IDbEntityBase, new()
        {
            if (newScope)
            {
                return _lifetimeScope.BeginLifetimeScope().Resolve<IRepository<TEntity, TMasterDbContextLocator, TSlaveDbContextLocator>>();
            }
            return _lifetimeScope.Resolve<IRepository<TEntity, TMasterDbContextLocator, TSlaveDbContextLocator>>();
        }

        #endregion
    }
}