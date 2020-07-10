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
using System.Linq;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Extensions
{
    /// <summary>
    /// Sql 查询或执行拓展类
    /// </summary>
    public static class SqlQueryOrExecuteExtensions
    {
        #region Sql 查询 + public static DataTable SqlQuery(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        /// <summary>
        /// Sql 查询
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataTable"/></returns>
        public static DataTable SqlQuery(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var (dbConnection, dbCommand) = databaseFacade.PrepareDbCommand(sql, commandType, parameters);
            using var dbDataReader = dbCommand.ExecuteReader();
            using var dataTable = new DataTable();

            dataTable.Load(dbDataReader);

            dbDataReader.Close();
            dbConnection.Close();
            dbCommand.Parameters.Clear();

            return dataTable;
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

        #region Sql 查询 + public static object SqlQuery(this DatabaseFacade databaseFacade, string sql, Type type, CommandType commandType = CommandType.Text, params object[] parameters)
        /// <summary>
        /// Sql 查询
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="type">返回值类型</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>object</returns>
        public static object SqlQuery(this DatabaseFacade databaseFacade, string sql, Type type, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            if (type == typeof(DataTable))
            {
                return SqlQuery(databaseFacade, sql, commandType, parameters);
            }
            return SqlQuery(databaseFacade, sql, commandType, parameters).ToList(type);
        }
        #endregion

        #region Sql 查询 + public static async Task<DataTable> SqlQueryAsync(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        /// <summary>
        /// Sql 查询
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static async Task<DataTable> SqlQueryAsync(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var (dbConnection, dbCommand) = await databaseFacade.PrepareDbCommandAsync(sql, commandType, parameters);
            using var dbDataReader = await dbCommand.ExecuteReaderAsync();
            using var dataTable = new DataTable();

            dataTable.Load(dbDataReader);

            await dbDataReader.CloseAsync();
            await dbConnection.CloseAsync();
            dbCommand.Parameters.Clear();

            return dataTable;
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

        #region Sql 查询 + public static Task<object> SqlQueryAsync(this DatabaseFacade databaseFacade, string sql, Type type, CommandType commandType = CommandType.Text, params object[] parameters)
        /// <summary>
        /// Sql 查询
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="type">返回值类型</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<object> SqlQueryAsync(this DatabaseFacade databaseFacade, string sql, Type type, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            if (type == typeof(DataTable))
            {
                var datatable = SqlQueryAsync(databaseFacade, sql, commandType, parameters);
                return Task.FromResult<object>(datatable.Result);
            }
            return SqlQueryAsync(databaseFacade, sql, commandType, parameters).ToListAsync(type);
        }
        #endregion


        #region Sql 执行 + internal static (Dictionary<string, object> outputValues, object returnValue) SqlExecute(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.StoredProcedure, params object[] parameters)
        /// <summary>
        /// Sql 执行
        /// <para>带 <c>OUTPUT</c> 和 <c>RETURN</c> 输出和返回值</para>
        /// <para>用于执行存储过程</para>
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/></param>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        internal static (Dictionary<string, object> outputValues, object returnValue) SqlExecute(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.StoredProcedure, params object[] parameters)
        {
            var sqlParameters = parameters.Any() ? (SqlParameter[])parameters : new SqlParameter[] { };
            var (dbConnection, dbCommand) = databaseFacade.PrepareDbCommand(sql, commandType, sqlParameters);

            var rowEffects = dbCommand.ExecuteNonQuery();

            dbConnection.Close();

            var outputValues = sqlParameters
                .Where(u => u.Direction == ParameterDirection.Output)
                .Select(u => new { Name = u.ParameterName, u.Value })
                .ToDictionary(u => u.Name, u => u.Value);

            var returnValue = sqlParameters.FirstOrDefault(u => u.Direction == ParameterDirection.ReturnValue)?.Value;

            dbCommand.Parameters.Clear();
            return (outputValues, returnValue);
        }
        #endregion

        #region Sql 执行 + internal static async Task<(Dictionary<string, object> outputValues, object returnValue)> SqlExecuteAsync(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.StoredProcedure, params object[] parameters)
        /// <summary>
        /// Sql 执行
        /// <para>带 <c>OUTPUT</c> 和 <c>RETURN</c> 输出和返回值</para>
        /// <para>用于执行存储过程</para>
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/></param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<(Dictionary<string, object> outputValues, object returnValue)> SqlExecuteAsync(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.StoredProcedure, params object[] parameters)
        {
            var sqlParameters = parameters.Any() ? (SqlParameter[])parameters : new SqlParameter[] { };
            var (dbConnection, dbCommand) = await databaseFacade.PrepareDbCommandAsync(sql, commandType, sqlParameters);

            var rowEffects = await dbCommand.ExecuteNonQueryAsync();

            await dbConnection.CloseAsync();

            var outputValues = sqlParameters
                .Where(u => u.Direction == ParameterDirection.Output)
                .Select(u => new { Name = u.ParameterName, u.Value })
                .ToDictionary(u => u.Name, u => u.Value);

            var returnValue = sqlParameters.FirstOrDefault(u => u.Direction == ParameterDirection.ReturnValue)?.Value;

            dbCommand.Parameters.Clear();
            return (outputValues, returnValue);
        }
        #endregion


        #region 准备 DbCommand 对象 + private static (DbConnection, DbCommand) PrepareDbCommand(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        /// <summary>
        /// 准备 <see cref="DbCommand"/> 对象
        /// <para>包括参数追加、性能监测包装</para>
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters">命令参数</param>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        private static (DbConnection, DbCommand) PrepareDbCommand(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dbConnection = new ProfiledDbConnection(databaseFacade.GetDbConnection(), MiniProfiler.Current);

            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }

            DbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandType = commandType;
            dbCommand.CommandText = sql;
            Utility.RectifySqlParameters(ref dbCommand, parameters);

            return (dbConnection, dbCommand);
        }
        #endregion

        #region 准备 DbCommand 对象 + private async static Task<(DbConnection, DbCommand)> PrepareDbCommandAsync(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        /// <summary>
        /// 准备 <see cref="DbCommand"/> 对象
        /// <para>包括参数追加、性能监测包装</para>
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters">命令参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        private async static Task<(DbConnection, DbCommand)> PrepareDbCommandAsync(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var dbConnection = new ProfiledDbConnection(databaseFacade.GetDbConnection(), MiniProfiler.Current);

            if (dbConnection.State == ConnectionState.Closed)
            {
                await dbConnection.OpenAsync();
            }

            DbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandType = commandType;
            dbCommand.CommandText = sql;
            Utility.RectifySqlParameters(ref dbCommand, parameters);

            return (dbConnection, dbCommand);
        }
        #endregion
    }
}