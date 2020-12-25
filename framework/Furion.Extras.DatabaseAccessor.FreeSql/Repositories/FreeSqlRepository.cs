using FreeSql;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 非泛型 FreeSql 仓储
    /// </summary>
    public partial class FreeSqlRepository : IFreeSqlRepository
    {
        /// <summary>
        /// 服务提供器
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        public FreeSqlRepository(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 切换仓储
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns>仓储</returns>
        public virtual IFreeSqlRepository<TEntity> Change<TEntity>()
            where TEntity : class, new()
        {
            return _serviceProvider.GetService<IFreeSqlRepository<TEntity>>();
        }
    }

    /// <summary>
    /// FreeSql 仓储实现类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class FreeSqlRepository<TEntity> : IFreeSqlRepository<TEntity>
    where TEntity : class, new()
    {
        /// <summary>
        /// 非泛型 FreeSql 仓储
        /// </summary>
        private readonly IFreeSqlRepository _freeSqlRepository;

        /// <summary>
        /// 初始化 FreeSql 客户端
        /// </summary>
        private readonly IFreeSql _db;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="freeSqlRepository"></param>
        /// <param name="db"></param>
        public FreeSqlRepository(IFreeSqlRepository freeSqlRepository, IFreeSql db)
        {
            _freeSqlRepository = freeSqlRepository;
            DynamicContext = Context = _db = db;

            Entities = _db.Select<TEntity>();
            Ado = _db.Ado;
        }

        /// <summary>
        /// 实体集合
        /// </summary>
        public virtual ISelect<TEntity> Entities { get; }

        /// <summary>
        /// 数据库上下文
        /// </summary>
        public virtual IFreeSql Context { get; }

        /// <summary>
        /// 动态数据库上下文
        /// </summary>
        public virtual dynamic DynamicContext { get; }

        /// <summary>
        /// 原生 Ado 对象
        /// </summary>
        public virtual IAdo Ado { get; }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <param name="expression">条件</param>
        /// <returns></returns>
        public long Count(Expression<Func<TEntity, bool>> expression)
        {
            _db.Select<TEntity>().Where(expression).Count(out long total);
            return total;
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <param name="expression">条件</param>
        /// <returns></returns>
        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _db.Select<TEntity>().Where(expression).CountAsync();
        }

        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool Any(Expression<Func<TEntity, bool>> expression)
        {
            return _db.Select<TEntity>().Any(expression);
        }

        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _db.Select<TEntity>().AnyAsync(expression);
        }

        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Single(dynamic id)
        {
            return _db.Select<TEntity>().WhereDynamic(id).ToOne();
        }

        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> SingleAsync(dynamic id)
        {
            return await _db.Select<TEntity>().WhereDynamic(id).ToOneAsync();
        }

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Insert(TEntity entity)
        {
            return _db.Insert(entity).ExecuteAffrows();
        }

        /// <summary>
        /// 新增多条记录
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int Insert(params TEntity[] entities)
        {
            return _db.Insert(entities).ExecuteAffrows();
        }

        /// <summary>
        /// 新增多条记录
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int Insert(IEnumerable<TEntity> entities)
        {
            return _db.Insert(entities.ToArray()).ExecuteAffrows();
        }

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task<int> InsertAsync(TEntity entity)
        {
            return _db.Insert(entity).ExecuteAffrowsAsync();
        }

        /// <summary>
        /// 新增多条记录
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual Task<int> InsertAsync(params TEntity[] entities)
        {
            return _db.Insert(entities).ExecuteAffrowsAsync();
        }

        /// <summary>
        /// 新增多条记录
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual Task<int> InsertAsync(IEnumerable<TEntity> entities)
        {
            return _db.Insert(entities.ToArray()).ExecuteAffrowsAsync();
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Update(TEntity entity)
        {
            return _db.Update<TEntity>(entity).ExecuteAffrows();
        }

        /// <summary>
        /// 更新多条记录
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int Update(params TEntity[] entities)
        {
            return _db.Update<TEntity>(entities).ExecuteAffrows();
        }

        /// <summary>
        /// 新增多条记录
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int Update(IEnumerable<TEntity> entities)
        {
            return _db.Update<TEntity>(entities.ToArray()).ExecuteAffrows();
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task<int> UpdateAsync(TEntity entity)
        {
            return _db.Update<TEntity>(entity).ExecuteAffrowsAsync();
        }

        /// <summary>
        /// 更新多条记录
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual Task<int> UpdateAsync(params TEntity[] entities)
        {
            return _db.Update<TEntity>(entities).ExecuteAffrowsAsync();
        }

        /// <summary>
        /// 更新多条记录
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual Task<int> UpdateAsync(IEnumerable<TEntity> entities)
        {
            return _db.Update<TEntity>(entities.ToArray()).ExecuteAffrowsAsync();
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Delete(TEntity entity)
        {
            return _db.Delete<TEntity>(entity).ExecuteAffrows();
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual int Delete(object key)
        {
            return _db.Delete<TEntity>(new[] { key }).ExecuteAffrows();
        }

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public virtual int Delete(params object[] keys)
        {
            return _db.Delete<TEntity>(keys).ExecuteAffrows();
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task<int> DeleteAsync(TEntity entity)
        {
            return _db.Delete<TEntity>(entity).ExecuteAffrowsAsync();
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual Task<int> DeleteAsync(object key)
        {
            return _db.Delete<TEntity>(new[] { key }).ExecuteAffrowsAsync();
        }

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public virtual Task<int> DeleteAsync(params object[] keys)
        {
            return _db.Delete<TEntity>(keys).ExecuteAffrowsAsync();
        }

        /// <summary>
        /// 根据表达式查询多条记录
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual ISelect<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return AsQueryable(predicate);
        }

        /// <summary>
        /// 根据表达式查询多条记录
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual ISelect<TEntity> Where(bool condition, Expression<Func<TEntity, bool>> predicate)
        {
            return AsQueryable().WhereIf(condition, predicate);
        }

        /// <summary>
        /// 构建查询分析器
        /// </summary>
        /// <returns></returns>
        public virtual ISelect<TEntity> AsQueryable()
        {
            return Entities;
        }

        /// <summary>
        /// 构建查询分析器
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual ISelect<TEntity> AsQueryable(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate);
        }

        /// <summary>
        /// 直接返回数据库结果
        /// </summary>
        /// <returns></returns>
        public virtual List<TEntity> AsEnumerable()
        {
            return AsQueryable().ToList();
        }

        /// <summary>
        /// 直接返回数据库结果
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual List<TEntity> AsEnumerable(Expression<Func<TEntity, bool>> predicate)
        {
            return AsQueryable(predicate).ToList();
        }

        /// <summary>
        /// 直接返回数据库结果
        /// </summary>
        /// <returns></returns>
        public virtual Task<List<TEntity>> AsAsyncEnumerable()
        {
            return AsQueryable().ToListAsync();
        }

        /// <summary>
        /// 直接返回数据库结果
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual Task<List<TEntity>> AsAsyncEnumerable(Expression<Func<TEntity, bool>> predicate)
        {
            return AsQueryable(predicate).ToListAsync();
        }

        /// <summary>
        /// 切换仓储
        /// </summary>
        /// <typeparam name="TChangeEntity">实体类型</typeparam>
        /// <returns>仓储</returns>
        public virtual IFreeSqlRepository<TChangeEntity> Change<TChangeEntity>()
            where TChangeEntity : class, new()
        {
            return _freeSqlRepository.Change<TChangeEntity>();
        }
    }
}