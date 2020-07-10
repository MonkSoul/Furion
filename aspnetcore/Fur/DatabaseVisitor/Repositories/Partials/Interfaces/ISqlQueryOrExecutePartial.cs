using Fur.DatabaseVisitor.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 泛型仓储 查询或执行sql语句 分部接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        #region 执行Sql返回 IQueryable{T} + IQueryable<TEntity> FromSqlRaw(string sql, params object[] parameters)
        /// <summary>
        /// 执行Sql返回 <see cref="IQueryable{T}"/>
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="IQueryable{T}"/></returns>
        IQueryable<TEntity> FromSqlRaw(string sql, params object[] parameters);
        #endregion

        #region 执行Sql返回 IQueryable{T} + IQueryable<TEntity> FromSqlRaw(string sql, object parameterModel)
        /// <summary>
        /// 执行Sql返回 <see cref="IQueryable{T}"/>
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="IQueryable{T}"/></returns>
        IQueryable<TEntity> FromSqlRaw(string sql, object parameterModel);
        #endregion


        #region sql 查询 + DataTable SqlQuery(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataTable"/></returns>
        DataTable SqlQuery(string sql, params object[] parameters);
        #endregion

        #region sql 查询 + Task<DataTable> SqlQueryAsync(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<DataTable> SqlQueryAsync(string sql, params object[] parameters);
        #endregion

        #region sql 查询 + DataTable SqlQuery(string sql, object parameterModel)
        /// <summary>
        /// sql 查询 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="DataTable"/></returns>
        DataTable SqlQuery(string sql, object parameterModel);
        #endregion

        #region Sql 查询 + Task<DataTable> SqlQueryAsync(string sql, object parameterModel)
        /// <summary>
        /// Sql 查询
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<DataTable> SqlQueryAsync(string sql, object parameterModel);
        #endregion


        #region sql 查询 + IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters);
        #endregion

        #region sql 查询 + Task<IEnumerable<T>> SqlQueryAsync<T>(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<IEnumerable<T>> SqlQueryAsync<T>(string sql, params object[] parameters);
        #endregion

        #region sql 查询 + IEnumerable<T> SqlQuery<T>(string sql, object parameterModel)
        /// <summary>
        /// sql 查询
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="sql">sql 查询</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<T> SqlQuery<T>(string sql, object parameterModel);
        #endregion

        #region sql 查询 + Task<IEnumerable<T>> SqlQueryAsync<T>(string sql, object parameterModel)
        /// <summary>
        /// sql 查询
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<IEnumerable<T>> SqlQueryAsync<T>(string sql, object parameterModel);
        #endregion


        #region sql 查询 + object SqlQuery(string sql, Type returnType, params object[] parameters)
        /// <summary>
        /// sql 查询
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="returnType">结果集类型</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>object</returns>
        object SqlQuery(string sql, Type returnType, params object[] parameters);
        #endregion

        #region sql 查询 + Task<object> SqlQueryAsync(string sql, Type returnType, params object[] parameters)
        /// <summary>
        /// sql 查询
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="returnType">结果集类型</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<object> SqlQueryAsync(string sql, Type returnType, params object[] parameters);
        #endregion

        // 数据集
        DataSet FromSqlDataSetQuery(string sql, params object[] parameters);

        Task<DataSet> FromSqlDataSetQueryAsync(string sql, params object[] parameters);

        DataSet FromSqlDataSetQuery(string sql, object parameterModel);

        Task<DataSet> FromSqlDataSetQueryAsync(string sql, object parameterModel);

        IEnumerable<T1> FromSqlDataSetQuery<T1>(string sql, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2) FromSqlDataSetQuery<T1, T2>(string sql, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) FromSqlDataSetQuery<T1, T2, T3>(string sql, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) FromSqlDataSetQuery<T1, T2, T3, T4>(string sql, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) FromSqlDataSetQuery<T1, T2, T3, T4, T5>(string sql, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6>(string sql, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(string sql, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, params object[] parameters);

        object FromSqlDataSetQuery(string sql, Type[] types, params object[] parameters);

        Task<IEnumerable<T1>> FromSqlDataSetQueryAsync<T1>(string sql, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> FromSqlDataSetQueryAsync<T1, T2>(string sql, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> FromSqlDataSetQueryAsync<T1, T2, T3>(string sql, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> FromSqlDataSetQueryAsync<T1, T2, T3, T4>(string sql, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5>(string sql, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(string sql, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(string sql, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, params object[] parameters);

        Task<object> FromSqlDataSetQueryAsync(string sql, Type[] types, params object[] parameters);

        IEnumerable<T1> FromSqlDataSetQuery<T1>(string sql, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2) FromSqlDataSetQuery<T1, T2>(string sql, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) FromSqlDataSetQuery<T1, T2, T3>(string sql, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) FromSqlDataSetQuery<T1, T2, T3, T4>(string sql, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) FromSqlDataSetQuery<T1, T2, T3, T4, T5>(string sql, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6>(string sql, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(string sql, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, object parameterModel);

        object FromSqlDataSetQuery(string sql, Type[] types, object parameterModel);

        Task<IEnumerable<T1>> FromSqlDataSetQueryAsync<T1>(string sql, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> FromSqlDataSetQueryAsync<T1, T2>(string sql, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> FromSqlDataSetQueryAsync<T1, T2, T3>(string sql, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> FromSqlDataSetQueryAsync<T1, T2, T3, T4>(string sql, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5>(string sql, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(string sql, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(string sql, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, object parameterModel);

        Task<object> FromSqlDataSetQueryAsync(string sql, Type[] types, object parameterModel);
    }
}
