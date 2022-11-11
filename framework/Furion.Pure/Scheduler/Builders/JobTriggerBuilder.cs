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

using Furion.TimeCrontab;
using System.Reflection;
using System.Text.Json;

namespace Furion.Scheduler;

/// <summary>
/// 作业触发器构建器
/// </summary>
[SuppressSniffer]
public sealed class JobTriggerBuilder : JobTrigger
{
    /// <summary>
    /// 构造函数
    /// </summary>
    private JobTriggerBuilder()
    {
    }

    /// <summary>
    /// 创建新的作业周期（间隔）触发器构建器
    /// </summary>
    /// <param name="interval">间隔（毫秒）</param>
    /// <returns><see cref="JobTriggerBuilder"/></returns>
    public static JobTriggerBuilder Period(int interval)
    {
        return Create(typeof(PeriodTrigger)).WithArgs(new object[] { interval });
    }

    /// <summary>
    /// 创建新的作业 Cron 触发器构建器
    /// </summary>
    /// <param name="schedule">Cron 表达式</param>
    /// <param name="format">Cron 表达式格式化类型，默认 <see cref="CronStringFormat.Default"/></param>
    /// <returns><see cref="JobTriggerBuilder"/></returns>
    public static JobTriggerBuilder Cron(string schedule, CronStringFormat format = CronStringFormat.Default)
    {
        return Create(typeof(CronTrigger)).WithArgs(new object[] { schedule, (int)format });
    }

    /// <summary>
    /// 创建作业触发器构建器
    /// </summary>
    /// <typeparam name="TJobTrigger"><see cref="JobTrigger"/> 派生类</typeparam>
    /// <returns><see cref="JobTriggerBuilder"/></returns>
    public static JobTriggerBuilder Create<TJobTrigger>()
        where TJobTrigger : JobTrigger
    {
        return Create(typeof(TJobTrigger));
    }

    /// <summary>
    /// 创建新的作业触发器构建器
    /// </summary>
    /// <param name="assemblyName">作业触发器类型所在程序集 Name</param>
    /// <param name="triggerTypeFullName">作业触发器类型 FullName</param>
    /// <returns><see cref="JobBuilder"/></returns>
    public static JobTriggerBuilder Create(string assemblyName, string triggerTypeFullName)
    {
        // 创建作业触发器构建器
        var jobTriggerBuilder = new JobTriggerBuilder().SetTriggerType(assemblyName, triggerTypeFullName);

        return jobTriggerBuilder;
    }

    /// <summary>
    /// 创建新的作业触发器构建器
    /// </summary>
    /// <param name="triggerType"><see cref="JobTrigger"/> 派生类</param>
    /// <returns><see cref="JobTriggerBuilder"/></returns>
    public static JobTriggerBuilder Create(Type triggerType)
    {
        // 创建作业触发器构建器
        var jobTriggerBuilder = new JobTriggerBuilder().SetTriggerType(triggerType);

        return jobTriggerBuilder;
    }

    /// <summary>
    /// 将 <see cref="JobTrigger"/> 转换成 <see cref="JobTrigger"/>
    /// </summary>
    /// <param name="jobTrigger"></param>
    /// <returns></returns>
    public static JobTriggerBuilder From(JobTrigger jobTrigger)
    {
        var jobTriggerBuilder = jobTrigger.MapTo<JobTriggerBuilder>();
        return jobTriggerBuilder;
    }

    /// <summary>
    /// 设置作业触发器参数
    /// </summary>
    /// <param name="args">作业触发器参数</param>
    /// <returns><see cref="JobTriggerBuilder"/></returns>
    public JobTriggerBuilder WithArgs(object[] args)
    {
        Args = args == null || args.Length == 0
            ? null
            : JsonSerializer.Serialize(args);
        RuntimeTriggerArgs = args;

        return this;
    }

    /// <summary>
    /// 设置作业触发器 Id
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <returns><see cref="JobBuilder"/></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public JobTriggerBuilder SetTriggerId(string triggerId)
    {
        TriggerId = triggerId;

        return this;
    }

    /// <summary>
    /// 设置作业类型
    /// </summary>
    /// <param name="assemblyName">作业触发器所在程序集 Name</param>
    /// <param name="triggerTypeFullName">作业触发器 FullName</param>
    /// <returns><see cref="JobTriggerBuilder"/></returns>
    public JobTriggerBuilder SetTriggerType(string assemblyName, string triggerTypeFullName)
    {
        // 空检查
        if (!string.IsNullOrWhiteSpace(assemblyName)) throw new ArgumentNullException(nameof(assemblyName));
        if (!string.IsNullOrWhiteSpace(triggerTypeFullName)) throw new ArgumentNullException(nameof(triggerTypeFullName));

        // 加载 GAC 全局应用程序缓存中的程序集及类型
        var triggerType = Assembly.Load(assemblyName).GetType(triggerTypeFullName);

        return SetTriggerType(triggerType);
    }

    /// <summary>
    /// 设置作业类型
    /// </summary>
    /// <param name="triggerType">作业触发器类型</param>
    /// <returns><see cref="JobTriggerBuilder"/></returns>
    public JobTriggerBuilder SetTriggerType(Type triggerType)
    {
        // 检查 triggerType 类型是否派生自 JobTrigger
        if (!typeof(JobTrigger).IsAssignableFrom(triggerType) || triggerType.IsInterface)
        {
            throw new InvalidOperationException("The <TriggerType> is not a valid JobTrigger type.");
        }

        // 最多只能包含一个构造函数
        if (triggerType.GetConstructors().Length > 1)
        {
            throw new InvalidOperationException("The <TriggerType> can contain at most one constructor.");
        }

        AssemblyName = triggerType.Assembly.GetName().Name;
        TriggerType = triggerType.FullName;
        RuntimeTriggerType = triggerType;

        return this;
    }

