using Autofac;
using Castle.DynamicProxy;
using Fur.DatabaseVisitor.Contexts;
using Fur.DatabaseVisitor.Extensions.Sql;
using Fur.DatabaseVisitor.Identifiers;
using Fur.DatabaseVisitor.Tangent.Attributes;
using Fur.DatabaseVisitor.Tangent.Attributes.Basics;
using Fur.Extensions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace Fur.DatabaseVisitor.Tangent
{
    /// <summary>
    /// 切面上下文工具类
    /// </summary>
    internal static class TangentDbContextUtilities
    {
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
                return tangentMethod.DbContext.Database.SqlScalarFunction<object>(name, tangentMethod.SqlParameters);
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

            var dbContextIdentifier = tangentAttribute.DbContextIdentifier ?? typeof(FurDbContextIdentifier);
            var dbContext = lifetimeScope.ResolveNamed<DbContext>(dbContextIdentifier.Name);

            var dbContextPool = lifetimeScope.Resolve<IDbContextPool>();
            dbContextPool.SaveDbContext(dbContext);

            var sqlParameters = method.GetParameters().ToSqlParameters(invocation.Arguments);
            var returnType = tangentAttribute.SourceType ?? method.ReturnType;

            return (new TangentMethodInfo
            {
                Method = method,
                SqlParameters = sqlParameters,
                DbContext = dbContext,
                ReturnType = method.ReturnType,
                ActReturnType = returnType,
                HasSourceType = tangentAttribute.SourceType != null,
                IsValueTupleReturnType = returnType.ToString().StartsWith("System.ValueTuple"),
                ValueTupleGenericTypeArguments = returnType.GenericTypeArguments
            }
            , tangentAttribute);
        }
        #endregion
    }
}
