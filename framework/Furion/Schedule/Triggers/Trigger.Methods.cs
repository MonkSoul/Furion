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
/// 作业触发器基类
/// </summary>
public partial class Trigger
{
    /// <summary>
    /// 计算下一个触发时间
    /// </summary>
    /// <param name="startAt">起始时间</param>
    /// <returns><see cref="DateTime"/></returns>
    public virtual DateTime GetNextOccurrence(DateTime startAt) => throw new NotImplementedException();

    /// <summary>
    /// 执行条件检查
    /// </summary>
    /// <param name="jobDetail">作业信息</param>
    /// <param name="startAt">起始时间</param>
    /// <returns><see cref="bool"/></returns>
    public virtual bool ShouldRun(JobDetail jobDetail, DateTime startAt)
    {
        return NextRunTime.Value <= startAt
            && LastRunTime != NextRunTime;
    }

    /// <summary>
    /// 获取作业触发器构建器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder GetBuilder()
    {
        return TriggerBuilder.From(this);
    }

    /// <summary>
    /// 作业触发器转字符串输出
    /// </summary>
    /// <returns><see cref="string"/></returns>
    public override string ToString()
    {
        return $"<{JobId} {TriggerId}>{(string.IsNullOrWhiteSpace(Description) ? string.Empty : $" {Description}")}";
    }

    /// <summary>
    /// 重置启动时最大触发次数等于一次的作业触发器
    /// </summary>
    /// <param name="useUtcTimestamp">是否使用 UTC 时间</param>
    internal void ResetMaxNumberOfRunsEqualOnceOnStart(bool useUtcTimestamp)
    {
        if (StartNow
            && ResetOnlyOnce
            && MaxNumberOfRuns == 1
            && NumberOfRuns == 1)
        {
            NumberOfRuns = 0;
            SetStatus(TriggerStatus.Ready);
            NextRunTime = CheckRunOnStarAndReturnNextRunTime(useUtcTimestamp);

            if (MaxNumberOfErrors > 0 && NumberOfErrors >= MaxNumberOfErrors)
            {
                NumberOfErrors = MaxNumberOfErrors - 1;
            }
        }
    }

    /// <summary>
    /// 记录运行信息和计算下一个触发时间
    /// </summary>
    /// <param name="jobDetail">作业信息</param>
    /// <param name="startAt">起始时间</param>
    /// <param name="useUtcTimestamp">是否使用 UTC 时间</param>
    internal void Increment(JobDetail jobDetail, DateTime startAt, bool useUtcTimestamp)
    {
        // 阻塞状态并没有实际执行，此时忽略次数递增和最近运行时间赋值
        if (Status != TriggerStatus.Blocked)
        {
            NumberOfRuns++;
            LastRunTime = NextRunTime;
        }

        // 检查下一次执行信息
        if (CheckNextOccurrence(jobDetail, startAt)) NextRunTime = GetNextRunTime(useUtcTimestamp);
    }

    /// <summary>
    /// 记录错误信息，包含错误次数和运行状态
    /// </summary>
    /// <param name="jobDetail">作业信息</param>
    /// <param name="startAt">起始时间</param>
    internal void IncrementErrors(JobDetail jobDetail, DateTime startAt)
    {
        NumberOfErrors++;

        // 检查下一次执行信息
        if (CheckNextOccurrence(jobDetail, startAt)) SetStatus(TriggerStatus.ErrorToReady);
    }

