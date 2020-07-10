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
        // 原始Sql查询
        IQueryable<TEntity> FromSqlRaw(string sql, params object[] parameters);

        IQueryable<TEntity> FromSqlRaw(string sql, object parameterModel);

        DataTable FromSqlQuery(string sql, params object[] parameters);

        Task<DataTable> FromSqlQueryAsync(string sql, params object[] parameters);

        DataTable FromSqlQuery(string sql, object parameterModel);

        Task<DataTable> FromSqlQueryAsync(string sql, object parameterModel);

        IEnumerable<T> FromSqlQuery<T>(string sql, params object[] parameters);

        Task<IEnumerable<T>> FromSqlQueryAsync<T>(string sql, params object[] parameters);

        IEnumerable<T> FromSqlQuery<T>(string sql, object parameterModel);

        Task<IEnumerable<T>> FromSqlQueryAsync<T>(string sql, object parameterModel);

        object FromSqlQuery(string sql, Type type, params object[] parameters);

        Task<object> FromSqlQueryAsync(string sql, Type type, params object[] parameters);

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
