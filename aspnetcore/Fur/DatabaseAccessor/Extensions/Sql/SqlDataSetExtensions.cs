using Fur.Attributes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Extensions
{
    /// <summary>
    /// Sql DataSet 拓展类
    /// </summary>
    [NonInflated]
    internal static class SqlDataSetExtensions
    {
        /// <summary>
        /// Sql 查询返回 <see cref="DataSet"/>
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="Microsoft.Data.SqlClient.SqlParameter"/></param>
        /// <returns><see cref="DataSet"/></returns>
        internal static DataSet SqlDataSet(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return databaseFacade.SqlDataAdapterFill(sql, commandType, parameters);
        }

        /// <summary>
        /// Sql 查询返回 <see cref="DataSet"/>
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="Microsoft.Data.SqlClient.SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static Task<DataSet> SqlDataSetAsync(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return databaseFacade.SqlDataAdapterFillAsync(sql, commandType, parameters);
        }

        /// <summary>
        /// Sql 查询返回一个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/></param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        internal static IEnumerable<T1> SqlDataSet<T1>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return SqlDataSet(databaseFacade, sql, commandType, parameters).ToList<T1>();
        }

        /// <summary>
        /// Sql 查询返回两个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        internal static (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlDataSet<T1, T2>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return SqlDataSet(databaseFacade, sql, commandType, parameters).ToList<T1, T2>();
        }

        /// <summary>
        /// Sql 查询返回三个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3}"/></returns>
        internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlDataSet<T1, T2, T3>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return SqlDataSet(databaseFacade, sql, commandType, parameters).ToList<T1, T2, T3>();
        }

        /// <summary>
        /// Sql 查询返回四个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4}"/></returns>
        internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlDataSet<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return SqlDataSet(databaseFacade, sql, commandType, parameters).ToList<T1, T2, T3, T4>();
        }

        /// <summary>
        /// Sql 查询返回五个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5}"/></returns>
        internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlDataSet<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return SqlDataSet(databaseFacade, sql, commandType, parameters).ToList<T1, T2, T3, T4, T5>();
        }

        /// <summary>
        /// Sql 查询返回六个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6}"/></returns>
        internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlDataSet<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return SqlDataSet(databaseFacade, sql, commandType, parameters).ToList<T1, T2, T3, T4, T5, T6>();
        }

        /// <summary>
        /// Sql 查询返回七个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6, T7}"/></returns>
        internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlDataSet<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return SqlDataSet(databaseFacade, sql, commandType, parameters).ToList<T1, T2, T3, T4, T5, T6, T7>();
        }

        /// <summary>
        /// Sql 查询返回八个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <typeparam name="T8">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/></returns>
        internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlDataSet<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return SqlDataSet(databaseFacade, sql, commandType, parameters).ToList<T1, T2, T3, T4, T5, T6, T7, T8>();
        }

        /// <summary>
        /// Sql 查询返回一个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static Task<IEnumerable<T1>> SqlDataSetAsync<T1>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return SqlDataSetAsync(databaseFacade, sql, commandType, parameters).ToListAsync<T1>();
        }

        /// <summary>
        /// Sql 查询返回两个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlDataSetAsync<T1, T2>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return SqlDataSetAsync(databaseFacade, sql, commandType, parameters).ToListAsync<T1, T2>();
        }

        /// <summary>
        /// Sql 查询返回三个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlDataSetAsync<T1, T2, T3>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return SqlDataSetAsync(databaseFacade, sql, commandType, parameters).ToListAsync<T1, T2, T3>();
        }

        /// <summary>
        /// Sql 查询返回四个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlDataSetAsync<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return SqlDataSetAsync(databaseFacade, sql, commandType, parameters).ToListAsync<T1, T2, T3, T4>();
        }

        /// <summary>
        /// Sql 查询返回五个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlDataSetAsync<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return SqlDataSetAsync(databaseFacade, sql, commandType, parameters).ToListAsync<T1, T2, T3, T4, T5>();
        }

        /// <summary>
        /// Sql 查询返回六个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlDataSetAsync<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return SqlDataSetAsync(databaseFacade, sql, commandType, parameters).ToListAsync<T1, T2, T3, T4, T5, T6>();
        }

        /// <summary>
        /// Sql 查询返回七个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlDataSetAsync<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return SqlDataSetAsync(databaseFacade, sql, commandType, parameters).ToListAsync<T1, T2, T3, T4, T5, T6, T7>();
        }

        /// <summary>
        /// Sql 查询返回八个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <typeparam name="T8">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return SqlDataSetAsync(databaseFacade, sql, commandType, parameters).ToListAsync<T1, T2, T3, T4, T5, T6, T7, T8>();
        }

        /// <summary>
        /// Sql 查询返回特定个数结果集
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="returnTypes">结果集类型数组</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>object</returns>
        internal static object SqlDataSet(this DatabaseFacade databaseFacade, string sql, Type[] returnTypes, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return SqlDataSet(databaseFacade, sql, commandType, parameters).ToList(returnTypes);
        }

        /// <summary>
        /// Sql 查询返回特定个数结果集
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="returnTypes">结果集类型数组</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static Task<object> SqlDataSetAsync(this DatabaseFacade databaseFacade, string sql, Type[] returnTypes, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return SqlDataSetAsync(databaseFacade, sql, commandType, parameters).ToListAsync(returnTypes);
        }
    }
}