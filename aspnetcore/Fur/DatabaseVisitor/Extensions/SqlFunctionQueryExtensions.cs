using Fur.DatabaseVisitor.Enums;
using Fur.DatabaseVisitor.Helpers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Extensions
{
    public static class SqlFunctionQueryExtensions
    {
        public static DataTable SqlFunctionQuery(this DatabaseFacade databaseFacade, DbCanExecuteTypeOptions dbCanExecuteTypeOptions, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(dbCanExecuteTypeOptions, name, parameters);
            return databaseFacade.SqlQuery(sql, parameters);
        }

        public static IEnumerable<T> SqlFunctionQuery<T>(this DatabaseFacade databaseFacade, DbCanExecuteTypeOptions dbCanExecuteTypeOptions, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(dbCanExecuteTypeOptions, name, parameters);
            return databaseFacade.SqlQuery<T>(sql, parameters);
        }

        public static Task<DataTable> SqlFunctionQueryAsync(this DatabaseFacade databaseFacade, DbCanExecuteTypeOptions dbCanExecuteTypeOptions, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(dbCanExecuteTypeOptions, name, parameters);
            return databaseFacade.SqlQueryAsync(sql, parameters);
        }

        public static Task<IEnumerable<T>> SqlFunctionQueryAsync<T>(this DatabaseFacade databaseFacade, DbCanExecuteTypeOptions dbCanExecuteTypeOptions, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(dbCanExecuteTypeOptions, name, parameters);
            return databaseFacade.SqlQueryAsync<T>(sql, parameters);
        }

        public static DataTable SqlFunctionQuery(this DatabaseFacade databaseFacade, DbCanExecuteTypeOptions dbCanExecuteTypeOptions, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(dbCanExecuteTypeOptions, name, parameterModel);
            return databaseFacade.SqlQuery(sql, parameters);
        }

        public static IEnumerable<T> SqlFunctionQuery<T>(this DatabaseFacade databaseFacade, DbCanExecuteTypeOptions dbCanExecuteTypeOptions, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(dbCanExecuteTypeOptions, name, parameterModel);
            return databaseFacade.SqlQuery<T>(sql, parameters);
        }

        public static Task<DataTable> SqlFunctionQueryAsync(this DatabaseFacade databaseFacade, DbCanExecuteTypeOptions dbCanExecuteTypeOptions, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(dbCanExecuteTypeOptions, name, parameterModel);
            return databaseFacade.SqlQueryAsync(sql, parameters);
        }

        public static Task<IEnumerable<T>> SqlFunctionQueryAsync<T>(this DatabaseFacade databaseFacade, DbCanExecuteTypeOptions dbCanExecuteTypeOptions, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(dbCanExecuteTypeOptions, name, parameterModel);
            return databaseFacade.SqlQueryAsync<T>(sql, parameters);
        }
    }
}
