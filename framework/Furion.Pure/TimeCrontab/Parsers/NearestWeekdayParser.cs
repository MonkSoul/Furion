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
/// Cron 字段值含 {0}W 字符解析器
/// </summary>
/// <remarks>
/// <para>表示离指定日期最近的工作日，即最后一个非周六周末日，仅在 <see cref="CrontabFieldKind.Day"/> 字段域中使用</para>
/// </remarks>
internal sealed class NearestWeekdayParser : ICronParser
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="specificValue">天数（具体值）</param>
    /// <param name="kind">Cron 字段种类</param>
    /// <exception cref="TimeCrontabException">Cron 字段种类</exception>
    public NearestWeekdayParser(int specificValue, CrontabFieldKind kind)
    {
        // 验证 {0}W 字符是否在 Day 字段域中使用
        if (kind != CrontabFieldKind.Day)
        {
            throw new TimeCrontabException(string.Format("The <{0}W> parser can only be used in the Day field.", specificValue));
        }

        // 判断天数是否在有效取值范围内
        var maximum = Constants.MaximumDateTimeValues[CrontabFieldKind.Day];
        if (specificValue <= 0 || specificValue > maximum)
        {
            throw new TimeCrontabException(string.Format("The <{0}W> is out of bounds for the Day field.", specificValue));
        }

        SpecificValue = specificValue;
        Kind = kind;
    }

    /// <summary>
    /// Cron 字段种类
    /// </summary>
    public CrontabFieldKind Kind { get; }

    /// <summary>
    /// 天数（具体值）
    /// </summary>
    public int SpecificValue { get; }

    /// <summary>
    /// 判断当前时间是否符合 Cron 字段种类解析规则
    /// </summary>
    /// <param name="datetime">当前时间</param>
    /// <returns><see cref="bool"/></returns>
    public bool IsMatch(DateTime datetime)
    {
        /*
         * W：表示有效工作日(周一到周五),只能出现在 Day 域，系统将在离指定日期的最近的有效工作日触发事件
         * 例如：在 Day 使用 5W，如果 5 日是星期六，则将在最近的工作日：星期五，即 4 日触发
         * 如果 5 日是星期天，则在 6 日(周一)触发；如果 5 日在星期一到星期五中的一天，则就在 5 日触发
         * 另外一点，W 的最近寻找不会跨过月份
         */

        // 如果这个月没有足够的天数则跳过（例如，二月没有 30 和 31 日）
        if (DateTime.DaysInMonth(datetime.Year, datetime.Month) < SpecificValue)
        {
            return false;
        }

        // 获取当前时间特定天数时间
        var specificDay = new DateTime(datetime.Year, datetime.Month, SpecificValue);

        // 最靠近的工作日时间
        DateTime closestWeekday;

        // 处理当天的不同情况
        switch (specificDay.DayOfWeek)
        {
            // 如果当天是周六，则退一天
            case DayOfWeek.Saturday:
                closestWeekday = specificDay.AddDays(-1);

                // 如果退一天不在本月，则转到下周一
                if (closestWeekday.Month != specificDay.Month)
                {
                    closestWeekday = specificDay.AddDays(2);
                }

                break;

            // 如果当天是周天，则进一天
            case DayOfWeek.Sunday:
                closestWeekday = specificDay.AddDays(1);

                // 如果进一天不在本月，则退到上周五
                if (closestWeekday.Month != specificDay.Month)
                {
                    closestWeekday = specificDay.AddDays(-2);
                }

                break;

            // 处理恰好是工作日情况，直接使用
            default:
                closestWeekday = specificDay;
                break;
        }

        return datetime.Day == closestWeekday.Day;
    }

    /// <summary>
    /// 将解析器转换成字符串输出
    /// </summary>
    /// <returns><see cref="string"/></returns>
    public override string ToString()
    {
        return string.Format("{0}W", SpecificValue);
    }
}