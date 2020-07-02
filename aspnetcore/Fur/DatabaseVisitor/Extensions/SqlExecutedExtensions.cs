using Fur.Extensions;
using Fur.Linq.Extensions;
using Mapster;
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
using System.Reflection;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Extensions
{
    /// <summary>
    /// 原始Sql执行拓展
    /// </summary>
    public static class SqlExecutedExtensions
    {
        #region 类转SqlParameter参数 +/* public static SqlParameter[] ToSqlParameters<TParameterModel>(this TParameterModel parameterModel) where TParameterModel : class
        /// <summary>
        /// 类转SqlParameter参数
        /// </summary>
        /// <typeparam name="TParameterModel">类泛型类型</typeparam>
        /// <param name="parameterModel">类泛型类型值</param>
        /// <returns>SqlParameter[]</returns>
        public static SqlParameter[] ToSqlParameters<TParameterModel>(this TParameterModel parameterModel) where TParameterModel : class
        {
            var type = parameterModel.GetType();
            var properities = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var paramValues = new List<SqlParameter>();

            for (int i = 0; i < properities.Length; i++)
            {
                var property = properities[i];
                var value = property.GetValue(parameterModel);
                if (value == null) continue;

                paramValues.Add(new SqlParameter(property.Name, value));
            }
            return paramValues.ToArray();
        }
        #endregion

        public static DataTable DbSqlQuery(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
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

        public static async Task<DataTable> DbSqlQueryAsync(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var (dbConnection, dbCommand) = await databaseFacade.WrapperDbConnectionAndCommandAsync(sql, parameters);
            var dbDataReader = await dbCommand.ExecuteReaderAsync();
            var dataTable = new DataTable();
            dataTable.Load(dbDataReader);
            await dbDataReader.CloseAsync();
            await dbConnection.CloseAsync();
            dbCommand.Parameters.Clear();
            return await Task.FromResult(dataTable);
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
            FixedAndCombineSqlParameters(ref dbCommand, parameters);

            return (dbConnection, dbCommand);
        }
        #endregion

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
            FixedAndCombineSqlParameters(ref dbCommand, parameters);

            return await Task.FromResult((dbConnection, dbCommand));
        }
        #endregion

        #region 修复和合并数据库命令参数 -/* private static void FixedAndCombineSqlParameters(ref DbCommand dbCommand, params object[] parameters)
        /// <summary>
        /// 修复和合并数据库命令参数
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <param name="parameters"></param>
        private static void FixedAndCombineSqlParameters(ref DbCommand dbCommand, params object[] parameters)
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
        #endregion

        public static IEnumerable<T> ToEnumerable<T>(this DataTable dataTable)
        {
            var list = new List<T>();
            var dataTables = dataTable.AsEnumerable().ToList();
            var returnType = typeof(T);

            if (returnType.IsValueType || typeof(string).Equals(returnType))
            {
                dataTables.ForEach(row => list.Add(row[0].Adapt<T>()));
            }
            else
            {
                var propertyInfos = returnType.GetProperties().ToList();
                dataTables.ForEach(row =>
                {
                    var obj = Activator.CreateInstance<T>();
                    propertyInfos.ForEach(p =>
                    {
                        var columnName = p.Name;
                        //if (p.IsDefined(typeof(ColumnAttribute))) columnName = p.GetCustomAttribute<ColumnAttribute>().Name;

                        if (dataTable.Columns.IndexOf(columnName) != -1 && row[columnName] != DBNull.Value)
                        {
                            p.SetPropertyValue(obj, row[columnName]);
                        }
                    });
                    list.Add(obj);
                });
            }
            return list;
        }

        public static object ToEnumerable(this DataTable dataTable, object obj)
        {
            var type = obj as Type;
            var returnType = type.IsGenericType ? type.GenericTypeArguments.FirstOrDefault() : type;

            var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(returnType));
            var dataTables = dataTable.AsEnumerable().ToList();

            if (returnType.IsValueType || typeof(string).Equals(returnType))
            {
                dataTables.ForEach(row =>
                {
                    list.GetType().GetMethod("Add").Invoke(list, new object[] { row[0].Adapt(row[0].GetType(), returnType) });
                });
            }
            else
            {
                var propertyInfos = returnType.GetProperties().ToList();
                dataTables.ForEach(row =>
                {
                    var obj = Activator.CreateInstance(returnType);
                    propertyInfos.ForEach(p =>
                    {
                        var columnName = p.Name;
                        //if (p.IsDefined(typeof(ColumnAttribute))) columnName = p.GetCustomAttribute<ColumnAttribute>().Name;

                        if (dataTable.Columns.IndexOf(columnName) != -1 && row[columnName] != DBNull.Value)
                        {
                            p.SetPropertyValue(obj, row[columnName]);
                        }
                    });
                    list.GetType().GetMethod("Add").Invoke(list, new object[] { obj });
                });
            }
            var results = list as IEnumerable<object>;
            return type.IsGenericType ? results : results.FirstOrDefault();
        }

        public static Task<IEnumerable<T>> ToEnumerableAsync<T>(this DataTable dataTable)
        {
            return Task.FromResult(dataTable.ToEnumerable<T>());
        }

        public static async Task<IEnumerable<T>> ToEnumerableAsync<T>(this Task<DataTable> dataTable)
        {
            var dataTableNoTask = await dataTable;
            return await dataTableNoTask.ToEnumerableAsync<T>();
        }

        public static Task<object> ToEnumerableAsync(this DataTable dataTable, object obj)
        {
            return Task.FromResult(dataTable.ToEnumerable(obj));
        }

        public static async Task<object> ToEnumerableAsync(this Task<DataTable> dataTable, object obj)
        {
            var dataTableNoTask = await dataTable;
            return await dataTableNoTask.ToEnumerableAsync(obj);
        }
    }
}
