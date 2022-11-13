﻿// MIT License
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
/// 作业持久化行为
/// </summary>
[SuppressSniffer]
public enum PersistenceBehavior : uint
{
    /// <summary>
    /// 追加作业
    /// </summary>
    AppendJob = 0,

    /// <summary>
    /// 更新作业
    /// </summary>
    UpdateJob = 1,

    /// <summary>
    /// 删除作业
    /// </summary>
    RemoveJob = 2,

    /// <summary>
    /// 追加作业触发器
    /// </summary>
    AppendTrigger = 3,

    /// <summary>
    /// 更新作业触发器
    /// </summary>
    UpdateTrigger = 4,

    /// <summary>
    /// 删除作业触发器
    /// </summary>
    RemoveTrigger = 5
}