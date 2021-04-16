using Furion.Extensions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 构建 Sql 执行部分
    /// </summary>
    public sealed partial class SqlBuilderPart
    {
        /// <summary>
        /// Sql 查询返回 DataTable
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>DataTable</returns>
        public DataTable SqlQuery(params DbParameter[] parameters)
        {
            return Build().ExecuteReader(SqlString, parameters);
        }

        /// <summary>
        /// Sql 查询返回 DataTable
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <returns>DataTable</returns>
        public DataTable SqlQuery(object model)
        {
            return Build().ExecuteReader(SqlString, model).dataTable;
        }

        /// <summary>
        /// Sql 查询返回 DataTable
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>Task{DataTable}</returns>
        public Task<DataTable> SqlQueryAsync(params DbParameter[] parameters)
        {
            return Build().ExecuteReaderAsync(SqlString, parameters);
        }

        /// <summary>
        /// Sql 查询返回 DataTable
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>Task{DataTable}</returns>
        public Task<DataTable> SqlQueryAsync(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            return Build().ExecuteReaderAsync(SqlString, parameters, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Sql 查询返回 DataTable
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>Task{DataTable}</returns>
        public async Task<DataTable> SqlQueryAsync(object model, CancellationToken cancellationToken = default)
        {
            var (dataTable, _) = await Build().ExecuteReaderAsync(SqlString, model, cancellationToken: cancellationToken);
            return dataTable;
        }

        /// <summary>
        /// Sql 查询返回 List 集合
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>List{T}</returns>
        public List<T> SqlQuery<T>(params DbParameter[] parameters)
        {
            return Build().ExecuteReader(SqlString, parameters).ToList<T>();
        }

        /// <summary>
        /// Sql 查询返回 List 集合
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <returns>List{T}</returns>
        public List<T> SqlQuery<T>(object model)
        {
            return Build().ExecuteReader(SqlString, model).dataTable.ToList<T>();
        }

        /// <summary>
        /// Sql 查询返回 List 集合
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>Task{List{T}}</returns>
        public async Task<List<T>> SqlQueryAsync<T>(params DbParameter[] parameters)
        {
            var dataTable = await Build().ExecuteReaderAsync(SqlString, parameters);
            return dataTable.ToList<T>();
        }

        /// <summary>
        /// Sql 查询返回 List 集合
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>Task{List{T}}</returns>
        public async Task<List<T>> SqlQueryAsync<T>(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var dataTable = await Build().ExecuteReaderAsync(SqlString, parameters, cancellationToken: cancellationToken);
            return dataTable.ToList<T>();
        }

        /// <summary>
        /// Sql 查询返回 List 集合
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>Task{List{T}}</returns>
        public async Task<List<T>> SqlQueryAsync<T>(object model, CancellationToken cancellationToken = default)
        {
            var (dataTable, _) = await Build().ExecuteReaderAsync(SqlString, model, cancellationToken: cancellationToken);
            return dataTable.ToList<T>();
        }

        /// <summary>
        ///  Sql 查询返回 DataSet
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>DataSet</returns>
        public DataSet SqlQueries(params DbParameter[] parameters)
        {
            return Build().DataAdapterFill(SqlString, parameters);
        }

        /// <summary>
        ///  Sql 查询返回 DataSet
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <returns>DataSet</returns>
        public DataSet SqlQueries(object model)
        {
            return Build().DataAdapterFill(SqlString, model).dataSet;
        }

        /// <summary>
        ///  Sql 查询返回 DataSet
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>Task{DataSet}</returns>
        public Task<DataSet> SqlQueriesAsync(params DbParameter[] parameters)
        {
            return Build().DataAdapterFillAsync(SqlString, parameters);
        }

        /// <summary>
        ///  Sql 查询返回 DataSet
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>Task{DataSet}</returns>
        public Task<DataSet> SqlQueriesAsync(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            return Build().DataAdapterFillAsync(SqlString, parameters, cancellationToken: cancellationToken);
        }

        /// <summary>
        ///  Sql 查询返回 DataSet
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>Task{DataSet}</returns>
        public async Task<DataSet> SqlQueriesAsync(object model, CancellationToken cancellationToken = default)
        {
            var (dataSet, _) = await Build().DataAdapterFillAsync(SqlString, model, cancellationToken: cancellationToken);
            return dataSet;
        }

        /// <summary>
        ///  Sql 查询返回 List 集合
        /// </summary>
        /// <typeparam name="T1">返回类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>List{T1}</returns>
        public List<T1> SqlQueries<T1>(params DbParameter[] parameters)
        {
            return Build().DataAdapterFill(SqlString, parameters).ToList<T1>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2) SqlQueries<T1, T2>(params DbParameter[] parameters)
        {
            return Build().DataAdapterFill(SqlString, parameters).ToList<T1, T2>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3) SqlQueries<T1, T2, T3>(params DbParameter[] parameters)
        {
            return Build().DataAdapterFill(SqlString, parameters).ToList<T1, T2, T3>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4) SqlQueries<T1, T2, T3, T4>(params DbParameter[] parameters)
        {
            return Build().DataAdapterFill(SqlString, parameters).ToList<T1, T2, T3, T4>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5) SqlQueries<T1, T2, T3, T4, T5>(params DbParameter[] parameters)
        {
            return Build().DataAdapterFill(SqlString, parameters).ToList<T1, T2, T3, T4, T5>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6) SqlQueries<T1, T2, T3, T4, T5, T6>(params DbParameter[] parameters)
        {
            return Build().DataAdapterFill(SqlString, parameters).ToList<T1, T2, T3, T4, T5, T6>();
        }

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
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7) SqlQueries<T1, T2, T3, T4, T5, T6, T7>(params DbParameter[] parameters)
        {
            return Build().DataAdapterFill(SqlString, parameters).ToList<T1, T2, T3, T4, T5, T6, T7>();
        }

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
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7, List<T8> list8) SqlQueries<T1, T2, T3, T4, T5, T6, T7, T8>(params DbParameter[] parameters)
        {
            return Build().DataAdapterFill(SqlString, parameters).ToList<T1, T2, T3, T4, T5, T6, T7, T8>();
        }

        /// <summary>
        ///  Sql 查询返回 List 集合
        /// </summary>
        /// <typeparam name="T1">返回类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <returns>List{T1}</returns>
        public List<T1> SqlQueries<T1>(object model)
        {
            return Build().DataAdapterFill(SqlString, model).dataSet.ToList<T1>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2) SqlQueries<T1, T2>(object model)
        {
            return Build().DataAdapterFill(SqlString, model).dataSet.ToList<T1, T2>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3) SqlQueries<T1, T2, T3>(object model)
        {
            return Build().DataAdapterFill(SqlString, model).dataSet.ToList<T1, T2, T3>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4) SqlQueries<T1, T2, T3, T4>(object model)
        {
            return Build().DataAdapterFill(SqlString, model).dataSet.ToList<T1, T2, T3, T4>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5) SqlQueries<T1, T2, T3, T4, T5>(object model)
        {
            return Build().DataAdapterFill(SqlString, model).dataSet.ToList<T1, T2, T3, T4, T5>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6) SqlQueries<T1, T2, T3, T4, T5, T6>(object model)
        {
            return Build().DataAdapterFill(SqlString, model).dataSet.ToList<T1, T2, T3, T4, T5, T6>();
        }

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
        /// <param name="model">参数模型</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7) SqlQueries<T1, T2, T3, T4, T5, T6, T7>(object model)
        {
            return Build().DataAdapterFill(SqlString, model).dataSet.ToList<T1, T2, T3, T4, T5, T6, T7>();
        }

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
        /// <param name="model">参数模型</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7, List<T8> list8) SqlQueries<T1, T2, T3, T4, T5, T6, T7, T8>(object model)
        {
            return Build().DataAdapterFill(SqlString, model).dataSet.ToList<T1, T2, T3, T4, T5, T6, T7, T8>();
        }

        /// <summary>
        ///  Sql 查询返回 List 集合
        /// </summary>
        /// <typeparam name="T1">返回类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>Task{List{T1}}</returns>
        public async Task<List<T1>> SqlQueriesAsync<T1>(params DbParameter[] parameters)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters);
            return dataset.ToList<T1>();
        }

        /// <summary>
        ///  Sql 查询返回 List 集合
        /// </summary>
        /// <typeparam name="T1">返回类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>Task{List{T1}}</returns>
        public async Task<List<T1>> SqlQueriesAsync<T1>(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, cancellationToken: cancellationToken);
            return dataset.ToList<T1>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2)> SqlQueriesAsync<T1, T2>(params DbParameter[] parameters)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters);
            return dataset.ToList<T1, T2>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2)> SqlQueriesAsync<T1, T2>(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlQueriesAsync<T1, T2, T3>(params DbParameter[] parameters)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters);
            return dataset.ToList<T1, T2, T3>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlQueriesAsync<T1, T2, T3>(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)> SqlQueriesAsync<T1, T2, T3, T4>(params DbParameter[] parameters)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters);
            return dataset.ToList<T1, T2, T3, T4>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)> SqlQueriesAsync<T1, T2, T3, T4>(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)> SqlQueriesAsync<T1, T2, T3, T4, T5>(params DbParameter[] parameters)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters);
            return dataset.ToList<T1, T2, T3, T4, T5>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)> SqlQueriesAsync<T1, T2, T3, T4, T5>(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6>(params DbParameter[] parameters)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters);
            return dataset.ToList<T1, T2, T3, T4, T5, T6>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6>(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5, T6>();
        }

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
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(params DbParameter[] parameters)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters);
            return dataset.ToList<T1, T2, T3, T4, T5, T6, T7>();
        }

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
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5, T6, T7>();
        }

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
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7, List<T8> list8)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(params DbParameter[] parameters)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters);
            return dataset.ToList<T1, T2, T3, T4, T5, T6, T7, T8>();
        }

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
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7, List<T8> list8)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5, T6, T7, T8>();
        }

        /// <summary>
        ///  Sql 查询返回 List 集合
        /// </summary>
        /// <typeparam name="T1">返回类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>List{T1}</returns>
        public async Task<List<T1>> SqlQueriesAsync<T1>(object model, CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Build().DataAdapterFillAsync(SqlString, model, cancellationToken: cancellationToken);
            return dataset.ToList<T1>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2)> SqlQueriesAsync<T1, T2>(object model, CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Build().DataAdapterFillAsync(SqlString, model, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlQueriesAsync<T1, T2, T3>(object model, CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Build().DataAdapterFillAsync(SqlString, model, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)> SqlQueriesAsync<T1, T2, T3, T4>(object model, CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Build().DataAdapterFillAsync(SqlString, model, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)> SqlQueriesAsync<T1, T2, T3, T4, T5>(object model, CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Build().DataAdapterFillAsync(SqlString, model, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5>();
        }

        /// <summary>
        /// Sql 查询返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6>(object model, CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Build().DataAdapterFillAsync(SqlString, model, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5, T6>();
        }

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
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(object model, CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Build().DataAdapterFillAsync(SqlString, model, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5, T6, T7>();
        }

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
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7, List<T8> list8)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(object model, CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Build().DataAdapterFillAsync(SqlString, model, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5, T6, T7, T8>();
        }

        /// <summary>
        /// 执行存储过程返回 DataTable
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>DataTable</returns>
        public DataTable SqlProcedureQuery(params DbParameter[] parameters)
        {
            return Build().ExecuteReader(SqlString, parameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 执行存储过程返回 DataTable
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <returns>DataTable</returns>
        public DataTable SqlProcedureQuery(object model)
        {
            return Build().ExecuteReader(SqlString, model, CommandType.StoredProcedure).dataTable;
        }

        /// <summary>
        /// 执行存储过程返回 DataTable
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>DataTable</returns>
        public Task<DataTable> SqlProcedureQueryAsync(params DbParameter[] parameters)
        {
            return Build().ExecuteReaderAsync(SqlString, parameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 执行存储过程返回 DataTable
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>DataTable</returns>
        public Task<DataTable> SqlProcedureQueryAsync(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            return Build().ExecuteReaderAsync(SqlString, parameters, CommandType.StoredProcedure, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 执行存储过程返回 DataTable
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>DataTable</returns>
        public async Task<DataTable> SqlProcedureQueryAsync(object model, CancellationToken cancellationToken = default)
        {
            var (dataTable, _) = await Build().ExecuteReaderAsync(SqlString, model, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return dataTable;
        }

        /// <summary>
        /// 执行存储过程返回 List 集合
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>List{T}</returns>
        public List<T> SqlProcedureQuery<T>(params DbParameter[] parameters)
        {
            return Build().ExecuteReader(SqlString, parameters, CommandType.StoredProcedure).ToList<T>();
        }

        /// <summary>
        /// 执行存储过程返回 List 集合
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <returns>List{T}</returns>
        public List<T> SqlProcedureQuery<T>(object model)
        {
            return Build().ExecuteReader(SqlString, model, CommandType.StoredProcedure).dataTable.ToList<T>();
        }

        /// <summary>
        /// 执行存储过程返回 List 集合
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>List{T}</returns>
        public async Task<List<T>> SqlProcedureQueryAsync<T>(params DbParameter[] parameters)
        {
            var dataTable = await Build().ExecuteReaderAsync(SqlString, parameters, CommandType.StoredProcedure);
            return dataTable.ToList<T>();
        }

        /// <summary>
        /// 执行存储过程返回 List 集合
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>List{T}</returns>
        public async Task<List<T>> SqlProcedureQueryAsync<T>(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var dataTable = await Build().ExecuteReaderAsync(SqlString, parameters, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return dataTable.ToList<T>();
        }

        /// <summary>
        /// 执行存储过程返回 List 集合
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>List{T}</returns>
        public async Task<List<T>> SqlProcedureQueryAsync<T>(object model, CancellationToken cancellationToken = default)
        {
            var (dataTable, _) = await Build().ExecuteReaderAsync(SqlString, model, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return dataTable.ToList<T>();
        }

        /// <summary>
        /// 执行存储过程返回 DataSet
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>DataSet</returns>
        public DataSet SqlProcedureQueries(params DbParameter[] parameters)
        {
            return Build().DataAdapterFill(SqlString, parameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 执行存储过程返回 DataSet
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <returns>DataSet</returns>
        public DataSet SqlProcedureQueries(object model)
        {
            return Build().DataAdapterFill(SqlString, model, CommandType.StoredProcedure).dataSet;
        }

        /// <summary>
        /// 执行存储过程返回 DataSet
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>DataSet</returns>
        public Task<DataSet> SqlProcedureQueriesAsync(params DbParameter[] parameters)
        {
            return Build().DataAdapterFillAsync(SqlString, parameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 执行存储过程返回 DataSet
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>DataSet</returns>
        public Task<DataSet> SqlProcedureQueriesAsync(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            return Build().DataAdapterFillAsync(SqlString, parameters, CommandType.StoredProcedure, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 执行存储过程返回 DataSet
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>DataSet</returns>
        public async Task<DataSet> SqlProcedureQueriesAsync(object model, CancellationToken cancellationToken = default)
        {
            var (dataSet, _) = await Build().DataAdapterFillAsync(SqlString, model, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return dataSet;
        }

        /// <summary>
        ///  执行存储过程返回 List 集合
        /// </summary>
        /// <typeparam name="T1">返回类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>List{T1}</returns>
        public List<T1> SqlProcedureQueries<T1>(params DbParameter[] parameters)
        {
            return Build().DataAdapterFill(SqlString, parameters, CommandType.StoredProcedure).ToList<T1>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2) SqlProcedureQueries<T1, T2>(params DbParameter[] parameters)
        {
            return Build().DataAdapterFill(SqlString, parameters, CommandType.StoredProcedure).ToList<T1, T2>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3) SqlProcedureQueries<T1, T2, T3>(params DbParameter[] parameters)
        {
            return Build().DataAdapterFill(SqlString, parameters, CommandType.StoredProcedure).ToList<T1, T2, T3>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4) SqlProcedureQueries<T1, T2, T3, T4>(params DbParameter[] parameters)
        {
            return Build().DataAdapterFill(SqlString, parameters, CommandType.StoredProcedure).ToList<T1, T2, T3, T4>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5) SqlProcedureQueries<T1, T2, T3, T4, T5>(params DbParameter[] parameters)
        {
            return Build().DataAdapterFill(SqlString, parameters, CommandType.StoredProcedure).ToList<T1, T2, T3, T4, T5>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6) SqlProcedureQueries<T1, T2, T3, T4, T5, T6>(params DbParameter[] parameters)
        {
            return Build().DataAdapterFill(SqlString, parameters, CommandType.StoredProcedure).ToList<T1, T2, T3, T4, T5, T6>();
        }

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
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7) SqlProcedureQueries<T1, T2, T3, T4, T5, T6, T7>(params DbParameter[] parameters)
        {
            return Build().DataAdapterFill(SqlString, parameters, CommandType.StoredProcedure).ToList<T1, T2, T3, T4, T5, T6, T7>();
        }

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
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7, List<T8> list8) SqlProcedureQueries<T1, T2, T3, T4, T5, T6, T7, T8>(params DbParameter[] parameters)
        {
            return Build().DataAdapterFill(SqlString, parameters, CommandType.StoredProcedure).ToList<T1, T2, T3, T4, T5, T6, T7, T8>();
        }

        /// <summary>
        ///  执行存储过程返回 List 集合
        /// </summary>
        /// <typeparam name="T1">返回类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <returns>List{T1}</returns>
        public List<T1> SqlProcedureQueries<T1>(object model)
        {
            return Build().DataAdapterFill(SqlString, model, CommandType.StoredProcedure).dataSet.ToList<T1>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2) SqlProcedureQueries<T1, T2>(object model)
        {
            return Build().DataAdapterFill(SqlString, model, CommandType.StoredProcedure).dataSet.ToList<T1, T2>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3) SqlProcedureQueries<T1, T2, T3>(object model)
        {
            return Build().DataAdapterFill(SqlString, model, CommandType.StoredProcedure).dataSet.ToList<T1, T2, T3>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4) SqlProcedureQueries<T1, T2, T3, T4>(object model)
        {
            return Build().DataAdapterFill(SqlString, model, CommandType.StoredProcedure).dataSet.ToList<T1, T2, T3, T4>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5) SqlProcedureQueries<T1, T2, T3, T4, T5>(object model)
        {
            return Build().DataAdapterFill(SqlString, model, CommandType.StoredProcedure).dataSet.ToList<T1, T2, T3, T4, T5>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6) SqlProcedureQueries<T1, T2, T3, T4, T5, T6>(object model)
        {
            return Build().DataAdapterFill(SqlString, model, CommandType.StoredProcedure).dataSet.ToList<T1, T2, T3, T4, T5, T6>();
        }

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
        /// <param name="model">参数模型</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7) SqlProcedureQueries<T1, T2, T3, T4, T5, T6, T7>(object model)
        {
            return Build().DataAdapterFill(SqlString, model, CommandType.StoredProcedure).dataSet.ToList<T1, T2, T3, T4, T5, T6, T7>();
        }

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
        /// <param name="model">参数模型</param>
        /// <returns>元组类型</returns>
        public (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7, List<T8> list8) SqlProcedureQueries<T1, T2, T3, T4, T5, T6, T7, T8>(object model)
        {
            return Build().DataAdapterFill(SqlString, model, CommandType.StoredProcedure).dataSet.ToList<T1, T2, T3, T4, T5, T6, T7, T8>();
        }

        /// <summary>
        ///  执行存储过程返回 List 集合
        /// </summary>
        /// <typeparam name="T1">返回类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>Task{List{T1}}</returns>
        public async Task<List<T1>> SqlProcedureQueriesAsync<T1>(params DbParameter[] parameters)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, CommandType.StoredProcedure);
            return dataset.ToList<T1>();
        }

        /// <summary>
        ///  执行存储过程返回 List 集合
        /// </summary>
        /// <typeparam name="T1">返回类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>Task{List{T1}}</returns>
        public async Task<List<T1>> SqlProcedureQueriesAsync<T1>(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return dataset.ToList<T1>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2)> SqlProcedureQueriesAsync<T1, T2>(params DbParameter[] parameters)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, CommandType.StoredProcedure);
            return dataset.ToList<T1, T2>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2)> SqlProcedureQueriesAsync<T1, T2>(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlProcedureQueriesAsync<T1, T2, T3>(params DbParameter[] parameters)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, CommandType.StoredProcedure);
            return dataset.ToList<T1, T2, T3>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlProcedureQueriesAsync<T1, T2, T3>(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)> SqlProcedureQueriesAsync<T1, T2, T3, T4>(params DbParameter[] parameters)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, CommandType.StoredProcedure);
            return dataset.ToList<T1, T2, T3, T4>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)> SqlProcedureQueriesAsync<T1, T2, T3, T4>(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5>(params DbParameter[] parameters)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, CommandType.StoredProcedure);
            return dataset.ToList<T1, T2, T3, T4, T5>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5>(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6>(params DbParameter[] parameters)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, CommandType.StoredProcedure);
            return dataset.ToList<T1, T2, T3, T4, T5, T6>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6>(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5, T6>();
        }

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
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(params DbParameter[] parameters)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, CommandType.StoredProcedure);
            return dataset.ToList<T1, T2, T3, T4, T5, T6, T7>();
        }

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
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5, T6, T7>();
        }

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
        /// <param name="parameters">命令参数</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7, List<T8> list8)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(params DbParameter[] parameters)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, CommandType.StoredProcedure);
            return dataset.ToList<T1, T2, T3, T4, T5, T6, T7, T8>();
        }

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
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7, List<T8> list8)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var dataset = await Build().DataAdapterFillAsync(SqlString, parameters, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5, T6, T7, T8>();
        }

        /// <summary>
        ///  执行存储过程返回 List 集合
        /// </summary>
        /// <typeparam name="T1">返回类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>List{T1}</returns>
        public async Task<List<T1>> SqlProcedureQueriesAsync<T1>(object model, CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Build().DataAdapterFillAsync(SqlString, model, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return dataset.ToList<T1>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2)> SqlProcedureQueriesAsync<T1, T2>(object model, CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Build().DataAdapterFillAsync(SqlString, model, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlProcedureQueriesAsync<T1, T2, T3>(object model, CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Build().DataAdapterFillAsync(SqlString, model, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)> SqlProcedureQueriesAsync<T1, T2, T3, T4>(object model, CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Build().DataAdapterFillAsync(SqlString, model, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5>(object model, CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Build().DataAdapterFillAsync(SqlString, model, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5>();
        }

        /// <summary>
        /// 执行存储过程返回 元组 集合
        /// </summary>
        /// <typeparam name="T1">元组元素类型</typeparam>
        /// <typeparam name="T2">元组元素类型</typeparam>
        /// <typeparam name="T3">元组元素类型</typeparam>
        /// <typeparam name="T4">元组元素类型</typeparam>
        /// <typeparam name="T5">元组元素类型</typeparam>
        /// <typeparam name="T6">元组元素类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6>(object model, CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Build().DataAdapterFillAsync(SqlString, model, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5, T6>();
        }

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
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(object model, CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Build().DataAdapterFillAsync(SqlString, model, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5, T6, T7>();
        }

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
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>元组类型</returns>
        public async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7, List<T8> list8)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(object model, CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Build().DataAdapterFillAsync(SqlString, model, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5, T6, T7, T8>();
        }

        /// <summary>
        /// 执行存储过程返回 单行单列
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>object</returns>
        public object SqlProcedureScalar(params DbParameter[] parameters)
        {
            return Build().ExecuteScalar(SqlString, parameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 执行存储过程返回 单行单列
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <returns>object</returns>
        public object SqlProcedureScalar(object model)
        {
            return Build().ExecuteScalar(SqlString, model, CommandType.StoredProcedure).result;
        }

        /// <summary>
        /// 执行存储过程返回 单行单列
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>object</returns>
        public Task<object> SqlProcedureScalarAsync(params DbParameter[] parameters)
        {
            return Build().ExecuteScalarAsync(SqlString, parameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 执行存储过程返回 单行单列
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>object</returns>
        public Task<object> SqlProcedureScalarAsync(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            return Build().ExecuteScalarAsync(SqlString, parameters, CommandType.StoredProcedure, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 执行存储过程返回 单行单列
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>object</returns>
        public async Task<object> SqlProcedureScalarAsync(object model, CancellationToken cancellationToken = default)
        {
            var (result, _) = await Build().ExecuteScalarAsync(SqlString, model, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return result;
        }

        /// <summary>
        /// 执行存储过程返回 单行单列
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>TResult</returns>
        public TResult SqlProcedureScalar<TResult>(params DbParameter[] parameters)
        {
            return Build().ExecuteScalar(SqlString, parameters, CommandType.StoredProcedure).ChangeType<TResult>();
        }

        /// <summary>
        /// 执行存储过程返回 单行单列
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <returns>TResult</returns>
        public TResult SqlProcedureScalar<TResult>(object model)
        {
            return Build().ExecuteScalar(SqlString, model, CommandType.StoredProcedure).result.ChangeType<TResult>();
        }

        /// <summary>
        /// 执行存储过程返回 单行单列
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>TResult</returns>
        public async Task<TResult> SqlProcedureScalarAsync<TResult>(params DbParameter[] parameters)
        {
            var result = await Build().ExecuteScalarAsync(SqlString, parameters, CommandType.StoredProcedure);
            return result.ChangeType<TResult>();
        }

        /// <summary>
        /// 执行存储过程返回 单行单列
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>TResult</returns>
        public async Task<TResult> SqlProcedureScalarAsync<TResult>(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var result = await Build().ExecuteScalarAsync(SqlString, parameters, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return result.ChangeType<TResult>();
        }

        /// <summary>
        /// 执行存储过程返回 单行单列
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>TResult</returns>
        public async Task<TResult> SqlProcedureScalarAsync<TResult>(object model, CancellationToken cancellationToken = default)
        {
            var (result, _) = await Build().ExecuteScalarAsync(SqlString, model, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return result.ChangeType<TResult>();
        }

        /// <summary>
        /// 执行存储过程无数据返回
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>int</returns>
        public int SqlProcedureNonQuery(params DbParameter[] parameters)
        {
            return Build().ExecuteNonQuery(SqlString, parameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 执行存储过程无数据返回
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <returns>int</returns>
        public int SqlProcedureNonQuery(object model)
        {
            return Build().ExecuteNonQuery(SqlString, model, CommandType.StoredProcedure).rowEffects;
        }

        /// <summary>
        /// 执行存储过程无数据返回
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>int</returns>
        public Task<int> SqlProcedureNonQueryAsync(params DbParameter[] parameters)
        {
            return Build().ExecuteNonQueryAsync(SqlString, parameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 执行存储过程无数据返回
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>int</returns>
        public Task<int> SqlProcedureNonQueryAsync(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            return Build().ExecuteNonQueryAsync(SqlString, parameters, CommandType.StoredProcedure, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 执行存储过程无数据返回
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>int</returns>
        public async Task<int> SqlProcedureNonQueryAsync(object model, CancellationToken cancellationToken = default)
        {
            var (rowEffects, _) = await Build().ExecuteNonQueryAsync(SqlString, model, CommandType.StoredProcedure, cancellationToken: cancellationToken);
            return rowEffects;
        }

        /// <summary>
        /// 执行 Sql 无数据返回
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>int</returns>
        public int SqlNonQuery(params DbParameter[] parameters)
        {
            return Build().ExecuteNonQuery(SqlString, parameters);
        }

        /// <summary>
        /// 执行 Sql 无数据返回
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <returns>int</returns>
        public int SqlNonQuery(object model)
        {
            return Build().ExecuteNonQuery(SqlString, model).rowEffects;
        }

        /// <summary>
        /// 执行 Sql 无数据返回
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>int</returns>
        public Task<int> SqlNonQueryAsync(params DbParameter[] parameters)
        {
            return Build().ExecuteNonQueryAsync(SqlString, parameters);
        }

        /// <summary>
        /// 执行 Sql 无数据返回
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>int</returns>
        public Task<int> SqlNonQueryAsync(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            return Build().ExecuteNonQueryAsync(SqlString, parameters, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 执行 Sql 无数据返回
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>int</returns>
        public async Task<int> SqlNonQueryAsync(object model, CancellationToken cancellationToken = default)
        {
            var (rowEffects, _) = await Build().ExecuteNonQueryAsync(SqlString, model, cancellationToken: cancellationToken);
            return rowEffects;
        }

        /// <summary>
        /// 执行 Sql 返回 单行单列
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>object</returns>
        public object SqlScalar(params DbParameter[] parameters)
        {
            return Build().ExecuteScalar(SqlString, parameters);
        }

        /// <summary>
        /// 执行 Sql 返回 单行单列
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <returns>object</returns>
        public object SqlScalar(object model)
        {
            return Build().ExecuteScalar(SqlString, model).result;
        }

        /// <summary>
        /// 执行 Sql 返回 单行单列
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>object</returns>
        public Task<object> SqlScalarAsync(params DbParameter[] parameters)
        {
            return Build().ExecuteScalarAsync(SqlString, parameters);
        }

        /// <summary>
        /// 执行 Sql 返回 单行单列
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>object</returns>
        public Task<object> SqlScalarAsync(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            return Build().ExecuteScalarAsync(SqlString, parameters, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 执行 Sql 返回 单行单列
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>object</returns>
        public async Task<object> SqlScalarAsync(object model, CancellationToken cancellationToken = default)
        {
            var (result, _) = await Build().ExecuteScalarAsync(SqlString, model, cancellationToken: cancellationToken);
            return result;
        }

        /// <summary>
        /// 执行 Sql 返回 单行单列
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>TResult</returns>
        public TResult SqlScalar<TResult>(params DbParameter[] parameters)
        {
            return Build().ExecuteScalar(SqlString, parameters).ChangeType<TResult>();
        }

        /// <summary>
        /// 执行 Sql 返回 单行单列
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <returns>TResult</returns>
        public TResult SqlScalar<TResult>(object model)
        {
            return Build().ExecuteScalar(SqlString, model).result.ChangeType<TResult>();
        }

        /// <summary>
        /// 执行 Sql 返回 单行单列
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>TResult</returns>
        public async Task<TResult> SqlScalarAsync<TResult>(params DbParameter[] parameters)
        {
            var result = await Build().ExecuteScalarAsync(SqlString, parameters);
            return result.ChangeType<TResult>();
        }

        /// <summary>
        /// 执行 Sql 返回 单行单列
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>TResult</returns>
        public async Task<TResult> SqlScalarAsync<TResult>(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var result = await Build().ExecuteScalarAsync(SqlString, parameters, cancellationToken: cancellationToken);
            return result.ChangeType<TResult>();
        }

        /// <summary>
        /// 执行 Sql 返回 单行单列
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>TResult</returns>
        public async Task<TResult> SqlScalarAsync<TResult>(object model, CancellationToken cancellationToken = default)
        {
            var (result, _) = await Build().ExecuteScalarAsync(SqlString, model, cancellationToken: cancellationToken);
            return result.ChangeType<TResult>();
        }

        /// <summary>
        /// 执行存储过程返回OUPUT、RETURN、结果集
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>ProcedureOutput</returns>
        public ProcedureOutputResult SqlProcedureOutput(DbParameter[] parameters)
        {
            parameters ??= Array.Empty<DbParameter>();

            // 执行存储过程
            var database = Build();
            var dataSet = database.DataAdapterFill(SqlString, parameters, CommandType.StoredProcedure);

            // 包装结果集
            return DbHelpers.WrapperProcedureOutput(database.ProviderName, parameters, dataSet);
        }

        /// <summary>
        /// 执行存储过程返回OUPUT、RETURN、结果集
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>ProcedureOutput</returns>
        public async Task<ProcedureOutputResult> SqlProcedureOutputAsync(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            parameters ??= Array.Empty<DbParameter>();

            // 执行存储过程
            var database = Build();
            var dataSet = await database.DataAdapterFillAsync(SqlString, parameters, CommandType.StoredProcedure, cancellationToken: cancellationToken);

            // 包装结果集
            return DbHelpers.WrapperProcedureOutput(database.ProviderName, parameters, dataSet);
        }

        /// <summary>
        /// 执行存储过程返回OUPUT、RETURN、结果集
        /// </summary>
        /// <param name="model">命令模型</param>
        /// <returns>ProcedureOutput</returns>
        public ProcedureOutputResult SqlProcedureOutput(object model)
        {
            // 执行存储过程
            var database = Build();
            var (dataSet, parameters) = database.DataAdapterFill(SqlString, model, CommandType.StoredProcedure);

            // 包装结果集
            return DbHelpers.WrapperProcedureOutput(database.ProviderName, parameters, dataSet);
        }

        /// <summary>
        /// 执行存储过程返回OUPUT、RETURN、结果集
        /// </summary>
        /// <param name="model">命令模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>ProcedureOutput</returns>
        public async Task<ProcedureOutputResult> SqlProcedureOutputAsync(object model, CancellationToken cancellationToken = default)
        {
            // 执行存储过程
            var database = Build();
            var (dataSet, parameters) = await database.DataAdapterFillAsync(SqlString, model, CommandType.StoredProcedure, cancellationToken: cancellationToken);

            // 包装结果集
            return DbHelpers.WrapperProcedureOutput(database.ProviderName, parameters, dataSet);
        }

        /// <summary>
        /// 执行存储过程返回OUPUT、RETURN、结果集
        /// </summary>
        /// <typeparam name="TResult">数据集结果</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>ProcedureOutput</returns>
        public ProcedureOutputResult<TResult> SqlProcedureOutput<TResult>(DbParameter[] parameters)
        {
            parameters ??= Array.Empty<DbParameter>();

            // 执行存储过程
            var database = Build();
            var dataSet = database.DataAdapterFill(SqlString, parameters, CommandType.StoredProcedure);

            // 包装结果集
            return DbHelpers.WrapperProcedureOutput<TResult>(database.ProviderName, parameters, dataSet);
        }

        /// <summary>
        /// 执行存储过程返回OUPUT、RETURN、结果集
        /// </summary>
        /// <typeparam name="TResult">数据集结果</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>ProcedureOutput</returns>
        public async Task<ProcedureOutputResult<TResult>> SqlProcedureOutputAsync<TResult>(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            parameters ??= Array.Empty<DbParameter>();

            // 执行存储过程
            var database = Build();
            var dataSet = await database.DataAdapterFillAsync(SqlString, parameters, CommandType.StoredProcedure, cancellationToken: cancellationToken);

            // 包装结果集
            return DbHelpers.WrapperProcedureOutput<TResult>(database.ProviderName, parameters, dataSet);
        }

        /// <summary>
        /// 执行存储过程返回OUPUT、RETURN、结果集
        /// </summary>
        /// <typeparam name="TResult">数据集结果</typeparam>
        /// <param name="model">命令模型</param>
        /// <returns>ProcedureOutput</returns>
        public ProcedureOutputResult<TResult> SqlProcedureOutput<TResult>(object model)
        {
            // 执行存储过程
            var database = Build();
            var (dataSet, parameters) = database.DataAdapterFill(SqlString, model, CommandType.StoredProcedure);

            // 包装结果集
            return DbHelpers.WrapperProcedureOutput<TResult>(database.ProviderName, parameters, dataSet);
        }

        /// <summary>
        /// 执行存储过程返回OUPUT、RETURN、结果集
        /// </summary>
        /// <typeparam name="TResult">数据集结果</typeparam>
        /// <param name="model">命令模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>ProcedureOutput</returns>
        public async Task<ProcedureOutputResult<TResult>> SqlProcedureOutputAsync<TResult>(object model, CancellationToken cancellationToken = default)
        {
            // 执行存储过程
            var database = Build();
            var (dataSet, parameters) = await database.DataAdapterFillAsync(SqlString, model, CommandType.StoredProcedure, cancellationToken: cancellationToken);

            // 包装结果集
            return DbHelpers.WrapperProcedureOutput<TResult>(database.ProviderName, parameters, dataSet);
        }

        /// <summary>
        /// 执行标量函数返回 单行单列
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>object</returns>
        public object SqlFunctionScalar(params DbParameter[] parameters)
        {
            var database = Build();
            SqlString = DbHelpers.GenerateFunctionSql(database.ProviderName, DbFunctionType.Scalar, SqlString, parameters);
            return database.ExecuteScalar(SqlString, parameters);
        }

        /// <summary>
        /// 执行标量函数返回 单行单列
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <returns>object</returns>
        public object SqlFunctionScalar(object model)
        {
            var database = Build();
            SqlString = DbHelpers.GenerateFunctionSql(database.ProviderName, DbFunctionType.Scalar, SqlString, model);
            return database.ExecuteScalar(SqlString, model).result;
        }

        /// <summary>
        /// 执行标量函数返回 单行单列
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>object</returns>
        public Task<object> SqlFunctionScalarAsync(params DbParameter[] parameters)
        {
            var database = Build();
            SqlString = DbHelpers.GenerateFunctionSql(database.ProviderName, DbFunctionType.Scalar, SqlString, parameters);
            return database.ExecuteScalarAsync(SqlString, parameters);
        }

        /// <summary>
        /// 执行标量函数返回 单行单列
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>object</returns>
        public Task<object> SqlFunctionScalarAsync(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var database = Build();
            SqlString = DbHelpers.GenerateFunctionSql(database.ProviderName, DbFunctionType.Scalar, SqlString, parameters);
            return database.ExecuteScalarAsync(SqlString, parameters, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 执行标量函数返回 单行单列
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>object</returns>
        public async Task<object> SqlFunctionScalarAsync(object model, CancellationToken cancellationToken = default)
        {
            var database = Build();
            SqlString = DbHelpers.GenerateFunctionSql(database.ProviderName, DbFunctionType.Scalar, SqlString, model);
            var (result, _) = await database.ExecuteScalarAsync(SqlString, model, cancellationToken: cancellationToken);
            return result;
        }

        /// <summary>
        /// 执行标量函数返回 单行单列
        /// </summary>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>TResult</returns>
        public TResult SqlFunctionScalar<TResult>(params DbParameter[] parameters)
        {
            var database = Build();
            SqlString = DbHelpers.GenerateFunctionSql(database.ProviderName, DbFunctionType.Scalar, SqlString, parameters);
            return database.ExecuteScalar(SqlString, parameters).ChangeType<TResult>();
        }

        /// <summary>
        /// 执行标量函数返回 单行单列
        /// </summary>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <returns>TResult</returns>
        public TResult SqlFunctionScalar<TResult>(object model)
        {
            var database = Build();
            SqlString = DbHelpers.GenerateFunctionSql(database.ProviderName, DbFunctionType.Scalar, SqlString, model);
            return database.ExecuteScalar(SqlString, model).result.ChangeType<TResult>();
        }

        /// <summary>
        /// 执行标量函数返回 单行单列
        /// </summary>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>TResult</returns>
        public async Task<TResult> SqlFunctionScalarAsync<TResult>(params DbParameter[] parameters)
        {
            var database = Build();
            SqlString = DbHelpers.GenerateFunctionSql(database.ProviderName, DbFunctionType.Scalar, SqlString, parameters);
            var result = await database.ExecuteScalarAsync(SqlString, parameters);
            return result.ChangeType<TResult>();
        }

        /// <summary>
        /// 执行标量函数返回 单行单列
        /// </summary>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>TResult</returns>
        public async Task<TResult> SqlFunctionScalarAsync<TResult>(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var database = Build();
            SqlString = DbHelpers.GenerateFunctionSql(database.ProviderName, DbFunctionType.Scalar, SqlString, parameters);
            var result = await database.ExecuteScalarAsync(SqlString, parameters, cancellationToken: cancellationToken);
            return result.ChangeType<TResult>();
        }

        /// <summary>
        /// 执行标量函数返回 单行单列
        /// </summary>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>object</returns>
        public async Task<TResult> SqlFunctionScalarAsync<TResult>(object model, CancellationToken cancellationToken = default)
        {
            var database = Build();
            SqlString = DbHelpers.GenerateFunctionSql(database.ProviderName, DbFunctionType.Scalar, SqlString, model);
            var (result, _) = await database.ExecuteScalarAsync(SqlString, model, cancellationToken: cancellationToken);
            return result.ChangeType<TResult>();
        }

        /// <summary>
        /// 执行表值函数返回 DataTable
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>DataTable</returns>
        public DataTable SqlFunctionQuery(params DbParameter[] parameters)
        {
            var database = Build();
            SqlString = DbHelpers.GenerateFunctionSql(database.ProviderName, DbFunctionType.Table, SqlString, parameters);
            return database.ExecuteReader(SqlString, parameters);
        }

        /// <summary>
        /// 执行表值函数返回 DataTable
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <returns>DataTable</returns>
        public DataTable SqlFunctionQuery(object model)
        {
            var database = Build();
            SqlString = DbHelpers.GenerateFunctionSql(database.ProviderName, DbFunctionType.Table, SqlString, model);
            return database.ExecuteReader(SqlString, model).dataTable;
        }

        /// <summary>
        /// 执行表值函数返回 DataTable
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <returns>Task{DataTable}</returns>
        public Task<DataTable> SqlFunctionQueryAsync(params DbParameter[] parameters)
        {
            var database = Build();
            SqlString = DbHelpers.GenerateFunctionSql(database.ProviderName, DbFunctionType.Table, SqlString, parameters);
            return database.ExecuteReaderAsync(SqlString, parameters);
        }

        /// <summary>
        /// 执行表值函数返回 DataTable
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>Task{DataTable}</returns>
        public Task<DataTable> SqlFunctionQueryAsync(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var database = Build();
            SqlString = DbHelpers.GenerateFunctionSql(database.ProviderName, DbFunctionType.Table, SqlString, parameters);
            return database.ExecuteReaderAsync(SqlString, parameters, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 执行表值函数返回 DataTable
        /// </summary>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>Task{DataTable}</returns>
        public async Task<DataTable> SqlFunctionQueryAsync(object model, CancellationToken cancellationToken = default)
        {
            var database = Build();
            SqlString = DbHelpers.GenerateFunctionSql(database.ProviderName, DbFunctionType.Table, SqlString, model);
            var (dataTable, _) = await database.ExecuteReaderAsync(SqlString, model, cancellationToken: cancellationToken);
            return dataTable;
        }

        /// <summary>
        /// 执行表值函数返回 List 集合
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>List{T}</returns>
        public List<T> SqlFunctionQuery<T>(params DbParameter[] parameters)
        {
            var database = Build();
            SqlString = DbHelpers.GenerateFunctionSql(database.ProviderName, DbFunctionType.Table, SqlString, parameters);
            return database.ExecuteReader(SqlString, parameters).ToList<T>();
        }

        /// <summary>
        /// 执行表值函数返回 List 集合
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <returns>List{T}</returns>
        public List<T> SqlFunctionQuery<T>(object model)
        {
            var database = Build();
            SqlString = DbHelpers.GenerateFunctionSql(database.ProviderName, DbFunctionType.Table, SqlString, model);
            return database.ExecuteReader(SqlString, model).dataTable.ToList<T>();
        }

        /// <summary>
        /// 执行表值函数返回 List 集合
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <returns>Task{List{T}}</returns>
        public async Task<List<T>> SqlFunctionQueryAsync<T>(params DbParameter[] parameters)
        {
            var database = Build();
            SqlString = DbHelpers.GenerateFunctionSql(database.ProviderName, DbFunctionType.Table, SqlString, parameters);
            var dataTable = await database.ExecuteReaderAsync(SqlString, parameters);
            return dataTable.ToList<T>();
        }

        /// <summary>
        /// 执行表值函数返回 List 集合
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>Task{List{T}}</returns>
        public async Task<List<T>> SqlFunctionQueryAsync<T>(DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var database = Build();
            SqlString = DbHelpers.GenerateFunctionSql(database.ProviderName, DbFunctionType.Table, SqlString, parameters);
            var dataTable = await database.ExecuteReaderAsync(SqlString, parameters, cancellationToken: cancellationToken);
            return dataTable.ToList<T>();
        }

        /// <summary>
        /// 执行表值函数返回 List 集合
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="model">参数模型</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>Task{List{T}}</returns>
        public async Task<List<T>> SqlFunctionQueryAsync<T>(object model, CancellationToken cancellationToken = default)
        {
            var database = Build();
            SqlString = DbHelpers.GenerateFunctionSql(database.ProviderName, DbFunctionType.Table, SqlString, model);
            var (dataTable, _) = await database.ExecuteReaderAsync(SqlString, model, cancellationToken: cancellationToken);
            return dataTable.ToList<T>();
        }

        /// <summary>
        /// 构建数据库对象
        /// </summary>
        /// <returns></returns>
        private DatabaseFacade Build()
        {
            var sqlRepositoryType = typeof(ISqlRepository<>).MakeGenericType(DbContextLocator);
            var sqlRepository = App.GetService(sqlRepositoryType, DbScoped);

            // 反射读取值
            return sqlRepositoryType.GetProperty(nameof(ISqlRepository.Database)).GetValue(sqlRepository) as DatabaseFacade;
        }
    }
}