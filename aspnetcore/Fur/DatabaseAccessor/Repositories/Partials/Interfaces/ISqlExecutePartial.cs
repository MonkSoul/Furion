using Fur.DatabaseAccessor.Models.Entities;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Repositories
{
    /// <summary>
    /// 泛型仓储 sql 执行 分部接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IRepositoryOfT<TEntity> where TEntity : class, IDbEntityBase, new()
    {
        #region Sql 执行返回受影响函数 + int SqlExecuteNonQuery(string sql, params object[] parameters)
        /// <summary>
        /// Sql 执行返回受影响函数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>int</returns>
        int SqlExecuteNonQuery(string sql, params object[] parameters);
        #endregion

        #region Sql 执行返回受影响函数 + int SqlExecuteNonQuery(string sql, object parameterModel)
        /// <summary>
        /// Sql 执行返回受影响函数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns>int</returns>
        int SqlExecuteNonQuery(string sql, object parameterModel);
        #endregion

        #region Sql 执行返回受影响函数 + Task<int> SqlExecuteNonQueryAsync(string sql, params object[] parameters)
        /// <summary>
        /// Sql 执行返回受影响函数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<int> SqlExecuteNonQueryAsync(string sql, params object[] parameters);
        #endregion

        #region Sql 执行返回受影响函数 + Task<int> SqlExecuteNonQueryAsync(string sql, object parameterModel)
        /// <summary>
        /// Sql 执行返回受影响函数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<int> SqlExecuteNonQueryAsync(string sql, object parameterModel);
        #endregion


        #region Sql 执行返回单行单列 + object SqlExecuteScalar(string sql, params object[] parameters)
        /// <summary>
        /// Sql 执行返回单行单列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>int</returns>
        object SqlExecuteScalar(string sql, params object[] parameters);
        #endregion

        #region Sql 执行返回单行单列 + object SqlExecuteScalar(string sql, object parameterModel)
        /// <summary>
        /// Sql 执行返回单行单列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns>int</returns>
        object SqlExecuteScalar(string sql, object parameterModel);
        #endregion

        #region Sql 执行返回单行单列 + Task<object> SqlExecuteScalarAsync(string sql, params object[] parameters)
        /// <summary>
        /// Sql 执行返回单行单列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<object> SqlExecuteScalarAsync(string sql, params object[] parameters);
        #endregion

        #region Sql 执行返回单行单列 + Task<object> SqlExecuteScalarAsync(string sql, object parameterModel)
        /// <summary>
        /// Sql 执行返回单行单列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<object> SqlExecuteScalarAsync(string sql, object parameterModel);
        #endregion


        #region Sql 执行返回单行单列 + TResult SqlExecuteScalar<TResult>(string sql, params object[] parameters)
        /// <summary>
        /// Sql 执行返回单行单列
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns></returns>
        TResult SqlExecuteScalar<TResult>(string sql, params object[] parameters);
        #endregion

        #region Sql 执行返回单行单列 + TResult SqlExecuteScalar<TResult>(string sql, object parameterModel)
        /// <summary>
        /// Sql 执行返回单行单列
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns>int</returns>
        TResult SqlExecuteScalar<TResult>(string sql, object parameterModel);
        #endregion

        #region Sql 执行返回单行单列 + Task<TResult> SqlExecuteScalarAsync<TResult>(string sql, params object[] parameters)
        /// <summary>
        /// Sql 执行返回单行单列
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<TResult> SqlExecuteScalarAsync<TResult>(string sql, params object[] parameters);
        #endregion

        #region Sql 执行返回单行单列 + Task<TResult> SqlExecuteScalarAsync<TResult>(string sql, object parameterModel)
        /// <summary>
        /// Sql 执行返回单行单列
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<TResult> SqlExecuteScalarAsync<TResult>(string sql, object parameterModel);
        #endregion
    }
}