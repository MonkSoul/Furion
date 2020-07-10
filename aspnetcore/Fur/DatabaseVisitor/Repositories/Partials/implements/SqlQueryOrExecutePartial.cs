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
        public virtual IQueryable<TEntity> FromSqlRaw(string sql, params object[] parameters)
        {
            return Entity.FromSqlRaw(sql, parameters);
        }

        public virtual IQueryable<TEntity> FromSqlRaw(string sql, object parameterModel)
        {
            return Entity.FromSqlRaw(sql, parameterModel.ToSqlParameters());
        }

        public virtual DataTable FromSqlQuery(string sql, params object[] parameters)
        {
            return Database.SqlQuery(sql, CommandType.Text, parameters);
        }

        public virtual Task<DataTable> FromSqlQueryAsync(string sql, params object[] parameters)
        {
            return Database.SqlQueryAsync(sql, CommandType.Text, parameters);
        }

        public virtual IEnumerable<T> FromSqlQuery<T>(string sql, params object[] parameters)
        {
            return Database.SqlQuery<T>(sql, CommandType.Text, parameters);
        }

        public virtual Task<IEnumerable<T>> FromSqlQueryAsync<T>(string sql, params object[] parameters)
        {
            return Database.SqlQueryAsync<T>(sql, CommandType.Text, parameters);
        }

        public virtual object FromSqlQuery(string sql, Type type, params object[] parameters)
        {
            return Database.SqlQuery(sql, type, CommandType.Text, parameters);
        }

        public virtual Task<object> FromSqlQueryAsync(string sql, Type type, params object[] parameters)
        {
            return Database.SqlQueryAsync(sql, type, CommandType.Text, parameters);
        }

        public virtual DataTable FromSqlQuery(string sql, object parameterModel)
        {
            return FromSqlQuery(sql, parameterModel.ToSqlParameters());
        }

        public virtual Task<DataTable> FromSqlQueryAsync(string sql, object parameterModel)
        {
            return FromSqlQueryAsync(sql, parameterModel.ToSqlParameters());
        }

        public virtual IEnumerable<T> FromSqlQuery<T>(string sql, object parameterModel)
        {
            return FromSqlQuery<T>(sql, parameterModel.ToSqlParameters());
        }

        public virtual Task<IEnumerable<T>> FromSqlQueryAsync<T>(string sql, object parameterModel)
        {
            return FromSqlQueryAsync<T>(sql, parameterModel.ToSqlParameters());
        }

        // 数据集

        public virtual DataSet FromSqlDataSetQuery(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery(sql, CommandType.Text, parameters);
        }

        public virtual Task<DataSet> FromSqlDataSetQueryAsync(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync(sql, CommandType.Text, parameters);
        }

        public virtual DataSet FromSqlDataSetQuery(string sql, object parameterModel)
        {
            return FromSqlDataSetQuery(sql, parameterModel.ToSqlParameters());
        }

        public virtual Task<DataSet> FromSqlDataSetQueryAsync(string sql, object parameterModel)
        {
            return FromSqlDataSetQueryAsync(sql, parameterModel.ToSqlParameters());
        }

        public virtual IEnumerable<T1> FromSqlDataSetQuery<T1>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1>(sql, CommandType.Text, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2) FromSqlDataSetQuery<T1, T2>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1, T2>(sql, CommandType.Text, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) FromSqlDataSetQuery<T1, T2, T3>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1, T2, T3>(sql, CommandType.Text, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) FromSqlDataSetQuery<T1, T2, T3, T4>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1, T2, T3, T4>(sql, CommandType.Text, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) FromSqlDataSetQuery<T1, T2, T3, T4, T5>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1, T2, T3, T4, T5>(sql, CommandType.Text, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1, T2, T3, T4, T5, T6>(sql, CommandType.Text, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(sql, CommandType.Text, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(sql, CommandType.Text, parameters);
        }

        public virtual object FromSqlDataSetQuery(string sql, Type[] types, params object[] parameters)
        {
            return Database.SqlDataSetQuery(sql, types, CommandType.Text, parameters);
        }

        public virtual Task<IEnumerable<T1>> FromSqlDataSetQueryAsync<T1>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1>(sql, CommandType.Text, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> FromSqlDataSetQueryAsync<T1, T2>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1, T2>(sql, CommandType.Text, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> FromSqlDataSetQueryAsync<T1, T2, T3>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1, T2, T3>(sql, CommandType.Text, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> FromSqlDataSetQueryAsync<T1, T2, T3, T4>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1, T2, T3, T4>(sql, CommandType.Text, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1, T2, T3, T4, T5>(sql, CommandType.Text, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(sql, CommandType.Text, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(sql, CommandType.Text, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(sql, CommandType.Text, parameters);
        }

        public virtual Task<object> FromSqlDataSetQueryAsync(string sql, Type[] types, params object[] parameters)
        {
            return Database.SqlDataSetQueryAsync(sql, types, CommandType.Text, parameters);
        }

        public virtual IEnumerable<T1> FromSqlDataSetQuery<T1>(string sql, object parameterModel)
        {
            return FromSqlDataSetQuery<T1>(sql, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2) FromSqlDataSetQuery<T1, T2>(string sql, object parameterModel)
        {
            return FromSqlDataSetQuery<T1, T2>(sql, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) FromSqlDataSetQuery<T1, T2, T3>(string sql, object parameterModel)
        {
            return FromSqlDataSetQuery<T1, T2, T3>(sql, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) FromSqlDataSetQuery<T1, T2, T3, T4>(string sql, object parameterModel)
        {
            return FromSqlDataSetQuery<T1, T2, T3, T4>(sql, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) FromSqlDataSetQuery<T1, T2, T3, T4, T5>(string sql, object parameterModel)
        {
            return FromSqlDataSetQuery<T1, T2, T3, T4, T5>(sql, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6>(string sql, object parameterModel)
        {
            return FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6>(sql, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(string sql, object parameterModel)
        {
            return FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(sql, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, object parameterModel)
        {
            return FromSqlDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(sql, parameterModel.ToSqlParameters());
        }

        public virtual object FromSqlDataSetQuery(string sql, Type[] types, object parameterModel)
        {
            return FromSqlDataSetQuery(sql, types, parameterModel.ToSqlParameters());
        }

        public virtual Task<IEnumerable<T1>> FromSqlDataSetQueryAsync<T1>(string sql, object parameterModel)
        {
            return FromSqlDataSetQueryAsync<T1>(sql, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> FromSqlDataSetQueryAsync<T1, T2>(string sql, object parameterModel)
        {
            return FromSqlDataSetQueryAsync<T1, T2>(sql, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> FromSqlDataSetQueryAsync<T1, T2, T3>(string sql, object parameterModel)
        {
            return FromSqlDataSetQueryAsync<T1, T2, T3>(sql, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> FromSqlDataSetQueryAsync<T1, T2, T3, T4>(string sql, object parameterModel)
        {
            return FromSqlDataSetQueryAsync<T1, T2, T3, T4>(sql, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5>(string sql, object parameterModel)
        {
            return FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5>(sql, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(string sql, object parameterModel)
        {
            return FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(sql, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(string sql, object parameterModel)
        {
            return FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(sql, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, object parameterModel)
        {
            return FromSqlDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(sql, parameterModel.ToSqlParameters());
        }

        public virtual Task<object> FromSqlDataSetQueryAsync(string sql, Type[] types, object parameterModel)
        {
            return FromSqlDataSetQueryAsync(sql, types, parameterModel.ToSqlParameters());
        }
    }
}
