// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion;
using Furion.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// WebApplication 拓展
/// </summary>
public static class AppWebApplicationBuilderExtensions
{
#if !NET5_0
    /// <summary>
    /// Web 应用注入
    /// </summary>
    /// <param name="webApplicationBuilder">Web应用构建器</param>
    /// <param name="configure"></param>
    /// <returns>WebApplicationBuilder</returns>
    public static WebApplicationBuilder Inject(this WebApplicationBuilder webApplicationBuilder, Action<WebApplicationBuilder, InjectOptions> configure = default)
    {
        // 载入服务配置选项
        var configureOptions = new InjectOptions();
        configure?.Invoke(webApplicationBuilder, configureOptions);

        // 为了兼容 .NET 5 无缝升级至 .NET 6，故传递 WebHost 和 Host
        InternalApp.WebHostEnvironment = webApplicationBuilder.Environment;

        // 初始化配置
        InternalApp.ConfigureApplication(webApplicationBuilder.WebHost, webApplicationBuilder.Host);

        return webApplicationBuilder;
    }

    /// <summary>
    /// 注册依赖组件
    /// </summary>
    /// <typeparam name="TComponent">派生自 <see cref="IServiceComponent"/></typeparam>
    /// <param name="webApplicationBuilder">Web应用构建器</param>
    /// <param name="options">组件参数</param>
    /// <returns></returns>
    public static WebApplicationBuilder AddComponent<TComponent>(this WebApplicationBuilder webApplicationBuilder, object options = default)
        where TComponent : class, IServiceComponent, new()
    {
        webApplicationBuilder.Services.AddComponent<TComponent>(options);

        return webApplicationBuilder;
    }

    /// <summary>
    /// 注册依赖组件
    /// </summary>
    /// <typeparam name="TComponent">派生自 <see cref="IServiceComponent"/></typeparam>
    /// <typeparam name="TComponentOptions">组件参数</typeparam>
    /// <param name="webApplicationBuilder">Web应用构建器</param>
    /// <param name="options">组件参数</param>
    /// <returns><see cref="WebApplicationBuilder"/></returns>
    public static WebApplicationBuilder AddComponent<TComponent, TComponentOptions>(this WebApplicationBuilder webApplicationBuilder, TComponentOptions options = default)
        where TComponent : class, IServiceComponent, new()
    {
        webApplicationBuilder.Services.AddComponent<TComponent, TComponentOptions>(options);

        return webApplicationBuilder;
    }

    /// <summary>
    /// 注册依赖组件
    /// </summary>
    /// <param name="webApplicationBuilder">Web应用构建器</param>
    /// <param name="componentType">组件类型</param>
    /// <param name="options">组件参数</param>
    /// <returns><see cref="WebApplicationBuilder"/></returns>
    public static WebApplicationBuilder AddComponent(this WebApplicationBuilder webApplicationBuilder, Type componentType, object options = default)
    {
        webApplicationBuilder.Services.AddComponent(componentType, options);

        return webApplicationBuilder;
    }

    /// <summary>
    /// 注册 WebApplicationBuilder 依赖组件
    /// </summary>
    /// <typeparam name="TComponent">派生自 <see cref="IWebComponent"/></typeparam>
    /// <param name="webApplicationBuilder">Web应用构建器</param>
    /// <param name="options">组件参数</param>
    /// <returns><see cref="WebApplicationBuilder"/></returns>
    public static WebApplicationBuilder AddWebComponent<TComponent>(this WebApplicationBuilder webApplicationBuilder, object options = default)
        where TComponent : class, IWebComponent, new()
    {
        webApplicationBuilder.AddWebComponent<TComponent>(options);

        return webApplicationBuilder;
    }

    /// <summary>
    /// 注册 WebApplicationBuilder 依赖组件
    /// </summary>
    /// <typeparam name="TComponent">派生自 <see cref="IWebComponent"/></typeparam>
    /// <typeparam name="TComponentOptions">组件参数</typeparam>
    /// <param name="webApplicationBuilder">Web应用构建器</param>
    /// <param name="options">组件参数</param>
    /// <returns><see cref="WebApplicationBuilder"/></returns>
    public static WebApplicationBuilder AddWebComponent<TComponent, TComponentOptions>(this WebApplicationBuilder webApplicationBuilder, TComponentOptions options = default)
        where TComponent : class, IWebComponent, new()
    {
        webApplicationBuilder.AddWebComponent<TComponent, TComponentOptions>(options);

        return webApplicationBuilder;
    }

    /// <summary>
    /// 注册 WebApplicationBuilder 依赖组件
    /// </summary>
    /// <param name="webApplicationBuilder"><see cref="WebApplicationBuilder"/></param>
    /// <param name="componentType">组件类型</param>
    /// <param name="options">组件参数</param>
    /// <returns><see cref="WebApplicationBuilder"/></returns>
    public static WebApplicationBuilder AddWebComponent(this WebApplicationBuilder webApplicationBuilder, Type componentType, object options = default)
    {
        // 创建组件依赖链
        var componentContextLinkList = Penetrates.CreateDependLinkList(componentType, options);

        // 逐条创建组件实例并调用
        foreach (var context in componentContextLinkList)
        {
            // 创建组件实例
            var component = Activator.CreateInstance(context.ComponentType) as IWebComponent;

            // 调用
            component.Load(webApplicationBuilder, context);
        }

        return webApplicationBuilder;
    }

    /// <summary>
    /// 解决 .NET6 WebApplication 模式下二级虚拟目录错误问题
    /// </summary>
    /// <param name="app"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseVirtualPath(this WebApplication app, Action<IApplicationBuilder> configuration)
    {
        if (!string.IsNullOrWhiteSpace(App.Settings.VirtualPath))
        {
            return app.Map(App.Settings.VirtualPath, configuration);
        }

        configuration(app);
        return app;
    }
#endif
}