using Fur.ApplicationBase.Attributes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Extensions.Sql
{
    /// <summary>
    /// Sql 存储过程 拓展类
    /// </summary>
    [NonWrapper]
    internal static class SqlProcedureExtensions
    {
        #region 执行存储过程 返回 DataTable + internal static DataTable SqlProcedure(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程 返回 DataTable
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataTable"/></returns>
        internal static DataTable SqlProcedure(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlExecuteReader(name, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程 返回 DataTable + internal static DataTable SqlProcedure(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        #region 执行存储过程 返回 DataTable + internal static Task<DataTable> SqlProcedureAsync(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程 返回 DataTable
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static Task<DataTable> SqlProcedureAsync(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlExecuteReaderAsync(name, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程 返回 DataTable + internal static Task<DataTable> SqlProcedureAsync(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        #region 执行存储过程 返回 DataSet + internal static DataSet SqlProcedureDataSet(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程 返回 DataSet
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataSet"/></returns>
        internal static DataSet SqlProcedureDataSet(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataAdapterFill(name, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程 返回 DataSet + internal static DataSet SqlProcedureDataSet(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        #region 执行存储过程 返回 DataSet + internal static Task<DataSet> SqlProcedureDataSetAsync(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程 返回 DataSet
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static Task<DataSet> SqlProcedureDataSetAsync(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataAdapterFillAsync(name, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程 返回 DataSet + internal static Task<DataSet> SqlProcedureDataSetAsync(this DatabaseFacade databaseFacade, string name, params object[] parameters)


        #region 执行存储过程 + internal static object SqlProcedureDataSet(this DatabaseFacade databaseFacade, string name, Type[] returnTypes, params object[] parameters)

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="returnTypes">结果集类型数组</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>object</returns>
        internal static object SqlProcedureDataSet(this DatabaseFacade databaseFacade, string name, Type[] returnTypes, params object[] parameters)
        {
            return databaseFacade.SqlDataSet(name, returnTypes, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程 + internal static object SqlProcedureDataSet(this DatabaseFacade databaseFacade, string name, Type[] returnTypes, params object[] parameters)

        #region 执行存储过程 + internal static Task<object> SqlProcedureDataSetAsync(this DatabaseFacade databaseFacade, string name, Type[] returnTypes, params object[] parameters)

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="returnTypes">结果集类型数组</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static Task<object> SqlProcedureDataSetAsync(this DatabaseFacade databaseFacade, string name, Type[] returnTypes, params object[] parameters)
        {
            return databaseFacade.SqlDataSetAsync(name, returnTypes, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程 + internal static Task<object> SqlProcedureDataSetAsync(this DatabaseFacade databaseFacade, string name, Type[] returnTypes, params object[] parameters)


        #region 执行存储过程 返回单个结果集 + internal static IEnumerable<T> SqlProcedure<T>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        internal static IEnumerable<T> SqlProcedure<T>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSet<T>(name, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程 返回单个结果集 + internal static IEnumerable<T> SqlProcedure<T>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        #region 执行存储过程 返回单个结果集 + internal static Task<IEnumerable<T>> SqlProcedureAsync<T>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程 返回单个结果集
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static Task<IEnumerable<T>> SqlProcedureAsync<T>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetAsync<T>(name, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程 返回单个结果集 + internal static Task<IEnumerable<T>> SqlProcedureAsync<T>(this DatabaseFacade databaseFacade, string name, params object[] parameters)


        #region 执行存储过程返回一个结果集 + internal static IEnumerable<T1> SqlProcedureDataSet<T1>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回一个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        internal static IEnumerable<T1> SqlProcedureDataSet<T1>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSet<T1>(name, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程返回一个结果集 + internal static IEnumerable<T1> SqlProcedureDataSet<T1>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        #region 执行存储过程返回两个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlProcedureDataSet<T1, T2>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回两个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        internal static (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlProcedureDataSet<T1, T2>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSet<T1, T2>(name, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程返回两个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlProcedureDataSet<T1, T2>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        #region 执行存储过程返回三个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlProcedureDataSet<T1, T2, T3>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

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
        internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlProcedureDataSet<T1, T2, T3>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSet<T1, T2, T3>(name, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程返回三个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlProcedureDataSet<T1, T2, T3>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        #region 执行存储过程返回四个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlProcedureDataSet<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

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
        internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlProcedureDataSet<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSet<T1, T2, T3, T4>(name, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程返回四个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlProcedureDataSet<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        #region 执行存储过程返回五个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlProcedureDataSet<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

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
        internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlProcedureDataSet<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSet<T1, T2, T3, T4, T5>(name, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程返回五个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlProcedureDataSet<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        #region 执行存储过程返回六个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

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
        internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSet<T1, T2, T3, T4, T5, T6>(name, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程返回六个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        #region 执行存储过程返回七个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

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
        internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSet<T1, T2, T3, T4, T5, T6, T7>(name, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程返回七个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        #region 执行存储过程返回八个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

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
        internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSet<T1, T2, T3, T4, T5, T6, T7, T8>(name, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程返回八个结果集 + internal static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string name, params object[] parameters)


        #region 执行存储过程返回一个结果集 + internal static Task<IEnumerable<T1>> SqlProcedureDataSetAsync<T1>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回一个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static Task<IEnumerable<T1>> SqlProcedureDataSetAsync<T1>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetAsync<T1>(name, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程返回一个结果集 + internal static Task<IEnumerable<T1>> SqlProcedureDataSetAsync<T1>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        #region 执行存储过程返回两个结果集 + internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlProcedureDataSetAsync<T1, T2>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回两个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlProcedureDataSetAsync<T1, T2>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetAsync<T1, T2>(name, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程返回两个结果集 + internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlProcedureDataSetAsync<T1, T2>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        #region 执行存储过程返回三个结果集 + internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlProcedureDataSetAsync<T1, T2, T3>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

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
        internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlProcedureDataSetAsync<T1, T2, T3>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetAsync<T1, T2, T3>(name, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程返回三个结果集 + internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlProcedureDataSetAsync<T1, T2, T3>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        #region 执行存储过程返回四个结果集 + internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlProcedureDataSetAsync<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

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
        internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlProcedureDataSetAsync<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetAsync<T1, T2, T3, T4>(name, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程返回四个结果集 + internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlProcedureDataSetAsync<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        #region 执行存储过程返回五个结果集 + internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

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
        internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetAsync<T1, T2, T3, T4, T5>(name, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程返回五个结果集 + internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        #region 执行存储过程返回六个结果集 + internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

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
        internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetAsync<T1, T2, T3, T4, T5, T6>(name, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程返回六个结果集 + internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        #region 执行存储过程返回七个结果集 + internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

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
        internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetAsync<T1, T2, T3, T4, T5, T6, T7>(name, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程返回七个结果集 + internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        #region 执行存储过程返回八个结果集 + internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string name, params object[] parameters)

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
        internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            return databaseFacade.SqlDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8>(name, CommandType.StoredProcedure, parameters);
        }

        #endregion 执行存储过程返回八个结果集 + internal static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string name, params object[] parameters)


        #region 执行存储过程 + internal static (Dictionary<string, object> outputValues, object returnValue) SqlProcedureNonQuery(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程
        /// <para>带 <c>OUTPUT</c> 和 <c>RETURN</c> 输出和返回值</para>
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        internal static (Dictionary<string, object> outputValues, object returnValue) SqlProcedureNonQuery(this DatabaseFacade databaseFacade, string name, params object[] parameters)
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

        #endregion 执行存储过程 + internal static (Dictionary<string, object> outputValues, object returnValue) SqlProcedureNonQuery(this DatabaseFacade databaseFacade, string name, params object[] parameters)

        #region 执行存储过程 + internal static Task<(Dictionary<string, object> outputValues, object returnValue)> SqlProcedureNonQueryAsync(this DatabaseFacade databaseFacade, string sql, params object[] parameters)

        /// <summary>
        /// 执行存储过程
        /// <para>带 <c>OUTPUT</c> 和 <c>RETURN</c> 输出和返回值</para>
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<(Dictionary<string, object> outputValues, object returnValue)> SqlProcedureNonQueryAsync(this DatabaseFacade databaseFacade, string name, params object[] parameters)
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

        #endregion 执行存储过程 + internal static Task<(Dictionary<string, object> outputValues, object returnValue)> SqlProcedureNonQueryAsync(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
    }
}