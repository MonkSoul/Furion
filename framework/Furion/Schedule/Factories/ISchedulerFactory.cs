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
/// 作业调度计划工厂服务
/// </summary>
public partial interface ISchedulerFactory
{
    /// <summary>
    /// 初始化作业调度计划
    /// </summary>
    /// <param name="stoppingToken">后台主机服务停止时取消任务 Token</param>
    /// <returns><see cref="Task"/></returns>
    Task InitializeAsync(CancellationToken stoppingToken = default);

    /// <summary>
    /// 查找所有符合触发的作业调度计划
    /// </summary>
    /// <param name="checkTime">检查时间</param>
    /// <returns></returns>
    IEnumerable<IScheduler> GetSchedulersThatShouldRun(DateTime checkTime);

    /// <summary>
    /// 等待作业调度后台服务被唤醒
    /// </summary>
    /// <param name="stoppingToken">后台主机服务停止时取消任务 Token</param>
    /// <returns><see cref="Task"/></returns>
    Task WaitForWakeUpAsync(CancellationToken stoppingToken = default);

    /// <summary>
    /// 强制唤醒作业调度后台服务
    /// </summary>
    void ForceRefresh();

    /// <summary>
    /// 记录作业执行状态
    /// </summary>
    /// <param name="jobDetail">作业信息</param>
    /// <param name="jobTrigger">作业触发器</param>
    /// <param name="behavior">作业持久化行为</param>
    void Record(JobDetail jobDetail, JobTrigger jobTrigger, PersistenceBehavior behavior = PersistenceBehavior.UpdateTrigger);
}