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