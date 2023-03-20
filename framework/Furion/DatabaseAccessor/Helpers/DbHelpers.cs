// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Furion.ClayObject;
using Furion.Extensions;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 数据库帮助类
/// </summary>
internal static class DbHelpers
{
    /// <summary>
    /// 参数匹配正则表达式
    /// </summary>
    internal const string ParamRegex = "[@:](?<param>[a-zA-Z][a-zA-Z0-9_]*)";

    /// <summary>
    /// 将模型转为 DbParameter 集合
    /// </summary>
    /// <param name="model">参数模型</param>
    /// <param name="dbCommand">数据库命令对象</param>
    /// <returns></returns>
    internal static DbParameter[] ConvertToDbParameters(object model, DbCommand dbCommand)
    {
        var modelType = model?.GetType();

        // 处理 JsonElement 类型
        if (model is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.Object) return ConvertToDbParameters((Dictionary<string, object>)jsonElement.ToObject(), dbCommand);

        // 处理 Clay 类型
        if (model is Clay clay) return ConvertToDbParameters((Dictionary<string, object>)clay.ToDictionary(), dbCommand);

        // 处理字典类型参数
        if (modelType == typeof(Dictionary<string, object>)) return ConvertToDbParameters((Dictionary<string, object>)model, dbCommand);

        var dbParameters = new List<DbParameter>();
        if (model == null || !modelType.IsClass) return dbParameters.ToArray();

        // 获取所有公开实例属性
        var properties = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        if (properties.Length == 0) return dbParameters.ToArray();

        // 检查 Sql 语句中参数个数
        CheckSqlParamsCount(dbCommand, out var isMatch, out var paramNames);

        // 如果命令参数都没有，则不用生成，存储过程直接返回 true
        if (!isMatch) return dbParameters.ToArray();

        // 判断是否是存储过程，如果是，则跳过检查参数数量
        var isStoredProcedure = dbCommand.CommandType == CommandType.StoredProcedure;

        // 遍历所有属性
        foreach (var property in properties)
        {
            // 如果不包含该命令参数且不是执行存储过程，则跳过
            if (!isStoredProcedure && !paramNames.Contains(property.Name, StringComparer.OrdinalIgnoreCase))
            {
                continue;
            }

            var propertyValue = property.GetValue(model) ?? DBNull.Value;

            // 创建命令参数
            var dbParameter = dbCommand.CreateParameter();

            // 判断属性是否贴有 [DbParameter] 特性
            if (property.IsDefined(typeof(DbParameterAttribute), true))
            {
                var dbParameterAttribute = property.GetCustomAttribute<DbParameterAttribute>(true);
                dbParameters.Add(ConfigureDbParameter(property.Name, propertyValue, dbParameterAttribute, dbParameter));
                continue;
            }

            dbParameter.ParameterName = property.Name;
            dbParameter.Value = propertyValue is JsonElement propertyJsonElement
                                    && propertyJsonElement.ValueKind != JsonValueKind.Object
                                    && propertyJsonElement.ValueKind != JsonValueKind.Array
                                ? propertyJsonElement.ToObject()
                                : propertyValue;    // 解决 object/json 类型值

            dbParameters.Add(dbParameter);
        }

        return dbParameters.ToArray();
    }

    /// <summary>
    /// 将字典转换成命令参数
    /// </summary>
    /// <param name="keyValues">字典</param>
    /// <param name="dbCommand">数据库命令对象</param>
    /// <returns></returns>
    internal static DbParameter[] ConvertToDbParameters(Dictionary<string, object> keyValues, DbCommand dbCommand)
    {
        var dbParameters = new List<DbParameter>();
        if (keyValues == null || keyValues.Count == 0) return dbParameters.ToArray();

        // 检查 Sql 语句中参数个数
        CheckSqlParamsCount(dbCommand, out var isMatch, out var paramNames);

        // 如果命令参数都没有，则不用生成，存储过程直接返回 true
        if (!isMatch) return dbParameters.ToArray();

        // 判断是否是存储过程，如果是，则跳过检查参数数量
        var isStoredProcedure = dbCommand.CommandType == CommandType.StoredProcedure;

        foreach (var key in keyValues.Keys)
        {
            // 如果不包含该命令参数且不是执行存储过程，则跳过
            if (!isStoredProcedure && !paramNames.Contains(key, StringComparer.OrdinalIgnoreCase))
            {
                continue;
            }

            var value = keyValues[key] ?? DBNull.Value;

            // 创建命令参数
            var dbParameter = dbCommand.CreateParameter();
            dbParameter.ParameterName = key;
            dbParameter.Value = value;
            dbParameters.Add(dbParameter);
        }

        return dbParameters.ToArray();
    }

