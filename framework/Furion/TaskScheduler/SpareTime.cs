using Furion.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Furion.TaskScheduler
{
    /// <summary>
    /// 后台业务静态类
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
        public static void Do(double interval, Action<SpareTimer, long> doWhat = default, string workerName = default, string description = default)
        {
            Do(interval, true, doWhat, workerName, description);
        }

        /// <summary>
        /// 开始执行简单任务（只执行一次）
        /// </summary>
        /// <param name="interval">时间间隔（毫秒）</param>
        /// <param name="doWhat"></param>
        /// <param name="workerName"></param>
        /// <param name="description"></param>
        public static void DoOnce(double interval, Action<SpareTimer, long> doWhat = default, string workerName = default, string description = default)
        {
            Do(interval, false, doWhat, workerName, description);
        }

        /// <summary>
        /// 模拟后台执行任务
        /// <para>10毫秒后执行</para>
        /// </summary>
        /// <param name="doWhat"></param>
        public static void DoOnce(Action doWhat = default)
        {
            if (doWhat == null) return;

            Do(10, false, (_, _) => doWhat());
        }

        /// <summary>
        /// 开始执行 Cron 表达式任务
        /// </summary>
        /// <param name="expression">Cron 表达式</param>
        /// <param name="doWhat"></param>
        /// <param name="workerName"></param>
        /// <param name="description"></param>
        public static void Do(string expression, Action<SpareTimer, long> doWhat = default, string workerName = default, string description = default)
        {
            Do(() =>
            {
                // 解析 Cron 表达式
                var cronExpression = CronExpression.Parse(expression, CronFormat.IncludeSeconds);

                // 获取下一个执行时间
                var nextTime = cronExpression.GetNextOccurrence(DateTimeOffset.Now, TimeZoneInfo.Local);
                return nextTime?.DateTime;
            }, doWhat, workerName, description);
        }

        /// <summary>
        /// 开始执行简单任务
        /// </summary>
        /// <param name="interval">时间间隔（毫秒）</param>
        /// <param name="continued">是否持续执行</param>
        /// <param name="doWhat"></param>
        /// <param name="workerName"></param>
        /// <param name="description"></param>
        public static void Do(double interval, bool continued = true, Action<SpareTimer, long> doWhat = default, string workerName = default, string description = default)
        {
            if (doWhat == null) return;

            // 创建定时器
            var timer = new SpareTimer(interval, workerName)
            {
                Type = SpareTimeTypes.Interval,
                Description = description,
                Status = SpareTimeStatus.Running
            };

            // 订阅执行事件
            timer.Elapsed += (sender, e) =>
            {
                // 记录次数
                _ = WorkerTimes.TryGetValue(workerName, out var prevTimes);
                var currentTimes = prevTimes + 1;
                _ = WorkerTimes.TryUpdate(workerName, currentTimes, prevTimes);

                // 解决重入问题
                _ = WorkerLockTimer.TryGetValue(workerName, out var inTimer);
                var beginTimer = inTimer;

                // 解决重入问题
                if (Interlocked.Exchange(ref inTimer, 1) == 0)
                {
                    // 停止任务
                    if (!continued) Cancel(timer.WorkerName);

                    // 执行任务
                    doWhat(timer, currentTimes);

                    // 处理重入问题
                    Interlocked.Exchange(ref inTimer, 0);
                    _ = WorkerLockTimer.TryUpdate(workerName, inTimer, beginTimer);
                }
            };

            timer.AutoReset = continued;
            timer.Start();
        }

        /// <summary>
        /// 开始执行下一发生时间任务
        /// </summary>
        /// <param name="nextTimeHandler">返回下一次执行时间</param>
        /// <param name="doWhat"></param>
        /// <param name="workerName"></param>
        /// <param name="description"></param>
        public static void Do(Func<DateTime?> nextTimeHandler, Action<SpareTimer, long> doWhat = default, string workerName = default, string description = default)
        {
            if (doWhat == null) return;

            workerName ??= Guid.NewGuid().ToString("N");

            // 每秒检查一次
            Do(1000, (timer, _) =>
            {
                // 获取下一个执行的时间
                var nextLocalTime = nextTimeHandler();

                if (nextLocalTime == null) Cancel(workerName);

                // 处理重复创建 Timer 问题
                if (WorkerNextTime.TryGetValue(workerName, out var _)) return;
                WorkerNextTime.TryAdd(workerName, nextLocalTime.Value);

                // 设置执行类型
                timer.Type = SpareTimeTypes.Cron;
                timer.Status = SpareTimeStatus.Running;

                // 执行任务
                var interval = (nextLocalTime.Value - DateTime.Now).TotalMilliseconds;
                DoOnce(interval, (_, times) =>
                {
                    WorkerNextTime.TryRemove(workerName, out var _);

                    doWhat(timer, times);
                }, GetSubWorkerName(workerName), description);
            }, workerName, description);
        }

        /// <summary>
        /// 开始某个任务
        /// </summary>
        /// <param name="workerName"></param>
        public static void Start(string workerName)
        {
            if (string.IsNullOrWhiteSpace(workerName)) throw new ArgumentNullException(workerName);

            // 判断任务是否存在
            if (!Workers.TryGetValue(workerName, out var timer)) return;

            // 启动任务
            if (!timer.Enabled)
            {
                timer.Status = SpareTimeStatus.Running;
                timer.Start();
            }

            // 开启子任务
            Start(GetSubWorkerName(workerName));
        }

        /// <summary>
        /// 开始某个任务
        /// </summary>
        /// <param name="workerName"></param>
        public static void Stop(string workerName)
        {
            if (string.IsNullOrWhiteSpace(workerName)) throw new ArgumentNullException(nameof(workerName));

            // 判断任务是否存在
            if (!Workers.TryGetValue(workerName, out var timer)) return;

            // 停止任务
            if (timer.Enabled)
            {
                timer.Status = SpareTimeStatus.Stopped;
                timer.Stop();
            }

            // 暂停子任务
            Stop(GetSubWorkerName(workerName));
        }

        /// <summary>
        /// 取消某个任务
        /// </summary>
        /// <param name="workerName"></param>
        public static void Cancel(string workerName)
        {
            if (string.IsNullOrWhiteSpace(workerName)) throw new ArgumentNullException(nameof(workerName));

            // 移除任务
            if (!Workers.TryRemove(workerName, out var timer)) return;
            _ = WorkerLockTimer.TryRemove(workerName, out _);
            _ = WorkerNextTime.TryRemove(workerName, out _);

            // 停止并销毁任务
            timer.Status = SpareTimeStatus.CanceledOrNone;
            timer.Stop();
            timer.Dispose();

            // 取消子任务
            Cancel(GetSubWorkerName(workerName));
        }

        /// <summary>
        /// 销毁所有任务
        /// </summary>
        public static void Dispose()
        {
            if (!Workers.Any()) return;

            foreach (var worker in Workers)
            {
                Cancel(worker.Key);
                _ = WorkerTimes.TryRemove(worker.Key, out _);
            }
        }

        /// <summary>
        /// 获取所有任务列表
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SpareTimer> GetTasks()
        {
            return Workers.Where(u => !u.Key.StartsWith(">>> "))
                          .Select(u => u.Value);
        }

        /// <summary>
        ///  获取子任务名称
        /// </summary>
        /// <param name="workerName"></param>
        /// <returns></returns>
        private static string GetSubWorkerName(string workerName)
        {
            return $">>> {workerName}";
        }

        /// <summary>
        /// 记录所有任务
        /// </summary>
        internal readonly static ConcurrentDictionary<string, SpareTimer> Workers;

        /// <summary>
        /// 任务执行次数记录器
        /// </summary>
        internal readonly static ConcurrentDictionary<string, long> WorkerTimes;

        /// <summary>
        /// 处理重入问题
        /// </summary>
        internal readonly static ConcurrentDictionary<string, long> WorkerLockTimer;

        /// <summary>
        /// 记录任务下一次执行时间
        /// </summary>
        internal readonly static ConcurrentDictionary<string, DateTime> WorkerNextTime;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static SpareTime()
        {
            Workers = new ConcurrentDictionary<string, SpareTimer>();
            WorkerTimes = new ConcurrentDictionary<string, long>();
            WorkerLockTimer = new ConcurrentDictionary<string, long>();
            WorkerNextTime = new ConcurrentDictionary<string, DateTime>();
        }
    }
}