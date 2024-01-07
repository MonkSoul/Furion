// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Schedule;

/// <summary>
/// 毫秒周期（间隔）作业触发器
/// </summary>
[SuppressSniffer]
public class PeriodTrigger : Trigger
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="interval">间隔（毫秒）</param>
    public PeriodTrigger(long interval)
    {
        // 最低运行毫秒数为 100ms
        if (interval < 100) throw new InvalidOperationException($"The interval cannot be less than 100ms, but the value is <{interval}ms>.");

        Interval = interval;
    }

    /// <summary>
    /// 间隔（毫秒）
    /// </summary>
    protected long Interval { get; }

    /// <summary>
    /// 计算下一个触发时间
    /// </summary>
    /// <param name="startAt">起始时间</param>
    /// <returns><see cref="DateTime"/></returns>
    public override DateTime GetNextOccurrence(DateTime startAt)
    {
        return startAt.AddMilliseconds(Interval);
    }

    /// <summary>
    /// 作业触发器转字符串输出
    /// </summary>
    /// <returns><see cref="string"/></returns>
    public override string ToString()
    {
        return $"<{JobId} {TriggerId}> {GetUnit()}{(string.IsNullOrWhiteSpace(Description) ? string.Empty : $" {Description.GetMaxLengthString()}")} {NumberOfRuns}ts";
    }

    /// <summary>
    /// 计算间隔单位
    /// </summary>
    /// <returns></returns>
    private string GetUnit()
    {
        // 毫秒
        if (Interval < 1000) return $"{Interval}ms";
        // 秒
        if (Interval >= 1000 && Interval < 1000 * 60 && Interval % 1000 == 0) return $"{Interval / 1000}s";
        // 分钟
        if (Interval >= 1000 * 60 && Interval < 1000 * 60 * 60 && Interval % (1000 * 60) == 0) return $"{Interval / (1000 * 60)}min";
        // 小时
        if (Interval >= 1000 * 60 * 60 && Interval < 1000 * 60 * 60 * 24 && Interval % (1000 * 60 * 60) == 0) return $"{Interval / (1000 * 60 * 60)}h";

        return $"{Interval}ms";
    }
}