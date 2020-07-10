using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Extensions;
using Fur.DependencyInjection.Lifetimes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 泛型仓储 存储过程操作 分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial class EFCoreRepositoryOfT<TEntity> : IRepositoryOfT<TEntity>, IScopedLifetimeOfT<TEntity> where TEntity : class, IDbEntity, new()
    {

        public virtual DataTable SqlProcedureQuery(string name, params object[] parameters)
        {
            return Database.SqlProcedureExecute(name, parameters);
        }

        public virtual Task<DataTable> SqlProcedureQueryAsync(string name, params object[] parameters)
        {
            return Database.SqlProcedureExecuteAsync(name, parameters);
        }

        public virtual DataTable SqlProcedureQuery(string name, object parameterModel)
        {
            return Database.SqlProcedureExecute(name, parameterModel.ToSqlParameters());
        }

        public virtual Task<DataTable> SqlProcedureQueryAsync(string name, object parameterModel)
        {
            return Database.SqlProcedureExecuteAsync(name, parameterModel.ToSqlParameters());
        }

        public virtual IEnumerable<T> SqlProcedureQuery<T>(string name, params object[] parameters)
        {
            return Database.SqlProcedureExecute<T>(name, parameters);
        }

        public virtual Task<IEnumerable<T>> SqlProcedureQueryAsync<T>(string name, params object[] parameters)
        {
            return Database.SqlProcedureExecuteAsync<T>(name, parameters);
        }

        public virtual IEnumerable<T> SqlProcedureQuery<T>(string name, object parameterModel)
        {
            return Database.SqlProcedureExecute<T>(name, parameterModel.ToSqlParameters());
        }

        public virtual Task<IEnumerable<T>> SqlProcedureQueryAsync<T>(string name, object parameterModel)
        {
            return Database.SqlProcedureExecuteAsync<T>(name, parameterModel.ToSqlParameters());
        }

        public virtual DataSet SqlProcedureDataSetQuery(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecute(name, parameters);
        }

        public virtual Task<DataSet> SqlProcedureDataSetQueryAsync(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecuteAsync(name, parameters);
        }

        public virtual DataSet SqlProcedureDataSetQuery(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecute(name, parameterModel.ToString());
        }

        public virtual Task<DataSet> SqlProcedureDataSetQueryAsync(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecuteAsync(name, parameterModel.ToSqlParameters());
        }


        public virtual (Dictionary<string, object> outputValues, object returnValue) SqlProcedureJustExecute(string name, params object[] parameters)
        {
            return Database.SqlProcedureJustExecute(name, parameters);
        }

        public virtual Task<(Dictionary<string, object> outputValues, object returnValue)> SqlProcedureJustExecuteAsync(string name, params object[] parameters)
        {
            return Database.SqlProcedureJustExecuteAsync(name, parameters);
        }

        public virtual (Dictionary<string, object> outputValues, object returnValue) SqlProcedureJustExecute(string name, object parameterModel)
        {
            return Database.SqlProcedureJustExecute(name, parameterModel.ToSqlParameters());
        }

        public virtual Task<(Dictionary<string, object> outputValues, object returnValue)> SqlProcedureJustExecuteAsync(string name, object parameterModel)
        {
            return Database.SqlProcedureJustExecuteAsync(name, parameterModel.ToSqlParameters());
        }


        public virtual IEnumerable<T1> SqlProcedureDataSetQuery<T1>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecute<T1>(name, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlProcedureDataSetQuery<T1, T2>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2>(name, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlProcedureDataSetQuery<T1, T2, T3>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3>(name, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlProcedureDataSetQuery<T1, T2, T3, T4>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3, T4>(name, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3, T4, T5>(name, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6>(name, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6, T7>(name, parameters);
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6, T7, T8>(name, parameters);
        }

        public virtual object SqlProcedureDataSetQuery(string name, Type[] returnTypes, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecute(name, returnTypes, parameters);
        }

        public virtual Task<IEnumerable<T1>> SqlProcedureDataSetQueryAsync<T1>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1>(name, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlProcedureDataSetQueryAsync<T1, T2>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2>(name, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlProcedureDataSetQueryAsync<T1, T2, T3>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3>(name, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4>(name, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5>(name, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6>(name, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7>(name, parameters);
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string name, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7, T8>(name, parameters);
        }

        public virtual Task<object> SqlProcedureDataSetQueryAsync(string name, Type[] returnTypes, params object[] parameters)
        {
            return Database.SqlProcedureDataSetExecuteAsync(name, returnTypes, parameters);
        }

        public virtual IEnumerable<T1> SqlProcedureDataSetQuery<T1>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecute<T1>(name, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlProcedureDataSetQuery<T1, T2>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2>(name, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlProcedureDataSetQuery<T1, T2, T3>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3>(name, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlProcedureDataSetQuery<T1, T2, T3, T4>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3, T4>(name, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3, T4, T5>(name, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6>(name, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6, T7>(name, parameterModel.ToSqlParameters());
        }

        public virtual (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecute<T1, T2, T3, T4, T5, T6, T7, T8>(name, parameterModel.ToSqlParameters());
        }

        public virtual object SqlProcedureDataSetQuery(string name, Type[] returnTypes, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecute(name, returnTypes, parameterModel).ToSqlParameters();
        }

        public virtual Task<IEnumerable<T1>> SqlProcedureDataSetQueryAsync<T1>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1>(name, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlProcedureDataSetQueryAsync<T1, T2>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2>(name, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlProcedureDataSetQueryAsync<T1, T2, T3>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3>(name, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4>(name, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5>(name, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6>(name, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7>(name, parameterModel.ToSqlParameters());
        }

        public virtual Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string name, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7, T8>(name, parameterModel.ToSqlParameters());
        }

        public virtual Task<object> SqlProcedureDataSetQueryAsync(string name, Type[] returnTypes, object parameterModel)
        {
            return Database.SqlProcedureDataSetExecuteAsync(name, returnTypes, parameterModel.ToSqlParameters());
        }


    }
}
