using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Extensions.Page;
using Microsoft.EntityFrameworkCore;
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
            return Entity.Find(id);
        }
        #endregion

        #region 查询单条 + public virtual ValueTask<TEntity> FindAsync(object id)
        /// <summary>
        /// 查询单条
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="ValueTask{TResult}"/></returns>
        public virtual ValueTask<TEntity> FindAsync(object id)
        {
            return Entity.FindAsync(id);
        }
        #endregion


        #region 查询单条 + public virtual TEntity Single()
        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual TEntity Single()
        {
            return Entity.Single();
        }
        #endregion

        #region 查询单条 + public virtual Task<TEntity> SingleAsync()
        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual Task<TEntity> SingleAsync()
        {
            return Entity.SingleAsync();
        }
        #endregion

        #region 查询单条 + public virtual TEntity Single(Expression<Func<TEntity, bool>> expression)
        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>实体</returns>
        public virtual TEntity Single(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.Single(expression);
        }
        #endregion

        #region 查询单条 + public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression)
        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.SingleAsync(expression);
        }
        #endregion


        #region 查询单条 + public virtual TEntity Single(bool noTracking)
        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual TEntity Single(bool noTracking)
        {
            if (noTracking) return DerailEntity.Single();
            else return Single();
        }
        #endregion

        #region 查询单条 + public virtual Task<TEntity> SingleAsync(bool noTracking)
        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual Task<TEntity> SingleAsync(bool noTracking)
        {
            if (noTracking) return DerailEntity.SingleAsync();
            else return SingleAsync();
        }
        #endregion

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
            if (noTracking) return DerailEntity.Single(expression);
            else return Single(expression);
        }
        #endregion

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
            if (noTracking) return DerailEntity.SingleAsync(expression);
            else return SingleAsync(expression);
        }
        #endregion


        #region 查询单条 + public virtual TEntity SingleOrDefault()
        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual TEntity SingleOrDefault()
        {
            return Entity.SingleOrDefault();
        }
        #endregion

        #region 查询单条 + public virtual Task<TEntity> SingleOrDefaultAsync()
        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual Task<TEntity> SingleOrDefaultAsync()
        {
            return Entity.SingleOrDefaultAsync();
        }
        #endregion

        #region 查询单条 + public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression)
        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>实体</returns>
        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.SingleOrDefault(expression);
        }
        #endregion

        #region 查询单条 + public virtual Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>实体</returns>
        public virtual Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.SingleOrDefaultAsync(expression);
        }
        #endregion


        #region 查询单条 + public virtual TEntity SingleOrDefault(bool noTracking)
        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual TEntity SingleOrDefault(bool noTracking)
        {
            if (noTracking) return DerailEntity.SingleOrDefault();
            else return SingleOrDefault();
        }
        #endregion

        #region 查询单条 + public virtual Task<TEntity> SingleOrDefaultAsync(bool noTracking)
        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual Task<TEntity> SingleOrDefaultAsync(bool noTracking)
        {
            if (noTracking) return DerailEntity.SingleOrDefaultAsync();
            else return SingleOrDefaultAsync();
        }
        #endregion

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
            if (noTracking) return DerailEntity.SingleOrDefault(expression);
            else return SingleOrDefault(expression);
        }
        #endregion

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
            if (noTracking) return DerailEntity.SingleOrDefaultAsync(expression);
            else return SingleOrDefaultAsync(expression);
        }
        #endregion


        #region 查询一条 + public virtual TEntity First()
        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c></para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual TEntity First()
        {
            return Entity.First();
        }
        #endregion

        #region 查询一条 + public virtual Task<TEntity> FirstAsync()
        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c></para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual Task<TEntity> FirstAsync()
        {
            return Entity.FirstAsync();
        }
        #endregion

        #region 查询一条 + public virtual TEntity First(Expression<Func<TEntity, bool>> expression)
        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c></para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>实体</returns>
        public virtual TEntity First(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.First(expression);
        }
        #endregion

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
            return Entity.FirstAsync(expression);
        }
        #endregion


        #region 查询一条 + public virtual TEntity First(bool noTracking)
        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c></para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual TEntity First(bool noTracking)
        {
            if (noTracking) return DerailEntity.First();
            else return First();
        }
        #endregion

        #region 查询一条 + public virtual Task<TEntity> FirstAsync(bool noTracking)
        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c></para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual Task<TEntity> FirstAsync(bool noTracking)
        {
            if (noTracking) return DerailEntity.FirstAsync();
            else return FirstAsync();
        }
        #endregion

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
            if (noTracking) return DerailEntity.First(expression);
            else return First(expression);
        }
        #endregion

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
            if (noTracking) return DerailEntity.FirstAsync(expression);
            else return FirstAsync(expression);
        }
        #endregion


        #region 查询一条 + public virtual TEntity FirstOrDefault()
        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual TEntity FirstOrDefault()
        {
            return Entity.FirstOrDefault();
        }
        #endregion

        #region 查询一条 + public virtual Task<TEntity> FirstOrDefaultAsync()
        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual Task<TEntity> FirstOrDefaultAsync()
        {
            return Entity.FirstOrDefaultAsync();
        }
        #endregion

        #region 查询一条 + public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression)
        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>实体</returns>
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.FirstOrDefault(expression);
        }
        #endregion

        #region 查询一条 + public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>实体</returns>
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.FirstOrDefaultAsync(expression);
        }
        #endregion


        #region 查询一条 + public virtual TEntity FirstOrDefault(bool noTracking)
        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual TEntity FirstOrDefault(bool noTracking)
        {
            if (noTracking) return DerailEntity.FirstOrDefault();
            else return FirstOrDefault();
        }
        #endregion

        #region 查询一条 + public virtual Task<TEntity> FirstOrDefaultAsync(bool noTracking)
        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <param name="noTracking">不跟踪实体</param>
        /// <returns>实体</returns>
        public virtual Task<TEntity> FirstOrDefaultAsync(bool noTracking)
        {
            if (noTracking) return DerailEntity.FirstOrDefaultAsync();
            else return FirstOrDefaultAsync();
        }
        #endregion

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
            if (noTracking) return DerailEntity.FirstOrDefault(expression);
            else return FirstOrDefault(expression);
        }
        #endregion

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
            if (noTracking) return DerailEntity.FirstOrDefaultAsync(expression);
            else return FirstOrDefaultAsync(expression);
        }
        #endregion


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
        #endregion

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
        #endregion

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
        #endregion

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
        #endregion


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
        #endregion

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
        #endregion

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
        #endregion

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
        #endregion


        #region 判断记录是否存在 + public virtual bool Any(Expression<Func<TEntity, bool>> expression = null)
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>存在与否</returns>
        public virtual bool Any(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? Entity.Any() : Entity.Any(expression);
        }
        #endregion

        #region 判断记录是否存在 + public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null)
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? Entity.AnyAsync() : Entity.AnyAsync(expression);
        }
        #endregion


        #region 获取记录条数 + public virtual int Count(Expression<Func<TEntity, bool>> expression = null)
        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>int</returns>
        public virtual int Count(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? Entity.Count() : Entity.Count(expression);
        }
        #endregion

        #region 获取记录条数 + public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null)
        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>int</returns>
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? Entity.CountAsync() : Entity.CountAsync(expression);
        }
        #endregion


        #region 获取实体队列中最大实体 + public virtual TEntity Max()
        /// <summary>
        /// 获取实体队列中最大实体
        /// </summary>
        /// <returns><see cref="TEntity"/></returns>
        public virtual TEntity Max()
        {
            return Entity.Max();
        }
        #endregion

        #region 获取实体队列中最大实体 + public virtual Task<TEntity> MaxAsync()
        /// <summary>
        /// 获取实体队列中最大实体
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<TEntity> MaxAsync()
        {
            return Entity.MaxAsync();
        }
        #endregion

        #region 获取最大值 + public virtual TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression)
        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TResult">值类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns>最大值</returns>
        public virtual TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return Entity.Max(expression);
        }
        #endregion

        #region 获取最大值 + public virtual Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TResult">值类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return Entity.MaxAsync(expression);
        }
        #endregion


        #region 获取实体队列中最小实体 + public virtual TEntity Min()
        /// <summary>
        /// 获取实体队列中最小实体
        /// </summary>
        /// <returns>实体</returns>
        public virtual TEntity Min()
        {
            return Entity.Min();
        }
        #endregion

        #region 获取实体队列中最小实体 + public virtual Task<TEntity> MinAsync()
        /// <summary>
        /// 获取实体队列中最小实体
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<TEntity> MinAsync()
        {
            return Entity.MinAsync();
        }
        #endregion

        #region 获取最小值 + public virtual TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression)
        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TResult">值类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns>最大值</returns>
        public virtual TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return Entity.Min(expression);
        }
        #endregion

        #region 获取最小值 + public virtual Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TResult">值类型</typeparam>
        /// <param name="expression">表达式</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return Entity.MinAsync(expression);
        }
        #endregion


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
            IQueryable<TEntity> entities = noTracking ? DerailEntity : Entity;
            if (ignoreQueryFilters) entities = entities.IgnoreQueryFilters();
            if (expression != null) entities = entities.Where(expression);

            return entities;
        }
        #endregion
    }
}
