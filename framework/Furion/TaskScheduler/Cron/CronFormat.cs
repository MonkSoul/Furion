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

using Furion.DependencyInjection;
using System;
using System.ComponentModel;

namespace Furion.TaskScheduler
{
    /// <summary>
    /// Cron 表达式支持类型
    /// </summary>
    [SuppressSniffer, Flags]
    public enum CronFormat
    {
        /// <summary>
        /// 只有 5 个字符：分钟，小时，月/天，天，周/天
        /// </summary>
        [Description("只有 5 个字符：分钟，小时，月/天，天，周/天")]
        Standard = 0,

        /// <summary>
        /// 支持秒解析，也就是 6 个字符
        /// </summary>
        [Description("支持秒解析，也就是 6 个字符")]
        IncludeSeconds = 1
    }
}