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

namespace Furion.Schedule;

/// <summary>
/// 作业集群服务
/// </summary>
/// <remarks>集群并不能达到负载均衡的效果，而仅仅提供了故障转移的功能，当一个任务服务器宕机时，另一个任务服务器会启动</remarks>
public interface IJobClusterServer
{
    /// <summary>
    /// 当前作业调度器启动通知
    /// </summary>
    /// <param name="context">作业集群服务上下文</param>
    void Start(JobClusterContext context);

    /// <summary>
    /// 等待被唤醒
    /// </summary>
    /// <param name="context">作业集群服务上下文</param>
    /// <returns><see cref="Task"/></returns>
    Task WaitingForAsync(JobClusterContext context);

    /// <summary>
    /// 当前作业调度器停止通知
    /// </summary>
    /// <param name="context">作业集群服务上下文</param>
    void Stop(JobClusterContext context);

    /// <summary>
    /// 当前作业调度器宕机
    /// </summary>
    /// <param name="context">作业集群服务上下文</param>
    void Crash(JobClusterContext context);
}