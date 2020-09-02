// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 可写仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class EFCoreRepository<TEntity>
         where TEntity : class, IDbEntityBase, new()
    {
        /// <summary>
        /// 根据主键查找
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual TEntity Find(object key)
        {
            return Entities.Find(key);
        }

        /// <summary>
        /// 根据多个主键查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public virtual TEntity Find(params object[] keyValues)
        {
            return Entities.Find(keyValues);
        }

        /// <summary>
        /// 根据主键查找
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindAsync(object key, CancellationToken cancellationToken = default)
        {
            var entity = await Entities.FindAsync(new object[] { key }, cancellationToken);
            return entity;
        }

        /// <summary>
        /// 根据多个主键查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        {
            var entity = await Entities.FindAsync(keyValues);
            return entity;
        }

        /// <summary>
        /// 根据多个主键查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken = default)
        {
            var entity = await Entities.FindAsync(keyValues, cancellationToken);
            return entity;
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <returns></returns>
        public virtual TEntity Single()
        {
            return DynamicEntities().Single();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity Single(bool noTracking)
        {
            return DynamicEntities(noTracking).Single();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return DynamicEntities().Single(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate, bool noTracking)
        {
            return DynamicEntities(noTracking).Single(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <returns></returns>
        public virtual TEntity SingleOrDefault()
        {
            return DynamicEntities().SingleOrDefault();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity SingleOrDefault(bool noTracking)
        {
            return DynamicEntities(noTracking).SingleOrDefault();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return DynamicEntities().SingleOrDefault(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking)
        {
            return DynamicEntities(noTracking).SingleOrDefault(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> SingleAsync(CancellationToken cancellationToken = default)
        {
            return DynamicEntities().SingleAsync(cancellationToken);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> SingleAsync(bool noTracking, CancellationToken cancellationToken = default)
        {
            return DynamicEntities(noTracking).SingleAsync(cancellationToken);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return DynamicEntities().SingleAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking, CancellationToken cancellationToken = default)
        {
            return DynamicEntities(noTracking).SingleAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <returns></returns>
        public virtual TEntity First()
        {
            return DynamicEntities().First();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity First(bool noTracking)
        {
            return DynamicEntities(noTracking).First();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity First(Expression<Func<TEntity, bool>> predicate)
        {
            return DynamicEntities().First(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity First(Expression<Func<TEntity, bool>> predicate, bool noTracking)
        {
            return DynamicEntities(noTracking).First(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <returns></returns>
        public virtual TEntity FirstOrDefault()
        {
            return DynamicEntities().FirstOrDefault();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity FirstOrDefault(bool noTracking)
        {
            return DynamicEntities(noTracking).FirstOrDefault();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return DynamicEntities().FirstOrDefault(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking)
        {
            return DynamicEntities(noTracking).FirstOrDefault(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FirstAsync(CancellationToken cancellationToken = default)
        {
            return DynamicEntities().FirstAsync(cancellationToken);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FirstAsync(bool noTracking, CancellationToken cancellationToken = default)
        {
            return DynamicEntities(noTracking).FirstAsync(cancellationToken);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return DynamicEntities().FirstAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking, CancellationToken cancellationToken = default)
        {
            return DynamicEntities(noTracking).FirstAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <returns></returns>
        public virtual TEntity Last()
        {
            return DynamicEntities().Last();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity Last(bool noTracking)
        {
            return DynamicEntities(noTracking).Last();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity Last(Expression<Func<TEntity, bool>> predicate)
        {
            return DynamicEntities().Last(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity Last(Expression<Func<TEntity, bool>> predicate, bool noTracking)
        {
            return DynamicEntities(noTracking).Last(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <returns></returns>
        public virtual TEntity LastOrDefault()
        {
            return DynamicEntities().LastOrDefault();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity LastOrDefault(bool noTracking)
        {
            return DynamicEntities(noTracking).LastOrDefault();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return DynamicEntities().LastOrDefault(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking)
        {
            return DynamicEntities(noTracking).LastOrDefault(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> LastAsync(CancellationToken cancellationToken = default)
        {
            return DynamicEntities().LastAsync(cancellationToken);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> LastAsync(bool noTracking, CancellationToken cancellationToken = default)
        {
            return DynamicEntities(noTracking).LastAsync(cancellationToken);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> LastAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return DynamicEntities().LastAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> LastAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking, CancellationToken cancellationToken = default)
        {
            return DynamicEntities(noTracking).LastAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Filter()
        {
            return CombineQueryable();
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Filter(bool noTracking)
        {
            return CombineQueryable(noTracking: noTracking);
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryFilters"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Filter(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true)
        {
            return CombineQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery);
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> expression)
        {
            return CombineQueryable(expression);
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            return CombineQueryable(expression, noTracking: noTracking);
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryFilters"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> expression = null, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true)
        {
            return CombineQueryable(expression, noTracking, ignoreQueryFilters, asSplitQuery);
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<List<TEntity>> FilterAsync(CancellationToken cancellationToken = default)
        {
            return CombineQueryable().ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<List<TEntity>> FilterAsync(bool noTracking, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(noTracking: noTracking).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryFilters"></param>
        /// <param name="asSplitQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<List<TEntity>> FilterAsync(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<List<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(expression).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<List<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> expression, bool noTracking, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(expression, noTracking: noTracking).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryFilters"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        public virtual Task<List<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> expression = null, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(expression, noTracking, ignoreQueryFilters, asSplitQuery).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual PagedList<TEntity> PagedFilter(int pageIndex, int pageSize)
        {
            return CombineQueryable().ToPagedList(pageIndex, pageSize);
        }

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual PagedList<TEntity> PagedFilter(int pageIndex, int pageSize, bool noTracking)
        {
            return CombineQueryable(noTracking: noTracking).ToPagedList(pageIndex, pageSize);
        }

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryPagedFilters"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        public virtual PagedList<TEntity> PagedFilter(int pageIndex, int pageSize, bool noTracking = true, bool ignoreQueryPagedFilters = false, bool asSplitQuery = true)
        {
            return CombineQueryable(null, noTracking, ignoreQueryPagedFilters, asSplitQuery).ToPagedList(pageIndex, pageSize);
        }

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual PagedList<TEntity> PagedFilter(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize)
        {
            return CombineQueryable(expression).ToPagedList(pageIndex, pageSize);
        }

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual PagedList<TEntity> PagedFilter(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize, bool noTracking)
        {
            return CombineQueryable(expression, noTracking: noTracking).ToPagedList(pageIndex, pageSize);
        }

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryPagedFilters"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        public virtual PagedList<TEntity> PagedFilter(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize, bool noTracking = true, bool ignoreQueryPagedFilters = false, bool asSplitQuery = true)
        {
            return CombineQueryable(expression, noTracking, ignoreQueryPagedFilters, asSplitQuery).ToPagedList(pageSize, pageIndex);
        }

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<PagedList<TEntity>> PagedFilterAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            return CombineQueryable().ToPagedListAsync(pageSize, pageIndex, cancellationToken);
        }

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<PagedList<TEntity>> PagedFilterAsync(int pageIndex, int pageSize, bool noTracking, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(noTracking: noTracking).ToPagedListAsync(pageIndex, pageSize, cancellationToken);
        }

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryPagedFilters"></param>
        /// <param name="asSplitQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<PagedList<TEntity>> PagedFilterAsync(int pageIndex, int pageSize, bool noTracking = true, bool ignoreQueryPagedFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(null, noTracking, ignoreQueryPagedFilters, asSplitQuery).ToPagedListAsync(pageIndex, pageSize, cancellationToken);
        }

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<PagedList<TEntity>> PagedFilterAsync(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(expression).ToPagedListAsync(pageIndex, pageSize, cancellationToken);
        }

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<PagedList<TEntity>> PagedFilterAsync(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize, bool noTracking, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(expression, noTracking: noTracking).ToPagedListAsync(pageIndex, pageSize, cancellationToken);
        }

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryPagedFilters"></param>
        /// <param name="asSplitQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<PagedList<TEntity>> PagedFilterAsync(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize, bool noTracking = true, bool ignoreQueryPagedFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(expression, noTracking, ignoreQueryPagedFilters, asSplitQuery).ToPagedListAsync(pageIndex, pageSize, cancellationToken);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <returns></returns>
        public virtual bool Any()
        {
            return CombineQueryable().Any();
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual bool Any(bool noTracking)
        {
            return CombineQueryable(noTracking: noTracking).Any();
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryAnys"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        public virtual bool Any(bool noTracking = true, bool ignoreQueryAnys = false, bool asSplitQuery = true)
        {
            return CombineQueryable(null, noTracking, ignoreQueryAnys, asSplitQuery).Any();
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual bool Any(Expression<Func<TEntity, bool>> expression)
        {
            return CombineQueryable().Any(expression);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual bool Any(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            return CombineQueryable(noTracking: noTracking).Any(expression);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryAnys"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        public virtual bool Any(Expression<Func<TEntity, bool>> expression = null, bool noTracking = true, bool ignoreQueryAnys = false, bool asSplitQuery = true)
        {
            return CombineQueryable(null, noTracking, ignoreQueryAnys, asSplitQuery).Any(expression);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return CombineQueryable().AnyAsync(cancellationToken);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> AnyAsync(bool noTracking, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(noTracking: noTracking).AnyAsync(cancellationToken);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryAnys"></param>
        /// <param name="asSplitQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> AnyAsync(bool noTracking = true, bool ignoreQueryAnys = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(null, noTracking, ignoreQueryAnys, asSplitQuery).AnyAsync(cancellationToken);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return CombineQueryable().AnyAsync(expression, cancellationToken);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, bool noTracking, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(noTracking: noTracking).AnyAsync(expression, cancellationToken);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryAnys"></param>
        /// <param name="asSplitQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null, bool noTracking = true, bool ignoreQueryAnys = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(null, noTracking, ignoreQueryAnys, asSplitQuery).AnyAsync(expression, cancellationToken);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            return CombineQueryable().Count();
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual int Count(bool noTracking)
        {
            return CombineQueryable(noTracking: noTracking).Count();
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryCounts"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        public virtual int Count(bool noTracking = true, bool ignoreQueryCounts = false, bool asSplitQuery = true)
        {
            return CombineQueryable(null, noTracking, ignoreQueryCounts, asSplitQuery).Count();
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<TEntity, bool>> expression)
        {
            return CombineQueryable().Count(expression);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            return CombineQueryable(noTracking: noTracking).Count(expression);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryCounts"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<TEntity, bool>> expression = null, bool noTracking = true, bool ignoreQueryCounts = false, bool asSplitQuery = true)
        {
            return CombineQueryable(null, noTracking, ignoreQueryCounts, asSplitQuery).Count(expression);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return CombineQueryable().CountAsync(cancellationToken);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> CountAsync(bool noTracking, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(noTracking: noTracking).CountAsync(cancellationToken);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryCounts"></param>
        /// <param name="asSplitQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> CountAsync(bool noTracking = true, bool ignoreQueryCounts = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(null, noTracking, ignoreQueryCounts, asSplitQuery).CountAsync(cancellationToken);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return CombineQueryable().CountAsync(expression, cancellationToken);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> expression, bool noTracking, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(noTracking: noTracking).CountAsync(expression, cancellationToken);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryCounts"></param>
        /// <param name="asSplitQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null, bool noTracking = true, bool ignoreQueryCounts = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(null, noTracking, ignoreQueryCounts, asSplitQuery).CountAsync(expression, cancellationToken);
        }

        /// <summary>
        /// 查看最小值
        /// </summary>
        /// <returns></returns>
        public virtual TEntity Min()
        {
            return CombineQueryable().Min();
        }

        /// <summary>
        /// 查看最小值
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity Min(bool noTracking)
        {
            return CombineQueryable(noTracking: noTracking).Min();
        }

        /// <summary>
        /// 查看最小值
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryMins"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        public virtual TEntity Min(bool noTracking = true, bool ignoreQueryMins = false, bool asSplitQuery = true)
        {
            return CombineQueryable(null, noTracking, ignoreQueryMins, asSplitQuery).Min();
        }

        /// <summary>
        /// 查看最小值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return CombineQueryable().Min(expression);
        }

        /// <summary>
        /// 查看最小值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression, bool noTracking)
        {
            return CombineQueryable(noTracking: noTracking).Min(expression);
        }

        /// <summary>
        /// 查看最小值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryMins"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        public virtual TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression = null, bool noTracking = true, bool ignoreQueryMins = false, bool asSplitQuery = true)
        {
            return CombineQueryable(null, noTracking, ignoreQueryMins, asSplitQuery).Min(expression);
        }

        /// <summary>
        /// 查看最小值
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> MinAsync(CancellationToken cancellationToken = default)
        {
            return CombineQueryable().MinAsync(cancellationToken);
        }

        /// <summary>
        /// 查看最小值
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> MinAsync(bool noTracking, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(noTracking: noTracking).MinAsync(cancellationToken);
        }

        /// <summary>
        /// 查看最小值
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryMins"></param>
        /// <param name="asSplitQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> MinAsync(bool noTracking = true, bool ignoreQueryMins = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(null, noTracking, ignoreQueryMins, asSplitQuery).MinAsync(cancellationToken);
        }

        /// <summary>
        /// 查看最小值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression, CancellationToken cancellationToken = default)
        {
            return CombineQueryable().MinAsync(expression, cancellationToken);
        }

        /// <summary>
        /// 查看最小值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression, bool noTracking, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(noTracking: noTracking).MinAsync(expression, cancellationToken);
        }

        /// <summary>
        /// 查看最小值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryMins"></param>
        /// <param name="asSplitQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression = null, bool noTracking = true, bool ignoreQueryMins = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(null, noTracking, ignoreQueryMins, asSplitQuery).MinAsync(expression, cancellationToken);
        }

        /// <summary>
        /// 查看最大值
        /// </summary>
        /// <returns></returns>
        public virtual TEntity Max()
        {
            return CombineQueryable().Max();
        }

        /// <summary>
        /// 查看最大值
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity Max(bool noTracking)
        {
            return CombineQueryable(noTracking: noTracking).Max();
        }

        /// <summary>
        /// 查看最大值
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryMaxs"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        public virtual TEntity Max(bool noTracking = true, bool ignoreQueryMaxs = false, bool asSplitQuery = true)
        {
            return CombineQueryable(null, noTracking, ignoreQueryMaxs, asSplitQuery).Max();
        }

        /// <summary>
        /// 查看最大值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return CombineQueryable().Max(expression);
        }

        /// <summary>
        /// 查看最大值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression, bool noTracking)
        {
            return CombineQueryable(noTracking: noTracking).Max(expression);
        }

        /// <summary>
        /// 查看最大值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryMaxs"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        public virtual TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression = null, bool noTracking = true, bool ignoreQueryMaxs = false, bool asSplitQuery = true)
        {
            return CombineQueryable(null, noTracking, ignoreQueryMaxs, asSplitQuery).Max(expression);
        }

        /// <summary>
        /// 查看最大值
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> MaxAsync(CancellationToken cancellationToken = default)
        {
            return CombineQueryable().MaxAsync(cancellationToken);
        }

        /// <summary>
        /// 查看最大值
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> MaxAsync(bool noTracking, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(noTracking: noTracking).MaxAsync(cancellationToken);
        }

        /// <summary>
        /// 查看最大值
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryMaxs"></param>
        /// <param name="asSplitQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> MaxAsync(bool noTracking = true, bool ignoreQueryMaxs = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(null, noTracking, ignoreQueryMaxs, asSplitQuery).MaxAsync(cancellationToken);
        }

        /// <summary>
        /// 查看最大值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression, CancellationToken cancellationToken = default)
        {
            return CombineQueryable().MaxAsync(expression, cancellationToken);
        }

        /// <summary>
        /// 查看最大值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression, bool noTracking, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(noTracking: noTracking).MaxAsync(expression, cancellationToken);
        }

        /// <summary>
        /// 查看最大值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryMaxs"></param>
        /// <param name="asSplitQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression = null, bool noTracking = true, bool ignoreQueryMaxs = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return CombineQueryable(null, noTracking, ignoreQueryMaxs, asSplitQuery).MaxAsync(expression, cancellationToken);
        }

        /// <summary>
        /// 动态实体
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        private IQueryable<TEntity> DynamicEntities(bool noTracking = false)
        {
            return !noTracking ? Entities : DerailEntities;
        }

        /// <summary>
        /// 组合查询
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryFilters"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        private IQueryable<TEntity> CombineQueryable(Expression<Func<TEntity, bool>> expression = null, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true)
        {
            var entities = DynamicEntities(noTracking);
            if (ignoreQueryFilters) entities = entities.IgnoreQueryFilters();
            if (asSplitQuery) entities = entities.AsSplitQuery();
            if (expression != null) entities = entities.Where(expression);

            return entities;
        }
    }
}