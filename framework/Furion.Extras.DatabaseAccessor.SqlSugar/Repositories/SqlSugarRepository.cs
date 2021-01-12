using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SqlSugar
{
    /// <summary>
    /// 非泛型 SqlSugar 仓储
    /// </summary>
    public partial class SqlSugarRepository : ISqlSugarRepository
    {
        /// <summary>
        /// 服务提供器
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        public SqlSugarRepository(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 切换仓储
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns>仓储</returns>
        public virtual ISqlSugarRepository<TEntity> Change<TEntity>()
            where TEntity : class, new()
        {
            return _serviceProvider.GetService<ISqlSugarRepository<TEntity>>();
        }
    }

    /// <summary>
    /// SqlSugar 仓储实现类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class SqlSugarRepository<TEntity> : ISqlSugarRepository<TEntity>
    where TEntity : class, new()
    {
        /// <summary>
        /// 非泛型 SqlSugar 仓储
        /// </summary>
        private readonly ISqlSugarRepository _sqlSugarRepository;

        /// <summary>
        /// 初始化 SqlSugar 客户端
        /// </summary>
        private readonly ISqlSugarClient _db;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlSugarRepository"></param>
        /// <param name="db"></param>
        public SqlSugarRepository(ISqlSugarRepository sqlSugarRepository
            , ISqlSugarClient db)
        {
            _sqlSugarRepository = sqlSugarRepository;

            DynamicContext = Context = _db = db;
            Entities = _db.Queryable<TEntity>();
            Ado = _db.Ado;
        }

        /// <summary>
        /// 实体集合
        /// </summary>
        public virtual ISugarQueryable<TEntity> Entities { get; }

        /// <summary>
        /// 数据库上下文
        /// </summary>
        public virtual ISqlSugarClient Context { get; }

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
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> whereExpression)
        {
            return _db.Queryable<TEntity>().Count(whereExpression);
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return _db.Queryable<TEntity>().CountAsync(whereExpression);
        }

        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public bool Any(Expression<Func<TEntity, bool>> whereExpression)
        {
            return _db.Queryable<TEntity>().Any(whereExpression);
        }

        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _db.Queryable<TEntity>().AnyAsync(whereExpression);
        }

        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TEntity Single(dynamic Id)
        {
            return _db.Queryable<TEntity>().InSingle(Id);
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public TEntity Single(Expression<Func<TEntity, bool>> whereExpression)
        {
            return _db.Queryable<TEntity>().Single(whereExpression);
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return _db.Queryable<TEntity>().SingleAsync(whereExpression);
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> whereExpression)
        {
            return _db.Queryable<TEntity>().First(whereExpression);
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _db.Queryable<TEntity>().FirstAsync(whereExpression);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<TEntity> ToList()
        {
            return _db.Queryable<TEntity>().ToList();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public List<TEntity> ToList(Expression<Func<TEntity, bool>> whereExpression)
        {
            return _db.Queryable<TEntity>().Where(whereExpression).ToList();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        public List<TEntity> ToList(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
        {
            return _db.Queryable<TEntity>().OrderByIF(orderByExpression != null, orderByExpression, orderByType).Where(whereExpression).ToList();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public Task<List<TEntity>> ToListAsync()
        {
            return _db.Queryable<TEntity>().ToListAsync();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public Task<List<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return _db.Queryable<TEntity>().Where(whereExpression).ToListAsync();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="orderByType"></param>
        /// <returns></returns>
        public Task<List<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
        {
            return _db.Queryable<TEntity>().OrderByIF(orderByExpression != null, orderByExpression, orderByType).Where(whereExpression).ToListAsync();
        }

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Insert(TEntity entity)
        {
            return _db.Insertable(entity).ExecuteCommand();
        }

        /// <summary>
        /// 新增多条记录
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int Insert(params TEntity[] entities)
        {
            return _db.Insertable(entities).ExecuteCommand();
        }

        /// <summary>
        /// 新增多条记录
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int Insert(IEnumerable<TEntity> entities)
        {
            return _db.Insertable(entities.ToArray()).ExecuteCommand();
        }

        /// <summary>
        /// 新增一条记录返回自增Id
        /// </summary>
        /// <param name="insertObj"></param>
        /// <returns></returns>
        public int InsertReturnIdentity(TEntity insertObj)
        {
            return _db.Insertable(insertObj).ExecuteReturnIdentity();
        }

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task<int> InsertAsync(TEntity entity)
        {
            return _db.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 新增多条记录
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual Task<int> InsertAsync(params TEntity[] entities)
        {
            return _db.Insertable(entities).ExecuteCommandAsync();
        }

        /// <summary>
        /// 新增多条记录
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual Task<int> InsertAsync(IEnumerable<TEntity> entities)
        {
            return _db.Insertable(entities.ToArray()).ExecuteCommandAsync();
        }

        /// <summary>
        /// 新增一条记录返回自增Id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<long> InsertReturnIdentityAsync(TEntity entity)
        {
            return await _db.Insertable(entity).ExecuteReturnBigIdentityAsync();
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Update(TEntity entity)
        {
            return _db.Updateable(entity).ExecuteCommand();
        }

        /// <summary>
        /// 更新多条记录
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int Update(params TEntity[] entities)
        {
            return _db.Updateable(entities).ExecuteCommand();
        }

        /// <summary>
        /// 新增多条记录
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int Update(IEnumerable<TEntity> entities)
        {
            return _db.Updateable(entities.ToArray()).ExecuteCommand();
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task<int> UpdateAsync(TEntity entity)
        {
            return _db.Updateable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 更新多条记录
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual Task<int> UpdateAsync(params TEntity[] entities)
        {
            return _db.Updateable(entities).ExecuteCommandAsync();
        }

        /// <summary>
        /// 新增多条记录
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual Task<int> UpdateAsync(IEnumerable<TEntity> entities)
        {
            return _db.Updateable(entities.ToArray()).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Delete(TEntity entity)
        {
            return _db.Deleteable(entity).ExecuteCommand();
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual int Delete(object key)
        {
            return _db.Deleteable<TEntity>().In(key).ExecuteCommand();
        }

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public virtual int Delete(params object[] keys)
        {
            return _db.Deleteable<TEntity>().In(keys).ExecuteCommand();
        }

        /// <summary>
        /// 自定义条件删除记录
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public int Delete(Expression<Func<TEntity, bool>> whereExpression)
        {
            return _db.Deleteable<TEntity>().Where(whereExpression).ExecuteCommand();
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task<int> DeleteAsync(TEntity entity)
        {
            return _db.Deleteable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual Task<int> DeleteAsync(object key)
        {
            return _db.Deleteable<TEntity>().In(key).ExecuteCommandAsync();
        }

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public virtual Task<int> DeleteAsync(params object[] keys)
        {
            return _db.Deleteable<TEntity>().In(keys).ExecuteCommandAsync();
        }

        /// <summary>
        /// 自定义条件删除记录
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _db.Deleteable<TEntity>().Where(whereExpression).ExecuteCommandAsync();
        }

        /// <summary>
        /// 根据表达式查询多条记录
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual ISugarQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return AsQueryable(predicate);
        }

        /// <summary>
        /// 根据表达式查询多条记录
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual ISugarQueryable<TEntity> Where(bool condition, Expression<Func<TEntity, bool>> predicate)
        {
            return AsQueryable().WhereIF(condition, predicate);
        }

        /// <summary>
        /// 构建查询分析器
        /// </summary>
        /// <returns></returns>
        public virtual ISugarQueryable<TEntity> AsQueryable()
        {
            return Entities;
        }

        /// <summary>
        /// 构建查询分析器
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual ISugarQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> predicate)
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
        public virtual ISqlSugarRepository<TChangeEntity> Change<TChangeEntity>()
            where TChangeEntity : class, new()
        {
            return _sqlSugarRepository.Change<TChangeEntity>();
        }
    }
}