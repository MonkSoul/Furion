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

using Furion.TimeCrontab;

namespace Furion.Schedule;

/// <summary>
/// 作业触发器静态类
/// </summary>
[SuppressSniffer]
public static class Triggers
{
    /// <summary>
    /// 创建作业触发器构建器
    /// </summary>
    /// <typeparam name="TTrigger"><see cref="Trigger"/> 派生类</typeparam>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Create<TTrigger>()
        where TTrigger : Trigger
    {
        return TriggerBuilder.Create<TTrigger>();
    }

    /// <summary>
    /// 创建作业触发器构建器
    /// </summary>
    /// <typeparam name="TTrigger"><see cref="Trigger"/> 派生类</typeparam>
    /// <param name="args">作业触发器参数</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Create<TTrigger>(params object[] args)
        where TTrigger : Trigger
    {
        return TriggerBuilder.Create<TTrigger>(args);
    }

    /// <summary>
    /// 创建新的作业触发器构建器
    /// </summary>
    /// <param name="assemblyName">作业触发器类型所在程序集 Name</param>
    /// <param name="triggerTypeFullName">作业触发器类型 FullName</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Create(string assemblyName, string triggerTypeFullName)
    {
        return TriggerBuilder.Create(assemblyName, triggerTypeFullName);
    }

    /// <summary>
    /// 创建新的作业触发器构建器
    /// </summary>
    /// <param name="assemblyName">作业触发器类型所在程序集 Name</param>
    /// <param name="triggerTypeFullName">作业触发器类型 FullName</param>
    /// <param name="args">作业触发器参数</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Create(string assemblyName, string triggerTypeFullName, params object[] args)
    {
        return TriggerBuilder.Create(assemblyName, triggerTypeFullName, args);
    }

    /// <summary>
    /// 创建新的作业触发器构建器
    /// </summary>
    /// <param name="triggerType"><see cref="Trigger"/> 派生类</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Create(Type triggerType)
    {
        return TriggerBuilder.Create(triggerType);
    }

    /// <summary>
    /// 创建新的作业触发器构建器
    /// </summary>
    /// <param name="triggerType"><see cref="Trigger"/> 派生类</param>
    /// <param name="args">作业触发器参数</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Create(Type triggerType, params object[] args)
    {
        return TriggerBuilder.Create(triggerType, args);
    }

    /// <summary>
    /// 将 <see cref="Trigger"/> 转换成 <see cref="TriggerBuilder"/>
    /// </summary>
    /// <param name="trigger">作业触发器</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder From(Trigger trigger)
    {
        return TriggerBuilder.From(trigger);
    }

    /// <summary>
    /// 将 JSON 字符串转换成 <see cref="TriggerBuilder"/>
    /// </summary>
    /// <param name="json">JSON 字符串</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder From(string json)
    {
        return TriggerBuilder.From(json);
    }

    /// <summary>
    /// 克隆作业触发器构建器
    /// </summary>
    /// <param name="fromTriggerBuilder">被克隆的作业触发器构建器</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Clone(TriggerBuilder fromTriggerBuilder)
    {
        return TriggerBuilder.Clone(fromTriggerBuilder);
    }

    /// <summary>
    /// 创建毫秒周期（间隔）作业触发器构建器
    /// </summary>
    /// <param name="interval">间隔（毫秒）</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Period(int interval)
    {
        return TriggerBuilder.Period(interval);
    }

    /// <summary>
    /// 创建秒周期（间隔）作业触发器构建器
    /// </summary>
    /// <param name="interval">间隔（秒）</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder PeriodSeconds(int interval)
    {
        return TriggerBuilder.PeriodSeconds(interval);
    }

    /// <summary>
    /// 创建 Cron 表达式作业触发器构建器
    /// </summary>
    /// <param name="schedule">Cron 表达式</param>
    /// <param name="format">Cron 表达式格式化类型，默认 <see cref="CronStringFormat.Default"/></param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Cron(string schedule, CronStringFormat format = CronStringFormat.Default)
    {
        return TriggerBuilder.Cron(schedule, format);
    }

    /// <summary>
    /// 创建每秒开始作业触发器构建器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Secondly()
    {
        return Cron("@secondly");
    }

    /// <summary>
    /// 创建指定特定秒开始作业触发器构建器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder SecondlyAt(params int[] fields)
    {
        // 空检查
        if (fields == null || fields.Length == 0) throw new ArgumentNullException(nameof(fields));

        return Cron($"{string.Join(',', fields)} * * * * *", CronStringFormat.WithSeconds);
    }

    /// <summary>
    /// 创建每分钟开始作业触发器构建器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Minutely()
    {
        return Cron("@minutely");
    }

