// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
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

using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace Furion.DatabaseAccessor;

/// <summary>
/// Sql 代理方法元数据
/// </summary>
[SuppressSniffer]
public sealed class SqlProxyMethod
{
    /// <summary>
    /// 参数模型
    /// </summary>
    public object ParameterModel { get; set; }

    /// <summary>
    /// 方法返回值
    /// </summary>
    public Type ReturnType { get; internal set; }

    /// <summary>
    /// 数据库操作上下文
    /// </summary>
    public DbContext Context { get; set; }

    /// <summary>
    /// 是否是异步方法
    /// </summary>
    public bool IsAsync { get; internal set; }

    /// <summary>
    /// 命令类型
    /// </summary>
    public CommandType CommandType { get; set; }

    /// <summary>
    /// 最终 Sql 语句
    /// </summary>
    public string FinalSql { get; set; }

    /// <summary>
    /// 当前执行的方法
    /// </summary>
    public MethodInfo Method { get; internal set; }

    /// <summary>
    /// 传递参数
    /// </summary>
    public object[] Arguments { get; internal set; }

    /// <summary>
    /// 拦截Id
    /// </summary>
    public string InterceptorId { get; internal set; }
}