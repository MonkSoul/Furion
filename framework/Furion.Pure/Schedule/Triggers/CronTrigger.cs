// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.TimeCrontab;

namespace Furion.Schedule;

/// <summary>
/// Cron 表达式作业触发器
/// </summary>
[SuppressSniffer]
public class CronTrigger : Trigger
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="schedule">Cron 表达式</param>
    /// <param name="args">动态参数类型，支持 <see cref="int"/>，<see cref="CronStringFormat"/> 和 object[]</param>
    public CronTrigger(string schedule, object args)
    {
        // 处理 int/long 转 CronStringFormat
        if (args is int or long)
        {
            Crontab = Crontab.Parse(schedule, (CronStringFormat)int.Parse(args.ToString()));
        }
        // 处理 CronStringFormat
        else if (args is CronStringFormat format)
        {
            Crontab = Crontab.Parse(schedule, format);
        }
        // 处理 Macro At
        else if (args is object[] fields)
        {
            Crontab = Crontab.ParseAt(schedule, fields);
        }
        else throw new NotImplementedException();
    }

    /// <summary>
    /// <see cref="Crontab"/> 对象
    /// </summary>
    private Crontab Crontab { get; }

    /// <summary>
    /// 计算下一个触发时间
    /// </summary>
    /// <param name="startAt">起始时间</param>
    /// <returns><see cref="DateTime"/></returns>
    public override DateTime GetNextOccurrence(DateTime startAt)
    {
        return Crontab.GetNextOccurrence(startAt);
    }

    /// <summary>
    /// 作业触发器转字符串输出
    /// </summary>
    /// <returns><see cref="string"/></returns>
    public override string ToString()
    {
        return $"<{JobId} {TriggerId}> {Crontab}{(string.IsNullOrWhiteSpace(Description) ? string.Empty : $" {Description.GetMaxLengthString()}")} {NumberOfRuns}ts";
    }
}