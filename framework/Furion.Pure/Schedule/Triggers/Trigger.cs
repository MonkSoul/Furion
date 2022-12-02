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

using System.Text.Json.Serialization;

namespace Furion.Schedule;

/// <summary>
/// 作业触发器基类
/// </summary>
[SuppressSniffer]
public partial class Trigger
{
    /// <summary>
    /// 作业触发器 Id
    /// </summary>
    [JsonInclude]
    public string TriggerId { get; internal set; }

    /// <summary>
    /// 作业 Id
    /// </summary>
    [JsonInclude]
    public string JobId { get; internal set; }

    /// <summary>
    /// 作业触发器类型
    /// </summary>
    /// <remarks>存储的是类型的 FullName</remarks>
    [JsonInclude]
    public string TriggerType { get; internal set; }

    /// <summary>
    /// 作业触发器类型所在程序集
    /// </summary>
    /// <remarks>存储的是程序集 Name</remarks>
    [JsonInclude]
    public string AssemblyName { get; internal set; }

    /// <summary>
    /// 作业触发器参数
    /// </summary>
    /// <remarks>运行时将反序列化为 object[] 类型并作为构造函数参数</remarks>
    [JsonInclude]
    public string Args { get; internal set; }

    /// <summary>
    /// 描述信息
    /// </summary>
    [JsonInclude]
    public string Description { get; internal set; }

    /// <summary>
    /// 作业触发器状态
    /// </summary>
    [JsonInclude]
    public TriggerStatus Status { get; internal set; } = TriggerStatus.Ready;

    /// <summary>
    /// 起始时间
    /// </summary>
    [JsonInclude]
    public DateTime? StartTime { get; internal set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    [JsonInclude]
    public DateTime? EndTime { get; internal set; }

    /// <summary>
    /// 最近运行时间
    /// </summary>
    [JsonInclude]
    public DateTime? LastRunTime { get; internal set; }

    /// <summary>
    /// 下一次运行时间
    /// </summary>
    [JsonInclude]
    public DateTime? NextRunTime { get; internal set; }

    /// <summary>
    /// 触发次数
    /// </summary>
    [JsonInclude]
    public long NumberOfRuns { get; internal set; }

    /// <summary>
    /// 最大触发次数
    /// </summary>
    /// <remarks>
    /// <para>0：不限制</para>
    /// <para>n：N 次</para>
    /// </remarks>
    [JsonInclude]
    public long MaxNumberOfRuns { get; internal set; }

    /// <summary>
    /// 出错次数
    /// </summary>
    [JsonInclude]
    public long NumberOfErrors { get; internal set; }

    /// <summary>
    /// 最大出错次数
    /// </summary>
    /// <remarks>
    /// <para>0：不限制</para>
    /// <para>n：N 次</para>
    /// </remarks>
    [JsonInclude]
    public long MaxNumberOfErrors { get; internal set; }

    /// <summary>
    /// 重试次数
    /// </summary>
    [JsonInclude]
    public int NumRetries { get; internal set; } = 0;

    /// <summary>
    /// 重试间隔时间
    /// </summary>
    /// <remarks>默认1000毫秒</remarks>
    [JsonInclude]
    public int RetryTimeout { get; internal set; } = 1000;

    /// <summary>
    /// 是否立即启动
    /// </summary>
    [JsonInclude]
    public bool StartNow { get; internal set; } = true;

    /// <summary>
    /// 是否启动时执行一次
    /// </summary>
    [JsonInclude]
    public bool RunOnStart { get; internal set; } = false;

    /// <summary>
    /// 是否在启动时重置最大触发次数等于一次的作业
    /// </summary>
    /// <remarks>解决因持久化数据已完成一次触发但启动时不再执行的问题</remarks>
    [JsonInclude]
    public bool ResetOnlyOnce { get; internal set; } = true;

    /// <summary>
    /// 作业触发器更新时间
    /// </summary>
    [JsonInclude]
    public DateTime? UpdatedTime { get; internal set; }

    /// <summary>
    /// 标记作业触发器持久化行为
    /// </summary>
    internal PersistenceBehavior Behavior { get; set; } = PersistenceBehavior.Appended;

    /// <summary>
    /// 作业触发器运行时类型
    /// </summary>
    internal Type RuntimeTriggerType { get; set; }

    /// <summary>
    /// 作业触发器运行时参数
    /// </summary>
    internal object[] RuntimeTriggerArgs { get; set; }
}