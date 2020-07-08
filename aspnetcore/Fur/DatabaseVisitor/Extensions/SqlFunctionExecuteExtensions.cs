using Fur.DatabaseVisitor.Helpers;
using Fur.DatabaseVisitor.Options;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Extensions
{
    /// <summary>
    /// 函数执行拓展类
    /// </summary>
    public static class SqlFunctionExecuteExtensions
    {
        #region 执行函数 + public static DataTable SqlFunctionExecute(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCompileTypeOptions, string name, params object[] parameters)
        /// <summary>
        /// 执行函数
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="dbCompileTypeOptions">数据库编译类型选项</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataTable"/></returns>
        public static DataTable SqlFunctionExecute(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCompileTypeOptions, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(dbCompileTypeOptions, name, parameters);
            return databaseFacade.SqlExecute(sql, parameters);
        }
        #endregion

        #region 执行函数 + public static IEnumerable<T> SqlFunctionExecute<T>(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCompileTypeOptions, string name, params object[] parameters)
        /// <summary>
        /// 执行函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="dbCompileTypeOptions">数据库编译类型选项</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<T> SqlFunctionExecute<T>(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCompileTypeOptions, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(dbCompileTypeOptions, name, parameters);
            return databaseFacade.SqlExecute<T>(sql, parameters);
        }
        #endregion

        #region 执行函数 + public static Task<DataTable> SqlFunctionExecuteAsync(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCompileTypeOptions, string name, params object[] parameters)
        /// <summary>
        /// 执行函数
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="dbCompileTypeOptions">数据库编译类型选项</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<DataTable> SqlFunctionExecuteAsync(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCompileTypeOptions, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(dbCompileTypeOptions, name, parameters);
            return databaseFacade.SqlExecuteAsync(sql, parameters);
        }
        #endregion

        #region 执行函数 + public static Task<IEnumerable<T>> SqlFunctionExecuteAsync<T>(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCompileTypeOptions, string name, params object[] parameters)
        /// <summary>
        /// 执行函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="dbCompileTypeOptions">数据库编译类型选项</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<IEnumerable<T>> SqlFunctionExecuteAsync<T>(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCompileTypeOptions, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(dbCompileTypeOptions, name, parameters);
            return databaseFacade.SqlExecuteAsync<T>(sql, parameters);
        }
        #endregion

        #region 执行函数 + public static DataTable SqlFunctionExecute(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCompileTypeOptions, string name, object parameterModel)
        /// <summary>
        /// 执行函数
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="dbCompileTypeOptions">数据库编译类型选项</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="DataTable"/></returns>
        public static DataTable SqlFunctionExecute(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCompileTypeOptions, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(dbCompileTypeOptions, name, parameterModel);
            return databaseFacade.SqlExecute(sql, parameters);
        }
        #endregion

        #region 执行函数 + public static IEnumerable<T> SqlFunctionExecute<T>(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCompileTypeOptions, string name, object parameterModel)
        /// <summary>
        /// 执行函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="dbCompileTypeOptions">数据库编译类型选项</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<T> SqlFunctionExecute<T>(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCompileTypeOptions, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(dbCompileTypeOptions, name, parameterModel);
            return databaseFacade.SqlExecute<T>(sql, parameters);
        }
        #endregion

        #region 执行函数 + public static Task<DataTable> SqlFunctionExecuteAsync(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCompileTypeOptions, string name, object parameterModel)
        /// <summary>
        /// 执行函数
        /// </summary>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="dbCompileTypeOptions">数据库编译类型选项</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<DataTable> SqlFunctionExecuteAsync(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCompileTypeOptions, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(dbCompileTypeOptions, name, parameterModel);
            return databaseFacade.SqlExecuteAsync(sql, parameters);
        }
        #endregion

        #region 执行函数 + public static Task<IEnumerable<T>> SqlFunctionExecuteAsync<T>(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCompileTypeOptions, string name, object parameterModel)
        /// <summary>
        /// 执行函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="databaseFacade">数据库操作对象</param>
        /// <param name="dbCompileTypeOptions">数据库编译类型选项</param>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public static Task<IEnumerable<T>> SqlFunctionExecuteAsync<T>(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCompileTypeOptions, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(dbCompileTypeOptions, name, parameterModel);
            return databaseFacade.SqlExecuteAsync<T>(sql, parameters);
        }
        #endregion
    }
}