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

namespace Furion.Schedule;

/// <summary>
/// 作业触发器构建器
/// </summary>
[SuppressSniffer]
public sealed class TriggerBuilder : JobTrigger
{
    /// <summary>
    /// 构造函数
    /// </summary>
    private TriggerBuilder()
    {
    }

    /// <summary>
    /// 创建作业周期（间隔）触发器构建器
    /// </summary>
    /// <param name="interval">间隔（毫秒）</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Period(int interval)
    {
        return Create(typeof(PeriodTrigger))
            .SetArgs(new object[] { interval });
    }

    /// <summary>
    /// 创建作业 Cron 触发器构建器
    /// </summary>
    /// <param name="schedule">Cron 表达式</param>
    /// <param name="format">Cron 表达式格式化类型，默认 <see cref="CronStringFormat.Default"/></param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Cron(string schedule, CronStringFormat format = CronStringFormat.Default)
    {
        return Create(typeof(CronTrigger))
            .SetArgs(new object[] { schedule, (int)format });
    }

    /// <summary>
    /// 创建作业触发器构建器
    /// </summary>
    /// <typeparam name="TTrigger"><see cref="JobTrigger"/> 派生类</typeparam>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Create<TTrigger>()
        where TTrigger : JobTrigger
    {
        return Create(typeof(TTrigger));
    }

    /// <summary>
    /// 创建新的作业触发器构建器
    /// </summary>
    /// <param name="assemblyName">作业触发器类型所在程序集 Name</param>
    /// <param name="triggerTypeFullName">作业触发器类型 FullName</param>
    /// <returns><see cref="JobBuilder"/></returns>
    public static TriggerBuilder Create(string assemblyName, string triggerTypeFullName)
    {
        return new TriggerBuilder()
            .SetTriggerType(assemblyName, triggerTypeFullName);
    }

    /// <summary>
    /// 创建新的作业触发器构建器
    /// </summary>
    /// <param name="triggerType"><see cref="JobTrigger"/> 派生类</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Create(Type triggerType)
    {
        return new TriggerBuilder()
            .SetTriggerType(triggerType);
    }

    /// <summary>
    /// 将 <see cref="JobTrigger"/> 转换成 <see cref="TriggerBuilder"/>
    /// </summary>
    /// <param name="trigger"></param>
    /// <returns></returns>
    public static TriggerBuilder From(JobTrigger trigger)
    {
        return trigger.MapTo<TriggerBuilder>();
    }

    /// <summary>
    /// 克隆作业触发器构建器
    /// </summary>
    /// <param name="fromTriggerBuilder">被克隆的作业触发器构建器</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public static TriggerBuilder Clone(TriggerBuilder fromTriggerBuilder)
    {
        return Create(fromTriggerBuilder.RuntimeTriggerType)
                     .SetArgs(fromTriggerBuilder.RuntimeTriggerArgs)
                     .SetDescription(fromTriggerBuilder.Description)
                     .SetStartTime(fromTriggerBuilder.StartTime)
                     .SetEndTime(fromTriggerBuilder.EndTime)
                     .SetMaxNumberOfRuns(fromTriggerBuilder.MaxNumberOfRuns)
                     .SetMaxNumberOfErrors(fromTriggerBuilder.MaxNumberOfErrors)
                     .SetNumRetries(fromTriggerBuilder.NumRetries)
                     .SetRetryTimeout(fromTriggerBuilder.RetryTimeout)
                     .SetStartNow(fromTriggerBuilder.StartNow);
    }

    /// <summary>
    /// 从目标值填充数据到作业触发器构建器
    /// </summary>
    /// <param name="value">目标值</param>
    /// <param name="ignoreNullValue">忽略空值</param>
    /// <returns><see cref="JobBuilder"/></returns>
    public TriggerBuilder LoadFrom(object value, bool ignoreNullValue = false)
    {
        if (value == null) return this;

        var valueType = value.GetType();
        if (valueType.IsInterface
            || valueType.IsValueType
            || valueType.IsEnum
            || valueType.IsArray) throw new InvalidOperationException(nameof(value));

        return value.MapTo<TriggerBuilder>(this, ignoreNullValue);
    }

    /// <summary>
    /// 设置作业触发器 Id
    /// </summary>
    /// <param name="triggerId">作业触发器 Id</param>
    /// <returns><see cref="JobBuilder"/></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public TriggerBuilder SetTriggerId(string triggerId)
    {
        TriggerId = triggerId;

        return this;
    }

    /// <summary>
    /// 设置作业类型
    /// </summary>
    /// <param name="assemblyName">作业触发器所在程序集 Name</param>
    /// <param name="triggerTypeFullName">作业触发器 FullName</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetTriggerType(string assemblyName, string triggerTypeFullName)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(assemblyName)) throw new ArgumentNullException(nameof(assemblyName));
        if (string.IsNullOrWhiteSpace(triggerTypeFullName)) throw new ArgumentNullException(nameof(triggerTypeFullName));

        // 加载 GAC 全局应用程序缓存中的程序集及类型
        var triggerType = Assembly.Load(assemblyName).GetType(triggerTypeFullName);

