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
using Mapster;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Fur.DatabaseAccessor
{
    [NonBeScan]
    internal static class DbHelpers
    {
        /// <summary>
        /// 创建数据库命令参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="dbParameterAttribute">参数特性</param>
        /// <returns>SqlParameter</returns>
        internal static SqlParameter CreateSqlParameter(string name, object value, DbParameterAttribute dbParameterAttribute)
        {
            // 设置参数方向
            var sqlParameter = new SqlParameter(name, value) { Direction = dbParameterAttribute.Direction };

            // 设置参数数据库类型
            if (dbParameterAttribute.DbType != null)
            {
                var type = dbParameterAttribute.DbType.GetType();
                if (type.IsEnum && typeof(SqlDbType).IsAssignableFrom(type))
                {
                    sqlParameter.SqlDbType = (SqlDbType)dbParameterAttribute.DbType;
                }
            }
            // 设置大小，解决NVarchar，Varchar 问题
            if (dbParameterAttribute.Size > 0)
            {
                sqlParameter.Size = dbParameterAttribute.Size;
            }

            return sqlParameter;
        }

        /// <summary>
        /// 生成函数执行 sql 语句
        /// </summary>
        /// <param name="providerName">ADO.NET 数据库对象</param>
        /// <param name="dbFunctionType">函数类型</param>
        /// <param name="funcName">函数名词</param>
        /// <param name="parameters">函数参数</param>
        /// <returns>sql 语句</returns>
        internal static string CombineFunctionSql(string providerName, DbFunctionType dbFunctionType, string funcName, params SqlParameter[] parameters)
        {
            // 检查是否支持函数
            DatabaseProvider.CheckFunctionSupported(providerName, dbFunctionType);

            parameters ??= Array.Empty<SqlParameter>();

            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"SELECT{(dbFunctionType == DbFunctionType.Table ? " * FROM" : "")} {funcName}(");

            // 生成函数参数
            for (var i = 0; i < parameters.Length; i++)
            {
                var sqlParameter = parameters[i];

                // 添加 @ 前缀参数
                stringBuilder.Append($"@{(sqlParameter.ParameterName.Replace("@", ""))}");

                // 处理最后一个参数逗号
                if (i != parameters.Length - 1)
                {
                    stringBuilder.Append(", ");
                }
            }
            stringBuilder.Append("); ");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 生成函数执行 sql 语句
        /// </summary>
        ///<param name="providerName">ADO.NET 数据库对象</param>
        /// <param name="dbFunctionType">函数类型</param>
        /// <param name="funcName">函数名词</param>
        /// <param name="model">参数模型</param>
        /// <returns>(string sql, SqlParameter[] parameters)</returns>
        internal static (string sql, SqlParameter[] parameters) CombineFunctionSql(string providerName, DbFunctionType dbFunctionType, string funcName, object model)
        {
            // 检查是否支持函数
            DatabaseProvider.CheckFunctionSupported(providerName, dbFunctionType);

            // 获取模型所有公开的属性
            var properities = model == null
                ? Array.Empty<PropertyInfo>()
                : model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var parameters = new List<SqlParameter>();
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"SELECT{(dbFunctionType == DbFunctionType.Table ? " * FROM" : "")} {funcName}(");

            for (var i = 0; i < properities.Length; i++)
            {
                var property = properities[i];
                var propertyValue = property.GetValue(model) ?? DBNull.Value;

                stringBuilder.Append($"@{property.Name}");

                // 处理最后一个参数逗号
                if (i != properities.Length - 1)
                {
                    stringBuilder.Append(", ");
                }

                // 判断属性是否贴有 [DbParameter] 特性
                if (property.IsDefined(typeof(DbParameterAttribute), true))
                {
                    var dbParameterAttribute = property.GetCustomAttribute<DbParameterAttribute>(true);
                    parameters.Add(DbHelpers.CreateSqlParameter(property.Name, propertyValue, dbParameterAttribute));
                    continue;
                }
                parameters.Add(new SqlParameter(property.Name, propertyValue));
            }

            stringBuilder.Append("); ");

            return (stringBuilder.ToString(), parameters.ToArray());
        }

        /// <summary>
        /// 包裹存储过程返回结果集
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <param name="dataSet">数据集</param>
        /// <returns>ProcedureOutput</returns>
        internal static ProcedureOutputResult WrapperProcedureOutput(SqlParameter[] parameters, DataSet dataSet)
        {
            // 读取输出返回值
            ReadOuputValue(parameters, out var outputValues, out var returnValue);

            return new ProcedureOutputResult
            {
                Result = dataSet,
                OutputValues = outputValues,
                ReturnValue = returnValue
            };
        }

        /// <summary>
        /// 包裹存储过程返回结果集
        /// </summary>
        /// <typeparam name="TResult">数据集结果</typeparam>
        /// <param name="parameters">命令参数</param>
        /// <param name="dataSet">数据集</param>
        /// <returns>ProcedureOutput</returns>
        internal static ProcedureOutputResult<TResult> WrapperProcedureOutput<TResult>(SqlParameter[] parameters, DataSet dataSet)
        {
            // 读取输出返回值
            ReadOuputValue(parameters, out var outputValues, out var returnValue);

            return new ProcedureOutputResult<TResult>
            {
                Result = dataSet.ToList(typeof(TResult)).Adapt<TResult>(),
                OutputValues = outputValues,
                ReturnValue = returnValue
            };
        }

        /// <summary>
        /// 包裹存储过程返回结果集
        /// </summary>
        /// <param name="parameters">命令参数</param>
        /// <param name="dataSet">数据集</param>
        /// <param name="type">返回类型</param>
        /// <returns>ProcedureOutput</returns>
        internal static object WrapperProcedureOutput(SqlParameter[] parameters, DataSet dataSet, Type type)
        {
            // 读取输出返回值
            ReadOuputValue(parameters, out var outputValues, out var returnValue);

            var procedureOutputResult = Activator.CreateInstance<ProcedureOutputResult<object>>();
            procedureOutputResult.Result = dataSet.ToList(type);
            procedureOutputResult.OutputValues = outputValues;
            procedureOutputResult.ReturnValue = returnValue;

            return procedureOutputResult;
        }

        /// <summary>
        /// 读取输出返回值
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <param name="outputValues">输出参数</param>
        /// <param name="returnValue">返回值</param>
        private static void ReadOuputValue(SqlParameter[] parameters, out List<ProcedureOutputValue> outputValues, out object returnValue)
        {
            // 查询所有OUTPUT值
            outputValues = parameters
               .Where(u => u.Direction == ParameterDirection.Output)
               .Select(u => new ProcedureOutputValue
               {
                   Name = u.ParameterName.Replace("@", ""),
                   Value = u.Value
               }).ToList();

            // 查询返回值
            returnValue = parameters.FirstOrDefault(u => u.Direction == ParameterDirection.ReturnValue)?.Value;
        }
    }
}