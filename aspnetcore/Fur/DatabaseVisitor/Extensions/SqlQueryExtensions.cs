using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Extensions
{
    /// <summary>
    /// Sql 查询 拓展类
    /// </summary>
    public static class SqlQueryExtensions
    {
        #region Sql 查询 返回 DataTable + public static DataTable SqlQuery(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        /// <summary>
        /// Sql 查询 返回 DataTable
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataTable"/></returns>
        public static DataTable SqlQuery(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return databaseFacade.SqlExecuteReader(sql, commandType, parameters);
        }
        #endregion

        #region Sql 查询 返回 DataTable + public static async Task<DataTable> SqlQueryAsync(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        /// <summary>
        /// Sql 查询 返回 DataTable
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<DataTable> SqlQueryAsync(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return databaseFacade.SqlExecuteReaderAsync(sql, commandType, parameters);
        }
        #endregion


        #region Sql 查询 + public static IEnumerable<T> SqlQuery<T>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        /// <summary>
        /// Sql 查询
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<T> SqlQuery<T>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return SqlQuery(databaseFacade, sql, commandType, parameters).ToList<T>();
        }
        #endregion

        #region Sql 查询 + public static Task<IEnumerable<T>> SqlQueryAsync<T>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        /// <summary>
        /// Sql 查询
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<IEnumerable<T>> SqlQueryAsync<T>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return SqlQueryAsync(databaseFacade, sql, commandType, parameters).ToListAsync<T>();
        }
        #endregion


        #region Sql 查询 + public static object SqlQuery(this DatabaseFacade databaseFacade, string sql, Type returnType, CommandType commandType = CommandType.Text, params object[] parameters)
        /// <summary>
        /// Sql 查询
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="returnType">返回值类型</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>object</returns>
        public static object SqlQuery(this DatabaseFacade databaseFacade, string sql, Type returnType, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            if (returnType == typeof(DataTable))
            {
                return SqlQuery(databaseFacade, sql, commandType, parameters);
            }
            return SqlQuery(databaseFacade, sql, commandType, parameters).ToList(returnType);
        }
        #endregion

        #region Sql 查询 + public static Task<object> SqlQueryAsync(this DatabaseFacade databaseFacade, string sql, Type returnType, CommandType commandType = CommandType.Text, params object[] parameters)
        /// Sql 查询
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="returnType">返回值类型</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<object> SqlQueryAsync(this DatabaseFacade databaseFacade, string sql, Type returnType, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            if (returnType == typeof(DataTable))
            {
                var datatable = SqlQueryAsync(databaseFacade, sql, commandType, parameters);
                return Task.FromResult<object>(datatable.Result);
            }
            return SqlQueryAsync(databaseFacade, sql, commandType, parameters).ToListAsync(returnType);
        }
        #endregion
    }
}