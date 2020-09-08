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

        /// <summary>
        ///  执行存储过程返回 List 集合
        /// </summary>
        /// <typeparam name="T1">返回类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>List<T1></returns>
        List<T1> SqlProcedureSet<T1>(string procName, params object[] parameters);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        (List<T1> list1, List<T2> list2) SqlProcedureSet<T1, T2>(string procName, params object[] parameters);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3) SqlProcedureSet<T1, T2, T3>(string procName, params object[] parameters);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4) SqlProcedureSet<T1, T2, T3, T4>(string procName, params object[] parameters);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5) SqlProcedureSet<T1, T2, T3, T4, T5>(string procName, params object[] parameters);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6) SqlProcedureSet<T1, T2, T3, T4, T5, T6>(string procName, params object[] parameters);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <typeparam name="T7">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7) SqlProcedureSet<T1, T2, T3, T4, T5, T6, T7>(string procName, params object[] parameters);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <typeparam name="T7">元组元素类型</typeparam>
        /// <typeparam name="T8">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7, List<T8> list8) SqlProcedureSet<T1, T2, T3, T4, T5, T6, T7, T8>(string procName, params object[] parameters);

        /// <summary>
        ///  执行存储过程返回 List 集合
        /// </summary>
        /// <typeparam name="T1">返回类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="model">参数模型</param>
        /// <returns>List<T1></returns>
        List<T1> SqlProcedureSet<T1>(string procName, object model);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="model">参数模型</param>
        /// <returns>元组类型</returns>
        (List<T1> list1, List<T2> list2) SqlProcedureSet<T1, T2>(string procName, object model);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="model">参数模型</param>
        /// <returns>元组类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3) SqlProcedureSet<T1, T2, T3>(string procName, object model);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="model">参数模型</param>
        /// <returns>元组类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4) SqlProcedureSet<T1, T2, T3, T4>(string procName, object model);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="model">参数模型</param>
        /// <returns>元组类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5) SqlProcedureSet<T1, T2, T3, T4, T5>(string procName, object model);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="model">参数模型</param>
        /// <returns>元组类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6) SqlProcedureSet<T1, T2, T3, T4, T5, T6>(string procName, object model);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <typeparam name="T7">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="model">参数模型</param>
        /// <returns>元组类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7) SqlProcedureSet<T1, T2, T3, T4, T5, T6, T7>(string procName, object model);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <typeparam name="T7">元组元素类型</typeparam>
        /// <typeparam name="T8">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="model">参数模型</param>
        /// <returns>元组类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7, List<T8> list8) SqlProcedureSet<T1, T2, T3, T4, T5, T6, T7, T8>(string procName, object model);

        /// <summary>
        ///  执行存储过程返回 List 集合
        /// </summary>
        /// <typeparam name="T1">返回类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>Task<List<T1>></returns>
        Task<List<T1>> SqlProcedureSetAsync<T1>(string procName, params object[] parameters);

        /// <summary>
        ///  执行存储过程返回 List 集合
        /// </summary>
        /// <typeparam name="T1">返回类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>Task<List<T1>></returns>
        Task<List<T1>> SqlProcedureSetAsync<T1>(string procName, object[] parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        Task<(List<T1> list1, List<T2> list2)> SqlProcedureSetAsync<T1, T2>(string procName, params object[] parameters);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        Task<(List<T1> list1, List<T2> list2)> SqlProcedureSetAsync<T1, T2>(string procName, object[] parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlProcedureSetAsync<T1, T2, T3>(string procName, params object[] parameters);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlProcedureSetAsync<T1, T2, T3>(string procName, object[] parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)> SqlProcedureSetAsync<T1, T2, T3, T4>(string procName, params object[] parameters);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)> SqlProcedureSetAsync<T1, T2, T3, T4>(string procName, object[] parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)> SqlProcedureSetAsync<T1, T2, T3, T4, T5>(string procName, params object[] parameters);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)> SqlProcedureSetAsync<T1, T2, T3, T4, T5>(string procName, object[] parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)> SqlProcedureSetAsync<T1, T2, T3, T4, T5, T6>(string procName, params object[] parameters);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)> SqlProcedureSetAsync<T1, T2, T3, T4, T5, T6>(string procName, object[] parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <typeparam name="T7">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7)> SqlProcedureSetAsync<T1, T2, T3, T4, T5, T6, T7>(string procName, params object[] parameters);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <typeparam name="T7">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7)> SqlProcedureSetAsync<T1, T2, T3, T4, T5, T6, T7>(string procName, object[] parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <typeparam name="T7">元组元素类型</typeparam>
        /// <typeparam name="T8">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7, List<T8> list8)> SqlProcedureSetAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string procName, params object[] parameters);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <typeparam name="T7">元组元素类型</typeparam>
        /// <typeparam name="T8">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7, List<T8> list8)> SqlProcedureSetAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string procName, object[] parameters, CancellationToken cancellationToken = default);

        /// <summary>
        ///  执行存储过程返回 List 集合
        /// </summary>
        /// <typeparam name="T1">返回类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>List<T1></returns>
        Task<List<T1>> SqlProcedureSetAsync<T1>(string procName, object model, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        Task<(List<T1> list1, List<T2> list2)> SqlProcedureSetAsync<T1, T2>(string procName, object model, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlProcedureSetAsync<T1, T2, T3>(string procName, object model, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)> SqlProcedureSetAsync<T1, T2, T3, T4>(string procName, object model, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)> SqlProcedureSetAsync<T1, T2, T3, T4, T5>(string procName, object model, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)> SqlProcedureSetAsync<T1, T2, T3, T4, T5, T6>(string procName, object model, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <typeparam name="T7">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7)> SqlProcedureSetAsync<T1, T2, T3, T4, T5, T6, T7>(string procName, object model, CancellationToken cancellationToken = default);

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <typeparam name="T7">元组元素类型</typeparam>
        /// <typeparam name="T8">元组元素类型</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7, List<T8> list8)> SqlProcedureSetAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string procName, object model, CancellationToken cancellationToken = default);
    }
}