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

namespace Furion.TaskScheduler;

/// <summary>
/// 内置时间调度器
/// </summary>
[SuppressSniffer]
public sealed class SpareTimer : System.Timers.Timer
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="workerName"></param>
    internal SpareTimer(string workerName = default) : base()
    {
        WorkerName = workerName ?? Guid.NewGuid().ToString("N");

        // 记录当前定时器
        if (!SpareTime.WorkerRecords.TryAdd(WorkerName, new WorkerRecord
        {
            Timer = this
        })) throw new InvalidOperationException($"The worker name `{WorkerName}` is exist.");
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="workerName"></param>
    internal SpareTimer(double interval, string workerName = default) : base(interval)
    {
        WorkerName = workerName ?? Guid.NewGuid().ToString("N");

        // 记录当前定时器
        if (!SpareTime.WorkerRecords.TryAdd(WorkerName, new WorkerRecord
        {
            Interlocked = 0,
            Tally = 0,
            Timer = this
        })) throw new InvalidOperationException($"The worker name `{WorkerName}` is exist.");
    }

    /// <summary>
    /// 当前任务名
    /// </summary>
    public string WorkerName { get; private set; }

    /// <summary>
    /// 任务类型
    /// </summary>
    public SpareTimeTypes Type { get; internal set; }

    /// <summary>
    /// 任务描述
    /// </summary>
    public string Description { get; internal set; }

    /// <summary>
    /// 任务状态
    /// </summary>
    public SpareTimeStatus Status { get; internal set; }

    /// <summary>
    /// 执行类型
    /// </summary>
    public SpareTimeExecuteTypes ExecuteType { get; internal set; } = SpareTimeExecuteTypes.Parallel;

    /// <summary>
    /// 异常信息
    /// </summary>
    public Dictionary<long, Exception> Exception { get; internal set; } = new Dictionary<long, Exception>();

    /// <summary>
    /// 任务执行计数
    /// </summary>
    public long Tally { get; internal set; } = 0;
}