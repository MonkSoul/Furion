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
/// 定时器执行状态器
/// </summary>
[SuppressSniffer]
public sealed class SpareTimerExecuter
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="timer"></param>
    /// <param name="status"></param>
    public SpareTimerExecuter(SpareTimer timer, int status)
    {
        Timer = timer;
        Status = status;
    }

    /// <summary>
    /// 定时器
    /// </summary>
    public SpareTimer Timer { get; internal set; }

    /// <summary>
    /// 状态
    /// </summary>
    /// <remarks>
    /// <para>0：任务开始</para>
    /// <para>1：执行之前</para>
    /// <para>2：执行成功</para>
    /// <para>3：执行失败</para>
    /// <para>-1：任务停止</para>
    /// <para>-2：任务取消</para>
    /// </remarks>
    public int Status { get; internal set; }
}