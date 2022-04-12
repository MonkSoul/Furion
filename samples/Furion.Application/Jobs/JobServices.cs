using Furion.Core;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Furion.TaskScheduler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Furion.Application;

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
        Console.WriteLine($"简单任务初始化 - {DateTime.Now:yyyy-MM-dd HH:mm:ss fff}");

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
        Console.WriteLine($"Cron 任务初始化 - {DateTime.Now:yyyy-MM-dd HH:mm:ss fff}");

        SpareTime.Do(cron, (t, i) =>
        {
            Console.WriteLine($"{t.WorkerName} -{t.Description} - {DateTime.Now:yyyy-MM-dd HH:mm:ss fff} - {i}");
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

    /// <summary>
    /// 获取所有任务信息
    /// </summary>
    /// <returns></returns>
    public IEnumerable<object> GetWorkers()
    {
        return SpareTime.GetWorkers().ToList().Select(u => new
        {
            u.WorkerName,
            Status = u.Status.ToString(),
            u.Description,
            Type = u.Type.ToString(),
            ExecuteType = u.ExecuteType.ToString()
        });
    }

    /// <summary>
    /// 串行执行任务
    /// </summary>
    /// <param name="jobName"></param>
    /// <returns></returns>
    public string InitSerialJob(string jobName = "serialJob")
    {
        Console.WriteLine($"串行执行任务 - {DateTime.Now:yyyy-MM-dd HH:mm:ss fff}");

        SpareTime.Do(1000, (t, i) =>
        {
            Thread.Sleep(5000); // 模拟执行耗时任务
            Console.WriteLine($"{t.WorkerName} -{t.Description} - {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {i}");
        }, jobName, "模拟测试任务", executeType: SpareTimeExecuteTypes.Serial);

        return jobName;
    }

    /// <summary>
    /// 测试异常任务
    /// </summary>
    public void TestExceptionWorker()
    {
        SpareTime.Do(1000, (t, c) =>
        {
            // 判断是否有异常
            if (t.Exception.Any())
            {
                Console.WriteLine(t.Exception.Values.LastOrDefault()?.Message);
            }

            // 执行第三次抛异常
            if (c > 5)
            {
                throw Oops.Oh("抛异常" + c);
            }
            else
            {
                Console.WriteLine($"{t.WorkerName} -{t.Description} - {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {c}");
            }
        }, "exceptionJob");
    }

    /// <summary>
    /// 测试只执行一次
    /// </summary>
    public void DoOnce()
    {
        SpareTime.DoOnce(5000, (t, c) =>
        {
            Console.WriteLine("测试执行一次");
        });
    }

    /// <summary>
    /// 测试下一次执行时间
    /// </summary>
    /// <param name="cron"></param>
    /// <returns></returns>
    public DateTimeOffset TestNextOccurrence(string cron)
    {
        return SpareTime.GetCronNextOccurrence(cron).Value;
    }

    /// <summary>
    /// 测试数据库操作和释放
    /// </summary>
    public void TestDbOperate()
    {
        SpareTime.Do(1000, (t, i) =>
        {
            Scoped.Create((f, s) =>
            {
                var rep = s.ServiceProvider.GetService<IRepository<Person>>();
                var entities = rep.DetachedEntities.Where(u => u.Id > 0);
            });

            Console.WriteLine($"{t.WorkerName} -{t.Description} - {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {i}");
        }, "dbJob", "测试数据库操作");
    }
}
