// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Reflection;

namespace Furion.Schedule;

/// <summary>
/// 作业调度计划构建器
/// </summary>
[SuppressSniffer]
public sealed class SchedulerBuilder
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jobBuilder">作业信息构建器</param>
    private SchedulerBuilder(JobBuilder jobBuilder)
    {
        JobBuilder = jobBuilder;
    }

    /// <summary>
    /// 标记作业调度计划持久化行为
    /// </summary>
    internal PersistenceBehavior Behavior { get; set; } = PersistenceBehavior.UpdateJob;

    /// <summary>
    /// 作业信息构建器
    /// </summary>
    public JobBuilder JobBuilder { get; private set; }

    /// <summary>
    /// 作业触发器构建器集合
    /// </summary>
    public List<JobTriggerBuilder> JobTriggerBuilders { get; private set; } = new();

    /// <summary>
    /// 创建作业调度程序构建器
    /// </summary>
    /// <param name="jobBuilder">作业信息构建器</param>
    /// <param name="triggerBuilders">作业触发器构建器集合</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    public static SchedulerBuilder Create(JobBuilder jobBuilder, params JobTriggerBuilder[] triggerBuilders)
    {
        // 空检查
        if (jobBuilder == null) throw new ArgumentNullException(nameof(jobBuilder));

        // 创建作业调度计划构建器
        var schedulerBuilder = new SchedulerBuilder(jobBuilder);

        // 批量添加触发器
        if (triggerBuilders != null && triggerBuilders.Length > 0)
        {
            schedulerBuilder.JobTriggerBuilders.AddRange(triggerBuilders);
        }

        // 判断是否扫描 IJob 实现类 [JobTrigger] 特性触发器
        if (jobBuilder.ScanTriggers)
        {
            var jobTriggerAttributes = jobBuilder.RuntimeJobType.GetCustomAttributes<JobTriggerAttribute>(true);
            foreach (var jobTriggerAttribute in jobTriggerAttributes)
            {
                // 创建作业触发器并添加到当前作业触发器构建器中
                var jobTriggerBuilder = JobTriggerBuilder.Create(jobTriggerAttribute.RuntimeTriggerType)
                    .WithArgs(jobTriggerAttribute.RuntimeTriggerArgs)
                    .SetTriggerId(jobTriggerAttribute.TriggerId)
                    .SetDescription(jobTriggerAttribute.Description)
                    .SetMaxNumberOfRuns(jobTriggerAttribute.MaxNumberOfRuns)
                    .SetMaxNumberOfErrors(jobTriggerAttribute.MaxNumberOfErrors)
                    .SetNumRetries(jobTriggerAttribute.NumRetries)
                    .SetRetryTimeout(jobTriggerAttribute.RetryTimeout)
                    .SetLogExecution(jobTriggerAttribute.LogExecution);

                schedulerBuilder.JobTriggerBuilders.Add(jobTriggerBuilder);
            }
        }

        return schedulerBuilder;
    }

    /// <summary>
    /// 标记作业调度计划为更新行为
    /// </summary>
    /// <returns></returns>
    public SchedulerBuilder Update()
    {
        Behavior = PersistenceBehavior.UpdateJob;
        return this;
    }

    /// <summary>
    /// 标记作业调度计划为删除行为
    /// </summary>
    /// <returns></returns>
    public SchedulerBuilder Deleted()
    {
        Behavior = PersistenceBehavior.RemoveJob;
        return this;
    }

    /// <summary>
    /// 将 <see cref="Scheduler"/> 转换成 <see cref="SchedulerBuilder"/>
    /// </summary>
    /// <param name="scheduler">作业调度计划</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    internal static SchedulerBuilder From(Scheduler scheduler)
    {
        var schedulerBuilder = new SchedulerBuilder(JobBuilder.From(scheduler.JobDetail))
        {
            JobTriggerBuilders = scheduler.JobTriggers.Select(t => JobTriggerBuilder.From(t.Value)).ToList()
        };

        return schedulerBuilder;
    }

    /// <summary>
    /// 构建 <see cref="Scheduler"/> 对象
    /// </summary>
    /// <returns><see cref="Scheduler"/></returns>
    internal Scheduler Build()
    {
        // 构建作业信息和作业触发器
        var jobDetail = JobBuilder.Build();

        // 构建作业触发器
        var jobTriggers = new Dictionary<string, JobTrigger>();

        // 遍历作业触发器构建器集合
        for (var i = 0; i < JobTriggerBuilders.Count; i++)
        {
            var jobTriggerBuilder = JobTriggerBuilders[i];

            // 配置默认 TriggerId
            if (string.IsNullOrWhiteSpace(jobTriggerBuilder.TriggerId))
            {
                jobTriggerBuilder.SetTriggerId($"{jobDetail.JobId}_trigger{i + 1}");
            }

            var jobTrigger = jobTriggerBuilder.Build(jobDetail.JobId);
            var succeed = jobTriggers.TryAdd(jobTrigger.TriggerId, jobTrigger);

            // 作业触发器 Id 唯一检查
            if (!succeed) throw new InvalidOperationException($"The TriggerId of <{jobTrigger.TriggerId}> already exists.");
        }

        // 创建作业调度计划
        var scheduler = new Scheduler(jobDetail, jobTriggers);

        return scheduler;
    }
}