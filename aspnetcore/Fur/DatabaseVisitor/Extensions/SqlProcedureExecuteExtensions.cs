using Fur.DatabaseVisitor.Options;
using Fur.DatabaseVisitor.Helpers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;

namespace Fur.DatabaseVisitor.Extensions
{
    public static class SqlProcedureExecuteExtensions
    {
        public static DataTable SqlProcedureQuery(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlExecute(sql, parameters);
        }

        public static IEnumerable<T> SqlProcedureQuery<T>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlExecute<T>(sql, parameters);
        }

        public static Task<DataTable> SqlProcedureQueryAsync(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlExecuteAsync(sql, parameters);
        }

        public static Task<IEnumerable<T>> SqlProcedureQueryAsync<T>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlExecuteAsync<T>(sql, parameters);
        }

        public static DataTable SqlProcedureQuery(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlExecute(sql, parameters);
        }

        public static IEnumerable<T> SqlProcedureQuery<T>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlExecute<T>(sql, parameters);
        }

        public static Task<DataTable> SqlProcedureQueryAsync(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlExecuteAsync(sql, parameters);
        }

        public static Task<IEnumerable<T>> SqlProcedureQueryAsync<T>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlExecuteAsync<T>(sql, parameters);
        }

        public static DataSet SqlProcedureDataSetQuery(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecute(sql, parameters);
        }

        public static Task<DataSet> SqlProcedureDataSetQueryAsync(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecuteAsync(sql, parameters);
        }

        public static DataSet SqlProcedureDataSetQuery(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecute(sql, parameters);
        }

        public static Task<DataSet> SqlProcedureDataSetQueryAsync(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecuteAsync(sql, parameters);
        }

        public static IEnumerable<T1> SqlProcedureDataSetQuery<T1>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecute<T1>(sql, parameters);
        }

        public static (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlProcedureDataSetQuery<T1, T2>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecute<T1, T2>(sql, parameters);
        }

        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlProcedureDataSetQuery<T1, T2, T3>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3>(sql, parameters);
        }

        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlProcedureDataSetQuery<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3, T4>(sql, parameters);
        }

        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3, T4, T5>(sql, parameters);
        }

        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3, T4, T5, T6>(sql, parameters);
        }

        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3, T4, T5, T6, T7>(sql, parameters);
        }

        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3, T4, T5, T6, T7, T8>(sql, parameters);
        }

        public static object SqlProcedureDataSetQuery(this DatabaseFacade databaseFacade, string name, Type[] types, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecute(sql, parameters, types, parameters);
        }

        public static Task<IEnumerable<T1>> SqlProcedureDataSetQueryAsync<T1>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecuteAsync<T1>(sql, parameters);
        }

        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlProcedureDataSetQueryAsync<T1, T2>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2>(sql, parameters);
        }

        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlProcedureDataSetQueryAsync<T1, T2, T3>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3>(sql, parameters);
        }

        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3, T4>(sql, parameters);
        }

        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3, T4, T5>(sql, parameters);
        }

        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3, T4, T5, T6>(sql, parameters);
        }

        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7>(sql, parameters);
        }

        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7, T8>(sql, parameters);
        }

        public static Task<object> SqlProcedureDataSetQueryAsync(this DatabaseFacade databaseFacade, string name, Type[] types, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameters);
            return databaseFacade.SqlDataSetExecuteAsync(sql, types, parameters);
        }

        public static IEnumerable<T1> SqlProcedureDataSetQuery<T1>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecute<T1>(sql, parameters);
        }

        public static (IEnumerable<T1> data1, IEnumerable<T2> data2) SqlProcedureDataSetQuery<T1, T2>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecute<T1, T2>(sql, parameters);
        }

        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3) SqlProcedureDataSetQuery<T1, T2, T3>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3>(sql, parameters);
        }

        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4) SqlProcedureDataSetQuery<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3, T4>(sql, parameters);
        }

        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3, T4, T5>(sql, parameters);
        }

        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3, T4, T5, T6>(sql, parameters);
        }

        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3, T4, T5, T6, T7>(sql, parameters);
        }

        public static (IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8) SqlProcedureDataSetQuery<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecute<T1, T2, T3, T4, T5, T6, T7, T8>(sql, parameters);
        }

        public static object SqlProcedureDataSetQuery(this DatabaseFacade databaseFacade, string name, Type[] types, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecute(sql, parameters, types, parameters);
        }

        public static Task<IEnumerable<T1>> SqlProcedureDataSetQueryAsync<T1>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecuteAsync<T1>(sql, parameters);
        }

        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2)> SqlProcedureDataSetQueryAsync<T1, T2>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2>(sql, parameters);
        }

        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3)> SqlProcedureDataSetQueryAsync<T1, T2, T3>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3>(sql, parameters);
        }

        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3, T4>(sql, parameters);
        }

        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3, T4, T5>(sql, parameters);
        }

        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3, T4, T5, T6>(sql, parameters);
        }

        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7>(sql, parameters);
        }

        public static Task<(IEnumerable<T1> data1, IEnumerable<T2> data2, IEnumerable<T3> data3, IEnumerable<T4> data4, IEnumerable<T5> data5, IEnumerable<T6> data6, IEnumerable<T7> data7, IEnumerable<T8> data8)> SqlProcedureDataSetQueryAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this DatabaseFacade databaseFacade, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecuteAsync<T1, T2, T3, T4, T5, T6, T7, T8>(sql, parameters);
        }

        public static Task<object> SqlProcedureDataSetQueryAsync(this DatabaseFacade databaseFacade, string name, Type[] types, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(DbCompileTypeOptions.DbProcedure, name, parameterModel);
            return databaseFacade.SqlDataSetExecuteAsync(sql, types, parameters);
        }

        public static (Dictionary<string, object> outputValues, object returnValue) SqlProcedureRepayQuery(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            return databaseFacade.SqlRepayExecute(sql, parameters);
        }

        public static Task<(Dictionary<string, object> outputValues, object returnValue)> SqlProcedureRepayQueryAsync(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            return databaseFacade.SqlRepayExecuteAsync(sql, parameters);
        }
    }
}