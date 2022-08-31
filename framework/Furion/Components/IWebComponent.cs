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

using Furion.Components;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace System;

/// <summary>
/// Web 组件依赖接口
/// </summary>
/// <remarks>注意，此时 Furion 还未载入</remarks>
public interface IWebComponent : IComponent
{
#if !NET5_0
    /// <summary>
    /// 装置 Web 应用构建器
    /// </summary>
    /// <remarks>注意，此时 Furion 还未载入</remarks>
    /// <param name="builder"><see cref="WebApplicationBuilder"/></param>
    /// <param name="componentContext">组件上下文</param>
    void Load(WebApplicationBuilder builder, ComponentContext componentContext);
#else
    /// <summary>
    /// 装置 Web 应用构建器
    /// </summary>
    /// <remarks>注意，此时 Furion 还未载入</remarks>
    /// <param name="builder"><see cref="IWebHostBuilder"/></param>
    /// <param name="componentContext">组件上下文</param>
    void Load(IWebHostBuilder builder, ComponentContext componentContext);
#endif
}