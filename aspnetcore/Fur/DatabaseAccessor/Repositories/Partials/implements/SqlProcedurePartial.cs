using Fur.DatabaseAccessor.Models;
using Fur.DatabaseAccessor.Extensions.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Fur.DatabaseAccessor.Models.Entities;

namespace Fur.DatabaseAccessor.Repositories
{
    /// <summary>
    /// 泛型仓储 存储过程操作 分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial class EFCoreRepositoryOfT<TEntity> : IRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        #region 执行存储过程返回 DataTable + public virtual DataTable SqlProcedure(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回 DataTable
        /// </summary>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataTable"/></returns>
        public virtual DataTable SqlProcedure(string name, params object[] parameters)
        {
            return Database.SqlProcedure(name, parameters);
        }

        #endregion 执行存储过程返回 DataTable + public virtual DataTable SqlProcedure(string name, params object[] parameters)

        #region 执行存储过程返回 DataTable + public virtual Task<DataTable> SqlProcedureAsync(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回 DataTable
        /// </summary>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<DataTable> SqlProcedureAsync(string name, params object[] parameters)
        {
            return Database.SqlProcedureAsync(name, parameters);
        }

        #endregion 执行存储过程返回 DataTable + public virtual Task<DataTable> SqlProcedureAsync(string name, params object[] parameters)

        #region 支持存储过程返回 DataTable + public virtual DataTable SqlProcedure(string name, object parameterModel)

        /// <summary>
        /// 支持存储过程返回 DataTable
        /// </summary>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="DataTable"/></returns>
        public virtual DataTable SqlProcedure(string name, object parameterModel)
        {
            return Database.SqlProcedure(name, parameterModel.ToSqlParameters());
        }

        #endregion 支持存储过程返回 DataTable + public virtual DataTable SqlProcedure(string name, object parameterModel)

        #region 执行存储过程返回 DataTable + public virtual Task<DataTable> SqlProcedureAsync(string name, object parameterModel)

        /// <summary>
        /// 执行存储过程返回 DataTable
        /// </summary>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<DataTable> SqlProcedureAsync(string name, object parameterModel)
        {
            return Database.SqlProcedureAsync(name, parameterModel.ToSqlParameters());
        }

        #endregion 执行存储过程返回 DataTable + public virtual Task<DataTable> SqlProcedureAsync(string name, object parameterModel)


        #region 执行存储过程返回单个结果集 + public virtual IEnumerable<T> SqlProcedure<T>(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回单个结果集
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public virtual IEnumerable<T> SqlProcedure<T>(string name, params object[] parameters)
        {
            return Database.SqlProcedure<T>(name, parameters);
        }

        #endregion 执行存储过程返回单个结果集 + public virtual IEnumerable<T> SqlProcedure<T>(string name, params object[] parameters)

        #region 执行存储过程返回单个结果集 + public virtual Task<IEnumerable<T>> SqlProcedureAsync<T>(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回单个结果集
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<IEnumerable<T>> SqlProcedureAsync<T>(string name, params object[] parameters)
        {
            return Database.SqlProcedureAsync<T>(name, parameters);
        }

        #endregion 执行存储过程返回单个结果集 + public virtual Task<IEnumerable<T>> SqlProcedureAsync<T>(string name, params object[] parameters)

        #region 执行存储过程返回单个结果集 + public virtual IEnumerable<T> SqlProcedure<T>(string name, object parameterModel)

        /// <summary>
        /// 执行存储过程返回单个结果集
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public virtual IEnumerable<T> SqlProcedure<T>(string name, object parameterModel)
        {
            return Database.SqlProcedure<T>(name, parameterModel.ToSqlParameters());
        }

        #endregion 执行存储过程返回单个结果集 + public virtual IEnumerable<T> SqlProcedure<T>(string name, object parameterModel)

        #region 执行存储过程返回单个结果集 + public virtual Task<IEnumerable<T>> SqlProcedureAsync<T>(string name, object parameterModel)

        /// <summary>
        /// 执行存储过程返回单个结果集
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<IEnumerable<T>> SqlProcedureAsync<T>(string name, object parameterModel)
        {
            return Database.SqlProcedureAsync<T>(name, parameterModel.ToSqlParameters());
        }

        #endregion 执行存储过程返回单个结果集 + public virtual Task<IEnumerable<T>> SqlProcedureAsync<T>(string name, object parameterModel)


        #region 执行存储过程返回 DataSet + public virtual DataSet SqlProcedureDataSet(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回 DataSet
        /// </summary>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataSet"/></returns>
        public virtual DataSet SqlProcedureDataSet(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSet(name, parameters);
        }

