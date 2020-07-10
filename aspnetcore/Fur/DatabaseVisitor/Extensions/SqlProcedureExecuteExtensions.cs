using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Extensions
{
    /// <summary>
    /// 存储过程执行拓展类
    /// </summary>
    public static class SqlProcedureExecuteExtensions
    {
        #region 执行存储过程 返回 DataTable + public static DataTable SqlProcedureExecute(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程 返回 DataTable
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataTable"/></returns>
        public static DataTable SqlProcedureExecute(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlExecuteReader(name, CommandType.StoredProcedure, parameters);
        }
        #endregion

        #region 执行存储过程 返回 DataTable + public static Task<DataTable> SqlProcedureExecuteAsync(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程 返回 DataTable
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<DataTable> SqlProcedureExecuteAsync(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlExecuteReaderAsync(name, CommandType.StoredProcedure, parameters);
        }
        #endregion


        #region 执行存储过程 返回 DataSet + public static DataSet SqlProcedureDataSetExecute(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程 返回 DataSet
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataSet"/></returns>
        public static DataSet SqlProcedureDataSetExecute(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataAdapterFill(name, CommandType.StoredProcedure, parameters);
        }
        #endregion

        #region 执行存储过程 返回 DataSet + public static Task<DataSet> SqlProcedureDataSetExecuteAsync(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程 返回 DataSet
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<DataSet> SqlProcedureDataSetExecuteAsync(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataAdapterFillAsync(name, CommandType.StoredProcedure, parameters);
        }
        #endregion


        #region 执行存储过程 + public static object SqlProcedureDataSetExecute(this DatabaseFacade databaseFacade, string name, Type[] returnTypes, params object[] parameters)
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="returnTypes">结果集类型数组</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>object</returns>
        public static object SqlProcedureDataSetExecute(this DatabaseFacade databaseFacade, string name, Type[] returnTypes, params object[] parameters)
        {
            return databaseFacade.SqlDataSetQuery(name, returnTypes, CommandType.StoredProcedure, parameters);
        }
        #endregion

        #region 执行存储过程 + public static Task<object> SqlProcedureDataSetExecuteAsync(this DatabaseFacade databaseFacade, string name, Type[] returnTypes, params object[] parameters)
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="returnTypes">结果集类型数组</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<object> SqlProcedureDataSetExecuteAsync(this DatabaseFacade databaseFacade, string name, Type[] returnTypes, params object[] parameters)
        {
            return databaseFacade.SqlDataSetQueryAsync(name, returnTypes, CommandType.StoredProcedure, parameters);
        }
        #endregion


        #region 执行存储过程 返回单个结果集 + public static IEnumerable<T> SqlProcedureExecute<T>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<T> SqlProcedureExecute<T>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetQuery<T>(name, CommandType.StoredProcedure, parameters);
        }
        #endregion

        #region 执行存储过程 返回单个结果集 + public static Task<IEnumerable<T>> SqlProcedureExecuteAsync<T>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程 返回单个结果集
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<IEnumerable<T>> SqlProcedureExecuteAsync<T>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetQueryAsync<T>(name, CommandType.StoredProcedure, parameters);
        }
        #endregion


        #region 执行存储过程返回一个结果集 + public static IEnumerable<T1> SqlProcedureDataSetExecute<T1>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程返回一个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<T1> SqlProcedureDataSetExecute<T1>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetQuery<T1>(name, CommandType.StoredProcedure, parameters);
        }
        #endregion

        #region 执行存储过程返回两个结果集 + public static (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlProcedureDataSetExecute<T1, T2>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程返回两个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlProcedureDataSetExecute<T1, T2>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetQuery<T1, T2>(name, CommandType.StoredProcedure, parameters);
        }
        #endregion

        #region 执行存储过程返回三个结果集 + public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlProcedureDataSetExecute<T1, T2, T3>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程返回三个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3}"/></returns>
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlProcedureDataSetExecute<T1, T2, T3>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetQuery<T1, T2, T3>(name, CommandType.StoredProcedure, parameters);
        }
        #endregion

        #region 执行存储过程返回四个结果集 + public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlProcedureDataSetExecute<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程返回四个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4}"/></returns>
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlProcedureDataSetExecute<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetQuery<T1, T2, T3, T4>(name, CommandType.StoredProcedure, parameters);
        }
        #endregion

        #region 执行存储过程返回五个结果集 + public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlProcedureDataSetExecute<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程返回五个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5}"/></returns>
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlProcedureDataSetExecute<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetQuery<T1, T2, T3, T4, T5>(name, CommandType.StoredProcedure, parameters);
        }
        #endregion

        #region 执行存储过程返回六个结果集 + public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程返回六个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6}"/></returns>
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetQuery<T1, T2, T3, T4, T5, T6>(name, CommandType.StoredProcedure, parameters);
        }
        #endregion

        #region 执行存储过程返回七个结果集 + public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程返回七个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6, T7}"/></returns>
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(name, CommandType.StoredProcedure, parameters);
        }
        #endregion

        #region 执行存储过程返回八个结果集 + public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程返回八个结果集
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
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/></returns>
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(name, CommandType.StoredProcedure, parameters);
        }
        #endregion


        #region 执行存储过程返回一个结果集 + public static Task<IEnumerable<T1>> SqlProcedureDataSetExecuteAsync<T1>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程返回一个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<IEnumerable<T1>> SqlProcedureDataSetExecuteAsync<T1>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetQueryAsync<T1>(name, CommandType.StoredProcedure, parameters);
        }
        #endregion

        #region 执行存储过程返回两个结果集 + public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlProcedureDataSetExecuteAsync<T1, T2>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程返回两个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlProcedureDataSetExecuteAsync<T1, T2>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetQueryAsync<T1, T2>(name, CommandType.StoredProcedure, parameters);
        }
        #endregion

        #region 执行存储过程返回三个结果集 + public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlProcedureDataSetExecuteAsync<T1, T2, T3>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程返回三个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlProcedureDataSetExecuteAsync<T1, T2, T3>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetQueryAsync<T1, T2, T3>(name, CommandType.StoredProcedure, parameters);
        }
        #endregion

        #region 执行存储过程返回四个结果集 + public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程返回四个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetQueryAsync<T1, T2, T3, T4>(name, CommandType.StoredProcedure, parameters);
        }
        #endregion

        #region 执行存储过程返回五个结果集 + public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程返回五个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetQueryAsync<T1, T2, T3, T4, T5>(name, CommandType.StoredProcedure, parameters);
        }
        #endregion

        #region 执行存储过程返回六个结果集 + public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程返回六个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(name, CommandType.StoredProcedure, parameters);
        }
        #endregion

        #region 执行存储过程返回七个结果集 + public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程返回七个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(name, CommandType.StoredProcedure, parameters);
        }
        #endregion

        #region 执行存储过程返回八个结果集 + public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程返回八个结果集
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
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(name, CommandType.StoredProcedure, parameters);
        }
        #endregion


        #region 执行存储过程 + public static (Dictionary<string, object> outputValues, object returnValue) SqlProcedureJustExecute(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程
        /// <para>带 <c>OUTPUT</c> 和 <c>RETURN</c> 输出和返回值</para>
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        public static (Dictionary<string, object> outputValues, object returnValue) SqlProcedureJustExecute(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sqlParameters = parameters.Any() ? (SqlParameter[])parameters : new SqlParameter[] { };
            var rowEffects = databaseFacade.SqlExecuteNonQuery(name, CommandType.StoredProcedure, sqlParameters);

            var outputValues = sqlParameters
               .Where(u => u.Direction == ParameterDirection.Output)
               .Select(u => new { Name = u.ParameterName, u.Value })
               .ToDictionary(u => u.Name, u => u.Value);

            var returnValue = sqlParameters.FirstOrDefault(u => u.Direction == ParameterDirection.ReturnValue)?.Value;

            return (outputValues, returnValue);
        }
        #endregion

        #region 执行存储过程 + public static Task<(Dictionary<string, object> outputValues, object returnValue)> SqlProcedureJustExecuteAsync(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        /// <summary>
        /// 执行存储过程
        /// <para>带 <c>OUTPUT</c> 和 <c>RETURN</c> 输出和返回值</para>
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static async Task<(Dictionary<string, object> outputValues, object returnValue)> SqlProcedureJustExecuteAsync(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sqlParameters = parameters.Any() ? (SqlParameter[])parameters : new SqlParameter[] { };
            var rowEffects = await databaseFacade.SqlExecuteNonQueryAsync(name, CommandType.StoredProcedure, sqlParameters);

            var outputValues = sqlParameters
             .Where(u => u.Direction == ParameterDirection.Output)
             .Select(u => new { Name = u.ParameterName, u.Value })
             .ToDictionary(u => u.Name, u => u.Value);

            var returnValue = sqlParameters.FirstOrDefault(u => u.Direction == ParameterDirection.ReturnValue)?.Value;

            return (outputValues, returnValue);
        }
        #endregion
    }
}