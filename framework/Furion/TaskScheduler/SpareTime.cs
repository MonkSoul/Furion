using Furion.DependencyInjection;
using System;
using System.Timers;

namespace Furion.TaskScheduler
{
    /// <summary>
    /// 后台业务静态类
    /// </summary>
    [SkipScan]
    public static class SpareTime
    {
        /// <summary>
        /// 执行简单任务
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="continued"></param>
        /// <param name="doWhat"></param>
        /// <param name="timer"></param>
        public static void Do(double interval, bool continued = true, Action<Timer> doWhat = default, Timer timer = default)
        {
            if (doWhat == null) return;

            timer ??= new Timer(interval);
            timer.Elapsed += (sender, e) =>
            {
                if (!continued)
                {
                    timer.Stop();
                    timer.Dispose();
                }

                doWhat(timer);
            };
            timer.AutoReset = continued;
            timer.Start();
        }

        /// <summary>
        /// 执行简单任务（持续的）
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="doWhat"></param>
        /// <param name="timer"></param>
        public static void Do(double interval, Action<Timer> doWhat = default, Timer timer = default)
        {
            Do(interval, true, doWhat, timer);
        }

        /// <summary>
        /// 执行简单任务（只执行一次）
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="doWhat"></param>
        /// <param name="timer"></param>
        public static void DoOnce(double interval, Action<Timer> doWhat = default, Timer timer = default)
        {
            Do(interval, false, doWhat, timer);
        }

        /// <summary>
        /// 执行 Cron 表达式任务
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="doWhat"></param>
        /// <param name="timer"></param>
        public static void Do(string expression, Action<Timer> doWhat = default, Timer timer = default)
        {
            if (doWhat == null) return;

            Do(1000, e =>
            {
                // 解析 Cron 表达式
                var cronExpression = CronExpression.Parse(expression, CronFormat.IncludeSeconds);

                // 获取下一个执行时间
                var nextTime = cronExpression.GetNextOccurrence(DateTimeOffset.Now, TimeZoneInfo.Local);
                var nextLocalTime = nextTime?.DateTime;
                if (nextLocalTime == null) e.Stop();

                DoOnce((nextLocalTime.Value - DateTime.Now).TotalSeconds, se => doWhat(se));
            }, timer);
        }
    }
}