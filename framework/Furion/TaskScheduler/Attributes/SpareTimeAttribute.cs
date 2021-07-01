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
using Furion.Templates.Extensions;
using System;

namespace Furion.TaskScheduler
{
    /// <summary>
    /// 配置定时任务特性
    /// </summary>
    [SuppressSniffer, AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SpareTimeAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="workerName"></param>
        public SpareTimeAttribute(double interval, string workerName = default)
        {
            Interval = interval;
            Type = SpareTimeTypes.Interval;
            WorkerName = workerName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="expressionOrKey">表达式或配置Key</param>
        /// <param name="workName"></param>
        public SpareTimeAttribute(string expressionOrKey, string workName = default)
        {
            // 支持从配置文件读取
            var realValue = expressionOrKey.Render();

            // 如果能够转换成整型，则采用间隔
            if (long.TryParse(realValue, out var interval))
            {
                Interval = interval;
                Type = SpareTimeTypes.Interval;
            }
            // 否则当作 Cron 表达式
            else
            {
                CronExpression = realValue;
                Type = SpareTimeTypes.Cron;
            }

            WorkerName = workName;
        }

        /// <summary>
        /// 间隔
        /// </summary>
        public double Interval { get; private set; }

        /// <summary>
        /// Cron 表达式
        /// </summary>
        public string CronExpression { get; private set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string WorkerName { get; private set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        internal SpareTimeTypes Type { get; private set; }

        /// <summary>
        /// 任务描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 只执行一次
        /// </summary>
        public bool DoOnce { get; set; } = false;

        /// <summary>
        /// 立即执行（默认等待启动）
        /// </summary>
        public bool StartNow { get; set; } = false;

        /// <summary>
        /// 执行类型
        /// </summary>
        public SpareTimeExecuteTypes ExecuteType { get; set; } = SpareTimeExecuteTypes.Parallel;

        /// <summary>
        /// Cron 表达式格式化方式
        /// </summary>
        public object CronFormat { get; set; }
    }
}