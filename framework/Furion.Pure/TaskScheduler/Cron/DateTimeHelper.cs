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
    /// 日期时间帮助类
    /// </summary>
    internal static class DateTimeHelper
    {
        private static readonly TimeSpan OneSecond = TimeSpan.FromSeconds(1);

        public static DateTimeOffset FloorToSeconds(DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.AddTicks(-GetExtraTicks(dateTimeOffset.Ticks));
        }

        public static bool IsRound(DateTimeOffset dateTimeOffset)
        {
            return GetExtraTicks(dateTimeOffset.Ticks) == 0;
        }

        private static long GetExtraTicks(long ticks)
        {
            return ticks % OneSecond.Ticks;
        }
    }
}