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

namespace Furion.TimeCrontab;

/// <summary>
/// Cron 表达式抽象类
/// </summary>
/// <remarks>主要将 Cron 表达式转换成 OOP 类进行操作</remarks>
public sealed partial class Crontab
{
    /// <summary>
    /// 创建指定特定秒开始作业触发器构建器
    /// </summary>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="Crontab"/></returns>
    public static Crontab SecondlyAt(params object[] fields)
    {
        // 检查字段合法性
        CheckFieldsNotNullOrEmpty(fields);

        return Parse($"{FieldsToString(fields)} * * * * *", CronStringFormat.WithSeconds);
    }

    /// <summary>
    /// 创建每分钟特定秒开始作业触发器构建器
    /// </summary>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="Crontab"/></returns>
    public static Crontab MinutelyAt(params object[] fields)
    {
        // 检查字段合法性
        CheckFieldsNotNullOrEmpty(fields);

        return Parse($"{FieldsToString(fields)} * * * * *", CronStringFormat.WithSeconds);
    }

    /// <summary>
    /// 创建每小时特定分钟开始作业触发器构建器
    /// </summary>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="Crontab"/></returns>
    public static Crontab HourlyAt(params object[] fields)
    {
        // 检查字段合法性
        CheckFieldsNotNullOrEmpty(fields);

        return Parse($"{FieldsToString(fields)} * * * *", CronStringFormat.Default);
    }

    /// <summary>
    /// 创建每天特定小时开始作业触发器构建器
    /// </summary>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="Crontab"/></returns>
    public static Crontab DailyAt(params object[] fields)
    {
        // 检查字段合法性
        CheckFieldsNotNullOrEmpty(fields);

        return Parse($"0 {FieldsToString(fields)} * * *", CronStringFormat.Default);
    }

    /// <summary>
    /// 创建每月特定天（午夜）开始作业触发器构建器
    /// </summary>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="Crontab"/></returns>
    public static Crontab MonthlyAt(params object[] fields)
    {
        // 检查字段合法性
        CheckFieldsNotNullOrEmpty(fields);

        return Parse($"0 0 {FieldsToString(fields)} * *", CronStringFormat.Default);
    }

    /// <summary>
    /// 创建每周特定星期几（午夜）开始作业触发器构建器
    /// </summary>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="Crontab"/></returns>
    public static Crontab WeeklyAt(params object[] fields)
    {
        // 检查字段合法性
        CheckFieldsNotNullOrEmpty(fields);

        return Parse($"0 0 * * {FieldsToString(fields)}", CronStringFormat.Default);
    }

    /// <summary>
    /// 创建每年特定月1号（午夜）开始作业触发器构建器
    /// </summary>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="Crontab"/></returns>
    public static Crontab YearlyAt(params object[] fields)
    {
        // 检查字段合法性
        CheckFieldsNotNullOrEmpty(fields);

        return Parse($"0 0 1 {FieldsToString(fields)} *", CronStringFormat.Default);
    }

    /// <summary>
    /// 检查字段域 非 Null 非空数组
    /// </summary>
    /// <param name="fields">字段值</param>
    private static void CheckFieldsNotNullOrEmpty(params object[] fields)
    {
        // 空检查
        if (fields == null || fields.Length == 0) throw new ArgumentNullException(nameof(fields));

        // 检查 fields 只能是 int，string 和非 null 类型
        if (fields.Any(f => f == null || (f.GetType() != typeof(int) && f.GetType() != typeof(string)))) throw new InvalidOperationException("Invalid Cron expression.");
    }

    /// <summary>
    /// 将字段域转换成 string
    /// </summary>
    /// <param name="fields">字段值</param>
    /// <returns><see cref="string"/></returns>
    private static string FieldsToString(params object[] fields)
    {
        return string.Join(",", fields);
    }
}