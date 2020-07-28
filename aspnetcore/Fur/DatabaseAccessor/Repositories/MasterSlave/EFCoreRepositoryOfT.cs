using Fur.DatabaseAccessor.Contexts.Locators;
using Fur.DatabaseAccessor.Models.Entities;
using Fur.DatabaseAccessor.Repositories.Multiple;

namespace Fur.DatabaseAccessor.Repositories.MasterSlave
{
    /// <summary>
    /// 泛型 主从同步/读写分离仓储实例
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TMasterDbContextLocator">主库数据库上下文定位器</typeparam>
    /// <typeparam name="TSlaveDbContextLocator">从库数据库上下文定位器</typeparam>
    public class EFCoreRepositoryOfT<TEntity, TMasterDbContextLocator, TSlaveDbContextLocator> : IRepositoryOfT<TEntity, TMasterDbContextLocator, TSlaveDbContextLocator>
        where TEntity : class, IDbEntityBase, new()
        where TMasterDbContextLocator : IDbContextLocator
        where TSlaveDbContextLocator : IDbContextLocator
    {
        public EFCoreRepositoryOfT(
            IRepositoryOfT<TEntity, TMasterDbContextLocator> masterRepository
            , IRepositoryOfT<TEntity, TSlaveDbContextLocator> slaveRepository)
        {
            Master = masterRepository;
            Slave = slaveRepository;
        }

        /// <summary>
        /// 主库
        /// </summary>
        public IRepositoryOfT<TEntity, TMasterDbContextLocator> Master { get; }

        /// <summary>
        /// 从库
        /// </summary>
        public IRepositoryOfT<TEntity, TSlaveDbContextLocator> Slave { get; }
    }
}