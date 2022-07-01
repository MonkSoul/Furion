// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

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
        where TComponent : class, IServiceComponent
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
        where TComponent : class, IServiceComponent
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