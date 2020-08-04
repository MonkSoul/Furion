using Autofac;
using Castle.DynamicProxy;
using Fur.AppCore.Attributes;
using Fur.DatabaseAccessor.Attributes;
using Fur.DatabaseAccessor.Contexts;
using Fur.DatabaseAccessor.Extensions;
using Fur.DatabaseAccessor.Tangent.Models;
using Fur.Extensions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor.Tangent.Utilities
{
    /// <summary>
    /// 切面上下文工具类
    /// </summary>
    [NonInflated]
    internal static class TangentDbContextUtilities
    {
        /// <summary>
        /// 处理同步
        /// </summary>
        /// <param name="invocation">拦截器对象</param>
        /// <param name="lifetimeScope">autofac 实例对象</param>
        /// <returns>object</returns>
        internal static object SynchronousInvoke(IInvocation invocation, ILifetimeScope lifetimeScope)
        {
            var (tangentMethod, tangentAttribute) = GetTangentMethodInfo(invocation, lifetimeScope);

            if (tangentAttribute is SqlQueryAttribute dbQueryAttribute)
            {
                return DbQueryExecute(tangentMethod, dbQueryAttribute);
            }
            else if (tangentAttribute is SqlNonQueryAttribute dbNonQueryAttribute)
            {
                return DbNonQueryExecute(tangentMethod, dbNonQueryAttribute);
            }
            else if (tangentAttribute is Attributes.SqlFunctionAttribute dbFunctionAttribute)
            {
                return DbFunctionExecute(tangentMethod, dbFunctionAttribute);
            }
            else if (tangentAttribute is SqlProcedureAttribute dbProcedureAttribute)
            {
                return DbProcedureExecute(tangentMethod, dbProcedureAttribute);
            }
            else
            {
                throw new NotSupportedException($"{tangentAttribute.GetType().Name}");
            }
        }

        /// <summary>
        /// 处理异步
        /// </summary>
        /// <param name="invocation">拦截器对象</param>
        /// <param name="lifetimeScope">autofac 实例对象</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<TResult> AsynchronousInvoke<TResult>(IInvocation invocation, ILifetimeScope lifetimeScope)
        {
            var (tangentMethod, tangentAttribute) = GetTangentMethodInfo(invocation, lifetimeScope);

            object result;
            if (tangentAttribute is SqlQueryAttribute dbQueryAttribute)
            {
                result = await DbQueryExecuteAsync(tangentMethod, dbQueryAttribute);
            }
            else if (tangentAttribute is SqlNonQueryAttribute dbNonQueryAttribute)
            {
                result = await DbNonQueryExecuteAsync(tangentMethod, dbNonQueryAttribute);
            }
            else if (tangentAttribute is SqlFunctionAttribute dbFunctionAttribute)
            {
                result = await DbFunctionExecuteAsync(tangentMethod, dbFunctionAttribute);
            }
            else if (tangentAttribute is SqlProcedureAttribute dbProcedureAttribute)
            {
                result = await DbProcedureExecuteAsync(tangentMethod, dbProcedureAttribute);
            }
            else
            {
                throw new NotSupportedException($"{tangentAttribute.GetType().Name}");
            }

            return (TResult)result;
        }

        /// <summary>
        /// 数据库查询
        /// </summary>
        /// <param name="tangentMethod">切面方法</param>
        /// <param name="dbQueryAttribute"><see cref="SqlQueryAttribute"/></param>
        /// <returns>object</returns>
        internal static object DbQueryExecute(TangentMethodInfo tangentMethod, SqlQueryAttribute dbQueryAttribute)
        {
            var sql = dbQueryAttribute.Sql;
            if (tangentMethod.ActReturnType == typeof(DataSet))
            {
                return tangentMethod.DbContext.Database.SqlDataAdapterFill(sql, CommandType.Text, tangentMethod.SqlParameters);
            }
            else if (tangentMethod.IsValueTupleReturnType)
            {
                object actResult = tangentMethod.DbContext.Database.SqlDataSet(sql, tangentMethod.ValueTupleGenericTypeArguments, CommandType.Text, tangentMethod.SqlParameters);
                if (!tangentMethod.HasSourceType)
                {
                    return actResult.Adapt(actResult.GetType(), tangentMethod.ReturnType);
                }
                else
                {
                    var valueTupeResult = actResult.Adapt(actResult.GetType(), tangentMethod.ActReturnType);
                    return valueTupeResult.Adapt(valueTupeResult.GetType(), tangentMethod.ReturnType);
                }
            }
            else
            {
                if (tangentMethod.ActReturnType.IsPrimitivePlusIncludeNullable())
                {
                    var result = tangentMethod.DbContext.Database.SqlExecuteScalar(sql, CommandType.Text, tangentMethod.SqlParameters);
                    return result.Adapt(result.GetType(), tangentMethod.ReturnType);
                }
                else
                {
                    var actResult = tangentMethod.DbContext.Database.SqlExecuteReader(sql, CommandType.Text, tangentMethod.SqlParameters);
                    if (tangentMethod.ActReturnType == typeof(DataTable)) return actResult;
                    else if (tangentMethod.ActReturnType == typeof(void)) return tangentMethod.ReturnType;
                    else
                    {
                        var classResult = actResult.ToList(tangentMethod.ActReturnType);
                        return classResult.Adapt(classResult.GetType(), tangentMethod.ReturnType);
                    }
                }
            }
        }

        /// <summary>
        /// 数据库查询
        /// </summary>
        /// <param name="tangentMethod">切面方法</param>
        /// <param name="dbQueryAttribute"><see cref="SqlQueryAttribute"/></param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<object> DbQueryExecuteAsync(TangentMethodInfo tangentMethod, SqlQueryAttribute dbQueryAttribute)
        {
            var sql = dbQueryAttribute.Sql;
            if (tangentMethod.ActReturnType == typeof(DataSet))
            {
                var result = await tangentMethod.DbContext.Database.SqlDataAdapterFillAsync(sql, CommandType.Text, tangentMethod.SqlParameters);
                return result;
            }
            else if (tangentMethod.IsValueTupleReturnType)
            {
                object actResult = await tangentMethod.DbContext.Database.SqlDataSetAsync(sql, tangentMethod.ValueTupleGenericTypeArguments, CommandType.Text, tangentMethod.SqlParameters);
                if (!tangentMethod.HasSourceType)
                {
                    return actResult.Adapt(actResult.GetType(), tangentMethod.ReturnType);
                }
                else
                {
                    var valueTupeResult = actResult.Adapt(actResult.GetType(), tangentMethod.ActReturnType);
                    return valueTupeResult.Adapt(valueTupeResult.GetType(), tangentMethod.ReturnType);
                }
            }
            else
            {
                if (tangentMethod.ActReturnType.IsPrimitivePlusIncludeNullable())
                {
                    var result = await tangentMethod.DbContext.Database.SqlExecuteScalarAsync(sql, CommandType.Text, tangentMethod.SqlParameters);
                    return result.Adapt(result.GetType(), tangentMethod.ReturnType);
                }
                else
                {
                    var actResult = await tangentMethod.DbContext.Database.SqlExecuteReaderAsync(sql, CommandType.Text, tangentMethod.SqlParameters);
                    if (tangentMethod.ActReturnType == typeof(DataTable)) return actResult;
                    else if (tangentMethod.ActReturnType == typeof(void)) return tangentMethod.ReturnType;
                    else
                    {
                        var classResult = actResult.ToList(tangentMethod.ActReturnType);
                        return classResult.Adapt(classResult.GetType(), tangentMethod.ReturnType);
                    }
                }
            }
        }

        /// <summary>
        /// 数据库非查询（增删改）
        /// </summary>
        /// <param name="tangentMethod">切面方法</param>
        /// <param name="dbNonQueryAttribute"><see cref="SqlNonQueryAttribute"/></param>
        /// <returns>int</returns>
        internal static int DbNonQueryExecute(TangentMethodInfo tangentMethod, SqlNonQueryAttribute dbNonQueryAttribute)
        {
            var sql = dbNonQueryAttribute.Sql;
            return tangentMethod.DbContext.Database.SqlExecuteNonQuery(sql, CommandType.Text, tangentMethod.SqlParameters);
        }

        /// <summary>
        /// 数据库非查询（增删改）
        /// </summary>
        /// <param name="tangentMethod">切面方法</param>
        /// <param name="dbNonQueryAttribute"><see cref="SqlNonQueryAttribute"/></param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<int> DbNonQueryExecuteAsync(TangentMethodInfo tangentMethod, SqlNonQueryAttribute dbNonQueryAttribute)
        {
            var sql = dbNonQueryAttribute.Sql;
            var result = await tangentMethod.DbContext.Database.SqlExecuteNonQueryAsync(sql, CommandType.Text, tangentMethod.SqlParameters);
            return result;
        }

        /// <summary>
        /// 数据库函数
        /// </summary>
        /// <param name="tangentMethod">切面方法</param>
        /// <param name="dbFunctionAttribute"><see cref="Attributes.SqlFunctionAttribute"/></param>
        /// <returns>int</returns>
        internal static object DbFunctionExecute(TangentMethodInfo tangentMethod, Attributes.SqlFunctionAttribute dbFunctionAttribute)
        {
            var name = dbFunctionAttribute.Name;
            if (tangentMethod.ActReturnType.IsPrimitivePlusIncludeNullable())
            {
                return tangentMethod.DbContext.Database.SqlScalarFunction(name, tangentMethod.SqlParameters);
            }
            else
            {
                var actResult = tangentMethod.DbContext.Database.SqlTableFunction(name, tangentMethod.SqlParameters);
                if (tangentMethod.ActReturnType == typeof(DataTable)) return actResult;
                else if (tangentMethod.ActReturnType == typeof(void)) return tangentMethod.ReturnType;
                else
                {
                    var classResult = actResult.ToList(tangentMethod.ActReturnType);
                    return classResult.Adapt(classResult.GetType(), tangentMethod.ReturnType);
                }
            }
        }

        /// <summary>
        /// 数据库函数
        /// </summary>
        /// <param name="tangentMethod">切面方法</param>
        /// <param name="dbFunctionAttribute"><see cref="Attributes.SqlFunctionAttribute"/></param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<object> DbFunctionExecuteAsync(TangentMethodInfo tangentMethod, Attributes.SqlFunctionAttribute dbFunctionAttribute)
        {
            var name = dbFunctionAttribute.Name;
            if (tangentMethod.ActReturnType.IsPrimitivePlusIncludeNullable())
            {
                var result = await tangentMethod.DbContext.Database.SqlScalarFunctionAsync(name, tangentMethod.SqlParameters);
                return result;
            }
            else
            {
                var actResult = await tangentMethod.DbContext.Database.SqlTableFunctionAsync(name, tangentMethod.SqlParameters);
                if (tangentMethod.ActReturnType == typeof(DataTable)) return actResult;
                else if (tangentMethod.ActReturnType == typeof(void)) return tangentMethod.ReturnType;
                else
                {
                    var classResult = actResult.ToList(tangentMethod.ActReturnType);
                    return classResult.Adapt(classResult.GetType(), tangentMethod.ReturnType);
                }
            }
        }

        /// <summary>
        /// 存储过程
        /// </summary>
        /// <param name="tangentMethod">切面方法</param>
        /// <param name="dbProcedureAttribute"><see cref="SqlProcedureAttribute"/></param>
        /// <returns>int</returns>
        internal static object DbProcedureExecute(TangentMethodInfo tangentMethod, SqlProcedureAttribute dbProcedureAttribute)
        {
            var name = dbProcedureAttribute.Name;

            if (dbProcedureAttribute.HasFeedback)
            {
                if (tangentMethod.ActReturnType == typeof(void)) return tangentMethod.ReturnType;
                {
                    return tangentMethod.DbContext.Database.SqlProcedureNonQuery(name, tangentMethod.SqlParameters);
                }
            }

            if (tangentMethod.ActReturnType == typeof(DataSet))
            {
                return tangentMethod.DbContext.Database.SqlProcedureDataSet(name, tangentMethod.SqlParameters);
            }
            else if (tangentMethod.IsValueTupleReturnType)
            {
                object actResult = tangentMethod.DbContext.Database.SqlProcedureDataSet(name, tangentMethod.ValueTupleGenericTypeArguments, tangentMethod.SqlParameters);
                if (!tangentMethod.HasSourceType)
                {
                    return actResult.Adapt(actResult.GetType(), tangentMethod.ReturnType);
                }
                else
                {
                    var valueTupeResult = actResult.Adapt(actResult.GetType(), tangentMethod.ActReturnType);
                    return valueTupeResult.Adapt(valueTupeResult.GetType(), tangentMethod.ReturnType);
                }
            }
            else
            {
                var actResult = tangentMethod.DbContext.Database.SqlProcedure(name, tangentMethod.SqlParameters);
                if (tangentMethod.ActReturnType == typeof(DataTable)) return actResult;
                else if (tangentMethod.ActReturnType == typeof(void)) return tangentMethod.ReturnType;
                else
                {
                    var classResult = actResult.ToList(tangentMethod.ActReturnType);
                    return classResult.Adapt(classResult.GetType(), tangentMethod.ReturnType);
                }
            }
        }

        /// <summary>
        /// 存储过程
        /// </summary>
        /// <param name="tangentMethod">切面方法</param>
        /// <param name="dbProcedureAttribute"><see cref="SqlProcedureAttribute"/></param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<object> DbProcedureExecuteAsync(TangentMethodInfo tangentMethod, SqlProcedureAttribute dbProcedureAttribute)
        {
            var name = dbProcedureAttribute.Name;

            if (dbProcedureAttribute.HasFeedback)
            {
                if (tangentMethod.ActReturnType == typeof(void)) return tangentMethod.ReturnType;
                {
                    var result = await tangentMethod.DbContext.Database.SqlProcedureNonQueryAsync(name, tangentMethod.SqlParameters);
                    return result;
                }
            }

            if (tangentMethod.ActReturnType == typeof(DataSet))
            {
                var result = await tangentMethod.DbContext.Database.SqlProcedureDataSetAsync(name, tangentMethod.SqlParameters);
                return result;
            }
            else if (tangentMethod.IsValueTupleReturnType)
            {
                object actResult = await tangentMethod.DbContext.Database.SqlProcedureDataSetAsync(name, tangentMethod.ValueTupleGenericTypeArguments, tangentMethod.SqlParameters);
                if (!tangentMethod.HasSourceType)
                {
                    return actResult.Adapt(actResult.GetType(), tangentMethod.ReturnType);
                }
                else
                {
                    var valueTupeResult = actResult.Adapt(actResult.GetType(), tangentMethod.ActReturnType);
                    return valueTupeResult.Adapt(valueTupeResult.GetType(), tangentMethod.ReturnType);
                }
            }
            else
            {
                var actResult = await tangentMethod.DbContext.Database.SqlProcedureAsync(name, tangentMethod.SqlParameters);
                if (tangentMethod.ActReturnType == typeof(DataTable)) return actResult;
                else if (tangentMethod.ActReturnType == typeof(void)) return tangentMethod.ReturnType;
                else
                {
                    var classResult = actResult.ToList(tangentMethod.ActReturnType);
                    return classResult.Adapt(classResult.GetType(), tangentMethod.ReturnType);
                }
            }
        }

        /// <summary>
        /// 获取切面方法信息类
        /// </summary>
        /// <param name="invocation">拦截器对象</param>
        /// <param name="lifetimeScope">autofac生命周期对象</param>
        /// <returns></returns>
        internal static (TangentMethodInfo tangentMethod, TangentAttribute tangentAttribute) GetTangentMethodInfo(IInvocation invocation, ILifetimeScope lifetimeScope)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;

            if (!method.IsDefined(typeof(TangentAttribute), true)) throw new InvalidExpressionException($"{method.Name}");

            var tangentAttribute = method.GetCustomAttribute<TangentAttribute>(true);

            var dbContextLocator = tangentAttribute.DbContextLocator;
            var dbContext = dbContextLocator != null ? lifetimeScope.ResolveNamed<DbContext>(dbContextLocator.Name) : lifetimeScope.Resolve<DbContext>();

            var dbContextPool = lifetimeScope.Resolve<IDbContextPool>();
            dbContextPool.SaveDbContext(dbContext);

            var sqlParameters = method.GetParameters().ToSqlParameters(invocation.Arguments);
            var actReturnType = tangentAttribute.DbExecuteType ?? method.ReturnType;
            // 处理 Task 类型
            if (actReturnType.ToString().StartsWith("System.Threading.Tasks.Task"))
            {
                if (actReturnType.GenericTypeArguments.Length > 0)
                {
                    actReturnType = actReturnType.GenericTypeArguments.First();
                }
                else
                {
                    actReturnType = typeof(void);
                }
            }

            var returnType = method.ReturnType;
            // 处理 Task 类型
            if (returnType.ToString().StartsWith("System.Threading.Tasks.Task"))
            {
                if (returnType.GenericTypeArguments.Length > 0)
                {
                    returnType = returnType.GenericTypeArguments.First();
                }
                else
                {
                    returnType = typeof(void);
                }
            }

            return (new TangentMethodInfo
            {
                Method = method,
                SqlParameters = sqlParameters,
                DbContext = dbContext,
                ReturnType = returnType,
                ActReturnType = actReturnType,
                HasSourceType = tangentAttribute.DbExecuteType != null,
                IsValueTupleReturnType = actReturnType.ToString().StartsWith("System.ValueTuple"),
                ValueTupleGenericTypeArguments = actReturnType.GenericTypeArguments
            }
            , tangentAttribute);
        }
    }
}