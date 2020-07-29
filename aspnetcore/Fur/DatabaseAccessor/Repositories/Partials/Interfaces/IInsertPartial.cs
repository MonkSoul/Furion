using Fur.DatabaseAccessor.Models.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Repositories
{
    /// <summary>
    /// 泛型仓储  新增操作 分部接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial interface IRepository<TEntity> where TEntity : class, IDbEntityBase, new()
    {
        #region 新增操作 + EntityEntry<TEntity> Insert(TEntity entity)

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> Insert(TEntity entity);

        #endregion

        #region 新增操作 + void Insert(params TEntity[] entities)

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        void Insert(params TEntity[] entities);

        #endregion

        #region 新增操作 + void Insert(IEnumerable<TEntity> entities)

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        void Insert(IEnumerable<TEntity> entities);

        #endregion

        #region 新增操作 + ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity)

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="ValueTask{TResult}"/></returns>
        ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity);

        #endregion

        #region 新增操作 + Task InsertAsync(params TEntity[] entities)

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task InsertAsync(params TEntity[] entities);

        #endregion

        #region 新增操作 + Task InsertAsync(IEnumerable<TEntity> entities)

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task"/></returns>
        Task InsertAsync(IEnumerable<TEntity> entities);

        #endregion

        #region 新增操作并立即保存 + EntityEntry<TEntity> InsertSaveChanges(TEntity entity)

        /// <summary>
        /// 新增操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="EntityEntry{TEntity}"/></returns>
        EntityEntry<TEntity> InsertSaveChanges(TEntity entity);

        #endregion

        #region 新增操作并立即保存 + void InsertSaveChanges(params TEntity[] entities)

        /// <summary>
        /// 新增操作并立即保存
        /// </summary>
        /// <param name="entities"></param>
        void InsertSaveChanges(params TEntity[] entities);

        #endregion

        #region 新增操作并立即保存 + void InsertSaveChanges(IEnumerable<TEntity> entities)

        /// <summary>
        /// 新增操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        void InsertSaveChanges(IEnumerable<TEntity> entities);

        #endregion

        #region 新增操作并立即保存 + ValueTask<EntityEntry<TEntity>> InsertSaveChangesAsync(TEntity entity)

        /// <summary>
        /// 新增操作并立即保存
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns><see cref="ValueTask{TResult}"/></returns>
        ValueTask<EntityEntry<TEntity>> InsertSaveChangesAsync(TEntity entity);

        #endregion

        #region 新增操作并立即保存 + Task InsertSaveChangesAsync(params TEntity[] entities)

        /// <summary>
        /// 新增操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task InsertSaveChangesAsync(params TEntity[] entities);

        #endregion

        #region 新增操作并立即保存 + Task InsertSaveChangesAsync(IEnumerable<TEntity> entities)

        /// <summary>
        /// 新增操作并立即保存
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns></returns>
        Task InsertSaveChangesAsync(IEnumerable<TEntity> entities);

        #endregion
    }
}