        #endregion 执行存储过程返回 DataSet + public virtual DataSet SqlProcedureDataSet(string name, params object[] parameters)

        #region 执行存储过程返回 DataSet + public virtual Task<DataSet> SqlProcedureDataSetAsync(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回 DataSet
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<DataSet> SqlProcedureDataSetAsync(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetAsync(name, parameters);
        }

        #endregion 执行存储过程返回 DataSet + public virtual Task<DataSet> SqlProcedureDataSetAsync(string name, params object[] parameters)

        #region 执行存储过程返回 DataSet + public virtual DataSet SqlProcedureDataSet(string name, object parameterModel)

        /// <summary>
        /// 执行存储过程返回 DataSet
        /// </summary>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="DataSet"/></returns>
        public virtual DataSet SqlProcedureDataSet(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSet(name, parameterModel.ToString());
        }

        #endregion 执行存储过程返回 DataSet + public virtual DataSet SqlProcedureDataSet(string name, object parameterModel)

        #region 执行存储过程返回 DataSet + public virtual Task<DataSet> SqlProcedureDataSetAsync(string name, object parameterModel)

        /// <summary>
        /// 执行存储过程返回 DataSet
        /// </summary>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<DataSet> SqlProcedureDataSetAsync(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetAsync(name, parameterModel.ToSqlParameters());
        }

        #endregion 执行存储过程返回 DataSet + public virtual Task<DataSet> SqlProcedureDataSetAsync(string name, object parameterModel)


        #region 执行存储过程返回一个结果集 + public virtual IEnumerable<T1> SqlProcedureDataSet<T1>(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回一个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public virtual IEnumerable<T1> SqlProcedureDataSet<T1>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSet<T1>(name, parameters);
        }

        #endregion 执行存储过程返回一个结果集 + public virtual IEnumerable<T1> SqlProcedureDataSet<T1>(string name, params object[] parameters)

        #region 执行存储过程返回两个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlProcedureDataSet<T1, T2>(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回两个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlProcedureDataSet<T1, T2>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSet<T1, T2>(name, parameters);
        }

        #endregion 执行存储过程返回两个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlProcedureDataSet<T1, T2>(string name, params object[] parameters)

        #region 执行存储过程返回三个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlProcedureDataSet<T1, T2, T3>(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回三个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlProcedureDataSet<T1, T2, T3>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSet<T1, T2, T3>(name, parameters);
        }

        #endregion 执行存储过程返回三个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlProcedureDataSet<T1, T2, T3>(string name, params object[] parameters)

        #region 执行存储过程返回四个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlProcedureDataSet<T1, T2, T3, T4>(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回四个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlProcedureDataSet<T1, T2, T3, T4>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSet<T1, T2, T3, T4>(name, parameters);
        }

        #endregion 执行存储过程返回四个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlProcedureDataSet<T1, T2, T3, T4>(string name, params object[] parameters)

        #region 执行存储过程返回五个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlProcedureDataSet<T1, T2, T3, T4, T5>(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回五个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlProcedureDataSet<T1, T2, T3, T4, T5>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSet<T1, T2, T3, T4, T5>(name, parameters);
        }

        #endregion 执行存储过程返回五个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlProcedureDataSet<T1, T2, T3, T4, T5>(string name, params object[] parameters)

        #region 执行存储过程返回六个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6>(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回六个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSet<T1, T2, T3, T4, T5, T6>(name, parameters);
        }

        #endregion 执行存储过程返回六个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6>(string name, params object[] parameters)

        #region 执行存储过程返回七个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7>(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回七个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6, T7}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7>(name, parameters);
        }

        #endregion 执行存储过程返回七个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7>(string name, params object[] parameters)

        #region 执行存储过程返回八个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7, T8>(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回八个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <typeparam name="T8">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7, T8>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7, T8>(name, parameters);
        }

        #endregion 执行存储过程返回八个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7, T8>(string name, params object[] parameters)


        #region 执行存储过程返回一个结果集 + public virtual Task<IEnumerable<T1>> SqlProcedureDataSetAsync<T1>(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回一个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public virtual Task<IEnumerable<T1>> SqlProcedureDataSetAsync<T1>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetAsync<T1>(name, parameters);
        }

        #endregion 执行存储过程返回一个结果集 + public virtual Task<IEnumerable<T1>> SqlProcedureDataSetAsync<T1>(string name, params object[] parameters)

        #region 执行存储过程返回两个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlProcedureDataSetAsync<T1, T2>(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回两个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlProcedureDataSetAsync<T1, T2>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetAsync<T1, T2>(name, parameters);
        }

        #endregion 执行存储过程返回两个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlProcedureDataSetAsync<T1, T2>(string name, params object[] parameters)

        #region 执行存储过程返回三个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlProcedureDataSetAsync<T1, T2, T3>(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回三个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlProcedureDataSetAsync<T1, T2, T3>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetAsync<T1, T2, T3>(name, parameters);
        }

        #endregion 执行存储过程返回三个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlProcedureDataSetAsync<T1, T2, T3>(string name, params object[] parameters)

        #region 执行存储过程返回四个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlProcedureDataSetAsync<T1, T2, T3, T4>(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回四个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlProcedureDataSetAsync<T1, T2, T3, T4>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetAsync<T1, T2, T3, T4>(name, parameters);
        }

        #endregion 执行存储过程返回四个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlProcedureDataSetAsync<T1, T2, T3, T4>(string name, params object[] parameters)

        #region 执行存储过程返回五个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5>(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回五个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetAsync<T1, T2, T3, T4, T5>(name, parameters);
        }

        #endregion 执行存储过程返回五个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5>(string name, params object[] parameters)

        #region 执行存储过程返回六个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6>(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回六个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6>(name, parameters);
        }

        #endregion 执行存储过程返回六个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6>(string name, params object[] parameters)

        #region 执行存储过程返回七个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7>(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回七个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7>(name, parameters);
        }

        #endregion 执行存储过程返回七个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7>(string name, params object[] parameters)

        #region 执行存储过程返回八个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程返回八个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <typeparam name="T8">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8>(name, parameters);
        }

        #endregion 执行存储过程返回八个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string name, params object[] parameters)


        #region 执行存储过程返回一个结果集 + public virtual IEnumerable<T1> SqlProcedureDataSet<T1>(string name, object parameterModel)

        /// <summary>
        /// 执行存储过程返回一个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public virtual IEnumerable<T1> SqlProcedureDataSet<T1>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSet<T1>(name, parameterModel.ToSqlParameters());
        }

        #endregion 执行存储过程返回一个结果集 + public virtual IEnumerable<T1> SqlProcedureDataSet<T1>(string name, object parameterModel)

        #region 执行存储过程返回两个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlProcedureDataSet<T1, T2>(string name, object parameterModel)

        /// <summary>
        /// 执行存储过程返回两个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlProcedureDataSet<T1, T2>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSet<T1, T2>(name, parameterModel.ToSqlParameters());
        }

        #endregion 执行存储过程返回两个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlProcedureDataSet<T1, T2>(string name, object parameterModel)

        #region 执行存储过程返回三个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlProcedureDataSet<T1, T2, T3>(string name, object parameterModel)

        /// <summary>
        /// 执行存储过程返回三个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2, T3}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlProcedureDataSet<T1, T2, T3>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSet<T1, T2, T3>(name, parameterModel.ToSqlParameters());
        }

        #endregion 执行存储过程返回三个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlProcedureDataSet<T1, T2, T3>(string name, object parameterModel)

        #region 执行存储过程返回四个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlProcedureDataSet<T1, T2, T3, T4>(string name, object parameterModel)

        /// <summary>
        /// 执行存储过程返回四个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlProcedureDataSet<T1, T2, T3, T4>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSet<T1, T2, T3, T4>(name, parameterModel.ToSqlParameters());
        }

        #endregion 执行存储过程返回四个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlProcedureDataSet<T1, T2, T3, T4>(string name, object parameterModel)

        #region 执行存储过程返回五个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlProcedureDataSet<T1, T2, T3, T4, T5>(string name, object parameterModel)

        /// <summary>
        /// 执行存储过程返回五个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlProcedureDataSet<T1, T2, T3, T4, T5>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSet<T1, T2, T3, T4, T5>(name, parameterModel.ToSqlParameters());
        }

        #endregion 执行存储过程返回五个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlProcedureDataSet<T1, T2, T3, T4, T5>(string name, object parameterModel)

        #region 执行存储过程返回六个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6>(string name, object parameterModel)

        /// <summary>
        /// 执行存储过程返回六个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSet<T1, T2, T3, T4, T5, T6>(name, parameterModel.ToSqlParameters());
        }

        #endregion 执行存储过程返回六个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6>(string name, object parameterModel)

        #region 执行存储过程返回七个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7>(string name, object parameterModel)

        /// <summary>
        /// 执行存储过程返回七个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6, T7}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7>(name, parameterModel.ToSqlParameters());
        }

        #endregion 执行存储过程返回七个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7>(string name, object parameterModel)

        #region 执行存储过程返回八个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7, T8>(string name, object parameterModel)

        /// <summary>
        /// 执行存储过程返回八个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <typeparam name="T8">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2, T3, T4, T5, T6, T7, TRest}"/></returns>
        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7, T8>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7, T8>(name, parameterModel.ToSqlParameters());
        }

        #endregion 执行存储过程返回八个结果集 + public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7, T8>(string name, object parameterModel)


        #region 执行存储过程返回一个结果集 + public virtual Task<IEnumerable<T1>> SqlProcedureDataSetAsync<T1>(string name, object parameterModel)

        /// <summary>
        /// 执行存储过程返回一个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<IEnumerable<T1>> SqlProcedureDataSetAsync<T1>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetAsync<T1>(name, parameterModel.ToSqlParameters());
        }

        #endregion 执行存储过程返回一个结果集 + public virtual Task<IEnumerable<T1>> SqlProcedureDataSetAsync<T1>(string name, object parameterModel)

        #region 执行存储过程返回两个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlProcedureDataSetAsync<T1, T2>(string name, object parameterModel)

        /// <summary>
        /// 执行存储过程返回两个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlProcedureDataSetAsync<T1, T2>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetAsync<T1, T2>(name, parameterModel.ToSqlParameters());
        }

        #endregion 执行存储过程返回两个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlProcedureDataSetAsync<T1, T2>(string name, object parameterModel)

        #region 执行存储过程返回三个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlProcedureDataSetAsync<T1, T2, T3>(string name, object parameterModel)

        /// <summary>
        /// 执行存储过程返回三个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlProcedureDataSetAsync<T1, T2, T3>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetAsync<T1, T2, T3>(name, parameterModel.ToSqlParameters());
        }

        #endregion 执行存储过程返回三个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlProcedureDataSetAsync<T1, T2, T3>(string name, object parameterModel)

        #region 执行存储过程返回四个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlProcedureDataSetAsync<T1, T2, T3, T4>(string name, object parameterModel)

        /// <summary>
        /// 执行存储过程返回四个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlProcedureDataSetAsync<T1, T2, T3, T4>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetAsync<T1, T2, T3, T4>(name, parameterModel.ToSqlParameters());
        }

        #endregion 执行存储过程返回四个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlProcedureDataSetAsync<T1, T2, T3, T4>(string name, object parameterModel)

        #region 执行存储过程返回五个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5>(string name, object parameterModel)

        /// <summary>
        /// 执行存储过程返回五个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetAsync<T1, T2, T3, T4, T5>(name, parameterModel.ToSqlParameters());
        }

        #endregion 执行存储过程返回五个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5>(string name, object parameterModel)

        #region 执行存储过程返回六个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6>(string name, object parameterModel)

        /// <summary>
        /// 执行存储过程返回六个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6>(name, parameterModel.ToSqlParameters());
        }

        #endregion 执行存储过程返回六个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6>(string name, object parameterModel)

        #region 执行存储过程返回七个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7>(string name, object parameterModel)

        /// <summary>
        /// 执行存储过程返回七个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7>(name, parameterModel.ToSqlParameters());
        }

        #endregion 执行存储过程返回七个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7>(string name, object parameterModel)

        #region 执行存储过程返回八个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string name, object parameterModel)

        /// <summary>
        /// 执行存储过程返回八个结果集
        /// </summary>
        /// <typeparam name="T1">结果集类型</typeparam>
        /// <typeparam name="T2">结果集类型</typeparam>
        /// <typeparam name="T3">结果集类型</typeparam>
        /// <typeparam name="T4">结果集类型</typeparam>
        /// <typeparam name="T5">结果集类型</typeparam>
        /// <typeparam name="T6">结果集类型</typeparam>
        /// <typeparam name="T7">结果集类型</typeparam>
        /// <typeparam name="T8">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8>(name, parameterModel.ToSqlParameters());
        }

        #endregion 执行存储过程返回八个结果集 + public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string name, object parameterModel)


        #region 执行存储过程 + public virtual object SqlProcedureDataSet(string name, Type[] returnTypes, params object[] parameters)

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="name">存储过程名称</param>
        /// <param name="returnTypes">结果集类型集合</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns>object</returns>
        public virtual object SqlProcedureDataSet(string name, Type[] returnTypes, params object[] parameters)
        {
            return Database.SqlProcedureDataSet(name, returnTypes, parameters);
        }

        #endregion 执行存储过程 + public virtual object SqlProcedureDataSet(string name, Type[] returnTypes, params object[] parameters)

        #region 执行存储过程 + public virtual Task<object> SqlProcedureDataSetAsync(string name, Type[] returnTypes, params object[] parameters)

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="name">存储过程名</param>
        /// <param name="returnTypes">结果集类型集合</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<object> SqlProcedureDataSetAsync(string name, Type[] returnTypes, params object[] parameters)
        {
            return Database.SqlProcedureDataSetAsync(name, returnTypes, parameters);
        }

        #endregion 执行存储过程 + public virtual Task<object> SqlProcedureDataSetAsync(string name, Type[] returnTypes, params object[] parameters)

        #region 执行存储过程 + public virtual object SqlProcedureDataSet(string name, Type[] returnTypes, object parameterModel)

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="name">存储过程名</param>
        /// <param name="returnTypes">结果集类型集合</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns>object</returns>
        public virtual object SqlProcedureDataSet(string name, Type[] returnTypes, object parameterModel)
        {
            return Database.SqlProcedureDataSet(name, returnTypes, parameterModel).ToSqlParameters();
        }

        #endregion 执行存储过程 + public virtual object SqlProcedureDataSet(string name, Type[] returnTypes, object parameterModel)

        #region 执行存储过程 + public virtual Task<object> SqlProcedureDataSetAsync(string name, Type[] returnTypes, object parameterModel)

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="name">存储过程名</param>
        /// <param name="returnTypes">结果集类型集合</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<object> SqlProcedureDataSetAsync(string name, Type[] returnTypes, object parameterModel)
        {
            return Database.SqlProcedureDataSetAsync(name, returnTypes, parameterModel.ToSqlParameters());
        }

        #endregion 执行存储过程 + public virtual Task<object> SqlProcedureDataSetAsync(string name, Type[] returnTypes, object parameterModel)


        #region 执行存储过程（带输出或返回值）+ public virtual (Dictionary<string, object> outputValues, object returnValue) SqlProcedureNonQuery(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程（带输出或返回值）
        /// </summary>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        public virtual (Dictionary<string, object> outputValues, object returnValue) SqlProcedureNonQuery(string name, params object[] parameters)
        {
            return Database.SqlProcedureNonQuery(name, parameters);
        }

        #endregion 执行存储过程（带输出或返回值）+ public virtual (Dictionary<string, object> outputValues, object returnValue) SqlProcedureNonQuery(string name, params object[] parameters)

        #region 执行存储过程（带输出或返回值）+ public virtual Task<(Dictionary<string, object> outputValues, object returnValue)> SqlProcedureNonQueryAsync(string name, params object[] parameters)

        /// <summary>
        /// 执行存储过程（带输出或返回值）
        /// </summary>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public virtual Task<(Dictionary<string, object> outputValues, object returnValue)> SqlProcedureNonQueryAsync(string name, params object[] parameters)
        {
            return Database.SqlProcedureNonQueryAsync(name, parameters);
        }

        #endregion 执行存储过程（带输出或返回值）+ public virtual Task<(Dictionary<string, object> outputValues, object returnValue)> SqlProcedureNonQueryAsync(string name, params object[] parameters)

        #region 执行存储过程（带输出或返回值）+ public virtual (Dictionary<string, object> outputValues, object returnValue) SqlProcedureNonQuery(string name, object parameterModel)

        /// <summary>
        /// 执行存储过程（带输出或返回值）
        /// </summary>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        public virtual (Dictionary<string, object> outputValues, object returnValue) SqlProcedureNonQuery(string name, object parameterModel)
        {
            return Database.SqlProcedureNonQuery(name, parameterModel.ToSqlParameters());
        }

        #endregion 执行存储过程（带输出或返回值）+ public virtual (Dictionary<string, object> outputValues, object returnValue) SqlProcedureNonQuery(string name, object parameterModel)

        #region 执行存储过程（带输出或返回值）+ public virtual Task<(Dictionary<string, object> outputValues, object returnValue)> SqlProcedureNonQueryAsync(string name, object parameterModel)

        /// <summary>
        /// 执行存储过程（带输出或返回值）
        /// </summary>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Tuple{T1, T2}"/></returns>
        public virtual Task<(Dictionary<string, object> outputValues, object returnValue)> SqlProcedureNonQueryAsync(string name, object parameterModel)
        {
            return Database.SqlProcedureNonQueryAsync(name, parameterModel.ToSqlParameters());
        }

        #endregion 执行存储过程（带输出或返回值）+ public virtual Task<(Dictionary<string, object> outputValues, object returnValue)> SqlProcedureNonQueryAsync(string name, object parameterModel)
    }
}