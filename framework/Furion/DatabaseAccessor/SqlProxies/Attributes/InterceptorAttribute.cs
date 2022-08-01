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

namespace Furion.DatabaseAccessor;

/// <summary>
/// Sql 代理拦截器
/// </summary>
/// <remarks>如果贴在静态方法中且 InterceptorId/MethodName 为空，则为全局拦截</remarks>
[SuppressSniffer, AttributeUsage(AttributeTargets.Method)]
public class InterceptorAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public InterceptorAttribute()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="interceptorIds"></param>
    public InterceptorAttribute(params string[] interceptorIds)
    {
        InterceptorIds = interceptorIds;
    }

    /// <summary>
    /// 方法名称
    /// </summary>
    public string[] InterceptorIds { get; set; }
}