    /// <summary>
    /// 设置描述信息
    /// </summary>
    /// <param name="description">描述信息</param>
    /// <returns><see cref="JobTriggerBuilder"/></returns>
    public JobTriggerBuilder SetDescription(string description)
    {
        Description = description;

        return this;
    }

    /// <summary>
    /// 设置起始时间
    /// </summary>
    /// <param name="startTime">起始时间</param>
    /// <returns><see cref="JobTriggerBuilder"/></returns>
    public JobTriggerBuilder SetStartTime(DateTime? startTime)
    {
        StartTime = startTime;

        return this;
    }

    /// <summary>
    /// 设置结束时间
    /// </summary>
    /// <param name="endTime">结束时间</param>
    /// <returns><see cref="JobTriggerBuilder"/></returns>
    public JobTriggerBuilder SetEndTime(DateTime? endTime)
    {
        EndTime = endTime;

        return this;
    }

    /// <summary>
    /// 设置触发次数
    /// </summary>
    /// <param name="numberOfRuns">触发次数</param>
    /// <returns><see cref="JobTriggerBuilder"/></returns>
    public JobTriggerBuilder SetNumberOfRuns(long numberOfRuns)
    {
        NumberOfRuns = numberOfRuns;

        return this;
    }

    /// <summary>
    /// 设置最大触发次数
    /// </summary>
    /// <param name="maxNumberOfRuns">最大触发次数</param>
    /// <remarks>
    /// <para>0：不限制</para>
    /// <para>>n：N 次</para>
    /// </remarks>
    /// <returns><see cref="JobTriggerBuilder"/></returns>
    public JobTriggerBuilder SetMaxNumberOfRuns(long maxNumberOfRuns)
    {
        MaxNumberOfRuns = maxNumberOfRuns;

        return this;
    }

    /// <summary>
    /// 设置出错次数
    /// </summary>
    /// <param name="numberOfErrors">出错次数</param>
    /// <returns><see cref="JobTriggerBuilder"/></returns>
    public JobTriggerBuilder SetNumberOfErrors(long numberOfErrors)
    {
        NumberOfErrors = numberOfErrors;

        return this;
    }

    /// <summary>
    /// 设置最大出错次数
    /// </summary>
    /// <param name="maxNumberOfErrors">最大出错次数</param>
    /// <remarks>
    /// <para>0：不限制</para>
    /// <para>n：N 次</para>
    /// </remarks>
    /// <returns><see cref="JobTriggerBuilder"/></returns>
    public JobTriggerBuilder SetMaxNumberOfErrors(long maxNumberOfErrors)
    {
        MaxNumberOfErrors = maxNumberOfErrors;

        return this;
    }

    /// <summary>
    /// 设置重试次数
    /// </summary>
    /// <param name="numRetries">重试次数</param>
    /// <returns><see cref="JobTriggerBuilder"/></returns>
    public JobTriggerBuilder SetNumRetries(int numRetries)
    {
        NumRetries = numRetries;

        return this;
    }

    /// <summary>
    /// 设置重试间隔时间
    /// </summary>
    /// <param name="retryTimeout">重试间隔时间</param>
    /// <returns><see cref="JobTriggerBuilder"/></returns>
    public JobTriggerBuilder SetRetryTimeout(int retryTimeout)
    {
        RetryTimeout = retryTimeout;

        return this;
    }

    /// <summary>
    /// 设置是否是否输出作业执行日志
    /// </summary>
    /// <param name="logExecution">是否输出作业执行日志</param>
    /// <returns><see cref="JobBuilder"/></returns>
    public JobTriggerBuilder SetLogExecution(bool logExecution)
    {
        LogExecution = logExecution;

        return this;
    }

    /// <summary>
    /// 隐藏触发器公开方法
    /// </summary>
    /// <param name="startAt"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    internal new DateTime GetNextOccurrence(DateTime startAt) => throw new NotImplementedException();

    /// <summary>
    /// 隐藏触发器公开方法
    /// </summary>
    /// <param name="checkTime">受检时间</param>
    /// <returns><see cref="bool"/></returns>
    internal new bool ShouldRun(DateTime checkTime) => throw new NotImplementedException();

    /// <summary>
    /// 构建 <see cref="JobTrigger"/> 对象
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <returns><see cref="JobTrigger"/></returns>
    internal JobTrigger Build(string jobId)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(jobId))
        {
            throw new ArgumentNullException(nameof(jobId));
        }

        JobId = jobId;

        // 判断是否带参数
        var withArgs = !(RuntimeTriggerArgs == null || RuntimeTriggerArgs.Length == 0);

        // 反射创建作业触发器对象
        var triggerObject = (!withArgs
            ? Activator.CreateInstance(RuntimeTriggerType!)
            : Activator.CreateInstance(RuntimeTriggerType!, RuntimeTriggerArgs));

        var jobTrigger = this.MapTo<JobTrigger>(triggerObject);
        return jobTrigger;
    }
}