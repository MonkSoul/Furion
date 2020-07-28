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
    public interface IRepositoryOfT<TEntity, TMasterDbContextLocator, TSlaveDbContextLocator>
        where TEntity : class, IDbEntityBase, new()
        where TMasterDbContextLocator : IDbContextLocator
        where TSlaveDbContextLocator : IDbContextLocator
    {
        /// <summary>
        /// 主库
        /// </summary>
        IRepositoryOfT<TEntity, TMasterDbContextLocator> Master { get; }

        /// <summary>
        /// 从库
        /// </summary>
        IRepositoryOfT<TEntity, TSlaveDbContextLocator> Slave { get; }
    }
}