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
/// 记录任务数据
/// </summary>
internal sealed class WorkerRecord
{
    /// <summary>
    /// 定时器
    /// </summary>
    internal SpareTimer Timer { get; set; }

    /// <summary>
    /// 任务执行计数
    /// </summary>
    internal long Tally { get; set; } = 0;

    /// <summary>
    /// 解决定时器重入问题
    /// </summary>
    internal long Interlocked { get; set; } = 0;

    /// <summary>
    /// Cron 表达式实际执行计数
    /// </summary>
    internal long CronActualTally { get; set; } = 0;

    /// <summary>
    /// 是否上一个已完成
    /// </summary>
    internal bool IsCompleteOfPrev { get; set; } = true;
}