    /// <summary>
    /// 配置数据库命令参数
    /// </summary>
    /// <param name="name">参数名</param>
    /// <param name="value">参数值</param>
    /// <param name="dbParameterAttribute">参数特性</param>
    /// <param name="dbParameter">数据库命令参数</param>
    /// <returns>DbParameter</returns>
    internal static DbParameter ConfigureDbParameter(string name, object value, DbParameterAttribute dbParameterAttribute, DbParameter dbParameter)
    {
        // 设置参数方向
        dbParameter.ParameterName = name;
        dbParameter.Value = value;
        dbParameter.Direction = dbParameterAttribute.Direction;

        // 设置参数数据库类型
        if (dbParameterAttribute.DbType != null)
        {
            var type = dbParameterAttribute.DbType.GetType();
            if (type.IsEnum)
            {
                // 处理通用 DbType 类型
                if (typeof(DbType).IsAssignableFrom(type)) dbParameter.DbType = (DbType)dbParameterAttribute.DbType;

                // 解决 Oracle 数据库游标类型参数
                if (type.FullName.Equals("Oracle.ManagedDataAccess.Client.OracleDbType", StringComparison.OrdinalIgnoreCase))
                {
                    dbParameter.GetType().GetProperty("OracleDbType")?.SetValue(dbParameter, dbParameterAttribute.DbType);
                }
            }
        }

        // 设置大小，解决NVarchar，Varchar 问题
        if (dbParameterAttribute.Size > 0)
        {
            dbParameter.Size = dbParameterAttribute.Size;
        }

        return dbParameter;
    }

    /// <summary>
    /// 生成函数执行 sql 语句
    /// </summary>
    /// <param name="providerName">ADO.NET 数据库对象</param>
    /// <param name="dbFunctionType">函数类型</param>
    /// <param name="funcName">函数名词</param>
    /// <param name="parameters">函数参数</param>
    /// <returns>sql 语句</returns>
    internal static string GenerateFunctionSql(string providerName, DbFunctionType dbFunctionType, string funcName, params DbParameter[] parameters)
    {
        // 检查是否支持函数
        DbProvider.CheckFunctionSupported(providerName, dbFunctionType);

        // 生成数据库表值函数 sql
        return GenerateDbFunctionSql(providerName, dbFunctionType, funcName, parameters.Select(u => u.ParameterName).ToArray());
    }

    /// <summary>
    /// 生成函数执行 sql 语句
    /// </summary>
    ///<param name="providerName">ADO.NET 数据库对象</param>
    /// <param name="dbFunctionType">函数类型</param>
    /// <param name="funcName">函数名词</param>
    /// <param name="model">参数模型</param>
    /// <returns>(string sql, DbParameter[] parameters)</returns>
    internal static string GenerateFunctionSql(string providerName, DbFunctionType dbFunctionType, string funcName, object model)
    {
        // 检查是否支持函数
        DbProvider.CheckFunctionSupported(providerName, dbFunctionType);

        var modelType = model?.GetType();
        // 处理字典类型参数
        if (modelType == typeof(Dictionary<string, object>)) return GenerateFunctionSql(providerName, dbFunctionType, funcName, (Dictionary<string, object>)model);

        // 获取模型所有公开的属性
        var properities = model == null
            ? Array.Empty<PropertyInfo>()
            : modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // 生成数据库表值函数 sql
        return GenerateDbFunctionSql(providerName, dbFunctionType, funcName, properities.Select(u => u.Name).ToArray());
    }

    /// <summary>
    /// 生成函数执行 sql 语句
    /// </summary>
    ///<param name="providerName">ADO.NET 数据库对象</param>
    /// <param name="dbFunctionType">函数类型</param>
    /// <param name="funcName">函数名词</param>
    /// <param name="keyValues">字典类型参数</param>
    /// <returns></returns>
    internal static string GenerateFunctionSql(string providerName, DbFunctionType dbFunctionType, string funcName, Dictionary<string, object> keyValues)
    {
        // 检查是否支持函数
        DbProvider.CheckFunctionSupported(providerName, dbFunctionType);

        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"SELECT{(dbFunctionType == DbFunctionType.Table ? " * FROM" : "")} {funcName}(");

        if (keyValues != null && keyValues.Count > 0)
        {
            var i = 0;
            foreach (var key in keyValues.Keys)
            {
                stringBuilder.Append(FixSqlParameterPlaceholder(providerName, key));

                // 处理最后一个参数逗号
                if (i != keyValues.Count - 1)
                {
                    stringBuilder.Append(", ");
                }
                i++;
            }
        }

        stringBuilder.Append("); ");

