using Fur.DatabaseVisitor.Helpers;
using Fur.DatabaseVisitor.Options;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Extensions
{
    /// <summary>
    /// 存储过程执行拓展类
    /// </summary>
    public static class SqlProcedureExecuteExtensions
    {
        #region 执行存储过程 + public static DataTable SqlProcedureExecute(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataTable"/></returns>
        public static DataTable SqlProcedureExecute(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlExecute(sql, parameters);
        }
        #endregion

        #region 执行存储过程 + public static Task<DataTable> SqlProcedureExecuteAsync(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<DataTable> SqlProcedureExecuteAsync(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlExecuteAsync(sql, parameters);
        }
        #endregion

        #region 执行存储过程 + public static DataTable SqlProcedureExecute(this DatabaseFacade databaseFacade, string name, object parameterModel)
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="DataTable"/></returns>
        public static DataTable SqlProcedureExecute(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlExecute(sql, parameters);
        }
        #endregion

        #region 执行存储过程 + public static Task<DataTable> SqlProcedureExecuteAsync(this DatabaseFacade databaseFacade, string name, object parameterModel)
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel"参数模型></param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<DataTable> SqlProcedureExecuteAsync(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlExecuteAsync(sql, parameters);
        }
        #endregion


        #region 执行存储过程 + public static DataSet SqlProcedureDataSetExecute(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataSet"/></returns>
        public static DataSet SqlProcedureDataSetExecute(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecute(sql, parameters);
        }
        #endregion

        #region 执行存储过程 + public static Task<DataSet> SqlProcedureDataSetExecuteAsync(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<DataSet> SqlProcedureDataSetExecuteAsync(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecuteAsync(sql, parameters);
        }
        #endregion

        #region 执行存储过程 + public static DataSet SqlProcedureDataSetExecute(this DatabaseFacade databaseFacade, string name, object parameterModel)
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="DataSet"/></returns>
        public static DataSet SqlProcedureDataSetExecute(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecute(sql, parameters);
        }
        #endregion

        #region 执行存储过程 + public static Task<DataSet> SqlProcedureDataSetExecuteAsync(this DatabaseFacade databaseFacade, string name, object parameterModel)
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<DataSet> SqlProcedureDataSetExecuteAsync(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecuteAsync(sql, parameters);
        }
        #endregion

        #region 执行存储过程 + public static object SqlProcedureDataSetExecute(this DatabaseFacade databaseFacade, string name, Type[] types, params object[] parameters)
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="types">结果集类型数组</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>object</returns>
        public static object SqlProcedureDataSetExecute(this DatabaseFacade databaseFacade, string name, Type[] types, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecute(sql, parameters, types, parameters);
        }
        #endregion

        #region 执行存储过程 + public static object SqlProcedureDataSetExecute(this DatabaseFacade databaseFacade, string name, Type[] types, object parameterModel)
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="types">结果集类型数组</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns>object</returns>
        public static object SqlProcedureDataSetExecute(this DatabaseFacade databaseFacade, string name, Type[] types, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecute(sql, parameters, types, parameters);
        }
        #endregion

        #region 执行存储过程 + public static Task<object> SqlProcedureDataSetExecuteAsync(this DatabaseFacade databaseFacade, string name, Type[] types, params object[] parameters)
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="types">结果集类型数组</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<object> SqlProcedureDataSetExecuteAsync(this DatabaseFacade databaseFacade, string name, Type[] types, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecuteAsync(sql, types, parameters);
        }
        #endregion

        #region 执行存储过程 + public static Task<object> SqlProcedureDataSetExecuteAsync(this DatabaseFacade databaseFacade, string name, Type[] types, object parameterModel)
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="types">结果集类型数组</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<object> SqlProcedureDataSetExecuteAsync(this DatabaseFacade databaseFacade, string name, Type[] types, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecuteAsync(sql, types, parameters);
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
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlExecute<T>(sql, parameters);
        }
        #endregion

        #region 执行存储过程 返回单个结果集 + public static IEnumerable<T> SqlProcedureExecute<T>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        /// <summary>
        /// 执行存储过程 返回单个结果集
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<T> SqlProcedureExecute<T>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlExecute<T>(sql, parameters);
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
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlExecuteAsync<T>(sql, parameters);
        }
        #endregion

        #region 执行存储过程 返回单个结果集 + public static Task<IEnumerable<T>> SqlProcedureExecuteAsync<T>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        /// <summary>
        /// 执行存储过程 返回单个结果集
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<IEnumerable<T>> SqlProcedureExecuteAsync<T>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlExecuteAsync<T>(sql, parameters);
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
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecute<T1>(sql, parameters);
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
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecute<T1, T2>(sql, parameters);
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
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3>(sql, parameters);
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
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3, T4>(sql, parameters);
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
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3, T4, T5>(sql, parameters);
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
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3, T4, T5, T6>(sql, parameters);
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
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3, T4, T5, T6, T7>(sql, parameters);
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
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3, T4, T5, T6, T7, T8>(sql, parameters);
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
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecuteAsync<T1>(sql, parameters);
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
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2>(sql, parameters);
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
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3>(sql, parameters);
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
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3, T4>(sql, parameters);
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
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3, T4, T5>(sql, parameters);
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
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3, T4, T5, T6>(sql, parameters);
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
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7>(sql, parameters);
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
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7, T8>(sql, parameters);
        }
        #endregion


        #region 执行存储过程返回一个结果集 + public static IEnumerable<T1> SqlProcedureDataSetExecute<T1>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        /// <summary>
        /// 执行存储过程返回一个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<T1> SqlProcedureDataSetExecute<T1>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecute<T1>(sql, parameters);
        }
        #endregion

        #region 执行存储过程返回两个结果集 + public static (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlProcedureDataSetExecute<T1, T2>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        /// <summary>
        /// 执行存储过程返回两个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlProcedureDataSetExecute<T1, T2>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecute<T1, T2>(sql, parameters);
        }
        #endregion

        #region 执行存储过程返回三个结果集 + public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlProcedureDataSetExecute<T1, T2, T3>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        /// <summary>
        /// 执行存储过程返回三个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2, T3}"/></returns>
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlProcedureDataSetExecute<T1, T2, T3>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3>(sql, parameters);
        }
        #endregion

        #region 执行存储过程返回四个结果集 + public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlProcedureDataSetExecute<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        /// <summary>
        /// 执行存储过程返回四个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4}"/></returns>
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlProcedureDataSetExecute<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3, T4>(sql, parameters);
        }
        #endregion

        #region 执行存储过程返回五个结果集 + public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlProcedureDataSetExecute<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string name, object parameterModel)
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
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5}"/></returns>
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlProcedureDataSetExecute<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3, T4, T5>(sql, parameters);
        }
        #endregion

        #region 执行存储过程返回六个结果集 + public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string name, object parameterModel)
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
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6}"/></returns>
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3, T4, T5, T6>(sql, parameters);
        }
        #endregion

        #region 执行存储过程返回七个结果集 + public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string name, object parameterModel)
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
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6, T7}"/></returns>
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3, T4, T5, T6, T7>(sql, parameters);
        }
        #endregion

        #region 执行存储过程返回八个结果集 + public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string name, object parameterModel)
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
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/></returns>
        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3, T4, T5, T6, T7, T8>(sql, parameters);
        }
        #endregion


        #region 执行存储过程返回一个结果集 + public static Task<IEnumerable<T1>> SqlProcedureDataSetExecuteAsync<T1>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        /// <summary>
        /// 执行存储过程返回一个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<IEnumerable<T1>> SqlProcedureDataSetExecuteAsync<T1>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecuteAsync<T1>(sql, parameters);
        }
        #endregion

        #region 执行存储过程返回两个结果集 + public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlProcedureDataSetExecuteAsync<T1, T2>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        /// <summary>
        /// 执行存储过程返回两个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlProcedureDataSetExecuteAsync<T1, T2>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2>(sql, parameters);
        }
        #endregion

        #region 执行存储过程返回三个结果集 + public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlProcedureDataSetExecuteAsync<T1, T2, T3>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        /// <summary>
        /// 执行存储过程返回三个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlProcedureDataSetExecuteAsync<T1, T2, T3>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3>(sql, parameters);
        }
        #endregion

        #region 执行存储过程返回四个结果集 + public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        /// <summary>
        /// 执行存储过程返回四个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3, T4>(sql, parameters);
        }
        #endregion

        #region 支持存储过程返回五个结果集 + public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        /// <summary>
        /// 支持存储过程返回五个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3, T4, T5>(sql, parameters);
        }
        #endregion

        #region 执行存储过程返回六个结果集 + public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string name, object parameterModel)
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
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3, T4, T5, T6>(sql, parameters);
        }
        #endregion

        #region 支持存储过程返回七个结果集 + public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        /// <summary>
        /// 支持存储过程返回七个结果集
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
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7>(sql, parameters);
        }
        #endregion

        #region 执行存储过程返回八个结果集 + public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string name, object parameterModel)
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
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7, T8>(sql, parameters);
        }
        #endregion


        #region 执行存储过程 + public static (Dictionary<string, object> outputValues, object returnValue) SqlProcedureRepayExecute(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程
        /// <para>带 <c>OUTPUT</c> 和 <c>RETURN</c> 输出和返回值</para>
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        public static (Dictionary<string, object> outputValues, object returnValue) SqlProcedureRepayExecute(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlRepayExecute(sql, parameters);
        }
        #endregion

        #region 执行存储过程 + public static Task<(Dictionary<string, object> outputValues, object returnValue)> SqlProcedureRepayExecuteAsync(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        /// <summary>
        /// 执行存储过程
        /// <para>带 <c>OUTPUT</c> 和 <c>RETURN</c> 输出和返回值</para>
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<(Dictionary<string, object> outputValues, object returnValue)> SqlProcedureRepayExecuteAsync(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlRepayExecuteAsync(sql, parameters);
        }
        #endregion

        #region 执行存储过程 + public static (Dictionary<string, object> outputValues, object returnValue) SqlProcedureRepayExecute(this DatabaseFacade databaseFacade, string name, object parameterModel)
        /// <summary>
        /// 执行存储过程
        /// <para>带 <c>OUTPUT</c> 和 <c>RETURN</c> 输出和返回值</para>
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        public static (Dictionary<string, object> outputValues, object returnValue) SqlProcedureRepayExecute(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlRepayExecute(sql, parameters);
        }
        #endregion

        #region 执行存储过程 + public static Task<(Dictionary<string, object> outputValues, object returnValue)> SqlProcedureRepayExecuteAsync(this DatabaseFacade databaseFacade, string name, object parameterModel)
        /// <summary>
        /// 执行存储过程
        /// <para>带 <c>OUTPUT</c> 和 <c>RETURN</c> 输出和返回值</para>
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<(Dictionary<string, object> outputValues, object returnValue)> SqlProcedureRepayExecuteAsync(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlRepayExecuteAsync(sql, parameters);
        }
        #endregion
    }
}