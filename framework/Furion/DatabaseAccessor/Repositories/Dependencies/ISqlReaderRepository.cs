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

using System.Data;
using System.Data.Common;

namespace Furion.DatabaseAccessor;

/// <summary>
/// Sql 查询仓储接口
/// </summary>
public interface ISqlReaderRepository : ISqlReaderRepository<MasterDbContextLocator>
{
}

/// <summary>
/// Sql 查询仓储接口
/// </summary>
/// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
public interface ISqlReaderRepository<TDbContextLocator> : IPrivateSqlReaderRepository
    where TDbContextLocator : class, IDbContextLocator
{
}

/// <summary>
/// Sql 查询仓储接口
/// </summary>
public interface IPrivateSqlReaderRepository : IPrivateRootRepository
{
    /// <summary>
    /// Sql 查询返回 DataTable
    /// </summary>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <returns>DataTable</returns>
    DataTable SqlQuery(string sql, params DbParameter[] parameters);

    /// <summary>
    /// Sql 查询返回 DataTable
    /// </summary>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">参数模型</param>
    /// <returns>DataTable</returns>
    DataTable SqlQuery(string sql, object model);

    /// <summary>
    /// Sql 查询返回 DataTable
    /// </summary>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <returns>Task{DataTable}</returns>
    Task<DataTable> SqlQueryAsync(string sql, params DbParameter[] parameters);

