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

using Furion.Components;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace System;

/// <summary>
/// 应用中间件接口
/// </summary>
public interface IApplicationComponent : IComponent
{
    /// <summary>
    /// 装置中间件
    /// </summary>
    /// <param name="app"><see cref="IApplicationBuilder"/></param>
    /// <param name="env"><see cref="IWebHostEnvironment"/></param>
    /// <param name="componentContext">组件上下文</param>
    void Load(IApplicationBuilder app, IWebHostEnvironment env, ComponentContext componentContext);
}