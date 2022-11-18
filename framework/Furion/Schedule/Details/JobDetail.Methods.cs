// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
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
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns><see cref="JobDetail"/></returns>
    public JobDetail AddOrUpdateProperty(string key, object value)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

        if (RuntimeProperties.ContainsKey(key)) RuntimeProperties[key] = value;
        else RuntimeProperties.TryAdd(key, value);

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
        // 这里不采用反射生成
        var columnNames = ColumnNames(naming);

        // 生成删除 SQL
        if (behavior == PersistenceBehavior.Removed)
        {
            return $@"DELETE FROM {tableName}
WHERE [{columnNames[0]}] = '{JobId}';";
        }
        // 生成新增 SQL
        else if (behavior == PersistenceBehavior.Appended)
        {
            return $@"INSERT INTO {tableName}(
    [{columnNames[0]}],
    [{columnNames[1]}],
    [{columnNames[2]}],
    [{columnNames[3]}],
    [{columnNames[4]}],
    [{columnNames[5]}],
    [{columnNames[6]}],
    [{columnNames[7]}],
    [{columnNames[8]}]
)
VALUES(
    '{JobId}',
    {Penetrates.GetSqlValueOrNull(GroupName)},
    '{JobType}',
    '{AssemblyName}',
    {Penetrates.GetSqlValueOrNull(Description)},
    {(Concurrent ? 1 : 0)},
    {(IncludeAnnotations ? 1 : 0)},
    {Penetrates.GetSqlValueOrNull(Properties)},
    {Penetrates.GetSqlValueOrNull(UpdatedTime)}
);";
        }
        // 生成更新 SQL
        else if (behavior == PersistenceBehavior.Updated)
        {
            return $@"UPDATE {tableName}
SET
    [{columnNames[0]}] = '{JobId}',
    [{columnNames[1]}] = {Penetrates.GetSqlValueOrNull(GroupName)},
    [{columnNames[2]}] = '{JobType}',
    [{columnNames[3]}] = '{AssemblyName}',
    [{columnNames[4]}] = {Penetrates.GetSqlValueOrNull(Description)},
    [{columnNames[5]}] = {(Concurrent ? 1 : 0)},
    [{columnNames[6]}] = {(IncludeAnnotations ? 1 : 0)},
    [{columnNames[7]}] = {Penetrates.GetSqlValueOrNull(Properties)},
    [{columnNames[8]}] = {Penetrates.GetSqlValueOrNull(UpdatedTime)}
WHERE [{columnNames[0]}] = '{JobId}';";
        }
        return string.Empty;
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
            writer.WriteString(Penetrates.GetNaming(nameof(UpdatedTime), naming), UpdatedTime?.ToString("o"));

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
            , $"##{Penetrates.GetNaming(nameof(UpdatedTime), naming)}## {UpdatedTime}"
        });
    }
}