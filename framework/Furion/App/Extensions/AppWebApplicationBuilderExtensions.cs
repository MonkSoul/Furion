// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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