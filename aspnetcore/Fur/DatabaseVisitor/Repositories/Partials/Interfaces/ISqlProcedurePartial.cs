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
        DataTable SqlProcedureQuery(string name, params object[] parameters);

        Task<DataTable> SqlProcedureQueryAsync(string name, params object[] parameters);

        DataTable SqlProcedureQuery(string name, object parameterModel);

        Task<DataTable> SqlProcedureQueryAsync(string name, object parameterModel);

        IEnumerable<T> SqlProcedureQuery<T>(string name, params object[] parameters);

        Task<IEnumerable<T>> SqlProcedureQueryAsync<T>(string name, params object[] parameters);

        IEnumerable<T> SqlProcedureQuery<T>(string name, object parameterModel);

        Task<IEnumerable<T>> SqlProcedureQueryAsync<T>(string name, object parameterModel);

        DataSet SqlProcedureDataSetQuery(string name, params object[] parameters);

        Task<DataSet> SqlProcedureDataSetQueryAsync(string name, params object[] parameters);

        DataSet SqlProcedureDataSetQuery(string name, object parameterModel);

        Task<DataSet> SqlProcedureDataSetQueryAsync(string name, object parameterModel);

        IEnumerable<T1> SqlProcedureDataSetQuery<T1>(string name, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlProcedureDataSetQuery<T1, T2>(string name, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlProcedureDataSetQuery<T1, T2, T3>(string name, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlProcedureDataSetQuery<T1, T2, T3, T4>(string name, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5>(string name, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6>(string name, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(string name, params object[] parameters);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(string name, params object[] parameters);

        object SqlProcedureDataSetQuery(string name, Type[] returnTypes, params object[] parameters);

        Task<IEnumerable<T1>> SqlProcedureDataSetQueryAsync<T1>(string name, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlProcedureDataSetQueryAsync<T1, T2>(string name, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlProcedureDataSetQueryAsync<T1, T2, T3>(string name, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4>(string name, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5>(string name, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(string name, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(string name, params object[] parameters);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string name, params object[] parameters);

        Task<object> SqlProcedureDataSetQueryAsync(string name, Type[] returnTypes, params object[] parameters);

        IEnumerable<T1> SqlProcedureDataSetQuery<T1>(string name, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlProcedureDataSetQuery<T1, T2>(string name, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlProcedureDataSetQuery<T1, T2, T3>(string name, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlProcedureDataSetQuery<T1, T2, T3, T4>(string name, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5>(string name, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6>(string name, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(string name, object parameterModel);

        (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(string name, object parameterModel);

        object SqlProcedureDataSetQuery(string name, Type[] returnTypes, object parameterModel);

        Task<IEnumerable<T1>> SqlProcedureDataSetQueryAsync<T1>(string name, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlProcedureDataSetQueryAsync<T1, T2>(string name, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlProcedureDataSetQueryAsync<T1, T2, T3>(string name, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4>(string name, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5>(string name, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(string name, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(string name, object parameterModel);

        Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string name, object parameterModel);

        Task<object> SqlProcedureDataSetQueryAsync(string name, Type[] returnTypes, object parameterModel);


        (Dictionary<string, object> outputValues, object returnValue) SqlProcedureJustExecute(string name, params object[] parameters);

        Task<(Dictionary<string, object> outputValues, object returnValue)> SqlProcedureJustExecuteAsync(string name, params object[] parameters);

        (Dictionary<string, object> outputValues, object returnValue) SqlProcedureJustExecute(string name, object parameterModel);

        Task<(Dictionary<string, object> outputValues, object returnValue)> SqlProcedureJustExecuteAsync(string name, object parameterModel);
    }
}
