using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Extensions;
using Fur.DatabaseVisitor.Options;
using Fur.DependencyInjection.Lifetimes;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Repositories
{
    /// <summary>
    /// 泛型仓储 函数操作 分部类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial class EFCoreRepositoryOfT<TEntity> : IRepositoryOfT<TEntity>, IScopedLifetimeOfT<TEntity> where TEntity : class, IDbEntity, new()
    {
        // 标量函数
        public virtual TResult SqlScalarFunctionQuery<TResult>(string name, params object[] parameters) where TResult : struct
        {
            return Database.SqlFunctionExecute<TResult>(DbFunctionTypeOptions.Scalar, name, parameters).FirstOrDefault();
        }

        public virtual async Task<TResult> SqlScalarFunctionQueryAsync<TResult>(string name, params object[] parameters) where TResult : struct
        {
            var results = await Database.SqlFunctionExecuteAsync<TResult>(DbFunctionTypeOptions.Scalar, name, parameters);
            return results.FirstOrDefault();
        }

        public virtual TResult SqlScalarFunctionQuery<TResult>(string name, object parameterModel) where TResult : struct
        {
            return Database.SqlFunctionExecute<TResult>(DbFunctionTypeOptions.Scalar, name, parameterModel).FirstOrDefault();
        }

        public virtual async Task<TResult> SqlScalarFunctionQueryAsync<TResult>(string name, object parameterModel) where TResult : struct
        {
            var results = await Database.SqlFunctionExecuteAsync<TResult>(DbFunctionTypeOptions.Scalar, name, parameterModel);
            return results.FirstOrDefault();
        }

        public virtual DataTable SqlTableFunctionQuery(string name, params object[] parameters)
        {
            return Database.SqlFunctionExecute(DbFunctionTypeOptions.Table, name, parameters);
        }

        public virtual Task<DataTable> SqlTableFunctionQueryAsync(string name, params object[] parameters)
        {
            return Database.SqlFunctionExecuteAsync(DbFunctionTypeOptions.Table, name, parameters);
        }

        public virtual DataTable SqlTableFunctionQuery(string name, object parameterModel)
        {
            return Database.SqlFunctionExecute(DbFunctionTypeOptions.Table, name, parameterModel);
        }

        public virtual Task<DataTable> SqlTableFunctionQueryAsync(string name, object parameterModel)
        {
            return Database.SqlFunctionExecuteAsync(DbFunctionTypeOptions.Table, name, parameterModel);
        }

        public virtual IEnumerable<T> SqlTableFunctionQuery<T>(string name, params object[] parameters)
        {
            return Database.SqlFunctionExecute<T>(DbFunctionTypeOptions.Table, name, parameters);
        }

        public virtual Task<IEnumerable<T>> SqlTableFunctionQueryAsync<T>(string name, params object[] parameters)
        {
            return Database.SqlFunctionExecuteAsync<T>(DbFunctionTypeOptions.Table, name, parameters);
        }

        public virtual IEnumerable<T> SqlTableFunctionQuery<T>(string name, object parameterModel)
        {
            return Database.SqlFunctionExecute<T>(DbFunctionTypeOptions.Table, name, parameterModel);
        }

        public virtual Task<IEnumerable<T>> SqlTableFunctionQueryAsync<T>(string name, object parameterModel)
        {
            return Database.SqlFunctionExecuteAsync<T>(DbFunctionTypeOptions.Table, name, parameterModel);
        }
    }
}