    /// <summary>
    /// Sql 查询返回 DataTable
    /// </summary>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>Task{DataTable}</returns>
    Task<DataTable> SqlQueryAsync(string sql, DbParameter[] parameters, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sql 查询返回 DataTable
    /// </summary>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">参数模型</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>Task{DataTable}</returns>
    Task<DataTable> SqlQueryAsync(string sql, object model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sql 查询返回 List 集合
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <returns>List{T}</returns>
    List<T> SqlQuery<T>(string sql, params DbParameter[] parameters);

    /// <summary>
    /// Sql 查询返回 List 集合
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">参数模型</param>
    /// <returns>List{T}</returns>
    List<T> SqlQuery<T>(string sql, object model);

    /// <summary>
    /// Sql 查询返回 List 集合
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <returns>Task{List{T}}</returns>
    Task<List<T>> SqlQueryAsync<T>(string sql, params DbParameter[] parameters);

    /// <summary>
    /// Sql 查询返回 List 集合
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>Task{List{T}}</returns>
    Task<List<T>> SqlQueryAsync<T>(string sql, DbParameter[] parameters, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sql 查询返回 List 集合
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">参数模型</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>Task{List{T}}</returns>
    Task<List<T>> SqlQueryAsync<T>(string sql, object model, CancellationToken cancellationToken = default);

    /// <summary>
    ///  Sql 查询返回 DataSet
    /// </summary>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <returns>DataSet</returns>
    DataSet SqlQueries(string sql, params DbParameter[] parameters);

    /// <summary>
    ///  Sql 查询返回 DataSet
    /// </summary>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">参数模型</param>
    /// <returns>DataSet</returns>
    DataSet SqlQueries(string sql, object model);

    /// <summary>
    ///  Sql 查询返回 DataSet
    /// </summary>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <returns>Task{DataSet}</returns>
    Task<DataSet> SqlQueriesAsync(string sql, params DbParameter[] parameters);

    /// <summary>
    ///  Sql 查询返回 DataSet
    /// </summary>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>Task{DataSet}</returns>
    Task<DataSet> SqlQueriesAsync(string sql, DbParameter[] parameters, CancellationToken cancellationToken = default);

    /// <summary>
    ///  Sql 查询返回 DataSet
    /// </summary>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">参数模型</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>Task{DataSet}</returns>
    Task<DataSet> SqlQueriesAsync(string sql, object model, CancellationToken cancellationToken = default);

    /// <summary>
    ///  Sql 查询返回 List 集合
    /// </summary>
    /// <typeparam name="T1">返回类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <returns>List{T1}</returns>
    List<T1> SqlQueries<T1>(string sql, params DbParameter[] parameters);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <returns>元组类型</returns>
    (List<T1> list1, List<T2> list2) SqlQueries<T1, T2>(string sql, params DbParameter[] parameters);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <returns>元组类型</returns>
    (List<T1> list1, List<T2> list2, List<T3> list3) SqlQueries<T1, T2, T3>(string sql, params DbParameter[] parameters);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <returns>元组类型</returns>
    (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4) SqlQueries<T1, T2, T3, T4>(string sql, params DbParameter[] parameters);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <returns>元组类型</returns>
    (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5) SqlQueries<T1, T2, T3, T4, T5>(string sql, params DbParameter[] parameters);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <typeparam name="T6">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <returns>元组类型</returns>
    (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6) SqlQueries<T1, T2, T3, T4, T5, T6>(string sql, params DbParameter[] parameters);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <typeparam name="T6">元组元素类型</typeparam>
    /// <typeparam name="T7">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <returns>元组类型</returns>
    (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7) SqlQueries<T1, T2, T3, T4, T5, T6, T7>(string sql, params DbParameter[] parameters);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <typeparam name="T6">元组元素类型</typeparam>
    /// <typeparam name="T7">元组元素类型</typeparam>
    /// <typeparam name="T8">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <returns>元组类型</returns>
    (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7, List<T8> list8) SqlQueries<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, params DbParameter[] parameters);

    /// <summary>
    ///  Sql 查询返回 List 集合
    /// </summary>
    /// <typeparam name="T1">返回类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">参数模型</param>
    /// <returns>List{T1}</returns>
    List<T1> SqlQueries<T1>(string sql, object model);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">参数模型</param>
    /// <returns>元组类型</returns>
    (List<T1> list1, List<T2> list2) SqlQueries<T1, T2>(string sql, object model);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">参数模型</param>
    /// <returns>元组类型</returns>
    (List<T1> list1, List<T2> list2, List<T3> list3) SqlQueries<T1, T2, T3>(string sql, object model);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">参数模型</param>
    /// <returns>元组类型</returns>
    (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4) SqlQueries<T1, T2, T3, T4>(string sql, object model);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">参数模型</param>
    /// <returns>元组类型</returns>
    (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5) SqlQueries<T1, T2, T3, T4, T5>(string sql, object model);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <typeparam name="T6">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">参数模型</param>
    /// <returns>元组类型</returns>
    (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6) SqlQueries<T1, T2, T3, T4, T5, T6>(string sql, object model);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <typeparam name="T6">元组元素类型</typeparam>
    /// <typeparam name="T7">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">参数模型</param>
    /// <returns>元组类型</returns>
    (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7) SqlQueries<T1, T2, T3, T4, T5, T6, T7>(string sql, object model);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <typeparam name="T6">元组元素类型</typeparam>
    /// <typeparam name="T7">元组元素类型</typeparam>
    /// <typeparam name="T8">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">参数模型</param>
    /// <returns>元组类型</returns>
    (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7, List<T8> list8) SqlQueries<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, object model);

    /// <summary>
    ///  Sql 查询返回 List 集合
    /// </summary>
    /// <typeparam name="T1">返回类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <returns>Task{List{T1}}</returns>
    Task<List<T1>> SqlQueriesAsync<T1>(string sql, params DbParameter[] parameters);

    /// <summary>
    ///  Sql 查询返回 List 集合
    /// </summary>
    /// <typeparam name="T1">返回类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>Task{List{T1}}</returns>
    Task<List<T1>> SqlQueriesAsync<T1>(string sql, DbParameter[] parameters, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <returns>元组类型</returns>
    Task<(List<T1> list1, List<T2> list2)> SqlQueriesAsync<T1, T2>(string sql, params DbParameter[] parameters);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>元组类型</returns>
    Task<(List<T1> list1, List<T2> list2)> SqlQueriesAsync<T1, T2>(string sql, DbParameter[] parameters, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <returns>元组类型</returns>
    Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlQueriesAsync<T1, T2, T3>(string sql, params DbParameter[] parameters);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>元组类型</returns>
    Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlQueriesAsync<T1, T2, T3>(string sql, DbParameter[] parameters, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <returns>元组类型</returns>
    Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)> SqlQueriesAsync<T1, T2, T3, T4>(string sql, params DbParameter[] parameters);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>元组类型</returns>
    Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)> SqlQueriesAsync<T1, T2, T3, T4>(string sql, DbParameter[] parameters, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <returns>元组类型</returns>
    Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)> SqlQueriesAsync<T1, T2, T3, T4, T5>(string sql, params DbParameter[] parameters);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>元组类型</returns>
    Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)> SqlQueriesAsync<T1, T2, T3, T4, T5>(string sql, DbParameter[] parameters, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <typeparam name="T6">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <returns>元组类型</returns>
    Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6>(string sql, params DbParameter[] parameters);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <typeparam name="T6">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>元组类型</returns>
    Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6>(string sql, DbParameter[] parameters, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <typeparam name="T6">元组元素类型</typeparam>
    /// <typeparam name="T7">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <returns>元组类型</returns>
    Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(string sql, params DbParameter[] parameters);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <typeparam name="T6">元组元素类型</typeparam>
    /// <typeparam name="T7">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>元组类型</returns>
    Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(string sql, DbParameter[] parameters, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <typeparam name="T6">元组元素类型</typeparam>
    /// <typeparam name="T7">元组元素类型</typeparam>
    /// <typeparam name="T8">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <returns>元组类型</returns>
    Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7, List<T8> list8)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, params DbParameter[] parameters);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <typeparam name="T6">元组元素类型</typeparam>
    /// <typeparam name="T7">元组元素类型</typeparam>
    /// <typeparam name="T8">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="parameters">命令参数</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>元组类型</returns>
    Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7, List<T8> list8)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, DbParameter[] parameters, CancellationToken cancellationToken = default);

