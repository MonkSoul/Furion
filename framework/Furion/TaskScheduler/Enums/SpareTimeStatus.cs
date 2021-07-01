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
using System.ComponentModel;

namespace Furion.TaskScheduler
{
    /// <summary>
    /// 任务状态
    /// </summary>
    [SuppressSniffer]
    public enum SpareTimeStatus
    {
        /// <summary>
        /// 运行中
        /// </summary>
        [Description("运行中")]
        Running,

        /// <summary>
        /// 已停止或未启动
        /// </summary>
        [Description("已停止或未启动")]
        Stopped,

        /// <summary>
        /// 单次执行失败
        /// </summary>
        [Description("任务停止并失败")]
        Failed,

        /// <summary>
        /// 任务已取消或没有该任务
        /// </summary>
        [Description("任务已取消或没有该任务")]
        CanceledOrNone
    }
}