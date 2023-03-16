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

using System.Data;

namespace Dapper;

/// <summary>
/// 非泛型 Dapper 仓储
/// </summary>
public partial interface IDapperRepository
{
    /// <summary>
    /// 连接上下文
    /// </summary>
    IDbConnection Context { get; }

    /// <summary>
    /// 动态连接上下文
    /// </summary>
    dynamic DynamicContext { get; }

    /// <summary>
    /// 查询返回动态类型
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <param name="buffered"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    IEnumerable<dynamic> Query(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

    /// <summary>
    /// 查询返回特定类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <param name="buffered"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

    /// <summary>
    /// 查询返回动态类型
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

    /// <summary>
    /// 查询返回特定类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

    /// <summary>
    /// 执行命令
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

    /// <summary>
    /// 执行命令
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

    /// <summary>
    /// 切换仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns>仓储</returns>
    IDapperRepository<TEntity> Change<TEntity>()
        where TEntity : class, new();
}

/// <summary>
/// Dapper 仓储接口定义
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public partial interface IDapperRepository<TEntity> : IDapperRepository
    where TEntity : class, new()
{
    /// <summary>
    /// 获取一条
    /// </summary>
    /// <param name="id"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    TEntity Get(object id, IDbTransaction transaction = null, int? commandTimeout = null);

    /// <summary>
    /// 获取一条
    /// </summary>
    /// <param name="id"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    Task<TEntity> GetAsync(object id, IDbTransaction transaction = null, int? commandTimeout = null);

    /// <summary>
    /// 获取所有实体
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    IEnumerable<TEntity> GetAll(IDbTransaction transaction = null, int? commandTimeout = null);

    /// <summary>
    /// 获取所有实体
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> GetAllAsync(IDbTransaction transaction = null, int? commandTimeout = null);

    /// <summary>
    /// 新增一条
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    long Insert(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null);

    /// <summary>
    /// 新增一条
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="sqlAdapter"></param>
    /// <returns></returns>
    Task<int> InsertAsync(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null, ISqlAdapter sqlAdapter = null);

    /// <summary>
    /// 新增多条
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    long Insert(IEnumerable<TEntity> entities, IDbTransaction transaction = null, int? commandTimeout = null);

    /// <summary>
    /// 新增多条
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="sqlAdapter"></param>
    /// <returns></returns>
    Task<int> InsertAsync(IEnumerable<TEntity> entities, IDbTransaction transaction = null, int? commandTimeout = null, ISqlAdapter sqlAdapter = null);

    /// <summary>
    /// 更新一条
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    bool Update(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null);

    /// <summary>
    /// 更新一条
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null);

    /// <summary>
    /// 更新多条
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    bool Update(IEnumerable<TEntity> entities, IDbTransaction transaction = null, int? commandTimeout = null);

    /// <summary>
    /// 更新多条
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(IEnumerable<TEntity> entities, IDbTransaction transaction = null, int? commandTimeout = null);

    /// <summary>
    /// 删除一条
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    bool Delete(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null);

    /// <summary>
    /// 删除一条
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null);

    /// <summary>
    /// 删除多条
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    bool Delete(IEnumerable<TEntity> entities, IDbTransaction transaction = null, int? commandTimeout = null);

    /// <summary>
    /// 删除多条
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(IEnumerable<TEntity> entities, IDbTransaction transaction = null, int? commandTimeout = null);
}