// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.17
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// Sql 执行代理类
    /// </summary>
    [SkipScan]
    public class SqlDispatchProxy : DispatchProxy, IDispatchProxy
    {
        /// <summary>
        /// 实例对象
        /// </summary>
        public object Target { get; set; }

        /// <summary>
        /// 服务提供器
        /// </summary>
        public IServiceProvider Services { get; set; }

        /// <summary>
        /// 拦截
        /// </summary>
        /// <param name="targetMethod"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            // 获取 Sql 代理方法信息
            var sqlProxyMethod = GetProxyMethod(targetMethod, args);
            return ExecuteSql(sqlProxyMethod);
        }

        /// <summary>
        /// 执行 Sql 操作
        /// </summary>
        /// <param name="sqlProxyMethod">代理方法</param>
        /// <returns></returns>
        private static object ExecuteSql(SqlProxyMethod sqlProxyMethod)
        {
            // 获取 ADO.NET 数据库操作对象
            var database = sqlProxyMethod.DbContext.Database;

            // 定义多次使用变量
            var returnType = sqlProxyMethod.ReturnType;
            var sql = sqlProxyMethod.FinalSql;
            var parameterModel = sqlProxyMethod.ParameterModel;
            var commandType = sqlProxyMethod.CommandType;
            var isAsync = sqlProxyMethod.IsAsync;

            // 处理 DataSet 返回值
            if (returnType == typeof(DataSet))
            {
                var (dataSet, _) = !isAsync
                    ? database.DataAdapterFill(sql, parameterModel, commandType)
                    : database.DataAdapterFillAsync(sql, parameterModel, commandType).GetAwaiter().GetResult();

                return !isAsync ? dataSet : Task.FromResult(dataSet);
            }
            // 处理 无返回值
            else if (returnType == typeof(void))
            {
                var (rowEffects, _) = !isAsync
                    ? database.ExecuteNonQuery(sql, parameterModel, commandType)
                    : database.ExecuteNonQueryAsync(sql, parameterModel, commandType).GetAwaiter().GetResult();

                return !isAsync ? rowEffects : Task.FromResult(rowEffects);
            }
            // 处理 元组类型 返回值
            else if (returnType.IsValueTuple())
            {
                var (dataSet, _) = !isAsync
                    ? database.DataAdapterFill(sql, parameterModel, commandType)
                    : database.DataAdapterFillAsync(sql, parameterModel, commandType).GetAwaiter().GetResult();

                var result = ConvertValueTuple(returnType, dataSet);
                return !isAsync ? result : Task.FromResult(result);
            }
            // 处理 基元类型 返回值
            else if (returnType.IsRichPrimitive())
            {
                var (result, _) = !isAsync
                    ? database.ExecuteScalar(sql, parameterModel, commandType)
                    : database.ExecuteScalarAsync(sql, parameterModel, commandType).GetAwaiter().GetResult();

                return !isAsync ? result : Task.FromResult(result);
            }
            // 处理 存储过程带输出类型 返回值
            else if (returnType == typeof(ProcedureOutputResult) || (returnType.IsGenericType && typeof(ProcedureOutputResult<>).IsAssignableFrom(returnType.GetGenericTypeDefinition())))
            {
                var (dataSet, dbParameters) = !isAsync
                    ? database.DataAdapterFill(sql, parameterModel, commandType)
                    : database.DataAdapterFillAsync(sql, parameterModel, commandType).GetAwaiter().GetResult();

                var result = ConvertProcedureOutputResult(returnType, dbParameters, dataSet);
                return !isAsync ? result : Task.FromResult(result);
            }
            else
            {
                var (dataTable, _) = !isAsync
                        ? database.ExecuteReader(sql, parameterModel, commandType)
                        : database.ExecuteReaderAsync(sql, parameterModel, commandType).GetAwaiter().GetResult();

                // 处理 DataTable 返回值
                if (returnType == typeof(DataTable)) return !isAsync ? dataTable : Task.FromResult(dataTable);
                else
                {
                    var list = dataTable.ToList(returnType);
                    var result = list.Adapt(list.GetType(), returnType);
                    return !isAsync ? result : Task.FromResult(result);
                }
            }
        }

        /// <summary>
        /// 处理元组类型返回值
        /// </summary>
        /// <param name="returnType">返回值类型</param>
        /// <param name="dataSet">数据集</param>
        /// <returns></returns>
        private static object ConvertValueTuple(Type returnType, DataSet dataSet)
        {
            var tupleList = dataSet.ToList(returnType);
            var result = tupleList.Adapt(tupleList.GetType(), returnType);
            return result;
        }

        /// <summary>
        /// 处理存储过程带输出结果
        /// </summary>
        /// <param name="returnType">返回值类型</param>
        /// <param name="parameters">命令参数</param>
        /// <param name="dataSet">数据集</param>
        /// <returns></returns>
        private static object ConvertProcedureOutputResult(Type returnType, DbParameter[] parameters, DataSet dataSet)
        {
            // 是否是泛型
            if (!returnType.IsGenericType)
            {
                return DbHelpers.WrapperProcedureOutput(parameters, dataSet);
            }
            else
            {
                var result = DbHelpers.WrapperProcedureOutput(parameters, dataSet, returnType.GenericTypeArguments.First());
                return result.Adapt(result.GetType(), returnType);
            }
        }

        /// <summary>
        /// 获取代理方法信息
        /// </summary>
        /// <param name="method">方法</param>
        /// <param name="args">参数列表</param>
        /// <returns>SqlProxyMethod</returns>
        private SqlProxyMethod GetProxyMethod(MethodInfo method, object[] args)
        {
            // 判断方法是否贴了注解
            if (!method.IsDefined(typeof(SqlProxyAttribute), true)) throw new InvalidOperationException("The method is missing the [SqlProxy] annotation");

            // 获取 Sql 代理特性
            var sqlProxyAttribute = method.GetCustomAttribute<SqlProxyAttribute>(true);

            // 判断是否是异步方法
            var isAsyncMethod = method.IsAsync();

            // 获取类型返回值并处理 Task 和 Task<T> 类型返回值
            var returnType = method.ReturnType;
            returnType = isAsyncMethod ? (returnType.GenericTypeArguments.FirstOrDefault() ?? typeof(void)) : returnType;

            // 获取数据库上下文
            var dbContext = GetDbContext(sqlProxyAttribute.DbContextLocator);

            // 转换方法参数
            var parameters = CombineDbParameter(method, args);

            // 定义最终 Sql 语句
            string finalSql;
            var commandType = CommandType.Text;

            // 如果是存储过程类型
            if (sqlProxyAttribute is SqlProcedureAttribute sqlProduceAttribute)
            {
                finalSql = sqlProduceAttribute.Name;
                commandType = CommandType.StoredProcedure;
            }
            // 如果是函数类型
            else if (sqlProxyAttribute is SqlFunctionAttribute sqlFunctionAttribute)
            {
                finalSql = DbHelpers.GenerateFunctionSql(dbContext.Database.ProviderName,
                    returnType.IsRichPrimitive() ? DbFunctionType.Scalar : DbFunctionType.Table,
                    sqlFunctionAttribute.Name,
                    parameters);
            }
            // 如果是纯Sql类型
            else if (sqlProxyAttribute is SqlExecuteAttribute sqlExecuteAttribute)
            {
                finalSql = sqlExecuteAttribute.Sql;
                commandType = sqlExecuteAttribute.CommandType;
            }
            else throw new NotSupportedException($"{sqlProxyAttribute.GetType().FullName} is an invalid annotation");

            // 返回
            return new SqlProxyMethod
            {
                ParameterModel = parameters,
                DbContext = dbContext,
                ReturnType = returnType,
                IsAsync = isAsyncMethod,
                CommandType = commandType,
                FinalSql = finalSql
            };
        }

        /// <summary>
        /// 获取数据库上下文
        /// </summary>
        /// <param name="dbContextLocator">数据库上下文定位器</param>
        /// <returns>数据库上下文</returns>
        private DbContext GetDbContext(Type dbContextLocator = null)
        {
            // 解析数据库上下文池和数据库上下文解析器
            var dbContextPool = Services.GetService<IDbContextPool>();
            var dbContextResolver = Services.GetService<Func<Type, IScoped, DbContext>>();

            // 解析数据库上下文
            var dbContext = dbContextResolver(dbContextLocator ?? typeof(MasterDbContextLocator), default);

            // 添加数据库上下文到池中
            dbContextPool.AddToPool(dbContext);

            return dbContext;
        }

        /// <summary>
        /// 创建数据库命令参数字典
        /// </summary>
        /// <param name="method"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private static object CombineDbParameter(MethodInfo method, object[] arguments)
        {
            var parameters = method.GetParameters();
            if (parameters == null || parameters.Length == 0) return null;

            // 只支持要么全是基元类型，或全部都是类类型
            if (!parameters.All(u => u.ParameterType.IsRichPrimitive()) && !parameters.All(u => u.ParameterType.IsClass))
                throw new InvalidOperationException("Invalid type cast");

            if (parameters.All(u => u.ParameterType.IsRichPrimitive()))
            {
                var dic = new Dictionary<string, object>();
                for (var i = 0; i < parameters.Length; i++)
                {
                    var parameter = parameters[i];
                    var parameterValue = arguments.Length > i ? arguments[i] : null;
                    dic.Add(parameter.Name, parameterValue);
                }
                return dic;
            }
            else return arguments.First();
        }
    }
}