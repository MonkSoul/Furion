// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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