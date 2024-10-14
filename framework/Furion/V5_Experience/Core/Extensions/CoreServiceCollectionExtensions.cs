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

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Furion.Extensions;

/// <summary>
///     核心模块 <see cref="IServiceCollection" /> 拓展类
/// </summary>
public static class CoreServiceCollectionExtensions
{
    /// <summary>
    ///     添加核心模块选项服务
    /// </summary>
    /// <param name="services">
    ///     <see cref="IServiceCollection" />
    /// </param>
    /// <returns>
    ///     <see cref="IServiceCollection" />
    /// </returns>
    public static IServiceCollection AddCoreOptions(this IServiceCollection services)
    {
        // 添加核心模块选项服务
        services.TryAddSingleton(new CoreOptions());

        return services;
    }

    /// <summary>
    ///     获取核心模块选项
    /// </summary>
    /// <param name="services">
    ///     <see cref="IServiceCollection" />
    /// </param>
    /// <returns>
    ///     <see cref="CoreOptions" />
    /// </returns>
    internal static CoreOptions GetCoreOptions(this IServiceCollection services)
    {
        // 添加核心模块选项服务
        services.AddCoreOptions();

        // 获取核心模块选项实例
        var coreOptions = services
            .Single(s => s.ServiceType == typeof(CoreOptions) && s.ImplementationInstance is not null)
            .ImplementationInstance as CoreOptions;

        // 空检查
        ArgumentNullException.ThrowIfNull(coreOptions);

        return coreOptions;
    }

    /// <summary>
    ///     登记组件注册信息
    /// </summary>
    /// <param name="services">
    ///     <see cref="IServiceCollection" />
    /// </param>
    /// <param name="assembly">
    ///     <see cref="Assembly" />
    /// </param>
    internal static void RegisterComponent(this IServiceCollection services, Assembly assembly)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(assembly);

        // 获取核心模块选项
        var coreOptions = services.GetCoreOptions();

        // 组件元数据
        var componentMetadata = new ComponentMetadata(assembly.GetName().Name!
            , assembly.GetVersion()
            , assembly.GetDescription());

        // 登记组件注册信息
        coreOptions.TryRegisterComponent(componentMetadata);
    }

    /// <summary>
    ///     登记组件注册信息
    /// </summary>
    /// <param name="services">
    ///     <see cref="IServiceCollection" />
    /// </param>
    /// <param name="typeInAssembly">
    ///     <see cref="Type" />
    /// </param>
    internal static void RegisterComponent(this IServiceCollection services, Type typeInAssembly)
    {
        // 空检查
        ArgumentNullException.ThrowIfNull(typeInAssembly);

        // 登记组件注册信息
        services.RegisterComponent(typeInAssembly.Assembly);
    }
}