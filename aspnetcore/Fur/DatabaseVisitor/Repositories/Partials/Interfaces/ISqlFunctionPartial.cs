using Fur.DatabaseVisitor.Entities;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 泛型仓储 函数操作 分部接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial interface IRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        #region 执行标量函数 + TResult SqlScalarFunctionQuery<TResult>(string name, params object[] parameters)
        /// <summary>
        /// 执行标量函数
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>结果类型值</returns>
        TResult SqlScalarFunctionQuery<TResult>(string name, params object[] parameters);
        #endregion

        #region 执行标量函数 + Task<TResult> SqlScalarFunctionQueryAsync<TResult>(string name, params object[] parameters)
        /// <summary>
        /// 执行标量函数
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<TResult> SqlScalarFunctionQueryAsync<TResult>(string name, params object[] parameters);
        #endregion

        #region 执行标量函数 + TResult SqlScalarFunctionQuery<TResult>(string name, object parameterModel)
        /// <summary>
        /// 执行标量函数
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns>结果类型值</returns>
        TResult SqlScalarFunctionQuery<TResult>(string name, object parameterModel);
        #endregion

        #region 执行标量函数 + Task<TResult> SqlScalarFunctionQueryAsync<TResult>(string name, object parameterModel)
        /// <summary>
        /// 执行标量函数
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<TResult> SqlScalarFunctionQueryAsync<TResult>(string name, object parameterModel);
        #endregion


        #region 执行表值函数 返回 DataTable + DataTable SqlTableFunctionQuery(string name, params object[] parameters)
        /// <summary>
        /// 执行表值函数 返回 DataTable
        /// </summary>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataTable"/></returns>
        DataTable SqlTableFunctionQuery(string name, params object[] parameters);
        #endregion

        #region 执行表值函数 返回 DataTable + Task<DataTable> SqlTableFunctionQueryAsync(string name, params object[] parameters)
        /// <summary>
        /// 执行表值函数 返回 DataTable
        /// </summary>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<DataTable> SqlTableFunctionQueryAsync(string name, params object[] parameters);
        #endregion

        #region 执行表值函数 返回 DataTable + DataTable SqlTableFunctionQuery(string name, object parameterModel)
        /// <summary>
        /// 执行表值函数 返回 DataTable
        /// </summary>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">函数模型</param>
        /// <returns><see cref="DataTable"/></returns>
        DataTable SqlTableFunctionQuery(string name, object parameterModel);
        #endregion

        #region 执行表值函数 返回 DataTable + Task<DataTable> SqlTableFunctionQueryAsync(string name, object parameterModel)
        /// <summary>
        /// 执行表值函数 返回 DataTable
        /// </summary>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<DataTable> SqlTableFunctionQueryAsync(string name, object parameterModel);
        #endregion


        #region 执行表值函数 + IEnumerable<T> SqlTableFunctionQuery<T>(string name, params object[] parameters)
        /// <summary>
        /// 执行表值函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<T> SqlTableFunctionQuery<T>(string name, params object[] parameters);
        #endregion

        #region 执行表值函数 + Task<IEnumerable<T>> SqlTableFunctionQueryAsync<T>(string name, params object[] parameters)
        /// <summary>
        /// 执行表值函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<IEnumerable<T>> SqlTableFunctionQueryAsync<T>(string name, params object[] parameters);
        #endregion

        #region 执行表值函数 + IEnumerable<T> SqlTableFunctionQuery<T>(string name, object parameterModel)
        /// <summary>
        /// 执行表值函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<T> SqlTableFunctionQuery<T>(string name, object parameterModel);
        #endregion

        #region 执行表值函数 + Task<IEnumerable<T>> SqlTableFunctionQueryAsync<T>(string name, object parameterModel)
        /// <summary>
        /// 执行表值函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<IEnumerable<T>> SqlTableFunctionQueryAsync<T>(string name, object parameterModel);
        #endregion
    }
}
