// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using Fur.Extensions;
using Fur.FriendlyException;
using Mapster;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// Sql 执行代理类
    /// </summary>
    [NonBeScan]
    public class SqlDispatchProxy : DispatchProxy, IDispatchProxy
    {
        /// <summary>
        /// 服务提供器
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

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
            var parameters = sqlProxyMethod.SqlParameters;
            var commandType = sqlProxyMethod.CommandType;
            var isAsync = sqlProxyMethod.IsAsync;

            // 处理 DataSet 返回值
            if (returnType == typeof(DataSet))
            {
                return !isAsync
                    ? database.DataAdapterFill(sql, parameters, commandType)
                    : database.DataAdapterFillAsync(sql, parameters, commandType);
            }
            // 处理 无返回值
            else if (returnType == typeof(void))
            {
                return !isAsync
                    ? database.ExecuteNonQuery(sql, parameters, commandType)
                    : database.ExecuteNonQueryAsync(sql, parameters, commandType);
            }
            // 处理 元组类型 返回值
            else if (returnType.IsValueTuple())
            {
                var dataSet = !isAsync
                    ? database.DataAdapterFill(sql, parameters, commandType)
                    : database.DataAdapterFillAsync(sql, parameters, commandType).GetAwaiter().GetResult();

                var result = ConvertValueTuple(returnType, dataSet);
                return !isAsync ? result : Task.FromResult(result);
            }
            // 处理 基元类型 返回值
            else if (returnType.IsRichPrimitive())
            {
                return !isAsync
                    ? database.ExecuteScalar(sql, parameters, commandType)
                    : database.ExecuteScalarAsync(sql, parameters, commandType);
            }
            // 处理 存储过程带输出类型 返回值
            else if (returnType == typeof(ProcedureOutputResult) || (returnType.IsGenericType && typeof(ProcedureOutputResult<>).IsAssignableFrom(returnType.GetGenericTypeDefinition())))
            {
                var dataSet = !isAsync
                    ? database.DataAdapterFill(sql, parameters, commandType)
                    : database.DataAdapterFillAsync(sql, parameters, commandType).GetAwaiter().GetResult();

                var result = ConvertProcedureOutputResult(returnType, parameters, dataSet);
                return !isAsync ? result : Task.FromResult(result);
            }
            else
            {
                var dataTable = !isAsync
                        ? database.ExecuteReader(sql, parameters, commandType)
                        : database.ExecuteReaderAsync(sql, parameters, commandType).GetAwaiter().GetResult();

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
        private static object ConvertProcedureOutputResult(Type returnType, SqlParameter[] parameters, DataSet dataSet)
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
            if (!method.IsDefined(typeof(SqlProxyAttribute), true)) throw Oops.Oh("The method is missing the [SqlProxy] annotation", typeof(InvalidOperationException));

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
            var parameters = method.GetParameters().ToSqlParameters(args);

            // 定义最终 Sql 语句
            string finalSql;
            var commandType = CommandType.Text;

            // 如果是存储过程类型
            if (sqlProxyAttribute is SqlProduceAttribute sqlProduceAttribute)
            {
                finalSql = sqlProduceAttribute.Name;
                commandType = CommandType.StoredProcedure;
            }
            // 如果是函数类型
            else if (sqlProxyAttribute is SqlFunctionAttribute sqlFunctionAttribute)
            {
                finalSql = DbHelpers.CombineFunctionSql(dbContext.Database.ProviderName,
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
            else throw Oops.Oh($"{sqlProxyAttribute.GetType().FullName} is an invalid annotation", typeof(NotSupportedException));

            // 返回
            return new SqlProxyMethod
            {
                SqlParameters = parameters,
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
            var dbContextPool = ServiceProvider.GetService<IDbContextPool>();
            var dbContextResolver = ServiceProvider.GetService<Func<Type, DbContext>>();

            // 解析数据库上下文
            var dbContext = dbContextResolver(dbContextLocator ?? typeof(DbContextLocator));

            // 添加数据库上下文到池中
            dbContextPool.AddToPool(dbContext);

            return dbContext;
        }
    }
}