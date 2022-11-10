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
/// 作业信息
/// </summary>
[SuppressSniffer]
public class JobDetail
{
    /// <summary>
    /// 作业 Id
    /// </summary>
    public string JobId { get; internal set; }

    /// <summary>
    /// 作业处理程序类型
    /// </summary>
    /// <remarks>存储的是类型的 FullName</remarks>
    public string JobType { get; internal set; }

    /// <summary>
    /// 作业处理程序类型所在程序集
    /// </summary>
    /// <remarks>存储的是程序集 Name</remarks>
    public string AssemblyName { get; internal set; }

    /// <summary>
    /// 描述信息
    /// </summary>
    public string Description { get; internal set; }

    /// <summary>
    /// 是否采用并发执行
    /// </summary>
    /// <remarks>如果设置为 false，那么使用串行执行</remarks>
    public bool Concurrent { get; internal set; } = true;

    /// <summary>
    /// 是否扫描 IJob 实现类 [JobTrigger] 特性触发器
    /// </summary>
    public bool ScanTriggers { get; internal set; } = false;

    /// <summary>
    /// 标记其他触发器正在执行
    /// </summary>
    /// <remarks>当 <see cref="Concurrent"/> 为 false 时有效，也就是串行执行</remarks>
    public bool Blocked { get; internal set; } = false;

    /// <summary>
    /// 作业处理程序运行时类型
    /// </summary>
    internal Type RuntimeJobType { get; set; }
}