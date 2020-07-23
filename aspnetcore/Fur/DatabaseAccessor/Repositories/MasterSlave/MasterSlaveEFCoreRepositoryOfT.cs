using Fur.DatabaseAccessor.Identifiers;
using Fur.DatabaseAccessor.Models.Entities;
using Fur.DatabaseAccessor.Repositories.Multiples;

namespace Fur.DatabaseAccessor.Repositories.MasterSlave
{
    /// <summary>
    /// 泛型 主从同步/读写分离仓储实例
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TMasterDbContextIdentifier">主库数据库上下文标识类</typeparam>
    /// <typeparam name="TSlaveDbContextIdentifier">从库数据库上下文标识类</typeparam>
    public class MasterSlaveEFCoreRepositoryOfT<TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier> : IMasterSlaveRepositoryOfT<TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier>
        where TEntity : class, IDbEntity, new()
        where TMasterDbContextIdentifier : IDbContextIdentifier
        where TSlaveDbContextIdentifier : IDbContextIdentifier
    {
        public MasterSlaveEFCoreRepositoryOfT(
            IMultipleRepositoryOfT<TEntity, TMasterDbContextIdentifier> masterRepository
            , IMultipleRepositoryOfT<TEntity, TSlaveDbContextIdentifier> slaveRepository)
        {
            Master = masterRepository;
            Slave = slaveRepository;
        }

        /// <summary>
        /// 主库
        /// </summary>
        public IMultipleRepositoryOfT<TEntity, TMasterDbContextIdentifier> Master { get; }

        /// <summary>
        /// 从库
        /// </summary>
        public IMultipleRepositoryOfT<TEntity, TSlaveDbContextIdentifier> Slave { get; }
    }
}