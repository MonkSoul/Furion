// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

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
        return hostBuilder;
    }
}