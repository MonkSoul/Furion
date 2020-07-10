using Fur.DatabaseVisitor.Options;
using Mapster;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Extensions
{
    /// <summary>
    /// Sql 函数 拓展类
    /// </summary>
    public static class SqlFunctionExtensions
    {
        #region 执行标量函数 返回 TResult + public static TResult SqlScalarFunctionExecute<TResult>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行标量函数 返回 TResult
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>TResult</returns>
        public static TResult SqlScalarFunctionExecute<TResult>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = CombineSql(DbFunctionTypeOptions.Scalar, name, parameters);
            return databaseFacade.SqlExecuteScalar(sql, CommandType.Text, parameters).Adapt<TResult>();
        }
        #endregion

        #region 执行标量函数 返回 TResult + public static TResult SqlScalarFunctionExecute<TResult>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行标量函数 返回 TResult
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static async Task<TResult> SqlScalarFunctionExecuteAsync<TResult>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = CombineSql(DbFunctionTypeOptions.Scalar, name, parameters);
            var result = await databaseFacade.SqlExecuteScalarAsync(sql, CommandType.Text, parameters);
            return result.Adapt<TResult>();
        }
        #endregion

        #region 执行标量函数 返回 TResult + public static TResult SqlScalarFunctionExecute<TResult>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        /// <summary>
        /// 执行标量函数 返回 TResult
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns>TResult</returns>
        public static TResult SqlScalarFunctionExecute<TResult>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = CombineSql(DbFunctionTypeOptions.Scalar, name, parameterModel);
            return databaseFacade.SqlExecuteScalar(sql, CommandType.Text, parameters).Adapt<TResult>();
        }
        #endregion

        #region 执行标量函数 返回 TResult + public static TResult SqlScalarFunctionExecute<TResult>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行标量函数 返回 TResult
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static async Task<TResult> SqlScalarFunctionExecuteAsync<TResult>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = CombineSql(DbFunctionTypeOptions.Scalar, name, parameterModel);
            var result = await databaseFacade.SqlExecuteScalarAsync(sql, CommandType.Text, parameters);
            return result.Adapt<TResult>();
        }
        #endregion


        #region 执行表值函数 返回 DataTable + public static DataTable SqlTableFunctionExecute(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行表值函数 返回 DataTable
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataTable"/></returns>
        public static DataTable SqlTableFunctionExecute(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = CombineSql(DbFunctionTypeOptions.Table, name, parameters);
            return databaseFacade.SqlExecuteReader(sql, CommandType.Text, parameters);
        }
        #endregion

        #region 执行表值函数 返回 DataTable + public static Task<DataTable> SqlTableFunctionExecuteAsync(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行表值函数 返回 DataTable
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<DataTable> SqlTableFunctionExecuteAsync(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = CombineSql(DbFunctionTypeOptions.Table, name, parameters);
            return databaseFacade.SqlExecuteReaderAsync(sql, CommandType.Text, parameters);
        }
        #endregion


        #region 执行表值函数 + public static IEnumerable<T> SqlTableFunctionExecute<T>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行表值函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<T> SqlTableFunctionExecute<T>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = CombineSql(DbFunctionTypeOptions.Table, name, parameters);
            return databaseFacade.SqlQuery<T>(sql, CommandType.Text, parameters);
        }
        #endregion

        #region 执行表值函数 + public static Task<IEnumerable<T>> SqlTableFunctionExecuteAsync<T>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        /// <summary>
        /// 执行表值函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<IEnumerable<T>> SqlTableFunctionExecuteAsync<T>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = CombineSql(DbFunctionTypeOptions.Table, name, parameters);
            return databaseFacade.SqlQueryAsync<T>(sql, CommandType.Text, parameters);
        }
        #endregion


        #region 执行表值函数 返回 DataTable + public static DataTable SqlTableFunctionExecute(this DatabaseFacade databaseFacade, string name, object parameterModel)
        /// <summary>
        /// 执行表值函数 返回 DataTable
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="DataTable"/></returns>
        public static DataTable SqlTableFunctionExecute(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = CombineSql(DbFunctionTypeOptions.Table, name, parameterModel);
            return databaseFacade.SqlExecuteReader(sql, CommandType.Text, parameters);
        }
        #endregion

        #region 执行表值函数 返回 DataTable + public static Task<DataTable> SqlTableFunctionExecuteAsync(this DatabaseFacade databaseFacade, string name, object parameterModel)
        /// <summary>
        /// 执行表值函数 返回 DataTable
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<DataTable> SqlTableFunctionExecuteAsync(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = CombineSql(DbFunctionTypeOptions.Table, name, parameterModel);
            return databaseFacade.SqlExecuteReaderAsync(sql, CommandType.Text, parameters);
        }
        #endregion


        #region 执行表值函数 + public static IEnumerable<T> SqlTableFunctionExecute<T>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        /// <summary>
        /// 执行表值函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<T> SqlTableFunctionExecute<T>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = CombineSql(DbFunctionTypeOptions.Table, name, parameterModel);
            return databaseFacade.SqlQuery<T>(sql, CommandType.Text, parameters);
        }
        #endregion

        #region 执行表值函数 + public static Task<IEnumerable<T>> SqlTableFunctionExecuteAsync<T>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        /// <summary>
        /// 执行表值函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<IEnumerable<T>> SqlTableFunctionExecuteAsync<T>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = CombineSql(DbFunctionTypeOptions.Table, name, parameterModel);
            return databaseFacade.SqlQueryAsync<T>(sql, CommandType.Text, parameters);
        }
        #endregion


        #region 组合Sql语句 + private static (string sql, SqlParameter[] parameters) CombineSql(DbFunctionTypeOptions dbFunctionTypeOptions, string name, object parameterModel = null)
        /// <summary>
        /// 组合Sql语句
        /// </summary>
        /// <param name="dbFunctionTypeOptions">数据库函数类型。参见：<see cref="DbFunctionTypeOptions"/></param>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        private static (string sql, SqlParameter[] parameters) CombineSql(DbFunctionTypeOptions dbFunctionTypeOptions, string name, object parameterModel = null)
        {
            var type = parameterModel?.GetType();
            var properities = type?.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var paramValues = new List<SqlParameter>();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"SELECT{(dbFunctionTypeOptions == DbFunctionTypeOptions.Table ? " * FROM " : "")} {name} (");

            for (int i = 0; i < properities?.Length; i++)
            {
                var property = properities[i];

                var value = property.GetValue(parameterModel);

                stringBuilder.Append($"@{property.Name},");
                paramValues.Add(new SqlParameter(property.Name, value ?? DBNull.Value));
            }

            var sql = stringBuilder.ToString();
            if (sql.EndsWith(",")) sql = sql[0..^1];
            sql += (")");

            return (sql, paramValues.ToArray());
        }
        #endregion

        #region 组合Sql语句 + private static string CombineSql(DbFunctionTypeOptions dbFunctionTypeOptions, string name, params object[] parameters)
        /// <summary>
        /// 组合Sql语句
        /// </summary>
        /// <param name="dbFunctionTypeOptions">数据库函数类型。参见：<see cref="DbFunctionTypeOptions"/></param>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>string</returns>
        private static string CombineSql(DbFunctionTypeOptions dbFunctionTypeOptions, string name, params object[] parameters)
        {
            var sqlParameters = parameters.Any() ? (SqlParameter[])parameters : new SqlParameter[] { };

            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"SELECT{(dbFunctionTypeOptions == DbFunctionTypeOptions.Table ? " * FROM " : "")} {name} (");

            for (int i = 0; i < sqlParameters.Length; i++)
            {
                var sqlParameter = sqlParameters[i];
                stringBuilder.Append($"@{(sqlParameter.ParameterName.Replace("@", ""))},");
            }

            var sql = stringBuilder.ToString();
            if (sql.EndsWith(",")) sql = sql[0..^1];
            sql += (")");

            return sql;
        }
        #endregion
    }
}