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

using System.ComponentModel;

namespace Furion.TaskScheduler;

/// <summary>
/// 任务状态
/// </summary>
[SuppressSniffer]
public enum SpareTimeStatus
{
    /// <summary>
    /// 运行中
    /// </summary>
    [Description("运行中")]
    Running,

    /// <summary>
    /// 已停止或未启动
    /// </summary>
    [Description("已停止或未启动")]
    Stopped,

    /// <summary>
    /// 单次执行失败
    /// </summary>
    [Description("任务停止并失败")]
    Failed,

    /// <summary>
    /// 任务已取消或没有该任务
    /// </summary>
    [Description("任务已取消或没有该任务")]
    CanceledOrNone
}