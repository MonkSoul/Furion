using Furion.DependencyInjection;
using Furion.Extensions;
using Furion.Reflection;
using Furion.Templates.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// Sql 执行代理类
    /// </summary>
    [SkipScan]
    public class SqlDispatchProxy : AspectDispatchProxy, IDispatchProxy
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
        /// 拦截同步方法
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public override object Invoke(MethodInfo method, object[] args)
        {
            // 获取 Sql 代理方法信息
            var sqlProxyMethod = GetProxyMethod(method, args);
            return ExecuteSql(sqlProxyMethod);
        }

        /// <summary>
        /// 拦截异步方法
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public override async Task InvokeAsync(MethodInfo method, object[] args)
        {
            // 获取 Sql 代理方法信息
            var sqlProxyMethod = GetProxyMethod(method, args);
            await ExecuteSqlAsync(sqlProxyMethod);
        }

        /// <summary>
        /// 拦截异步带返回值方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public override async Task<T> InvokeAsyncT<T>(MethodInfo method, object[] args)
        {
            // 获取 Sql 代理方法信息
            var sqlProxyMethod = GetProxyMethod(method, args);
            return await ExecuteSqlOfTAsync<T>(sqlProxyMethod);
        }

        /// <summary>
        /// 执行 Sql 操作
        /// </summary>
        /// <param name="sqlProxyMethod">代理方法</param>
        /// <returns></returns>
        private static object ExecuteSql(SqlProxyMethod sqlProxyMethod)
        {
            // 获取 ADO.NET 数据库操作对象
            var database = sqlProxyMethod.Context.Database;

            // 定义多次使用变量
            var returnType = sqlProxyMethod.ReturnType;
            var sql = sqlProxyMethod.FinalSql;
            var parameterModel = sqlProxyMethod.ParameterModel;
            var commandType = sqlProxyMethod.CommandType;

            // 处理 DataSet 返回值
            if (returnType == typeof(DataSet))
            {
                var (dataSet, _) = database.DataAdapterFill(sql, parameterModel, commandType);
                return dataSet;
            }
            // 处理 无返回值
            else if (returnType == typeof(void))
            {
                var (rowEffects, _) = database.ExecuteNonQuery(sql, parameterModel, commandType);
                return rowEffects;
            }
            // 处理 元组类型 返回值
            else if (returnType.IsValueTuple())
            {
                var (dataSet, _) = database.DataAdapterFill(sql, parameterModel, commandType);
                var result = dataSet.ToValueTuple(returnType);
                return result;
            }
            // 处理 基元类型 返回值
            else if (returnType.IsRichPrimitive())
            {
                var (result, _) = database.ExecuteScalar(sql, parameterModel, commandType);
                return result.ChangeType(returnType);
            }
            // 处理 存储过程带输出类型 返回值
            else if (returnType == typeof(ProcedureOutputResult) || (returnType.IsGenericType && typeof(ProcedureOutputResult<>).IsAssignableFrom(returnType.GetGenericTypeDefinition())))
            {
                var (dataSet, dbParameters) = database.DataAdapterFill(sql, parameterModel, commandType);

                // 处理返回值
                var result = !returnType.IsGenericType
                    ? DbHelpers.WrapperProcedureOutput(database.ProviderName, dbParameters, dataSet)
                    : DbHelpers.WrapperProcedureOutput(database.ProviderName, dbParameters, dataSet, returnType.GenericTypeArguments.First());
                return result;
            }
            else
            {
                var (dataTable, _) = database.ExecuteReader(sql, parameterModel, commandType);

                // 处理 DataTable 返回值
                if (returnType == typeof(DataTable)) return dataTable;
                else
                {
                    var list = dataTable.ToList(returnType);
                    return list;
                }
            }
        }

        /// <summary>
        /// 执行 Sql 操作
        /// </summary>
        /// <param name="sqlProxyMethod">代理方法</param>
        /// <returns></returns>
        private static async Task ExecuteSqlAsync(SqlProxyMethod sqlProxyMethod)
        {
            // 获取 ADO.NET 数据库操作对象
            var database = sqlProxyMethod.Context.Database;

            // 定义多次使用变量
            var sql = sqlProxyMethod.FinalSql;
            var parameterModel = sqlProxyMethod.ParameterModel;
            var commandType = sqlProxyMethod.CommandType;

            _ = await database.ExecuteNonQueryAsync(sql, parameterModel, commandType);
        }

        /// <summary>
        /// 执行 Sql 操作
        /// </summary>
        /// <param name="sqlProxyMethod">代理方法</param>
        /// <returns></returns>
        private static async Task<T> ExecuteSqlOfTAsync<T>(SqlProxyMethod sqlProxyMethod)
        {
            // 获取 ADO.NET 数据库操作对象
            var database = sqlProxyMethod.Context.Database;

            // 定义多次使用变量
            var returnType = sqlProxyMethod.ReturnType;
            var sql = sqlProxyMethod.FinalSql;
            var parameterModel = sqlProxyMethod.ParameterModel;
            var commandType = sqlProxyMethod.CommandType;

            // 处理 DataSet 返回值
            if (returnType == typeof(DataSet))
            {
                var (dataSet, _) = await database.DataAdapterFillAsync(sql, parameterModel, commandType);
                return (T)(dataSet as object);
            }
            // 处理 元组类型 返回值
            else if (returnType.IsValueTuple())
            {
                var (dataSet, _) = await database.DataAdapterFillAsync(sql, parameterModel, commandType);
                var result = dataSet.ToValueTuple(returnType);

                return (T)result;
            }
            // 处理 基元类型 返回值
            else if (returnType.IsRichPrimitive())
            {
                var (result, _) = await database.ExecuteScalarAsync(sql, parameterModel, commandType);
                return (T)result;
            }
            // 处理 存储过程带输出类型 返回值
            else if (returnType == typeof(ProcedureOutputResult) || (returnType.IsGenericType && typeof(ProcedureOutputResult<>).IsAssignableFrom(returnType.GetGenericTypeDefinition())))
            {
                var (dataSet, dbParameters) = await database.DataAdapterFillAsync(sql, parameterModel, commandType);

                // 处理返回值
                var result = !returnType.IsGenericType
                    ? DbHelpers.WrapperProcedureOutput(database.ProviderName, dbParameters, dataSet)
                    : DbHelpers.WrapperProcedureOutput(database.ProviderName, dbParameters, dataSet, returnType.GenericTypeArguments.First());

                return (T)result;
            }
            else
            {
                var (dataTable, _) = await database.ExecuteReaderAsync(sql, parameterModel, commandType);

                // 处理 DataTable 返回值
                if (returnType == typeof(DataTable)) return (T)(dataTable as object);
                else
                {
                    var list = await dataTable.ToListAsync(returnType);
                    return (T)list;
                }
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
            if (!method.IsDefined(typeof(SqlProxyAttribute), true)) throw new InvalidOperationException("The method is missing the [SqlProxy] annotation.");

            // 获取 Sql 代理特性
            var sqlProxyAttribute = method.GetCustomAttribute<SqlProxyAttribute>(true);

            // 获取方法真实返回值类型
            var returnType = method.GetMethodRealReturnType();

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
            else throw new NotSupportedException($"{sqlProxyAttribute.GetType().FullName} is an invalid annotation.");

            // 解析方法参数及参数值并渲染模板
            var methodParameterInfos = method.GetParameters().Select((u, i) => new MethodParameterInfo
            {
                Parameter = u,
                Name = u.Name,
                Value = args[i]
            });
            // 渲染模板
            finalSql = finalSql.Render(methodParameterInfos.ToDictionary(u => u.Name, u => u.Value));

            // 返回
            return new SqlProxyMethod
            {
                ParameterModel = parameters,
                Context = dbContext,
                ReturnType = returnType,
                IsAsync = method.IsAsync(),
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
            // 解析数据库上下文解析器
            var dbContextResolver = Services.GetService<Func<Type, IScoped, DbContext>>();

            // 解析数据库上下文
            var dbContext = dbContextResolver(dbContextLocator ?? typeof(MasterDbContextLocator), default);

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
                throw new InvalidOperationException("Invalid type cast.");

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