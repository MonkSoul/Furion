// MIT License
//
// Copyright (c) 2020-2023 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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

        return $@"INSERT INTO {tableName}(
    {columnNames[0]},
    {columnNames[1]},
    {columnNames[2]},
    {columnNames[3]},
    {columnNames[4]},
    {columnNames[5]},
    {columnNames[6]},
    {columnNames[7]},
    {columnNames[8]}
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
    {Penetrates.GetNoNumberSqlValueOrNull(UpdatedTime.ToUnspecifiedString())}
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

        return $@"UPDATE {tableName}
SET
    {columnNames[0]} = {Penetrates.GetNoNumberSqlValueOrNull(JobId)},
    {columnNames[1]} = {Penetrates.GetNoNumberSqlValueOrNull(GroupName)},
    {columnNames[2]} = {Penetrates.GetNoNumberSqlValueOrNull(JobType)},
    {columnNames[3]} = {Penetrates.GetNoNumberSqlValueOrNull(AssemblyName)},
    {columnNames[4]} = {Penetrates.GetNoNumberSqlValueOrNull(Description)},
    {columnNames[5]} = {Penetrates.GetBooleanSqlValue(Concurrent)},
    {columnNames[6]} = {Penetrates.GetBooleanSqlValue(IncludeAnnotations)},
    {columnNames[7]} = {Penetrates.GetNoNumberSqlValueOrNull(Properties)},
    {columnNames[8]} = {Penetrates.GetNoNumberSqlValueOrNull(UpdatedTime.ToUnspecifiedString())}
WHERE {columnNames[0]} = {Penetrates.GetNoNumberSqlValueOrNull(JobId)};";
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

        return $@"DELETE FROM {tableName}
WHERE {columnNames[0]} = {Penetrates.GetNoNumberSqlValueOrNull(JobId)};";
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
            writer.WriteString(Penetrates.GetNaming(nameof(UpdatedTime), naming), UpdatedTime.ToUnspecifiedString());

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
            , $"##{Penetrates.GetNaming(nameof(UpdatedTime), naming)}## {UpdatedTime.ToUnspecifiedString()}"
        });
    }
}