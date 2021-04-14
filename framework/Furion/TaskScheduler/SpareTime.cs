using Furion.DependencyInjection;
using System;
using System.Collections.Concurrent;

namespace Furion.TaskScheduler
{
    /// <summary>
    /// 后台业务静态类
    /// </summary>
    [SkipScan]
    public static class SpareTime
    {
        /// <summary>
        /// 记录所有任务
        /// </summary>
        internal static ConcurrentDictionary<string, SpareTimer> Workers;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static SpareTime()
        {
            Workers = new ConcurrentDictionary<string, SpareTimer>();
        }

        /// <summary>
        /// 开始执行简单任务
        /// </summary>
        /// <param name="interval">时间间隔（毫秒）</param>
        /// <param name="continued">是否持续执行</param>
        /// <param name="doWhat"></param>
        /// <param name="workerName"></param>
        public static void Do(double interval, bool continued = true, Action<SpareTimer> doWhat = default, string workerName = default)
        {
            if (doWhat == null) return;

            var timer = new SpareTimer(interval, workerName);
            timer.Elapsed += (sender, e) =>
            {
                // 停止任务
                if (!continued) Cancel(timer.WorkerName);

                doWhat(timer);
            };
            timer.AutoReset = continued;
            timer.Start();
        }

        /// <summary>
        /// 开始执行简单任务（持续的）
        /// </summary>
        /// <param name="interval">时间间隔（毫秒）</param>
        /// <param name="doWhat"></param>
        /// <param name="workerName"></param>
        public static void Do(double interval, Action<SpareTimer> doWhat = default, string workerName = default)
        {
            Do(interval, true, doWhat, workerName);
        }

        /// <summary>
        /// 开始执行简单任务（只执行一次）
        /// </summary>
        /// <param name="interval">时间间隔（毫秒）</param>
        /// <param name="doWhat"></param>
        /// <param name="workerName"></param>
        public static void DoOnce(double interval, Action<SpareTimer> doWhat = default, string workerName = default)
        {
            Do(interval, false, doWhat, workerName);
        }

        /// <summary>
        /// 开始执行 Cron 表达式任务
        /// </summary>
        /// <param name="expression">Cron 表达式</param>
        /// <param name="doWhat"></param>
        /// <param name="workerName"></param>
        public static void Do(string expression, Action<SpareTimer> doWhat = default, string workerName = default)
        {
            if (doWhat == null) return;

            workerName ??= Guid.NewGuid().ToString("N");

            Do(1000, timer =>
            {
                // 解析 Cron 表达式
                var cronExpression = CronExpression.Parse(expression, CronFormat.IncludeSeconds);

                // 获取下一个执行时间
                var nextTime = cronExpression.GetNextOccurrence(DateTimeOffset.Now, TimeZoneInfo.Local);
                var nextLocalTime = nextTime?.DateTime;

                // 停止任务
                if (nextLocalTime == null) Cancel(timer.WorkerName);

                // 执行任务
                DoOnce((nextLocalTime.Value - DateTime.Now).TotalSeconds, subTimer => doWhat(subTimer), $"{workerName}[{expression}]");
            }, workerName);
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
            if (!timer.Enabled) timer.Start();
        }

        /// <summary>
        /// 开始某个任务
        /// </summary>
        /// <param name="workerName"></param>
        public static void Stop(string workerName)
        {
            if (string.IsNullOrWhiteSpace(workerName)) throw new ArgumentNullException(workerName);

            // 判断任务是否存在
            if (!Workers.TryGetValue(workerName, out var timer)) return;

            // 停止任务
            if (timer.Enabled) timer.Stop();
        }

        /// <summary>
        /// 取消某个任务
        /// </summary>
        /// <param name="workerName"></param>
        public static void Cancel(string workerName)
        {
            if (string.IsNullOrWhiteSpace(workerName)) throw new ArgumentNullException(workerName);

            // 判断任务是否存在
            if (!Workers.TryGetValue(workerName, out var timer)) return;

            if (timer.Enabled)
            {
                timer.Stop();
                timer.Dispose();
            }

            // 移除
            _ = Workers.TryRemove(workerName, out _);
        }
    }
}