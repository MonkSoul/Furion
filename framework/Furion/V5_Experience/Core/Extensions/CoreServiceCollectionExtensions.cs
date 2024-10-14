// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

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