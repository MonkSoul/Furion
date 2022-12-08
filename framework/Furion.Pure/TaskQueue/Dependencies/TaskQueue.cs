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

using System.Threading.Channels;

namespace Furion.TaskQueue;

/// <summary>
/// 任务队列默认实现
/// </summary>
internal sealed partial class TaskQueue : ITaskQueue
{
    /// <summary>
    /// 队列通道
    /// </summary>
    private readonly Channel<Func<CancellationToken, ValueTask>> _queue;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="capacity">队列通道默认容量，超过该容量进入等待</param>
    public TaskQueue(int capacity)
    {
        // 配置通道，设置超出默认容量后进入等待
        var boundedChannelOptions = new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };

        // 创建有限容量通道
        _queue = Channel.CreateBounded<Func<CancellationToken, ValueTask>>(boundedChannelOptions);
    }

    /// <summary>
    /// 任务项入队
    /// </summary>
    /// <param name="taskHandler">任务处理委托</param>
    /// <param name="delay">延迟时间</param>
    public void Enqueue(Action taskHandler, int delay = 0)
    {
        // 空检查
        if (taskHandler == default)
        {
            throw new ArgumentNullException(nameof(taskHandler));
        }

        _ = EnqueueAsync(_ =>
          {
              taskHandler();
              return ValueTask.CompletedTask;
          }, delay);
    }

    /// <summary>
    /// 任务项入队
    /// </summary>
    /// <param name="taskHandler">任务处理委托</param>
    /// <param name="delay">延迟时间</param>
    /// <returns><see cref="ValueTask"/></returns>
    public async ValueTask EnqueueAsync(Func<CancellationToken, ValueTask> taskHandler, int delay = 0)
    {
        // 空检查
        if (taskHandler == default)
        {
            throw new ArgumentNullException(nameof(taskHandler));
        }

        // 写入管道队列
        await _queue.Writer.WriteAsync(async (cancellationToken) =>
        {
            if (delay > 0)
            {
                await Task.Delay(delay, cancellationToken);
            }

            await taskHandler(cancellationToken);
        });
    }

    /// <summary>
    /// 任务项出队
    /// </summary>
    /// <param name="cancellationToken">取消任务 Token</param>
    /// <returns><see cref="ValueTask"/></returns>
    public async ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(CancellationToken cancellationToken)
    {
        // 读取管道队列
        var taskHandler = await _queue.Reader.ReadAsync(cancellationToken);
        return taskHandler;
    }
}