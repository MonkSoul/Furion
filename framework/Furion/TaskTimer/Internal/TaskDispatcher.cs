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
        public override Task InvokeAsync(TaskMessage message)
        {
            // 测试间隔任务
            //CreateTask(new TaskMessageMetadata
            //{
            //    SchedulerType = TaskSchedulerTypes.Interval,
            //    SchedulerValue = "1000",
            //    InvokeType = TaskInvokeTypes.Parallel
            //}).Start();

            // 测试 Cron 表达式任务
            //CreateTask(new TaskMessageMetadata
            //{
            //    SchedulerType = TaskSchedulerTypes.CronExpression,
            //    SchedulerValue = "*/5 * * * * *",
            //    InvokeType = TaskInvokeTypes.Parallel
            //}).Start();

            // 测试超时任务
            CreateTask(new TaskMessageMetadata
            {
                SchedulerType = TaskSchedulerTypes.CronExpression,
                SchedulerValue = "*/5 * * * * *",
                InvokeType = TaskInvokeTypes.Parallel,
                InvokeTimeout = 5000
            }).Start();

            return Task.CompletedTask;
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
                await InvokeAsync(taskMessageMetadata, cancellationTokenSource);

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

            // 只有时间相等才触发
            var totalMilliseconds = (cronNextOccurrence.Value - DateTimeOffset.UtcNow.ToLocalTime()).TotalMilliseconds;
            if (Math.Round(Math.Round(totalMilliseconds, 3, MidpointRounding.ToEven)) > 30) return;

            // 延迟 100ms 后执行，解决零点问题
            await Task.Delay(100, cancellationTokenSource.Token);

            // 执行任务
            await InvokeAsync(taskMessageMetadata, cancellationTokenSource);

            // 每 30ms 检查一次
            await Task.Delay(30, cancellationTokenSource.Token);
        }

        /// <summary>
        /// 调用任务
        /// </summary>
        /// <param name="taskMessageMetadata"></param>
        /// <param name="cancellationTokenSource"></param>
        private static async Task InvokeAsync(TaskMessageMetadata taskMessageMetadata, CancellationTokenSource cancellationTokenSource)
        {
            // 创建一个关联取消任务 Token ，用于执行任务处理程序
            var refCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationTokenSource.Token);
            var refCancellationToken = refCancellationTokenSource.Token;

            // 判断任务执行类型
            switch (taskMessageMetadata.InvokeType)
            {
                // 串行执行任务
                case TaskInvokeTypes.Serial:
                    await InvokeHandlerAsync(taskMessageMetadata, cancellationTokenSource);
                    break;
                // 并行执行任务
                case TaskInvokeTypes.Parallel:
                    Parallel.Invoke(async () => await InvokeHandlerAsync(taskMessageMetadata, cancellationTokenSource));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 调用任务 Handler 处理程序
        /// </summary>
        /// <param name="taskMessageMetadata"></param>
        /// <param name="cancellationTokenSource"></param>
        private static async Task InvokeHandlerAsync(TaskMessageMetadata taskMessageMetadata, CancellationTokenSource cancellationTokenSource)
        {
            // 创建一个关联取消任务 Token ，用于执行任务处理程序
            var refCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationTokenSource.Token);
            var refCancellationToken = refCancellationTokenSource.Token;

            var timeoutTask = Task.Delay(taskMessageMetadata.InvokeTimeout > 0 ? taskMessageMetadata.InvokeTimeout : int.MaxValue);
            var handlerTask = new Task(async () =>
            {
                // 监听任务取消
                while (!refCancellationToken.IsCancellationRequested)
                {
                    //=============================== 这里就是调用任务触发方法（包含重试、超时） ============================

                    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    Thread.Sleep(1000); // 模拟超时任务

                    await Task.CompletedTask;
                    break;  // 跳出
                }
            });
            handlerTask.Start();
            // 控制超时
            var resultTask = await Task.WhenAny(handlerTask, timeoutTask);
            if (resultTask == timeoutTask)
            {
                Console.WriteLine($"已经超时：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                refCancellationTokenSource.Cancel();
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
