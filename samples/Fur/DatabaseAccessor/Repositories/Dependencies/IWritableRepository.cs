using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 可写仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial interface IWritableRepository<TEntity>
        : IWritableRepository<TEntity, MasterDbContextLocator>
        , IInsertableRepository<TEntity>
        , IUpdateableRepository<TEntity>
        , IDeletableRepository<TEntity>
        , IOperableRepository<TEntity>
        where TEntity : class, IPrivateEntity, new()
    {
    }

    /// <summary>
    /// 可写仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    public partial interface IWritableRepository<TEntity, TDbContextLocator>
        : IInsertableRepository<TEntity, TDbContextLocator>
        , IUpdateableRepository<TEntity, TDbContextLocator>
        , IDeletableRepository<TEntity, TDbContextLocator>
        , IOperableRepository<TEntity, TDbContextLocator>
        , IPrivateRepository
    where TEntity : class, IPrivateEntity, new()
    where TDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// 接受所有更改
        /// </summary>
        void AcceptAllChanges();

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <returns></returns>
        int SavePoolNow();

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        int SavePoolNow(bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SavePoolNowAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 保存数据库上下文池中所有已更改的数据库上下文
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SavePoolNowAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <returns></returns>
        int SaveNow();

        /// <summary>
        /// 提交更改操作
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        int SaveNow(bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 提交更改操作（异步）
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveNowAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 提交更改操作（异步）
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveNowAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}