        return SetTriggerType(triggerType);
    }

    /// <summary>
    /// 设置作业类型
    /// </summary>
    /// <param name="triggerType">作业触发器类型</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetTriggerType(Type triggerType)
    {
        // 检查 triggerType 类型是否派生自 JobTrigger
        if (!typeof(JobTrigger).IsAssignableFrom(triggerType)
            || triggerType.IsInterface
            || triggerType.IsAbstract) throw new InvalidOperationException("The <triggerType> is not a valid JobTrigger type.");

        // 最多只能包含一个构造函数
        if (triggerType.GetConstructors().Length > 1) throw new InvalidOperationException("The <triggerType> can contain at most one constructor.");

        AssemblyName = triggerType.Assembly.GetName().Name;
        TriggerType = triggerType.FullName;
        RuntimeTriggerType = triggerType;

        return this;
    }

    /// <summary>
    /// 设置作业触发器参数
    /// </summary>
    /// <param name="args">作业触发器参数</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetArgs(object[] args)
    {
        Args = args == null || args.Length == 0
            ? null
            : JsonSerializer.Serialize(args);
        RuntimeTriggerArgs = args;

        return this;
    }

    /// <summary>
    /// 设置描述信息
    /// </summary>
    /// <param name="description">描述信息</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetDescription(string description)
    {
        Description = description;

        return this;
    }

    /// <summary>
    /// 设置作业触发器状态
    /// </summary>
    /// <param name="status">作业触发器状态</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public new TriggerBuilder SetStatus(TriggerStatus status)
    {
        Status = status;

        return this;
    }

    /// <summary>
    /// 设置起始时间
    /// </summary>
    /// <param name="startTime">起始时间</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetStartTime(DateTime? startTime)
    {
        StartTime = startTime;

        return this;
    }

    /// <summary>
    /// 设置结束时间
    /// </summary>
    /// <param name="endTime">结束时间</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetEndTime(DateTime? endTime)
    {
        EndTime = endTime;

        return this;
    }

    /// <summary>
    /// 设置最近运行时间
    /// </summary>
    /// <param name="lastRunTime">最近运行时间</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetLastRunTime(DateTime? lastRunTime)
    {
        LastRunTime = lastRunTime;

        return this;
    }

    /// <summary>
    /// 设置下一次运行时间
    /// </summary>
    /// <param name="nextRunTime">下一次运行时间</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetNextRunTime(DateTime? nextRunTime)
    {
        NextRunTime = nextRunTime;

        return this;
    }

    /// <summary>
    /// 设置触发次数
    /// </summary>
    /// <param name="numberOfRuns">触发次数</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetNumberOfRuns(long numberOfRuns)
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
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetMaxNumberOfRuns(long maxNumberOfRuns)
    {
        MaxNumberOfRuns = maxNumberOfRuns;

        return this;
    }

    /// <summary>
    /// 设置出错次数
    /// </summary>
    /// <param name="numberOfErrors">出错次数</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetNumberOfErrors(long numberOfErrors)
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
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetMaxNumberOfErrors(long maxNumberOfErrors)
    {
        MaxNumberOfErrors = maxNumberOfErrors;

        return this;
    }

    /// <summary>
    /// 设置重试次数
    /// </summary>
    /// <param name="numRetries">重试次数</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetNumRetries(int numRetries)
    {
        NumRetries = numRetries;

        return this;
    }

    /// <summary>
    /// 设置重试间隔时间
    /// </summary>
    /// <param name="retryTimeout">重试间隔时间</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetRetryTimeout(int retryTimeout)
    {
        RetryTimeout = retryTimeout;

        return this;
    }

    /// <summary>
    /// 设置是否立即启动
    /// </summary>
    /// <param name="startNow">是否立即启动</param>
    /// <returns><see cref="TriggerBuilder"/></returns>
    public TriggerBuilder SetStartNow(bool startNow)
    {
        StartNow = startNow;

        if (startNow == false && Status != TriggerStatus.NotStart)
        {
            Status = TriggerStatus.NotStart;
        }

        return this;
    }

    /// <summary>
    /// 隐藏作业触发器公开方法
    /// </summary>
    /// <param name="startAt"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public new DateTime GetNextOccurrence(DateTime startAt) => throw new NotImplementedException();

    /// <summary>
    /// 隐藏作业触发器公开方法
    /// </summary>
    /// <param name="checkTime">受检时间</param>
    /// <returns><see cref="bool"/></returns>
    public new bool ShouldRun(DateTime checkTime) => throw new NotImplementedException();

    /// <summary>
    /// 构建 <see cref="JobTrigger"/> 对象
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <returns><see cref="JobTrigger"/></returns>
    internal JobTrigger Build(string jobId)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(jobId)) throw new ArgumentNullException(nameof(jobId));

        // 检查 StartTime 和 EndTime 的关系，StartTime 不能大于 EndTime
        if (StartTime != null && EndTime != null
            && StartTime.Value > EndTime.Value) throw new InvalidOperationException("The start time cannot be greater than the end time.");

        JobId = jobId;

        // 判断是否带参数
        var hasArgs = !(RuntimeTriggerArgs == null || RuntimeTriggerArgs.Length == 0);

        // 反射创建作业触发器对象
        var triggerInstance = (!hasArgs
            ? Activator.CreateInstance(type: RuntimeTriggerType)
            : Activator.CreateInstance(RuntimeTriggerType, RuntimeTriggerArgs));

        return this.MapTo<JobTrigger>(triggerInstance);
    }
}