    /// <summary>
    /// 计算下一次运行时间
    /// </summary>
    /// <param name="useUtcTimestamp">是否使用 UTC 时间</param>
    /// <returns><see cref="DateTime"/></returns>
    internal DateTime? GetNextRunTime(bool useUtcTimestamp)
    {
        // 如果未启动或不是正常的触发器状态，则返回 null
        if (StartNow == false || (Status != TriggerStatus.Ready
            && Status != TriggerStatus.ErrorToReady
            && Status != TriggerStatus.Running
            && Status != TriggerStatus.Blocked)) return null;

        // 如果已经设置了 NextRunTime 且其值大于当前时间，则返回当前 NextRunTime（可能因为其他方式修改了改值导致触发时间不是精准计算的时间）
        if (NextRunTime != null && NextRunTime.Value > Penetrates.GetNowTime(useUtcTimestamp)) return NextRunTime;

        var startAt = GetStartAt(useUtcTimestamp);
        return startAt == null
            ? null
            : GetNextOccurrence(startAt.Value);
    }

    /// <summary>
    /// 检查是否启动时执行一次并返回下一次执行时间
    /// </summary>
    /// <param name="useUtcTimestamp">是否使用 UTC 时间</param>
    /// <returns><see cref="DateTime"/></returns>
    internal DateTime? CheckRunOnStarAndReturnNextRunTime(bool useUtcTimestamp)
    {
        return !(StartNow && RunOnStart)
              ? GetNextRunTime(useUtcTimestamp)
              : Penetrates.GetNowTime(useUtcTimestamp).AddSeconds(-1);
    }

    /// <summary>
    /// 设置作业触发器状态
    /// </summary>
    /// <param name="status"><see cref="TriggerStatus"/></param>
    internal void SetStatus(TriggerStatus status)
    {
        if (Status == status) return;
        Status = status;
    }

    /// <summary>
    /// 是否是正常触发器状态
    /// </summary>
    /// <returns><see cref="bool"/></returns>
    internal bool IsNormalStatus()
    {
        var isNormalStatus = Status != TriggerStatus.Backlog
            && Status != TriggerStatus.Pause
            && Status != TriggerStatus.Archived
            && Status != TriggerStatus.Panic
            && Status != TriggerStatus.Overrun
            && Status != TriggerStatus.Unoccupied
            && Status != TriggerStatus.NotStart
            && Status != TriggerStatus.Unknown
            && Status != TriggerStatus.Unhandled;

        // 如果不是正常触发器状态，NextRunTime 强制设置为 null
        if (!isNormalStatus) NextRunTime = null;

        return isNormalStatus;
    }

    /// <summary>
    /// 检查下一次执行信息
    /// </summary>
    /// <param name="jobDetail">作业信息</param>
    /// <param name="startAt">起始时间</param>
    /// <returns><see cref="bool"/></returns>
    internal bool CheckNextOccurrence(JobDetail jobDetail, DateTime startAt)
    {
        // 检查作业信息运行时类型
        if (jobDetail.RuntimeJobType == null)
        {
            SetStatus(TriggerStatus.Unhandled);
            NextRunTime = null;
            return false;
        }

        // 检查作业触发器运行时类型
        if (RuntimeTriggerType == null)
        {
            SetStatus(TriggerStatus.Unknown);
            NextRunTime = null;
            return false;
        }

        // 检查是否立即启动
        if (StartNow == false)
        {
            SetStatus(TriggerStatus.NotStart);
            NextRunTime = null;
            return false;
        }

        // 开始时间检查
        if (StartTime != null && StartTime.Value > startAt)
        {
            SetStatus(TriggerStatus.Backlog);
            return false;
        }

        // 结束时间检查
        if (EndTime != null && EndTime.Value < startAt)
        {
            SetStatus(TriggerStatus.Archived);
            return false;
        }

        // 最大次数判断
        if (MaxNumberOfRuns > 0 && NumberOfRuns >= MaxNumberOfRuns)
        {
            SetStatus(TriggerStatus.Overrun);
            return false;
        }

        // 最大错误数判断
        if (MaxNumberOfErrors > 0 && NumberOfErrors >= MaxNumberOfErrors)
        {
            SetStatus(TriggerStatus.Panic);
            return false;
        }

        // 状态检查
        if (!IsNormalStatus())
        {
            return false;
        }

        // 下一次运行时间空判断
        if (NextRunTime == null)
        {
            if (IsNormalStatus()) SetStatus(TriggerStatus.Unoccupied);
            return false;
        }

        return true;
    }

