using Fur.Attributes;
using Fur.Linq.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Extensions
{
    /// <summary>
    /// ADO.NET 拓展类
    /// </summary>
    [NonInflated]
    internal static class SqlAdoNetExtensions
    {
        /// <summary>
        /// 执行 Sql 返回 DataTable
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataTable"/></returns>
        internal static DataTable SqlExecuteReader(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
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

        /// <summary>
        /// 执行 Sql 返回 DataTable
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<DataTable> SqlExecuteReaderAsync(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
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

        /// <summary>
        /// 执行 Sql 返回受影响函数（无查询）
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>int</returns>
        internal static int SqlExecuteNonQuery(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var (dbConnection, dbCommand) = databaseFacade.PrepareDbCommand(sql, commandType, parameters);

            var rowEffects = dbCommand.ExecuteNonQuery();

            dbConnection.Close();

            dbCommand.Parameters.Clear();
            return rowEffects;
        }

        /// <summary>
        /// 执行 Sql 返回受影响函数（无查询）
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<int> SqlExecuteNonQueryAsync(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var (dbConnection, dbCommand) = await databaseFacade.PrepareDbCommandAsync(sql, commandType, parameters);

            var rowEffects = await dbCommand.ExecuteNonQueryAsync();

            await dbConnection.CloseAsync();

            dbCommand.Parameters.Clear();
            return rowEffects;
        }

        /// <summary>
        /// 执行 Sql 返回单行单列
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>object</returns>
        internal static object SqlExecuteScalar(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var (dbConnection, dbCommand) = databaseFacade.PrepareDbCommand(sql, commandType, parameters);

            var result = dbCommand.ExecuteScalar();

            dbConnection.Close();

            dbCommand.Parameters.Clear();
            return result;
        }

        /// <summary>
        /// 执行 Sql 返回单行单列
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<object> SqlExecuteScalarAsync(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var (dbConnection, dbCommand) = await databaseFacade.PrepareDbCommandAsync(sql, commandType, parameters);

            var result = await dbCommand.ExecuteScalarAsync();

            await dbConnection.CloseAsync();

            dbCommand.Parameters.Clear();
            return result;
        }

        /// <summary>
        /// 执行 Sql 返回 DataSet
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="Microsoft.Data.SqlClient.SqlParameter"/></param>
        /// <returns><see cref="DataSet"/></returns>
        internal static DataSet SqlDataAdapterFill(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var (dbConnection, dbCommand, dbDataAdapter) = PrepareDbDataAdapter(databaseFacade, sql, commandType, parameters);
            using var dataSet = new DataSet();

            dbDataAdapter.Fill(dataSet);

            dbConnection.Close();
            dbCommand.Parameters.Clear();

            return dataSet;
        }

        /// <summary>
        /// 执行 Sql 返回 DataSet
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="sql">sql 语句</param>
        /// <param name="commandType">命令类型 <see cref="CommandType"/></param>
        /// <param name="parameters"><see cref="Microsoft.Data.SqlClient.SqlParameter"/></param>
        /// <returns><see cref="DataSet"/></returns>
        internal static async Task<DataSet> SqlDataAdapterFillAsync(this DatabaseFacade databaseFacade, string sql, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var (dbConnection, dbCommand, dbDataAdapter) = await PrepareDbDataAdapterAsync(databaseFacade, sql, commandType, parameters);
            using var dataSet = new DataSet();

            dbDataAdapter.Fill(dataSet);

            await dbConnection.CloseAsync();
            dbCommand.Parameters.Clear();

            return dataSet;
        }

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
            RectifySqlParameters(ref dbCommand, parameters);

            return (dbConnection, dbCommand);
        }

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
            RectifySqlParameters(ref dbCommand, parameters);

            return (dbConnection, dbCommand);
        }

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
            RectifySqlParameters(ref dbCommand, parameters);
            dbDataAdapter.SelectCommand = dbCommand;

            return (dbConnection, dbCommand, dbDataAdapter);
        }

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
            RectifySqlParameters(ref dbCommand, parameters);
            dbDataAdapter.SelectCommand = dbCommand;

            return await Task.FromResult((dbConnection, dbCommand, dbDataAdapter));
        }

        /// <summary>
        /// 纠正 <see cref="SqlParameter"/> 参数
        /// </summary>
        /// <param name="dbCommand"><see cref="DbCommand"/> 参数</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        private static void RectifySqlParameters(ref DbCommand dbCommand, params object[] parameters)
        {
            if (parameters.IsNullOrEmpty()) return;

            foreach (SqlParameter parameter in parameters)
            {
                if (!parameter.ParameterName.Contains("@"))
                {
                    parameter.ParameterName = $"@{parameter.ParameterName}";
                }
                dbCommand.Parameters.Add(parameter);
            }
        }
    }
}