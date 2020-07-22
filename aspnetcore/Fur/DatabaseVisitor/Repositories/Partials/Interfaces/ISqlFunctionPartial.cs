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
        #region 执行标量函数 + TResult SqlScalarFunction<TResult>(string name, params object[] parameters)

        /// <summary>
        /// 执行标量函数
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>结果类型值</returns>
        TResult SqlScalarFunction<TResult>(string name, params object[] parameters);

        #endregion 执行标量函数 + TResult SqlScalarFunction<TResult>(string name, params object[] parameters)

        #region 执行标量函数 + Task<TResult> SqlScalarFunctionAsync<TResult>(string name, params object[] parameters)

        /// <summary>
        /// 执行标量函数
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<TResult> SqlScalarFunctionAsync<TResult>(string name, params object[] parameters);

        #endregion 执行标量函数 + Task<TResult> SqlScalarFunctionAsync<TResult>(string name, params object[] parameters)

        #region 执行标量函数 + TResult SqlScalarFunction<TResult>(string name, object parameterModel)

        /// <summary>
        /// 执行标量函数
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns>结果类型值</returns>
        TResult SqlScalarFunction<TResult>(string name, object parameterModel);

        #endregion 执行标量函数 + TResult SqlScalarFunction<TResult>(string name, object parameterModel)

        #region 执行标量函数 + Task<TResult> SqlScalarFunctionAsync<TResult>(string name, object parameterModel)

        /// <summary>
        /// 执行标量函数
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<TResult> SqlScalarFunctionAsync<TResult>(string name, object parameterModel);

        #endregion 执行标量函数 + Task<TResult> SqlScalarFunctionAsync<TResult>(string name, object parameterModel)

        #region 执行表值函数 返回 DataTable + DataTable SqlTableFunction(string name, params object[] parameters)

        /// <summary>
        /// 执行表值函数 返回 DataTable
        /// </summary>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataTable"/></returns>
        DataTable SqlTableFunction(string name, params object[] parameters);

        #endregion 执行表值函数 返回 DataTable + DataTable SqlTableFunction(string name, params object[] parameters)

        #region 执行表值函数 返回 DataTable + Task<DataTable> SqlTableFunctionAsync(string name, params object[] parameters)

        /// <summary>
        /// 执行表值函数 返回 DataTable
        /// </summary>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<DataTable> SqlTableFunctionAsync(string name, params object[] parameters);

        #endregion 执行表值函数 返回 DataTable + Task<DataTable> SqlTableFunctionAsync(string name, params object[] parameters)

        #region 执行表值函数 返回 DataTable + DataTable SqlTableFunction(string name, object parameterModel)

        /// <summary>
        /// 执行表值函数 返回 DataTable
        /// </summary>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">函数模型</param>
        /// <returns><see cref="DataTable"/></returns>
        DataTable SqlTableFunction(string name, object parameterModel);

        #endregion 执行表值函数 返回 DataTable + DataTable SqlTableFunction(string name, object parameterModel)

        #region 执行表值函数 返回 DataTable + Task<DataTable> SqlTableFunctionAsync(string name, object parameterModel)

        /// <summary>
        /// 执行表值函数 返回 DataTable
        /// </summary>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<DataTable> SqlTableFunctionAsync(string name, object parameterModel);

        #endregion 执行表值函数 返回 DataTable + Task<DataTable> SqlTableFunctionAsync(string name, object parameterModel)

        #region 执行表值函数 + IEnumerable<T> SqlTableFunction<T>(string name, params object[] parameters)

        /// <summary>
        /// 执行表值函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<T> SqlTableFunction<T>(string name, params object[] parameters);

        #endregion 执行表值函数 + IEnumerable<T> SqlTableFunction<T>(string name, params object[] parameters)

        #region 执行表值函数 + Task<IEnumerable<T>> SqlTableFunctionAsync<T>(string name, params object[] parameters)

        /// <summary>
        /// 执行表值函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<IEnumerable<T>> SqlTableFunctionAsync<T>(string name, params object[] parameters);

        #endregion 执行表值函数 + Task<IEnumerable<T>> SqlTableFunctionAsync<T>(string name, params object[] parameters)

        #region 执行表值函数 + IEnumerable<T> SqlTableFunction<T>(string name, object parameterModel)

        /// <summary>
        /// 执行表值函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<T> SqlTableFunction<T>(string name, object parameterModel);

        #endregion 执行表值函数 + IEnumerable<T> SqlTableFunction<T>(string name, object parameterModel)

        #region 执行表值函数 + Task<IEnumerable<T>> SqlTableFunctionAsync<T>(string name, object parameterModel)

        /// <summary>
        /// 执行表值函数
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">函数名称</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<IEnumerable<T>> SqlTableFunctionAsync<T>(string name, object parameterModel);

        #endregion 执行表值函数 + Task<IEnumerable<T>> SqlTableFunctionAsync<T>(string name, object parameterModel)
    }
}