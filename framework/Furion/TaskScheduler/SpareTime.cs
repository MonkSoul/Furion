using Furion.DependencyInjection;
using System;
using System.Timers;

namespace Furion.TaskScheduler
{
    /// <summary>
    /// 开启后台任务静态类
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
        public static void Do(double interval, bool continued = true, Action<Timer> doWhat = default)
        {
            if (doWhat == null) return;

            var timer = new Timer(interval);
            timer.Elapsed += (sender, e) =>
            {
                if (!continued) timer.Stop();

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
        public static void Do(double interval, Action<Timer> doWhat = default)
        {
            Do(interval, true, doWhat);
        }

        /// <summary>
        /// 执行简单任务（只执行一次）
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="doWhat"></param>
        public static void DoOnce(double interval, Action<Timer> doWhat = default)
        {
            Do(interval, false, doWhat);
        }

        /// <summary>
        /// 执行表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="doWhat"></param>
        public static void DoCron(string expression, Action<Timer> doWhat = default)
        {
            Do(1000, e =>
            {
                var cronExpression = CronExpression.Parse(expression, CronFormat.IncludeSeconds);
                var nextTime = cronExpression.GetNextOccurrence(DateTimeOffset.Now, TimeZoneInfo.Local);
                var nextLocalTime = nextTime?.DateTime;
                if (nextLocalTime == null) e.Stop();

                DoOnce((nextLocalTime.Value - DateTime.Now).TotalSeconds, se => doWhat(se));
            });
        }
    }
}