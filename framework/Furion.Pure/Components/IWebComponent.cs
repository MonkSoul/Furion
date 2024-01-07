// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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