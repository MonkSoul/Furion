using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 泛型仓储 查询操作 分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial interface IRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        #region 查询单条 + TEntity Find(object id)
        /// <summary>
        /// 查询单条
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体</returns>
        TEntity Find(object id);
        #endregion

        #region 查询单条 + ValueTask<TEntity> FindAsync(object id)
        /// <summary>
        /// 查询单条
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="ValueTask{TResult}"/></returns>
        ValueTask<TEntity> FindAsync(object id);
        #endregion


        #region 查询单条 + TEntity Single()
        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <returns>实体</returns>
        TEntity Single();
        #endregion

        #region 查询单条 + Task<TEntity> SingleAsync()
        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <returns>实体</returns>
        Task<TEntity> SingleAsync();
        #endregion

        #region 查询单条 + TEntity Single(Expression<Func<TEntity, bool>> expression)
        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>实体</returns>
        TEntity Single(Expression<Func<TEntity, bool>> expression);
        #endregion

        #region 查询单条 + Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression)
        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression);
        #endregion


        #region 查询单条 + TEntity Single(bool noTracking)
        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        TEntity Single(bool noTracking);
        #endregion

        #region 查询单条 + Task<TEntity> SingleAsync(bool noTracking)
        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        Task<TEntity> SingleAsync(bool noTracking);
        #endregion

        #region 查询单条 + TEntity Single(Expression<Func<TEntity, bool>> expression, bool noTracking)
        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        TEntity Single(Expression<Func<TEntity, bool>> expression, bool noTracking);
        #endregion

        #region 查询单条 + Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)
        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression, bool noTracking);
        #endregion


        #region 查询单条 + TEntity SingleOrDefault()
        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <returns>实体</returns>
        TEntity SingleOrDefault();
        #endregion

        #region 查询单条 + Task<TEntity> SingleOrDefaultAsync()
        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <returns>实体</returns>
        Task<TEntity> SingleOrDefaultAsync();
        #endregion

        #region 查询单条 + TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression)
        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>实体</returns>
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression);
        #endregion

        #region 查询单条 + Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>实体</returns>
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression);
        #endregion


        #region 查询单条 + TEntity SingleOrDefault(bool noTracking)
        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        TEntity SingleOrDefault(bool noTracking);
        #endregion

        #region 查询单条 + Task<TEntity> SingleOrDefaultAsync(bool noTracking)
        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns></returns>
        Task<TEntity> SingleOrDefaultAsync(bool noTracking);
        #endregion

        #region 查询单条 + TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking)
        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking);
        #endregion

        #region 查询单条 + Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)
        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns></returns>
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking);
        #endregion


        TEntity First();

        TEntity First(bool noTracking);

        Task<TEntity> FirstAsync();

        Task<TEntity> FirstAsync(bool noTracking);

        TEntity First(Expression<Func<TEntity, bool>> expression);

        TEntity First(Expression<Func<TEntity, bool>> expression, bool noTracking);

        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression, bool noTracking);

        TEntity FirstOrDefault();

        TEntity FirstOrDefault(bool noTracking);

        Task<TEntity> FirstOrDefaultAsync();

        Task<TEntity> FirstOrDefaultAsync(bool noTracking);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking);

        // 查询多条
        IQueryable<TEntity> Get(bool noTracking = true, bool ignoreQueryFilters = false);

        Task<List<TEntity>> GetAsync(bool noTracking = true, bool ignoreQueryFilters = false);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryFilters = false);

        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryFilters = false);

        // 分页查询
        IPagedListOfT<TEntity> GetPage(int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false);

        Task<IPagedListOfT<TEntity>> GetPageAsync(int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false);

        IPagedListOfT<TEntity> GetPage(Expression<Func<TEntity, bool>> expression, int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false);

        Task<IPagedListOfT<TEntity>> GetPageAsync(Expression<Func<TEntity, bool>> expression, int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false);
    }
}
