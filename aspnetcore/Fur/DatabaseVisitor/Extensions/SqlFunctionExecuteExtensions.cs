using Fur.DatabaseVisitor.Options;
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
    /// 函数执行拓展类
    /// </summary>
    public static class SqlFunctionExecuteExtensions
    {
        #region 执行函数 + public static DataTable SqlFunctionExecute(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbFunctionTypeOptions, string name, params object[] parameters)
        /// <summary>
        /// 执行函数
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="dbFunctionTypeOptions">数据库编译类型选项</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataTable"/></returns>
        public static DataTable SqlFunctionExecute(this DatabaseFacade databaseFacade, DbFunctionTypeOptions dbFunctionTypeOptions, string name, params object[] parameters)
        {
            var sql = CombineSql(dbFunctionTypeOptions, name, parameters);
            return databaseFacade.SqlExecute(sql, CommandType.Text, parameters);
        }
        #endregion

        #region 执行函数 + public static IEnumerable<T> SqlFunctionExecute<T>(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbFunctionTypeOptions, string name, params object[] parameters)
        /// <summary>
        /// 执行函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="dbFunctionTypeOptions">数据库编译类型选项</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<T> SqlFunctionExecute<T>(this DatabaseFacade databaseFacade, DbFunctionTypeOptions dbFunctionTypeOptions, string name, params object[] parameters)
        {
            var sql = CombineSql(dbFunctionTypeOptions, name, parameters);
            return databaseFacade.SqlExecute<T>(sql, CommandType.Text, parameters);
        }
        #endregion

        #region 执行函数 + public static Task<DataTable> SqlFunctionExecuteAsync(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbFunctionTypeOptions, string name, params object[] parameters)
        /// <summary>
        /// 执行函数
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="dbFunctionTypeOptions">数据库编译类型选项</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<DataTable> SqlFunctionExecuteAsync(this DatabaseFacade databaseFacade, DbFunctionTypeOptions dbFunctionTypeOptions, string name, params object[] parameters)
        {
            var sql = CombineSql(dbFunctionTypeOptions, name, parameters);
            return databaseFacade.SqlExecuteAsync(sql, CommandType.Text, parameters);
        }
        #endregion

        #region 执行函数 + public static Task<IEnumerable<T>> SqlFunctionExecuteAsync<T>(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbFunctionTypeOptions, string name, params object[] parameters)
        /// <summary>
        /// 执行函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="dbFunctionTypeOptions">数据库编译类型选项</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<IEnumerable<T>> SqlFunctionExecuteAsync<T>(this DatabaseFacade databaseFacade, DbFunctionTypeOptions dbFunctionTypeOptions, string name, params object[] parameters)
        {
            var sql = CombineSql(dbFunctionTypeOptions, name, parameters);
            return databaseFacade.SqlExecuteAsync<T>(sql, CommandType.Text, parameters);
        }
        #endregion

        #region 执行函数 + public static DataTable SqlFunctionExecute(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbFunctionTypeOptions, string name, object parameterModel)
        /// <summary>
        /// 执行函数
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="dbFunctionTypeOptions">数据库编译类型选项</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="DataTable"/></returns>
        public static DataTable SqlFunctionExecute(this DatabaseFacade databaseFacade, DbFunctionTypeOptions dbFunctionTypeOptions, string name, object parameterModel)
        {
            var (sql, parameters) = CombineSql(dbFunctionTypeOptions, name, parameterModel);
            return databaseFacade.SqlExecute(sql, CommandType.Text, parameters);
        }
        #endregion

        #region 执行函数 + public static IEnumerable<T> SqlFunctionExecute<T>(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbFunctionTypeOptions, string name, object parameterModel)
        /// <summary>
        /// 执行函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="dbFunctionTypeOptions">数据库编译类型选项</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<T> SqlFunctionExecute<T>(this DatabaseFacade databaseFacade, DbFunctionTypeOptions dbFunctionTypeOptions, string name, object parameterModel)
        {
            var (sql, parameters) = CombineSql(dbFunctionTypeOptions, name, parameterModel);
            return databaseFacade.SqlExecute<T>(sql, CommandType.Text, parameters);
        }
        #endregion

        #region 执行函数 + public static Task<DataTable> SqlFunctionExecuteAsync(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbFunctionTypeOptions, string name, object parameterModel)
        /// <summary>
        /// 执行函数
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="dbFunctionTypeOptions">数据库编译类型选项</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<DataTable> SqlFunctionExecuteAsync(this DatabaseFacade databaseFacade, DbFunctionTypeOptions dbFunctionTypeOptions, string name, object parameterModel)
        {
            var (sql, parameters) = CombineSql(dbFunctionTypeOptions, name, parameterModel);
            return databaseFacade.SqlExecuteAsync(sql, CommandType.Text, parameters);
        }
        #endregion

        #region 执行函数 + public static Task<IEnumerable<T>> SqlFunctionExecuteAsync<T>(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbFunctionTypeOptions, string name, object parameterModel)
        /// <summary>
        /// 执行函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="dbFunctionTypeOptions">数据库编译类型选项</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<IEnumerable<T>> SqlFunctionExecuteAsync<T>(this DatabaseFacade databaseFacade, DbFunctionTypeOptions dbFunctionTypeOptions, string name, object parameterModel)
        {
            var (sql, parameters) = CombineSql(dbFunctionTypeOptions, name, parameterModel);
            return databaseFacade.SqlExecuteAsync<T>(sql, CommandType.Text, parameters);
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
            stringBuilder.Append($"SELECT{(dbFunctionTypeOptions == DbFunctionTypeOptions.DbTableFunction ? " * FROM " : "")} {name} (");

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
            stringBuilder.Append($"SELECT{(dbFunctionTypeOptions == DbFunctionTypeOptions.DbTableFunction ? " * FROM " : "")} {name} (");

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