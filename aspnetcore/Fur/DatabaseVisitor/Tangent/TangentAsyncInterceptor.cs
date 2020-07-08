using Autofac;
using Autofac.Extensions.DependencyInjection;
using Castle.DynamicProxy;
using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Options;
using Fur.DatabaseVisitor.Extensions;
using Fur.DatabaseVisitor.Helpers;
using Fur.DatabaseVisitor.Identifiers;
using Fur.DatabaseVisitor.Tangent.Attributes;
using Fur.DatabaseVisitor.TenantSaaS;
using Mapster;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Fur.Extensions;

namespace Fur.DatabaseVisitor.Tangent
{
    public class TangentAsyncInterceptor : IAsyncInterceptor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DbContext _dbContext;
        private readonly ITenantProvider _tenantProvider;

        public TangentAsyncInterceptor(
            IServiceProvider serviceProvider
            , DbContext dbContext
            , ITenantProvider tenantProvider)
        {
            _serviceProvider = serviceProvider;
            _dbContext = dbContext;
            _tenantProvider = tenantProvider;
        }

        public void InterceptAsynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = AsynchronousInvoke(invocation);
        }

        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            var result = AsynchronousOfTInvoke<TResult>(invocation);
            invocation.ReturnValue = result;
        }

        public void InterceptSynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = SynchronousInvoke(invocation);
        }

        // 处理异步不带返回值
        private Task AsynchronousInvoke(IInvocation invocation)
        {
            ResolveMethodInfo(invocation, out MethodInfo methodInfo, out string sql, out SqlParameter[] parameters, out Type sourceType, out DbContext whichDbContext);

            var result = whichDbContext.Database.SqlQueryAsync(sql, parameters);
            return Task.FromResult(result.Result);
        }

        // 处理异步带返回值
        private async Task<TResult> AsynchronousOfTInvoke<TResult>(IInvocation invocation)
        {
            ResolveMethodInfo(invocation, out MethodInfo methodInfo, out string sql, out SqlParameter[] parameters, out Type sourceType, out DbContext whichDbContext);

            if (sourceType != null && sourceType.ToString().StartsWith("System.Threading.Tasks.Task"))
            {
                sourceType = sourceType.GenericTypeArguments.First();
            }
            var methodActualReturnType = typeof(TResult);
            if (methodActualReturnType == typeof(DataSet))
            {
                object result = await whichDbContext.Database.SqlDataSetQueryAsync(sql, parameters);
                return (TResult)result;
            }
            else if (methodActualReturnType.ToString().StartsWith("System.ValueTuple"))
            {
                object result = await whichDbContext.Database.SqlDataSetQueryAsync(sql, (sourceType ?? methodActualReturnType).GenericTypeArguments.ToArray(), parameters);
                var defaultResult = result.Adapt(result.GetType(), sourceType ?? methodActualReturnType);
                return (TResult)(sourceType == null ? defaultResult : defaultResult?.Adapt(defaultResult.GetType(), methodActualReturnType));
            }
            else
            {
                object result = await whichDbContext.Database.SqlQueryAsync(sql, (sourceType ?? methodActualReturnType), parameters);
                if (methodActualReturnType == typeof(DataTable)) return (TResult)result;
                else
                {
                    return (TResult)result.Adapt(result.GetType(), methodActualReturnType);
                }
            }
        }

        // 处理同步
        private object SynchronousInvoke(IInvocation invocation)
        {
            ResolveMethodInfo(invocation, out MethodInfo methodInfo, out string sql, out SqlParameter[] parameters, out Type sourceType, out DbContext whichDbContext);

            var methodReturnType = methodInfo.ReturnType;

            if (methodReturnType == typeof(DataSet))
            {
                return whichDbContext.Database.SqlDataSetQuery(sql, parameters);
            }
            else if (methodReturnType.ToString().StartsWith("System.ValueTuple"))
            {
                var result = whichDbContext.Database.SqlDataSetQuery(sql, (sourceType ?? methodReturnType).GenericTypeArguments.ToArray(), parameters);

                var defaultResult = result.Adapt(result.GetType(), sourceType ?? methodReturnType);
                return sourceType == null ? defaultResult : defaultResult?.Adapt(defaultResult.GetType(), methodReturnType);
            }
            else
            {
                var result = whichDbContext.Database.SqlQuery(sql, parameters);
                if (methodReturnType == typeof(DataTable)) return result;
                else if (methodReturnType == typeof(void)) return typeof(void);
                else
                {
                    var objectResult = result.ToEnumerable(sourceType ?? methodReturnType);
                    return sourceType == null ? objectResult : objectResult?.Adapt(objectResult.GetType(), methodReturnType);
                }
            }
        }

        private void ResolveMethodInfo(IInvocation invocation, out MethodInfo methodInfo, out string sql, out SqlParameter[] parameters, out Type sourceType, out DbContext whichDbContext)
        {
            methodInfo = invocation.MethodInvocationTarget ?? invocation.Method;
            var tangentAttribute = methodInfo.GetCustomAttributes(true).FirstOrDefault(u => typeof(TangentAttribute).IsAssignableFrom(u.GetType())) as TangentAttribute
?? throw new System.Exception($"Invalid method invoke: {methodInfo.Name}.");

            (sql, parameters) = CombineSql(methodInfo, tangentAttribute, invocation.Arguments);
            sourceType = tangentAttribute.SourceType;

            var dbContextIdentifierType = tangentAttribute.DbContextIdentifier;
            if (dbContextIdentifierType != null && typeof(IDbContextIdentifier).IsAssignableFrom(dbContextIdentifierType))
            {
                whichDbContext = _serviceProvider.GetAutofacRoot().ResolveNamed<DbContext>(dbContextIdentifierType.Name);
            }
            else
            {
                whichDbContext = _dbContext;
            }
        }

        private (string sql, SqlParameter[] parameters) CombineSql(MethodInfo methodInfo, TangentAttribute tangentAttribute, object[] arguments)
        {
            var parameters = new List<SqlParameter>();

            var firstArguments = arguments?.FirstOrDefault();
            var IsPrimitiveArguments = firstArguments != null && firstArguments.GetType().IsPrimitivePlusIncludeNullable();

            string sql;
            if (tangentAttribute is DbSentenceAttribute dbSentenceAttribute)
            {
                sql = dbSentenceAttribute.Sql;

                parameters.AddRange(!IsPrimitiveArguments ? firstArguments.ToSqlParameters() : CreateSqlParameters(methodInfo, arguments));
            }
            else if (tangentAttribute is DbProcedureAttribute dbProcedureAttribute)
            {
                sql = CreateSql(methodInfo, arguments, parameters, DbCompileTypeOptions.DbProcedure, IsPrimitiveArguments, dbProcedureAttribute);
            }
            else if (tangentAttribute is DbScalarFunctionAttribute dbScalarFunctionAttribute)
            {
                sql = CreateSql(methodInfo, arguments, parameters, DbCompileTypeOptions.DbScalarFunction, IsPrimitiveArguments, dbScalarFunctionAttribute);
            }
            else if (tangentAttribute is DbTableFunctionAttribute dbTableFunctionAttribute)
            {
                sql = CreateSql(methodInfo, arguments, parameters, DbCompileTypeOptions.DbTableFunction, IsPrimitiveArguments, dbTableFunctionAttribute);
            }
            else throw new System.SystemException($"Unsupported type: {tangentAttribute.GetType().Name}.");

            return (sql, parameters.ToArray());
        }

        private string CreateSql(MethodInfo methodInfo, object[] arguments, List<SqlParameter> parameters, DbCompileTypeOptions dbCanExecuteTypeOptions, bool IsPrimitiveArguments, DbCanInvokeAttribute dbCanInvokeAttribute)
        {
            string sql;
            if (IsPrimitiveArguments)
            {
                var sqlParameters = CreateSqlParameters(methodInfo, arguments);
                sql = Helper.CombineExecuteSql(dbCanExecuteTypeOptions, dbCanInvokeAttribute.Name, sqlParameters);
                parameters.AddRange(sqlParameters);
            }
            else
            {
                var result = Helper.CombineExecuteSql(dbCanExecuteTypeOptions, dbCanInvokeAttribute.Name, arguments?.FirstOrDefault());
                sql = result.sql;
                parameters.AddRange(result.parameters);
            }

            return sql;
        }

        private SqlParameter[] CreateSqlParameters(MethodInfo methodInfo, object[] arguments)
        {
            var parameters = new List<SqlParameter>();
            var methodParameters = methodInfo.GetParameters();

            for (int i = 0; i < methodParameters.Length; i++)
            {
                // 占位符
                if (methodParameters[i].Name == nameof(DbEntityBaseOfT<int>.TenantId))
                {
                    parameters.Add(new SqlParameter(methodParameters[i].Name, _tenantProvider.GetTenantId()));
                }
                else
                {
                    parameters.Add(new SqlParameter(methodParameters[i].Name, arguments[i] ?? DBNull.Value));
                }
            }

            return parameters.ToArray();
        }
    }
}