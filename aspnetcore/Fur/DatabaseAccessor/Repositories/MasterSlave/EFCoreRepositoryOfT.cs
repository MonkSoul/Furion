using Fur.DatabaseAccessor.Contexts.Identifiers;
using Fur.DatabaseAccessor.Models.Entities;
using Fur.DatabaseAccessor.Repositories.Multiple;

namespace Fur.DatabaseAccessor.Repositories.MasterSlave
{
    /// <summary>
    /// 泛型 主从同步/读写分离仓储实例
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TMasterDbContextIdentifier">主库数据库上下文标识类</typeparam>
    /// <typeparam name="TSlaveDbContextIdentifier">从库数据库上下文标识类</typeparam>
    public class EFCoreRepositoryOfT<TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier> : IRepositoryOfT<TEntity, TMasterDbContextIdentifier, TSlaveDbContextIdentifier>
        where TEntity : class, IDbEntityBase, new()
        where TMasterDbContextIdentifier : IDbContextIdentifier
        where TSlaveDbContextIdentifier : IDbContextIdentifier
    {
        public EFCoreRepositoryOfT(
            IRepositoryOfT<TEntity, TMasterDbContextIdentifier> masterRepository
            , IRepositoryOfT<TEntity, TSlaveDbContextIdentifier> slaveRepository)
        {
            Master = masterRepository;
            Slave = slaveRepository;
        }

        /// <summary>
        /// 主库
        /// </summary>
        public IRepositoryOfT<TEntity, TMasterDbContextIdentifier> Master { get; }

        /// <summary>
        /// 从库
        /// </summary>
        public IRepositoryOfT<TEntity, TSlaveDbContextIdentifier> Slave { get; }
    }
}