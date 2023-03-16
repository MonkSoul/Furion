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