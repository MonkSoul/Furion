using Furion.DependencyInjection;
using System;
using System.Text.RegularExpressions;

namespace Furion.TaskScheduler
{
    /// <summary>
    /// 配置定时任务特性
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SpareTimeAttribute : Attribute
    {
        /// <summary>
        /// 模板正则表达式
        /// </summary>
        private const string templatePattern = @"\{(?<p>.+?)\}";

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
            var realValue = ResolveTemplate(expressionOrKey);

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
        public CronFormat CronFormat { get; set; } = CronFormat.Standard;

        /// <summary>
        /// 解析模板（可配置 appsetting.json 的键）
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        private static string ResolveTemplate(string template)
        {
            if (string.IsNullOrWhiteSpace(template)) return default;

            if (Regex.IsMatch(template, templatePattern))
            {
                var configKey = Regex.Match(template, templatePattern).Groups["p"].Value;
                return App.GetConfig<string>(configKey) ?? configKey;
            }

            return template;
        }
    }
}