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
/// 作业调度持久化器
/// </summary>
public interface IJobPersistence
{
    /// <summary>
    /// 作业调度器预加载服务
    /// </summary>
    /// <returns><see cref="IEnumerable{SchedulerBuilder}"/></returns>
    IEnumerable<SchedulerBuilder> Preload();

    /// <summary>
    /// 作业计划加载完成通知
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <param name="builder">作业计划构建器</param>
    /// <returns><see cref="SchedulerBuilder"/></returns>
    SchedulerBuilder OnLoaded(string jobId, SchedulerBuilder builder);

    /// <summary>
    /// 作业信息更改通知
    /// </summary>
    /// <param name="context">作业信息持久化上下文</param>
    void OnChanged(PersistenceContext context);

    /// <summary>
    /// 作业触发器更改通知
    /// </summary>
    /// <param name="context">作业触发器持久化上下文</param>
    void OnTriggerChanged(PersistenceTriggerContext context);
}