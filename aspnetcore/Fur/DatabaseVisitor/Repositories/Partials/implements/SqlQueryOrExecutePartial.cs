using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Extensions;
using Fur.DependencyInjection.Lifetimes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 泛型仓储 查询或执行sql语句 分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial class EFCoreRepositoryOfT<TEntity> : IRepositoryOfT<TEntity>, IScopedLifetimeOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        #region 执行 Sql 返回 IQueryable{T} + public virtual IQueryable<TEntity> FromSqlRaw(string sql, params object[] parameters)
        /// <summary>
        /// 执行 Sql 返回 <see cref="IQueryable{T}"/>
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="IQueryable{T}"/></returns>
        public virtual IQueryable<TEntity> FromSqlRaw(string sql, params object[] parameters)
        {
            return Entity.FromSqlRaw(sql, parameters);
        }
        #endregion

        #region 执行 Sql 返回 IQueryable{T} + public virtual IQueryable<TEntity> FromSqlRaw(string sql, object parameterModel)
        /// <summary>
        /// 执行 Sql 返回 IQueryable{T}
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="IQueryable{T}"/></returns>
        public virtual IQueryable<TEntity> FromSqlRaw(string sql, object parameterModel)
        {
            return Entity.FromSqlRaw(sql, parameterModel.ToSqlParameters());
        }
        #endregion


        #region sql 查询 + public virtual DataTable SqlQuery(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataTable"/></returns>
        public virtual DataTable SqlQuery(string sql, params object[] parameters)
        {
            return Database.SqlQuery(sql, CommandType.Text, parameters);
        }
        #endregion

        #region sql 查询 + public virtual Task<DataTable> SqlQueryAsync(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<DataTable> SqlQueryAsync(string sql, params object[] parameters)
        {
            return Database.SqlQueryAsync(sql, CommandType.Text, parameters);
        }
        #endregion

        #region sql 查询 + public virtual DataTable SqlQuery(string sql, object parameterModel)
        /// <summary>
        /// sql 查询 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="DataTable"/></returns>
        public virtual DataTable SqlQuery(string sql, object parameterModel)
        {
            return SqlQuery(sql, parameterModel.ToSqlParameters());
        }
        #endregion

        #region Sql 查询 + public virtual Task<DataTable> SqlQueryAsync(string sql, object parameterModel)
        /// <summary>
        /// Sql 查询
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<DataTable> SqlQueryAsync(string sql, object parameterModel)
        {
            return SqlQueryAsync(sql, parameterModel.ToSqlParameters());
        }
        #endregion


        #region sql 查询 + public virtual IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public virtual IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters)
        {
            return Database.SqlQuery<T>(sql, CommandType.Text, parameters);
        }
        #endregion

        #region sql 查询 + public virtual Task<IEnumerable<T>> SqlQueryAsync<T>(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<IEnumerable<T>> SqlQueryAsync<T>(string sql, params object[] parameters)
        {
            return Database.SqlQueryAsync<T>(sql, CommandType.Text, parameters);
        }
        #endregion


        #region sql 查询 + public virtual IEnumerable<T> SqlQuery<T>(string sql, object parameterModel)
        /// <summary>
        /// sql 查询
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="sql">sql 查询</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public virtual IEnumerable<T> SqlQuery<T>(string sql, object parameterModel)
        {
            return SqlQuery<T>(sql, parameterModel.ToSqlParameters());
        }
        #endregion

        #region sql 查询 + public virtual Task<IEnumerable<T>> SqlQueryAsync<T>(string sql, object parameterModel)
        /// <summary>
        /// sql 查询
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<IEnumerable<T>> SqlQueryAsync<T>(string sql, object parameterModel)
        {
            return SqlQueryAsync<T>(sql, parameterModel.ToSqlParameters());
        }
        #endregion


        #region sql 查询 + public virtual object SqlQuery(string sql, Type returnType, params object[] parameters)
        /// <summary>
        /// sql 查询
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="returnType">结果集类型</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>object</returns>
        public virtual object SqlQuery(string sql, Type returnType, params object[] parameters)
        {
            return Database.SqlQuery(sql, returnType, CommandType.Text, parameters);
        }
        #endregion

        #region sql 查询 + public virtual Task<object> SqlQueryAsync(string sql, Type returnType, params object[] parameters)
        /// <summary>
        /// sql 查询
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="returnType">结果集类型</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<object> SqlQueryAsync(string sql, Type returnType, params object[] parameters)
        {
            return Database.SqlQueryAsync(sql, returnType, CommandType.Text, parameters);
        }
        #endregion


        #region sql 查询 返回 DataSet + public virtual DataSet SqlDataSetQuery(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询 返回 DataSet
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataSet"/></returns>
        public virtual DataSet SqlDataSetQuery(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery(sql, CommandType.Text, parameters);
        }
        #endregion

        #region sql 查询 返回 DataSet + public virtual Task<DataSet> SqlDataSetQueryAsync(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询 返回 DataSet
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<DataSet> SqlDataSetQueryAsync(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync(sql, CommandType.Text, parameters);
        }
        #endregion

        #region sql 查询 返回 DataSet + public virtual DataSet SqlDataSetQuery(string sql, object parameterModel)
        /// <summary>
        /// sql 查询 返回 DataSet
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="DataSet"/></returns>
        public virtual DataSet SqlDataSetQuery(string sql, object parameterModel)
        {
            return SqlDataSetQuery(sql, parameterModel.ToSqlParameters());
        }
        #endregion

        #region sql 查询 返回 DataSet + public virtual Task<DataSet> SqlDataSetQueryAsync(string sql, object parameterModel)
        /// <summary>
        /// sql 查询 返回 DataSet
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">命令参数</param>
        /// <returns><see cref="DataSet"/></returns>
        public virtual Task<DataSet> SqlDataSetQueryAsync(string sql, object parameterModel)
        {
            return SqlDataSetQueryAsync(sql, parameterModel.ToSqlParameters());
        }
        #endregion


        #region sql 查询 返回一个结果集 + public virtual IEnumerable<T1> SqlDataSetQuery<T1>(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询 返回一个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public virtual IEnumerable<T1> SqlDataSetQuery<T1>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1>(sql, CommandType.Text, parameters);
        }
        #endregion

        #region sql 查询 返回两个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlDataSetQuery<T1, T2>(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询 返回两个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="IEnumerable{T}"/></param>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlDataSetQuery<T1, T2>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1, T2>(sql, CommandType.Text, parameters);
        }
        #endregion

        #region sql 查询 返回三个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlDataSetQuery<T1, T2, T3>(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询 返回三个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlDataSetQuery<T1, T2, T3>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1, T2, T3>(sql, CommandType.Text, parameters);
        }
        #endregion

        #region sql 查询 返回四个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlDataSetQuery<T1, T2, T3, T4>(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询 返回四个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlDataSetQuery<T1, T2, T3, T4>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1, T2, T3, T4>(sql, CommandType.Text, parameters);
        }
        #endregion

        #region sql 查询 返回五个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlDataSetQuery<T1, T2, T3, T4, T5>(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询 返回五个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlDataSetQuery<T1, T2, T3, T4, T5>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1, T2, T3, T4, T5>(sql, CommandType.Text, parameters);
        }
        #endregion

        #region sql 查询 返回六个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlDataSetQuery<T1, T2, T3, T4, T5, T6>(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询 返回六个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlDataSetQuery<T1, T2, T3, T4, T5, T6>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1, T2, T3, T4, T5, T6>(sql, CommandType.Text, parameters);
        }
        #endregion

        #region sql 查询 返回七个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询 返回七个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6, T7}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(sql, CommandType.Text, parameters);
        }
        #endregion

        #region sql 查询 返回八个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询 返回八个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <typeparam name="T8">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(sql, CommandType.Text, parameters);
        }
        #endregion


        #region sql 查询 返回一个结果集 + public virtual Task<IEnumerable<T1>> SqlDataSetQueryAsync<T1>(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询 返回一个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<IEnumerable<T1>> SqlDataSetQueryAsync<T1>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1>(sql, CommandType.Text, parameters);
        }
        #endregion

        #region sql 查询 返回两个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlDataSetQueryAsync<T1, T2>(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询 返回两个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="IEnumerable{T}"/></param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlDataSetQueryAsync<T1, T2>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1, T2>(sql, CommandType.Text, parameters);
        }
        #endregion

        #region sql 查询 返回三个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlDataSetQueryAsync<T1, T2, T3>(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询 返回三个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlDataSetQueryAsync<T1, T2, T3>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1, T2, T3>(sql, CommandType.Text, parameters);
        }
        #endregion

        #region sql 查询 返回四个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlDataSetQueryAsync<T1, T2, T3, T4>(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询 返回四个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlDataSetQueryAsync<T1, T2, T3, T4>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1, T2, T3, T4>(sql, CommandType.Text, parameters);
        }
        #endregion

        #region sql 查询 返回五个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5>(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询 返回五个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1, T2, T3, T4, T5>(sql, CommandType.Text, parameters);
        }
        #endregion

        #region sql 查询 返回六个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询 返回六个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(sql, CommandType.Text, parameters);
        }
        #endregion

        #region sql 查询 返回七个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询 返回七个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(sql, CommandType.Text, parameters);
        }
        #endregion

        #region sql 查询 返回八个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, params object[] parameters)
        /// <summary>
        /// sql 查询 返回八个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <typeparam name="T8">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(sql, CommandType.Text, parameters);
        }
        #endregion


        #region sql 查询返回一个结果集 + public virtual IEnumerable<T1> SqlDataSetQuery<T1>(string sql, object parameterModel)
        /// <summary>
        /// sql 查询返回一个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public virtual IEnumerable<T1> SqlDataSetQuery<T1>(string sql, object parameterModel)
        {
            return SqlDataSetQuery<T1>(sql, parameterModel.ToSqlParameters());
        }
        #endregion

        #region sql 查询返回两个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlDataSetQuery<T1, T2>(string sql, object parameterModel)
        /// <summary>
        /// sql 查询返回两个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlDataSetQuery<T1, T2>(string sql, object parameterModel)
        {
            return SqlDataSetQuery<T1, T2>(sql, parameterModel.ToSqlParameters());
        }
        #endregion

        #region sql 查询返回三个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlDataSetQuery<T1, T2, T3>(string sql, object parameterModel)
        /// <summary>
        /// sql 查询返回三个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2, T3}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlDataSetQuery<T1, T2, T3>(string sql, object parameterModel)
        {
            return SqlDataSetQuery<T1, T2, T3>(sql, parameterModel.ToSqlParameters());
        }
        #endregion

        #region sql 查询返回四个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlDataSetQuery<T1, T2, T3, T4>(string sql, object parameterModel)
        /// <summary>
        /// sql 查询返回四个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlDataSetQuery<T1, T2, T3, T4>(string sql, object parameterModel)
        {
            return SqlDataSetQuery<T1, T2, T3, T4>(sql, parameterModel.ToSqlParameters());
        }
        #endregion

        #region sql 查询返回五个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlDataSetQuery<T1, T2, T3, T4, T5>(string sql, object parameterModel)
        /// <summary>
        /// sql 查询返回五个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlDataSetQuery<T1, T2, T3, T4, T5>(string sql, object parameterModel)
        {
            return SqlDataSetQuery<T1, T2, T3, T4, T5>(sql, parameterModel.ToSqlParameters());
        }
        #endregion

        #region sql 查询返回六个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlDataSetQuery<T1, T2, T3, T4, T5, T6>(string sql, object parameterModel)
        /// <summary>
        /// sql 查询返回六个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlDataSetQuery<T1, T2, T3, T4, T5, T6>(string sql, object parameterModel)
        {
            return SqlDataSetQuery<T1, T2, T3, T4, T5, T6>(sql, parameterModel.ToSqlParameters());
        }
        #endregion

        #region sql 查询返回七个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(string sql, object parameterModel)
        /// <summary>
        /// sql 查询返回七个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6, T7}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(string sql, object parameterModel)
        {
            return SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(sql, parameterModel.ToSqlParameters());
        }
        #endregion

        #region sql 查询返回八个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, object parameterModel)
        /// <summary>
        /// sql 查询返回八个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <typeparam name="T8">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, object parameterModel)
        {
            return SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(sql, parameterModel.ToSqlParameters());
        }
        #endregion


        #region sql 查询返回一个结果集 + public virtual Task<IEnumerable<T1>> SqlDataSetQueryAsync<T1>(string sql, object parameterModel)
        /// <summary>
        /// sql 查询返回一个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<IEnumerable<T1>> SqlDataSetQueryAsync<T1>(string sql, object parameterModel)
        {
            return SqlDataSetQueryAsync<T1>(sql, parameterModel.ToSqlParameters());
        }
        #endregion

        #region sql 查询返回两个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlDataSetQueryAsync<T1, T2>(string sql, object parameterModel)
        /// <summary>
        /// sql 查询返回两个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlDataSetQueryAsync<T1, T2>(string sql, object parameterModel)
        {
            return SqlDataSetQueryAsync<T1, T2>(sql, parameterModel.ToSqlParameters());
        }
        #endregion

        #region sql 查询返回三个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlDataSetQueryAsync<T1, T2, T3>(string sql, object parameterModel)
        /// <summary>
        /// sql 查询返回三个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlDataSetQueryAsync<T1, T2, T3>(string sql, object parameterModel)
        {
            return SqlDataSetQueryAsync<T1, T2, T3>(sql, parameterModel.ToSqlParameters());
        }
        #endregion

        #region sql 查询返回四个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlDataSetQueryAsync<T1, T2, T3, T4>(string sql, object parameterModel)
        /// <summary>
        /// sql 查询返回四个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlDataSetQueryAsync<T1, T2, T3, T4>(string sql, object parameterModel)
        {
            return SqlDataSetQueryAsync<T1, T2, T3, T4>(sql, parameterModel.ToSqlParameters());
        }
        #endregion

        #region sql 查询返回五个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5>(string sql, object parameterModel)
        /// <summary>
        /// sql 查询返回五个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5>(string sql, object parameterModel)
        {
            return SqlDataSetQueryAsync<T1, T2, T3, T4, T5>(sql, parameterModel.ToSqlParameters());
        }
        #endregion

        #region sql 查询返回六个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(string sql, object parameterModel)
        /// <summary>
        /// sql 查询返回六个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(string sql, object parameterModel)
        {
            return SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(sql, parameterModel.ToSqlParameters());
        }
        #endregion

        #region sql 查询返回七个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(string sql, object parameterModel)
        /// <summary>
        /// sql 查询返回七个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(string sql, object parameterModel)
        {
            return SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(sql, parameterModel.ToSqlParameters());
        }
        #endregion

        #region sql 查询返回八个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, object parameterModel)
        /// <summary>
        /// sql 查询返回八个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <typeparam name="T8">结果集类型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, object parameterModel)
        {
            return SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(sql, parameterModel.ToSqlParameters());
        }
        #endregion


        #region sql 执行 + public virtual object SqlDataSetQuery(string sql, Type[] returnTypes, params object[] parameters)
        /// <summary>
        /// sql 执行
        /// <para>返回 <see cref="IEnumerable{T}"/> 类型 或 <see cref="Tuple"/> 类型</para>
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="returnTypes">结果集类型集合</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>object</returns>
        public virtual object SqlDataSetQuery(string sql, Type[] returnTypes, params object[] parameters)
        {
            return Database.SqlDataSetQuery(sql, returnTypes, CommandType.Text, parameters);
        }
        #endregion

        #region sql 执行 + public virtual Task<object> SqlDataSetQueryAsync(string sql, Type[] returnTypes, params object[] parameters)
        /// <summary>
        /// sql 执行
        /// <para>返回 <see cref="IEnumerable{T}"/> 类型 或 <see cref="Tuple"/> 类型</para>
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="returnTypes">结果集类型集合</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<object> SqlDataSetQueryAsync(string sql, Type[] returnTypes, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync(sql, returnTypes, CommandType.Text, parameters);
        }
        #endregion

        #region sql 执行 + public virtual object SqlDataSetQuery(string sql, Type[] returnTypes, object parameterModel)
        /// <summary>
        /// sql 执行
        /// <para>返回 <see cref="IEnumerable{T}"/> 类型 或 <see cref="Tuple"/> 类型</para>
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="returnTypes">结果集类型集合</param>
        /// <param name="parameterModel">parameterModel</param>
        /// <returns>object</returns>
        public virtual object SqlDataSetQuery(string sql, Type[] returnTypes, object parameterModel)
        {
            return SqlDataSetQuery(sql, returnTypes, parameterModel.ToSqlParameters());
        }
        #endregion

        #region sql 执行 + public virtual Task<object> SqlDataSetQueryAsync(string sql, Type[] returnTypes, object parameterModel)
        /// <summary>
        /// sql 执行
        /// <para>返回 <see cref="IEnumerable{T}"/> 类型 或 <see cref="Tuple"/> 类型</para>
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="returnTypes">结果集类型集合</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns>object</returns>
        public virtual Task<object> SqlDataSetQueryAsync(string sql, Type[] returnTypes, object parameterModel)
        {
            return SqlDataSetQueryAsync(sql, returnTypes, parameterModel.ToSqlParameters());
        }
        #endregion
    }
}