    /// <summary>
    /// 创建每分钟特定秒开始作业触发器构建器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder MinutelyAt(params int[] fields)
    {
        // 空检查
        if (fields == null || fields.Length == 0) throw new ArgumentNullException(nameof(fields));

        return Cron($"{string.Join(',', fields)} * * * * *", CronStringFormat.WithSeconds);
    }

    /// <summary>
    /// 创建每小时开始作业触发器构建器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Hourly()
    {
        return Cron("@hourly");
    }

    /// <summary>
    /// 创建每小时特定分钟开始作业触发器构建器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder HourlyAt(params int[] fields)
    {
        // 空检查
        if (fields == null || fields.Length == 0) throw new ArgumentNullException(nameof(fields));

        return Cron($"{string.Join(',', fields)} * * * *", CronStringFormat.Default);
    }

    /// <summary>
    /// 创建每天（午夜）开始作业触发器构建器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Daily()
    {
        return Cron("@daily");
    }

    /// <summary>
    /// 创建每天特定小时开始作业触发器构建器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder DailyAt(params int[] fields)
    {
        // 空检查
        if (fields == null || fields.Length == 0) throw new ArgumentNullException(nameof(fields));

        return Cron($"0 {string.Join(',', fields)} * * *", CronStringFormat.Default);
    }

    /// <summary>
    /// 创建每月1号（午夜）开始作业触发器构建器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Monthly()
    {
        return Cron("@monthly");
    }

    /// <summary>
    /// 创建每月特定天（午夜）开始作业触发器构建器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder MonthlyAt(params int[] fields)
    {
        // 空检查
        if (fields == null || fields.Length == 0) throw new ArgumentNullException(nameof(fields));

        return Cron($"0 0 {string.Join(',', fields)} * *", CronStringFormat.Default);
    }

    /// <summary>
    /// 创建每周日（午夜）开始作业触发器构建器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Weekly()
    {
        return Cron("@weekly");
    }

    /// <summary>
    /// 创建每周特定星期几（午夜）开始作业触发器构建器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder WeeklyAt(params int[] fields)
    {
        // 空检查
        if (fields == null || fields.Length == 0) throw new ArgumentNullException(nameof(fields));

        return Cron($"0 0 * * {string.Join(',', fields)}", CronStringFormat.Default);
    }

    /// <summary>
    /// 创建每周特定星期几（午夜）开始作业触发器构建器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder WeeklyAt(params string[] fields)
    {
        // 空检查
        if (fields == null || fields.Length == 0) throw new ArgumentNullException(nameof(fields));

        return Cron($"0 0 * * {string.Join(',', fields)}", CronStringFormat.Default);
    }

    /// <summary>
    /// 创建每周特定星期几（午夜）开始作业触发器构建器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder WeeklyAt(params object[] fields)
    {
        // 空检查
        if (fields == null || fields.Length == 0) throw new ArgumentNullException(nameof(fields));
        // 检查 fields 只能是 int 和字符串类型
        if (fields.Any(f => f.GetType() != typeof(int) && f.GetType() != typeof(string))) throw new InvalidOperationException("Invalid Cron expression.");

        return Cron($"0 0 * * {string.Join(',', fields)}", CronStringFormat.Default);
    }

    /// <summary>
    /// 创建每年1月1号（午夜）开始作业触发器构建器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Yearly()
    {
        return Cron("@yearly");
    }

    /// <summary>
    /// 创建每年特定月1号（午夜）开始作业触发器构建器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder YearlyAt(params int[] fields)
    {
        // 空检查
        if (fields == null || fields.Length == 0) throw new ArgumentNullException(nameof(fields));

        return Cron($"0 0 1 {string.Join(',', fields)} *", CronStringFormat.Default);
    }

    /// <summary>
    /// 创建每年特定月1号（午夜）开始作业触发器构建器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder YearlyAt(params string[] fields)
    {
        // 空检查
        if (fields == null || fields.Length == 0) throw new ArgumentNullException(nameof(fields));

        return Cron($"0 0 1 {string.Join(',', fields)} *", CronStringFormat.Default);
    }

    /// <summary>
    /// 创建每年特定月1号（午夜）开始作业触发器构建器
    /// </summary>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder YearlyAt(params object[] fields)
    {
        // 空检查
        if (fields == null || fields.Length == 0) throw new ArgumentNullException(nameof(fields));
        // 检查 fields 只能是 int 和字符串类型
        if (fields.Any(f => f.GetType() != typeof(int) && f.GetType() != typeof(string))) throw new InvalidOperationException("Invalid Cron expression.");

        return Cron($"0 0 1 {string.Join(',', fields)} *", CronStringFormat.Default);
    }
}