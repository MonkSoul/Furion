// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.TimeCrontab;

namespace Furion.Schedule;

/// <summary>
/// 作业触发器
/// </summary>
public sealed partial class TriggerBuilder
{
    /// <summary>
    /// 设置作业触发器
    /// </summary>
    /// <typeparam name="TTrigger"><see cref="Trigger"/> 派生类</typeparam>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterTo<TTrigger>()
        where TTrigger : Trigger
    {
        return AlterTo<TTrigger>();
    }

    /// <summary>
    /// 设置作业触发器
    /// </summary>
    /// <typeparam name="TTrigger"><see cref="Trigger"/> 派生类</typeparam>
    /// <param name="args">作业触发器参数</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterTo<TTrigger>(params object[] args)
        where TTrigger : Trigger
    {
        return SetTriggerType<TTrigger>().SetArgs(args);
    }

    /// <summary>
    /// 设置作业触发器
    /// </summary>
    /// <param name="assemblyName">作业触发器类型所在程序集 Name</param>
    /// <param name="triggerTypeFullName">作业触发器类型 FullName</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterTo(string assemblyName, string triggerTypeFullName)
    {
        return AlterTo(assemblyName, triggerTypeFullName);
    }

    /// <summary>
    /// 设置作业触发器
    /// </summary>
    /// <param name="assemblyName">作业触发器类型所在程序集 Name</param>
    /// <param name="triggerTypeFullName">作业触发器类型 FullName</param>
    /// <param name="args">作业触发器参数</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterTo(string assemblyName, string triggerTypeFullName, params object[] args)
    {
        return SetTriggerType(assemblyName, triggerTypeFullName).SetArgs(args);
    }

    /// <summary>
    /// 设置作业触发器
    /// </summary>
    /// <param name="triggerType"><see cref="Trigger"/> 派生类</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterTo(Type triggerType)
    {
        return AlterTo(triggerType);
    }

    /// <summary>
    /// 设置作业触发器
    /// </summary>
    /// <param name="triggerType"><see cref="Trigger"/> 派生类</param>
    /// <param name="args">作业触发器参数</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterTo(Type triggerType, params object[] args)
    {
        return SetTriggerType(triggerType).SetArgs(args);
    }

    /// <summary>
    /// 设置毫秒周期（间隔）作业触发器
    /// </summary>
    /// <param name="interval">间隔（毫秒）</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterToPeriod(long interval)
    {
        return SetTriggerType<PeriodTrigger>().SetArgs(interval);
    }

    /// <summary>
    /// 设置秒周期（间隔）作业触发器
    /// </summary>
    /// <param name="interval">间隔（秒）</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterToPeriodSeconds(long interval)
    {
        return AlterToPeriod(interval * 1000);
    }

    /// <summary>
    /// 设置分钟周期（间隔）作业触发器
    /// </summary>
    /// <param name="interval">间隔（分钟）</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterToPeriodMinutes(long interval)
    {
        return AlterToPeriod(interval * 1000 * 60);
    }

    /// <summary>
    /// 设置小时周期（间隔）作业触发器
    /// </summary>
    /// <param name="interval">间隔（小时）</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterToPeriodHours(long interval)
    {
        return AlterToPeriod(interval * 1000 * 60 * 60);
    }

    /// <summary>
    /// 设置 Cron 表达式作业触发器
    /// </summary>
    /// <param name="schedule">Cron 表达式</param>
    /// <param name="format">Cron 表达式格式化类型，默认 <see cref="CronStringFormat.Default"/></param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterToCron(string schedule, CronStringFormat format = CronStringFormat.Default)
    {
        return SetTriggerType<CronTrigger>().SetArgs(schedule, format);
    }

    /// <summary>
    /// 设置 Cron 表达式作业触发器
    /// </summary>
    /// <param name="schedule">Cron 表达式</param>
    /// <param name="args">动态参数类型，支持 <see cref="int"/> 和 object[]</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterToCron(string schedule, object args)
    {
        return SetTriggerType<CronTrigger>().SetArgs(schedule, args);
    }

    /// <summary>
    /// 设置每秒开始作业触发器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterToSecondly()
    {
        return AlterToCron("@secondly");
    }

    /// <summary>
    /// 设置指定特定秒开始作业触发器
    /// </summary>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterToSecondlyAt(params object[] fields)
    {
        return AlterToCron("@secondly", fields);
    }

    /// <summary>
    /// 设置每分钟开始作业触发器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterToMinutely()
    {
        return AlterToCron("@minutely");
    }

    /// <summary>
    /// 设置每分钟特定秒开始作业触发器
    /// </summary>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterToMinutelyAt(params object[] fields)
    {
        return AlterToCron("@minutely", fields);
    }

    /// <summary>
    /// 设置每小时开始作业触发器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterToHourly()
    {
        return AlterToCron("@hourly");
    }

    /// <summary>
    /// 设置每小时特定分钟开始作业触发器
    /// </summary>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterToHourlyAt(params object[] fields)
    {
        return AlterToCron("@hourly", fields);
    }

    /// <summary>
    /// 设置每天（午夜）开始作业触发器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterToDaily()
    {
        return AlterToCron("@daily");
    }

    /// <summary>
    /// 设置每天特定小时开始作业触发器
    /// </summary>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterToDailyAt(params object[] fields)
    {
        return AlterToCron("@daily", fields);
    }

    /// <summary>
    /// 设置每月1号（午夜）开始作业触发器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterToMonthly()
    {
        return AlterToCron("@monthly");
    }

    /// <summary>
    /// 设置每月特定天（午夜）开始作业触发器
    /// </summary>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterToMonthlyAt(params object[] fields)
    {
        return AlterToCron("@monthly", fields);
    }

    /// <summary>
    /// 设置每周日（午夜）开始作业触发器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterToWeekly()
    {
        return AlterToCron("@weekly");
    }

    /// <summary>
    /// 设置每周特定星期几（午夜）开始作业触发器
    /// </summary>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterToWeeklyAt(params object[] fields)
    {
        return AlterToCron("@weekly", fields);
    }

    /// <summary>
    /// 设置每年1月1号（午夜）开始作业触发器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterToYearly()
    {
        return AlterToCron("@yearly");
    }

    /// <summary>
    /// 设置每年特定月1号（午夜）开始作业触发器
    /// </summary>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterToYearlyAt(params object[] fields)
    {
        return AlterToCron("@yearly", fields);
    }

    /// <summary>
    /// 设置每周一至周五（午夜）开始作业触发器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder AlterToWorkday()
    {
        return AlterToCron("@workday");
    }
}