using System;

namespace Furion.TaskScheduler
{
    /// <summary>
    /// Cron 表达式标识
    /// </summary>
    [Flags]
    internal enum CronExpressionFlag : byte
    {
        DayOfMonthLast = 0b00001,
        DayOfWeekLast = 0b00010,
        Interval = 0b00100,
        NearestWeekday = 0b01000,
        NthDayOfWeek = 0b10000
    }
}