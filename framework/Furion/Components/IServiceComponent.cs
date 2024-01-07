// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Components;
using Microsoft.Extensions.DependencyInjection;

namespace System;

/// <summary>
/// 服务组件依赖接口
/// </summary>
public interface IServiceComponent : IComponent
{
    /// <summary>
    /// 装载服务
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="componentContext">组件上下文</param>
    void Load(IServiceCollection services, ComponentContext componentContext);
}