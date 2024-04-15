// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

using Dapper.Contrib.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Dapper;

/// <summary>
/// 非泛型 Dapper 仓储
/// </summary>
public partial class DapperRepository : IDapperRepository
{
    /// <summary>
    /// 数据库连接对象
    /// </summary>
    private readonly IDbConnection _db;

    /// <summary>
    /// 服务提供器
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="db"></param>
    public DapperRepository(
        IServiceProvider serviceProvider
        , IDbConnection db)
    {
        _serviceProvider = serviceProvider;
        _db = db;
    }

    /// <summary>
    /// 连接上下文
    /// </summary>
    public virtual IDbConnection Context
    {
        get
        {
            if (_db.State != ConnectionState.Open) _db.Open();
            return _db;
        }
    }

    /// <summary>
    /// 动态连接上下文
    /// </summary>
    public virtual dynamic DynamicContext { get; }

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
    public virtual IEnumerable<dynamic> Query(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
    {
        return Context.Query(sql, param, transaction, buffered, commandTimeout, commandType);
    }

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
    public virtual IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
    {
        return Context.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
    }

    /// <summary>
    /// 查询返回动态类型
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    public virtual Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return Context.QueryAsync(sql, param, transaction, commandTimeout, commandType);
    }

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
    public virtual Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return Context.QueryAsync<T>(sql: sql, param, transaction, commandTimeout, commandType);
    }

    /// <summary>
    /// 执行命令
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    public virtual int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return Context.Execute(sql, param, transaction, commandTimeout, commandType);
    }

    /// <summary>
    /// 执行命令
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    public virtual Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return Context.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
    }

    /// <summary>
    /// 切换仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns>仓储</returns>
    public virtual IDapperRepository<TEntity> Change<TEntity>()
        where TEntity : class, new()
    {
        return _serviceProvider.GetService<IDapperRepository<TEntity>>();
    }
}

/// <summary>
/// Dapper 仓储实现类
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public partial class DapperRepository<TEntity> : DapperRepository, IDapperRepository<TEntity>
    where TEntity : class, new()
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="db"></param>
    public DapperRepository(IServiceProvider serviceProvider
        , IDbConnection db) : base(serviceProvider, db)
    {
    }

    /// <summary>
    /// 获取一条
    /// </summary>
    /// <param name="id"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    public virtual TEntity Get(object id, IDbTransaction transaction = null, int? commandTimeout = null)
    {
        return Context.Get<TEntity>(id, transaction, commandTimeout);
    }

    /// <summary>
    /// 获取一条
    /// </summary>
    /// <param name="id"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    public virtual Task<TEntity> GetAsync(object id, IDbTransaction transaction = null, int? commandTimeout = null)
    {
        return Context.GetAsync<TEntity>(id, transaction, commandTimeout);
    }

    /// <summary>
    /// 获取所有实体
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    public virtual IEnumerable<TEntity> GetAll(IDbTransaction transaction = null, int? commandTimeout = null)
    {
        return Context.GetAll<TEntity>(transaction, commandTimeout);
    }

    /// <summary>
    /// 获取所有实体
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    public virtual Task<IEnumerable<TEntity>> GetAllAsync(IDbTransaction transaction = null, int? commandTimeout = null)
    {
        return Context.GetAllAsync<TEntity>(transaction, commandTimeout);
    }

    /// <summary>
    /// 新增一条
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    public virtual long Insert(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null)
    {
        return Context.Insert(entity, transaction, commandTimeout);
    }

    /// <summary>
    /// 新增一条
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="sqlAdapter"></param>
    /// <returns></returns>
    public virtual Task<int> InsertAsync(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null, ISqlAdapter sqlAdapter = null)
    {
        return Context.InsertAsync(entity, transaction, commandTimeout, sqlAdapter);
    }

    /// <summary>
    /// 新增多条
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    public virtual long Insert(IEnumerable<TEntity> entities, IDbTransaction transaction = null, int? commandTimeout = null)
    {
        return Context.Insert(entities, transaction, commandTimeout);
    }

    /// <summary>
    /// 新增多条
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="sqlAdapter"></param>
    /// <returns></returns>
    public virtual Task<int> InsertAsync(IEnumerable<TEntity> entities, IDbTransaction transaction = null, int? commandTimeout = null, ISqlAdapter sqlAdapter = null)
    {
        return Context.InsertAsync(entities, transaction, commandTimeout, sqlAdapter);
    }

    /// <summary>
    /// 更新一条
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    public virtual bool Update(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null)
    {
        return Context.Update(entity, transaction, commandTimeout);
    }

    /// <summary>
    /// 更新一条
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    public virtual Task<bool> UpdateAsync(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null)
    {
        return Context.UpdateAsync(entity, transaction, commandTimeout);
    }

    /// <summary>
    /// 更新多条
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    public virtual bool Update(IEnumerable<TEntity> entities, IDbTransaction transaction = null, int? commandTimeout = null)
    {
        return Context.Update(entities, transaction, commandTimeout);
    }

    /// <summary>
    /// 更新多条
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    public virtual Task<bool> UpdateAsync(IEnumerable<TEntity> entities, IDbTransaction transaction = null, int? commandTimeout = null)
    {
        return Context.UpdateAsync(entities, transaction, commandTimeout);
    }

    /// <summary>
    /// 删除一条
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    public virtual bool Delete(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null)
    {
        return Context.Delete(entity, transaction, commandTimeout);
    }

    /// <summary>
    /// 删除一条
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    public virtual Task<bool> DeleteAsync(TEntity entity, IDbTransaction transaction = null, int? commandTimeout = null)
    {
        return Context.DeleteAsync(entity, transaction, commandTimeout);
    }

    /// <summary>
    /// 删除多条
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    public virtual bool Delete(IEnumerable<TEntity> entities, IDbTransaction transaction = null, int? commandTimeout = null)
    {
        return Context.Delete(entities, transaction, commandTimeout);
    }

    /// <summary>
    /// 删除多条
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="transaction"></param>
    /// <param name="commandTimeout"></param>
    /// <returns></returns>
    public virtual Task<bool> DeleteAsync(IEnumerable<TEntity> entities, IDbTransaction transaction = null, int? commandTimeout = null)
    {
        return Context.DeleteAsync(entities, transaction, commandTimeout);
    }
}