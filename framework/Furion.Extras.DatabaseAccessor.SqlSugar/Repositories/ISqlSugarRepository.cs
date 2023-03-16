// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using System.Linq.Expressions;

namespace SqlSugar;

/// <summary>
/// 非泛型 SqlSugar 仓储
/// </summary>
public partial interface ISqlSugarRepository
{
    /// <summary>
    /// 切换仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns>仓储</returns>
    ISqlSugarRepository<TEntity> Change<TEntity>()
        where TEntity : class, new();

    /// <summary>
    /// 数据库上下文
    /// </summary>
    SqlSugarClient Context { get; }

    /// <summary>
    /// 动态数据库上下文
    /// </summary>
    dynamic DynamicContext { get; }

    /// <summary>
    /// 原生 Ado 对象
    /// </summary>
    IAdo Ado { get; }
}

/// <summary>
/// SqlSugar 仓储接口定义
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public partial interface ISqlSugarRepository<TEntity>
    where TEntity : class, new()
{
    /// <summary>
    /// 实体集合
    /// </summary>
    ISugarQueryable<TEntity> Entities { get; }

    /// <summary>
    /// 数据库上下文
    /// </summary>
    SqlSugarClient Context { get; }

    /// <summary>
    /// 动态数据库上下文
    /// </summary>
    dynamic DynamicContext { get; }

    /// <summary>
    /// 原生 Ado 对象
    /// </summary>
    IAdo Ado { get; }

    /// <summary>
    /// 获取总数
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    int Count(Expression<Func<TEntity, bool>> whereExpression);

    /// <summary>
    /// 获取总数
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression);

    /// <summary>
    /// 检查是否存在
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    bool Any(Expression<Func<TEntity, bool>> whereExpression);

    /// <summary>
    /// 检查是否存在
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression);

    /// <summary>
    /// 通过主键获取实体
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    TEntity Single(dynamic Id);

    /// <summary>
    /// 获取一个实体
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    TEntity Single(Expression<Func<TEntity, bool>> whereExpression);

    /// <summary>
    /// 获取一个实体
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> whereExpression);

    /// <summary>
    /// 获取一个实体
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    TEntity FirstOrDefault(Expression<Func<TEntity, bool>> whereExpression);

    /// <summary>
    /// 获取一个实体
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> whereExpression);

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    List<TEntity> ToList();

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    List<TEntity> ToList(Expression<Func<TEntity, bool>> whereExpression);

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <param name="orderByExpression"></param>
    /// <param name="orderByType"></param>
    /// <returns></returns>
    List<TEntity> ToList(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    Task<List<TEntity>> ToListAsync();

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    Task<List<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> whereExpression);

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <param name="orderByExpression"></param>
    /// <param name="orderByType"></param>
    /// <returns></returns>
    Task<List<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);

    /// <summary>
    /// 新增一条记录
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    int Insert(TEntity entity);

    /// <summary>
    /// 新增多条记录
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    int Insert(params TEntity[] entities);

    /// <summary>
    /// 新增多条记录
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    int Insert(IEnumerable<TEntity> entities);

    /// <summary>
    /// 新增一条记录返回自增Id
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    int InsertReturnIdentity(TEntity entity);

    /// <summary>
    /// 新增一条记录
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<int> InsertAsync(TEntity entity);

    /// <summary>
    /// 新增多条记录
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<int> InsertAsync(params TEntity[] entities);

    /// <summary>
    /// 新增多条记录
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<int> InsertAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// 新增一条记录返回自增Id
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<long> InsertReturnIdentityAsync(TEntity entity);

    /// <summary>
    /// 更新一条记录
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    int Update(TEntity entity);

    /// <summary>
    /// 更新多条记录
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    int Update(params TEntity[] entities);

    /// <summary>
    /// 更新多条记录
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    int Update(IEnumerable<TEntity> entities);

    /// <summary>
    /// 更新一条记录
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<int> UpdateAsync(TEntity entity);

    /// <summary>
    /// 更新多条记录
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<int> UpdateAsync(params TEntity[] entities);

    /// <summary>
    /// 更新多条记录
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<int> UpdateAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// 删除一条记录
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    int Delete(TEntity entity);

    /// <summary>
    /// 删除一条记录
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    int Delete(object key);

    /// <summary>
    /// 删除多条记录
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    int Delete(params object[] keys);

    /// <summary>
    /// 自定义条件删除记录
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    int Delete(Expression<Func<TEntity, bool>> whereExpression);

    /// <summary>
    /// 删除一条记录
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<int> DeleteAsync(TEntity entity);

    /// <summary>
    /// 删除一条记录
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<int> DeleteAsync(object key);

    /// <summary>
    /// 删除多条记录
    /// </summary>
    /// <param name="keys"></param>
    /// <returns></returns>
    Task<int> DeleteAsync(params object[] keys);

    /// <summary>
    /// 自定义条件删除记录
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    Task<int> DeleteAsync(Expression<Func<TEntity, bool>> whereExpression);

    /// <summary>
    /// 根据表达式查询多条记录
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    ISugarQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 根据表达式查询多条记录
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    ISugarQueryable<TEntity> Where(bool condition, Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 构建查询分析器
    /// </summary>
    /// <returns></returns>
    ISugarQueryable<TEntity> AsQueryable();

    /// <summary>
    /// 构建查询分析器
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    ISugarQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 直接返回数据库结果
    /// </summary>
    /// <returns></returns>
    List<TEntity> AsEnumerable();

    /// <summary>
    /// 直接返回数据库结果
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    List<TEntity> AsEnumerable(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 直接返回数据库结果
    /// </summary>
    /// <returns></returns>
    Task<List<TEntity>> AsAsyncEnumerable();

    /// <summary>
    /// 直接返回数据库结果
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<List<TEntity>> AsAsyncEnumerable(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 切换仓储
    /// </summary>
    /// <typeparam name="TChangeEntity"></typeparam>
    /// <returns></returns>
    ISqlSugarRepository<TChangeEntity> Change<TChangeEntity>() where TChangeEntity : class, new();
}