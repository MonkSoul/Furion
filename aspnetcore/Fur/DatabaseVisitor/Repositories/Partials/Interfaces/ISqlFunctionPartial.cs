using Fur.DatabaseVisitor.Entities;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 泛型仓储 函数操作 分部接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial interface IRepositoryOfT<TEntity> where TEntity : class, IDbEntity, new()
    {

        TResult SqlScalarFunctionQuery<TResult>(string name, params object[] parameters) where TResult : struct;

        Task<TResult> SqlScalarFunctionQueryAsync<TResult>(string name, params object[] parameters) where TResult : struct;

        TResult SqlScalarFunctionQuery<TResult>(string name, object parameterModel) where TResult : struct;

        Task<TResult> SqlScalarFunctionQueryAsync<TResult>(string name, object parameterModel) where TResult : struct;

        DataTable SqlTableFunctionQuery(string name, params object[] parameters);

        Task<DataTable> SqlTableFunctionQueryAsync(string name, params object[] parameters);

        DataTable SqlTableFunctionQuery(string name, object parameterModel);

        Task<DataTable> SqlTableFunctionQueryAsync(string name, object parameterModel);

        IEnumerable<T> SqlTableFunctionQuery<T>(string name, params object[] parameters);

        Task<IEnumerable<T>> SqlTableFunctionQueryAsync<T>(string name, params object[] parameters);

        IEnumerable<T> SqlTableFunctionQuery<T>(string name, object parameterModel);

        Task<IEnumerable<T>> SqlTableFunctionQueryAsync<T>(string name, object parameterModel);
    }
}
