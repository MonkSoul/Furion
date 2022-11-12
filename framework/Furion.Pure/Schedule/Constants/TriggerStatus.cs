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
/// 作业触发器状态
/// </summary>
[SuppressSniffer]
public enum TriggerStatus : uint
{
    /// <summary>
    /// 积压
    /// </summary>
    /// <remarks>起始时间大于当前时间</remarks>
    Backlog = 0,

    /// <summary>
    /// 就绪
    /// </summary>
    Ready = 1,

    /// <summary>
    /// 正在运行
    /// </summary>
    Running = 2,

    /// <summary>
    /// 暂停
    /// </summary>
    Pause = 3,

    /// <summary>
    /// 阻塞
    /// </summary>
    /// <remarks>本该执行但是没有执行</remarks>
    Blocked = 4,

    /// <summary>
    /// 就绪（此前执行失败）
    /// </summary>
    /// <remarks>运行错误当并未超出最大错误数，进入下一轮就绪</remarks>
    ErrorToReady = 5,

    /// <summary>
    /// 归档
    /// </summary>
    /// <remarks>结束时间小于当前时间</remarks>
    Archived = 6,

    /// <summary>
    /// 崩溃
    /// </summary>
    /// <remarks>错误次数超出了最大错误数</remarks>
    Panic = 7,

    /// <summary>
    /// 超限
    /// </summary>
    /// <remarks>运行次数超出了最大限制</remarks>
    Overrun = 8,

    /// <summary>
    /// 空
    /// </summary>
    /// <remarks>下一次执行时间为 null</remarks>
    None = 9
}