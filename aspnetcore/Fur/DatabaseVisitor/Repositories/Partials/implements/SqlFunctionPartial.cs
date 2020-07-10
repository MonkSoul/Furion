using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Extensions;
using Fur.DatabaseVisitor.Options;
using Fur.DependencyInjection.Lifetimes;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 泛型仓储 函数操作 分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial class EFCoreRepositoryOfT<TEntity> : IRepositoryOfT<TEntity>, IScopedLifetimeOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        #region 执行标量函数 + public virtual TResult SqlScalarFunctionQuery<TResult>(string name, params object[] parameters)
        /// <summary>
        /// 执行标量函数
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>结果类型值</returns>
        public virtual TResult SqlScalarFunctionQuery<TResult>(string name, params object[] parameters)
        {
            return Database.SqlFunctionExecute<TResult>(DbFunctionTypeOptions.Scalar, name, parameters).FirstOrDefault();
        }
        #endregion

        #region 执行标量函数 + public virtual async Task<TResult> SqlScalarFunctionQueryAsync<TResult>(string name, params object[] parameters)
        /// <summary>
        /// 执行标量函数
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<TResult> SqlScalarFunctionQueryAsync<TResult>(string name, params object[] parameters)
        {
            var results = await Database.SqlFunctionExecuteAsync<TResult>(DbFunctionTypeOptions.Scalar, name, parameters);
            return results.FirstOrDefault();
        }
        #endregion

        #region 执行标量函数 + public virtual TResult SqlScalarFunctionQuery<TResult>(string name, object parameterModel)
        /// <summary>
        /// 执行标量函数
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns>结果类型值</returns>
        public virtual TResult SqlScalarFunctionQuery<TResult>(string name, object parameterModel)
        {
            return Database.SqlFunctionExecute<TResult>(DbFunctionTypeOptions.Scalar, name, parameterModel).FirstOrDefault();
        }
        #endregion

        #region 执行标量函数 + public virtual async Task<TResult> SqlScalarFunctionQueryAsync<TResult>(string name, object parameterModel)
        /// <summary>
        /// 执行标量函数
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<TResult> SqlScalarFunctionQueryAsync<TResult>(string name, object parameterModel)
        {
            var results = await Database.SqlFunctionExecuteAsync<TResult>(DbFunctionTypeOptions.Scalar, name, parameterModel);
            return results.FirstOrDefault();
        }
        #endregion


        #region 执行表值函数 返回 DataTable + public virtual DataTable SqlTableFunctionQuery(string name, params object[] parameters)
        /// <summary>
        /// 执行表值函数 返回 DataTable
        /// </summary>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataTable"/></returns>
        public virtual DataTable SqlTableFunctionQuery(string name, params object[] parameters)
        {
            return Database.SqlFunctionExecute(DbFunctionTypeOptions.Table, name, parameters);
        }
        #endregion

        #region 执行表值函数 返回 DataTable + public virtual Task<DataTable> SqlTableFunctionQueryAsync(string name, params object[] parameters)
        /// <summary>
        /// 执行表值函数 返回 DataTable
        /// </summary>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<DataTable> SqlTableFunctionQueryAsync(string name, params object[] parameters)
        {
            return Database.SqlFunctionExecuteAsync(DbFunctionTypeOptions.Table, name, parameters);
        }
        #endregion

        #region 执行表值函数 返回 DataTable + public virtual DataTable SqlTableFunctionQuery(string name, object parameterModel)
        /// <summary>
        /// 执行表值函数 返回 DataTable
        /// </summary>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">函数模型</param>
        /// <returns><see cref="DataTable"/></returns>
        public virtual DataTable SqlTableFunctionQuery(string name, object parameterModel)
        {
            return Database.SqlFunctionExecute(DbFunctionTypeOptions.Table, name, parameterModel);
        }
        #endregion

        #region 执行表值函数 返回 DataTable + public virtual Task<DataTable> SqlTableFunctionQueryAsync(string name, object parameterModel)
        /// <summary>
        /// 执行表值函数 返回 DataTable
        /// </summary>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<DataTable> SqlTableFunctionQueryAsync(string name, object parameterModel)
        {
            return Database.SqlFunctionExecuteAsync(DbFunctionTypeOptions.Table, name, parameterModel);
        }
        #endregion


        #region 执行表值函数 + public virtual IEnumerable<T> SqlTableFunctionQuery<T>(string name, params object[] parameters)
        /// <summary>
        /// 执行表值函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public virtual IEnumerable<T> SqlTableFunctionQuery<T>(string name, params object[] parameters)
        {
            return Database.SqlFunctionExecute<T>(DbFunctionTypeOptions.Table, name, parameters);
        }
        #endregion

        #region 执行表值函数 + public virtual Task<IEnumerable<T>> SqlTableFunctionQueryAsync<T>(string name, params object[] parameters)
        /// <summary>
        /// 执行表值函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<IEnumerable<T>> SqlTableFunctionQueryAsync<T>(string name, params object[] parameters)
        {
            return Database.SqlFunctionExecuteAsync<T>(DbFunctionTypeOptions.Table, name, parameters);
        }
        #endregion

        #region 执行表值函数 + public virtual IEnumerable<T> SqlTableFunctionQuery<T>(string name, object parameterModel)
        /// <summary>
        /// 执行表值函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public virtual IEnumerable<T> SqlTableFunctionQuery<T>(string name, object parameterModel)
        {
            return Database.SqlFunctionExecute<T>(DbFunctionTypeOptions.Table, name, parameterModel);
        }
        #endregion

        #region 执行表值函数 + public virtual Task<IEnumerable<T>> SqlTableFunctionQueryAsync<T>(string name, object parameterModel)
        /// <summary>
        /// 执行表值函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<IEnumerable<T>> SqlTableFunctionQueryAsync<T>(string name, object parameterModel)
        {
            return Database.SqlFunctionExecuteAsync<T>(DbFunctionTypeOptions.Table, name, parameterModel);
        }
        #endregion
    }
}
