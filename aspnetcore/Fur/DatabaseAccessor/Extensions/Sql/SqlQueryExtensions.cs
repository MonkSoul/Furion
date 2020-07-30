using Fur.ApplicationBase.Attributes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Extensions.Sql
{
    /// <summary>
    /// Sql 查询 拓展类
    /// </summary>
    [NonWrapper]
    internal static class SqlQueryExtensions
    {
        /// <summary>
        /// Sql 查询 返回 DataTable
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataTable"/></returns>
        internal static DataTable SqlQuery(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return databaseFacade.SqlExecuteReader(sql, commandType, parameters);
        }

        /// <summary>
        /// Sql 查询 返回 DataTable
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static Task<DataTable> SqlQueryAsync(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return databaseFacade.SqlExecuteReaderAsync(sql, commandType, parameters);
        }

        /// <summary>
        /// Sql 查询
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        internal static IEnumerable<T> SqlQuery<T>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return SqlQuery(databaseFacade, sql, commandType, parameters).ToList<T>();
        }

        /// <summary>
        /// Sql 查询
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static Task<IEnumerable<T>> SqlQueryAsync<T>(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            return SqlQueryAsync(databaseFacade, sql, commandType, parameters).ToListAsync<T>();
        }

        /// <summary>
        /// Sql 查询
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="returnType">返回值类型</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>object</returns>
        internal static object SqlQuery(this DatabaseFacade databaseFacade, string sql, Type returnType, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            if (returnType == typeof(DataTable))
            {
                return SqlQuery(databaseFacade, sql, commandType, parameters);
            }
            return SqlQuery(databaseFacade, sql, commandType, parameters).ToList(returnType);
        }

        /// Sql 查询
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="returnType">返回值类型</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static Task<object> SqlQueryAsync(this DatabaseFacade databaseFacade, string sql, Type returnType, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            if (returnType == typeof(DataTable))
            {
                var datatable = SqlQueryAsync(databaseFacade, sql, commandType, parameters);
                return Task.FromResult<object>(datatable.Result);
            }
            return SqlQueryAsync(databaseFacade, sql, commandType, parameters).ToListAsync(returnType);
        }
    }
}