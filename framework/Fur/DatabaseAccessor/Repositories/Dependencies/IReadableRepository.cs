// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 可读仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IReadableRepository<TEntity>
        where TEntity : class, IDbEntityBase, new()
    {
        /// <summary>
        /// 根据主键查找
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TEntity Find(object key);

        /// <summary>
        /// 根据多个主键查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        TEntity Find(params object[] keyValues);

        /// <summary>
        /// 根据主键查找
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync(object key, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据多个主键查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync(params object[] keyValues);

        /// <summary>
        /// 根据多个主键查找
        /// </summary>
        /// <param name="keyValues"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <returns></returns>
        TEntity Single();

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        TEntity Single(bool noTracking);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        TEntity Single(Expression<Func<TEntity, bool>> predicate, bool noTracking);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <returns></returns>
        TEntity SingleOrDefault();

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        TEntity SingleOrDefault(bool noTracking);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> SingleAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> SingleAsync(bool noTracking, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <returns></returns>
        TEntity First();

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        TEntity First(bool noTracking);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity First(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        TEntity First(Expression<Func<TEntity, bool>> predicate, bool noTracking);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <returns></returns>
        TEntity FirstOrDefault();

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        TEntity FirstOrDefault(bool noTracking);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FirstAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FirstAsync(bool noTracking, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <returns></returns>
        TEntity Last();

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        TEntity Last(bool noTracking);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity Last(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        TEntity Last(Expression<Func<TEntity, bool>> predicate, bool noTracking);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <returns></returns>
        TEntity LastOrDefault();

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        TEntity LastOrDefault(bool noTracking);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> LastAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> LastAsync(bool noTracking, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> LastAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取一条
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> LastAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> Filter();

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        IQueryable<TEntity> Filter(bool noTracking);

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryFilters"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        IQueryable<TEntity> Filter(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = false);

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> expression, bool noTracking);

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryFilters"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> expression = null, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = false);

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<TEntity>> FilterAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<TEntity>> FilterAsync(bool noTracking, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryFilters"></param>
        /// <param name="asSplitQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<TEntity>> FilterAsync(bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> expression, bool noTracking, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryFilters"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        Task<List<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> expression = null, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagedList<TEntity> PagedFilter(int pageIndex, int pageSize);

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        PagedList<TEntity> PagedFilter(int pageIndex, int pageSize, bool noTracking);

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryPagedFilters"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        PagedList<TEntity> PagedFilter(int pageIndex, int pageSize, bool noTracking = true, bool ignoreQueryPagedFilters = false, bool asSplitQuery = false);

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagedList<TEntity> PagedFilter(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize);

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        PagedList<TEntity> PagedFilter(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize, bool noTracking);

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
        PagedList<TEntity> PagedFilter(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize, bool noTracking = true, bool ignoreQueryPagedFilters = false, bool asSplitQuery = false);

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagedList<TEntity>> PagedFilterAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagedList<TEntity>> PagedFilterAsync(int pageIndex, int pageSize, bool noTracking, CancellationToken cancellationToken = default);

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
        Task<PagedList<TEntity>> PagedFilterAsync(int pageIndex, int pageSize, bool noTracking = true, bool ignoreQueryPagedFilters = false, bool asSplitQuery = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagedList<TEntity>> PagedFilterAsync(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        /// <summary>
        /// 分页查询多条
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagedList<TEntity>> PagedFilterAsync(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize, bool noTracking, CancellationToken cancellationToken = default);

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
        Task<PagedList<TEntity>> PagedFilterAsync(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize, bool noTracking = true, bool ignoreQueryPagedFilters = false, bool asSplitQuery = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <returns></returns>
        bool Any();

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        bool Any(bool noTracking);

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryAnys"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        bool Any(bool noTracking = true, bool ignoreQueryAnys = false, bool asSplitQuery = false);

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        bool Any(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        bool Any(Expression<Func<TEntity, bool>> expression, bool noTracking);

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryAnys"></param>
        /// <param name="asSplitQuery"></param>
        /// <returns></returns>
        bool Any(Expression<Func<TEntity, bool>> expression = null, bool noTracking = true, bool ignoreQueryAnys = false, bool asSplitQuery = false);

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(bool noTracking, CancellationToken cancellationToken = default);

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryAnys"></param>
        /// <param name="asSplitQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(bool noTracking = true, bool ignoreQueryAnys = false, bool asSplitQuery = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, bool noTracking, CancellationToken cancellationToken = default);

        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="noTracking"></param>
        /// <param name="ignoreQueryAnys"></param>
        /// <param name="asSplitQuery"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null, bool noTracking = true, bool ignoreQueryAnys = false, bool asSplitQuery = false, CancellationToken cancellationToken = default);
    }
}