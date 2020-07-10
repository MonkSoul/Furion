using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Page;
using Fur.DependencyInjection.Lifetimes;
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
    public partial class EFCoreRepositoryOfT<TEntity> : IRepositoryOfT<TEntity>, IScopedLifetimeOfT<TEntity> where TEntity : class, IDbEntity, new()
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


        public virtual TEntity SingleOrDefault()
        {
            return Entity.SingleOrDefault();
        }

        public virtual Task<TEntity> SingleOrDefaultAsync()
        {
            return Entity.SingleOrDefaultAsync();
        }

        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.SingleOrDefault(expression);
        }

        public virtual Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.SingleOrDefaultAsync(expression);
        }


        public virtual TEntity SingleOrDefault(bool noTracking)
        {
            if (noTracking) return DerailEntity.SingleOrDefault();
            else return SingleOrDefault();
        }

        public virtual Task<TEntity> SingleOrDefaultAsync(bool noTracking)
        {
            if (noTracking) return DerailEntity.SingleOrDefaultAsync();
            else return SingleOrDefaultAsync();
        }

        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return DerailEntity.SingleOrDefault(expression);
            else return SingleOrDefault(expression);
        }

        public virtual Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return DerailEntity.SingleOrDefaultAsync(expression);
            else return SingleOrDefaultAsync(expression);
        }

        public virtual TEntity First()
        {
            return Entity.First();
        }

        public virtual Task<TEntity> FirstAsync()
        {
            return Entity.FirstAsync();
        }

        public virtual TEntity First(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.First(expression);
        }

        public virtual Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.FirstAsync(expression);
        }

        public virtual TEntity FirstOrDefault()
        {
            return Entity.FirstOrDefault();
        }

        public virtual Task<TEntity> FirstOrDefaultAsync()
        {
            return Entity.FirstOrDefaultAsync();
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.FirstOrDefault(expression);
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Entity.FirstOrDefaultAsync(expression);
        }

        public virtual TEntity First(bool noTracking)
        {
            if (noTracking) return DerailEntity.First();
            else return First();
        }

        public virtual Task<TEntity> FirstAsync(bool noTracking)
        {
            if (noTracking) return DerailEntity.FirstAsync();
            else return FirstAsync();
        }

        public virtual TEntity First(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return DerailEntity.First(expression);
            else return First(expression);
        }

        public virtual Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return DerailEntity.FirstAsync(expression);
            else return FirstAsync(expression);
        }

        public virtual TEntity FirstOrDefault(bool noTracking)
        {
            if (noTracking) return DerailEntity.FirstOrDefault();
            else return FirstOrDefault();
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(bool noTracking)
        {
            if (noTracking) return DerailEntity.FirstOrDefaultAsync();
            else return FirstOrDefaultAsync();
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return DerailEntity.FirstOrDefault(expression);
            else return FirstOrDefault(expression);
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            if (noTracking) return DerailEntity.FirstOrDefaultAsync(expression);
            else return FirstOrDefaultAsync(expression);
        }

        // 获取所有
        public virtual IQueryable<TEntity> Get(bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return GetQueryConditionCombine(null, noTracking, ignoreQueryFilters);
        }

        public virtual Task<List<TEntity>> GetAsync(bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var query = GetQueryConditionCombine(null, noTracking, ignoreQueryFilters);
            return query.ToListAsync();
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            return GetQueryConditionCombine(expression, noTracking, ignoreQueryFilters);
        }

        public virtual Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var query = GetQueryConditionCombine(expression, noTracking, ignoreQueryFilters);
            return query.ToListAsync();
        }

        public virtual IPagedListOfT<TEntity> GetPage(int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var query = GetQueryConditionCombine(null, noTracking, ignoreQueryFilters);
            return query.ToPagedList(pageIndex, pageSize);
        }

        public virtual Task<IPagedListOfT<TEntity>> GetPageAsync(int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var query = GetQueryConditionCombine(null, noTracking, ignoreQueryFilters);
            return query.ToPagedListAsync(pageIndex, pageSize);
        }

        public virtual IPagedListOfT<TEntity> GetPage(Expression<Func<TEntity, bool>> expression, int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var query = GetQueryConditionCombine(expression, noTracking, ignoreQueryFilters);
            return query.ToPagedList(pageIndex, pageSize);
        }

        public virtual Task<IPagedListOfT<TEntity>> GetPageAsync(Expression<Func<TEntity, bool>> expression, int pageIndex = 0, int pageSize = 20, bool noTracking = true, bool ignoreQueryFilters = false)
        {
            var query = GetQueryConditionCombine(expression, noTracking, ignoreQueryFilters);
            return query.ToPagedListAsync(pageIndex, pageSize);
        }

    }
}
