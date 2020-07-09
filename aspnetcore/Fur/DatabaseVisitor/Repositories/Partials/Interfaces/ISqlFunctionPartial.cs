using Fur.DatabaseVisitor.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 函数操作分部类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {

        TResult FromSqlScalarFunctionQuery<TResult>(string name, params object[] parameters) where TResult : struct;

        Task<TResult> FromSqlScalarFunctionQueryAsync<TResult>(string name, params object[] parameters) where TResult : struct;

        TResult FromSqlScalarFunctionQuery<TResult>(string name, object parameterModel) where TResult : struct;

        Task<TResult> FromSqlScalarFunctionQueryAsync<TResult>(string name, object parameterModel) where TResult : struct;

        DataTable FromSqlTableFunctionQuery(string name, params object[] parameters);

        Task<DataTable> FromSqlTableFunctionQueryAsync(string name, params object[] parameters);

        DataTable FromSqlTableFunctionQuery(string name, object parameterModel);

        Task<DataTable> FromSqlTableFunctionQueryAsync(string name, object parameterModel);

        IEnumerable<T> FromSqlTableFunctionQuery<T>(string name, params object[] parameters);

        Task<IEnumerable<T>> FromSqlTableFunctionQueryAsync<T>(string name, params object[] parameters);

        IEnumerable<T> FromSqlTableFunctionQuery<T>(string name, object parameterModel);

        Task<IEnumerable<T>> FromSqlTableFunctionQueryAsync<T>(string name, object parameterModel);
    }
}
