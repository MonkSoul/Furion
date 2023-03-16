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
using Furion.Extensions;
using Furion.Reflection;
using Microsoft.AspNetCore.Hosting;

namespace Microsoft.Extensions.Hosting;

/// <summary>
/// 主机构建器拓展类
/// </summary>
[SuppressSniffer]
public static class HostBuilderExtensions
{
    /// <summary>
    /// Web 主机注入
    /// </summary>
    /// <param name="hostBuilder">Web主机构建器</param>
    /// <param name="configure"></param>
    /// <returns>IWebHostBuilder</returns>
    public static IWebHostBuilder Inject(this IWebHostBuilder hostBuilder, Action<IWebHostBuilder, InjectOptions> configure = default)
    {
        // 载入服务配置选项
        var configureOptions = new InjectOptions();
        configure?.Invoke(hostBuilder, configureOptions);

        // 获取默认程序集名称
        var defaultAssemblyName = configureOptions.AssemblyName ?? Reflect.GetAssemblyName(typeof(HostBuilderExtensions));

        //  获取环境变量 ASPNETCORE_HOSTINGSTARTUPASSEMBLIES 配置
        var environmentVariables = Environment.GetEnvironmentVariable("ASPNETCORE_HOSTINGSTARTUPASSEMBLIES");
        var combineAssembliesName = $"{defaultAssemblyName};{environmentVariables}".ClearStringAffixes(1, ";");

        hostBuilder.UseSetting(WebHostDefaults.HostingStartupAssembliesKey, combineAssembliesName);

        // 实现假的 Starup，解决泛型主机启动问题
        hostBuilder.UseStartup<FakeStartup>();
        return hostBuilder;
    }

    /// <summary>
    /// 泛型主机注入
    /// </summary>
    /// <param name="hostBuilder">泛型主机注入构建器</param>
    /// <param name="configure"></param>
    /// <returns>IHostBuilder</returns>
    public static IHostBuilder Inject(this IHostBuilder hostBuilder, Action<IHostBuilder, InjectOptions> configure = default)
    {
        // 载入服务配置选项
        var configureOptions = new InjectOptions();
        configure?.Invoke(hostBuilder, configureOptions);

        InternalApp.ConfigureApplication(hostBuilder, configureOptions.AutoRegisterBackgroundService);

        return hostBuilder;
    }

    /// <summary>
    /// 注册 IWebHostBuilder 依赖组件
    /// </summary>
    /// <typeparam name="TComponent">派生自 <see cref="IWebComponent"/></typeparam>
    /// <param name="hostBuilder">Web应用构建器</param>
    /// <param name="options">组件参数</param>
    /// <returns><see cref="IWebHostBuilder"/></returns>
    public static IWebHostBuilder AddWebComponent<TComponent>(this IWebHostBuilder hostBuilder, object options = default)
        where TComponent : class, IWebComponent, new()
    {
        hostBuilder.AddWebComponent<TComponent>(options);

        return hostBuilder;
    }

    /// <summary>
    /// 注册 IWebHostBuilder 依赖组件
    /// </summary>
    /// <typeparam name="TComponent">派生自 <see cref="IWebComponent"/></typeparam>
    /// <typeparam name="TComponentOptions">组件参数</typeparam>
    /// <param name="hostBuilder">Web应用构建器</param>
    /// <param name="options">组件参数</param>
    /// <returns><see cref="IWebHostBuilder"/></returns>
    public static IWebHostBuilder AddWebComponent<TComponent, TComponentOptions>(this IWebHostBuilder hostBuilder, TComponentOptions options = default)
        where TComponent : class, IWebComponent, new()
    {
        hostBuilder.AddWebComponent<TComponent, TComponentOptions>(options);

        return hostBuilder;
    }

    /// <summary>
    /// 注册 IWebHostBuilder 依赖组件
    /// </summary>
    /// <param name="hostBuilder"><see cref="IWebHostBuilder"/></param>
    /// <param name="componentType">组件类型</param>
    /// <param name="options">组件参数</param>
    /// <returns><see cref="IWebHostBuilder"/></returns>
    public static IWebHostBuilder AddWebComponent(this IWebHostBuilder hostBuilder, Type componentType, object options = default)
    {
#if NET5_0
        // 创建组件依赖链
        var componentContextLinkList = Penetrates.CreateDependLinkList(componentType, options);

        // 逐条创建组件实例并调用
        foreach (var context in componentContextLinkList)
        {
            // 创建组件实例
            var component = Activator.CreateInstance(context.ComponentType) as IWebComponent;

            // 调用
            component.Load(hostBuilder, context);
        }

#endif

        return hostBuilder;
    }
}