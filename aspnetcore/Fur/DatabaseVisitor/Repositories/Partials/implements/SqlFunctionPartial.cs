using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Extensions;
using Fur.DependencyInjection.Lifetimes;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 泛型仓储 函数操作 分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial class EFCoreRepositoryOfT<TEntity> : IRepositoryOfT<TEntity>, IScopedLifetimeOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        #region 执行标量函数 + public virtual TResult SqlScalarFunction<TResult>(string name, params object[] parameters)
        /// <summary>
        /// 执行标量函数
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>结果类型值</returns>
        public virtual TResult SqlScalarFunction<TResult>(string name, params object[] parameters)
        {
            return Database.SqlScalarFunction<TResult>(name, parameters);
        }
        #endregion

        #region 执行标量函数 + public virtual Task<TResult> SqlScalarFunctionAsync<TResult>(string name, params object[] parameters)
        /// <summary>
        /// 执行标量函数
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<TResult> SqlScalarFunctionAsync<TResult>(string name, params object[] parameters)
        {
            return Database.SqlScalarFunctionAsync<TResult>(name, parameters);
        }
        #endregion

        #region 执行标量函数 + public virtual TResult SqlScalarFunction<TResult>(string name, object parameterModel)
        /// <summary>
        /// 执行标量函数
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns>结果类型值</returns>
        public virtual TResult SqlScalarFunction<TResult>(string name, object parameterModel)
        {
            return Database.SqlScalarFunction<TResult>(name, parameterModel);
        }
        #endregion

        #region 执行标量函数 + public virtual Task<TResult> SqlScalarFunctionAsync<TResult>(string name, object parameterModel)
        /// <summary>
        /// 执行标量函数
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<TResult> SqlScalarFunctionAsync<TResult>(string name, object parameterModel)
        {
            return Database.SqlScalarFunctionAsync<TResult>(name, parameterModel);
        }
        #endregion


        #region 执行表值函数 返回 DataTable + public virtual DataTable SqlTableFunction(string name, params object[] parameters)
        /// <summary>
        /// 执行表值函数 返回 DataTable
        /// </summary>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataTable"/></returns>
        public virtual DataTable SqlTableFunction(string name, params object[] parameters)
        {
            return Database.SqlTableFunction(name, parameters);
        }
        #endregion

        #region 执行表值函数 返回 DataTable + public virtual Task<DataTable> SqlTableFunctionAsync(string name, params object[] parameters)
        /// <summary>
        /// 执行表值函数 返回 DataTable
        /// </summary>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<DataTable> SqlTableFunctionAsync(string name, params object[] parameters)
        {
            return Database.SqlTableFunctionAsync(name, parameters);
        }
        #endregion

        #region 执行表值函数 返回 DataTable + public virtual DataTable SqlTableFunction(string name, object parameterModel)
        /// <summary>
        /// 执行表值函数 返回 DataTable
        /// </summary>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">函数模型</param>
        /// <returns><see cref="DataTable"/></returns>
        public virtual DataTable SqlTableFunction(string name, object parameterModel)
        {
            return Database.SqlTableFunction(name, parameterModel);
        }
        #endregion

        #region 执行表值函数 返回 DataTable + public virtual Task<DataTable> SqlTableFunctionAsync(string name, object parameterModel)
        /// <summary>
        /// 执行表值函数 返回 DataTable
        /// </summary>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<DataTable> SqlTableFunctionAsync(string name, object parameterModel)
        {
            return Database.SqlTableFunctionAsync(name, parameterModel);
        }
        #endregion


        #region 执行表值函数 + public virtual IEnumerable<T> SqlTableFunction<T>(string name, params object[] parameters)
        /// <summary>
        /// 执行表值函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public virtual IEnumerable<T> SqlTableFunction<T>(string name, params object[] parameters)
        {
            return Database.SqlTableFunction<T>(name, parameters);
        }
        #endregion

        #region 执行表值函数 + public virtual Task<IEnumerable<T>> SqlTableFunctionAsync<T>(string name, params object[] parameters)
        /// <summary>
        /// 执行表值函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<IEnumerable<T>> SqlTableFunctionAsync<T>(string name, params object[] parameters)
        {
            return Database.SqlTableFunctionAsync<T>(name, parameters);
        }
        #endregion

        #region 执行表值函数 + public virtual IEnumerable<T> SqlTableFunction<T>(string name, object parameterModel)
        /// <summary>
        /// 执行表值函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public virtual IEnumerable<T> SqlTableFunction<T>(string name, object parameterModel)
        {
            return Database.SqlTableFunction<T>(name, parameterModel);
        }
        #endregion

        #region 执行表值函数 + public virtual Task<IEnumerable<T>> SqlTableFunctionAsync<T>(string name, object parameterModel)
        /// <summary>
        /// 执行表值函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<IEnumerable<T>> SqlTableFunctionAsync<T>(string name, object parameterModel)
        {
            return Database.SqlTableFunctionAsync<T>(name, parameterModel);
        }
        #endregion
    }
}
