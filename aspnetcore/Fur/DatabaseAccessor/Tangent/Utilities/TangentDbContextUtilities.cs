using Autofac;
using Castle.DynamicProxy;
using Fur.ApplicationBase.Attributes;
using Fur.DatabaseAccessor.Contexts.Pools;
using Fur.DatabaseAccessor.Extensions.Sql;
using Fur.DatabaseAccessor.Tangent.Attributes;
using Fur.DatabaseAccessor.Tangent.Attributes.Basics;
using Fur.DatabaseAccessor.Tangent.Models;
using Fur.TypeExtensions;
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
    [NonWrapper]
    internal static class TangentDbContextUtilities
    {
        #region 处理同步 + internal static object SynchronousInvoke(IInvocation invocation, ILifetimeScope lifetimeScope)

        /// <summary>
        /// 处理同步
        /// </summary>
        /// <param name="invocation">拦截器对象</param>
        /// <param name="lifetimeScope">autofac 实例对象</param>
        /// <returns>object</returns>
        internal static object SynchronousInvoke(IInvocation invocation, ILifetimeScope lifetimeScope)
        {
            var (tangentMethod, tangentAttribute) = TangentDbContextUtilities.GetTangentMethodInfo(invocation, lifetimeScope);

            if (tangentAttribute is DbQueryAttribute dbQueryAttribute)
            {
                return TangentDbContextUtilities.DbQueryExecute(tangentMethod, dbQueryAttribute);
            }
            else if (tangentAttribute is DbNonQueryAttribute dbNonQueryAttribute)
            {
                return TangentDbContextUtilities.DbNonQueryExecute(tangentMethod, dbNonQueryAttribute);
            }
            else if (tangentAttribute is Attributes.DbFunctionAttribute dbFunctionAttribute)
            {
                return TangentDbContextUtilities.DbFunctionExecute(tangentMethod, dbFunctionAttribute);
            }
            else if (tangentAttribute is DbProcedureAttribute dbProcedureAttribute)
            {
                return TangentDbContextUtilities.DbProcedureExecute(tangentMethod, dbProcedureAttribute);
            }
            else
            {
                throw new NotSupportedException($"{tangentAttribute.GetType().Name}");
            }
        }

        #endregion

        #region 处理异步 + internal static async Task<TResult> AsynchronousOfTInvoke<TResult>(IInvocation invocation, ILifetimeScope lifetimeScope)

        /// <summary>
        /// 处理异步
        /// </summary>
        /// <param name="invocation">拦截器对象</param>
        /// <param name="lifetimeScope">autofac 实例对象</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<TResult> AsynchronousOfTInvoke<TResult>(IInvocation invocation, ILifetimeScope lifetimeScope)
        {
            var (tangentMethod, tangentAttribute) = TangentDbContextUtilities.GetTangentMethodInfo(invocation, lifetimeScope);

            object result;
            if (tangentAttribute is DbQueryAttribute dbQueryAttribute)
            {
                result = await TangentDbContextUtilities.DbQueryExecuteAsync(tangentMethod, dbQueryAttribute);
            }
            else if (tangentAttribute is DbNonQueryAttribute dbNonQueryAttribute)
            {
                result = await TangentDbContextUtilities.DbNonQueryExecuteAsync(tangentMethod, dbNonQueryAttribute);
            }
            else if (tangentAttribute is Attributes.DbFunctionAttribute dbFunctionAttribute)
            {
                result = await TangentDbContextUtilities.DbFunctionExecuteAsync(tangentMethod, dbFunctionAttribute);
            }
            else if (tangentAttribute is DbProcedureAttribute dbProcedureAttribute)
            {
                result = await TangentDbContextUtilities.DbProcedureExecuteAsync(tangentMethod, dbProcedureAttribute);
            }
            else
            {
                throw new NotSupportedException($"{tangentAttribute.GetType().Name}");
            }

            return (TResult)result;
        }

        #endregion

        #region 数据库查询 + internal static object DbQueryExecute(TangentMethodInfo tangentMethod, DbQueryAttribute dbQueryAttribute)

        /// <summary>
        /// 数据库查询
        /// </summary>
        /// <param name="tangentMethod">切面方法</param>
        /// <param name="dbQueryAttribute"><see cref="DbQueryAttribute"/></param>
        /// <returns>object</returns>
        internal static object DbQueryExecute(TangentMethodInfo tangentMethod, DbQueryAttribute dbQueryAttribute)
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

        #endregion

        #region 数据库查询 + internal static async Task<object> DbQueryExecuteAsync(TangentMethodInfo tangentMethod, DbQueryAttribute dbQueryAttribute)

        /// <summary>
        /// 数据库查询
        /// </summary>
        /// <param name="tangentMethod">切面方法</param>
        /// <param name="dbQueryAttribute"><see cref="DbQueryAttribute"/></param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<object> DbQueryExecuteAsync(TangentMethodInfo tangentMethod, DbQueryAttribute dbQueryAttribute)
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

        #endregion

        #region 数据库非查询（增删改） + internal static int DbNonQueryExecute(TangentMethodInfo tangentMethod, DbNonQueryAttribute dbNonQueryAttribute)

        /// <summary>
        /// 数据库非查询（增删改）
        /// </summary>
        /// <param name="tangentMethod">切面方法</param>
        /// <param name="dbNonQueryAttribute"><see cref="DbNonQueryAttribute"/></param>
        /// <returns>int</returns>
        internal static int DbNonQueryExecute(TangentMethodInfo tangentMethod, DbNonQueryAttribute dbNonQueryAttribute)
        {
            var sql = dbNonQueryAttribute.Sql;
            return tangentMethod.DbContext.Database.SqlExecuteNonQuery(sql, CommandType.Text, tangentMethod.SqlParameters);
        }

        #endregion

        #region 数据库非查询（增删改） + internal static async Task<int> DbNonQueryExecuteAsync(TangentMethodInfo tangentMethod, DbNonQueryAttribute dbNonQueryAttribute)

        /// <summary>
        /// 数据库非查询（增删改）
        /// </summary>
        /// <param name="tangentMethod">切面方法</param>
        /// <param name="dbNonQueryAttribute"><see cref="DbNonQueryAttribute"/></param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<int> DbNonQueryExecuteAsync(TangentMethodInfo tangentMethod, DbNonQueryAttribute dbNonQueryAttribute)
        {
            var sql = dbNonQueryAttribute.Sql;
            var result = await tangentMethod.DbContext.Database.SqlExecuteNonQueryAsync(sql, CommandType.Text, tangentMethod.SqlParameters);
            return result;
        }

        #endregion

        #region 数据库函数 + internal static object DbFunctionExecute(TangentMethodInfo tangentMethod, Attributes.DbFunctionAttribute dbFunctionAttribute)

        /// <summary>
        /// 数据库函数
        /// </summary>
        /// <param name="tangentMethod">切面方法</param>
        /// <param name="dbFunctionAttribute"><see cref="Attributes.DbFunctionAttribute"/></param>
        /// <returns>int</returns>
        internal static object DbFunctionExecute(TangentMethodInfo tangentMethod, Attributes.DbFunctionAttribute dbFunctionAttribute)
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

        #endregion

        #region 数据库函数 + internal static async Task<object> DbFunctionExecuteAsync(TangentMethodInfo tangentMethod, Attributes.DbFunctionAttribute dbFunctionAttribute)

        /// <summary>
        /// 数据库函数
        /// </summary>
        /// <param name="tangentMethod">切面方法</param>
        /// <param name="dbFunctionAttribute"><see cref="Attributes.DbFunctionAttribute"/></param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<object> DbFunctionExecuteAsync(TangentMethodInfo tangentMethod, Attributes.DbFunctionAttribute dbFunctionAttribute)
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

        #endregion

        #region 数据库存储过程 + internal static object DbProcedureExecute(TangentMethodInfo tangentMethod, DbProcedureAttribute dbProcedureAttribute)

        /// <summary>
        /// 存储过程
        /// </summary>
        /// <param name="tangentMethod">切面方法</param>
        /// <param name="dbProcedureAttribute"><see cref="DbProcedureAttribute"/></param>
        /// <returns>int</returns>
        internal static object DbProcedureExecute(TangentMethodInfo tangentMethod, DbProcedureAttribute dbProcedureAttribute)
        {
            var name = dbProcedureAttribute.Name;

            if (dbProcedureAttribute.WithOutputOrReturn)
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

        #endregion

        #region 数据库存储过程 + internal static async Task<object> DbProcedureExecuteAsync(TangentMethodInfo tangentMethod, DbProcedureAttribute dbProcedureAttribute)

        /// <summary>
        /// 存储过程
        /// </summary>
        /// <param name="tangentMethod">切面方法</param>
        /// <param name="dbProcedureAttribute"><see cref="DbProcedureAttribute"/></param>
        /// <returns><see cref="Task{TResult}"/></returns>
        internal static async Task<object> DbProcedureExecuteAsync(TangentMethodInfo tangentMethod, DbProcedureAttribute dbProcedureAttribute)
        {
            var name = dbProcedureAttribute.Name;

            if (dbProcedureAttribute.WithOutputOrReturn)
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

        #endregion

        #region 获取切面方法信息类 + internal static (TangentMethodInfo tangentMethod, TangentAttribute tangentAttribute) GetTangentMethodInfo(IInvocation invocation, ILifetimeScope lifetimeScope)

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

        #endregion
    }
}