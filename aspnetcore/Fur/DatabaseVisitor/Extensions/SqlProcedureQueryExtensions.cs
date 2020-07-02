using Fur.DatabaseVisitor.Enums;
using Fur.DatabaseVisitor.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Extensions
{
    public static class SqlProcedureQueryExtensions
    {
        public static DataTable SqlProcedureQuery(this DatabaseFacade databaseFacade, string name, object[] parameters = null)
        {
            var sql = Helper.CombineExecuteSql(ExcuteSqlOptions.Procedure, name, parameters);
            return databaseFacade.SqlQuery(sql, parameters);
        }

        public static IEnumerable<T> SqlProcedureQuery<T>(this DatabaseFacade databaseFacade, string name, object[] parameters = null)
        {
            var sql = Helper.CombineExecuteSql(ExcuteSqlOptions.Procedure, name, parameters);
            return databaseFacade.SqlQuery<T>(sql, parameters);
        }

        public static Task<DataTable> SqlProcedureQueryAsync(this DatabaseFacade databaseFacade, string name, object[] parameters = null)
        {
            var sql = Helper.CombineExecuteSql(ExcuteSqlOptions.Procedure, name, parameters);
            return databaseFacade.SqlQueryAsync(sql, parameters);
        }

        public static Task<IEnumerable<T>> SqlProcedureQueryAsync<T>(this DatabaseFacade databaseFacade, string name, object[] parameters = null)
        {
            var sql = Helper.CombineExecuteSql(ExcuteSqlOptions.Procedure, name, parameters);
            return databaseFacade.SqlQueryAsync<T>(sql, parameters);
        }




        public static DataTable SqlProcedureQuery<TParameterModel>(this DatabaseFacade databaseFacade, string name, TParameterModel parameterModel) where TParameterModel : class
        {
            var (sql, parameters) = Helper.CombineExecuteSql(ExcuteSqlOptions.Procedure, name, parameterModel);
            return databaseFacade.SqlQuery(sql, parameters);
        }

        public static IEnumerable<T> SqlProcedureQuery<T, TParameterModel>(this DatabaseFacade databaseFacade, string name, TParameterModel parameterModel) where TParameterModel : class
        {
            var (sql, parameters) = Helper.CombineExecuteSql(ExcuteSqlOptions.Procedure, name, parameterModel);
            return databaseFacade.SqlQuery<T>(sql, parameters);
        }

        public static Task<DataTable> SqlProcedureQueryAsync<TParameterModel>(this DatabaseFacade databaseFacade, string name, TParameterModel parameterModel) where TParameterModel : class
        {
            var (sql, parameters) = Helper.CombineExecuteSql(ExcuteSqlOptions.Procedure, name, parameterModel);
            return databaseFacade.SqlQueryAsync(sql, parameters);
        }

        public static Task<IEnumerable<T>> SqlProcedureQueryAsync<T, TParameterModel>(this DatabaseFacade databaseFacade, string name, TParameterModel parameterModel) where TParameterModel : class
        {
            var (sql, parameters) = Helper.CombineExecuteSql(ExcuteSqlOptions.Procedure, name, parameterModel);
            return databaseFacade.SqlQueryAsync<T>(sql, parameters);
        }
    }
}
