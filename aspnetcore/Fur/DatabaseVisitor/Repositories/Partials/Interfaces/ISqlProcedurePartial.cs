using Fur.DatabaseVisitor.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 泛型仓储 存储过程操作 分部接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial interface IRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        #region 执行存储过程返回 DataTable + DataTable SqlProcedure(string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程返回 DataTable
        /// </summary>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataTable"/></returns>
        DataTable SqlProcedure(string name, params object[] parameters);
        #endregion

        #region 执行存储过程返回 DataTable + Task<DataTable> SqlProcedureAsync(string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程返回 DataTable
        /// </summary>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<DataTable> SqlProcedureAsync(string name, params object[] parameters);
        #endregion

        #region 执行存储过程返回 DataTable + DataTable SqlProcedure(string name, object parameterModel)
        /// <summary>
        /// 支持存储过程返回 DataTable
        /// </summary>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="DataTable"/></returns>
        DataTable SqlProcedure(string name, object parameterModel);
        #endregion

        #region 执行存储过程返回 DataTable + Task<DataTable> SqlProcedureAsync(string name, object parameterModel)
        /// <summary>
        /// 执行存储过程返回 DataTable
        /// </summary>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<DataTable> SqlProcedureAsync(string name, object parameterModel);
        #endregion


        #region 执行存储过程返回单个结果集 + IEnumerable<T> SqlProcedure<T>(string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程返回单个结果集
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<T> SqlProcedure<T>(string name, params object[] parameters);
        #endregion

        #region 执行存储过程返回单个结果集 + Task<IEnumerable<T>> SqlProcedureAsync<T>(string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程返回单个结果集
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<IEnumerable<T>> SqlProcedureAsync<T>(string name, params object[] parameters);
        #endregion

        #region 执行存储过程返回单个结果集 + IEnumerable<T> SqlProcedure<T>(string name, object parameterModel)
        /// <summary>
        /// 执行存储过程返回单个结果集
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<T> SqlProcedure<T>(string name, object parameterModel);
        #endregion

        #region 执行存储过程返回单个结果集 + Task<IEnumerable<T>> SqlProcedureAsync<T>(string name, object parameterModel)
        /// <summary>
        /// 执行存储过程返回单个结果集
        /// </summary>
        /// <typeparam name="T">结果集类型</typeparam>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<IEnumerable<T>> SqlProcedureAsync<T>(string name, object parameterModel);
        #endregion


        #region 执行存储过程返回 DataSet + DataSet SqlProcedureDataSet(string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程返回 DataSet
        /// </summary>
        /// <param name="name">存储过程名</param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="DataSet"/></returns>
        DataSet SqlProcedureDataSet(string name, params object[] parameters);
        #endregion

        #region 执行存储过程返回 DataSet + Task<DataSet> SqlProcedureDataSetAsync(string name, params object[] parameters)
        /// <summary>
        /// 执行存储过程返回 DataSet
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameters"><see cref="SqlParameter"/> 参数</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<DataSet> SqlProcedureDataSetAsync(string name, params object[] parameters);
        #endregion

        #region 执行存储过程返回 DataSet + DataSet SqlProcedureDataSet(string name, object parameterModel)
        /// <summary>
        /// 执行存储过程返回 DataSet
        /// </summary>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="DataSet"/></returns>
        DataSet SqlProcedureDataSet(string name, object parameterModel);
        #endregion

        #region 执行存储过程返回 DataSet + Task<DataSet> SqlProcedureDataSetAsync(string name, object parameterModel)
        /// <summary>
        /// 执行存储过程返回 DataSet
        /// </summary>
        /// <param name="name">存储过程名</param>
        /// <param name="parameterModel">参数模型</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<DataSet> SqlProcedureDataSetAsync(string name, object parameterModel);
        #endregion


        IEnumerable<T1> SqlProcedureDataSet<T1>(string name, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlProcedureDataSet<T1, T2>(string name, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlProcedureDataSet<T1, T2, T3>(string name, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlProcedureDataSet<T1, T2, T3, T4>(string name, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlProcedureDataSet<T1, T2, T3, T4, T5>(string name, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6>(string name, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7>(string name, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7, T8>(string name, params object[] parameters);

        object SqlProcedureDataSet(string name, Type[] returnTypes, params object[] parameters);

        Task<IEnumerable<T1>> SqlProcedureDataSetAsync<T1>(string name, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlProcedureDataSetAsync<T1, T2>(string name, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlProcedureDataSetAsync<T1, T2, T3>(string name, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlProcedureDataSetAsync<T1, T2, T3, T4>(string name, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5>(string name, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6>(string name, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7>(string name, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string name, params object[] parameters);

        Task<object> SqlProcedureDataSetAsync(string name, Type[] returnTypes, params object[] parameters);

        IEnumerable<T1> SqlProcedureDataSet<T1>(string name, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlProcedureDataSet<T1, T2>(string name, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlProcedureDataSet<T1, T2, T3>(string name, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlProcedureDataSet<T1, T2, T3, T4>(string name, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlProcedureDataSet<T1, T2, T3, T4, T5>(string name, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6>(string name, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7>(string name, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlProcedureDataSet<T1, T2, T3, T4, T5, T6, T7, T8>(string name, object parameterModel);

        object SqlProcedureDataSet(string name, Type[] returnTypes, object parameterModel);

        Task<IEnumerable<T1>> SqlProcedureDataSetAsync<T1>(string name, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlProcedureDataSetAsync<T1, T2>(string name, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlProcedureDataSetAsync<T1, T2, T3>(string name, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlProcedureDataSetAsync<T1, T2, T3, T4>(string name, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5>(string name, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6>(string name, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7>(string name, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlProcedureDataSetAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string name, object parameterModel);

        Task<object> SqlProcedureDataSetAsync(string name, Type[] returnTypes, object parameterModel);



        (Dictionary<string, object> outputValues, object returnValue) SqlProcedureNonQuery(string name, params object[] parameters);

        Task<(Dictionary<string, object> outputValues, object returnValue)> SqlProcedureNonQueryAsync(string name, params object[] parameters);

        (Dictionary<string, object> outputValues, object returnValue) SqlProcedureNonQuery(string name, object parameterModel);

        Task<(Dictionary<string, object> outputValues, object returnValue)> SqlProcedureNonQueryAsync(string name, object parameterModel);
    }
}