    /// <summary>
    /// 执行条件检查（内部检查）
    /// </summary>
    /// <param name="jobDetail">作业信息</param>
    /// <param name="startAt">起始时间</param>
    /// <returns><see cref="bool"/></returns>
    internal bool InternalShouldRun(JobDetail jobDetail, DateTime startAt)
    {
        // 调用派生类 ShouldRun 方法
        return CheckNextOccurrence(jobDetail, startAt) && ShouldRun(jobDetail, startAt);
    }

    /// <summary>
    /// 带命名规则的数据库列名
    /// </summary>
    private readonly ConcurrentDictionary<NamingConventions, string[]> _namingColumnNames = new();

    /// <summary>
    /// 获取数据库列名
    /// </summary>
    /// <param name="naming">命名法</param>
    /// <returns>string[]</returns>
    private string[] ColumnNames(NamingConventions naming = NamingConventions.CamelCase)
    {
        // 如果字典中已经存在过，则直接返回
        var contains = _namingColumnNames.TryGetValue(naming, out var columnNames);
        if (contains) return columnNames;

        // 否则创建新的
        var nameColumnNames = new[]
        {
            Penetrates.GetNaming(nameof(TriggerId), naming)    // 第一个是标识，禁止移动位置
            , Penetrates.GetNaming(nameof(JobId), naming)   // 第二个是作业标识，禁止移动位置
            , Penetrates.GetNaming(nameof(TriggerType), naming)
            , Penetrates.GetNaming(nameof(AssemblyName), naming)
            , Penetrates.GetNaming(nameof(Args), naming)
            , Penetrates.GetNaming(nameof(Description), naming)
            , Penetrates.GetNaming(nameof(Status), naming)
            , Penetrates.GetNaming(nameof(StartTime), naming)
            , Penetrates.GetNaming(nameof(EndTime), naming)
            , Penetrates.GetNaming(nameof(LastRunTime), naming)
            , Penetrates.GetNaming(nameof(NextRunTime), naming)
            , Penetrates.GetNaming(nameof(NumberOfRuns), naming)
            , Penetrates.GetNaming(nameof(MaxNumberOfRuns), naming)
            , Penetrates.GetNaming(nameof(NumberOfErrors), naming)
            , Penetrates.GetNaming(nameof(MaxNumberOfErrors), naming)
            , Penetrates.GetNaming(nameof(NumRetries), naming)
            , Penetrates.GetNaming(nameof(RetryTimeout), naming)
            , Penetrates.GetNaming(nameof(StartNow), naming)
            , Penetrates.GetNaming(nameof(RunOnStart), naming)
            , Penetrates.GetNaming(nameof(ResetOnlyOnce), naming)
            , Penetrates.GetNaming(nameof(UpdatedTime), naming)
        };
        _ = _namingColumnNames.TryAdd(naming, nameColumnNames);

        return nameColumnNames;
    }

