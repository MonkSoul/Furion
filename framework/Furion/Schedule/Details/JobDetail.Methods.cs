// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

using Furion.Templates;
using System.Collections.Concurrent;

namespace Furion.Schedule;

/// <summary>
/// 作业信息
/// </summary>
public partial class JobDetail
{
    /// <summary>
    /// 获取作业所有额外数据
    /// </summary>
    /// <returns><see cref="Dictionary{String,Object}"/></returns>
    public Dictionary<string, object> GetProperties()
    {
        return RuntimeProperties;
    }

    /// <summary>
    /// 检查作业信息额外数据键是否存在
    /// </summary>
    /// <param name="key">键</param>
    /// <returns><see cref="bool"/></returns>
    public bool ContainsProperty(string key)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

        return RuntimeProperties.ContainsKey(key);
    }

    /// <summary>
    /// 获取作业信息额外数据
    /// </summary>
    /// <param name="key">键</param>
    /// <returns><see cref="object"/></returns>
    public object GetProperty(string key)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

        if (!RuntimeProperties.ContainsKey(key)) return default;

        return RuntimeProperties[key];
    }

    /// <summary>
    /// 获取作业信息额外数据
    /// </summary>
    /// <typeparam name="T">结果泛型类型</typeparam>
    /// <param name="key">键</param>
    /// <returns>T 类型</returns>
    public T GetProperty<T>(string key)
    {
        var value = GetProperty(key);
        if (value == null) return default;
        return (T)value;
    }

    /// <summary>
    /// 添加作业信息额外数据
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns><see cref="JobDetail"/></returns>
    public JobDetail AddProperty(string key, object value)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

        RuntimeProperties.TryAdd(key, value);
        Properties = Penetrates.Serialize(RuntimeProperties);

        return this;
    }

    /// <summary>
    /// 添加或更新作业信息额外数据
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="key">键</param>
    /// <param name="newValue">新值</param>
    /// <param name="updateAction">更新委托，如果传递了该参数，那么键存在使则使用该参数的返回值</param>
    /// <returns><see cref="JobDetail"/></returns>
    public JobDetail AddOrUpdateProperty<T>(string key, T newValue, Func<T, object> updateAction = default)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

        if (RuntimeProperties.ContainsKey(key))
        {
            RuntimeProperties[key] = updateAction == null
                ? newValue
                : updateAction((T)RuntimeProperties[key]);
        }
        else RuntimeProperties.TryAdd(key, newValue);

        Properties = Penetrates.Serialize(RuntimeProperties);

        return this;
    }

    /// <summary>
    /// 删除作业信息额外数据
    /// </summary>
    /// <param name="key">键</param>
    /// <returns><see cref="JobDetail"/></returns>
    public JobDetail RemoveProperty(string key)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

        if (RuntimeProperties.ContainsKey(key))
        {
            RuntimeProperties.Remove(key);
            Properties = Penetrates.Serialize(RuntimeProperties);
        }

        return this;
    }

    /// <summary>
    /// 清空作业信息额外数据
    /// </summary>
    /// <returns><see cref="JobDetail"/></returns>
    public JobDetail ClearProperties()
    {
        RuntimeProperties.Clear();
        Properties = Penetrates.Serialize(RuntimeProperties);

        return this;
    }

    /// <summary>
    /// 获取作业信息构建器
    /// </summary>
    /// <returns><see cref="JobBuilder"/></returns>
    public JobBuilder GetBuilder()
    {
        return JobBuilder.From(this);
    }

    /// <summary>
    /// 作业信息转字符串输出
    /// </summary>
    /// <returns><see cref="string"/></returns>
    public override string ToString()
    {
        return $"<{JobId}>{(string.IsNullOrWhiteSpace(Description) ? string.Empty : $" {Description.GetMaxLengthString()}")} [{(Concurrent ? "C" : "S")}]";
    }

    /// <summary>
    /// 带命名规则的数据库列名
    /// </summary>
    private readonly ConcurrentDictionary<NamingConventions, string[]> _namingColumnNames = new();

    /// <summary>
    /// 获取数据库列名
    /// </summary>
    /// <remarks>避免多次反射</remarks>
    /// <returns>string[]</returns>
    private string[] ColumnNames(NamingConventions naming = NamingConventions.CamelCase)
    {
        // 如果字典中已经存在过，则直接返回
        var contains = _namingColumnNames.TryGetValue(naming, out var columnNames);
        if (contains) return columnNames;

        // 否则创建新的
        var nameColumnNames = new[]
        {
            Penetrates.GetNaming(nameof(JobId), naming) // 第一个是标识，禁止移动位置
            , Penetrates.GetNaming(nameof(GroupName), naming)
            , Penetrates.GetNaming(nameof(JobType), naming)
            , Penetrates.GetNaming(nameof(AssemblyName), naming)
            , Penetrates.GetNaming(nameof(Description), naming)
            , Penetrates.GetNaming(nameof(Concurrent), naming)
            , Penetrates.GetNaming(nameof(IncludeAnnotations), naming)
            , Penetrates.GetNaming(nameof(Properties), naming)
            , Penetrates.GetNaming(nameof(UpdatedTime), naming)
        };
        _ = _namingColumnNames.TryAdd(naming, nameColumnNames);

        return nameColumnNames;
    }

    /// <summary>
    /// 转换成 Sql 语句
    /// </summary>
    /// <param name="tableName">数据库表名</param>
    /// <param name="behavior">持久化行为</param>
    /// <param name="naming">命名法</param>
    /// <returns><see cref="string"/></returns>
    public string ConvertToSQL(string tableName, PersistenceBehavior behavior, NamingConventions naming = NamingConventions.CamelCase)
    {
        // 判断是否自定义了 SQL 输出程序
        if (JobDetailOptions.ConvertToSQLConfigure != null)
        {
            return JobDetailOptions.ConvertToSQLConfigure(tableName, ColumnNames(naming), this, behavior, naming);
        }

        // 生成新增 SQL
        if (behavior == PersistenceBehavior.Appended)
        {
            return ConvertToInsertSQL(tableName, naming);
        }
        // 生成更新 SQL
        else if (behavior == PersistenceBehavior.Updated)
        {
            return ConvertToUpdateSQL(tableName, naming);
        }
        // 生成删除 SQL
        else if (behavior == PersistenceBehavior.Removed)
        {
            return ConvertToDeleteSQL(tableName, naming);
        }

        return string.Empty;
    }

    /// <summary>
    /// 转换成 Sql 新增语句
    /// </summary>
    /// <param name="tableName">数据库表名</param>
    /// <param name="naming">命名法</param>
    /// <returns><see cref="string"/></returns>
    public string ConvertToInsertSQL(string tableName, NamingConventions naming = NamingConventions.CamelCase)
    {
        // 不使用反射生成，为了使顺序可控，生成 SQL 可控，性能损耗最小
        var columnNames = ColumnNames(naming);

        return $@"INSERT INTO {Penetrates.WrapDatabaseFieldName(tableName)}(
    {Penetrates.WrapDatabaseFieldName(columnNames[0])},
    {Penetrates.WrapDatabaseFieldName(columnNames[1])},
    {Penetrates.WrapDatabaseFieldName(columnNames[2])},
    {Penetrates.WrapDatabaseFieldName(columnNames[3])},
    {Penetrates.WrapDatabaseFieldName(columnNames[4])},
    {Penetrates.WrapDatabaseFieldName(columnNames[5])},
    {Penetrates.WrapDatabaseFieldName(columnNames[6])},
    {Penetrates.WrapDatabaseFieldName(columnNames[7])},
    {Penetrates.WrapDatabaseFieldName(columnNames[8])}
)
VALUES(
    {Penetrates.GetNoNumberSqlValueOrNull(JobId)},
    {Penetrates.GetNoNumberSqlValueOrNull(GroupName)},
    {Penetrates.GetNoNumberSqlValueOrNull(JobType)},
    {Penetrates.GetNoNumberSqlValueOrNull(AssemblyName)},
    {Penetrates.GetNoNumberSqlValueOrNull(Description)},
    {Penetrates.GetBooleanSqlValue(Concurrent)},
    {Penetrates.GetBooleanSqlValue(IncludeAnnotations)},
    {Penetrates.GetNoNumberSqlValueOrNull(Properties)},
    {Penetrates.GetNoNumberSqlValueOrNull(UpdatedTime.ToFormatString())}
);";
    }

    /// <summary>
    /// 转换成 Sql 更新语句
    /// </summary>
    /// <param name="tableName">数据库表名</param>
    /// <param name="naming">命名法</param>
    /// <returns><see cref="string"/></returns>
    public string ConvertToUpdateSQL(string tableName, NamingConventions naming = NamingConventions.CamelCase)
    {
        // 不使用反射生成，为了使顺序可控，生成 SQL 可控，性能损耗最小
        var columnNames = ColumnNames(naming);

        return $@"UPDATE {Penetrates.WrapDatabaseFieldName(tableName)}
SET
    {Penetrates.WrapDatabaseFieldName(columnNames[0])} = {Penetrates.GetNoNumberSqlValueOrNull(JobId)},
    {Penetrates.WrapDatabaseFieldName(columnNames[1])} = {Penetrates.GetNoNumberSqlValueOrNull(GroupName)},
    {Penetrates.WrapDatabaseFieldName(columnNames[2])} = {Penetrates.GetNoNumberSqlValueOrNull(JobType)},
    {Penetrates.WrapDatabaseFieldName(columnNames[3])} = {Penetrates.GetNoNumberSqlValueOrNull(AssemblyName)},
    {Penetrates.WrapDatabaseFieldName(columnNames[4])} = {Penetrates.GetNoNumberSqlValueOrNull(Description)},
    {Penetrates.WrapDatabaseFieldName(columnNames[5])} = {Penetrates.GetBooleanSqlValue(Concurrent)},
    {Penetrates.WrapDatabaseFieldName(columnNames[6])} = {Penetrates.GetBooleanSqlValue(IncludeAnnotations)},
    {Penetrates.WrapDatabaseFieldName(columnNames[7])} = {Penetrates.GetNoNumberSqlValueOrNull(Properties)},
    {Penetrates.WrapDatabaseFieldName(columnNames[8])} = {Penetrates.GetNoNumberSqlValueOrNull(UpdatedTime.ToFormatString())}
WHERE {Penetrates.WrapDatabaseFieldName(columnNames[0])} = {Penetrates.GetNoNumberSqlValueOrNull(JobId)};";
    }

    /// <summary>
    /// 转换成 Sql 删除语句
    /// </summary>
    /// <param name="tableName">数据库表名</param>
    /// <param name="naming">命名法</param>
    /// <returns><see cref="string"/></returns>
    public string ConvertToDeleteSQL(string tableName, NamingConventions naming = NamingConventions.CamelCase)
    {
        // 不使用反射生成，为了使顺序可控，生成 SQL 可控，性能损耗最小
        var columnNames = ColumnNames(naming);

        return $@"DELETE FROM {Penetrates.WrapDatabaseFieldName(tableName)}
WHERE {Penetrates.WrapDatabaseFieldName(columnNames[0])} = {Penetrates.GetNoNumberSqlValueOrNull(JobId)};";
    }

    /// <summary>
    /// 转换成 JSON 字符串
    /// </summary>
    /// <param name="naming">命名法</param>
    /// <returns><see cref="string"/></returns>
    public string ConvertToJSON(NamingConventions naming = NamingConventions.CamelCase)
    {
        return Penetrates.Write(writer =>
        {
            writer.WriteStartObject();

            writer.WriteString(Penetrates.GetNaming(nameof(JobId), naming), JobId);
            writer.WriteString(Penetrates.GetNaming(nameof(GroupName), naming), GroupName);
            writer.WriteString(Penetrates.GetNaming(nameof(JobType), naming), JobType);
            writer.WriteString(Penetrates.GetNaming(nameof(AssemblyName), naming), AssemblyName);
            writer.WriteString(Penetrates.GetNaming(nameof(Description), naming), Description);
            writer.WriteBoolean(Penetrates.GetNaming(nameof(Concurrent), naming), Concurrent);
            writer.WriteBoolean(Penetrates.GetNaming(nameof(IncludeAnnotations), naming), IncludeAnnotations);
            writer.WriteString(Penetrates.GetNaming(nameof(Properties), naming), Properties);
            writer.WriteString(Penetrates.GetNaming(nameof(UpdatedTime), naming), UpdatedTime.ToFormatString());

            writer.WriteEndObject();
        });
    }

    /// <summary>
    /// 转换成 Monitor 字符串
    /// </summary>
    /// <param name="naming">命名法</param>
    /// <returns><see cref="string"/></returns>
    public string ConvertToMonitor(NamingConventions naming = NamingConventions.CamelCase)
    {
        return TP.Wrapper(nameof(JobDetail), Description ?? JobType, new[]
        {
            $"##{Penetrates.GetNaming(nameof(JobId), naming)}## {JobId}"
            , $"##{Penetrates.GetNaming(nameof(GroupName), naming)}## {GroupName}"
            , $"##{Penetrates.GetNaming(nameof(JobType), naming)}## {JobType}"
            , $"##{Penetrates.GetNaming(nameof(AssemblyName), naming)}## {AssemblyName}"
            , $"##{Penetrates.GetNaming(nameof(Description), naming)}## {Description}"
            , $"##{Penetrates.GetNaming(nameof(Concurrent), naming)}## {Concurrent}"
            , $"##{Penetrates.GetNaming(nameof(IncludeAnnotations), naming)}## {IncludeAnnotations}"
            , $"##{Penetrates.GetNaming(nameof(Properties), naming)}## {Properties}"
            , $"##{Penetrates.GetNaming(nameof(UpdatedTime), naming)}## {UpdatedTime.ToFormatString()}"
        });
    }
}