// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

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