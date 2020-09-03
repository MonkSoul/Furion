// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using Fur.FriendlyException;
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
    /// 可写仓储分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial class EFCoreRepository<TEntity>
         where TEntity : class, IEntityBase, new()
    {
        /// <summary>
        /// 未查询到数据异常消息
        /// </summary>
        private const string NotFoundErrorMessage = "Sequence contains no elements";

        /// <summary>
        /// 根据键查询一条记录
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>数据库中的实体</returns>
        public virtual TEntity Find(object key)
        {
            var entity = FindOrDefault(key) ?? throw Oops.Oh(NotFoundErrorMessage, typeof(InvalidOperationException));
            return entity;
        }

        /// <summary>
        /// 根据多个键查询一条记录
        /// </summary>
        /// <param name="keyValues">多个键</param>
        /// <returns>数据库中的实体</returns>
        public virtual TEntity Find(params object[] keyValues)
        {
            var entity = FindOrDefault(keyValues) ?? throw Oops.Oh(NotFoundErrorMessage, typeof(InvalidOperationException));
            return entity;
        }

        /// <summary>
        /// 根据键查询一条记录
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="cancellationToken">异步令牌</param>
        /// <returns>数据库实体</returns>
        public virtual async Task<TEntity> FindAsync(object key, CancellationToken cancellationToken = default)
        {
            var entity = await FindOrDefaultAsync(key, cancellationToken);
            return entity ?? throw Oops.Oh(NotFoundErrorMessage, typeof(InvalidOperationException));
        }

        /// <summary>
        /// 根据多个键查询一条记录
        /// </summary>
        /// <param name="keyValues">多个键</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        {
            var entity = await FindOrDefaultAsync(keyValues);
            return entity ?? throw Oops.Oh(NotFoundErrorMessage, typeof(InvalidOperationException));
        }

        /// <summary>
        /// 根据多个键查询一条记录
        /// </summary>
        /// <param name="keyValues">多个键</param>
        /// <param name="cancellationToken">异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken = default)
        {
            var entity = await FindOrDefaultAsync(keyValues, cancellationToken);
            return entity ?? throw Oops.Oh(NotFoundErrorMessage, typeof(InvalidOperationException));
        }

        /// <summary>
        /// 根据键查询一条记录
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>数据库中的实体</returns>
        public virtual TEntity FindOrDefault(object key)
        {
            return Entities.Find(key);
        }

        /// <summary>
        /// 根据多个键查询一条记录
        /// </summary>
        /// <param name="keyValues">多个键</param>
        /// <returns>数据库中的实体</returns>
        public virtual TEntity FindOrDefault(params object[] keyValues)
        {
            return Entities.Find(keyValues);
        }

        /// <summary>
        /// 根据键查询一条记录
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="cancellationToken">异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<TEntity> FindOrDefaultAsync(object key, CancellationToken cancellationToken = default)
        {
            var entity = await Entities.FindAsync(new object[] { key }, cancellationToken);
            return entity;
        }

        /// <summary>
        /// 根据多个键查询一条记录
        /// </summary>
        /// <param name="keyValues">多个键</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<TEntity> FindOrDefaultAsync(params object[] keyValues)
        {
            var entity = await Entities.FindAsync(keyValues);
            return entity;
        }

        /// <summary>
        /// 根据多个键查询一条记录
        /// </summary>
        /// <param name="keyValues">多个键</param>
        /// <param name="cancellationToken">异步令牌</param>
        /// <returns>数据库中的实体</returns>
        public virtual async Task<TEntity> FindOrDefaultAsync(object[] keyValues, CancellationToken cancellationToken = default)
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
            return AsQueryable().Single();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity Single(bool noTracking)
        {
            return AsQueryable(noTracking).Single();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return AsQueryable().Single(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate, bool noTracking)
        {
            return AsQueryable(noTracking).Single(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <returns></returns>
        public virtual TEntity SingleOrDefault()
        {
            return AsQueryable().SingleOrDefault();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity SingleOrDefault(bool noTracking)
        {
            return AsQueryable(noTracking).SingleOrDefault();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return AsQueryable().SingleOrDefault(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking)
        {
            return AsQueryable(noTracking).SingleOrDefault(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> SingleAsync(CancellationToken cancellationToken = default)
        {
            return AsQueryable().SingleAsync(cancellationToken);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> SingleAsync(bool noTracking, CancellationToken cancellationToken = default)
        {
            return AsQueryable(noTracking).SingleAsync(cancellationToken);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return AsQueryable().SingleAsync(predicate, cancellationToken);
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
            return AsQueryable(noTracking).SingleAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <returns></returns>
        public virtual TEntity First()
        {
            return AsQueryable().First();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity First(bool noTracking)
        {
            return AsQueryable(noTracking).First();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity First(Expression<Func<TEntity, bool>> predicate)
        {
            return AsQueryable().First(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity First(Expression<Func<TEntity, bool>> predicate, bool noTracking)
        {
            return AsQueryable(noTracking).First(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <returns></returns>
        public virtual TEntity FirstOrDefault()
        {
            return AsQueryable().FirstOrDefault();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity FirstOrDefault(bool noTracking)
        {
            return AsQueryable(noTracking).FirstOrDefault();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return AsQueryable().FirstOrDefault(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking)
        {
            return AsQueryable(noTracking).FirstOrDefault(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FirstAsync(CancellationToken cancellationToken = default)
        {
            return AsQueryable().FirstAsync(cancellationToken);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FirstAsync(bool noTracking, CancellationToken cancellationToken = default)
        {
            return AsQueryable(noTracking).FirstAsync(cancellationToken);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return AsQueryable().FirstAsync(predicate, cancellationToken);
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
            return AsQueryable(noTracking).FirstAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <returns></returns>
        public virtual TEntity Last()
        {
            return AsQueryable().Last();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity Last(bool noTracking)
        {
            return AsQueryable(noTracking).Last();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity Last(Expression<Func<TEntity, bool>> predicate)
        {
            return AsQueryable().Last(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity Last(Expression<Func<TEntity, bool>> predicate, bool noTracking)
        {
            return AsQueryable(noTracking).Last(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <returns></returns>
        public virtual TEntity LastOrDefault()
        {
            return AsQueryable().LastOrDefault();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity LastOrDefault(bool noTracking)
        {
            return AsQueryable(noTracking).LastOrDefault();
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return AsQueryable().LastOrDefault(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking)
        {
            return AsQueryable(noTracking).LastOrDefault(predicate);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> LastAsync(CancellationToken cancellationToken = default)
        {
            return AsQueryable().LastAsync(cancellationToken);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> LastAsync(bool noTracking, CancellationToken cancellationToken = default)
        {
            return AsQueryable(noTracking).LastAsync(cancellationToken);
        }

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> LastAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return AsQueryable().LastAsync(predicate, cancellationToken);
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
            return AsQueryable(noTracking).LastAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Filter()
        {
            return AsQueryable();
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Filter(bool noTracking)
        {
            return AsQueryable(noTracking: noTracking);
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
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery);
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> expression)
        {
            return AsQueryable(expression);
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            return AsQueryable(expression, noTracking: noTracking);
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryFilters"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true)
        {
            return AsQueryable(expression, noTracking, ignoreQueryFilters, asSplitQuery);
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<List<TEntity>> FilterAsync(CancellationToken cancellationToken = default)
        {
            return AsQueryable().ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<List<TEntity>> FilterAsync(bool noTracking, CancellationToken cancellationToken = default)
        {
            return AsQueryable(noTracking: noTracking).ToListAsync(cancellationToken);
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
            return AsQueryable(null, noTracking, ignoreQueryFilters, asSplitQuery).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<List<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return AsQueryable(expression).ToListAsync(cancellationToken);
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
            return AsQueryable(expression, noTracking: noTracking).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryFilters"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        public virtual Task<List<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return AsQueryable(expression, noTracking, ignoreQueryFilters, asSplitQuery).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual PagedList<TEntity> PagedFilter(int pageIndex, int pageSize)
        {
            return AsQueryable().ToPagedList(pageIndex, pageSize);
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
            return AsQueryable(noTracking: noTracking).ToPagedList(pageIndex, pageSize);
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
            return AsQueryable(null, noTracking, ignoreQueryPagedFilters, asSplitQuery).ToPagedList(pageIndex, pageSize);
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
            return AsQueryable(expression).ToPagedList(pageIndex, pageSize);
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
            return AsQueryable(expression, noTracking: noTracking).ToPagedList(pageIndex, pageSize);
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
            return AsQueryable(expression, noTracking, ignoreQueryPagedFilters, asSplitQuery).ToPagedList(pageSize, pageIndex);
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
            return AsQueryable().ToPagedListAsync(pageSize, pageIndex, cancellationToken);
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
            return AsQueryable(noTracking: noTracking).ToPagedListAsync(pageIndex, pageSize, cancellationToken);
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
            return AsQueryable(null, noTracking, ignoreQueryPagedFilters, asSplitQuery).ToPagedListAsync(pageIndex, pageSize, cancellationToken);
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
            return AsQueryable(expression).ToPagedListAsync(pageIndex, pageSize, cancellationToken);
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
            return AsQueryable(expression, noTracking: noTracking).ToPagedListAsync(pageIndex, pageSize, cancellationToken);
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
            return AsQueryable(expression, noTracking, ignoreQueryPagedFilters, asSplitQuery).ToPagedListAsync(pageIndex, pageSize, cancellationToken);
        }

        /// <summary>
        /// 查看记录是否存在
        /// </summary>
        /// <returns></returns>
        public virtual bool Any()
        {
            return AsQueryable().Any();
        }

        /// <summary>
        /// 查看记录是否存在
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual bool Any(bool noTracking)
        {
            return AsQueryable(noTracking: noTracking).Any();
        }

        /// <summary>
        /// 查看记录是否存在
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryAnys"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        public virtual bool Any(bool noTracking = true, bool ignoreQueryAnys = false, bool asSplitQuery = true)
        {
            return AsQueryable(null, noTracking, ignoreQueryAnys, asSplitQuery).Any();
        }

        /// <summary>
        /// 查看记录是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual bool Any(Expression<Func<TEntity, bool>> expression)
        {
            return AsQueryable().Any(expression);
        }

        /// <summary>
        /// 查看记录是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual bool Any(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            return AsQueryable(noTracking: noTracking).Any(expression);
        }

        /// <summary>
        /// 查看记录是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryAnys"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        public virtual bool Any(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryAnys = false, bool asSplitQuery = true)
        {
            return AsQueryable(null, noTracking, ignoreQueryAnys, asSplitQuery).Any(expression);
        }

        /// <summary>
        /// 查看记录是否存在
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return AsQueryable().AnyAsync(cancellationToken);
        }

        /// <summary>
        /// 查看记录是否存在
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> AnyAsync(bool noTracking, CancellationToken cancellationToken = default)
        {
            return AsQueryable(noTracking: noTracking).AnyAsync(cancellationToken);
        }

        /// <summary>
        /// 查看记录是否存在
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryAnys"></param>
        /// <param name="asSplitQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> AnyAsync(bool noTracking = true, bool ignoreQueryAnys = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return AsQueryable(null, noTracking, ignoreQueryAnys, asSplitQuery).AnyAsync(cancellationToken);
        }

        /// <summary>
        /// 查看记录是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return AsQueryable().AnyAsync(expression, cancellationToken);
        }

        /// <summary>
        /// 查看记录是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, bool noTracking, CancellationToken cancellationToken = default)
        {
            return AsQueryable(noTracking: noTracking).AnyAsync(expression, cancellationToken);
        }

        /// <summary>
        /// 查看记录是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryAnys"></param>
        /// <param name="asSplitQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryAnys = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return AsQueryable(null, noTracking, ignoreQueryAnys, asSplitQuery).AnyAsync(expression, cancellationToken);
        }

        /// <summary>
        /// 查看记录是否全部满足某个条件
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual bool All(Expression<Func<TEntity, bool>> expression)
        {
            return AsQueryable().All(expression);
        }

        /// <summary>
        /// 查看记录是否全部满足某个条件
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual bool All(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            return AsQueryable(noTracking: noTracking).All(expression);
        }

        /// <summary>
        /// 查看记录是否全部满足某个条件
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryAlls"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        public virtual bool All(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryAlls = false, bool asSplitQuery = true)
        {
            return AsQueryable(null, noTracking, ignoreQueryAlls, asSplitQuery).All(expression);
        }

        /// <summary>
        /// 查看记录是否全部满足某个条件
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> AllAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return AsQueryable().AllAsync(expression, cancellationToken);
        }

        /// <summary>
        /// 查看记录是否全部满足某个条件
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> AllAsync(Expression<Func<TEntity, bool>> expression, bool noTracking, CancellationToken cancellationToken = default)
        {
            return AsQueryable(noTracking: noTracking).AllAsync(expression, cancellationToken);
        }

        /// <summary>
        /// 查看记录是否全部满足某个条件
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryAlls"></param>
        /// <param name="asSplitQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> AllAsync(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryAlls = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return AsQueryable(null, noTracking, ignoreQueryAlls, asSplitQuery).AllAsync(expression, cancellationToken);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            return AsQueryable().Count();
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual int Count(bool noTracking)
        {
            return AsQueryable(noTracking: noTracking).Count();
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
            return AsQueryable(null, noTracking, ignoreQueryCounts, asSplitQuery).Count();
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<TEntity, bool>> expression)
        {
            return AsQueryable().Count(expression);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<TEntity, bool>> expression, bool noTracking)
        {
            return AsQueryable(noTracking: noTracking).Count(expression);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryCounts"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryCounts = false, bool asSplitQuery = true)
        {
            return AsQueryable(null, noTracking, ignoreQueryCounts, asSplitQuery).Count(expression);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return AsQueryable().CountAsync(cancellationToken);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> CountAsync(bool noTracking, CancellationToken cancellationToken = default)
        {
            return AsQueryable(noTracking: noTracking).CountAsync(cancellationToken);
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
            return AsQueryable(null, noTracking, ignoreQueryCounts, asSplitQuery).CountAsync(cancellationToken);
        }

        /// <summary>
        /// 查看记录条数
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return AsQueryable().CountAsync(expression, cancellationToken);
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
            return AsQueryable(noTracking: noTracking).CountAsync(expression, cancellationToken);
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
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> expression, bool noTracking = true, bool ignoreQueryCounts = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return AsQueryable(null, noTracking, ignoreQueryCounts, asSplitQuery).CountAsync(expression, cancellationToken);
        }

        /// <summary>
        /// 查看最小值
        /// </summary>
        /// <returns></returns>
        public virtual TEntity Min()
        {
            return AsQueryable().Min();
        }

        /// <summary>
        /// 查看最小值
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity Min(bool noTracking)
        {
            return AsQueryable(noTracking: noTracking).Min();
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
            return AsQueryable(null, noTracking, ignoreQueryMins, asSplitQuery).Min();
        }

        /// <summary>
        /// 查看最小值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return AsQueryable().Min(expression);
        }

        /// <summary>
        /// 查看最小值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression, bool noTracking)
        {
            return AsQueryable(noTracking: noTracking).Min(expression);
        }

        /// <summary>
        /// 查看最小值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryMins"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        public virtual TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression, bool noTracking = true, bool ignoreQueryMins = false, bool asSplitQuery = true)
        {
            return AsQueryable(null, noTracking, ignoreQueryMins, asSplitQuery).Min(expression);
        }

        /// <summary>
        /// 查看最小值
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> MinAsync(CancellationToken cancellationToken = default)
        {
            return AsQueryable().MinAsync(cancellationToken);
        }

        /// <summary>
        /// 查看最小值
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> MinAsync(bool noTracking, CancellationToken cancellationToken = default)
        {
            return AsQueryable(noTracking: noTracking).MinAsync(cancellationToken);
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
            return AsQueryable(null, noTracking, ignoreQueryMins, asSplitQuery).MinAsync(cancellationToken);
        }

        /// <summary>
        /// 查看最小值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression, CancellationToken cancellationToken = default)
        {
            return AsQueryable().MinAsync(expression, cancellationToken);
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
            return AsQueryable(noTracking: noTracking).MinAsync(expression, cancellationToken);
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
        public virtual Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression, bool noTracking = true, bool ignoreQueryMins = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return AsQueryable(null, noTracking, ignoreQueryMins, asSplitQuery).MinAsync(expression, cancellationToken);
        }

        /// <summary>
        /// 查看最大值
        /// </summary>
        /// <returns></returns>
        public virtual TEntity Max()
        {
            return AsQueryable().Max();
        }

        /// <summary>
        /// 查看最大值
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TEntity Max(bool noTracking)
        {
            return AsQueryable(noTracking: noTracking).Max();
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
            return AsQueryable(null, noTracking, ignoreQueryMaxs, asSplitQuery).Max();
        }

        /// <summary>
        /// 查看最大值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            return AsQueryable().Max(expression);
        }

        /// <summary>
        /// 查看最大值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        public virtual TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression, bool noTracking)
        {
            return AsQueryable(noTracking: noTracking).Max(expression);
        }

        /// <summary>
        /// 查看最大值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryMaxs"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        public virtual TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression, bool noTracking = true, bool ignoreQueryMaxs = false, bool asSplitQuery = true)
        {
            return AsQueryable(null, noTracking, ignoreQueryMaxs, asSplitQuery).Max(expression);
        }

        /// <summary>
        /// 查看最大值
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> MaxAsync(CancellationToken cancellationToken = default)
        {
            return AsQueryable().MaxAsync(cancellationToken);
        }

        /// <summary>
        /// 查看最大值
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TEntity> MaxAsync(bool noTracking, CancellationToken cancellationToken = default)
        {
            return AsQueryable(noTracking: noTracking).MaxAsync(cancellationToken);
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
            return AsQueryable(null, noTracking, ignoreQueryMaxs, asSplitQuery).MaxAsync(cancellationToken);
        }

        /// <summary>
        /// 查看最大值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression, CancellationToken cancellationToken = default)
        {
            return AsQueryable().MaxAsync(expression, cancellationToken);
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
            return AsQueryable(noTracking: noTracking).MaxAsync(expression, cancellationToken);
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
        public virtual Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression, bool noTracking = true, bool ignoreQueryMaxs = false, bool asSplitQuery = true, CancellationToken cancellationToken = default)
        {
            return AsQueryable(null, noTracking, ignoreQueryMaxs, asSplitQuery).MaxAsync(expression, cancellationToken);
        }
    }
}