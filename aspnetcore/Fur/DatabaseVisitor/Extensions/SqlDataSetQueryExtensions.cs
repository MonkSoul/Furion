using Fur.DatabaseVisitor.Utilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Extensions
{
    /// <summary>
    /// Sql 查询返回多结果集拓展类
    /// </summary>
    public static class SqlDataSetQueryExtensions
    {
        #region Sql 查询返回 DataSet + public static DataSet SqlDataSetQuery(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        /// <summary>
        /// Sql 查询返回 <see cref="DataSet"/>
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="Microsoft.Data.SqlClient.SqlParameter"/></param>
        /// <returns><see cref="DataSet"/></returns>
        public static DataSet SqlDataSetQuery(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var (dbConnection, dbCommand, dbDataAdapter) = PrepareDbDataAdapter(databaseFacade, sql, commandType, parameters);
            using var dataSet = new DataSet();

            dbDataAdapter.Fill(dataSet);

            dbConnection.Close();
            dbCommand.Parameters.Clear();

            return dataSet;
        }
        #endregion

        #region Sql 查询返回 DataSet + public static async Task<DataSet> SqlDataSetQueryAsync(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        /// <summary>
        /// Sql 查询返回 <see cref="DataSet"/>
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="Microsoft.Data.SqlClient.SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static async Task<DataSet> SqlDataSetQueryAsync(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var (dbConnection, dbCommand, dbDataAdapter) = await PrepareDbDataAdapterAsync(databaseFacade, sql, commandType, parameters);
            using var dataSet = new DataSet();

            dbDataAdapter.Fill(dataSet);

            await dbConnection.CloseAsync();
            dbCommand.Parameters.Clear();

            return dataSet;
        }
        #endregion


        #region Sql 查询返回一个结果集 + public static IEnumerable<T1> SqlDataSetQuery<T1>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        /// <summary>
        /// Sql 查询返回一个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/></param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<T1> SqlDataSetQuery<T1>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = SqlDataSetQuery(databaseFacade, sql, commandType, parameters);

            if (dataset.Tables.Count == 0) return default;
            return dataset.Tables[0].ToList<T1>();
        }
        #endregion

        #region Sql 查询返回两个结果集 + public static (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlDataSetQuery<T1, T2>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
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
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlDataSetQuery<T1, T2>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = SqlDataSetQuery(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>());
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default);
            }
            return default;
        }
        #endregion

        #region Sql 查询返回三个结果集 + public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlDataSetQuery<T1, T2, T3>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
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
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlDataSetQuery<T1, T2, T3>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = SqlDataSetQuery(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>());
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default);
            }
            return default;
        }
        #endregion

        #region Sql 查询返回四个结果集 + public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlDataSetQuery<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
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
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlDataSetQuery<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = SqlDataSetQuery(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 4)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>());
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default, default);
            }
            return default;
        }
        #endregion

        #region Sql 查询返回五个结果集 + public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlDataSetQuery<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
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
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlDataSetQuery<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = SqlDataSetQuery(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 5)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>());
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default, default, default);
            }
            return default;
        }
        #endregion

        #region Sql 查询返回六个结果集 + public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlDataSetQuery<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
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
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlDataSetQuery<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = SqlDataSetQuery(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 6)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>());
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), default);
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), default, default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), default, default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default, default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default, default, default, default);
            }
            return default;
        }
        #endregion

        #region Sql 查询返回七个结果集 + public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
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
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = SqlDataSetQuery(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 7)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>(), dataset.Tables[6].ToList<T7>());
            }
            else if (dataset.Tables.Count == 6)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>(), default);
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), default, default);
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), default, default, default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), default, default, default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default, default, default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default, default, default, default, default);
            }
            return default;
        }
        #endregion

        #region Sql 查询返回八个结果集 + public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
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
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = SqlDataSetQuery(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 8)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>(), dataset.Tables[6].ToList<T7>(), dataset.Tables[7].ToList<T8>());
            }
            else if (dataset.Tables.Count == 7)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>(), dataset.Tables[6].ToList<T7>(), default);
            }
            else if (dataset.Tables.Count == 6)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>(), default, default);
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), default, default, default);
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), default, default, default, default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), default, default, default, default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default, default, default, default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default, default, default, default, default, default);
            }
            return default;
        }
        #endregion


        #region Sql 查询返回一个结果集 + public static async Task<IEnumerable<T1>> SqlDataSetQueryAsync<T1>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        /// <summary>
        /// Sql 查询返回一个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static async Task<IEnumerable<T1>> SqlDataSetQueryAsync<T1>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = await SqlDataSetQueryAsync(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count == 0) return default;
            return dataset.Tables[0].ToList<T1>();
        }
        #endregion

        #region Sql 查询返回两个结果集 + public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlDataSetQueryAsync<T1, T2>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
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
        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlDataSetQueryAsync<T1, T2>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = await SqlDataSetQueryAsync(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>());
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default);
            }
            return default;
        }
        #endregion

        #region Sql 查询返回三个结果集 + public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlDataSetQueryAsync<T1, T2, T3>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
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
        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlDataSetQueryAsync<T1, T2, T3>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = await SqlDataSetQueryAsync(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>());
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default);
            }
            return default;
        }
        #endregion

        #region Sql 查询返回四个结果集 + public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlDataSetQueryAsync<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
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
        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlDataSetQueryAsync<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = await SqlDataSetQueryAsync(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 4)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>());
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default, default);
            }
            return default;
        }
        #endregion

        #region Sql 查询返回五个结果集 + public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
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
        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = await SqlDataSetQueryAsync(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 5)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>());
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default, default, default);
            }
            return default;
        }
        #endregion

        #region Sql 查询返回六个结果集 + public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
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
        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = await SqlDataSetQueryAsync(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 6)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>());
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), default);
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), default, default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), default, default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default, default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default, default, default, default);
            }
            return default;
        }
        #endregion

        #region Sql 查询返回七个结果集 + public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
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
        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = await SqlDataSetQueryAsync(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 7)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>(), dataset.Tables[6].ToList<T7>());
            }
            else if (dataset.Tables.Count == 6)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>(), default);
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), default, default);
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), default, default, default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), default, default, default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default, default, default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default, default, default, default, default);
            }
            return default;
        }
        #endregion

        #region Sql 查询返回八个结果集 + public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
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
        public static async Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = await SqlDataSetQueryAsync(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 8)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>(), dataset.Tables[6].ToList<T7>(), dataset.Tables[7].ToList<T8>());
            }
            else if (dataset.Tables.Count == 7)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>(), dataset.Tables[6].ToList<T7>(), default);
            }
            else if (dataset.Tables.Count == 6)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), dataset.Tables[5].ToList<T6>(), default, default);
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), dataset.Tables[4].ToList<T5>(), default, default, default);
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), dataset.Tables[3].ToList<T4>(), default, default, default, default);
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), dataset.Tables[2].ToList<T3>(), default, default, default, default, default);
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList<T1>(), dataset.Tables[1].ToList<T2>(), default, default, default, default, default, default);
            }
            else if (dataset.Tables.Count == 1)
            {
                return (dataset.Tables[0].ToList<T1>(), default, default, default, default, default, default, default);
            }
            return default;
        }
        #endregion


        #region Sql 查询返回特定个数结果集 + public static object SqlDataSetQuery(this DatabaseFacade databaseFacade, string sql, Type[] types, CommandType commandType = CommandType.Text, params object[] parameters)
        /// <summary>
        /// Sql 查询返回特定个数结果集
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="types">结果集类型数组</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>object</returns>
        public static object SqlDataSetQuery(this DatabaseFacade databaseFacade, string sql, Type[] types, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = SqlDataSetQuery(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 8)
            {
                return (dataset.Tables[0].ToList(types[0]), dataset.Tables[1].ToList(types[1]), dataset.Tables[2].ToList(types[2]), dataset.Tables[3].ToList(types[3]), dataset.Tables[4].ToList(types[4]), dataset.Tables[5].ToList(types[5]), dataset.Tables[6].ToList(types[6]), dataset.Tables[7].ToList(types[7]));
            }
            else if (dataset.Tables.Count == 7)
            {
                return (dataset.Tables[0].ToList(types[0]), dataset.Tables[1].ToList(types[1]), dataset.Tables[2].ToList(types[2]), dataset.Tables[3].ToList(types[3]), dataset.Tables[4].ToList(types[4]), dataset.Tables[5].ToList(types[5]), dataset.Tables[6].ToList(types[6]));
            }
            else if (dataset.Tables.Count == 6)
            {
                return (dataset.Tables[0].ToList(types[0]), dataset.Tables[1].ToList(types[1]), dataset.Tables[2].ToList(types[2]), dataset.Tables[3].ToList(types[3]), dataset.Tables[4].ToList(types[4]), dataset.Tables[5].ToList(types[5]));
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToList(types[0]), dataset.Tables[1].ToList(types[1]), dataset.Tables[2].ToList(types[2]), dataset.Tables[3].ToList(types[3]), dataset.Tables[4].ToList(types[4]));
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToList(types[0]), dataset.Tables[1].ToList(types[1]), dataset.Tables[2].ToList(types[2]), dataset.Tables[3].ToList(types[3]));
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList(types[0]), dataset.Tables[1].ToList(types[1]), dataset.Tables[2].ToList(types[2]));
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList(types[0]), dataset.Tables[1].ToList(types[1]));
            }
            else if (dataset.Tables.Count == 1)
            {
                return dataset.Tables[0].ToList(types[0]);
            }
            return default;
        }
        #endregion

        #region Sql 查询返回特定个数结果集 + public static async Task<object> SqlDataSetQueryAsync(this DatabaseFacade databaseFacade, string sql, Type[] types, CommandType commandType = CommandType.Text, params object[] parameters)
        /// <summary>
        /// Sql 查询返回特定个数结果集
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="types">结果集类型数组</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static async Task<object> SqlDataSetQueryAsync(this DatabaseFacade databaseFacade, string sql, Type[] types, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dataset = await SqlDataSetQueryAsync(databaseFacade, sql, commandType, parameters);
            if (dataset.Tables.Count >= 8)
            {
                return (dataset.Tables[0].ToList(types[0]), dataset.Tables[1].ToList(types[1]), dataset.Tables[2].ToList(types[2]), dataset.Tables[3].ToList(types[3]), dataset.Tables[4].ToList(types[4]), dataset.Tables[5].ToList(types[5]), dataset.Tables[6].ToList(types[6]), dataset.Tables[7].ToList(types[7]));
            }
            else if (dataset.Tables.Count == 7)
            {
                return (dataset.Tables[0].ToList(types[0]), dataset.Tables[1].ToList(types[1]), dataset.Tables[2].ToList(types[2]), dataset.Tables[3].ToList(types[3]), dataset.Tables[4].ToList(types[4]), dataset.Tables[5].ToList(types[5]), dataset.Tables[6].ToList(types[6]));
            }
            else if (dataset.Tables.Count == 6)
            {
                return (dataset.Tables[0].ToList(types[0]), dataset.Tables[1].ToList(types[1]), dataset.Tables[2].ToList(types[2]), dataset.Tables[3].ToList(types[3]), dataset.Tables[4].ToList(types[4]), dataset.Tables[5].ToList(types[5]));
            }
            else if (dataset.Tables.Count == 5)
            {
                return (dataset.Tables[0].ToList(types[0]), dataset.Tables[1].ToList(types[1]), dataset.Tables[2].ToList(types[2]), dataset.Tables[3].ToList(types[3]), dataset.Tables[4].ToList(types[4]));
            }
            else if (dataset.Tables.Count == 4)
            {
                return (dataset.Tables[0].ToList(types[0]), dataset.Tables[1].ToList(types[1]), dataset.Tables[2].ToList(types[2]), dataset.Tables[3].ToList(types[3]));
            }
            else if (dataset.Tables.Count == 3)
            {
                return (dataset.Tables[0].ToList(types[0]), dataset.Tables[1].ToList(types[1]), dataset.Tables[2].ToList(types[2]));
            }
            else if (dataset.Tables.Count == 2)
            {
                return (dataset.Tables[0].ToList(types[0]), dataset.Tables[1].ToList(types[1]));
            }
            else if (dataset.Tables.Count == 1)
            {
                return dataset.Tables[0].ToList(types[0]);
            }
            return default;
        }
        #endregion


        #region 准备 DbDataAdapter 对象 + private static (DbConnection, DbCommand, DbDataAdapter) PrepareDbDataAdapter(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        /// <summary>
        /// 准备 <see cref="DbDataAdapter"/> 对象
        /// <para>包括参数追加、性能监测包装</para>
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3}"/></returns>
        private static (DbConnection, DbCommand, DbDataAdapter) PrepareDbDataAdapter(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dbConnection = databaseFacade.GetDbConnection();
            var dbProviderFactory = DbProviderFactories.GetFactory(dbConnection);
            dbConnection = new ProfiledDbConnection(dbConnection, MiniProfiler.Current);
            var profiledDbProviderFactory = new ProfiledDbProviderFactory(dbProviderFactory, true);

            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }

            DbCommand dbCommand = dbConnection.CreateCommand();
            var dbDataAdapter = profiledDbProviderFactory.CreateDataAdapter();
            dbCommand.CommandType = commandType;
            dbCommand.CommandText = sql;
            Utility.RectifySqlParameters(ref dbCommand, parameters);
            dbDataAdapter.SelectCommand = dbCommand;

            return (dbConnection, dbCommand, dbDataAdapter);
        }
        #endregion

        #region 准备 DbDataAdapter 对象 + private static async Task<(DbConnection, DbCommand, DbDataAdapter)> PrepareDbDataAdapterAsync(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        /// <summary>
        /// 准备 <see cref="DbDataAdapter"/> 对象
        /// <para>包括参数追加、性能监测包装</para>
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3}"/></returns>
        private static async Task<(DbConnection, DbCommand, DbDataAdapter)> PrepareDbDataAdapterAsync(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dbConnection = databaseFacade.GetDbConnection();
            var dbProviderFactory = DbProviderFactories.GetFactory(dbConnection);
            dbConnection = new ProfiledDbConnection(dbConnection, MiniProfiler.Current);
            var profiledDbProviderFactory = new ProfiledDbProviderFactory(dbProviderFactory, true);

            if (dbConnection.State == ConnectionState.Closed)
            {
                await dbConnection.OpenAsync();
            }

            DbCommand dbCommand = dbConnection.CreateCommand();
            var dbDataAdapter = profiledDbProviderFactory.CreateDataAdapter();
            dbCommand.CommandType = commandType;
            dbCommand.CommandText = sql;
            Utility.RectifySqlParameters(ref dbCommand, parameters);
            dbDataAdapter.SelectCommand = dbCommand;

            return await Task.FromResult((dbConnection, dbCommand, dbDataAdapter));
        }
        #endregion
    }
}