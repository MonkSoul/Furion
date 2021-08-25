// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.IPCChannel;
using Furion.Templates.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Furion.TaskTimer
{
    /// <summary>
    /// 任务分发器
    /// </summary>
    /// <remarks>统一通过该类创建定时任务</remarks>
    internal sealed class TaskDispatcher : ChannelHandler<TaskMessage>
    {
        /// <summary>
        /// 并发标识
        /// </summary>
        private static int increment = 0;

        /// <summary>
        /// 调度核心代码
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async override Task InvokeAsync(TaskMessage message)
        {
            // 获取所有任务列表并循环载入任务（未完成代码）
            await CreateTask(null);
        }

        /// <summary>
        /// 创建一个任务
        /// </summary>
        /// <param name="taskMessageMetadata"></param>
        /// <returns></returns>
        private static Task CreateTask(TaskMessageMetadata taskMessageMetadata)
        {
            // 保证创建任务是原子操作，避免线程抢占
            if (Interlocked.Exchange(ref increment, 1) != 0) return Task.CompletedTask;

            // 创建取消任务 Token
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            // 创建线程信号灯，控制任务暂停和继续（Reset()：表示红灯/暂停，Set(）：标识绿灯/通行，WaitOne() ：等待信号）
            var manualResetEvent = new ManualResetEvent(true);

            // 任务绝对过期时间（毫秒），即使是不间断的任务也会受限该参数配置
            if (taskMessageMetadata.AbsoluteExpiration > 0) cancellationTokenSource.CancelAfter(taskMessageMetadata.AbsoluteExpiration);

            // 创建任务
            var task = new Task(async () =>
            {
                // 监听任务取消
                while (!cancellationToken.IsCancellationRequested)
                {
                    // 如果调度值不是一个有效的值，则取消任务
                    if (string.IsNullOrWhiteSpace(taskMessageMetadata.SchedulerValue)) cancellationTokenSource.Cancel();

                    // 等待线程信号灯（不阻塞）
                    manualResetEvent.WaitOne();

                    // 判断任务调度类型
                    switch (taskMessageMetadata.SchedulerType)
                    {
                        // 间隔任务
                        case TaskSchedulerTypes.Interval:
                            await InitiateIntervalTask(taskMessageMetadata, cancellationTokenSource);
                            break;
                        // Cron 表达式任务
                        case TaskSchedulerTypes.CronExpression:
                            await InitiateCronExpressionTask(taskMessageMetadata, cancellationTokenSource);
                            break;
                        default:
                            break;
                    };
                }
            });

            return task;
        }

        /// <summary>
        /// 初始化间隔任务
        /// </summary>
        /// <param name="taskMessageMetadata"></param>
        /// <param name="cancellationTokenSource"></param>
        /// <returns></returns>
        private static async Task InitiateIntervalTask(TaskMessageMetadata taskMessageMetadata, CancellationTokenSource cancellationTokenSource)
        {
            var isVaildInterval = int.TryParse(taskMessageMetadata.SchedulerValue.Render(), out var interval);

            // 如果不是有效的间隔值，则取消任务
            if (!isVaildInterval || interval <= 0) cancellationTokenSource.Cancel();
            else
            {
                // 执行任务
                await InvokeAsync(taskMessageMetadata);

                // 挂起线程
                await Task.Delay(interval, cancellationTokenSource.Token);
            }
        }

        /// <summary>
        /// 初始化 Cron 表达式任务
        /// </summary>
        /// <param name="taskMessageMetadata"></param>
        /// <param name="cancellationTokenSource"></param>
        /// <returns></returns>
        private static async Task InitiateCronExpressionTask(TaskMessageMetadata taskMessageMetadata, CancellationTokenSource cancellationTokenSource)
        {
            // 获取下一个触发时间
            var cronNextOccurrence = GetCronNextOccurrence(taskMessageMetadata.SchedulerValue);

            // 如果不是一个有效的时间，则取消任务
            if (cronNextOccurrence == null || cronNextOccurrence.Value.Offset.Equals(TimeSpan.Zero)) cancellationTokenSource.Cancel();

            // 判断时间是否相等
            var interval = (cronNextOccurrence.Value - DateTimeOffset.UtcNow.ToLocalTime()).TotalSeconds;
            if (Math.Floor(interval) != 0) return;

            // 延迟 100ms 后执行，解决零点问题
            await Task.Delay(100, cancellationTokenSource.Token);

            // 执行任务
            await InvokeAsync(taskMessageMetadata);
        }

        /// <summary>
        /// 调用任务
        /// </summary>
        /// <param name="taskMessageMetadata"></param>
        private static async Task InvokeAsync(TaskMessageMetadata taskMessageMetadata)
        {
            // 判断任务执行类型
            switch (taskMessageMetadata.InvokeType)
            {
                // 串行执行任务
                case TaskInvokeTypes.Serial:
                    //=============================== 这里就是调用任务触发方法（包含重试、超时） ============================
                    await Task.CompletedTask;
                    break;
                // 并行执行任务
                case TaskInvokeTypes.Parallel:
                    Parallel.Invoke(async () =>
                    {
                        //=============================== 这里就是调用任务触发方法（包含重试、超时） ============================
                        await Task.CompletedTask;
                    });
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取 Cron 表达式下一个发生时间
        /// </summary>
        /// <param name="schedulerValue"></param>
        /// <returns></returns>
        private static DateTimeOffset? GetCronNextOccurrence(string schedulerValue)
        {
            // 渲染配置模板
            var renderValue = schedulerValue.Render();

            // 分割 Cron 表达式每部分
            var cronExpressionParts = renderValue.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var cronFormat = cronExpressionParts.Length <= 5 ? CronFormat.Standard : CronFormat.IncludeSeconds;

            // 解析 Cron 表达式
            var cronExpression = CronExpression.Parse(renderValue, cronFormat);

            // 获取下一个执行时间
            var nextTime = cronExpression.GetNextOccurrence(DateTimeOffset.UtcNow, TimeZoneInfo.Local);
            return nextTime;
        }
    }
}
