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

namespace Furion.Scheduler;

/// <summary>
/// 作业调度持久化上下文
/// </summary>
[SuppressSniffer]
public sealed class PersistenceContext
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="jobTriggerId">作业触发器 Id</param>
    /// <param name="jobDetail">作业信息</param>
    /// <param name="jobTrigger">作业触发器</param>
    /// <param name="behavior">作业持久化行为</param>
    internal PersistenceContext(string jobId
        , string jobTriggerId
        , JobDetail jobDetail
        , JobTrigger jobTrigger
        , PersistenceBehavior behavior)
    {
        JobId = jobId;
        JobTriggerId = jobTriggerId;
        JobDetail = jobDetail;
        JobTrigger = jobTrigger;
        Behavior = behavior;
    }

    /// <summary>
    /// 作业 Id
    /// </summary>
    public string JobId { get; }

    /// <summary>
    /// 作业触发器 Id
    /// </summary>
    public string JobTriggerId { get; }

    /// <summary>
    /// 作业信息
    /// </summary>
    public JobDetail JobDetail { get; }

    /// <summary>
    /// 作业触发器
    /// </summary>
    public JobTrigger JobTrigger { get; }

    /// <summary>
    /// 作业持久化行为
    /// </summary>
    public PersistenceBehavior Behavior { get; }
}