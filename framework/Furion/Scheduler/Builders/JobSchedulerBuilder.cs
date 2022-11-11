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

namespace Furion.Scheduler;

/// <summary>
/// 作业调度计划构建器
/// </summary>
[SuppressSniffer]
public sealed class JobSchedulerBuilder
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jobBuilder">作业信息构建器</param>
    private JobSchedulerBuilder(JobBuilder jobBuilder)
    {
        JobBuilder = jobBuilder;
    }

    /// <summary>
    /// 标记作业调度计划持久化行为
    /// </summary>
    internal PersistenceBehavior Behavior { get; set; } = PersistenceBehavior.Update;

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
    /// <returns><see cref="JobSchedulerBuilder"/></returns>
    public static JobSchedulerBuilder Create(JobBuilder jobBuilder, params JobTriggerBuilder[] triggerBuilders)
    {
        // 空检查
        if (jobBuilder == null) throw new ArgumentNullException(nameof(jobBuilder));

        // 创建作业调度计划构建器
        var jobSchedulerBuilder = new JobSchedulerBuilder(jobBuilder);

        // 批量添加触发器
        if (triggerBuilders != null && triggerBuilders.Length > 0)
        {
            jobSchedulerBuilder.JobTriggerBuilders.AddRange(triggerBuilders);
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

                jobSchedulerBuilder.JobTriggerBuilders.Add(jobTriggerBuilder);
            }
        }

        return jobSchedulerBuilder;
    }

    /// <summary>
    /// 将 <see cref="JobScheduler"/> 转换成 <see cref="JobSchedulerBuilder"/>
    /// </summary>
    /// <param name="jobScheduler">作业调度计划</param>
    /// <returns><see cref="JobSchedulerBuilder"/></returns>
    public static JobSchedulerBuilder From(JobScheduler jobScheduler)
    {
        var jobSchedulerBuilder = new JobSchedulerBuilder(JobBuilder.From(jobScheduler.JobDetail))
        {
            JobTriggerBuilders = jobScheduler.JobTriggers.Select(t => JobTriggerBuilder.From(t.Value)).ToList()
        };

        return jobSchedulerBuilder;
    }

    /// <summary>
    /// 标记作业调度计划为更新行为
    /// </summary>
    /// <returns></returns>
    public JobSchedulerBuilder Update()
    {
        Behavior = PersistenceBehavior.Update;
        return this;
    }

    /// <summary>
    /// 标记作业调度计划为删除行为
    /// </summary>
    /// <returns></returns>
    public JobSchedulerBuilder Deleted()
    {
        Behavior = PersistenceBehavior.Deleted;
        return this;
    }

    /// <summary>
    /// 构建 <see cref="JobScheduler"/> 对象
    /// </summary>
    /// <returns><see cref="JobScheduler"/></returns>
    internal JobScheduler Build()
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
        var jobScheduler = new JobScheduler(jobDetail, jobTriggers);

        return jobScheduler;
    }
}