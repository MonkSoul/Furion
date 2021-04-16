using Furion.DynamicApiController;
using Furion.TaskScheduler;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Furion.Application
{
    [ApiDescriptionSettings("Job")]
    public class JobServices : IDynamicApiController
    {
        /// <summary>
        /// 初始化一个简单任务
        /// </summary>
        /// <param name="jobName"></param>
        /// <returns></returns>
        public string InitJob(string jobName = "jobName")
        {
            Console.WriteLine("简单任务初始化");

            SpareTime.Do(1000, (t, i) =>
            {
                Console.WriteLine($"{t.WorkerName} -{t.Description} - {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {i}");
            }, jobName, "模拟测试任务");

            SpareTime.Do(1000, (t, i) =>
            {
                Console.WriteLine($"{t.WorkerName} -{t.Description} - {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {i}");
            }, jobName, "模拟测试任务");

            return jobName;
        }

        /// <summary>
        /// 初始化一个 Cron 表达式任务，5秒执行一次
        /// </summary>
        /// <param name="cron"></param>
        /// <param name="jobName"></param>
        /// <returns></returns>
        public string InitCronJob([FromBody] string cron = "*/5 * * * * *", string jobName = "cronName")
        {
            Console.WriteLine("Cron 任务初始化");

            SpareTime.Do(cron, (t, i) =>
            {
                Console.WriteLine($"{t.WorkerName} -{t.Description} - {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {i}");
            }, jobName, "模拟测试任务");

            return jobName;
        }

        /// <summary>
        /// 开始一个简单任务
        /// </summary>
        /// <param name="jobName"></param>
        /// <returns></returns>
        public string StartJob(string jobName = "jobName")
        {
            Console.WriteLine("任务开始");
            SpareTime.Start(jobName);

            return jobName;
        }

        /// <summary>
        /// 停止一个简单任务
        /// </summary>
        /// <param name="jobName"></param>
        /// <returns></returns>
        public string StopJob(string jobName = "jobName")
        {
            Console.WriteLine("任务停止");
            SpareTime.Stop(jobName);

            return jobName;
        }

        /// <summary>
        /// 取消一个简单任务
        /// </summary>
        /// <param name="jobName"></param>
        /// <returns></returns>
        public string CancelJob(string jobName = "jobName")
        {
            Console.WriteLine("任务取消");
            SpareTime.Cancel(jobName);

            return jobName;
        }
    }
}