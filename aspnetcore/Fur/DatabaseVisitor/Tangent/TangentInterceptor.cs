using Castle.DynamicProxy;
using Fur.DatabaseVisitor.Enums;
using Fur.DatabaseVisitor.Extensions;
using Fur.DatabaseVisitor.Helpers;
using Fur.DatabaseVisitor.Tangent.Attributes;
using Mapster;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Fur.DatabaseVisitor.Tangent
{
    public class TangentInterceptor : IInterceptor
    {
        private readonly DbContext _dbContext;

        public TangentInterceptor(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Intercept(IInvocation invocation)
        {
            invocation.ReturnValue = ExecuteSql(invocation);
        }

        private object ExecuteSql(IInvocation invocation)
        {
            var methodInfo = invocation.MethodInvocationTarget ?? invocation.Method;
            var tangentAttribute = methodInfo.GetCustomAttributes(true).FirstOrDefault(u => typeof(TangentAttribute).IsAssignableFrom(u.GetType())) as TangentAttribute
                ?? throw new System.Exception($"Invalid method invoke: {methodInfo.Name}.");

            var (sql, parameters) = AnalysisMethodInfoToSql(methodInfo, tangentAttribute, invocation.Arguments);

            // 处理异步和非异步（还未处理异步）
            var methodReturnType = methodInfo.ReturnType;
            var sourceType = tangentAttribute.SourceType;
            if (methodReturnType == typeof(DataSet))
            {
                return _dbContext.Database.SqlDataSetQuery(sql, parameters);
            }
            else if (methodReturnType.ToString().StartsWith("System.ValueTuple"))
            {
                var result = _dbContext.Database.SqlDataSetQuery(sql, (sourceType ?? methodReturnType).GenericTypeArguments.ToArray(), parameters);

                var defaultResult = result.Adapt(result.GetType(), sourceType ?? methodReturnType);
                return sourceType == null ? defaultResult : defaultResult?.Adapt(defaultResult.GetType(), methodReturnType);
            }
            else
            {
                var result = _dbContext.Database.SqlQuery(sql, parameters);
                if (methodReturnType == typeof(DataTable)) return result;
                else if (methodReturnType == typeof(void)) invocation.ReturnValue = typeof(void);
                else
                {
                    var objectResult = result.ToEnumerable(sourceType ?? methodReturnType);
                    return sourceType == null ? objectResult : objectResult?.Adapt(objectResult.GetType(), methodReturnType);
                }
            }

            return null;
        }

        private (string sql, SqlParameter[] parameters) AnalysisMethodInfoToSql(MethodInfo methodInfo, TangentAttribute tangentAttribute, object[] arguments)
        {
            var parameters = new List<SqlParameter>();

            var firstArguments = arguments?.FirstOrDefault();
            var IsPrimitiveArguments = firstArguments != null && Fur.AttachController.Helpers.Helper.IsPrimitive(firstArguments.GetType());

            string sql;
            if (tangentAttribute is DbSentenceAttribute dbSentenceAttribute)
            {
                sql = dbSentenceAttribute.Sql;

                parameters.AddRange(!IsPrimitiveArguments ? firstArguments.ToSqlParameters() : CreateSqlParameters(methodInfo, arguments));
            }
            else if (tangentAttribute is DbProcedureAttribute dbProcedureAttribute)
            {
                sql = CreateSql(methodInfo, arguments, parameters, DbCanExecuteTypeOptions.DbProcedure, IsPrimitiveArguments, dbProcedureAttribute);
            }
            else if (tangentAttribute is DbScalarFunctionAttribute dbScalarFunctionAttribute)
            {
                sql = CreateSql(methodInfo, arguments, parameters, DbCanExecuteTypeOptions.DbScalarFunction, IsPrimitiveArguments, dbScalarFunctionAttribute);
            }
            else if (tangentAttribute is DbTableFunctionAttribute dbTableFunctionAttribute)
            {
                sql = CreateSql(methodInfo, arguments, parameters, DbCanExecuteTypeOptions.DbTableFunction, IsPrimitiveArguments, dbTableFunctionAttribute);
            }
            else throw new System.SystemException($"Unsupported type: {tangentAttribute.GetType().Name}.");

            return (sql, parameters.ToArray());
        }

        private string CreateSql(MethodInfo methodInfo, object[] arguments, List<SqlParameter> parameters, DbCanExecuteTypeOptions dbCanExecuteTypeOptions, bool IsPrimitiveArguments, DbCanInvokeAttribute dbCanInvokeAttribute)
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
                parameters.Add(new SqlParameter(methodParameters[i].Name, arguments[i] ?? DBNull.Value));
            }

            return parameters.ToArray();
        }
    }
}
