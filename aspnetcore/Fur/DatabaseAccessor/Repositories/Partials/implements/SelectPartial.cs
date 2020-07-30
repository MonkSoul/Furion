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
    public partial class EFCoreRepository<TEntity> : IRepository<TEntity> where TEntity : class, IDbEntityBase, new()
    {
        /// <summary>
        /// 查询单条
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体</returns>
        public virtual TEntity Find(object id)
        {
            return Entities.Find(id);
        }

        /// <summary>
        /// 查询单条
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns><see cref="ValueTask{TResult}"/></returns>
        public virtual ValueTask<TEntity> FindAsync(object id)
        {
            return Entities.FindAsync(id);
        }

        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual TEntity Single()
        {
            return Entities.Single();
        }

        /// <summary>
        /// 查询单条
        /// <para>如果结果集找不到或包含多条将抛异常</para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual Task<TEntity> SingleAsync()
        {
            return Entities.SingleAsync();
        }

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

        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual TEntity SingleOrDefault()
        {
            return Entities.SingleOrDefault();
        }

        /// <summary>
        /// 查询单条
        /// <para>如果包含多条将抛异常</para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual Task<TEntity> SingleOrDefaultAsync()
        {
            return Entities.SingleOrDefaultAsync();
        }

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

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c></para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual TEntity First()
        {
            return Entities.First();
        }

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c></para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual Task<TEntity> FirstAsync()
        {
            return Entities.FirstAsync();
        }

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

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual TEntity FirstOrDefault()
        {
            return Entities.FirstOrDefault();
        }

        /// <summary>
        /// 查询一条
        /// <para>类似 <c>select top 1 * from table.</c>，没找到会返回null</para>
        /// </summary>
        /// <returns>实体</returns>
        public virtual Task<TEntity> FirstOrDefaultAsync()
        {
            return Entities.FirstOrDefaultAsync();
        }

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

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <param name="ignoreQueryFilters">忽略过滤器</param>
        /// <returns><see cref="PagedList{T}"/></returns>
        public virtual PagedList<TEntity> PagedAll(int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var query = CombineIQueryable(null, noTracking, ignoreQueryFilters);
            return query.ToPagedList(pageIndex, pageSize);
        }

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <param name="ignoreQueryFilters">忽略过滤器</param>
        /// <returns>多个实体</returns>
        public virtual Task<PagedList<TEntity>> PagedAllAsync(int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var query = CombineIQueryable(null, noTracking, ignoreQueryFilters);
            return query.ToPagedListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <param name="ignoreQueryFilters">忽略过滤器</param>
        /// <returns>多个实体</returns>
        public virtual PagedList<TEntity> PagedAll(Expression<Func<TEntity, bool>> expression, int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var query = CombineIQueryable(expression, noTracking, ignoreQueryFilters);
            return query.ToPagedList(pageIndex, pageSize);
        }

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="noTracking">不跟踪实体</param>
        /// <param name="ignoreQueryFilters">忽略过滤器</param>
        /// <returns>多个实体</returns>
        public virtual Task<PagedList<TEntity>> PagedAllAsync(Expression<Func<TEntity, bool>> expression, int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var query = CombineIQueryable(expression, noTracking, ignoreQueryFilters);
            return query.ToPagedListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>存在与否</returns>
        public virtual bool Any(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? Entities.Any() : Entities.Any(expression);
        }

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? Entities.AnyAsync() : Entities.AnyAsync(expression);
        }

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>int</returns>
        public virtual int Count(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? Entities.Count() : Entities.Count(expression);
        }

        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>int</returns>
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? Entities.CountAsync() : Entities.CountAsync(expression);
        }

        /// <summary>
        /// 获取实体队列中最大实体
        /// </summary>
        /// <returns><see cref="TEntity"/></returns>
        public virtual TEntity Max()
        {
            return Entities.Max();
        }

        /// <summary>
        /// 获取实体队列中最大实体
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<TEntity> MaxAsync()
        {
            return Entities.MaxAsync();
        }

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

        /// <summary>
        /// 获取实体队列中最小实体
        /// </summary>
        /// <returns>实体</returns>
        public virtual TEntity Min()
        {
            return Entities.Min();
        }

        /// <summary>
        /// 获取实体队列中最小实体
        /// </summary>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<TEntity> MinAsync()
        {
            return Entities.MinAsync();
        }

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
    }
}