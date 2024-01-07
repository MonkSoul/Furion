// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.TimeCrontab;

/// <summary>
/// <see cref="DayOfWeek"/> 拓展类
/// </summary>
internal static class DayOfWeekExtensions
{
    /// <summary>
    /// 将 C# 中 <see cref="DayOfWeek"/> 枚举元素转换成数值
    /// </summary>
    /// <param name="dayOfWeek"><see cref="DayOfWeek"/> 枚举</param>
    /// <returns><see cref="int"/></returns>
    internal static int ToCronDayOfWeek(this DayOfWeek dayOfWeek)
    {
        return Constants.CronDays[dayOfWeek];
    }

    /// <summary>
    /// 将数值转换成 C# 中 <see cref="DayOfWeek"/> 枚举元素
    /// </summary>
    /// <param name="dayOfWeek"></param>
    /// <returns></returns>
    internal static DayOfWeek ToDayOfWeek(this int dayOfWeek)
    {
        return Constants.CronDays.First(x => x.Value == dayOfWeek).Key;
    }

    /// <summary>
    /// 获取当前年月最后一个星期几
    /// </summary>
    /// <param name="dayOfWeek">星期几，<see cref="DayOfWeek"/> 类型</param>
    /// <param name="year">年</param>
    /// <param name="month">月</param>
    /// <returns><see cref="int"/></returns>
    internal static int LastDayOfMonth(this DayOfWeek dayOfWeek, int year, int month)
    {
        var daysInMonth = DateTime.DaysInMonth(year, month);
        var currentDay = new DateTime(year, month, daysInMonth);

        // 从月底天数进行递归查找
        while (currentDay.DayOfWeek != dayOfWeek)
        {
            currentDay = currentDay.AddDays(-1);
        }

        return currentDay.Day;
    }
}