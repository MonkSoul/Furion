using Fur.DatabaseAccessor.Extensions.Sql;
using Fur.DatabaseAccessor.Models.Entities;
using Mapster;
using System.Data;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Repositories
{
    /// <summary>
    /// 泛型仓储 sql 执行 分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial class EFCoreRepositoryOfT<TEntity> : IRepositoryOfT<TEntity> where TEntity : class, IDbEntityBase, new()
    {
        #region Sql 执行返回受影响函数 + public virtual int SqlExecuteNonQuery(string sql, params object[] parameters)
        /// <summary>
        /// Sql 执行返回受影响函数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>int</returns>
        public virtual int SqlExecuteNonQuery(string sql, params object[] parameters)
        {
            return Database.SqlExecuteNonQuery(sql, CommandType.Text, parameters);
        }
        #endregion

        #region Sql 执行返回受影响函数 + public virtual int SqlExecuteNonQuery(string sql, object parameterModel)
        /// <summary>
        /// Sql 执行返回受影响函数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns>int</returns>
        public virtual int SqlExecuteNonQuery(string sql, object parameterModel)
        {
            return Database.SqlExecuteNonQuery(sql, CommandType.Text, parameterModel.ToSqlParameters());
        }
        #endregion

        #region Sql 执行返回受影响函数 + public virtual Task<int> SqlExecuteNonQueryAsync(string sql, params object[] parameters)
        /// <summary>
        /// Sql 执行返回受影响函数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<int> SqlExecuteNonQueryAsync(string sql, params object[] parameters)
        {
            return Database.SqlExecuteNonQueryAsync(sql, CommandType.Text, parameters);
        }
        #endregion

        #region Sql 执行返回受影响函数 + public virtual Task<int> SqlExecuteNonQueryAsync(string sql, object parameterModel)
        /// <summary>
        /// Sql 执行返回受影响函数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<int> SqlExecuteNonQueryAsync(string sql, object parameterModel)
        {
            return Database.SqlExecuteNonQueryAsync(sql, CommandType.Text, parameterModel.ToSqlParameters());
        }
        #endregion


        #region Sql 执行返回单行单列 + public virtual object SqlExecuteScalar(string sql, params object[] parameters)
        /// <summary>
        /// Sql 执行返回单行单列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>int</returns>
        public virtual object SqlExecuteScalar(string sql, params object[] parameters)
        {
            return Database.SqlExecuteScalar(sql, CommandType.Text, parameters);
        }
        #endregion

        #region Sql 执行返回单行单列 + public virtual object SqlExecuteScalar(string sql, object parameterModel)
        /// <summary>
        /// Sql 执行返回单行单列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns>int</returns>
        public virtual object SqlExecuteScalar(string sql, object parameterModel)
        {
            return Database.SqlExecuteScalar(sql, CommandType.Text, parameterModel.ToSqlParameters());
        }
        #endregion

        #region Sql 执行返回单行单列 + public virtual Task<object> SqlExecuteScalarAsync(string sql, params object[] parameters)
        /// <summary>
        /// Sql 执行返回单行单列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<object> SqlExecuteScalarAsync(string sql, params object[] parameters)
        {
            return Database.SqlExecuteScalarAsync(sql, CommandType.Text, parameters);
        }
        #endregion

        #region Sql 执行返回单行单列 + public virtual Task<object> SqlExecuteScalarAsync(string sql, object parameterModel)
        /// <summary>
        /// Sql 执行返回单行单列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<object> SqlExecuteScalarAsync(string sql, object parameterModel)
        {
            return Database.SqlExecuteScalarAsync(sql, CommandType.Text, parameterModel.ToSqlParameters());
        }
        #endregion


        #region Sql 执行返回单行单列 + public virtual TResult SqlExecuteScalar<TResult>(string sql, params object[] parameters)
        /// <summary>
        /// Sql 执行返回单行单列
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns></returns>
        public virtual TResult SqlExecuteScalar<TResult>(string sql, params object[] parameters)
        {
            return SqlExecuteScalar(sql, parameters).Adapt<TResult>();
        }
        #endregion

        #region Sql 执行返回单行单列 + public virtual TResult SqlExecuteScalar<TResult>(string sql, object parameterModel)
        /// <summary>
        /// Sql 执行返回单行单列
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns>int</returns>
        public virtual TResult SqlExecuteScalar<TResult>(string sql, object parameterModel)
        {
            return SqlExecuteScalar(sql, parameterModel).Adapt<TResult>();
        }
        #endregion

        #region Sql 执行返回单行单列 + public async Task<TResult> SqlExecuteScalarAsync<TResult>(string sql, params object[] parameters)
        /// <summary>
        /// Sql 执行返回单行单列
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public async Task<TResult> SqlExecuteScalarAsync<TResult>(string sql, params object[] parameters)
        {
            var obj = await SqlExecuteScalarAsync(sql, parameters);
            return obj.Adapt<TResult>();
        }
        #endregion

        #region Sql 执行返回单行单列 + public virtual async Task<TResult> SqlExecuteScalarAsync<TResult>(string sql, object parameterModel)
        /// <summary>
        /// Sql 执行返回单行单列
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual async Task<TResult> SqlExecuteScalarAsync<TResult>(string sql, object parameterModel)
        {
            var obj = await SqlExecuteScalarAsync(sql, parameterModel);
            return obj.Adapt<TResult>();
        }
        #endregion
    }
}