    /// <summary>
    /// 获取触发器初始化时间
    /// </summary>
    /// <param name="useUtcTimestamp">是否使用 UTC 时间</param>
    /// <returns><see cref="DateTime"/> 或者 null</returns>
    private DateTime? GetStartAt(bool useUtcTimestamp)
    {
        var nowTime = Penetrates.GetNowTime(useUtcTimestamp);
        if (StartTime == null)
        {
            if (EndTime == null) return nowTime;
            else return EndTime.Value < nowTime ? null : nowTime;
        }
        else return StartTime.Value < nowTime ? nowTime : StartTime.Value;
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
        if (TriggerOptions.ConvertToSQLConfigure != null)
        {
            return TriggerOptions.ConvertToSQLConfigure(tableName, ColumnNames(naming), this, behavior, naming);
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
    {columnNames[8]},
    {columnNames[9]},
    {columnNames[10]},
    {columnNames[11]},
    {columnNames[12]},
    {columnNames[13]},
    {columnNames[14]},
    {columnNames[15]},
    {columnNames[16]},
    {columnNames[17]},
    {columnNames[18]},
    {columnNames[19]},
    {columnNames[20]}
)
VALUES(
    '{TriggerId}',
    '{JobId}',
    '{TriggerType}',
    '{AssemblyName}',
    {Penetrates.GetNoNumberSqlValueOrNull(Args)},
    {Penetrates.GetNoNumberSqlValueOrNull(Description)},
    {((int)Status)},
    {Penetrates.GetNoNumberSqlValueOrNull(StartTime)},
    {Penetrates.GetNoNumberSqlValueOrNull(EndTime)},
    {Penetrates.GetNoNumberSqlValueOrNull(LastRunTime)},
    {Penetrates.GetNoNumberSqlValueOrNull(NextRunTime)},
    {NumberOfRuns},
    {MaxNumberOfRuns},
    {NumberOfErrors},
    {MaxNumberOfErrors},
    {NumRetries},
    {RetryTimeout},
    {(StartNow ? 1 : 0)},
    {(RunOnStart ? 1 : 0)},
    {(ResetOnlyOnce ? 1 : 0)},
    {Penetrates.GetNoNumberSqlValueOrNull(UpdatedTime)}
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
    {columnNames[0]} = '{TriggerId}',
    {columnNames[1]} = '{JobId}',
    {columnNames[2]} = '{TriggerType}',
    {columnNames[3]} = '{AssemblyName}',
    {columnNames[4]} = {Penetrates.GetNoNumberSqlValueOrNull(Args)},
    {columnNames[5]} = {Penetrates.GetNoNumberSqlValueOrNull(Description)},
    {columnNames[6]} = {((int)Status)},
    {columnNames[7]} = {Penetrates.GetNoNumberSqlValueOrNull(StartTime)},
    {columnNames[8]} = {Penetrates.GetNoNumberSqlValueOrNull(EndTime)},
    {columnNames[9]} = {Penetrates.GetNoNumberSqlValueOrNull(LastRunTime)},
    {columnNames[10]} = {Penetrates.GetNoNumberSqlValueOrNull(NextRunTime)},
    {columnNames[11]} = {NumberOfRuns},
    {columnNames[12]} = {MaxNumberOfRuns},
    {columnNames[13]} = {NumberOfErrors},
    {columnNames[14]} = {MaxNumberOfErrors},
    {columnNames[15]} = {NumRetries},
    {columnNames[16]} = {RetryTimeout},
    {columnNames[17]} = {(StartNow ? 1 : 0)},
    {columnNames[18]} = {(RunOnStart ? 1 : 0)},
    {columnNames[19]} = {(ResetOnlyOnce ? 1 : 0)},
    {columnNames[20]} = {Penetrates.GetNoNumberSqlValueOrNull(UpdatedTime)}
WHERE {columnNames[0]} = '{TriggerId}' AND {columnNames[1]} = '{JobId}';";
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
WHERE {columnNames[0]} = '{TriggerId}' AND {columnNames[1]} = '{JobId}';";
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

            writer.WriteString(Penetrates.GetNaming(nameof(TriggerId), naming), TriggerId);
            writer.WriteString(Penetrates.GetNaming(nameof(JobId), naming), JobId);
            writer.WriteString(Penetrates.GetNaming(nameof(TriggerType), naming), TriggerType);
            writer.WriteString(Penetrates.GetNaming(nameof(AssemblyName), naming), AssemblyName);
            writer.WriteString(Penetrates.GetNaming(nameof(Args), naming), Args);
            writer.WriteString(Penetrates.GetNaming(nameof(Description), naming), Description);
            writer.WriteNumber(Penetrates.GetNaming(nameof(Status), naming), (int)Status);
            writer.WriteString(Penetrates.GetNaming(nameof(StartTime), naming), StartTime?.ToString("o"));
            writer.WriteString(Penetrates.GetNaming(nameof(EndTime), naming), EndTime?.ToString("o"));
            writer.WriteString(Penetrates.GetNaming(nameof(LastRunTime), naming), LastRunTime?.ToString("o"));
            writer.WriteString(Penetrates.GetNaming(nameof(NextRunTime), naming), NextRunTime?.ToString("o"));
            writer.WriteNumber(Penetrates.GetNaming(nameof(NumberOfRuns), naming), NumberOfRuns);
            writer.WriteNumber(Penetrates.GetNaming(nameof(MaxNumberOfRuns), naming), MaxNumberOfRuns);
            writer.WriteNumber(Penetrates.GetNaming(nameof(NumberOfErrors), naming), NumberOfErrors);
            writer.WriteNumber(Penetrates.GetNaming(nameof(MaxNumberOfErrors), naming), MaxNumberOfErrors);
            writer.WriteNumber(Penetrates.GetNaming(nameof(NumRetries), naming), NumRetries);
            writer.WriteNumber(Penetrates.GetNaming(nameof(RetryTimeout), naming), RetryTimeout);
            writer.WriteBoolean(Penetrates.GetNaming(nameof(StartNow), naming), StartNow);
            writer.WriteBoolean(Penetrates.GetNaming(nameof(RunOnStart), naming), RunOnStart);
            writer.WriteBoolean(Penetrates.GetNaming(nameof(ResetOnlyOnce), naming), ResetOnlyOnce);
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
        return TP.Wrapper(nameof(Trigger), Description ?? TriggerType, new[]
        {
            $"##{Penetrates.GetNaming(nameof(TriggerId), naming)}## {TriggerId}"
            , $"##{Penetrates.GetNaming(nameof(JobId), naming)}## {JobId}"
            , $"##{Penetrates.GetNaming(nameof(TriggerType), naming)}## {TriggerType}"
            , $"##{Penetrates.GetNaming(nameof(AssemblyName), naming)}## {AssemblyName}"
            , $"##{Penetrates.GetNaming(nameof(Args), naming)}## {Args}"
            , $"##{Penetrates.GetNaming(nameof(Description), naming)}## {Description}"
            , $"##{Penetrates.GetNaming(nameof(Status), naming)}## {Status}"
            , $"##{Penetrates.GetNaming(nameof(StartTime), naming)}## {StartTime}"
            , $"##{Penetrates.GetNaming(nameof(EndTime), naming)}## {EndTime}"
            , $"##{Penetrates.GetNaming(nameof(LastRunTime), naming)}## {LastRunTime}"
            , $"##{Penetrates.GetNaming(nameof(NextRunTime), naming)}## {NextRunTime}"
            , $"##{Penetrates.GetNaming(nameof(NumberOfRuns), naming)}## {NumberOfRuns}"
            , $"##{Penetrates.GetNaming(nameof(MaxNumberOfRuns), naming)}## {MaxNumberOfRuns}"
            , $"##{Penetrates.GetNaming(nameof(NumberOfErrors), naming)}## {NumberOfErrors}"
            , $"##{Penetrates.GetNaming(nameof(MaxNumberOfErrors), naming)}## {MaxNumberOfErrors}"
            , $"##{Penetrates.GetNaming(nameof(NumRetries), naming)}## {NumRetries}"
            , $"##{Penetrates.GetNaming(nameof(RetryTimeout), naming)}## {RetryTimeout}"
            , $"##{Penetrates.GetNaming(nameof(StartNow), naming)}## {StartNow}"
            , $"##{Penetrates.GetNaming(nameof(RunOnStart), naming)}## {RunOnStart}"
            , $"##{Penetrates.GetNaming(nameof(ResetOnlyOnce), naming)}## {ResetOnlyOnce}"
            , $"##{Penetrates.GetNaming(nameof(UpdatedTime), naming)}## {UpdatedTime}"
        });
    }
}