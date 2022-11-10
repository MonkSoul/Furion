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

namespace Furion.Scheduler;

/// <summary>
/// Cron 表达式作业触发器
/// </summary>
internal sealed class CronTrigger : JobTrigger
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="schedule">Cron 表达式</param>
    /// <param name="format">Cron 表达式格式化类型</param>
    public CronTrigger(string schedule, int format)
    {
        Crontab = Crontab.Parse(schedule, (CronStringFormat)format);
    }

    /// <summary>
    /// <see cref="Crontab"/> 对象
    /// </summary>
    private Crontab Crontab { get; }

    /// <summary>
    /// 计算下一个触发时间
    /// </summary>
    /// <param name="startAt">起始时间</param>
    /// <returns><see cref="DateTime"/>?</returns>
    public override DateTime GetNextOccurrence(DateTime startAt)
    {
        return Crontab.GetNextOccurrence(startAt);
    }

    /// <summary>
    /// 作业触发器转字符串输出
    /// </summary>
    /// <returns><see cref="string"/></returns>
    public override string ToString()
    {
        return $"{Description} {Crontab}";
    }
}