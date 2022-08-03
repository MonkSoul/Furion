// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
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
/// Cron 字段值含 {0}#{1} 字符解析器
/// </summary>
/// <remarks>
/// <para>表示月中第{0}个星期{1}，仅在 <see cref="CrontabFieldKind.DayOfWeek"/> 字段域中使用</para>
/// </remarks>
internal sealed class SpecificDayOfWeekInMonthParser : ICronParser
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dayOfWeek">星期，0 = 星期天，7 = 星期六</param>
    /// <param name="weekNumber">月中第几个星期</param>
    /// <param name="kind">Cron 字段种类</param>
    /// <exception cref="TimeCrontabException"></exception>
    public SpecificDayOfWeekInMonthParser(int dayOfWeek, int weekNumber, CrontabFieldKind kind)
    {
        // 验证星期数有效性
        if (weekNumber <= 0 || weekNumber > 5)
        {
            throw new TimeCrontabException(string.Format("Week number = {0} is out of bounds.", weekNumber));
        }

        // 验证 L 字符是否在 DayOfWeek 字段域中使用
        if (kind != CrontabFieldKind.DayOfWeek)
        {
            throw new TimeCrontabException(string.Format("The <{0}#{1}> parser can only be used in the Day of Week field.", dayOfWeek, weekNumber));
        }

        DayOfWeek = dayOfWeek;
        DateTimeDayOfWeek = dayOfWeek.ToDayOfWeek();
        WeekNumber = weekNumber;
        Kind = kind;
    }

    /// <summary>
    /// Cron 字段种类
    /// </summary>
    public CrontabFieldKind Kind { get; }

    /// <summary>
    /// 星期
    /// </summary>
    public int DayOfWeek { get; }

    /// <summary>
    /// <see cref="DayOfWeek"/> 类型星期
    /// </summary>
    private DayOfWeek DateTimeDayOfWeek { get; }

    /// <summary>
    /// 月中第几个星期
    /// </summary>
    public int WeekNumber { get; }

    /// <summary>
    /// 判断当前时间是否符合 Cron 字段种类解析规则
    /// </summary>
    /// <param name="datetime">当前时间</param>
    /// <returns><see cref="bool"/></returns>
    public bool IsMatch(DateTime datetime)
    {
        // 获取当前时间所在月第一天
        var currentDay = new DateTime(datetime.Year, datetime.Month, 1);

        // 第几个星期计数器
        var weekCount = 0;

        // 限制当前循环仅在本月
        while (currentDay.Month == datetime.Month)
        {
            // 首先确认星期是否相等，如果相等，则计数器 + 1
            if (currentDay.DayOfWeek == DateTimeDayOfWeek)
            {
                weekCount++;

                // 如果计算器和指定 WeekNumber 一致，则退出循环
                if (weekCount == WeekNumber)
                {
                    break;
                }

                // 否则，则追加一周（即7天）进入下一次循环
                currentDay = currentDay.AddDays(7);
            }
            // 如果星期不相等，则追加一天i将纳入下一次循环
            else
            {
                currentDay = currentDay.AddDays(1);
            }
        }

        // 如果最后计算出现跨月份情况，则不匹配
        if (currentDay.Month != datetime.Month)
        {
            return false;
        }

        return datetime.Day == currentDay.Day;
    }

    /// <summary>
    /// 将解析器转换成字符串输出
    /// </summary>
    /// <returns><see cref="string"/></returns>
    public override string ToString()
    {
        return string.Format("{0}#{1}", DayOfWeek, WeekNumber);
    }
}