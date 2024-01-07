// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Components;
using Microsoft.AspNetCore.Hosting;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// 组件应用中间件拓展类
/// </summary>
[SuppressSniffer]
public static class ComponentApplicationBuilderExtensions
{
    /// <summary>
    /// 注册依赖组件
    /// </summary>
    /// <typeparam name="TComponent">派生自 <see cref="IApplicationComponent"/></typeparam>
    /// <param name="app"><see cref="IApplicationBuilder"/></param>
    /// <param name="env"><see cref="IWebHostEnvironment"/></param>
    /// <param name="options">组件参数</param>
    /// <returns><see cref="IApplicationBuilder"/></returns>
    public static IApplicationBuilder UseComponent<TComponent>(this IApplicationBuilder app, IWebHostEnvironment env, object options = default)
        where TComponent : class, IApplicationComponent, new()
    {
        return app.UseComponent<TComponent, object>(env, options);
    }

    /// <summary>
    /// 注册依赖组件
    /// </summary>
    /// <typeparam name="TComponent">派生自 <see cref="IApplicationComponent"/></typeparam>
    /// <typeparam name="TComponentOptions">组件参数</typeparam>
    /// <param name="app"><see cref="IApplicationBuilder"/></param>
    /// <param name="env"><see cref="IWebHostEnvironment"/></param>
    /// <param name="options">组件参数</param>
    /// <returns><see cref="IApplicationBuilder"/></returns>
    public static IApplicationBuilder UseComponent<TComponent, TComponentOptions>(this IApplicationBuilder app, IWebHostEnvironment env, TComponentOptions options = default)
        where TComponent : class, IApplicationComponent, new()
    {
        return app.UseComponent(env, typeof(TComponent), options);
    }

    /// <summary>
    /// 注册依赖组件
    /// </summary>
    /// <param name="app"><see cref="IApplicationBuilder"/></param>
    /// <param name="env"><see cref="IWebHostEnvironment"/></param>
    /// <param name="componentType">组件类型</param>
    /// <param name="options">组件参数</param>
    /// <returns><see cref="IApplicationBuilder"/></returns>
    public static IApplicationBuilder UseComponent(this IApplicationBuilder app, IWebHostEnvironment env, Type componentType, object options = default)
    {
        // 创建组件依赖链
        var componentContextLinkList = Penetrates.CreateDependLinkList(componentType, options);

        // 逐条创建组件实例并调用
        foreach (var componentContext in componentContextLinkList)
        {
            // 创建组件实例
            var component = Activator.CreateInstance(componentContext.ComponentType) as IApplicationComponent;

            // 调用
            component.Load(app, env, componentContext);
        }

        return app;
    }
}