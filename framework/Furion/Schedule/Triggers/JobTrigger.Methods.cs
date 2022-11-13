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
/// 作业触发器基类
/// </summary>
public abstract partial class JobTrigger
{
    /// <summary>
    /// 计算下一个触发时间
    /// </summary>
    /// <param name="startAt">起始时间</param>
    /// <returns><see cref="DateTime"/></returns>
    public virtual DateTime GetNextOccurrence(DateTime startAt) => throw new NotImplementedException();

    /// <summary>
    /// 执行条件检查
    /// </summary>
    /// <param name="startAt">起始时间</param>
    /// <returns><see cref="bool"/></returns>
    public virtual bool ShouldRun(DateTime startAt)
    {
        return NextRunTime.Value <= startAt
            && LastRunTime != NextRunTime;
    }

    /// <summary>
    /// 记录运行信息和计算下一个触发时间
    /// </summary>
    internal void Increment()
    {
        // 阻塞状态并没有实际执行，此时忽略次数递增和最近运行时间赋值
        if (Status != TriggerStatus.Blocked)
        {
            NumberOfRuns++;
            LastRunTime = NextRunTime;
        }

        NextRunTime = GetNextRunTime();
    }

    /// <summary>
    /// 计算下一次运行时间
    /// </summary>
    /// <returns></returns>
    internal DateTime? GetNextRunTime()
    {
        var startAt = GetStartAt();
        return startAt == null
            ? null
            : GetNextOccurrence(startAt.Value);
    }

    /// <summary>
    /// 记录错误信息，包含错误次数和运行状态
    /// </summary>
    internal void IncrementErrors()
    {
        NumberOfErrors++;

        // 如果错误次数大于最大错误数，则表示该触发器是奔溃状态
        if (MaxNumberOfErrors > 0 && NumberOfErrors >= MaxNumberOfErrors) SetStatus(TriggerStatus.Panic);
        // 否则是就绪（错误状态）
        else SetStatus(TriggerStatus.ErrorToReady);
    }

    /// <summary>
    /// 设置作业触发器状态
    /// </summary>
    /// <param name="status"></param>
    internal void SetStatus(TriggerStatus status)
    {
        if (Status == status) return;
        Status = status;
    }

    /// <summary>
    /// 执行条件检查（内部检查）
    /// </summary>
    /// <param name="startAt">起始时间</param>
    /// <returns><see cref="bool"/></returns>
    internal bool InternalShouldRun(DateTime startAt)
    {
        // 状态检查
        if (Status != TriggerStatus.Ready
            && Status != TriggerStatus.ErrorToReady
            && Status != TriggerStatus.Running
            && Status != TriggerStatus.Blocked)  // 本该执行但是没有执行
        {
            return false;
        }

        // 开始时间检查
        if (StartTime != null && StartTime.Value > startAt)
        {
            SetStatus(TriggerStatus.Backlog);
            return false;
        }

        // 结束时间检查
        if (EndTime != null && EndTime.Value < startAt)
        {
            SetStatus(TriggerStatus.Archived);
            return false;
        }

        // 下一次运行时间空判断
        if (NextRunTime == null)
        {
            SetStatus(TriggerStatus.None);
            return false;
        }

        // 最大次数判断
        if (MaxNumberOfRuns > 0 && NumberOfRuns >= MaxNumberOfRuns)
        {
            SetStatus(TriggerStatus.Overrun);
            return false;
        }

        // 最大错误数判断
        if (MaxNumberOfErrors > 0 && NumberOfErrors >= MaxNumberOfErrors)
        {
            SetStatus(TriggerStatus.Panic);
            return false;
        }

        // 调用派生类 ShouldRun 方法
        return ShouldRun(startAt);
    }

    /// <summary>
    /// 获取触发器初始化时间
    /// </summary>
    /// <returns><see cref="DateTime"/> 或者 null</returns>
    private DateTime? GetStartAt()
    {
        var nowTime = DateTime.UtcNow;
        if (StartTime == null)
        {
            if (EndTime == null) return nowTime;
            else return EndTime.Value < nowTime ? null : nowTime;
        }
        else return StartTime.Value < nowTime ? nowTime : StartTime.Value;
    }
}