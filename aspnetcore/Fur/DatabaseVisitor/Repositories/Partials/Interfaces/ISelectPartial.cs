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
    /// 查询操作分部类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        TEntity Find(object id);

        ValueTask<TEntity> FindAsync(object id);

        TEntity Single();

        TEntity Single(bool noTracking);

        Task<TEntity> SingleAsync();

        Task<TEntity> SingleAsync(bool noTracking);

        TEntity Single(Expression<Func<TEntity, bool>> expression);

        TEntity Single(Expression<Func<TEntity, bool>> expression, bool noTracking);

        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression, bool noTracking);

        TEntity SingleOrDefault();

        TEntity SingleOrDefault(bool noTracking);

        Task<TEntity> SingleOrDefaultAsync();

        Task<TEntity> SingleOrDefaultAsync(bool noTracking);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression, bool noTracking);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool noTracking);

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
