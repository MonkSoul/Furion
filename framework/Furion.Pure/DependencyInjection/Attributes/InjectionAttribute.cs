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

namespace Furion.DependencyInjection;

/// <summary>
/// 设置依赖注入方式
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Class)]
public class InjectionAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="exceptInterfaces"></param>
    public InjectionAttribute(params Type[] exceptInterfaces)
        : this(InjectionActions.Add, exceptInterfaces)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="action"></param>
    /// <param name="exceptInterfaces"></param>
    public InjectionAttribute(InjectionActions action, params Type[] exceptInterfaces)
    {
        Action = action;
        Pattern = InjectionPatterns.All;
        ExceptInterfaces = exceptInterfaces ?? Array.Empty<Type>();
        Order = 0;
    }

    /// <summary>
    /// 添加服务方式，存在不添加，或继续添加
    /// </summary>
    public InjectionActions Action { get; set; }

    /// <summary>
    /// 注册选项
    /// </summary>
    public InjectionPatterns Pattern { get; set; }

    /// <summary>
    /// 注册别名
    /// </summary>
    /// <remarks>多服务时使用</remarks>
    public string Named { get; set; }

    /// <summary>
    /// 排序，排序越大，则在后面注册
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// 排除接口
    /// </summary>
    public Type[] ExceptInterfaces { get; set; }

    /// <summary>
    /// 代理类型，必须继承 DispatchProxy、IDispatchProxy
    /// </summary>
    public Type Proxy { get; set; }
}