// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// Sql 执行仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface ISqlExecutableRepository<TEntity> : ISqlExecutableRepository<TEntity, DbContextLocator>
        where TEntity : class, IEntityBase, new()
    {
    }

    /// <summary>
    /// Sql 执行仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库实体定位器</typeparam>
    public interface ISqlExecutableRepository<TEntity, TDbContextLocator> : IRepositoryDependency
        where TEntity : class, IEntityBase, new()
        where TDbContextLocator : class, IDbContextLocator, new()
    {
        /// <summary>
        /// 执行存储过程返回 DataTable
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>DataTable</returns>
        DataTable SqlProcedure(string procName, params object[] parameters);

        /// <summary>
        /// 执行存储过程返回 DataTable
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="model">参数模型</param>
        /// <returns>DataTable</returns>
        DataTable SqlProcedure(string procName, object model);

        /// <summary>
        /// 执行存储过程返回 DataTable
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>DataTable</returns>
        Task<DataTable> SqlProcedureAsync(string procName, params object[] parameters);

        /// <summary>
        /// 执行存储过程返回 DataTable
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>DataTable</returns>
        Task<DataTable> SqlProcedureAsync(string procName, object[] parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行存储过程返回 DataTable
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>DataTable</returns>
        Task<DataTable> SqlProcedureAsync(string procName, object model, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行存储过程返回 List 集合
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>List<T></returns>
        List<T> SqlProcedure<T>(string procName, params object[] parameters);

        /// <summary>
        /// 执行存储过程返回 List 集合
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="model">参数模型</param>
        /// <returns>List<T></returns>
        List<T> SqlProcedure<T>(string procName, object model);

        /// <summary>
        /// 执行存储过程返回 List 集合
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>List<T></returns>
        Task<List<T>> SqlProcedureAsync<T>(string procName, params object[] parameters);

        /// <summary>
        /// 执行存储过程返回 List 集合
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>List<T></returns>
        Task<List<T>> SqlProcedureAsync<T>(string procName, object[] parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行存储过程返回 List 集合
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>List<T></returns>
        Task<List<T>> SqlProcedureAsync<T>(string procName, object model, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行存储过程返回 DataSet
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>DataSet</returns>
        DataSet SqlProcedureSet(string procName, params object[] parameters);

        /// <summary>
        /// 执行存储过程返回 DataSet
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="model">参数模型</param>
        /// <returns>DataSet</returns>
        DataSet SqlProcedureSet(string procName, object model);

        /// <summary>
        /// 执行存储过程返回 DataSet
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>DataSet</returns>
        Task<DataSet> SqlProcedureSetAsync(string procName, params object[] parameters);

        /// <summary>
        /// 执行存储过程返回 DataSet
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>DataSet</returns>
        Task<DataSet> SqlProcedureSetAsync(string procName, object[] parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行存储过程返回 DataSet
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>DataSet</returns>
        Task<DataSet> SqlProcedureSetAsync(string procName, object model, CancellationToken cancellationToken = default);
    }
}