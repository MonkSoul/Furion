// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

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
        where TComponent : class, IApplicationComponent
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
        where TComponent : class, IApplicationComponent
    {
        // 解析组件类型
        var componentType = typeof(TComponent);

        // 初始化组件依赖链
        var dependLinkList = new List<Type> { componentType };
        var componentContextLinkList = new List<ComponentContext>
        {
            new ComponentContext
            {
                Parameter = options,
                ParameterType = typeof(TComponentOptions),
                ComponentType = componentType,
            }
        };

        // 创建组件依赖链
        Penetrates.CreateDependLinkList(componentType, ref dependLinkList, ref componentContextLinkList);

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