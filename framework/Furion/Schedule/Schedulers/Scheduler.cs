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
/// 作业计划
/// </summary>
internal sealed partial class Scheduler : IScheduler
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jobDetail">作业信息</param>
    /// <param name="triggers">作业触发器集合</param>
    internal Scheduler(JobDetail jobDetail, Dictionary<string, Trigger> triggers)
    {
        JobId = jobDetail.JobId;
        GroupName = jobDetail.GroupName;
        JobDetail = jobDetail;
        Triggers = triggers;
    }

    /// <summary>
    /// 作业 Id
    /// </summary>
    internal string JobId { get; private set; }

    /// <summary>
    /// 作业组名称
    /// </summary>
    internal string GroupName { get; private set; }

    /// <summary>
    /// 作业信息
    /// </summary>
    internal JobDetail JobDetail { get; private set; }

    /// <summary>
    /// 作业触发器集合
    /// </summary>
    internal Dictionary<string, Trigger> Triggers { get; private set; } = new();

    /// <summary>
    /// 作业处理程序实例
    /// </summary>
    internal IJob JobHandler { get; set; }

    /// <summary>
    /// 作业计划工厂
    /// </summary>
    internal ISchedulerFactory Factory { get; set; }

    /// <summary>
    /// 作业调度器日志服务
    /// </summary>
    internal IScheduleLogger Logger { get; set; }

    /// <summary>
    /// 是否使用 UTC 时间
    /// </summary>
    internal bool UseUtcTimestamp { get; set; }
}