    /// <summary>
    ///  Sql 查询返回 List 集合
    /// </summary>
    /// <typeparam name="T1">返回类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">参数模型</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>List{T1}</returns>
    Task<List<T1>> SqlQueriesAsync<T1>(string sql, object model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">参数模型</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>元组类型</returns>
    Task<(List<T1> list1, List<T2> list2)> SqlQueriesAsync<T1, T2>(string sql, object model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">参数模型</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>元组类型</returns>
    Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlQueriesAsync<T1, T2, T3>(string sql, object model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">参数模型</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>元组类型</returns>
    Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)> SqlQueriesAsync<T1, T2, T3, T4>(string sql, object model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">参数模型</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>元组类型</returns>
    Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)> SqlQueriesAsync<T1, T2, T3, T4, T5>(string sql, object model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <typeparam name="T6">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">参数模型</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>元组类型</returns>
    Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6>(string sql, object model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <typeparam name="T6">元组元素类型</typeparam>
    /// <typeparam name="T7">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">参数模型</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>元组类型</returns>
    Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(string sql, object model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sql 查询返回 元组 集合
    /// </summary>
    /// <typeparam name="T1">元组元素类型</typeparam>
    /// <typeparam name="T2">元组元素类型</typeparam>
    /// <typeparam name="T3">元组元素类型</typeparam>
    /// <typeparam name="T4">元组元素类型</typeparam>
    /// <typeparam name="T5">元组元素类型</typeparam>
    /// <typeparam name="T6">元组元素类型</typeparam>
    /// <typeparam name="T7">元组元素类型</typeparam>
    /// <typeparam name="T8">元组元素类型</typeparam>
    /// <param name="sql">sql 语句</param>
    /// <param name="model">参数模型</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>元组类型</returns>
    Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7, List<T8> list8)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, object model, CancellationToken cancellationToken = default);
}