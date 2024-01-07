// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using Furion.Components;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 组件应用服务集合拓展类
/// </summary>
[SuppressSniffer]
public static class ComponentServiceCollectionExtensions
{
    /// <summary>
    /// 注册单个组件
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <param name="services"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IServiceCollection AddComponent<TComponent>(this IServiceCollection services, object options = default)
        where TComponent : class, IServiceComponent, new()
    {
        return services.AddComponent<TComponent, object>(options);
    }

    /// <summary>
    /// 注册依赖组件
    /// </summary>
    /// <typeparam name="TComponent">派生自 <see cref="IServiceComponent"/></typeparam>
    /// <typeparam name="TComponentOptions">组件参数</typeparam>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="options">组件参数</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddComponent<TComponent, TComponentOptions>(this IServiceCollection services, TComponentOptions options = default)
        where TComponent : class, IServiceComponent, new()
    {
        return services.AddComponent(typeof(TComponent), options);
    }

    /// <summary>
    /// 注册依赖组件
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="componentType">组件类型</param>
    /// <param name="options">组件参数</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddComponent(this IServiceCollection services, Type componentType, object options = default)
    {
        // 创建组件依赖链
        var componentContextLinkList = Penetrates.CreateDependLinkList(componentType, options);

        // 逐条创建组件实例并调用
        foreach (var context in componentContextLinkList)
        {
            // 创建组件实例
            var component = Activator.CreateInstance(context.ComponentType) as IServiceComponent;

            // 调用
            component.Load(services, context);
        }

        return services;
    }
}