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
    /// 处理不同平台时区的区别
    /// </summary>
    internal static class TimeZoneHelper
    {
        public static bool IsAmbiguousTime(TimeZoneInfo zone, DateTime ambiguousTime)
        {
            return zone.IsAmbiguousTime(ambiguousTime.AddTicks(1));
        }

        public static TimeSpan GetDaylightOffset(TimeZoneInfo zone, DateTime ambiguousDateTime)
        {
            var offsets = GetAmbiguousOffsets(zone, ambiguousDateTime);
            var baseOffset = zone.BaseUtcOffset;

            if (offsets[0] != baseOffset) return offsets[0];

            return offsets[1];
        }

        public static DateTimeOffset GetDaylightTimeStart(TimeZoneInfo zone, DateTime invalidDateTime)
        {
            var dstTransitionDateTime = new DateTime(invalidDateTime.Year, invalidDateTime.Month, invalidDateTime.Day,
                invalidDateTime.Hour, invalidDateTime.Minute, 0, 0, invalidDateTime.Kind);

            while (zone.IsInvalidTime(dstTransitionDateTime))
            {
                dstTransitionDateTime = dstTransitionDateTime.AddMinutes(1);
            }

            var dstOffset = zone.GetUtcOffset(dstTransitionDateTime);

            return new DateTimeOffset(dstTransitionDateTime, dstOffset);
        }

        public static DateTimeOffset GetStandardTimeStart(TimeZoneInfo zone, DateTime ambiguousTime, TimeSpan daylightOffset)
        {
            var dstTransitionEnd = GetDstTransitionEndDateTime(zone, ambiguousTime);

            return new DateTimeOffset(dstTransitionEnd, daylightOffset).ToOffset(zone.BaseUtcOffset);
        }

        public static DateTimeOffset GetAmbiguousIntervalEnd(TimeZoneInfo zone, DateTime ambiguousTime)
        {
            var dstTransitionEnd = GetDstTransitionEndDateTime(zone, ambiguousTime);

            return new DateTimeOffset(dstTransitionEnd, zone.BaseUtcOffset);
        }

        public static DateTimeOffset GetDaylightTimeEnd(TimeZoneInfo zone, DateTime ambiguousTime, TimeSpan daylightOffset)
        {
            var daylightTransitionEnd = GetDstTransitionEndDateTime(zone, ambiguousTime);

            return new DateTimeOffset(daylightTransitionEnd.AddTicks(-1), daylightOffset);
        }

        private static TimeSpan[] GetAmbiguousOffsets(TimeZoneInfo zone, DateTime ambiguousTime)
        {
            return zone.GetAmbiguousTimeOffsets(ambiguousTime.AddTicks(1));
        }

        private static DateTime GetDstTransitionEndDateTime(TimeZoneInfo zone, DateTime ambiguousDateTime)
        {
            var dstTransitionDateTime = new DateTime(ambiguousDateTime.Year, ambiguousDateTime.Month, ambiguousDateTime.Day,
                ambiguousDateTime.Hour, ambiguousDateTime.Minute, 0, 0, ambiguousDateTime.Kind);

            while (zone.IsAmbiguousTime(dstTransitionDateTime))
            {
                dstTransitionDateTime = dstTransitionDateTime.AddMinutes(1);
            }

            return dstTransitionDateTime;
        }
    }
}