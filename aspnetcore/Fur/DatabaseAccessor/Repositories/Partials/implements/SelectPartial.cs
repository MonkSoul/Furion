using Fur.DatabaseAccessor.Extensions.Paged;
using Fur.DatabaseAccessor.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Repositories
{
    /// <summary>
    /// 泛型仓储 查询操作 分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial class EFCoreRepositoryOfT<TEntity> : IRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        #region 查询单条 + public virtual TEntity Find(object id)

        /// <summary>
        /// 查询单条
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体</returns>
        public virtual TEntity Find(object id)
        {
            return Entities.Find(id);
        }

        #endregion 查询单条 + public virtual TEntity Find(object id)

        #region 查询单条 + public virtual ValueTask<TEntity> FindAsync(object id)

        /// <summary>
        /// 查询单条
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="ValueTask{TResult}"/></returns>
        public virtual ValueTask<TEntity> FindAsync(object id)
        {
            return Entities.FindAsync(id);
        }

        #endregion 查询单条 + public virtual ValueTask<TEntity> FindAsync(object id)

        #region 查询单条 + public virtual TEntity Single()

        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual TEntity Single()
        {
            return Entities.Single();
        }

        #endregion 查询单条 + public virtual TEntity Single()

        #region 查询单条 + public virtual Task<TEntity> SingleAsync()

        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual Task<TEntity> SingleAsync()
        {
            return Entities.SingleAsync();
        }

        #endregion 查询单条 + public virtual Task<TEntity> SingleAsync()

        #region 查询单条 + public virtual TEntity Single(Expression<Func<TEntity, bool>> expression)

        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>实体</returns>
        public virtual TEntity Single(Expression<Func<TEntity, bool>> expression)
        {
            return Entities.Single(expression);
        }

        #endregion 查询单条 + public virtual TEntity Single(Expression<Func<TEntity, bool>> expression)

        #region 查询单条 + public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression)

        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Entities.SingleAsync(expression);
        }

        #endregion 查询单条 + public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression)

        #region 查询单条 + public virtual TEntity Single(bool noTracking)

        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual TEntity Single(bool noTracking)
        {
            if (noTracking) return DerailEntities.Single();
            else return Single();
        }

        #endregion 查询单条 + public virtual TEntity Single(bool noTracking)

        #region 查询单条 + public virtual Task<TEntity> SingleAsync(bool noTracking)

        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual Task<TEntity> SingleAsync(bool noTracking)
        {
            if (noTracking) return DerailEntities.SingleAsync();
            else return SingleAsync();
        }

        #endregion 查询单条 + public virtual Task<TEntity> SingleAsync(bool noTracking)

        #region 查询单条 + public virtual TEntity Single(Expression<Func<TEntity, bool>> expression, bool noTracking)

        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual TEntity Single(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return DerailEntities.Single(expression);
            else return Single(expression);
        }

        #endregion 查询单条 + public virtual TEntity Single(Expression<Func<TEntity, bool>> expression, bool noTracking)

        #region 查询单条 + public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)

        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return DerailEntities.SingleAsync(expression);
            else return SingleAsync(expression);
        }

        #endregion 查询单条 + public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)

        #region 查询单条 + public virtual TEntity SingleOrDefault()

        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual TEntity SingleOrDefault()
        {
            return Entities.SingleOrDefault();
        }

        #endregion 查询单条 + public virtual TEntity SingleOrDefault()

        #region 查询单条 + public virtual Task<TEntity> SingleOrDefaultAsync()

        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual Task<TEntity> SingleOrDefaultAsync()
        {
            return Entities.SingleOrDefaultAsync();
        }

        #endregion 查询单条 + public virtual Task<TEntity> SingleOrDefaultAsync()

        #region 查询单条 + public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression)

        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>实体</returns>
        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression)
        {
            return Entities.SingleOrDefault(expression);
        }

        #endregion 查询单条 + public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression)

        #region 查询单条 + public virtual Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression)

        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>实体</returns>
        public virtual Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Entities.SingleOrDefaultAsync(expression);
        }

        #endregion 查询单条 + public virtual Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression)

        #region 查询单条 + public virtual TEntity SingleOrDefault(bool noTracking)

        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual TEntity SingleOrDefault(bool noTracking)
        {
            if (noTracking) return DerailEntities.SingleOrDefault();
            else return SingleOrDefault();
        }

        #endregion 查询单条 + public virtual TEntity SingleOrDefault(bool noTracking)

        #region 查询单条 + public virtual Task<TEntity> SingleOrDefaultAsync(bool noTracking)

        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual Task<TEntity> SingleOrDefaultAsync(bool noTracking)
        {
            if (noTracking) return DerailEntities.SingleOrDefaultAsync();
            else return SingleOrDefaultAsync();
        }

        #endregion 查询单条 + public virtual Task<TEntity> SingleOrDefaultAsync(bool noTracking)

        #region 查询单条 + public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking)

        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return DerailEntities.SingleOrDefault(expression);
            else return SingleOrDefault(expression);
        }

        #endregion 查询单条 + public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking)

        #region 查询单条 + public virtual Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)

        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return DerailEntities.SingleOrDefaultAsync(expression);
            else return SingleOrDefaultAsync(expression);
        }

        #endregion 查询单条 + public virtual Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)

        #region 查询一条 + public virtual TEntity First()

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c></para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual TEntity First()
        {
            return Entities.First();
        }

        #endregion 查询一条 + public virtual TEntity First()

        #region 查询一条 + public virtual Task<TEntity> FirstAsync()

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c></para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual Task<TEntity> FirstAsync()
        {
            return Entities.FirstAsync();
        }

        #endregion 查询一条 + public virtual Task<TEntity> FirstAsync()

        #region 查询一条 + public virtual TEntity First(Expression<Func<TEntity, bool>> expression)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c></para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>实体</returns>
        public virtual TEntity First(Expression<Func<TEntity, bool>> expression)
        {
            return Entities.First(expression);
        }

        #endregion 查询一条 + public virtual TEntity First(Expression<Func<TEntity, bool>> expression)

        #region 查询一条 + public virtual Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c></para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Entities.FirstAsync(expression);
        }

        #endregion 查询一条 + public virtual Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression)

        #region 查询一条 + public virtual TEntity First(bool noTracking)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c></para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual TEntity First(bool noTracking)
        {
            if (noTracking) return DerailEntities.First();
            else return First();
        }

        #endregion 查询一条 + public virtual TEntity First(bool noTracking)

        #region 查询一条 + public virtual Task<TEntity> FirstAsync(bool noTracking)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c></para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual Task<TEntity> FirstAsync(bool noTracking)
        {
            if (noTracking) return DerailEntities.FirstAsync();
            else return FirstAsync();
        }

        #endregion 查询一条 + public virtual Task<TEntity> FirstAsync(bool noTracking)

        #region 查询一条 + public virtual TEntity First(Expression<Func<TEntity, bool>> expression, bool noTracking)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c></para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns></returns>
        public virtual TEntity First(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return DerailEntities.First(expression);
            else return First(expression);
        }

        #endregion 查询一条 + public virtual TEntity First(Expression<Func<TEntity, bool>> expression, bool noTracking)

        #region 查询一条 + public virtual Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c></para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return DerailEntities.FirstAsync(expression);
            else return FirstAsync(expression);
        }

        #endregion 查询一条 + public virtual Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)

        #region 查询一条 + public virtual TEntity FirstOrDefault()

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual TEntity FirstOrDefault()
        {
            return Entities.FirstOrDefault();
        }

        #endregion 查询一条 + public virtual TEntity FirstOrDefault()

        #region 查询一条 + public virtual Task<TEntity> FirstOrDefaultAsync()

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual Task<TEntity> FirstOrDefaultAsync()
        {
            return Entities.FirstOrDefaultAsync();
        }

        #endregion 查询一条 + public virtual Task<TEntity> FirstOrDefaultAsync()

        #region 查询一条 + public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>实体</returns>
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression)
        {
            return Entities.FirstOrDefault(expression);
        }

        #endregion 查询一条 + public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression)

        #region 查询一条 + public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>实体</returns>
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Entities.FirstOrDefaultAsync(expression);
        }

        #endregion 查询一条 + public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)

        #region 查询一条 + public virtual TEntity FirstOrDefault(bool noTracking)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual TEntity FirstOrDefault(bool noTracking)
        {
            if (noTracking) return DerailEntities.FirstOrDefault();
            else return FirstOrDefault();
        }

        #endregion 查询一条 + public virtual TEntity FirstOrDefault(bool noTracking)

        #region 查询一条 + public virtual Task<TEntity> FirstOrDefaultAsync(bool noTracking)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual Task<TEntity> FirstOrDefaultAsync(bool noTracking)
        {
            if (noTracking) return DerailEntities.FirstOrDefaultAsync();
            else return FirstOrDefaultAsync();
        }

        #endregion 查询一条 + public virtual Task<TEntity> FirstOrDefaultAsync(bool noTracking)

        #region 查询一条 + public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return DerailEntities.FirstOrDefault(expression);
            else return FirstOrDefault(expression);
        }

        #endregion 查询一条 + public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking)

        #region 查询一条 + public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return DerailEntities.FirstOrDefaultAsync(expression);
            else return FirstOrDefaultAsync(expression);
        }

        #endregion 查询一条 + public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)

        #region 查询多条 + public virtual IQueryable<TEntity> All(bool noTracking = true, bool ignoreQueryFilters = false)

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <param name="ignoreQueryFilters">忽略过滤器</param>
        /// <returns>多个实体</returns>
        public virtual IQueryable<TEntity> All(bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return CombineIQueryable(null, noTracking, ignoreQueryFilters);
        }

        #endregion 查询多条 + public virtual IQueryable<TEntity> All(bool noTracking = true, bool ignoreQueryFilters = false)

        #region 查询多条 + public virtual Task<List<TEntity>> AllAsync(bool noTracking = true, bool ignoreQueryFilters = false)

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <param name="ignoreQueryFilters">忽略过滤器</param>
        /// <returns>多个实体</returns>
        public virtual Task<List<TEntity>> AllAsync(bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var query = CombineIQueryable(null, noTracking, ignoreQueryFilters);
            return query.ToListAsync();
        }

        #endregion 查询多条 + public virtual Task<List<TEntity>> AllAsync(bool noTracking = true, bool ignoreQueryFilters = false)

        #region 查询多条 + public virtual IQueryable<TEntity> All(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryFilters = false)

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <param name="ignoreQueryFilters">忽略过滤器</param>
        /// <returns>多个实体</returns>
        public virtual IQueryable<TEntity> All(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return CombineIQueryable(expression, noTracking, ignoreQueryFilters);
        }

        #endregion 查询多条 + public virtual IQueryable<TEntity> All(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryFilters = false)

        #region 查询多条 + public virtual Task<List<TEntity>> AllAsync(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryFilters = false)

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <param name="ignoreQueryFilters">忽略过滤器</param>
        /// <returns>多个实体</returns>
        public virtual Task<List<TEntity>> AllAsync(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var query = CombineIQueryable(expression, noTracking, ignoreQueryFilters);
            return query.ToListAsync();
        }

        #endregion 查询多条 + public virtual Task<List<TEntity>> AllAsync(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryFilters = false)

        #region 分页查询多条 + public virtual PagedListOfT<TEntity> PagedAll(int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <param name="ignoreQueryFilters">忽略过滤器</param>
        /// <returns><see cref="PagedListOfT{T}"/></returns>
        public virtual PagedListOfT<TEntity> PagedAll(int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var query = CombineIQueryable(null, noTracking, ignoreQueryFilters);
            return query.ToPagedList(pageIndex, pageSize);
        }

        #endregion 分页查询多条 + public virtual PagedListOfT<TEntity> PagedAll(int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)

        #region 分页查询多条 + public virtual Task<PagedListOfT<TEntity>> PagedAllAsync(int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <param name="ignoreQueryFilters">忽略过滤器</param>
        /// <returns>多个实体</returns>
        public virtual Task<PagedListOfT<TEntity>> PagedAllAsync(int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var query = CombineIQueryable(null, noTracking, ignoreQueryFilters);
            return query.ToPagedListAsync(pageIndex, pageSize);
        }

        #endregion 分页查询多条 + public virtual Task<PagedListOfT<TEntity>> PagedAllAsync(int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)

        #region 分页查询多条 + public virtual PagedListOfT<TEntity> PagedAll(Expression<Func<TEntity, bool>> expression, int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <param name="ignoreQueryFilters">忽略过滤器</param>
        /// <returns>多个实体</returns>
        public virtual PagedListOfT<TEntity> PagedAll(Expression<Func<TEntity, bool>> expression, int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var query = CombineIQueryable(expression, noTracking, ignoreQueryFilters);
            return query.ToPagedList(pageIndex, pageSize);
        }

        #endregion 分页查询多条 + public virtual PagedListOfT<TEntity> PagedAll(Expression<Func<TEntity, bool>> expression, int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)

        #region 分页查询多条 + public virtual Task<PagedListOfT<TEntity>> PagedAllAsync(Expression<Func<TEntity, bool>> expression, int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <param name="ignoreQueryFilters">忽略过滤器</param>
        /// <returns>多个实体</returns>
        public virtual Task<PagedListOfT<TEntity>> PagedAllAsync(Expression<Func<TEntity, bool>> expression, int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var query = CombineIQueryable(expression, noTracking, ignoreQueryFilters);
            return query.ToPagedListAsync(pageIndex, pageSize);
        }

        #endregion 分页查询多条 + public virtual Task<PagedListOfT<TEntity>> PagedAllAsync(Expression<Func<TEntity, bool>> expression, int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)

        #region 判断记录是否存在 + public virtual bool Any(Expression<Func<TEntity, bool>> expression = null)

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>存在与否</returns>
        public virtual bool Any(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? Entities.Any() : Entities.Any(expression);
        }

        #endregion 判断记录是否存在 + public virtual bool Any(Expression<Func<TEntity, bool>> expression = null)

        #region 判断记录是否存在 + public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null)

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? Entities.AnyAsync() : Entities.AnyAsync(expression);
        }

        #endregion 判断记录是否存在 + public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null)

        #region 获取记录条数 + public virtual int Count(Expression<Func<TEntity, bool>> expression = null)

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>int</returns>
        public virtual int Count(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? Entities.Count() : Entities.Count(expression);
        }

        #endregion 获取记录条数 + public virtual int Count(Expression<Func<TEntity, bool>> expression = null)

        #region 获取记录条数 + public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null)

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>int</returns>
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? Entities.CountAsync() : Entities.CountAsync(expression);
        }

        #endregion 获取记录条数 + public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null)

        #region 获取实体队列中最大实体 + public virtual TEntity Max()

        /// <summary>
        /// 获取实体队列中最大实体
        /// </summary>
        /// <returns><see cref="TEntity"/></returns>
        public virtual TEntity Max()
        {
            return Entities.Max();
        }

        #endregion 获取实体队列中最大实体 + public virtual TEntity Max()

        #region 获取实体队列中最大实体 + public virtual Task<TEntity> MaxAsync()

        /// <summary>
        /// 获取实体队列中最大实体
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<TEntity> MaxAsync()
        {
            return Entities.MaxAsync();
        }

        #endregion 获取实体队列中最大实体 + public virtual Task<TEntity> MaxAsync()

        #region 获取最大值 + public virtual TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression)

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TResult">值类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns>最大值</returns>
        public virtual TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return Entities.Max(expression);
        }

        #endregion 获取最大值 + public virtual TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression)

        #region 获取最大值 + public virtual Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression)

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TResult">值类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return Entities.MaxAsync(expression);
        }

        #endregion 获取最大值 + public virtual Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression)

        #region 获取实体队列中最小实体 + public virtual TEntity Min()

        /// <summary>
        /// 获取实体队列中最小实体
        /// </summary>
        /// <returns>实体</returns>
        public virtual TEntity Min()
        {
            return Entities.Min();
        }

        #endregion 获取实体队列中最小实体 + public virtual TEntity Min()

        #region 获取实体队列中最小实体 + public virtual Task<TEntity> MinAsync()

        /// <summary>
        /// 获取实体队列中最小实体
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<TEntity> MinAsync()
        {
            return Entities.MinAsync();
        }

        #endregion 获取实体队列中最小实体 + public virtual Task<TEntity> MinAsync()

        #region 获取最小值 + public virtual TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression)

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TResult">值类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns>最大值</returns>
        public virtual TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return Entities.Min(expression);
        }

        #endregion 获取最小值 + public virtual TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression)

        #region 获取最小值 + public virtual Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression)

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TResult">值类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return Entities.MinAsync(expression);
        }

        #endregion 获取最小值 + public virtual Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression)

        #region 组合查询条件 + private IQueryable<TEntity> CombineIQueryable(Expression<Func<TEntity, bool>> expression = null, bool noTracking = true, bool ignoreQueryFilters = false)

        /// <summary>
        /// 组合查询条件
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <param name="ignoreQueryFilters">忽略过滤器</param>
        /// <returns><see cref="IQueryable{T}"/></returns>
        private IQueryable<TEntity> CombineIQueryable(Expression<Func<TEntity, bool>> expression = null, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> entities = noTracking ? DerailEntities : Entities;
            if (ignoreQueryFilters) entities = entities.IgnoreQueryFilters();
            if (expression != null) entities = entities.Where(expression);

            return entities;
        }

        #endregion 组合查询条件 + private IQueryable<TEntity> CombineIQueryable(Expression<Func<TEntity, bool>> expression = null, bool noTracking = true, bool ignoreQueryFilters = false)
    }
}