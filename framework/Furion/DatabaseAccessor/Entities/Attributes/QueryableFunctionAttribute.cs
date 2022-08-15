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

using Microsoft.EntityFrameworkCore;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 实体函数配置特性
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Method)]
public class QueryableFunctionAttribute : DbFunctionAttribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">函数名</param>
    /// <param name="schema">架构名</param>
    public QueryableFunctionAttribute(string name, string schema = null) : base(name, schema)
    {
        DbContextLocators = Array.Empty<Type>();
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">函数名</param>
    /// <param name="schema">架构名</param>
    /// <param name="dbContextLocators">数据库上下文定位器</param>
    public QueryableFunctionAttribute(string name, string schema = null, params Type[] dbContextLocators) : base(name, schema)
    {
        DbContextLocators = dbContextLocators ?? Array.Empty<Type>();
    }

    /// <summary>
    /// 数据库上下文定位器
    /// </summary>
    public Type[] DbContextLocators { get; set; }
}