        return stringBuilder.ToString();
    }

    /// <summary>
    /// 包裹存储过程返回结果集
    /// </summary>
    /// <param name="providerName"></param>
    /// <param name="parameters">命令参数</param>
    /// <param name="dataSet">数据集</param>
    /// <returns>ProcedureOutput</returns>
    internal static ProcedureOutputResult WrapperProcedureOutput(string providerName, DbParameter[] parameters, DataSet dataSet)
    {
        // 读取输出返回值
        ReadOuputValue(providerName, parameters, out var outputValues, out var returnValue);

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
    /// <param name="providerName"></param>
    /// <param name="parameters">命令参数</param>
    /// <param name="dataSet">数据集</param>
    /// <returns>ProcedureOutput</returns>
    internal static ProcedureOutputResult<TResult> WrapperProcedureOutput<TResult>(string providerName, DbParameter[] parameters, DataSet dataSet)
    {
        // 读取输出返回值
        ReadOuputValue(providerName, parameters, out var outputValues, out var returnValue);

        return new ProcedureOutputResult<TResult>
        {
            Result = (TResult)dataSet.ToValueTuple(typeof(TResult)),
            OutputValues = outputValues,
            ReturnValue = returnValue
        };
    }

    /// <summary>
    /// 包裹存储过程返回结果集
    /// </summary>
    /// <param name="providerName"></param>
    /// <param name="parameters">命令参数</param>
    /// <param name="dataSet">数据集</param>
    /// <param name="type">返回类型</param>
    /// <returns>ProcedureOutput</returns>
    internal static object WrapperProcedureOutput(string providerName, DbParameter[] parameters, DataSet dataSet, Type type)
    {
        var wrapperProcedureOutputMethod = typeof(DbHelpers)
                .GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
                .First(u => u.Name == "WrapperProcedureOutput" && u.IsGenericMethod)
                .MakeGenericMethod(type);

        return wrapperProcedureOutputMethod.Invoke(null, new object[] { providerName, parameters, dataSet });
    }

    /// <summary>
    /// 数据没找到异常
    /// </summary>
    /// <returns></returns>
    internal static InvalidOperationException DataNotFoundException()
    {
        return new InvalidOperationException("Sequence contains no elements.");
    }

    /// <summary>
    /// 修正不同数据库命令参数前缀不一致问题
    /// </summary>
    /// <param name="providerName"></param>
    /// <param name="parameterName"></param>
    /// <param name="isFixed"></param>
    /// <returns></returns>
    internal static string FixSqlParameterPlaceholder(string providerName, string parameterName, bool isFixed = true)
    {
        // 纠正 Oracle 和 DM 数据库 SQL 命令参数
        var placeholder = !DbProvider.IsDatabaseFor(providerName, DbProvider.Oracle)
            && !DbProvider.IsDatabaseFor(providerName, DbProvider.Dm) ? "@" : ":";
        if (parameterName.StartsWith("@") || parameterName.StartsWith(":"))
        {
            parameterName = parameterName[1..];
        }

        return isFixed ? placeholder + parameterName : parameterName;
    }

    /// <summary>
    /// 读取输出返回值
    /// </summary>
    /// <param name="providerName"></param>
    /// <param name="parameters">参数</param>
    /// <param name="outputValues">输出参数</param>
    /// <param name="returnValue">返回值</param>
    private static void ReadOuputValue(string providerName, DbParameter[] parameters, out IEnumerable<ProcedureOutputValue> outputValues, out object returnValue)
    {
        // 查询所有OUTPUT值
        outputValues = parameters
           .Where(u => u.Direction == ParameterDirection.Output)
           .Select(u => new ProcedureOutputValue
           {
               Name = FixSqlParameterPlaceholder(providerName, u.ParameterName, false),
               Value = u.Value
           });

        // 查询返回值
        returnValue = parameters.FirstOrDefault(u => u.Direction == ParameterDirection.ReturnValue)?.Value;
    }

    /// <summary>
    /// 生存表值函数 sql
    /// </summary>
    /// <param name="providerName"></param>
    /// <param name="dbFunctionType"></param>
    /// <param name="funcName"></param>
    /// <param name="parameterNames"></param>
    /// <returns></returns>
    private static string GenerateDbFunctionSql(string providerName, DbFunctionType dbFunctionType, string funcName, string[] parameterNames)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append($"SELECT{(dbFunctionType == DbFunctionType.Table ? " * FROM" : "")} {funcName}(");

        for (var i = 0; i < parameterNames.Length; i++)
        {
            var propertyName = parameterNames[i];

            stringBuilder.Append(FixSqlParameterPlaceholder(providerName, propertyName));

            // 处理最后一个参数逗号
            if (i != parameterNames.Length - 1)
            {
                stringBuilder.Append(", ");
            }
        }

        stringBuilder.Append("); ");

        return stringBuilder.ToString();
    }

    /// <summary>
    /// 检查 Sql 中命令参数个数
    /// </summary>
    /// <remarks>如果是存储过程，则返回 true</remarks>
    /// <param name="dbCommand"></param>
    /// <param name="isMatch"></param>
    /// <param name="paramNames"></param>
    private static void CheckSqlParamsCount(DbCommand dbCommand, out bool isMatch, out string[] paramNames)
    {
        // 存储过程排除参数数量校验，函数无需排除，因为函数最终是生成 SQL 去执行
        if (dbCommand.CommandType == CommandType.StoredProcedure)
        {
            isMatch = true;
            paramNames = Array.Empty<string>();
            return;
        }

        // 处理参数不对等问题，Orache 数据库要求参数必须和 sql 标注的一致的数量，错误代码：ORA-01006
        var regex = new Regex(ParamRegex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
        isMatch = regex.IsMatch(dbCommand.CommandText);
        paramNames = !isMatch
            ? Array.Empty<string>()
            : regex.Matches(dbCommand.CommandText).Select(u => u.Groups["param"].Value).ToArray();
    }
}