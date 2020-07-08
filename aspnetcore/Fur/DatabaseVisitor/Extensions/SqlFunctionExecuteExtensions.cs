using Fur.DatabaseVisitor.Options;
using Fur.DatabaseVisitor.Helpers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Extensions
{
    public static class SqlFunctionExecuteExtensions
    {
        public static DataTable SqlFunctionQuery(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCanExecuteTypeOptions, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(dbCanExecuteTypeOptions, name, parameters);
            return databaseFacade.SqlExecute(sql, parameters);
        }

        public static IEnumerable<T> SqlFunctionQuery<T>(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCanExecuteTypeOptions, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(dbCanExecuteTypeOptions, name, parameters);
            return databaseFacade.SqlExecute<T>(sql, parameters);
        }

        public static Task<DataTable> SqlFunctionQueryAsync(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCanExecuteTypeOptions, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(dbCanExecuteTypeOptions, name, parameters);
            return databaseFacade.SqlExecuteAsync(sql, parameters);
        }

        public static Task<IEnumerable<T>> SqlFunctionQueryAsync<T>(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCanExecuteTypeOptions, string name, params object[] parameters)
        {
            var sql = Helper.CombineExecuteSql(dbCanExecuteTypeOptions, name, parameters);
            return databaseFacade.SqlExecuteAsync<T>(sql, parameters);
        }

        public static DataTable SqlFunctionQuery(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCanExecuteTypeOptions, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(dbCanExecuteTypeOptions, name, parameterModel);
            return databaseFacade.SqlExecute(sql, parameters);
        }

        public static IEnumerable<T> SqlFunctionQuery<T>(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCanExecuteTypeOptions, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(dbCanExecuteTypeOptions, name, parameterModel);
            return databaseFacade.SqlExecute<T>(sql, parameters);
        }

        public static Task<DataTable> SqlFunctionQueryAsync(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCanExecuteTypeOptions, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(dbCanExecuteTypeOptions, name, parameterModel);
            return databaseFacade.SqlExecuteAsync(sql, parameters);
        }

        public static Task<IEnumerable<T>> SqlFunctionQueryAsync<T>(this DatabaseFacade databaseFacade, DbCompileTypeOptions dbCanExecuteTypeOptions, string name, object parameterModel)
        {
            var (sql, parameters) = Helper.CombineExecuteSql(dbCanExecuteTypeOptions, name, parameterModel);
            return databaseFacade.SqlExecuteAsync<T>(sql, parameters);
        }
    }
}