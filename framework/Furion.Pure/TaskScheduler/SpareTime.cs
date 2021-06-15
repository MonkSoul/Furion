// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.8.7
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Furion.TaskScheduler
{
    /// <summary>
    /// 后台任务静态类
    /// </summary>
    [SkipScan]
    public static class SpareTime
    {
        /// <summary>
        /// 开始执行简单任务（持续的）
        /// </summary>
        /// <param name="interval">时间间隔（毫秒）</param>
        /// <param name="doWhat"></param>
        /// <param name="workerName"></param>
        /// <param name="description"></param>
        /// <param name="startNow"></param>
        /// <param name="cancelInNoneNextTime"></param>
        /// <param name="executeType"></param>
        public static void Do(double interval, Action<SpareTimer, long> doWhat = default, string workerName = default, string description = default, bool startNow = true, bool cancelInNoneNextTime = true, SpareTimeExecuteTypes executeType = SpareTimeExecuteTypes.Parallel)
        {
            Do(() => interval, doWhat, workerName, description, startNow, cancelInNoneNextTime, executeType, true);
        }

        /// <summary>
        /// 开始执行简单任务（只执行一次）
        /// </summary>
        /// <param name="interval">时间间隔（毫秒）</param>
        /// <param name="doWhat"></param>
        /// <param name="workerName"></param>
        /// <param name="description"></param>
        /// <param name="startNow"></param>
        /// <param name="cancelInNoneNextTime"></param>
        /// <param name="executeType"></param>
        public static void DoOnce(double interval, Action<SpareTimer, long> doWhat = default, string workerName = default, string description = default, bool startNow = true, bool cancelInNoneNextTime = true, SpareTimeExecuteTypes executeType = SpareTimeExecuteTypes.Parallel)
        {
            Do(() => interval, doWhat, workerName, description, startNow, cancelInNoneNextTime, executeType, false);
        }

        /// <summary>
        /// 模拟后台执行任务
        /// <para>10毫秒后执行</para>
        /// </summary>
        /// <param name="doWhat"></param>
        /// <param name="interval"></param>
        /// <param name="cancelInNoneNextTime"></param>
        /// <param name="executeType"></param>
        public static void DoIt(Action doWhat = default, double interval = 30, bool cancelInNoneNextTime = true, SpareTimeExecuteTypes executeType = SpareTimeExecuteTypes.Parallel)
        {
            if (doWhat == null) return;

            DoOnce(interval, async (_, _) =>
            {
                doWhat();
                await Task.CompletedTask;
            }, cancelInNoneNextTime: cancelInNoneNextTime, executeType: executeType);
        }

        /// <summary>
        /// 开始执行 Cron 表达式任务
        /// </summary>
        /// <param name="expression">Cron 表达式</param>
        /// <param name="doWhat"></param>
        /// <param name="workerName"></param>
        /// <param name="description"></param>
        /// <param name="startNow"></param>
        /// <param name="cancelInNoneNextTime"></param>
        /// <param name="cronFormat">配置 Cron 表达式格式化</param>
        /// <param name="executeType"></param>
        public static void Do(string expression, Action<SpareTimer, long> doWhat = default, string workerName = default, string description = default, bool startNow = true, bool cancelInNoneNextTime = true, CronFormat cronFormat = CronFormat.Standard, SpareTimeExecuteTypes executeType = SpareTimeExecuteTypes.Parallel)
        {
            Do(() => GetCronNextOccurrence(expression, cronFormat), doWhat, workerName, description, startNow, cancelInNoneNextTime, executeType);
        }

        /// <summary>
        /// 开始执行简单任务
        /// </summary>
        /// <param name="intervalHandler">时间间隔（毫秒）</param>
        /// <param name="doWhat"></param>
        /// <param name="workerName"></param>
        /// <param name="description"></param>
        /// <param name="startNow"></param>
        /// <param name="cancelInNoneNextTime"></param>
        /// <param name="executeType"></param>
        /// <param name="continued">是否持续执行</param>
        public static void Do(Func<double> intervalHandler, Action<SpareTimer, long> doWhat = default, string workerName = default, string description = default, bool startNow = true, bool cancelInNoneNextTime = true, SpareTimeExecuteTypes executeType = SpareTimeExecuteTypes.Parallel, bool continued = true)
        {
            if (doWhat == null) return;

            // 自动生成任务名称
            workerName ??= Guid.NewGuid().ToString("N");

            // 获取执行间隔
            var interval = intervalHandler();

            // 判断是否在下一个空时间取消任务
            if (cancelInNoneNextTime)
            {
                if (interval <= 0)
                {
                    Cancel(workerName);
                    return;
                }
            }
            else
            {
                if (interval <= 0) return;
            }

            // 创建定时器
            var timer = new SpareTimer(interval, workerName)
            {
                Type = SpareTimeTypes.Interval,
                Description = description,
                Status = startNow ? SpareTimeStatus.Running : SpareTimeStatus.Stopped,
                ExecuteType = executeType
            };

            // 订阅执行事件
            timer.Elapsed += async (sender, e) =>
            {
                // 获取当前任务的记录
                _ = WorkerRecords.TryGetValue(workerName, out var currentRecord);

                // 处理串行执行问题
                if (timer.ExecuteType == SpareTimeExecuteTypes.Serial)
                {
                    if (!currentRecord.IsCompleteOfPrev) return;

                    // 立即更新多线程状态
                    currentRecord.IsCompleteOfPrev = false;
                    UpdateWorkerRecord(workerName, currentRecord);
                }

                // 记录执行次数
                if (timer.Type == SpareTimeTypes.Interval) currentRecord.Timer.Tally = timer.Tally = currentRecord.Tally += 1;

                // 处理多线程并发问题（重入问题）
                var interlocked = currentRecord.Interlocked;
                if (Interlocked.Exchange(ref interlocked, 1) == 0)
                {
                    try
                    {
                        // 执行任务
                        doWhat(timer, currentRecord.Tally);
                    }
                    catch (Exception ex)
                    {
                        // 记录任务异常
                        currentRecord.Timer.Exception.TryAdd(currentRecord.Tally, ex);

                        // 如果任务执行超过 10 次失败，则停止任务
                        if (currentRecord.Timer.Exception.Count > 10)
                        {
                            Stop(workerName, true);
                        }
                    }
                    finally
                    {
                        // 处理串行执行问题
                        currentRecord.IsCompleteOfPrev = true;

                        // 更新任务记录
                        UpdateWorkerRecord(workerName, currentRecord);
                    }

                    // 如果间隔小于或等于 0 取消任务
                    if (interval <= 0) Cancel(workerName);

                    // 停止任务
                    if (!continued) Cancel(workerName);

                    // 处理重入问题
                    Interlocked.Exchange(ref interlocked, 0);

                    await Task.CompletedTask;
                }
            };

            timer.AutoReset = continued;
            if (startNow) timer.Start();
        }

        /// <summary>
        /// 开始执行下一发生时间任务
        /// </summary>
        /// <param name="nextTimeHandler">返回下一次执行时间</param>
        /// <param name="doWhat"></param>
        /// <param name="workerName"></param>
        /// <param name="description"></param>
        /// <param name="startNow"></param>
        /// <param name="cancelInNoneNextTime">在下一个空时间取消任务</param>
        /// <param name="executeType"></param>
        public static void Do(Func<DateTime?> nextTimeHandler, Action<SpareTimer, long> doWhat = default, string workerName = default, string description = default, bool startNow = true, bool cancelInNoneNextTime = true, SpareTimeExecuteTypes executeType = SpareTimeExecuteTypes.Parallel)
        {
            if (doWhat == null) return;

            // 每秒检查一次
            Do(1000, async (timer, tally) =>
            {
                // 获取下一个执行的时间
                var nextLocalTime = nextTimeHandler();

                // 判断是否在下一个空时间取消任务
                if (cancelInNoneNextTime)
                {
                    if (nextLocalTime == null)
                    {
                        Cancel(workerName);
                        return;
                    }
                }
                else
                {
                    if (nextLocalTime == null) return;
                }

                // 获取当前任务的记录
                _ = WorkerRecords.TryGetValue(workerName, out var currentRecord);

                // 更新任务信息
                currentRecord.Timer.Type = timer.Type = SpareTimeTypes.Cron;
                currentRecord.Timer.Status = timer.Status = SpareTimeStatus.Running;
                currentRecord.Timer.Tally = timer.Tally = currentRecord.CronActualTally;

                // 只有时间相等才触发
                var interval = (nextLocalTime.Value - DateTime.Now).TotalSeconds;
                if (Math.Floor(interval) != 0)
                {
                    UpdateWorkerRecord(workerName, currentRecord);
                    return;
                }

                // 更新实际执行次数
                currentRecord.Timer.Tally = timer.Tally = currentRecord.CronActualTally += 1;
                UpdateWorkerRecord(workerName, currentRecord);

                // 执行方法
                doWhat(timer, currentRecord.CronActualTally);

                await Task.CompletedTask;
            }, workerName, description, startNow, cancelInNoneNextTime, executeType);
        }

        /// <summary>
        /// 开始简单任务（持续的）
        /// <para>用于 Worker Services</para>
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="doWhat"></param>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public static Task DoAsync(int interval, Action doWhat, CancellationToken stoppingToken)
        {
            return DoAsync(() => interval, doWhat, stoppingToken);
        }

        /// <summary>
        /// 开始简单任务（持续的）
        /// <para>用于 Worker Services</para>
        /// </summary>
        /// <param name="intervalHandler"></param>
        /// <param name="doWhat"></param>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public static Task DoAsync(Func<int> intervalHandler, Action doWhat, CancellationToken stoppingToken)
        {
            if (doWhat == null) return Task.CompletedTask;

            var interval = intervalHandler();
            if (interval <= 0) return Task.CompletedTask;

            try
            {
                doWhat();
            }
            catch { }
            finally { }

            return Task.Delay(interval, stoppingToken);
        }

        /// <summary>
        /// 开始 Cron 表达式任务（持续的）
        /// <para>用于 Worker Services</para>
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="doWhat"></param>
        /// <param name="stoppingToken"></param>
        /// <param name="cronFormat"></param>
        /// <returns></returns>
        public static Task DoAsync(string expression, Action doWhat, CancellationToken stoppingToken, CronFormat cronFormat = CronFormat.Standard)
        {
            return DoAsync(() => GetCronNextOccurrence(expression, cronFormat), doWhat, stoppingToken);
        }

        /// <summary>
        /// 开始 Cron 表达式任务（持续的）
        /// <para>用于 Worker Services</para>
        /// </summary>
        /// <param name="nextTimeHandler"></param>
        /// <param name="doWhat"></param>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public static Task DoAsync(Func<DateTime?> nextTimeHandler, Action doWhat, CancellationToken stoppingToken)
        {
            if (doWhat == null) return Task.CompletedTask;

            // 计算下一次执行时间
            var nextLocalTime = nextTimeHandler();
            if (nextLocalTime == null) return Task.CompletedTask;

            // 只有时间相等才触发
            var interval = (nextLocalTime.Value - DateTime.Now).TotalSeconds;
            if (Math.Floor(interval) != 0) return Task.CompletedTask;

            try
            {
                doWhat();
            }
            catch { }
            finally { }

            return Task.Delay(1000, stoppingToken);
        }

        /// <summary>
        /// 开始某个任务
        /// </summary>
        /// <param name="workerName"></param>
        public static void Start(string workerName)
        {
            if (string.IsNullOrWhiteSpace(workerName)) throw new ArgumentNullException(workerName);

            // 判断任务是否存在
            if (!WorkerRecords.TryGetValue(workerName, out var workerRecord)) return;

            // 获取定时器
            var timer = workerRecord.Timer;

            // 启动任务
            if (!timer.Enabled)
            {
                // 如果任务过去是失败的，则清楚异常信息后启动
                if (timer.Status == SpareTimeStatus.Failed) timer.Exception.Clear();

                timer.Status = SpareTimeStatus.Running;
                timer.Start();
            }
        }

        /// <summary>
        /// 停止某个任务
        /// </summary>
        /// <param name="workerName"></param>
        /// <param name="isFaild"></param>
        public static void Stop(string workerName, bool isFaild = false)
        {
            if (string.IsNullOrWhiteSpace(workerName)) throw new ArgumentNullException(nameof(workerName));

            // 判断任务是否存在
            if (!WorkerRecords.TryGetValue(workerName, out var workerRecord)) return;

            // 获取定时器
            var timer = workerRecord.Timer;

            // 停止任务
            if (timer.Enabled)
            {
                timer.Status = !isFaild ? SpareTimeStatus.Stopped : SpareTimeStatus.Failed;
                timer.Stop();
            }
        }

        /// <summary>
        /// 取消某个任务
        /// </summary>
        /// <param name="workerName"></param>
        public static void Cancel(string workerName)
        {
            if (string.IsNullOrWhiteSpace(workerName)) throw new ArgumentNullException(nameof(workerName));

            // 判断任务是否存在
            if (!WorkerRecords.TryRemove(workerName, out var workerRecord)) return;

            // 获取定时器
            var timer = workerRecord.Timer;

            // 停止并销毁任务
            timer.Status = SpareTimeStatus.CanceledOrNone;
            timer.Stop();
            timer.Dispose();
        }

        /// <summary>
        /// 销毁所有任务
        /// </summary>
        public static void Dispose()
        {
            if (!WorkerRecords.Any()) return;

            foreach (var workerRecord in WorkerRecords)
            {
                Cancel(workerRecord.Key);
            }
        }

        /// <summary>
        /// 获取所有任务列表
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SpareTimer> GetWorkers()
        {
            return WorkerRecords.Select(u => u.Value.Timer);
        }

        /// <summary>
        /// 获取 Cron 表达式下一个发生时间
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cronFormat"></param>
        /// <returns></returns>
        public static DateTime? GetCronNextOccurrence(string expression, CronFormat cronFormat = CronFormat.Standard)
        {
            // 解析 Cron 表达式
            var cronExpression = CronExpression.Parse(expression, cronFormat);

            // 获取下一个执行时间
            var nextTime = cronExpression.GetNextOccurrence(DateTimeOffset.Now, TimeZoneInfo.Local);
            return nextTime?.DateTime;
        }

        /// <summary>
        /// 更新工作记录
        /// </summary>
        /// <param name="workerName"></param>
        /// <param name="newRecord"></param>
        private static void UpdateWorkerRecord(string workerName, WorkerRecord newRecord)
        {
            _ = WorkerRecords.TryGetValue(workerName, out var currentRecord);
            _ = WorkerRecords.TryUpdate(workerName, newRecord, currentRecord);
        }

        /// <summary>
        /// 记录任务
        /// </summary>
        internal static readonly ConcurrentDictionary<string, WorkerRecord> WorkerRecords;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static SpareTime()
        {
            WorkerRecords = new ConcurrentDictionary<string, WorkerRecord>();
        }
    }
}