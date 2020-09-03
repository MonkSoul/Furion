// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 可操作仓储分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial class EFCoreRepository<TEntity>
         where TEntity : class, IEntityBase, new()
    {
        /// <summary>
        /// 新增或更新一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>代理中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdate(TEntity entity)
        {
            return IsKeySet(entity) ? Update(entity) : Insert(entity);
        }

        /// <summary>
        /// 新增或更新一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateAsync(entity) : InsertAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条记录并立即执行
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateNow(TEntity entity)
        {
            return IsKeySet(entity) ? UpdateNow(entity) : InsertNow(entity);
        }

        /// <summary>
        /// 新增或更新一条记录并立即执行
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns>数据库中的实体</returns>
        public virtual EntityEntry<TEntity> InsertOrUpdateNow(TEntity entity, bool acceptAllChangesOnSuccess)
        {
            return IsKeySet(entity) ? UpdateNow(entity, acceptAllChangesOnSuccess) : InsertNow(entity, acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 新增或更新一条记录并立即执行
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateNowAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateNowAsync(entity, cancellationToken) : InsertNowAsync(entity, cancellationToken);
        }

        /// <summary>
        /// 新增或更新一条记录并立即执行
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual Task<EntityEntry<TEntity>> InsertOrUpdateNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return IsKeySet(entity) ? UpdateNowAsync(entity, acceptAllChangesOnSuccess, cancellationToken) : InsertNowAsync(entity, acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}