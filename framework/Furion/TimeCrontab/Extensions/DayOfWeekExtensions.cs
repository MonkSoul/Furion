// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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