using Furion;
using Furion.DependencyInjection;
using Furion.TaskScheduler;
using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 任务调度类服务拓展
    /// </summary>
    [SkipScan]
    public static class TaskSchedulerServiceCollectionExtensions
    {
        /// <summary>
        /// 添加任务调度服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddTaskScheduler(this IServiceCollection services)
        {
            // 查找所有贴了 [SpareTime] 特性的方法，并且含有一个参数，参数为 SpareTimer 类型
            var taskMethods = App.EffectiveTypes
                    // 查询符合条件的任务类型
                    .Where(u => u.IsClass && !u.IsInterface && !u.IsAbstract && typeof(ISpareTimeWorker).IsAssignableFrom(u))
                    // 查询符合条件的任务方法
                    .SelectMany(u =>
                        u.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                         .Where(m => m.IsDefined(typeof(SpareTimeAttribute), false)
                                                    && m.GetParameters().Length == 1
                                                    && m.GetParameters()[0].ParameterType == typeof(SpareTimer)
                                                    && m.ReturnType == typeof(void))
                         .GroupBy(m => m.DeclaringType));

            if (!taskMethods.Any()) return services;

            // 遍历所有的 Worker
            foreach (var item in taskMethods)
            {
                if (!item.Any()) continue;

                // 创建任务对象
                var typeInstance = Activator.CreateInstance(item.Key);

                foreach (var method in item)
                {
                    // 创建委托类型
                    var action = (Action<SpareTimer>)Delegate.CreateDelegate(typeof(Action<SpareTimer>), typeInstance, method.Name);

                    // 获取所有任务特性
                    var spareTimeAttributes = method.GetCustomAttributes<SpareTimeAttribute>();

                    // 注册任务
                    foreach (var spareTimeAttribute in spareTimeAttributes)
                    {
                        switch (spareTimeAttribute.Type)
                        {
                            // 执行间隔任务
                            case SpareTimeTypes.Interval:
                                // 执行一次
                                if (spareTimeAttribute.DoOnce)
                                {
                                    SpareTime.DoOnce(spareTimeAttribute.Interval, action, spareTimeAttribute.WorkName, spareTimeAttribute.Description);
                                }
                                // 不间断执行
                                else
                                {
                                    SpareTime.Do(spareTimeAttribute.Interval, action, spareTimeAttribute.WorkName, spareTimeAttribute.Description);
                                }
                                break;
                            // 执行 Cron 表达式任务
                            case SpareTimeTypes.Cron:
                                SpareTime.Do(spareTimeAttribute.CronExpression, action, spareTimeAttribute.WorkName, spareTimeAttribute.Description);
                                break;

                            default:
                                break;
                        }
                    }
                }
            }

            return services;
        }
    }
}