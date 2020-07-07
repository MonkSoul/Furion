using Fur.DatabaseVisitor.Helpers;
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
    /// 原始Sql执行拓展
    /// </summary>
    public static class SqlQueryExtensions
    {
        public static DataTable SqlQuery(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var (dbConnection, dbCommand) = databaseFacade.WrapperDbConnectionAndCommand(sql, parameters);
            var dbDataReader = dbCommand.ExecuteReader();
            var dataTable = new DataTable();
            dataTable.Load(dbDataReader);
            dbDataReader.Close();
            dbConnection.Close();
            dbCommand.Parameters.Clear();
            return dataTable;
        }

        public static IEnumerable<T> SqlQuery<T>(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            return SqlQuery(databaseFacade, sql, parameters).ToEnumerable<T>();
        }

        public static object SqlQuery(this DatabaseFacade databaseFacade, string sql, object obj, params object[] parameters)
        {
            var type = obj as Type;
            if (type == typeof(DataTable))
            {
                return SqlQuery(databaseFacade, sql, parameters);
            }
            return SqlQuery(databaseFacade, sql, parameters).ToEnumerable(obj);
        }

        public static async Task<DataTable> SqlQueryAsync(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var (dbConnection, dbCommand) = await databaseFacade.WrapperDbConnectionAndCommandAsync(sql, parameters);
            var dbDataReader = await dbCommand.ExecuteReaderAsync();
            var dataTable = new DataTable();
            dataTable.Load(dbDataReader);
            await dbDataReader.CloseAsync();
            await dbConnection.CloseAsync();
            dbCommand.Parameters.Clear();
            return dataTable;
        }

        public static Task<IEnumerable<T>> SqlQueryAsync<T>(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            return SqlQueryAsync(databaseFacade, sql, parameters).ToEnumerableAsync<T>();
        }

        public static Task<object> SqlQueryAsync(this DatabaseFacade databaseFacade, string sql, object obj, params object[] parameters)
        {
            var type = obj as Type;
            if (type == typeof(DataTable))
            {
                var datatable = SqlQueryAsync(databaseFacade, sql, parameters);
                return Task.FromResult<object>(datatable.Result);
            }
            return SqlQueryAsync(databaseFacade, sql, parameters).ToEnumerableAsync(obj);
        }

        public static (Dictionary<string, object> outputValues, object returnValue) SqlNonQuery(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var sqlParameters = parameters.Any() ? (SqlParameter[])parameters : new SqlParameter[] { };
            var (dbConnection, dbCommand) = databaseFacade.WrapperDbConnectionAndCommand(sql, sqlParameters);
            var rowEffects = dbCommand.ExecuteNonQuery();
            dbConnection.Close();

            // 读取 OUTPUT 参数
            var outputValues = sqlParameters
                .Where(u => u.Direction == ParameterDirection.Output)
                .Select(u => new { Name = u.ParameterName, u.Value })
                .ToDictionary(u => u.Name, u => u.Value);

            // 读取返回值
            var returnValue = sqlParameters.FirstOrDefault(u => u.Direction == ParameterDirection.ReturnValue)?.Value;

            dbCommand.Parameters.Clear();
            return (outputValues, returnValue);
        }

        public static async Task<(Dictionary<string, object> outputValues, object returnValue)> SqlNonQueryAsync(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var sqlParameters = parameters.Any() ? (SqlParameter[])parameters : new SqlParameter[] { };
            var (dbConnection, dbCommand) = await databaseFacade.WrapperDbConnectionAndCommandAsync(sql, sqlParameters);
            var rowEffects = await dbCommand.ExecuteNonQueryAsync();
            await dbConnection.CloseAsync();

            // 读取 OUTPUT 参数
            var outputValues = sqlParameters
                .Where(u => u.Direction == ParameterDirection.Output)
                .Select(u => new { Name = u.ParameterName, u.Value })
                .ToDictionary(u => u.Name, u => u.Value);

            // 读取返回值
            var returnValue = sqlParameters.FirstOrDefault(u => u.Direction == ParameterDirection.ReturnValue)?.Value;

            dbCommand.Parameters.Clear();
            return (outputValues, returnValue);
        }


        #region 包装数据库链接和执行命令对象 -/* private static (DbConnection, DbCommand) WrapperDbConnectionAndCommand(this DatabaseFacade databaseFacade, string sql, params object[] parameters)

        /// <summary>
        /// 包装数据库链接和执行命令对象
        /// </summary>
        /// <param name="databaseFacade"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static (DbConnection, DbCommand) WrapperDbConnectionAndCommand(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var dbConnection = new ProfiledDbConnection(databaseFacade.GetDbConnection(), MiniProfiler.Current);
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }

            DbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = sql;
            Helper.FixedAndCombineSqlParameters(ref dbCommand, parameters);

            return (dbConnection, dbCommand);
        }

        #endregion 包装数据库链接和执行命令对象 -/* private static (DbConnection, DbCommand) WrapperDbConnectionAndCommand(this DatabaseFacade databaseFacade, string sql, params object[] parameters)

        #region 包装数据库链接和执行命令对象 -/* private async static Task<(DbConnection, DbCommand)> WrapperDbConnectionAndCommandAsync(this DatabaseFacade databaseFacade, string sql, params object[] parameters)

        /// <summary>
        /// 包装数据库链接和执行命令对象
        /// </summary>
        /// <param name="databaseFacade"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private async static Task<(DbConnection, DbCommand)> WrapperDbConnectionAndCommandAsync(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var dbConnection = new ProfiledDbConnection(databaseFacade.GetDbConnection(), MiniProfiler.Current);
            if (dbConnection.State == ConnectionState.Closed)
            {
                await dbConnection.OpenAsync();
            }

            DbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = sql;
            Helper.FixedAndCombineSqlParameters(ref dbCommand, parameters);

            return (dbConnection, dbCommand);
        }

        #endregion 包装数据库链接和执行命令对象 -/* private async static Task<(DbConnection, DbCommand)> WrapperDbConnectionAndCommandAsync(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
    }
}